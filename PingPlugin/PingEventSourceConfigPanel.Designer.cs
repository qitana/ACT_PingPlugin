namespace Qitana.PingPlugin
{
    partial class PingEventSourceConfigPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PingEventSourceConfigPanel));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_Enabled = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Interval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Timeout = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Timeout)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.checkBox_Enabled, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown_Interval, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown_Timeout, 1, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBox_Enabled
            // 
            resources.ApplyResources(this.checkBox_Enabled, "checkBox_Enabled");
            this.checkBox_Enabled.Name = "checkBox_Enabled";
            this.checkBox_Enabled.UseVisualStyleBackColor = true;
            this.checkBox_Enabled.CheckedChanged += new System.EventHandler(this.checkBox_Enabled_CheckedChanged);
            // 
            // numericUpDown_Interval
            // 
            resources.ApplyResources(this.numericUpDown_Interval, "numericUpDown_Interval");
            this.numericUpDown_Interval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Interval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_Interval.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Interval.Name = "numericUpDown_Interval";
            this.numericUpDown_Interval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Interval.ValueChanged += new System.EventHandler(this.numericUpDown_Interval_ValueChanged);
            // 
            // numericUpDown_Timeout
            // 
            resources.ApplyResources(this.numericUpDown_Timeout, "numericUpDown_Timeout");
            this.numericUpDown_Timeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Timeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_Timeout.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Timeout.Name = "numericUpDown_Timeout";
            this.numericUpDown_Timeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Timeout.ValueChanged += new System.EventHandler(this.numericUpDown_Timeout_ValueChanged);
            // 
            // PingEventSourceConfigPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PingEventSourceConfigPanel";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Timeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_Enabled;
        private System.Windows.Forms.NumericUpDown numericUpDown_Interval;
        private System.Windows.Forms.NumericUpDown numericUpDown_Timeout;
    }
}
