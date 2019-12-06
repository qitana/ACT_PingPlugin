using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qitana.PingPlugin
{
    public partial class PingEventSourceConfigPanel : UserControl
    {
        private PingEventSource source;
        private PingEventSourceConfig config;
        public PingEventSourceConfigPanel(PingEventSource source)
        {
            InitializeComponent();

            this.source = source;
            this.config = source.Config;

            SetupControlProperties();
        }

        private void SetupControlProperties()
        {
            this.checkBox_Enabled.Checked = config.Enabled;
            this.numericUpDown_Interval.Value = config.Interval;
            this.numericUpDown_Timeout.Value = config.Timeout;
        }


        private void checkBox_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            this.config.Enabled = this.checkBox_Enabled.Checked;
        }

        private void numericUpDown_Interval_ValueChanged(object sender, EventArgs e)
        {
            this.config.Interval = Decimal.ToInt32(this.numericUpDown_Interval.Value);
        }

        private void numericUpDown_Timeout_ValueChanged(object sender, EventArgs e)
        {
            this.config.Timeout = Decimal.ToInt32(this.numericUpDown_Timeout.Value);
        }
    }
}
