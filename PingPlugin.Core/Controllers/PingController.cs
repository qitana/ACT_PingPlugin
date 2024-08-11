using Newtonsoft.Json;
using System;
using System.Net.NetworkInformation;

namespace Qitana.PingPlugin
{
    internal sealed class PingController : IDisposable
    {
        private Ping ping;
        private byte[] pingBytes = { 0x00 };
        private PingOptions pingOptions = new System.Net.NetworkInformation.PingOptions(128, true);
        private System.Timers.Timer timer;

        public delegate void PingCompletedEvent(PingCompletedEventArgs e);
        public event PingCompletedEvent OnPingCompleted;


        public string Address { get; set; }
        public int Interval { get; set; }
        public int Timeout { get; set; }
        public bool Enabled { get; set; }

        public PingController(string address = "", int interval = 1000, int timeout = 1000, bool enabled = false)
        {
            Address = address;
            Interval = interval;
            Timeout = timeout;
            Enabled = enabled;

            ping = new Ping();

            ping.PingCompleted += (sender, e) =>
            {
                OnPingCompleted(new PingCompletedEventArgs
                {
                    Address = e.Reply.Address.ToString(),
                    RTT = e.Reply.RoundtripTime,
                    Status = e.Reply.Status.ToString(),
                    TTL = (e.Reply.Options != null) ? e.Reply.Options.Ttl : 0,
                    TimestampRaw = ((PingUserToken)e.UserState).Timestamp,
                });
            };

            timer = new System.Timers.Timer()
            {
                Interval = Interval,
                AutoReset = true
            };

            timer.Elapsed += (sender, e) =>
            {
                timer.Interval = Interval;

                if (!Enabled)
                {
                    return;
                }
                if (string.IsNullOrEmpty(Address) || string.IsNullOrWhiteSpace(Address))
                {
                    return;
                }

                try
                {
                    ping.SendAsync(Address, Timeout, pingBytes, pingOptions, new PingUserToken());
                }
                catch (Exception ex)
                {
                    OnPingCompleted(new PingCompletedEventArgs
                    {
                        Status = "Exception",
                        Message = ex.Message
                    });
                }
            };
        }

        public void Start()
        {
            timer?.Start();
        }

        public void Stop()
        {
            timer?.Stop();
        }

        public void Dispose()
        {
            timer?.Stop();
            timer?.Dispose();
        }

        private class PingUserToken
        {
            public DateTimeOffset Timestamp { get; internal set; } = DateTimeOffset.UtcNow;
        }
    }
    public class PingCompletedEventArgs
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
