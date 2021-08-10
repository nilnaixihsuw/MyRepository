namespace Mediinfo.WinForm.HIS.Controls.FirstLevelFramework
{
    partial class MainFormBase
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
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper1 = new Mediinfo.WinForm.SystemInfoHelper();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormBase));
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.Tools = new DevExpress.XtraBars.Bar();
            this.MainMenu = new DevExpress.XtraBars.Bar();
            this.StatusBar = new DevExpress.XtraBars.Bar();
            this.barStatic_Company = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_User = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_KeShi = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_date = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_IP = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_RunningMode = new DevExpress.XtraBars.BarStaticItem();
            this.barStatic_ServerUrl = new DevExpress.XtraBars.BarStaticItem();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.panel = new Mediinfo.WinForm.MediPanelControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.timer1 = new System.Windows.Forms.Timer();
            this.clientFrmCacheWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.Tools,
            this.MainMenu,
            this.StatusBar});
            this.barManager.Controller = this.barAndDockingController1;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.DockWindowTabFont = new System.Drawing.Font("微软雅黑", 11F);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStatic_Company,
            this.barStatic_User,
            this.barStatic_KeShi,
            this.barStatic_date,
            this.barStatic_IP,
            this.barButtonItem1,
            this.barStatic_RunningMode,
            this.barStatic_ServerUrl});
            this.barManager.MainMenu = this.MainMenu;
            this.barManager.MaxItemId = 8;
            this.barManager.StatusBar = this.StatusBar;
            // 
            // Tools
            // 
            this.Tools.BarName = "Tools";
            this.Tools.DockCol = 0;
            this.Tools.DockRow = 1;
            this.Tools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.Tools.OptionsBar.AllowQuickCustomization = false;
            this.Tools.OptionsBar.DrawDragBorder = false;
            this.Tools.OptionsBar.MultiLine = true;
            this.Tools.OptionsBar.UseWholeRow = true;
            this.Tools.Text = "工具栏";
            // 
            // MainMenu
            // 
            this.MainMenu.BarName = "Main menu";
            this.MainMenu.DockCol = 0;
            this.MainMenu.DockRow = 0;
            this.MainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.MainMenu.OptionsBar.AllowQuickCustomization = false;
            this.MainMenu.OptionsBar.DrawDragBorder = false;
            this.MainMenu.OptionsBar.MultiLine = true;
            this.MainMenu.OptionsBar.UseWholeRow = true;
            this.MainMenu.Text = "主菜单";
            // 
            // StatusBar
            // 
            this.StatusBar.BarName = "Status bar";
            this.StatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.StatusBar.DockCol = 0;
            this.StatusBar.DockRow = 0;
            this.StatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.StatusBar.FloatLocation = new System.Drawing.Point(1310, 488);
            this.StatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_Company),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_User),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_KeShi),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_date),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_IP),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_RunningMode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStatic_ServerUrl)});
            this.StatusBar.OptionsBar.AllowDelete = true;
            this.StatusBar.OptionsBar.AllowQuickCustomization = false;
            this.StatusBar.OptionsBar.DrawDragBorder = false;
            this.StatusBar.OptionsBar.UseWholeRow = true;
            this.StatusBar.Text = "状态栏";
            // 
            // barStatic_Company
            // 
            this.barStatic_Company.Caption = "联众智慧科技股份有限公司";
            this.barStatic_Company.Id = 0;
            this.barStatic_Company.Name = "barStatic_Company";
            // 
            // barStatic_User
            // 
            this.barStatic_User.Id = 1;
            this.barStatic_User.Name = "barStatic_User";
            // 
            // barStatic_KeShi
            // 
            this.barStatic_KeShi.Id = 2;
            this.barStatic_KeShi.Name = "barStatic_KeShi";
            // 
            // barStatic_date
            // 
            this.barStatic_date.Id = 3;
            this.barStatic_date.Name = "barStatic_date";
            // 
            // barStatic_IP
            // 
            this.barStatic_IP.Id = 4;
            this.barStatic_IP.Name = "barStatic_IP";
            // 
            // barStatic_RunningMode
            // 
            this.barStatic_RunningMode.Id = 6;
            this.barStatic_RunningMode.Name = "barStatic_RunningMode";
            // 
            // barStatic_ServerUrl
            // 
            this.barStatic_ServerUrl.Id = 7;
            this.barStatic_ServerUrl.Name = "barStatic_ServerUrl";
            // 
            // barAndDockingController1
            // 
            //this.barAndDockingController1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1008, 49);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.barDockControlBottom.Appearance.Options.UseFont = true;
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 571);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1008, 30);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 49);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 522);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1008, 49);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 522);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 5;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // panel
            // 
            this.panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            systemInfoHelper1.ControlAssemblyName = null;
            systemInfoHelper1.ControlClassName = null;
            systemInfoHelper1.ControlForFormName = null;
            systemInfoHelper1.ControlFormAssemblyName = null;
            systemInfoHelper1.ControlFormClassName = null;
            systemInfoHelper1.ControlFormDYCS = null;
            systemInfoHelper1.ControlFormGongNengID = null;
            systemInfoHelper1.ControlFormNameSpace = null;
            systemInfoHelper1.ControlFromYingYongID = null;
            systemInfoHelper1.ControlName = null;
            systemInfoHelper1.ControlNameSpace = null;
            systemInfoHelper1.CurrentControlParentFrm = null;
            systemInfoHelper1.CurrentSystemDBConnStr = null;
            this.panel.developerHelper = systemInfoHelper1;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.IsDoubleBuffer = false;
            this.panel.IsHiddedTopBorder = false;
            this.panel.Location = new System.Drawing.Point(0, 49);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1008, 522);
            this.panel.TabIndex = 9;
            this.panel.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // defaultLookAndFeel1
            // 
            //this.defaultLookAndFeel1.LookAndFeel.SkinName = "mediskindevexpressstyle";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tick16.png");
            this.imageList1.Images.SetKeyName(1, "tick01.png");
            this.imageList1.Images.SetKeyName(2, "tick.png");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // clientFrmCacheWorker
            // 
            this.clientFrmCacheWorker.WorkerReportsProgress = true;
            this.clientFrmCacheWorker.WorkerSupportsCancellation = true;
            this.clientFrmCacheWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.clientFrmCacheWorker_DoWork);
            this.clientFrmCacheWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.clientFrmCacheWorker_ProgressChanged);
            this.clientFrmCacheWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.clientFrmCacheWorker_RunWorkerCompleted);
            // 
            // MainFormBase
            // 
            this.Appearance.Options.UseBackColor = true;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            //this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "MainFormBase";
            this.Text = "联众智慧";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainFormBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar Tools;
        private DevExpress.XtraBars.Bar MainMenu;
        private DevExpress.XtraBars.Bar StatusBar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStatic_Company;
        private DevExpress.XtraBars.BarStaticItem barStatic_User;
        private DevExpress.XtraBars.BarStaticItem barStatic_KeShi;
        private DevExpress.XtraBars.BarStaticItem barStatic_date;
        private DevExpress.XtraBars.BarStaticItem barStatic_IP;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private MediPanelControl panel;
        private DevExpress.XtraBars.BarStaticItem barStatic_RunningMode;
        private DevExpress.XtraBars.BarStaticItem barStatic_ServerUrl;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker clientFrmCacheWorker;
    }
}