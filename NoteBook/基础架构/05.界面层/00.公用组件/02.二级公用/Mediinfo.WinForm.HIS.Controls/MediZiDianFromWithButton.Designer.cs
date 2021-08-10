namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediZiDianFromWithButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediZiDianFromWithButton));
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl();
            this.mediButtonExit = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitControl)).BeginInit();
            this.splitControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleZiDianXx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbXianShiZF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitControl
            // 
            this.splitControl.Location = new System.Drawing.Point(5, 6);
            this.splitControl.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.splitControl.Panel2.Controls.Add(this.mediPanelControl2);
            this.splitControl.Size = new System.Drawing.Size(791, 533);
            // 
            // titleZiDianXx
            // 
            this.titleZiDianXx.LabelVisible = true;
            this.titleZiDianXx.Size = new System.Drawing.Size(593, 30);
            // 
            // titleBarType
            // 
            this.titleBarType.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.titleBarType.LabelVisible = true;
            this.titleBarType.Size = new System.Drawing.Size(177, 30);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(0, 30);
            this.panelMain.Size = new System.Drawing.Size(593, 444);
            // 
            // panelLeft
            // 
            this.panelLeft.Location = new System.Drawing.Point(0, 30);
            this.panelLeft.Size = new System.Drawing.Size(177, 487);
            // 
            // cbXianShiZF
            // 
            this.cbXianShiZF.Properties.Appearance.Options.UseFont = true;
            this.cbXianShiZF.Size = new System.Drawing.Size(82, 28);
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl2.Controls.Add(this.mediButtonExit);
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mediPanelControl2.Location = new System.Drawing.Point(0, 474);
            this.mediPanelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(593, 43);
            this.mediPanelControl2.TabIndex = 0;
            // 
            // mediButtonExit
            // 
            this.mediButtonExit.Appearance.Options.UseTextOptions = true;
            this.mediButtonExit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonExit.Location = new System.Drawing.Point(515, 4);
            this.mediButtonExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediButtonExit.Name = "mediButtonExit";
            this.mediButtonExit.Size = new System.Drawing.Size(75, 32);
            this.mediButtonExit.TabIndex = 0;
            this.mediButtonExit.Text = "关闭(&X)";
            this.mediButtonExit.UnboundExpression = null;
            // 
            // MediZiDianFromWithButton
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 545);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MediZiDianFromWithButton";
            this.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Text = "MediZiDianFromWithButtons";
            ((System.ComponentModel.ISupportInitialize)(this.splitControl)).EndInit();
            this.splitControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleZiDianXx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbXianShiZF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinForm.MediPanelControl mediPanelControl2;
        protected WinForm.MediButton mediButtonExit;
    }
}