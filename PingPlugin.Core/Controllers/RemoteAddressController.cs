using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qitana.PingPlugin
{
    internal sealed class RemoteAddressController: IDisposable
    {
        private System.Timers.Timer timer;

        public delegate void RemoteAddressChangedEvent(string address);
        public event RemoteAddressChangedEvent OnRemoteAddressChanged;
        public string RemoteAddress { get; private set; }

        public RemoteAddressController()
        {
            timer = new System.Timers.Timer()
            {
                Interval = 2500,
                AutoReset = true
            };

            timer.Elapsed += (sender, e) =>
            {
                string address = FFXIVProcessHelper.GetFFXIVRemoteAddresses().FirstOrDefault();
                if (address != RemoteAddress)
                {
                    RemoteAddress = address;
                    OnRemoteAddressChanged(address);

                    if(string.IsNullOrEmpty(address))
                    {
                        timer.Interval = 2500;
                    }
                    else
                    {
                        timer.Interval = 15000;
                    }
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
    }
}
