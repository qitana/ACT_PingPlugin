using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Script.Serialization;
using RainbowMage.OverlayPlugin;

namespace Qitana.PingPlugin
{
    [Serializable()]

    public class PingOverlay : OverlayBase<PingOverlayConfig>
    {
        private PingController pingController = null;
        private bool suppress_log = false;
        private bool isDebug = false;
        private object _lock = new object();

        public PingOverlay(PingOverlayConfig config) : base(config, config.Name)
        {
            if (config.Name.Equals("PingDebug"))
            {
                isDebug = true;
            }
        }

        public override void Dispose()
        {
            this.xivWindowTimer.Enabled = false;
            this.timer.Enabled = false;
            this.pingController?.Dispose();
            base.Dispose();
        }

        public void ChangeProcessId(int processId)
        {
            lock (_lock)
            {
                Process p = null;

                if (Config.FollowFFXIVPlugin)
                {
                    if (FFXIVPluginHelper.Instance != null)
                    {
                        p = FFXIVPluginHelper.GetFFXIVProcess;
                    }
                }
                else
                {
                    p = FFXIVProcessHelper.GetFFXIVProcess(processId);
                }

                if ((pingController == null && p != null) ||
                    (pingController != null && p != null && p.Id != pingController.Process.Id))
                {
                    try
                    {
                        pingController = new PingController(this, p);
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                        suppress_log = true;
                        pingController = null;
                    }
                }
                else if (pingController != null && p == null)
                {
                    pingController.Dispose();
                    pingController = null;
                }
            }
        }

        public void LogDebug(string format, params object[] args)
        {
            string prefix = isDebug ? "DEBUG: " : "";
            LogLevel level = isDebug ? LogLevel.Info : LogLevel.Debug;
            Log(level, prefix + format, args);
        }

        public void LogError(string format, params object[] args)
        {
            if (suppress_log == false)
            {
                Log(LogLevel.Error, format, args);
            }
        }

        public void LogWarning(string format, params object[] args)
        {
            if (suppress_log == false)
            {
                Log(LogLevel.Warning, format, args);
            }
        }

        public void LogInfo(string format, params object[] args)
            => Log(LogLevel.Info, format, args);

        /// <summary>
        /// プロセスの有効性をチェック
        /// </summary>
        private void CheckProcessId()
        {
            try
            {
                if (Config.FollowFFXIVPlugin)
                {
                    Process p = null;
                    if (FFXIVPluginHelper.Instance != null)
                    {
                        p = FFXIVPluginHelper.GetFFXIVProcess;
                        if (p == null || (pingController != null && pingController.Process.Id != p.Id))
                        {
                            pingController?.Dispose();
                            pingController = null;
                        }
                    }
                }

                if (pingController == null)
                {
                    ChangeProcessId(0);
                }
                else if (pingController.ValidateProcess())
                {
                    // スキャン間隔をもどす
                    if (timer.Interval != this.Config.PingInterval)
                    {
                        timer.Interval = this.Config.PingInterval;
                    }

                    if (suppress_log == true)
                    {
                        suppress_log = false;
                    }
                }
                else
                {
                    pingController?.Dispose();
                    pingController = null;
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        protected override void Update()
        {
            int delay = 3000;
            try
            {
                // プロセスチェック
                CheckProcessId();

                if (pingController == null)
                {
                    // スキャン間隔を一旦遅くする
                    timer.Interval = delay;
                    if (suppress_log == false)
                    {
                        suppress_log = true;
                        LogWarning(Messages.ProcessNotFound);
                        LogDebug(Messages.UpdateScanInterval, delay);
                    }
                }

                string updateScript = CreateEventDispatcherScript();
                if (this.Overlay != null &&
                    this.Overlay.Renderer != null &&
                    this.Overlay.Renderer.Browser != null)
                {
                    this.Overlay.Renderer.Browser.GetMainFrame().ExecuteJavaScript(updateScript, null, 0);
                }
            }
            catch (Exception ex)
            {
                LogError("Update: {0} {1}", this.Name, ex.ToString());
            }
        }

        /// <summary>
        /// データを取得し、JSONを作る
        /// </summary>
        /// <returns></returns>
        internal string CreateJsonData()
        {
            // シリアライザ
            var serializer = new JavaScriptSerializer();


            // Overlay に渡すオブジェクトを作成
            PingResultsObject pingResultsObject = new PingResultsObject();

            // なんかプロセスがおかしいとき
            if (pingController == null || pingController.ValidateProcess() == false)
            {
                return serializer.Serialize(pingResultsObject);
            }

            try
            {
                pingResultsObject.CurrentAddress = pingController.TargetAddress == null ? "" : pingController.TargetAddress.ToString();
                pingResultsObject.CurrentTTL = pingController.LatestTTL;
                pingResultsObject.PingResults = new List<PingResult>(pingController.PingResults);

                pingResultsObject.Stats10 = pingResultsObject.CalcStats(10);
                pingResultsObject.Stats30 = pingResultsObject.CalcStats(30);
                pingResultsObject.Stats60 = pingResultsObject.CalcStats(60);
                pingResultsObject.Stats120 = pingResultsObject.CalcStats(120);
                pingResultsObject.Stats180 = pingResultsObject.CalcStats(180);
                pingResultsObject.Stats300 = pingResultsObject.CalcStats(300);

            }
            catch (Exception ex)
            {
                LogError("Update: {1}", this.Name, ex);
            }
            return serializer.Serialize(pingResultsObject);
        }


        private string CreateEventDispatcherScript()
            => "var ActXiv = { 'PingData': " + this.CreateJsonData() + " };\n" +
               "document.dispatchEvent(new CustomEvent('onOverlayDataUpdate', { detail: ActXiv }));";

        /// <summary>
        /// スキャン間隔を更新する
        /// </summary>
        public void UpdateScanInterval()
        {
            timer.Interval = this.Config.PingInterval / 5;
            if (pingController != null)
            {
                pingController.PingInterval = this.Config.PingInterval;
            }
            LogDebug(Messages.UpdateScanInterval, this.Config.PingInterval);
        }

        /// <summary>
        /// スキャンを開始する
        /// </summary>
        public new void Start()
        {
            if (OverlayAddonMain.UpdateMessage != String.Empty)
            {
                LogInfo(OverlayAddonMain.UpdateMessage);
                OverlayAddonMain.UpdateMessage = String.Empty;
            }
            if (this.Config.IsVisible == false)
            {
                return;
            }
            LogInfo(Messages.StartScanning);
            suppress_log = false;
            timer.Start();
        }

        /// <summary>
        /// スキャンを停止する
        /// </summary>
        public new void Stop()
        {
            if (timer.Enabled)
            {
                timer.Stop();
                LogInfo(Messages.StopScanning);
            }
        }

        protected override void InitializeTimer() => base.InitializeTimer();

        //// JSON用オブジェクト
        private class PingResultsObject
        {
            public long Timestamp { get; internal set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            public string CurrentAddress { get; internal set; } = string.Empty;
            public int CurrentTTL { get; internal set; } = 0;
            public List<PingResult> PingResults { get; internal set; } = new List<PingResult>();

            public Stats Stats10 { get; internal set; } = new Stats();
            public Stats Stats30 { get; internal set; } = new Stats();
            public Stats Stats60 { get; internal set; } = new Stats();
            public Stats Stats120 { get; internal set; } = new Stats();
            public Stats Stats180 { get; internal set; } = new Stats();
            public Stats Stats300 { get; internal set; } = new Stats();

            public class Stats
            {
                public double LossRate { get; internal set; } = 0.0d;
                public double RTT { get; internal set; } = 0.0d;
                public double Jitter { get; internal set; } = 0.0d;
            }

            public Stats CalcStats(int interval)
            {
                var stats = new Stats();

                if (this.PingResults == null)
                {
                    return stats;
                }

                // interval で指定されたデータを抽出
                var data = this.PingResults.Where(x => x.TimestampRaw >= DateTimeOffset.UtcNow.AddSeconds(-1 * interval));
                // dataのうち、成功したもの
                var data_success = data.Where(x => x.Status == "Success");

                // LossRate 計算
                int cnt = 0;
                int cnt_success = 0;

                if (data != null)
                {
                    cnt = data.Count();
                }

                if (data_success != null)
                {
                    cnt_success = data_success.Count();
                }

                if (cnt != 0)
                {
                    stats.LossRate = (1.00d - (double)cnt_success / (double)cnt) * 100.0d;
                }
                else
                {
                    stats.LossRate = 0.0d;
                }

                // RTT計算
                try
                {
                    stats.RTT = data_success.Average(x => x.RTT);
                }
                catch
                {
                    stats.RTT = 0.0d;
                }

                //Jitter計算
                long jitterSum = 0;
                int jitterCnt = 0;
                var data_success_list = data_success.OrderBy(x => x.TimestampRaw).ToList();
                for (int i = 0; i < data_success_list.Count; i++)
                {
                    if (data_success_list[i] == data_success.Last())
                    {
                        break;
                    }

                    var current = data_success_list[i].RTT;
                    var next = data_success_list[i + 1].RTT;

                    jitterSum += Math.Abs(current - next);
                    jitterCnt++;
                }

                if (jitterCnt > 0)
                {
                    stats.Jitter = (double)jitterSum / jitterCnt;
                }
                else
                {
                    stats.Jitter = 0.0d;
                }

                return stats;
            }
        }
    }
}
