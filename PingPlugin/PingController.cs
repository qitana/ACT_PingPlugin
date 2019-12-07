using Newtonsoft.Json;
using System;
using System.Net.NetworkInformation;

namespace Qitana.PingPlugin
{
    public sealed class PingController : IDisposable
    {
        PingEventSourceConfig Config;

        private Ping ping;
        private byte[] pingBytes = { 0x00 };
        private PingOptions pingOptions = new System.Net.NetworkInformation.PingOptions(128, true);
        private System.Timers.Timer pingTimer;

        public delegate void PingCompletedEvent(PingResult e);
        public event PingCompletedEvent OnPingCompleted;


        public string Address { get; set; } = string.Empty;
        public int Interval { get; set; } = 1000;
        public int Timeout { get; set; } = 1000;

        public PingController(PingEventSourceConfig config)
        {
            this.Config = config;

            ping = new Ping();
            ping.PingCompleted += (sender, e) =>
            {
                OnPingCompleted(new PingResult()
                {

                    Address = e.Reply.Address.ToString(),
                    RTT = e.Reply.RoundtripTime,
                    Status = e.Reply.Status.ToString(),
                    TTL = (e.Reply.Options != null) ? e.Reply.Options.Ttl : 0,
                    TimestampRaw = ((PingUserToken)e.UserState).Timestamp,

                });
            };

            pingTimer = new System.Timers.Timer()
            {
                Interval = this.Interval,
                AutoReset = true
            };
            pingTimer.Elapsed += (sender, e) =>
            {
                pingTimer.Interval = this.Config.Interval;
                if (string.IsNullOrWhiteSpace(Address))
                {
                    return;
                }

                try
                {
                    ping.SendAsync(Address, this.Timeout, pingBytes, pingOptions, new PingUserToken());
                }
                catch (Exception ex)
                {
                    OnPingCompleted(new PingResult()
                    {
                        Status = "Exception",
                        Message = ex.Message
                    });
                }
            };

        }

        public void Start()
        {
            pingTimer?.Start();
        }

        public void Stop()
        {
            pingTimer?.Stop();
        }

        public void Dispose()
        {
            pingTimer?.Stop();
            pingTimer?.Dispose();
        }

        private class PingUserToken
        {
            public DateTimeOffset Timestamp { get; internal set; } = DateTimeOffset.UtcNow;
        }
    }

    public class PingResult
    {
        [JsonIgnore]
        public DateTimeOffset TimestampRaw { get; set; } = DateTimeOffset.UtcNow;
        public long Timestamp => TimestampRaw.ToUniversalTime().ToUnixTimeSeconds();
        public string Address { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public string Message { get; set; }
        public long RTT { get; set; }
        public int TTL { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
