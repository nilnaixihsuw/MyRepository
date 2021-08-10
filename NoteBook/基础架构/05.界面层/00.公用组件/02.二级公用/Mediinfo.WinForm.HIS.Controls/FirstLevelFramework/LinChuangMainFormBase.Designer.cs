namespace Mediinfo.WinForm.HIS.Controls.FirstLevelFramework
{
    partial class LinChuangMainFormBase
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
            this.mediButton1 = new Mediinfo.WinForm.MediButton();
            this.mediLCMainFormTabControl = new Mediinfo.WinForm.MediSuperTabControl();
            this.mediLCBarManager = new Mediinfo.WinForm.MediBarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStatic_date = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemKongBai1 = new DevExpress.XtraBars.BarStaticItem();
            this.userNamebsi = new DevExpress.XtraBars.BarStaticItem();
            this.yingYongMCbsi = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticHosName = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemZhuanJia = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemKongBai2 = new DevExpress.XtraBars.BarStaticItem();
            this.dateInfobsi = new DevExpress.XtraBars.BarStaticItem();
            this.ipInfobsi = new DevExpress.XtraBars.BarStaticItem();
            this.companyInfobsi = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemKongBai3 = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_RunningMode = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_ServerUrl = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemKongBai4 = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_Version = new DevExpress.XtraBars.BarStaticItem();
            this.linChuangBarController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.mediLCMainFormTabControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediLCBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linChuangBarController)).BeginInit();
            this.SuspendLayout();
            // 
            // mediButton1
            // 
            this.mediButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mediButton1.Appearance.Options.UseTextOptions = true;
            this.mediButton1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButton1.Location = new System.Drawing.Point(929, 12);
            this.mediButton1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = new System.Drawing.Size(70, 26);
            this.mediButton1.TabIndex = 2;
            this.mediButton1.Text = "mediButton1";
            this.mediButton1.UnboundExpression = null;
            // 
            // mediLCMainFormTabControl
            // 
            this.mediLCMainFormTabControl.ActiveEditor = null;
            this.mediLCMainFormTabControl.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.mediLCMainFormTabControl.Appearance.Options.UseBackColor = true;
            this.mediLCMainFormTabControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediLCMainFormTabControl.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediLCMainFormTabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.mediLCMainFormTabControl.developerHelper = null;
            this.mediLCMainFormTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediLCMainFormTabControl.EditValue = null;
            this.mediLCMainFormTabControl.IsAdsSide = false;
            this.mediLCMainFormTabControl.KeShiName = null;
            this.mediLCMainFormTabControl.LeftDistance = 0;
            this.mediLCMainFormTabControl.Location = new System.Drawing.Point(0, 0);
            this.mediLCMainFormTabControl.Name = "mediLCMainFormTabControl";
            this.mediLCMainFormTabControl.ParentForm = this;
            this.mediLCMainFormTabControl.Size = new System.Drawing.Size(1366, 698);
            this.mediLCMainFormTabControl.TabIndex = 0;
            this.mediLCMainFormTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.mediTabControl1_SelectedPageChanged);
            this.mediLCMainFormTabControl.CloseButtonClick += new System.EventHandler(this.mediTabControl1_CloseButtonClick);
            // 
            // mediLCBarManager
            // 
            this.mediLCBarManager.AllowCustomization = false;
            this.mediLCBarManager.AllowShowToolbarsPopup = false;
            this.mediLCBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.mediLCBarManager.Controller = this.linChuangBarController;
            this.mediLCBarManager.developerHelper = null;
            this.mediLCBarManager.DockControls.Add(this.barDockControl1);
            this.mediLCBarManager.DockControls.Add(this.barDockControl2);
            this.mediLCBarManager.DockControls.Add(this.barDockControl3);
            this.mediLCBarManager.DockControls.Add(this.barDockControl4);
            this.mediLCBarManager.Form = this;
            this.mediLCBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.yingYongMCbsi,
            this.userNamebsi,
            this.dateInfobsi,
            this.ipInfobsi,
            this.companyInfobsi,
            this.barStatic_RunningMode,
            this.barStatic_ServerUrl,
            this.barStatic_date,
            this.barStaticItemKongBai1,
            this.barStaticItemKongBai2,
            this.barStaticItemKongBai3,
            this.barStaticItemZhuanJia,
            this.barStaticHosName});
            this.mediLCBarManager.MaxItemId = 13;
            this.mediLCBarManager.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_date),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticHosName),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemKongBai1),
            new DevExpress.XtraBars.LinkPersistInfo(this.yingYongMCbsi),
            new DevExpress.XtraBars.LinkPersistInfo(this.userNamebsi),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemZhuanJia),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemKongBai2),
            new DevExpress.XtraBars.LinkPersistInfo(this.dateInfobsi),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipInfobsi),
            new DevExpress.XtraBars.LinkPersistInfo(this.companyInfobsi),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemKongBai3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_RunningMode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_ServerUrl),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemKongBai4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_Version)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DisableCustomization = true;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStatic_date
            // 
            this.barStatic_date.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStatic_date.Id = 7;
            this.barStatic_date.Name = "barStatic_date";
            // 
            // barStaticItemKongBai1
            // 
            this.barStaticItemKongBai1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemKongBai1.Id = 8;
            this.barStaticItemKongBai1.Name = "barStaticItemKongBai1";
            // 
            // userNamebsi
            // 
            this.userNamebsi.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.userNamebsi.Id = 1;
            this.userNamebsi.Name = "userNamebsi";
            // 
            // yingYongMCbsi
            // 
            this.yingYongMCbsi.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.yingYongMCbsi.Id = 0;
            this.yingYongMCbsi.Name = "yingYongMCbsi";
            // 
            // barStaticHosName
            // 
            this.barStaticHosName.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticHosName.Id = 12;
            this.barStaticHosName.Name = "barStaticHosName";
            // 
            // barStaticItemZhuanJia
            // 
            this.barStaticItemZhuanJia.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemZhuanJia.Id = 11;
            this.barStaticItemZhuanJia.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Red;
            this.barStaticItemZhuanJia.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barStaticItemZhuanJia.Name = "barStaticItemZhuanJia";
            // 
            // barStaticItemKongBai2
            // 
            this.barStaticItemKongBai2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemKongBai2.Id = 9;
            this.barStaticItemKongBai2.Name = "barStaticItemKongBai2";
            // 
            // dateInfobsi
            // 
            this.dateInfobsi.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateInfobsi.Id = 2;
            this.dateInfobsi.Name = "dateInfobsi";
            // 
            // ipInfobsi
            // 
            this.ipInfobsi.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ipInfobsi.Id = 3;
            this.ipInfobsi.Name = "ipInfobsi";
            // 
            // companyInfobsi
            // 
            this.companyInfobsi.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.companyInfobsi.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.companyInfobsi.Caption = "Copyright © 1999-2019 联众智慧 版权所有";
            this.companyInfobsi.Id = 4;
            this.companyInfobsi.Name = "companyInfobsi";
            // 
            // barStaticItemKongBai3
            // 
            this.barStaticItemKongBai3.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemKongBai3.Id = 10;
            this.barStaticItemKongBai3.Name = "barStaticItemKongBai3";

            // 
            // barStaticItemKongBai4
            // 
            this.barStaticItemKongBai4.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemKongBai4.Id = 15;
            this.barStaticItemKongBai4.Name = "barStaticItemKongBai4";
            // 
            // barStatic_RunningMode
            // 
            this.barStatic_RunningMode.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStatic_RunningMode.Id = 5;
            this.barStatic_RunningMode.Name = "barStatic_RunningMode";
            // 
            // barStatic_ServerUrl
            // 
            this.barStatic_ServerUrl.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStatic_ServerUrl.Id = 6;
            this.barStatic_ServerUrl.Name = "barStatic_ServerUrl";
            // 
            // barStatic_Version
            // 
            this.barStatic_Version.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStatic_Version.Id = 13;
            this.barStatic_Version.Name = "barStatic_Version";
            // 
            // linChuangBarController
            // 
            this.linChuangBarController.PropertiesBar.AllowLinkLighting = false;
            this.linChuangBarController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.linChuangBarController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.mediLCBarManager;
            this.barDockControl1.Size = new System.Drawing.Size(1366, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 698);
            this.barDockControl2.Manager = this.mediLCBarManager;
            this.barDockControl2.Size = new System.Drawing.Size(1366, 30);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Manager = this.mediLCBarManager;
            this.barDockControl3.Size = new System.Drawing.Size(0, 698);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1366, 0);
            this.barDockControl4.Manager = this.mediLCBarManager;
            this.barDockControl4.Size = new System.Drawing.Size(0, 698);
            // 
            // LinChuangMainFormBase
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1366, 728);
            this.Controls.Add(this.mediLCMainFormTabControl);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "LinChuangMainFormBase";
            this.Text = "LinChuangMainFormBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinChuangMainFormBase_FormClosing);
            this.Load += new System.EventHandler(this.LinChuangMainFormBase_Load);
            this.Shown += new System.EventHandler(this.LinChuangMainFormBase_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mediLCMainFormTabControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediLCBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linChuangBarController)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MediButton mediButton1;
        private MediBarManager mediLCBarManager;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarStaticItem yingYongMCbsi;
        private DevExpress.XtraBars.BarStaticItem userNamebsi;
        private DevExpress.XtraBars.BarStaticItem dateInfobsi;
        private DevExpress.XtraBars.BarStaticItem ipInfobsi;
        private DevExpress.XtraBars.BarStaticItem companyInfobsi;
        private DevExpress.XtraBars.BarAndDockingController linChuangBarController;
        public MediSuperTabControl mediLCMainFormTabControl;
        private DevExpress.XtraBars.BarStaticItem barStatic_RunningMode;
        private DevExpress.XtraBars.BarStaticItem barStatic_ServerUrl;
        private DevExpress.XtraBars.BarStaticItem barStatic_date;
        private DevExpress.XtraBars.BarStaticItem barStaticItemKongBai1;
        private DevExpress.XtraBars.BarStaticItem barStaticItemKongBai2;
        private DevExpress.XtraBars.BarStaticItem barStaticItemKongBai3;
        private DevExpress.XtraBars.BarStaticItem barStaticItemZhuanJia;
        private DevExpress.XtraBars.BarStaticItem barStaticHosName;
        private DevExpress.XtraBars.BarStaticItem barStaticItemKongBai4;
        private DevExpress.XtraBars.BarStaticItem barStatic_Version;
    }
}