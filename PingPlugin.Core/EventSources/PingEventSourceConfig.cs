using System;
using Newtonsoft.Json.Linq;
using RainbowMage.OverlayPlugin;

namespace Qitana.PingPlugin
{
    [Serializable]
    public class PingEventSourceConfig
    {
        public bool Enabled { get; set; } = false;
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
            pluginConfig.EventSourceConfigs["qitana.Ping"] = JObject.FromObject(this);
        }

    }

}
