namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediFloatMsgBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mediTimer1 = new Mediinfo.WinForm.Common.MediTimer();
            this.mediMsgText = new Mediinfo.WinForm.MediLabel();
            this.SuspendLayout();
            // 
            // mediTimer1
            // 
            this.mediTimer1.Tick += new System.EventHandler(this.MediTimer1_Tick);
            // 
            // mediMsgText
            // 
            this.mediMsgText.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediMsgText.Appearance.ForeColor = System.Drawing.Color.White;
            this.mediMsgText.Appearance.Options.UseFont = true;
            this.mediMsgText.Appearance.Options.UseForeColor = true;
            this.mediMsgText.Appearance.Options.UseTextOptions = true;
            this.mediMsgText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mediMsgText.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediMsgText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.mediMsgText.developerHelper = null;
            this.mediMsgText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediMsgText.Location = new System.Drawing.Point(0, 0);
            this.mediMsgText.Name = "mediMsgText";
            this.mediMsgText.Size = new System.Drawing.Size(381, 81);
            this.mediMsgText.TabIndex = 1;
            this.mediMsgText.Text = "测试文本";
            // 
            // MediFloatMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(381, 81);
            this.Controls.Add(this.mediMsgText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MediFloatMsgBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.MediFloatMsgBox_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Common.MediTimer mediTimer1;
        private MediLabel mediMsgText;
    }
}