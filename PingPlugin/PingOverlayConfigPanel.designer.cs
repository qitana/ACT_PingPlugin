namespace Qitana.PingPlugin
{
    partial class PingOverlayConfigPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PingOverlayConfigPanel));
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label_Process = new System.Windows.Forms.Label();
            this.table_Process = new System.Windows.Forms.TableLayoutPanel();
            this.comboProcessList = new System.Windows.Forms.ComboBox();
            this.buttonRefreshProcessList = new System.Windows.Forms.Button();
            this.checkFollowFFXIVPlugin = new System.Windows.Forms.CheckBox();
            this.label_ShowOverlay = new System.Windows.Forms.Label();
            this.checkPingVisible = new System.Windows.Forms.CheckBox();
            this.label_Clickthru = new System.Windows.Forms.Label();
            this.checkPingClickThru = new System.Windows.Forms.CheckBox();
            this.label_LockOverlay = new System.Windows.Forms.Label();
            this.checkPingLock = new System.Windows.Forms.CheckBox();
            this.label_URL = new System.Windows.Forms.Label();
            this.table_URL = new System.Windows.Forms.TableLayoutPanel();
            this.textPingUrl = new System.Windows.Forms.TextBox();
            this.buttonPingSelectFile = new System.Windows.Forms.Button();
            this.label_PingInterval = new System.Windows.Forms.Label();
            this.nudPingInterval = new System.Windows.Forms.NumericUpDown();
            this.label_Hotkey = new System.Windows.Forms.Label();
            this.table_Hotkey = new System.Windows.Forms.TableLayoutPanel();
            this.checkPingEnableGlobalHotkey = new System.Windows.Forms.CheckBox();
            this.textPingGlobalHotkey = new System.Windows.Forms.TextBox();
            this.label_Framerate = new System.Windows.Forms.Label();
            this.nudPingMaxFrameRate = new System.Windows.Forms.NumericUpDown();
            this.label_Help = new System.Windows.Forms.Label();
            this.panel_Buttons = new System.Windows.Forms.Panel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPingCopyActXiv = new System.Windows.Forms.Button();
            this.buttonPingReloadBrowser = new System.Windows.Forms.Button();
            this.tableLayoutPanel7.SuspendLayout();
            this.table_Process.SuspendLayout();
            this.table_URL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPingInterval)).BeginInit();
            this.table_Hotkey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPingMaxFrameRate)).BeginInit();
            this.panel_Buttons.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.label_Process, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.table_Process, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label_ShowOverlay, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.checkPingVisible, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label_Clickthru, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.checkPingClickThru, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label_LockOverlay, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.checkPingLock, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.label_URL, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.table_URL, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.label_PingInterval, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.nudPingInterval, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.label_Hotkey, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.table_Hotkey, 1, 6);
            this.tableLayoutPanel7.Controls.Add(this.label_Framerate, 0, 7);
            this.tableLayoutPanel7.Controls.Add(this.nudPingMaxFrameRate, 1, 7);
            this.tableLayoutPanel7.Controls.Add(this.label_Help, 0, 8);
            this.tableLayoutPanel7.Controls.Add(this.panel_Buttons, 1, 9);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // label_Process
            // 
            resources.ApplyResources(this.label_Process, "label_Process");
            this.label_Process.Name = "label_Process";
            // 
            // table_Process
            // 
            resources.ApplyResources(this.table_Process, "table_Process");
            this.table_Process.Controls.Add(this.comboProcessList, 0, 0);
            this.table_Process.Controls.Add(this.buttonRefreshProcessList, 1, 0);
            this.table_Process.Controls.Add(this.checkFollowFFXIVPlugin, 2, 0);
            this.table_Process.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.table_Process.Name = "table_Process";
            // 
            // comboProcessList
            // 
            resources.ApplyResources(this.comboProcessList, "comboProcessList");
            this.comboProcessList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProcessList.FormattingEnabled = true;
            this.comboProcessList.Name = "comboProcessList";
            this.comboProcessList.SelectedIndexChanged += new System.EventHandler(this.comboProcessList_SelectedIndexChanged);
            // 
            // buttonRefreshProcessList
            // 
            resources.ApplyResources(this.buttonRefreshProcessList, "buttonRefreshProcessList");
            this.buttonRefreshProcessList.Name = "buttonRefreshProcessList";
            this.buttonRefreshProcessList.UseVisualStyleBackColor = true;
            this.buttonRefreshProcessList.Click += new System.EventHandler(this.buttonRefreshProcessList_Click);
            // 
            // checkFollowFFXIVPlugin
            // 
            resources.ApplyResources(this.checkFollowFFXIVPlugin, "checkFollowFFXIVPlugin");
            this.checkFollowFFXIVPlugin.Name = "checkFollowFFXIVPlugin";
            this.checkFollowFFXIVPlugin.UseVisualStyleBackColor = true;
            this.checkFollowFFXIVPlugin.CheckedChanged += new System.EventHandler(this.checkFollowFFXIVPlugin_CheckedChanged);
            // 
            // label_ShowOverlay
            // 
            resources.ApplyResources(this.label_ShowOverlay, "label_ShowOverlay");
            this.label_ShowOverlay.Name = "label_ShowOverlay";
            // 
            // checkPingVisible
            // 
            resources.ApplyResources(this.checkPingVisible, "checkPingVisible");
            this.checkPingVisible.Name = "checkPingVisible";
            this.checkPingVisible.UseVisualStyleBackColor = true;
            this.checkPingVisible.CheckedChanged += new System.EventHandler(this.checkPingVisible_CheckedChanged);
            // 
            // label_Clickthru
            // 
            resources.ApplyResources(this.label_Clickthru, "label_Clickthru");
            this.label_Clickthru.Name = "label_Clickthru";
            // 
            // checkPingClickThru
            // 
            resources.ApplyResources(this.checkPingClickThru, "checkPingClickThru");
            this.checkPingClickThru.Name = "checkPingClickThru";
            this.checkPingClickThru.UseVisualStyleBackColor = true;
            this.checkPingClickThru.CheckedChanged += new System.EventHandler(this.checkPingClickThru_CheckedChanged);
            // 
            // label_LockOverlay
            // 
            resources.ApplyResources(this.label_LockOverlay, "label_LockOverlay");
            this.label_LockOverlay.Name = "label_LockOverlay";
            // 
            // checkPingLock
            // 
            resources.ApplyResources(this.checkPingLock, "checkPingLock");
            this.checkPingLock.Name = "checkPingLock";
            this.checkPingLock.UseVisualStyleBackColor = true;
            this.checkPingLock.CheckedChanged += new System.EventHandler(this.checkPingLock_CheckedChanged);
            // 
            // label_URL
            // 
            resources.ApplyResources(this.label_URL, "label_URL");
            this.label_URL.Name = "label_URL";
            // 
            // table_URL
            // 
            resources.ApplyResources(this.table_URL, "table_URL");
            this.table_URL.Controls.Add(this.textPingUrl, 0, 0);
            this.table_URL.Controls.Add(this.buttonPingSelectFile, 1, 0);
            this.table_URL.Name = "table_URL";
            // 
            // textPingUrl
            // 
            resources.ApplyResources(this.textPingUrl, "textPingUrl");
            this.textPingUrl.Name = "textPingUrl";
            this.textPingUrl.TextChanged += new System.EventHandler(this.textPingUrl_TextChanged);
            // 
            // buttonPingSelectFile
            // 
            resources.ApplyResources(this.buttonPingSelectFile, "buttonPingSelectFile");
            this.buttonPingSelectFile.Name = "buttonPingSelectFile";
            this.buttonPingSelectFile.UseVisualStyleBackColor = true;
            this.buttonPingSelectFile.Click += new System.EventHandler(this.buttonPingSelectFile_Click);
            // 
            // label_PingInterval
            // 
            resources.ApplyResources(this.label_PingInterval, "label_PingInterval");
            this.label_PingInterval.Name = "label_PingInterval";
            // 
            // nudPingInterval
            // 
            resources.ApplyResources(this.nudPingInterval, "nudPingInterval");
            this.nudPingInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPingInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPingInterval.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPingInterval.Name = "nudPingInterval";
            this.nudPingInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPingInterval.ValueChanged += new System.EventHandler(this.nudPingInterval_ValueChanged);
            // 
            // label_Hotkey
            // 
            resources.ApplyResources(this.label_Hotkey, "label_Hotkey");
            this.label_Hotkey.Name = "label_Hotkey";
            // 
            // table_Hotkey
            // 
            resources.ApplyResources(this.table_Hotkey, "table_Hotkey");
            this.table_Hotkey.Controls.Add(this.checkPingEnableGlobalHotkey, 0, 0);
            this.table_Hotkey.Controls.Add(this.textPingGlobalHotkey, 1, 0);
            this.table_Hotkey.Name = "table_Hotkey";
            // 
            // checkPingEnableGlobalHotkey
            // 
            resources.ApplyResources(this.checkPingEnableGlobalHotkey, "checkPingEnableGlobalHotkey");
            this.checkPingEnableGlobalHotkey.Name = "checkPingEnableGlobalHotkey";
            this.checkPingEnableGlobalHotkey.UseVisualStyleBackColor = true;
            this.checkPingEnableGlobalHotkey.CheckedChanged += new System.EventHandler(this.checkPingEnableGlobalHotkey_CheckedChanged);
            // 
            // textPingGlobalHotkey
            // 
            resources.ApplyResources(this.textPingGlobalHotkey, "textPingGlobalHotkey");
            this.textPingGlobalHotkey.Name = "textPingGlobalHotkey";
            this.textPingGlobalHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPingGlobalHotkey_KeyDown);
            // 
            // label_Framerate
            // 
            resources.ApplyResources(this.label_Framerate, "label_Framerate");
            this.label_Framerate.Name = "label_Framerate";
            // 
            // nudPingMaxFrameRate
            // 
            resources.ApplyResources(this.nudPingMaxFrameRate, "nudPingMaxFrameRate");
            this.nudPingMaxFrameRate.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudPingMaxFrameRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPingMaxFrameRate.Name = "nudPingMaxFrameRate";
            this.nudPingMaxFrameRate.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudPingMaxFrameRate.ValueChanged += new System.EventHandler(this.nudPingMaxFrameRate_ValueChanged);
            // 
            // label_Help
            // 
            resources.ApplyResources(this.label_Help, "label_Help");
            this.tableLayoutPanel7.SetColumnSpan(this.label_Help, 2);
            this.label_Help.Name = "label_Help";
            // 
            // panel_Buttons
            // 
            this.panel_Buttons.Controls.Add(this.tableLayoutPanel8);
            resources.ApplyResources(this.panel_Buttons, "panel_Buttons");
            this.panel_Buttons.Name = "panel_Buttons";
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.buttonPingCopyActXiv, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.buttonPingReloadBrowser, 1, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // buttonPingCopyActXiv
            // 
            resources.ApplyResources(this.buttonPingCopyActXiv, "buttonPingCopyActXiv");
            this.buttonPingCopyActXiv.Name = "buttonPingCopyActXiv";
            this.buttonPingCopyActXiv.UseVisualStyleBackColor = true;
            this.buttonPingCopyActXiv.Click += new System.EventHandler(this.buttonPingCopyActXiv_Click);
            // 
            // buttonPingReloadBrowser
            // 
            resources.ApplyResources(this.buttonPingReloadBrowser, "buttonPingReloadBrowser");
            this.buttonPingReloadBrowser.Name = "buttonPingReloadBrowser";
            this.buttonPingReloadBrowser.UseVisualStyleBackColor = true;
            this.buttonPingReloadBrowser.Click += new System.EventHandler(this.buttonPingReloadBrowser_Click);
            // 
            // PingOverlayConfigPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel7);
            this.Name = "PingOverlayConfigPanel";
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.table_Process.ResumeLayout(false);
            this.table_Process.PerformLayout();
            this.table_URL.ResumeLayout(false);
            this.table_URL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPingInterval)).EndInit();
            this.table_Hotkey.ResumeLayout(false);
            this.table_Hotkey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPingMaxFrameRate)).EndInit();
            this.panel_Buttons.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label_Help;
        private System.Windows.Forms.Label label_Framerate;
        private System.Windows.Forms.NumericUpDown nudPingMaxFrameRate;
        private System.Windows.Forms.Label label_Clickthru;
        private System.Windows.Forms.Label label_ShowOverlay;
        private System.Windows.Forms.Label label_URL;
        private System.Windows.Forms.CheckBox checkPingVisible;
        private System.Windows.Forms.CheckBox checkPingClickThru;
        private System.Windows.Forms.Panel panel_Buttons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button buttonPingReloadBrowser;
        private System.Windows.Forms.Button buttonPingCopyActXiv;
        private System.Windows.Forms.TableLayoutPanel table_URL;
        private System.Windows.Forms.TextBox textPingUrl;
        private System.Windows.Forms.Button buttonPingSelectFile;
        private System.Windows.Forms.Label label_PingInterval;
        private System.Windows.Forms.NumericUpDown nudPingInterval;
        private System.Windows.Forms.Label label_Hotkey;
        private System.Windows.Forms.CheckBox checkPingEnableGlobalHotkey;
        private System.Windows.Forms.TextBox textPingGlobalHotkey;
        private System.Windows.Forms.TableLayoutPanel table_Hotkey;
        private System.Windows.Forms.Label label_LockOverlay;
        private System.Windows.Forms.CheckBox checkPingLock;
        private System.Windows.Forms.Label label_Process;
        private System.Windows.Forms.TableLayoutPanel table_Process;
        private System.Windows.Forms.ComboBox comboProcessList;
        private System.Windows.Forms.Button buttonRefreshProcessList;
        private System.Windows.Forms.CheckBox checkFollowFFXIVPlugin;
    }
}
