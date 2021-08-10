namespace Mediinfo.WinForm.HIS.Controls
{
    partial class W_GY_SAOMA
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
            this.components = new System.ComponentModel.Container();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediTextBox1 = new Mediinfo.WinForm.MediTextBox();
            this.mediButton1 = new Mediinfo.WinForm.MediButton();
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediLabel2 = new Mediinfo.WinForm.MediLabel();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBox1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.BorderSize = 1;
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.mediLabel2);
            this.mediPanelControl1.Controls.Add(this.mediTextBox1);
            this.mediPanelControl1.Controls.Add(this.mediButton1);
            this.mediPanelControl1.Controls.Add(this.mediLabel1);
            this.mediPanelControl1.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.IsShowBorderColor = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(1, 1);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(398, 178);
            this.mediPanelControl1.TabIndex = 0;
            this.mediPanelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mediPanelControl1_MouseDown);
            // 
            // mediTextBox1
            // 
            this.mediTextBox1.developerHelper = null;
            this.mediTextBox1.EnterMoveNextControl = true;
            this.mediTextBox1.IsOpenEnterNext = false;
            this.mediTextBox1.Location = new System.Drawing.Point(18, 76);
            this.mediTextBox1.MinimumSize = new System.Drawing.Size(0, 26);
            this.mediTextBox1.Name = "mediTextBox1";
            this.mediTextBox1.Properties.AllowMouseWheel = false;
            this.mediTextBox1.Properties.developerHelper = null;
            this.mediTextBox1.Properties.UnboundExpression = null;
            this.mediTextBox1.Size = new System.Drawing.Size(361, 26);
            this.mediTextBox1.TabIndex = 13;
            this.mediTextBox1.UnboundExpression = null;
            this.mediTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mediTextBox1_KeyDown);
            // 
            // mediButton1
            // 
            this.mediButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButton1.Location = new System.Drawing.Point(299, 138);
            this.mediButton1.Margin = new System.Windows.Forms.Padding(4);
            this.mediButton1.MediButtonStyle = Mediinfo.WinForm.ButtonType.BlueButton;
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = new System.Drawing.Size(80, 26);
            this.mediButton1.TabIndex = 9;
            this.mediButton1.Text = "返回";
            this.mediButton1.UnboundExpression = null;
            this.mediButton1.Click += new System.EventHandler(this.mediButton1_Click);
            // 
            // mediLabel1
            // 
            this.mediLabel1.Appearance.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.mediLabel1.Appearance.Options.UseFont = true;
            this.mediLabel1.developerHelper = null;
            this.mediLabel1.Location = new System.Drawing.Point(18, 23);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(75, 20);
            this.mediLabel1.TabIndex = 12;
            this.mediLabel1.Text = "请刷二维码";
            // 
            // mediLabel2
            // 
            this.mediLabel2.Appearance.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.mediLabel2.Appearance.Options.UseFont = true;
            this.mediLabel2.developerHelper = null;
            this.mediLabel2.Location = new System.Drawing.Point(18, 49);
            this.mediLabel2.Name = "mediLabel2";
            this.mediLabel2.Size = new System.Drawing.Size(210, 20);
            this.mediLabel2.TabIndex = 15;
            this.mediLabel2.Tag = "";
            this.mediLabel2.Text = "请通过微信或支付宝扫码支付！";
            this.mediLabel2.Visible = false;
            // 
            // W_GY_SAOMA
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(400, 180);
            this.Controls.Add(this.mediPanelControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "W_GY_SAOMA";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = true;
            this.Text = "MediPromptDialog";
            this.Shown += new System.EventHandler(this.W_GY_SAOMA_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            this.mediPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBox1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
        private MediButton mediButton1;
        private MediLabel mediLabel1;
        private MediTextBox mediTextBox1;
        private MediLabel mediLabel2;
    }
}