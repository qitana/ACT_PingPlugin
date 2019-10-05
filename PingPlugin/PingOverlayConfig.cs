using System;
using System.Xml.Serialization;
using RainbowMage.OverlayPlugin;

namespace Qitana.PingPlugin
{
    [Serializable]
    public class PingOverlayConfig : OverlayConfigBase
    {
        public event EventHandler<PingIntervalChangedEventArgs> PingIntervalChanged;
        public event EventHandler<FollowFFXIVPluginChangedEventArgs> FollowFFXIVPluginChanged;

        public PingOverlayConfig(string name)
            : base(name)
        {
            this._pingInterval = 1000;
            this.Url = new Uri(System.IO.Path.Combine(OverlayAddonMain.ResourcesDirectory, @"PingPlugin\pingStatus.html")).ToString();
            this._followFFXIVPlugin = false;
        }

        private PingOverlayConfig()
            : base(null)
        {
        }

        public override Type OverlayType => typeof(PingOverlay);

        private int _pingInterval;
        [XmlElement("PingInterval")]
        public int PingInterval
        {
            get
            {
                return this._pingInterval;
            }
            set
            {
                if (this._pingInterval != value)
                {
                    this._pingInterval = value;
                    PingIntervalChanged?.Invoke(this, new PingIntervalChangedEventArgs(this._pingInterval));
                }
            }
        }

        private bool _followFFXIVPlugin;
        [XmlElement("FollowFFXIVPlugin")]
        public bool FollowFFXIVPlugin
        {
            get
            {
                return this._followFFXIVPlugin;
            }
            set
            {
                if (this._followFFXIVPlugin != value)
                {
                    this._followFFXIVPlugin = value;
                    FollowFFXIVPluginChanged?.Invoke(this, new FollowFFXIVPluginChangedEventArgs(this._followFFXIVPlugin));
                }
            }
        }
    }
}
