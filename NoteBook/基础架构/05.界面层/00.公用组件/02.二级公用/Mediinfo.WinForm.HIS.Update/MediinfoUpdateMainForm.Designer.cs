namespace Mediinfo.WinForm.HIS.Update
{
    partial class MediinfoUpdateMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediinfoUpdateMainForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uploadbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblHospital = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pbLog = new System.Windows.Forms.PictureBox();
            this.mediWaitCircle = new Mediinfo.WinForm.HIS.Update.MediWaitCircleControl();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLog)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(501, 145);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 29);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(501, 196);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(218, 29);
            this.textBox2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(465, 352);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 35);
            this.panel1.TabIndex = 5;
            // 
            // uploadbackgroundWorker
            // 
            this.uploadbackgroundWorker.WorkerReportsProgress = true;
            this.uploadbackgroundWorker.WorkerSupportsCancellation = true;
            this.uploadbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploadbackgroundWorker_DoWork);
            this.uploadbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.uploadbackgroundWorker_ProgressChanged);
            this.uploadbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uploadbackgroundWorker_RunWorkerCompleted);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mediWaitCircle);
            this.panel2.Location = new System.Drawing.Point(491, 350);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 35);
            this.panel2.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Enabled = false;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.Location = new System.Drawing.Point(609, 305);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 34);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(468, 305);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 34);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // lblHospital
            // 
            this.lblHospital.AutoSize = true;
            this.lblHospital.BackColor = System.Drawing.Color.Transparent;
            this.lblHospital.Font = new System.Drawing.Font("微软雅黑", 17.25F);
            this.lblHospital.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblHospital.Location = new System.Drawing.Point(570, 55);
            this.lblHospital.Name = "lblHospital";
            this.lblHospital.Size = new System.Drawing.Size(220, 30);
            this.lblHospital.TabIndex = 6;
            this.lblHospital.Text = "新一代医院信息系统";
            // 
            // lblIP
            // 
            this.lblIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIP.AutoSize = true;
            this.lblIP.BackColor = System.Drawing.Color.Transparent;
            this.lblIP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIP.ForeColor = System.Drawing.Color.Gray;
            this.lblIP.Location = new System.Drawing.Point(495, 397);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(109, 17);
            this.lblIP.TabIndex = 8;
            this.lblIP.Text = "本机：192.168.0.1";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(657, 397);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(180, 17);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "版本号：V6.1";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbLog
            // 
            this.pbLog.BackColor = System.Drawing.Color.Transparent;
            this.pbLog.Image = global::Mediinfo.WinForm.HIS.Update.Properties.Resources.login_up_bg;
            this.pbLog.Location = new System.Drawing.Point(498, 113);
            this.pbLog.Name = "pbLog";
            this.pbLog.Size = new System.Drawing.Size(328, 226);
            this.pbLog.TabIndex = 10;
            this.pbLog.TabStop = false;
            // 
            // mediWaitCircle
            // 
            this.mediWaitCircle.Activate = true;
            this.mediWaitCircle.Description = "系统正在初始化...";
            this.mediWaitCircle.DescriptionFont = new System.Drawing.Font("Arial", 8F);
            this.mediWaitCircle.DescriptionFontColor = System.Drawing.SystemColors.MenuHighlight;
            this.mediWaitCircle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediWaitCircle.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
            this.mediWaitCircle.HotSpokeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(130)))), ((int)(((byte)(30)))));
            this.mediWaitCircle.InnerRadius = 6.25F;
            this.mediWaitCircle.Location = new System.Drawing.Point(0, 0);
            this.mediWaitCircle.Name = "mediWaitCircle";
            this.mediWaitCircle.NumberOfSpokes = 12;
            this.mediWaitCircle.Size = new System.Drawing.Size(355, 35);
            this.mediWaitCircle.SpokeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(187)))), ((int)(((byte)(75)))));
            this.mediWaitCircle.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
            this.mediWaitCircle.TabIndex = 0;
            this.mediWaitCircle.Text = "mediWaitCircleControl1";
            this.mediWaitCircle.Thickness = 2F;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopyRight.AutoSize = true;
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCopyRight.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyRight.Location = new System.Drawing.Point(495, 421);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(350, 17);
            this.lblCopyRight.TabIndex = 7;
            this.lblCopyRight.Text = "Copyright © 1999~2020 联众智慧科技股份有限公司 版权所有";
            // 
            // MediinfoUpdateMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(880, 452);
            this.Controls.Add(this.pbLog);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblHospital);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MediinfoUpdateMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediinfoUpdateMainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MediinfoUpdateMainForm_FormClosed);
            this.Load += new System.EventHandler(this.MediinfoUpdateMainForm_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
     
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        //private MediWaitCircleControl mediWaitCircleControl1;
        private System.Windows.Forms.Panel panel1;
       // private MediProgressBarControl uploadprogressBarControl;
        private System.ComponentModel.BackgroundWorker uploadbackgroundWorker;
        private MediWaitCircleControl mediWaitCircle;
        //private MediProgressBarControl medipdbc;
        private System.Windows.Forms.Panel panel2;
       // private System.Windows.Forms.Label downingFilelabel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblHospital;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pbLog;
        private System.Windows.Forms.Label lblCopyRight;
    }
}