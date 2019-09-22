using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Web.Script.Serialization;

namespace Qitana.PingPlugin
{
    public class PingController : IDisposable
    {
        private Thread pingThread;
        private Thread targetAddressThread;

        internal int PingInterval { get; set; } = 1000;

        internal List<PingResult> PingResults { get; private set; }
        private object pingResultsLock = new object();

        internal Process Process { get; private set; }
        internal IPAddress TargetAddress { get; private set; } = null;
        internal int LatestTTL { get; private set; } = 0;

        private readonly PingOverlay overlay;

        public PingController(PingOverlay overlay, Process process)
        {
            this.overlay = overlay;
            Process = process;
            PingResults = new List<PingResult>();

            targetAddressThread = new Thread(new ThreadStart(UpdateTargetAddress))
            {
                IsBackground = true
            };
            targetAddressThread.Start();

            pingThread = new Thread(new ThreadStart(ExecutePing))
            {
                IsBackground = true
            };
            pingThread.Start();

            overlay.LogInfo("Process found: pid: {0}", process.Id);

        }

        public void Dispose()
        {
            overlay.LogDebug("PingController Instance disposed");
            pingThread.Abort();
            targetAddressThread.Abort();
        }

        private class PingUserToken
        {
            public DateTimeOffset Timestamp { get; internal set; } = DateTimeOffset.UtcNow;
        }
        private void ExecutePing()
        {

            Ping ping = new Ping();
            byte[] bytes = { 0xff };
            int timeout = 1000;
            PingOptions opts = new System.Net.NetworkInformation.PingOptions(128, true);

            ping.PingCompleted -= PingCompleted;
            ping.PingCompleted += PingCompleted;

            while (true)
            {
                Thread.Sleep(PingInterval);

                if (!this.ValidateProcess())
                {
                    Thread.Sleep(1000);
                    return;
                }

                if (TargetAddress != null)
                {
                    ping.SendAsync(TargetAddress, timeout, bytes, opts, new PingUserToken());
                }
            }
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var now = DateTimeOffset.Now;

            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                return;
            }

            if (PingResults == null)
            {
                return;
            }

            lock (pingResultsLock)
            {
                int ttl = (e.Reply.Options != null) ? e.Reply.Options.Ttl : 0;

                this.PingResults.Add(new PingResult
                {
                    TimestampRaw = ((PingUserToken)e.UserState).Timestamp,
                    Address = e.Reply.Address.ToString(),
                    Status = e.Reply.Status.ToString(),
                    RTT = e.Reply.RoundtripTime,
                    TTL = ttl,
                });

                this.PingResults.RemoveAll(x => x.TimestampRaw < now.AddSeconds(-300));


                if (e.Reply.Status == IPStatus.Success)
                {
                    this.LatestTTL = ttl;
                }
            }
        }

        public bool ValidateProcess()
        {
            if (Process == null)
            {
                return false;
            }

            if (Process.HasExited)
            {
                return false;
            }

            return true;
        }

        private void UpdateTargetAddress()
        {
            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                }

                if (!this.ValidateProcess())
                {
                    Thread.Sleep(1000);
                    return;
                }

                var addrs = GetRemoteAddresses();

                if (addrs.Count == 0)
                {
                    continue;
                }

                foreach (var addr in addrs)
                {
                    try
                    {
                        this.TargetAddress = IPAddress.Parse(addr);
                        continue;
                    }
                    catch { }
                }
            }
        }

        private List<string> GetRemoteAddresses()
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

                    if (!Process.HasExited && Process.Id > 0 && Process.Id == o.OwningPid)
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
    }

    public class PingResult
    {
        [ScriptIgnore]
        public DateTimeOffset TimestampRaw { get; set; } = DateTimeOffset.UtcNow;
        public long Timestamp => TimestampRaw.ToUniversalTime().ToUnixTimeSeconds();
        public string Address { get; set; }
        public string Status { get; set; }
        public long RTT { get; set; }
        public int TTL { get; set; }
    }
}
