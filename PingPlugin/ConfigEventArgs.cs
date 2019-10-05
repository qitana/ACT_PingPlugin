using System;

namespace Qitana.PingPlugin
{
    public class PingIntervalChangedEventArgs : EventArgs
    {
        public int NewPingInterval { get; private set; }
        public PingIntervalChangedEventArgs(int newPingInterval)
        {
            this.NewPingInterval = newPingInterval;
        }
    }

    public class FollowFFXIVPluginChangedEventArgs : EventArgs
    {
        public bool NewFollowFFXIVPlugin { get; private set; }
        public FollowFFXIVPluginChangedEventArgs(bool newValue)
        {
            this.NewFollowFFXIVPlugin = newValue;
        }
    }
}
