namespace Mediinfo.WinForm.HIS.Controls
{
    partial class W_GY_TISIXX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(W_GY_TISIXX));
            this.mediPanelControlALL = new Mediinfo.WinForm.MediPanelControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mediPictureEdit1 = new Mediinfo.WinForm.MediPictureEdit();
            this.mediButtonRight = new Mediinfo.WinForm.MediButton();
            this.mediButtonCenter = new Mediinfo.WinForm.MediButton();
            this.mediButtonLeft = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControlALL)).BeginInit();
            this.mediPanelControlALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mediPanelControlALL
            // 
            this.mediPanelControlALL.Appearance.BackColor = System.Drawing.Color.White;
            this.mediPanelControlALL.Appearance.Options.UseBackColor = true;
            this.mediPanelControlALL.BorderSize = 1;
            this.mediPanelControlALL.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControlALL.Controls.Add(this.richTextBox1);
            this.mediPanelControlALL.Controls.Add(this.mediPictureEdit1);
            this.mediPanelControlALL.Controls.Add(this.mediButtonRight);
            this.mediPanelControlALL.Controls.Add(this.mediButtonCenter);
            this.mediPanelControlALL.Controls.Add(this.mediButtonLeft);
            this.mediPanelControlALL.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControlALL.developerHelper = null;
            this.mediPanelControlALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControlALL.IsDoubleBuffer = false;
            this.mediPanelControlALL.IsHiddedTopBorder = false;
            this.mediPanelControlALL.IsShowBorderColor = false;
            this.mediPanelControlALL.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControlALL.Name = "mediPanelControlALL";
            this.mediPanelControlALL.Size = new System.Drawing.Size(504, 186);
            this.mediPanelControlALL.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.richTextBox1.Location = new System.Drawing.Point(190, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(270, 53);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // mediPictureEdit1
            // 
            this.mediPictureEdit1.developerHelper = null;
            this.mediPictureEdit1.EditValue = ((object)(resources.GetObject("mediPictureEdit1.EditValue")));
            this.mediPictureEdit1.Location = new System.Drawing.Point(156, 26);
            this.mediPictureEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.mediPictureEdit1.Name = "mediPictureEdit1";
            this.mediPictureEdit1.Properties.AllowFocused = false;
            this.mediPictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.mediPictureEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.mediPictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.mediPictureEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.mediPictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPictureEdit1.Properties.developerHelper = null;
            this.mediPictureEdit1.Properties.NullText = " ";
            this.mediPictureEdit1.Properties.RelativeImagePath = "";
            this.mediPictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.mediPictureEdit1.Properties.UnboundExpression = null;
            this.mediPictureEdit1.RelativeImagePath = "";
            this.mediPictureEdit1.Size = new System.Drawing.Size(30, 28);
            this.mediPictureEdit1.TabIndex = 6;
            // 
            // mediButtonRight
            // 
            this.mediButtonRight.Appearance.ForeColor = System.Drawing.Color.Black;
            this.mediButtonRight.Appearance.Options.UseForeColor = true;
            this.mediButtonRight.Appearance.Options.UseTextOptions = true;
            this.mediButtonRight.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonRight.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.mediButtonRight.AppearancePressed.Options.UseBackColor = true;
            this.mediButtonRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonRight.Location = new System.Drawing.Point(340, 100);
            this.mediButtonRight.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButtonRight.Name = "mediButtonRight";
            this.mediButtonRight.Size = new System.Drawing.Size(120, 35);
            this.mediButtonRight.TabIndex = 2;
            this.mediButtonRight.Text = "NO";
            this.mediButtonRight.UnboundExpression = null;
            this.mediButtonRight.Click += new System.EventHandler(this.mediButtonRight_Click);
            // 
            // mediButtonCenter
            // 
            this.mediButtonCenter.Appearance.ForeColor = System.Drawing.Color.Black;
            this.mediButtonCenter.Appearance.Options.UseForeColor = true;
            this.mediButtonCenter.Appearance.Options.UseTextOptions = true;
            this.mediButtonCenter.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonCenter.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.mediButtonCenter.AppearancePressed.Options.UseBackColor = true;
            this.mediButtonCenter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonCenter.Location = new System.Drawing.Point(190, 100);
            this.mediButtonCenter.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButtonCenter.Name = "mediButtonCenter";
            this.mediButtonCenter.Size = new System.Drawing.Size(120, 35);
            this.mediButtonCenter.TabIndex = 1;
            this.mediButtonCenter.Text = "NO";
            this.mediButtonCenter.UnboundExpression = null;
            this.mediButtonCenter.Click += new System.EventHandler(this.mediButtonCenter_Click);
            // 
            // mediButtonLeft
            // 
            this.mediButtonLeft.Appearance.ForeColor = System.Drawing.Color.Black;
            this.mediButtonLeft.Appearance.Options.UseForeColor = true;
            this.mediButtonLeft.Appearance.Options.UseTextOptions = true;
            this.mediButtonLeft.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonLeft.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.mediButtonLeft.AppearancePressed.Options.UseBackColor = true;
            this.mediButtonLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonLeft.Location = new System.Drawing.Point(40, 100);
            this.mediButtonLeft.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButtonLeft.Margin = new System.Windows.Forms.Padding(4);
            this.mediButtonLeft.Name = "mediButtonLeft";
            this.mediButtonLeft.Size = new System.Drawing.Size(120, 35);
            this.mediButtonLeft.TabIndex = 0;
            this.mediButtonLeft.Text = "NO";
            this.mediButtonLeft.UnboundExpression = null;
            this.mediButtonLeft.Click += new System.EventHandler(this.mediButtonLeft_Click);
            // 
            // W_GY_TISIXX
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(504, 186);
            this.Controls.Add(this.mediPanelControlALL);
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "W_GY_TISIXX";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "联众智慧提示";
            this.Shown += new System.EventHandler(this.W_ZJ_DAIGUAHTS_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControlALL)).EndInit();
            this.mediPanelControlALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControlALL;
        private MediButton mediButtonLeft;
        private MediButton mediButtonRight;
        private MediButton mediButtonCenter;
        private MediPictureEdit mediPictureEdit1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}