using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using RainbowMage.OverlayPlugin;
using System.Diagnostics;

namespace Qitana.PingPlugin
{
    public partial class PingOverlayConfigPanel : UserControl
    {
        private PingOverlay overlay;
        private PingOverlayConfig config;

        public PingOverlayConfigPanel(PingOverlay overlay)
        {
            InitializeComponent();

            this.overlay = overlay;
            this.config = overlay.Config;

            SetupControlProperties();
            SetupConfigEventHandlers();
            RefreshProcessList();
            this.comboProcessList.SelectedItem = "Automatic";
        }

        private void SetupControlProperties()
        {
            this.checkPingVisible.Checked = this.config.IsVisible;
            this.checkPingClickThru.Checked = this.config.IsClickThru;
            this.checkPingLock.Checked = this.config.IsLocked;
            this.textPingUrl.Text = this.config.Url;
            this.nudPingMaxFrameRate.Value = this.config.MaxFrameRate;
            this.nudPingInterval.Value = this.config.PingInterval;
            this.checkPingEnableGlobalHotkey.Checked = this.config.GlobalHotkeyEnabled;
            this.textPingGlobalHotkey.Enabled = this.checkPingEnableGlobalHotkey.Checked;
            this.textPingGlobalHotkey.Text = GetHotkeyString(this.config.GlobalHotkeyModifiers, this.config.GlobalHotkey);
            this.checkFollowFFXIVPlugin.Checked = this.config.FollowFFXIVPlugin;
        }

        private void SetupConfigEventHandlers()
        {
            this.config.VisibleChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.checkPingVisible.Checked = e.IsVisible;
                });
            };
            this.config.ClickThruChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.checkPingClickThru.Checked = e.IsClickThru;
                });
            };
            this.config.LockChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.checkPingLock.Checked = e.IsLocked;
                });
            };
            this.config.UrlChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.textPingUrl.Text = e.NewUrl;
                });
            };
            this.config.MaxFrameRateChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.nudPingMaxFrameRate.Value = e.NewFrameRate;
                });
            };
            this.config.PingIntervalChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.nudPingInterval.Value = e.NewPingInterval;
                });
            };
            this.config.GlobalHotkeyEnabledChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.checkPingEnableGlobalHotkey.Checked = e.NewGlobalHotkeyEnabled;
                    this.textPingGlobalHotkey.Enabled = this.checkPingEnableGlobalHotkey.Checked;
                });
            };
            this.config.GlobalHotkeyChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.textPingGlobalHotkey.Text = GetHotkeyString(this.config.GlobalHotkeyModifiers, e.NewHotkey);
                });
            };
            this.config.GlobalHotkeyModifiersChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.textPingGlobalHotkey.Text = GetHotkeyString(e.NewHotkey, this.config.GlobalHotkey);
                });
            };
            this.config.FollowFFXIVPluginChanged += (o, e) =>
            {
                this.InvokeIfRequired(() =>
                {
                    this.checkFollowFFXIVPlugin.Checked = e.NewFollowFFXIVPlugin;
                });
            };
        }

        private void InvokeIfRequired(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void checkPingVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.config.IsVisible = this.checkPingVisible.Checked;
            if (this.overlay != null)
            {
                if (this.config.IsVisible == true) {
                    this.overlay.Start();
                }
                else
                {
                    this.overlay.Stop();
                }
            }
        }

        private void checkPingClickThru_CheckedChanged(object sender, EventArgs e)
        {
            this.config.IsClickThru = this.checkPingClickThru.Checked;
        }

        private void checkPingLock_CheckedChanged(object sender, EventArgs e)
        {
            this.config.IsLocked = this.checkPingLock.Checked;
        }

        private void textPingUrl_TextChanged(object sender, EventArgs e)
        {
            this.config.Url = this.textPingUrl.Text;
        }

        private void buttonPingSelectFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.config.Url = new Uri(ofd.FileName).ToString();
            }
        }

        private void buttonPingCopyActXiv_Click(object sender, EventArgs e)
        {
            var json = this.overlay.CreateJsonData();
            if (!string.IsNullOrWhiteSpace(json))
            {
                Clipboard.SetText("var ActXiv = { 'PingData': " + json + " };\n");
            }
        }

        private void buttonPingReloadBrowser_Click(object sender, EventArgs e)
        {
            this.overlay.Navigate(this.config.Url);
        }

        private void nudPingMaxFrameRate_ValueChanged(object sender, EventArgs e)
        {
            this.config.MaxFrameRate = (int)nudPingMaxFrameRate.Value;
        }

        private void nudPingInterval_ValueChanged(object sender, EventArgs e)
        {
            this.config.PingInterval = (int)nudPingInterval.Value;
            if (this.overlay != null)
            {
                this.overlay.UpdateScanInterval();
            }
        }

        private void checkPingEnableGlobalHotkey_CheckedChanged(object sender, EventArgs e)
        {
            this.config.GlobalHotkeyEnabled = this.checkPingEnableGlobalHotkey.Checked;
            this.textPingGlobalHotkey.Enabled = this.config.GlobalHotkeyEnabled;
        }

        private void textPingGlobalHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            var key = RemoveModifiers(e.KeyCode, e.Modifiers);
            this.config.GlobalHotkey = key;
            this.config.GlobalHotkeyModifiers = e.Modifiers;
        }

        private void checkFollowFFXIVPlugin_CheckedChanged(object sender, EventArgs e)
        {
            this.config.FollowFFXIVPlugin = this.checkFollowFFXIVPlugin.Checked;
            if (this.config.FollowFFXIVPlugin)
            {
                this.comboProcessList.Enabled = false;
                this.buttonRefreshProcessList.Enabled = false;
            }
            else
            {
                this.comboProcessList.Enabled = true;
                this.buttonRefreshProcessList.Enabled = true;
            }
        }

        /// <summary>
        ///   Generates human readable keypress string
        ///   人間が読めるキー押下文字列を生成します
        /// </summary>
        /// <param name="Modifier"></param>
        /// <param name="key"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        private string GetHotkeyString(Keys Modifier, Keys key, String defaultText = "")
        {
            StringBuilder sbKeys = new StringBuilder();
            if ((Modifier & Keys.Shift) == Keys.Shift)
            {
                sbKeys.Append("Shift + ");
            }
            if ((Modifier & Keys.Control) == Keys.Control)
            {
                sbKeys.Append("Ctrl + ");
            }
            if ((Modifier & Keys.Alt) == Keys.Alt)
            {
                sbKeys.Append("Alt + ");
            }
            if ((Modifier & Keys.LWin) == Keys.LWin || (Modifier & Keys.RWin) == Keys.RWin)
            {
                sbKeys.Append("Win + ");
            }
            sbKeys.Append(Enum.ToObject(typeof(Keys), key).ToString());
            return sbKeys.ToString();
        }

        /// <summary>
        ///  Removes stray references to Left/Right shifts, etc and modifications of the actual key value caused by bitwise operations
        ///  ビット単位の操作に起因する左/右シフト、などと実際のキー値の変更に浮遊の参照を削除します。
        /// </summary>
        /// <param name="KeyCode"></param>
        /// <param name="Modifiers"></param>
        /// <returns></returns>
        private Keys RemoveModifiers(Keys KeyCode, Keys Modifiers)
        {
            var key = KeyCode;
            var modifiers = new List<Keys>() { Keys.ControlKey, Keys.LControlKey, Keys.Alt, Keys.ShiftKey, Keys.Shift, Keys.LShiftKey, Keys.RShiftKey, Keys.Control, Keys.LWin, Keys.RWin };
            foreach (var mod in modifiers)
            {
                if (key.HasFlag(mod))
                {
                    if (key == mod)
                        key &= ~mod;
                }
            }
            return key;
        }

        /// <summary>
        /// Refresh Process list
        /// </summary>
        private void RefreshProcessList()
        {
            this.comboProcessList.Items.Clear();
            this.comboProcessList.Items.Add("Automatic");
            IList<Process> fFXIVProcessList = FFXIVProcessHelper.GetFFXIVProcessList();
            foreach (Process current in fFXIVProcessList)
            {
                this.comboProcessList.Items.Add(current.Id.ToString());
            }
        }

        private void buttonRefreshProcessList_Click(object sender, EventArgs e)
        {
            object selectedItem = this.comboProcessList.SelectedItem;
            this.RefreshProcessList();
            if (this.comboProcessList.Items.Contains(selectedItem))
            {
                this.comboProcessList.SelectedItem = selectedItem;
                return;
            }
            this.comboProcessList.SelectedItem = "Automatic";
        }

        private void comboProcessList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.overlay != null)
            {
                string s = ((string)this.comboProcessList.SelectedItem) ?? "";
                int processID = 0;
                int.TryParse(s, out processID);
                this.overlay.ChangeProcessId(processID);
            }
        }
    }
}
