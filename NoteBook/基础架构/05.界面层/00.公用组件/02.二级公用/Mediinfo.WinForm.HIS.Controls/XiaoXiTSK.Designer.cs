namespace Mediinfo.WinForm.HIS.Controls
{
    partial class XiaoXiTSK
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
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediLabel2 = new Mediinfo.WinForm.MediLabel();
            this.mediLabel_YZSL = new Mediinfo.WinForm.MediLabel();
            this.mediLabel4 = new Mediinfo.WinForm.MediLabel();
            this.mediBlueButton_CK = new Mediinfo.WinForm.MediBlueButton();
            this.mediLabel5 = new Mediinfo.WinForm.MediLabel();
            this.mediButton1 = new Mediinfo.WinForm.MediButton();
            this.mediLabel6 = new Mediinfo.WinForm.MediLabel();
            this.mediTimer1 = new Mediinfo.WinForm.Common.MediTimer();
            this.SuspendLayout();
            // 
            // mediLabel1
            // 
            this.mediLabel1.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediLabel1.Appearance.Options.UseFont = true;
            this.mediLabel1.developerHelper = null;
            this.mediLabel1.Location = new System.Drawing.Point(8, 50);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(14, 28);
            this.mediLabel1.TabIndex = 0;
            this.mediLabel1.Text = "●";
            // 
            // mediLabel2
            // 
            this.mediLabel2.Appearance.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediLabel2.Appearance.Options.UseFont = true;
            this.mediLabel2.developerHelper = null;
            this.mediLabel2.Location = new System.Drawing.Point(32, 53);
            this.mediLabel2.Name = "mediLabel2";
            this.mediLabel2.Size = new System.Drawing.Size(114, 25);
            this.mediLabel2.TabIndex = 1;
            this.mediLabel2.Text = "医嘱变更人数";
            // 
            // mediLabel_YZSL
            // 
            this.mediLabel_YZSL.Appearance.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediLabel_YZSL.Appearance.ForeColor = System.Drawing.Color.Red;
            this.mediLabel_YZSL.Appearance.Options.UseFont = true;
            this.mediLabel_YZSL.Appearance.Options.UseForeColor = true;
            this.mediLabel_YZSL.developerHelper = null;
            this.mediLabel_YZSL.Location = new System.Drawing.Point(219, 48);
            this.mediLabel_YZSL.Name = "mediLabel_YZSL";
            this.mediLabel_YZSL.Size = new System.Drawing.Size(15, 31);
            this.mediLabel_YZSL.TabIndex = 2;
            this.mediLabel_YZSL.Text = "0";
            // 
            // mediLabel4
            // 
            this.mediLabel4.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediLabel4.Appearance.Options.UseFont = true;
            this.mediLabel4.developerHelper = null;
            this.mediLabel4.Location = new System.Drawing.Point(265, 50);
            this.mediLabel4.Name = "mediLabel4";
            this.mediLabel4.Size = new System.Drawing.Size(21, 28);
            this.mediLabel4.TabIndex = 3;
            this.mediLabel4.Text = "人";
            // 
            // mediBlueButton_CK
            // 
            this.mediBlueButton_CK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediBlueButton_CK.developerHelper = null;
            this.mediBlueButton_CK.Location = new System.Drawing.Point(206, 108);
            this.mediBlueButton_CK.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediBlueButton_CK.Name = "mediBlueButton_CK";
            this.mediBlueButton_CK.Size = new System.Drawing.Size(80, 30);
            this.mediBlueButton_CK.TabIndex = 4;
            this.mediBlueButton_CK.Text = "查看";
            this.mediBlueButton_CK.UnboundExpression = null;
            this.mediBlueButton_CK.Click += new System.EventHandler(this.mediBlueButton_CK_Click);
            // 
            // mediLabel5
            // 
            this.mediLabel5.Appearance.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediLabel5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(204)))));
            this.mediLabel5.Appearance.Options.UseFont = true;
            this.mediLabel5.Appearance.Options.UseForeColor = true;
            this.mediLabel5.developerHelper = null;
            this.mediLabel5.Location = new System.Drawing.Point(32, 6);
            this.mediLabel5.Name = "mediLabel5";
            this.mediLabel5.Size = new System.Drawing.Size(114, 25);
            this.mediLabel5.TabIndex = 5;
            this.mediLabel5.Text = "医嘱变更消息";
            // 
            // mediButton1
            // 
            this.mediButton1.Appearance.Options.UseTextOptions = true;
            this.mediButton1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButton1.Location = new System.Drawing.Point(270, 6);
            this.mediButton1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = new System.Drawing.Size(24, 26);
            this.mediButton1.TabIndex = 6;
            this.mediButton1.Text = "X";
            this.mediButton1.UnboundExpression = null;
            this.mediButton1.Click += new System.EventHandler(this.mediButton1_Click);
            // 
            // mediLabel6
            // 
            this.mediLabel6.Appearance.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.message;
            this.mediLabel6.Appearance.Options.UseImage = true;
            this.mediLabel6.developerHelper = null;
            this.mediLabel6.Location = new System.Drawing.Point(10, 10);
            this.mediLabel6.Name = "mediLabel6";
            this.mediLabel6.Size = new System.Drawing.Size(16, 20);
            this.mediLabel6.TabIndex = 7;
            // 
            // XiaoXiTSK
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 144);
            this.Controls.Add(this.mediLabel6);
            this.Controls.Add(this.mediButton1);
            this.Controls.Add(this.mediLabel5);
            this.Controls.Add(this.mediBlueButton_CK);
            this.Controls.Add(this.mediLabel4);
            this.Controls.Add(this.mediLabel_YZSL);
            this.Controls.Add(this.mediLabel2);
            this.Controls.Add(this.mediLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "XiaoXiTSK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "医嘱变更消息";
            this.Window = this;
            this.Load += new System.EventHandler(this.XiaoXiTSK_Load);
            this.Shown += new System.EventHandler(this.XiaoXiTSK_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MediLabel mediLabel1;
        private MediLabel mediLabel2;
        private MediLabel mediLabel_YZSL;
        private MediLabel mediLabel4;
        private MediBlueButton mediBlueButton_CK;
        private MediLabel mediLabel5;
        private MediButton mediButton1;
        private MediLabel mediLabel6;
        private Common.MediTimer mediTimer1;
    }
}