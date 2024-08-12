using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.NetworkInformation;

namespace Qitana.PingPlugin
{
    internal sealed class PingController : IDisposable
    {
        PingEventSourceConfig Config;

        private Ping ping;
        private byte[] pingBytes = { 0x00 };
        private PingOptions pingOptions = new PingOptions(128, true);
        private System.Timers.Timer timer;
        private string lastErroredAddress = null;

        public delegate void PingEvent(PingEventArgs e);
        public event PingEvent OnPingCompleted;

        public PingController(PingEventSourceConfig config)
        {
            Config = config;

            ping = new Ping();

            ping.PingCompleted += (sender, e) =>
            {
                OnPingCompleted(new PingEventArgs
                {
                    Address = e?.Reply?.Address?.ToString() ?? null,
                    RTT = e?.Reply?.RoundtripTime ?? -1,
                    Status = e?.Reply?.Status.ToString() ?? null,
                    TTL = e?.Reply?.Options?.Ttl ?? -1,
                    TimestampRaw = ((PingUserToken)e.UserState)?.Timestamp ?? DateTimeOffset.UtcNow
                });
            };

            timer = new System.Timers.Timer()
            {
                Interval = Config.Interval,
                AutoReset = true
            };

            timer.Elapsed += (sender, e) =>
            {
                timer.Interval = Config.Interval;

                // If the EventSource is disabled, return
                if (!Config.Enabled)
                {
                    return;
                }

                // Copy the remote address to a new string
                var remoteAddress = new string(Config.RemoteAddress.ToCharArray());

                // If the remote address is empty, return
                if (string.IsNullOrEmpty(remoteAddress) || string.IsNullOrWhiteSpace(remoteAddress))
                {
                    return;
                }

                // If the remote address is the same as the last errored address, return
                if (remoteAddress == lastErroredAddress)
                {
                    return;
                }
                else
                {
                    lastErroredAddress = null;
                }

                // If the remote address is not an IP address, return
                if (!IPAddress.TryParse(remoteAddress, out IPAddress address))
                {
                    lastErroredAddress = remoteAddress;
                    OnPingCompleted(new PingEventArgs
                    {
                        Status = "Exception",
                        Message = $"{remoteAddress} is not a valid IP address."
                    });
                    return;
                }

                try
                {
                    ping.SendAsync(remoteAddress, Config.Timeout, pingBytes, pingOptions, new PingUserToken());
                }
                catch (Exception ex)
                {
                    OnPingCompleted(new PingEventArgs
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
    public class PingEventArgs
    {
        [JsonIgnore]
        public DateTimeOffset TimestampRaw { get; set; } = DateTimeOffset.UtcNow;
        public long Timestamp => TimestampRaw.ToUniversalTime().ToUnixTimeSeconds();
        public string Address { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public string Message { get; set; }
        public long RTT { get; set; } = -1;
        public int TTL { get; set; } = -1;

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
