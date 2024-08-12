using System;
using Newtonsoft.Json.Linq;
using RainbowMage.OverlayPlugin;

namespace Qitana.PingPlugin
{
    [Serializable]
    public class PingEventSourceConfig
    {
        public bool Enabled { get; set; } = false;
        public bool TrackFFXIVRemoteAddress { get; set; } = false;
        public string RemoteAddress { get; set; } = "";
        public int Interval { get; set; } = 1000;
        public int Timeout { get; set; } = 1000;
        
        public static PingEventSourceConfig LoadConfig(IPluginConfig pluginConfig)
        {
            var result = new PingEventSourceConfig();

            if (pluginConfig.EventSourceConfigs.ContainsKey("qitana.Ping"))
            {
                var obj = pluginConfig.EventSourceConfigs["qitana.Ping"];

                if (obj.TryGetValue("Enabled", out JToken enabled))
                {
                    result.Enabled = enabled.ToObject<bool>();
                }
                
                if (obj.TryGetValue("TrackFFXIVRemoteAddress", out JToken trackFFXIVRemoteAddress))
                {
                   result.TrackFFXIVRemoteAddress = trackFFXIVRemoteAddress.ToObject<bool>();
                }

                if (obj.TryGetValue("RemoteAddress", out JToken remoteAddress))
                {
                    result.RemoteAddress = remoteAddress.ToObject<string>();
                }

                if (obj.TryGetValue("Interval", out JToken interval))
                {
                    result.Interval = interval.ToObject<int>();
                }

                if (obj.TryGetValue("Timeout", out JToken timeout))
                {
                    result.Timeout = timeout.ToObject<int>();
                }
            }

            return result;
        }

        public void SaveConfig(IPluginConfig pluginConfig)
        {
            if(TrackFFXIVRemoteAddress == true)
            {
                RemoteAddress = "";
            }

            pluginConfig.EventSourceConfigs["qitana.Ping"] = JObject.FromObject(this);
        }

    }

}
