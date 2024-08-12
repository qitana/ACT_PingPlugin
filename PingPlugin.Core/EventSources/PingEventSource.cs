using Newtonsoft.Json.Linq;
using RainbowMage.OverlayPlugin;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Qitana.PingPlugin
{
    public class PingEventSource : EventSourceBase
    {
        public PingEventSourceConfig Config { get; set; }
        
        private RemoteAddressController remoteAddressController;
        private PingController pingController;
        private FixedSizeQueue<PingCompletedEventArgs> pingResults;


        public PingEventSource(TinyIoCContainer container) : base(container)
        {
#if DEBUG
            Log(LogLevel.Info, "PING: ### DEBUG ENABLED ###");
#endif

            Name = "Ping";
            pingResults = new FixedSizeQueue<PingCompletedEventArgs>(300);

            RegisterEventTypes(new List<string>()
            {
                "onPingStatusUpdateEvent"
            });

            RegisterEventHandler("getPingResults", (msg) =>
            {
                return JArray.FromObject(pingResults.ToArray());
            });
        }

        public override void Start()
        {
            pingController?.Dispose();
            pingController = new PingController("", Config.Interval, Config.Timeout, Config.Enabled);
            pingController.OnPingCompleted += (result) =>
            {
                if (result.Status == "Exception")
                {
                    Log(LogLevel.Warning, "PING: Ping Exception: " + result.Message);
                    return;
                }
                pingResults.Enqueue(result);
                DispatchToJS(new JSEvents.PingStatusUpdateEvent(result.ToJson()));
            };
            pingController.Start();


            remoteAddressController?.Dispose();
            remoteAddressController = new RemoteAddressController();
            remoteAddressController.OnRemoteAddressChanged += (address) =>
            {
                try
                {
                    pingController.Address = address;
#if DEBUG
                    Log(LogLevel.Info, "PING: Remote address updated: " + address);
#endif
                }
                catch (Exception ex)
                {
                    Log(LogLevel.Warning, "PING: Failed to update remote address: " + ex.Message);
                }
            };
            remoteAddressController.Start();

            Log(LogLevel.Info, "PING: Ping event source started");
            
        }

        public override void Stop()
        {
            remoteAddressController?.Stop();
            pingController?.Stop();
            Log(LogLevel.Info, "PING: Ping event source stopped");
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

        protected override void Update()
        {
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
