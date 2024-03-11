using Newtonsoft.Json.Linq;
using RainbowMage.OverlayPlugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qitana.PingPlugin
{
    public class PingEventSource : EventSourceBase
    {
        public PingEventSourceConfig Config { get; private set; }

        private PingController pingController;
        private System.Timers.Timer processCheckTimer;

        public PingEventSource(ILogger logger) : base (logger)
        {
            Name = "Ping";

            RegisterEventTypes(new List<string>()
            {
                "onPingStatusUpdateEvent"
            });
        }

        public override void Start()
        {
            //base.Start();
            pingController?.Dispose();
            pingController = new PingController(this.Config);
            pingController.OnPingCompleted += (e) =>
            {
                DispatchToJS(new JSEvents.PingStatusUpdateEvent(e.ToJson()));
            };

            //processCheckTimer
            processCheckTimer = new System.Timers.Timer()
            {
                Interval = 5000,
                AutoReset = true,
            };
            processCheckTimer.Elapsed += (sender, e) =>
            {
                pingController.Address = FFXIVRemoteAddress;
            };
            processCheckTimer.Start();
            pingController.Start();

            logger.Log(LogLevel.Info, "Ping: Started.");
        }

        public override void Stop()
        {
            //base.Stop();
            pingController?.Stop();
            processCheckTimer?.Stop();
        }

        protected override void Update()
        {

        }

        protected override void Dispose(bool disposing)
        {
            pingController?.Stop();
            pingController?.Dispose();
            processCheckTimer?.Stop();
            processCheckTimer?.Dispose();
            base.Dispose();
        }

        private string FFXIVRemoteAddress => GetRemoteAddresses(FFXIVProcessHelper.GetFFXIVProcess).FirstOrDefault();

        private List<string> GetRemoteAddresses(Process process)
        {
            List<string> result = new List<string>();

            int size = 0;
            uint AF_INET = 2; // IPv4

            //必要サイズの取得            
            NativeMethods.GetExtendedTcpTable(IntPtr.Zero, ref size, true, AF_INET, NativeMethods.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

            //メモリ割当て
            var p = Marshal.AllocHGlobal(size);

            //TCPテーブルの取得            
            if (NativeMethods.GetExtendedTcpTable(p, ref size, true, AF_INET, NativeMethods.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0) == 0)
            {
                var num = Marshal.ReadInt32(p); //MIB_TCPTABLE_OWNER_PID.dwNumEntries(データ数)
                var ptr = IntPtr.Add(p, 4);
                for (int i = 0; i < num; i++)
                {
                    var o = (NativeMethods.MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(ptr, typeof(NativeMethods.MIB_TCPROW_OWNER_PID));
                    if (o.RemoteAddr == 0)
                    {
                        o.RemotePort = 0;
                    }

                    if (!process.HasExited && process.Id > 0 && process.Id == o.OwningPid)
                    {
                        if (NativeMethods.StateStrings[o.State] == "ESTABLISHED")
                        {
                            var b = BitConverter.GetBytes(o.RemoteAddr);
                            var remoteAddr = string.Format("{0}.{1}.{2}.{3}", b[0], b[1], b[2], b[3]);

                            if (remoteAddr == "0.0.0.0" || remoteAddr == "127.0.0.1")
                            {
                                continue;
                            }

                            result.Add(remoteAddr);
                        }
                    }

                    ptr = IntPtr.Add(ptr, Marshal.SizeOf(typeof(NativeMethods.MIB_TCPROW_OWNER_PID)));//次のデータ
                }
                Marshal.FreeHGlobal(p);  //メモリ開放
            }

            // 重複排除
            return result.Distinct().ToList();
        }


        public override Control CreateConfigControl()
        {
            return new PingEventSourceConfigPanel(this);
        }

        public override void LoadConfig(IPluginConfig config)
        {
            Config = PingEventSourceConfig.LoadConfig(config);
        }

        public override void SaveConfig(IPluginConfig config)
        {
            Config.SaveConfig(config);
        }

        public void DispatchToJS(JSEvent e)
        {
            JObject ev = new JObject();
            ev["type"] = e.EventName();
            ev["detail"] = JObject.FromObject(e);
            DispatchEvent(ev);
        }
    }

    public interface JSEvent
    {
        string EventName();
    };

    public class JSEvents
    {
        public class PingStatusUpdateEvent : JSEvent
        {
            public string statusJson;
            public PingStatusUpdateEvent(string statusJson) { this.statusJson = statusJson; }
            public string EventName() { return "onPingStatusUpdateEvent"; }
        }
    }
}
