namespace Mediinfo.WinForm.HIS.Main
{
    partial class ServerConnectTestForm
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
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper5 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper2 = new Mediinfo.WinForm.SystemInfoHelper();
            this.mediLayoutControl1 = new Mediinfo.WinForm.Common.MediLayoutControl();
            this.exceptionMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.medibtnDetailInfo = new Mediinfo.WinForm.MediButton();
            this.QuitMediBtn = new Mediinfo.WinForm.MediButton();
            this.repeatMediBtn = new Mediinfo.WinForm.MediButton();
            this.mediCustomProgressBar1 = new Mediinfo.WinForm.HIS.Controls.mediCustomProgressBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.serverConnectTestWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.mediLayoutControl1)).BeginInit();
            this.mediLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exceptionMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // mediLayoutControl1
            // 
            this.mediLayoutControl1.AutoScroll = false;
            this.mediLayoutControl1.Controls.Add(this.exceptionMemoEdit);
            this.mediLayoutControl1.Controls.Add(this.medibtnDetailInfo);
            this.mediLayoutControl1.Controls.Add(this.QuitMediBtn);
            this.mediLayoutControl1.Controls.Add(this.repeatMediBtn);
            this.mediLayoutControl1.Controls.Add(this.mediCustomProgressBar1);
            systemInfoHelper5.ControlAssemblyName = null;
            systemInfoHelper5.ControlClassName = null;
            systemInfoHelper5.ControlForFormName = null;
            systemInfoHelper5.ControlFormAssemblyName = null;
            systemInfoHelper5.ControlFormClassName = null;
            systemInfoHelper5.ControlFormDYCS = null;
            systemInfoHelper5.ControlFormGongNengID = null;
            systemInfoHelper5.ControlFormNameSpace = null;
            systemInfoHelper5.ControlFromYingYongID = null;
            systemInfoHelper5.ControlName = null;
            systemInfoHelper5.ControlNameSpace = null;
            systemInfoHelper5.CurrentControlParentFrm = null;
            systemInfoHelper5.CurrentSystemDBConnStr = null;
            this.mediLayoutControl1.developerHelper = systemInfoHelper5;
            this.mediLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.mediLayoutControl1.MaximumSize = new System.Drawing.Size(284, 0);
            this.mediLayoutControl1.MinimumSize = new System.Drawing.Size(284, 115);
            this.mediLayoutControl1.Name = "mediLayoutControl1";
            this.mediLayoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.mediLayoutControl1.OptionsView.AlwaysScrollActiveControlIntoView = false;
            this.mediLayoutControl1.Root = this.layoutControlGroup1;
            this.mediLayoutControl1.Size = new System.Drawing.Size(284, 300);
            this.mediLayoutControl1.TabIndex = 1;
            this.mediLayoutControl1.Text = "mediLayoutControl1";
            // 
            // exceptionMemoEdit
            // 
            this.exceptionMemoEdit.Location = new System.Drawing.Point(2, 116);
            this.exceptionMemoEdit.Name = "exceptionMemoEdit";
            this.exceptionMemoEdit.Properties.ReadOnly = true;
            this.exceptionMemoEdit.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.exceptionMemoEdit.Size = new System.Drawing.Size(280, 182);
            this.exceptionMemoEdit.StyleController = this.mediLayoutControl1;
            this.exceptionMemoEdit.TabIndex = 5;
            // 
            // medibtnDetailInfo
            // 
            this.medibtnDetailInfo.Appearance.Options.UseTextOptions = true;
            this.medibtnDetailInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.medibtnDetailInfo.developerHelper = null;
            this.medibtnDetailInfo.Location = new System.Drawing.Point(2, 85);
            this.medibtnDetailInfo.Name = "medibtnDetailInfo";
            this.medibtnDetailInfo.Size = new System.Drawing.Size(69, 27);
            this.medibtnDetailInfo.StyleController = this.mediLayoutControl1;
            this.medibtnDetailInfo.TabIndex = 4;
            this.medibtnDetailInfo.Text = "详细信息";
            this.medibtnDetailInfo.UnboundExpression = null;
            this.medibtnDetailInfo.Click += new System.EventHandler(this.medibtnDetailInfo_Click);
            // 
            // QuitMediBtn
            // 
            this.QuitMediBtn.Appearance.Options.UseTextOptions = true;
            this.QuitMediBtn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.QuitMediBtn.developerHelper = null;
            this.QuitMediBtn.Location = new System.Drawing.Point(208, 85);
            this.QuitMediBtn.Name = "QuitMediBtn";
            this.QuitMediBtn.Size = new System.Drawing.Size(74, 27);
            this.QuitMediBtn.StyleController = this.mediLayoutControl1;
            this.QuitMediBtn.TabIndex = 1;
            this.QuitMediBtn.Text = "退出(&Q)";
            this.QuitMediBtn.UnboundExpression = null;
            this.QuitMediBtn.Click += new System.EventHandler(this.QuitMediBtn_Click);
            // 
            // repeatMediBtn
            // 
            this.repeatMediBtn.Appearance.Options.UseTextOptions = true;
            this.repeatMediBtn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repeatMediBtn.developerHelper = null;
            this.repeatMediBtn.Location = new System.Drawing.Point(132, 85);
            this.repeatMediBtn.Name = "repeatMediBtn";
            this.repeatMediBtn.Size = new System.Drawing.Size(72, 27);
            this.repeatMediBtn.StyleController = this.mediLayoutControl1;
            this.repeatMediBtn.TabIndex = 0;
            this.repeatMediBtn.Text = "重试(&R)";
            this.repeatMediBtn.UnboundExpression = null;
            this.repeatMediBtn.Click += new System.EventHandler(this.repeatMediBtn_Click);
            // 
            // mediCustomProgressBar1
            // 
            systemInfoHelper2.ControlAssemblyName = null;
            systemInfoHelper2.ControlClassName = null;
            systemInfoHelper2.ControlForFormName = null;
            systemInfoHelper2.ControlFormAssemblyName = null;
            systemInfoHelper2.ControlFormClassName = null;
            systemInfoHelper2.ControlFormDYCS = null;
            systemInfoHelper2.ControlFormGongNengID = null;
            systemInfoHelper2.ControlFormNameSpace = null;
            systemInfoHelper2.ControlFromYingYongID = null;
            systemInfoHelper2.ControlName = null;
            systemInfoHelper2.ControlNameSpace = null;
            systemInfoHelper2.CurrentControlParentFrm = null;
            systemInfoHelper2.CurrentSystemDBConnStr = null;
            this.mediCustomProgressBar1.developerHelper = systemInfoHelper2;
            this.mediCustomProgressBar1.IsVisiblePercent = false;
            this.mediCustomProgressBar1.Location = new System.Drawing.Point(2, 2);
            this.mediCustomProgressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mediCustomProgressBar1.MaximumSize = new System.Drawing.Size(280, 79);
            this.mediCustomProgressBar1.MinimumSize = new System.Drawing.Size(280, 79);
            this.mediCustomProgressBar1.Name = "mediCustomProgressBar1";
            this.mediCustomProgressBar1.ShowCaption = false;
            this.mediCustomProgressBar1.ShowDescription = true;
            this.mediCustomProgressBar1.Size = new System.Drawing.Size(280, 79);
            this.mediCustomProgressBar1.TabIndex = 0;
            this.mediCustomProgressBar1.Totaluploadedcount = 0;
            this.mediCustomProgressBar1.UploadCountDes = "服务器连接失败...";
            this.mediCustomProgressBar1.UploadedFilePercentProcess = "";
            this.mediCustomProgressBar1.UpLoadFileInfo = "请稍后";
            this.mediCustomProgressBar1.Load += new System.EventHandler(this.mediCustomProgressBar1_Load);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 300);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.mediCustomProgressBar1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(284, 83);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.repeatMediBtn;
            this.layoutControlItem2.Location = new System.Drawing.Point(130, 83);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(76, 31);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.QuitMediBtn;
            this.layoutControlItem3.Location = new System.Drawing.Point(206, 83);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(78, 31);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(73, 83);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(57, 31);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.medibtnDetailInfo;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 83);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(73, 31);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.exceptionMemoEdit;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 114);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(284, 186);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // serverConnectTestWorker
            // 
            this.serverConnectTestWorker.WorkerReportsProgress = true;
            this.serverConnectTestWorker.WorkerSupportsCancellation = true;
            this.serverConnectTestWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.serverConnectTestWorker_DoWork);
            this.serverConnectTestWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.serverConnectTestWorker_ProgressChanged);
            this.serverConnectTestWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.serverConnectTestWorker_RunWorkerCompleted);
            // 
            // ServerConnectTestForm
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(284, 300);
            this.Controls.Add(this.mediLayoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.MaximumSize = new System.Drawing.Size(284, 300);
            this.MinimumSize = new System.Drawing.Size(284, 300);
            this.Name = "ServerConnectTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "服务端连接重试";
            this.Load += new System.EventHandler(this.ServerConnectTestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediLayoutControl1)).EndInit();
            this.mediLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exceptionMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Common.MediLayoutControl mediLayoutControl1;
        private MediButton QuitMediBtn;
        private MediButton repeatMediBtn;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private HIS.Controls.mediCustomProgressBar mediCustomProgressBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.ComponentModel.BackgroundWorker serverConnectTestWorker;
        private DevExpress.XtraEditors.MemoEdit exceptionMemoEdit;
        private MediButton medibtnDetailInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}