using Mediinfo.WinForm.HIS.Controls;

namespace Mediinfo.WinForm.HIS.Main
{
    partial class HISConnectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HISConnectForm));
            this.mediTitleBar1 = new Mediinfo.WinForm.MediTitleBar();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediButtonTest = new Mediinfo.WinForm.MediButton();
            this.mediButtonSave = new Mediinfo.WinForm.MediButton();
            this.mediButtonExit = new Mediinfo.WinForm.MediButton();
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediDataLayoutControl1 = new Mediinfo.WinForm.HIS.Controls.MediDataLayoutControl();
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediTextBoxPassword = new Mediinfo.WinForm.MediTextBox();
            this.mediTextBoxUserName = new Mediinfo.WinForm.MediTextBox();
            this.mediTextBoxDataSource = new Mediinfo.WinForm.MediTextBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.mediTitleBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediDataLayoutControl1)).BeginInit();
            this.mediDataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxDataSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // mediTitleBar1
            // 
            this.mediTitleBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mediTitleBar1.LabeBackColor = System.Drawing.Color.Empty;
            this.mediTitleBar1.LabeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.mediTitleBar1.LabelEnabled = true;
            this.mediTitleBar1.LabelFont = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediTitleBar1.LabelSize = new System.Drawing.Size(104, 19);
            this.mediTitleBar1.LabelText = "请配置数据库连接";
            this.mediTitleBar1.LabelVisible = true;
            this.mediTitleBar1.LabePadding = new System.Windows.Forms.Padding(0);
            this.mediTitleBar1.Location = new System.Drawing.Point(2, 2);
            this.mediTitleBar1.Name = "mediTitleBar1";
            this.mediTitleBar1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.mediTitleBar1.Size = new System.Drawing.Size(390, 25);
            this.mediTitleBar1.TabIndex = 0;
            this.mediTitleBar1.UnboundExpression = null;
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.mediButtonTest);
            this.mediPanelControl1.Controls.Add(this.mediButtonSave);
            this.mediPanelControl1.Controls.Add(this.mediButtonExit);
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mediPanelControl1.Location = new System.Drawing.Point(2, 180);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(390, 36);
            this.mediPanelControl1.TabIndex = 2;
            // 
            // mediButtonTest
            // 
            this.mediButtonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mediButtonTest.Appearance.Options.UseTextOptions = true;
            this.mediButtonTest.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonTest.Location = new System.Drawing.Point(147, 5);
            this.mediButtonTest.Name = "mediButtonTest";
            this.mediButtonTest.Size = new System.Drawing.Size(75, 26);
            this.mediButtonTest.TabIndex = 2;
            this.mediButtonTest.Text = "测试连接";
            this.mediButtonTest.UnboundExpression = null;
            this.mediButtonTest.Click += new System.EventHandler(this.mediButtonTest_Click);
            // 
            // mediButtonSave
            // 
            this.mediButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mediButtonSave.Appearance.Options.UseTextOptions = true;
            this.mediButtonSave.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonSave.Location = new System.Drawing.Point(227, 5);
            this.mediButtonSave.Name = "mediButtonSave";
            this.mediButtonSave.Size = new System.Drawing.Size(75, 26);
            this.mediButtonSave.TabIndex = 1;
            this.mediButtonSave.Text = "保存(&S)";
            this.mediButtonSave.UnboundExpression = null;
            this.mediButtonSave.Click += new System.EventHandler(this.mediButtonSave_Click);
            // 
            // mediButtonExit
            // 
            this.mediButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mediButtonExit.Appearance.Options.UseTextOptions = true;
            this.mediButtonExit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButtonExit.CausesValidation = false;
            this.mediButtonExit.Location = new System.Drawing.Point(308, 5);
            this.mediButtonExit.Name = "mediButtonExit";
            this.mediButtonExit.Size = new System.Drawing.Size(75, 26);
            this.mediButtonExit.TabIndex = 3;
            this.mediButtonExit.Text = "退出(&X)";
            this.mediButtonExit.UnboundExpression = null;
            this.mediButtonExit.Click += new System.EventHandler(this.mediButtonExit_Click);
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.Controls.Add(this.mediDataLayoutControl1);
            this.mediPanelControl2.Controls.Add(this.mediPanelControl1);
            this.mediPanelControl2.Controls.Add(this.mediTitleBar1);
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl2.Location = new System.Drawing.Point(5, 5);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(394, 218);
            this.mediPanelControl2.TabIndex = 3;
            // 
            // mediDataLayoutControl1
            // 
            this.mediDataLayoutControl1.BackColor = System.Drawing.Color.White;
            this.mediDataLayoutControl1.Controls.Add(this.mediLabel1);
            this.mediDataLayoutControl1.Controls.Add(this.mediTextBoxPassword);
            this.mediDataLayoutControl1.Controls.Add(this.mediTextBoxUserName);
            this.mediDataLayoutControl1.Controls.Add(this.mediTextBoxDataSource);
            this.mediDataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediDataLayoutControl1.EnterMoveNextControl = true;
            this.mediDataLayoutControl1.LayoutControlGroupCollection = new DevExpress.XtraLayout.LayoutControlGroup[] {
        this.layoutControlGroup1};
            this.mediDataLayoutControl1.Location = new System.Drawing.Point(2, 27);
            this.mediDataLayoutControl1.Name = "mediDataLayoutControl1";
            this.mediDataLayoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mediDataLayoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediDataLayoutControl1.ReaderOnly = false;
            this.mediDataLayoutControl1.Root = this.layoutControlGroup1;
            this.mediDataLayoutControl1.Size = new System.Drawing.Size(390, 153);
            this.mediDataLayoutControl1.TabIndex = 1;
            this.mediDataLayoutControl1.Text = "mediDataLayoutControl1";
            // 
            // mediLabel1
            // 
            this.mediLabel1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.mediLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.mediLabel1.Location = new System.Drawing.Point(9, 122);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(372, 17);
            this.mediLabel1.StyleController = this.mediDataLayoutControl1;
            this.mediLabel1.TabIndex = 7;
            this.mediLabel1.Text = "请配置数据库登录用户的信息，如不清楚请联系系统管理员";
            // 
            // mediTextBoxPassword
            // 
            this.mediTextBoxPassword.EnterMoveNextControl = true;
            this.mediTextBoxPassword.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mediTextBoxPassword.Location = new System.Drawing.Point(48, 86);
            this.mediTextBoxPassword.Name = "mediTextBoxPassword";
            this.mediTextBoxPassword.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.mediTextBoxPassword.Properties.Appearance.Options.UseFont = true;
            this.mediTextBoxPassword.Properties.Mask_DataType = Mediinfo.WinForm.DataType.Default;
            this.mediTextBoxPassword.Properties.UseSystemPasswordChar = true;
            this.mediTextBoxPassword.Size = new System.Drawing.Size(333, 26);
            this.mediTextBoxPassword.StyleController = this.mediDataLayoutControl1;
            this.mediTextBoxPassword.TabIndex = 6;
            this.mediTextBoxPassword.UnboundExpression = null;
            // 
            // mediTextBoxUserName
            // 
            this.mediTextBoxUserName.EnterMoveNextControl = true;
            this.mediTextBoxUserName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mediTextBoxUserName.Location = new System.Drawing.Point(48, 48);
            this.mediTextBoxUserName.Name = "mediTextBoxUserName";
            this.mediTextBoxUserName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.mediTextBoxUserName.Properties.Appearance.Options.UseFont = true;
            this.mediTextBoxUserName.Properties.Mask_DataType = Mediinfo.WinForm.DataType.Default;
            this.mediTextBoxUserName.Size = new System.Drawing.Size(333, 26);
            this.mediTextBoxUserName.StyleController = this.mediDataLayoutControl1;
            this.mediTextBoxUserName.TabIndex = 5;
            this.mediTextBoxUserName.UnboundExpression = null;
            // 
            // mediTextBoxDataSource
            // 
            this.mediTextBoxDataSource.EnterMoveNextControl = true;
            this.mediTextBoxDataSource.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mediTextBoxDataSource.Location = new System.Drawing.Point(48, 10);
            this.mediTextBoxDataSource.Name = "mediTextBoxDataSource";
            this.mediTextBoxDataSource.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.mediTextBoxDataSource.Properties.Appearance.Options.UseFont = true;
            this.mediTextBoxDataSource.Properties.Mask_DataType = Mediinfo.WinForm.DataType.Default;
            this.mediTextBoxDataSource.Size = new System.Drawing.Size(333, 26);
            this.mediTextBoxDataSource.StyleController = this.mediDataLayoutControl1;
            this.mediTextBoxDataSource.TabIndex = 4;
            this.mediTextBoxDataSource.UnboundExpression = null;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 4, 4);
            this.layoutControlGroup1.Size = new System.Drawing.Size(390, 153);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.mediTextBoxDataSource;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 38);
            this.layoutControlItem1.Text = "服务名";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.mediTextBoxUserName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 38);
            this.layoutControlItem2.Text = "用户名";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.mediTextBoxPassword;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(380, 38);
            this.layoutControlItem3.Text = "密码";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.mediLabel1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 114);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem4.Size = new System.Drawing.Size(380, 31);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // HISConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 228);
            this.ControlBox = false;
            this.Controls.Add(this.mediPanelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HISConnectForm";
            this.Text = "数据库设置";
            this.Shown += new System.EventHandler(this.HISConnectForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mediTitleBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediDataLayoutControl1)).EndInit();
            this.mediDataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxDataSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WinForm.MediTitleBar mediTitleBar1;
        private Controls.MediDataLayoutControl mediDataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private WinForm.MediPanelControl mediPanelControl1;
        private WinForm.MediButton mediButtonExit;
        private WinForm.MediButton mediButtonSave;
        private WinForm.MediButton mediButtonTest;
        private WinForm.MediTextBox mediTextBoxDataSource;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private WinForm.MediTextBox mediTextBoxPassword;
        private WinForm.MediTextBox mediTextBoxUserName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private WinForm.MediPanelControl mediPanelControl2;
        private WinForm.MediLabel mediLabel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}