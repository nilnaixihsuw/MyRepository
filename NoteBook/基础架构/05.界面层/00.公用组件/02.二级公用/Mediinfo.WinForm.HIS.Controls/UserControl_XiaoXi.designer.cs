namespace Mediinfo.WinForm.HIS.Controls
{
    partial class UserControl_XiaoXi
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mediBlueButtonFaSong = new Mediinfo.WinForm.MediBlueButton();
            this.mediMemoEditNR = new Mediinfo.WinForm.MediMemoEdit();
            this.mediLabel3 = new Mediinfo.WinForm.MediLabel();
            this.mediLabel2 = new Mediinfo.WinForm.MediLabel();
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediButtonGB = new Mediinfo.WinForm.MediButton();
            this.mediTextBoxShouJianRen = new Mediinfo.WinForm.MediTextBox();
            this.mediTextBoxBT = new Mediinfo.WinForm.MediTextBox();
            this.mediButtonTianJiaSJR = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.mediMemoEditNR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxShouJianRen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxBT.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mediBlueButtonFaSong
            // 
            this.mediBlueButtonFaSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mediBlueButtonFaSong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediBlueButtonFaSong.developerHelper = null;
            this.mediBlueButtonFaSong.Location = new System.Drawing.Point(420, 403);
            this.mediBlueButtonFaSong.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediBlueButtonFaSong.Name = "mediBlueButtonFaSong";
            this.mediBlueButtonFaSong.Size = new System.Drawing.Size(70, 26);
            this.mediBlueButtonFaSong.TabIndex = 14;
            this.mediBlueButtonFaSong.Text = "发送";
            this.mediBlueButtonFaSong.UnboundExpression = null;
            this.mediBlueButtonFaSong.Click += new System.EventHandler(this.mediBlueButtonFaSong_Click);
            // 
            // mediMemoEditNR
            // 
            this.mediMemoEditNR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mediMemoEditNR.developerHelper = null;
            this.mediMemoEditNR.Location = new System.Drawing.Point(60, 94);
            this.mediMemoEditNR.Name = "mediMemoEditNR";
            this.mediMemoEditNR.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mediMemoEditNR.Properties.Appearance.Options.UseBackColor = true;
            this.mediMemoEditNR.Properties.developerHelper = null;
            this.mediMemoEditNR.Size = new System.Drawing.Size(430, 297);
            this.mediMemoEditNR.TabIndex = 13;
            // 
            // mediLabel3
            // 
            this.mediLabel3.developerHelper = null;
            this.mediLabel3.Location = new System.Drawing.Point(24, 96);
            this.mediLabel3.Name = "mediLabel3";
            this.mediLabel3.Size = new System.Drawing.Size(30, 20);
            this.mediLabel3.TabIndex = 10;
            this.mediLabel3.Text = "正文";
            // 
            // mediLabel2
            // 
            this.mediLabel2.developerHelper = null;
            this.mediLabel2.Location = new System.Drawing.Point(9, 26);
            this.mediLabel2.Name = "mediLabel2";
            this.mediLabel2.Size = new System.Drawing.Size(45, 20);
            this.mediLabel2.TabIndex = 11;
            this.mediLabel2.Text = "收件人";
            // 
            // mediLabel1
            // 
            this.mediLabel1.developerHelper = null;
            this.mediLabel1.Location = new System.Drawing.Point(24, 58);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(30, 20);
            this.mediLabel1.TabIndex = 12;
            this.mediLabel1.Text = "主题";
            // 
            // mediButtonGB
            // 
            this.mediButtonGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mediButtonGB.Appearance.Options.UseTextOptions = true;
            this.mediButtonGB.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonGB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonGB.Location = new System.Drawing.Point(333, 403);
            this.mediButtonGB.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButtonGB.Name = "mediButtonGB";
            this.mediButtonGB.Size = new System.Drawing.Size(70, 26);
            this.mediButtonGB.TabIndex = 9;
            this.mediButtonGB.Text = "取消";
            this.mediButtonGB.UnboundExpression = null;
            // 
            // mediTextBoxShouJianRen
            // 
            this.mediTextBoxShouJianRen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mediTextBoxShouJianRen.developerHelper = null;
            this.mediTextBoxShouJianRen.Enabled = false;
            this.mediTextBoxShouJianRen.EnterMoveNextControl = true;
            this.mediTextBoxShouJianRen.IsOpenEnterNext = false;
            this.mediTextBoxShouJianRen.Location = new System.Drawing.Point(60, 23);
            this.mediTextBoxShouJianRen.MinimumSize = new System.Drawing.Size(0, 26);
            this.mediTextBoxShouJianRen.Name = "mediTextBoxShouJianRen";
            this.mediTextBoxShouJianRen.Properties.AllowMouseWheel = false;
            this.mediTextBoxShouJianRen.Properties.developerHelper = null;
            this.mediTextBoxShouJianRen.Properties.UnboundExpression = null;
            this.mediTextBoxShouJianRen.Size = new System.Drawing.Size(405, 26);
            this.mediTextBoxShouJianRen.TabIndex = 7;
            this.mediTextBoxShouJianRen.UnboundExpression = null;
            // 
            // mediTextBoxBT
            // 
            this.mediTextBoxBT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mediTextBoxBT.developerHelper = null;
            this.mediTextBoxBT.EnterMoveNextControl = true;
            this.mediTextBoxBT.IsOpenEnterNext = false;
            this.mediTextBoxBT.Location = new System.Drawing.Point(60, 58);
            this.mediTextBoxBT.MinimumSize = new System.Drawing.Size(0, 26);
            this.mediTextBoxBT.Name = "mediTextBoxBT";
            this.mediTextBoxBT.Properties.AllowMouseWheel = false;
            this.mediTextBoxBT.Properties.developerHelper = null;
            this.mediTextBoxBT.Properties.UnboundExpression = null;
            this.mediTextBoxBT.Size = new System.Drawing.Size(430, 26);
            this.mediTextBoxBT.TabIndex = 8;
            this.mediTextBoxBT.UnboundExpression = null;
            // 
            // mediButtonTianJiaSJR
            // 
            this.mediButtonTianJiaSJR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mediButtonTianJiaSJR.Appearance.Options.UseTextOptions = true;
            this.mediButtonTianJiaSJR.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonTianJiaSJR.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediButtonTianJiaSJR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonTianJiaSJR.ImageOptions.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.icon_xinzeng;
            this.mediButtonTianJiaSJR.Location = new System.Drawing.Point(471, 23);
            this.mediButtonTianJiaSJR.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButtonTianJiaSJR.Name = "mediButtonTianJiaSJR";
            this.mediButtonTianJiaSJR.Size = new System.Drawing.Size(19, 26);
            this.mediButtonTianJiaSJR.TabIndex = 15;
            this.mediButtonTianJiaSJR.Text = "mediButton2";
            this.mediButtonTianJiaSJR.UnboundExpression = null;
            this.mediButtonTianJiaSJR.Click += new System.EventHandler(this.mediButtonTianJiaSJR_Click);
            // 
            // UserControl_XiaoXi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.mediButtonTianJiaSJR);
            this.Controls.Add(this.mediBlueButtonFaSong);
            this.Controls.Add(this.mediMemoEditNR);
            this.Controls.Add(this.mediLabel3);
            this.Controls.Add(this.mediLabel2);
            this.Controls.Add(this.mediLabel1);
            this.Controls.Add(this.mediButtonGB);
            this.Controls.Add(this.mediTextBoxShouJianRen);
            this.Controls.Add(this.mediTextBoxBT);
            this.Name = "UserControl_XiaoXi";
            this.Size = new System.Drawing.Size(502, 443);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediMemoEditNR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxShouJianRen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxBT.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MediButton mediButtonTianJiaSJR;
        private MediBlueButton mediBlueButtonFaSong;
        private MediMemoEdit mediMemoEditNR;
        private MediLabel mediLabel3;
        private MediLabel mediLabel2;
        private MediLabel mediLabel1;
        private MediButton mediButtonGB;
        private MediTextBox mediTextBoxShouJianRen;
        private MediTextBox mediTextBoxBT;
    }
}
