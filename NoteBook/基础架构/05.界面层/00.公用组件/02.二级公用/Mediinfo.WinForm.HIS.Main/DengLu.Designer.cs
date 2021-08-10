using DevExpress.XtraEditors;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.WinForm.HIS.Controls;

namespace Mediinfo.WinForm.HIS.Main
{
    partial class DengLu
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper1 = new Mediinfo.WinForm.SystemInfoHelper();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DengLu));
            Mediinfo.WinForm.HIS.Controls.RepositoryItemMediGridLookUpEdit.GridLookUpEditConfig gridLookUpEditConfig1 = new Mediinfo.WinForm.HIS.Controls.RepositoryItemMediGridLookUpEdit.GridLookUpEditConfig();
            this.backgroundWorker_XiTong = new System.ComponentModel.BackgroundWorker();
            this.textBox_YongHuMing = new Mediinfo.WinForm.MediTextBox();
            this.textBox_MiMa = new Mediinfo.WinForm.MediTextBox();
            this.eGYYINGYONGBindingSource = new System.Windows.Forms.BindingSource();
            this.mediPanelControlTips = new Mediinfo.WinForm.MediPanelControl();
            this.progressPanelTips = new Mediinfo.WinForm.Common.MediWaitCircleControl();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl();
            this.mediButtonExit = new Mediinfo.WinForm.MediButton();
            this.mediButtonDengLu = new Mediinfo.WinForm.MediButton();
            this.gridLookUpEdit1 = new Mediinfo.WinForm.HIS.Controls.MediGridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.lblHospital = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.picEdit_ErWm = new DevExpress.XtraEditors.PictureEdit();
            this.mediTimer = new Mediinfo.WinForm.Common.MediTimer();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.textBox_YongHuMing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox_MiMa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eGYYINGYONGBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControlTips)).BeginInit();
            this.mediPanelControlTips.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_ErWm.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker_XiTong
            // 
            this.backgroundWorker_XiTong.WorkerReportsProgress = true;
            this.backgroundWorker_XiTong.WorkerSupportsCancellation = true;
            this.backgroundWorker_XiTong.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_XiTong_DoWork);
            this.backgroundWorker_XiTong.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_XiTong_ProgressChanged);
            this.backgroundWorker_XiTong.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_XiTong_RunWorkerCompleted);
            // 
            // textBox_YongHuMing
            // 
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
            systemInfoHelper1.CurrentFormUsedParam = null;
            systemInfoHelper1.CurrentSystemDBConnStr = null;
            this.textBox_YongHuMing.developerHelper = systemInfoHelper1;
            this.textBox_YongHuMing.EditValue = "";
            this.textBox_YongHuMing.EnterMoveNextControl = true;
            this.textBox_YongHuMing.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_YongHuMing.IsOpenEnterNext = true;
            this.textBox_YongHuMing.Location = new System.Drawing.Point(20, 10);
            this.textBox_YongHuMing.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_YongHuMing.MinimumSize = new System.Drawing.Size(0, 26);
            this.textBox_YongHuMing.Name = "textBox_YongHuMing";
            this.textBox_YongHuMing.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.textBox_YongHuMing.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.textBox_YongHuMing.Properties.Appearance.Options.UseFont = true;
            this.textBox_YongHuMing.Properties.AutoHeight = false;
            this.textBox_YongHuMing.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.textBox_YongHuMing.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_YongHuMing.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("textBox_YongHuMing.Properties.ContextImageOptions.Image")));
            this.textBox_YongHuMing.Properties.developerHelper = null;
            this.textBox_YongHuMing.Properties.NullValuePrompt = "请输入工号";
            this.textBox_YongHuMing.Properties.NullValuePromptShowForEmptyValue = true;
            this.textBox_YongHuMing.Properties.ReadOnly = true;
            this.textBox_YongHuMing.Properties.ValidateOnEnterKey = true;
            this.textBox_YongHuMing.Properties.Enter += new System.EventHandler(this.textBox_YongHuMing_Properties_Enter);
            this.textBox_YongHuMing.Size = new System.Drawing.Size(350, 45);
            this.textBox_YongHuMing.TabIndex = 1;
            this.textBox_YongHuMing.UnboundExpression = null;
            this.textBox_YongHuMing.InvalidValue += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.textBox_YongHuMing_InvalidValue);
            this.textBox_YongHuMing.DoubleClick += new System.EventHandler(this.textBox_YongHuMing_DoubleClick);
            // 
            // textBox_MiMa
            // 
            this.textBox_MiMa.developerHelper = null;
            this.textBox_MiMa.EditValue = "";
            this.textBox_MiMa.EnterMoveNextControl = true;
            this.textBox_MiMa.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_MiMa.IsOpenEnterNext = true;
            this.textBox_MiMa.Location = new System.Drawing.Point(21, 70);
            this.textBox_MiMa.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.textBox_MiMa.MinimumSize = new System.Drawing.Size(0, 26);
            this.textBox_MiMa.Name = "textBox_MiMa";
            this.textBox_MiMa.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.textBox_MiMa.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.textBox_MiMa.Properties.Appearance.Options.UseFont = true;
            this.textBox_MiMa.Properties.AutoHeight = false;
            this.textBox_MiMa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.textBox_MiMa.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("textBox_MiMa.Properties.ContextImageOptions.Image")));
            this.textBox_MiMa.Properties.developerHelper = null;
            this.textBox_MiMa.Properties.NullValuePrompt = "请输入密码";
            this.textBox_MiMa.Properties.NullValuePromptShowForEmptyValue = true;
            this.textBox_MiMa.Properties.ReadOnly = true;
            this.textBox_MiMa.Properties.UseSystemPasswordChar = true;
            this.textBox_MiMa.Size = new System.Drawing.Size(350, 45);
            this.textBox_MiMa.TabIndex = 2;
            this.textBox_MiMa.UnboundExpression = null;
            this.textBox_MiMa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_MiMa_KeyDown);
            // 
            // mediPanelControlTips
            // 
            this.mediPanelControlTips.BorderSize = 1;
            this.mediPanelControlTips.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControlTips.Controls.Add(this.progressPanelTips);
            this.mediPanelControlTips.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControlTips.developerHelper = null;
            this.mediPanelControlTips.IsDoubleBuffer = false;
            this.mediPanelControlTips.IsHiddedTopBorder = false;
            this.mediPanelControlTips.IsShowBorderColor = false;
            this.mediPanelControlTips.Location = new System.Drawing.Point(21, 250);
            this.mediPanelControlTips.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
            this.mediPanelControlTips.Name = "mediPanelControlTips";
            this.mediPanelControlTips.Size = new System.Drawing.Size(355, 35);
            this.mediPanelControlTips.TabIndex = 13;
            // 
            // progressPanelTips
            // 
            this.progressPanelTips.Activate = true;
            this.progressPanelTips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.progressPanelTips.Description = "系统正在初始化...";
            this.progressPanelTips.DescriptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressPanelTips.DescriptionFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.progressPanelTips.developerHelper = null;
            this.progressPanelTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressPanelTips.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
            this.progressPanelTips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(194)))), ((int)(((byte)(246)))));
            this.progressPanelTips.HotSpokeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(130)))), ((int)(((byte)(30)))));
            this.progressPanelTips.InnerRadius = 6F;
            this.progressPanelTips.Location = new System.Drawing.Point(0, 0);
            this.progressPanelTips.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.progressPanelTips.Name = "progressPanelTips";
            this.progressPanelTips.NumberOfSpokes = 12;
            this.progressPanelTips.Size = new System.Drawing.Size(355, 35);
            this.progressPanelTips.SpokeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(187)))), ((int)(((byte)(75)))));
            this.progressPanelTips.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
            this.progressPanelTips.TabIndex = 6;
            this.progressPanelTips.Text = "mediWaitCircleControl1";
            this.progressPanelTips.Thickness = 2F;
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.mediPanelControl1.Appearance.Options.UseBackColor = true;
            this.mediPanelControl1.BorderSize = 1;
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.mediButtonExit);
            this.mediPanelControl1.Controls.Add(this.mediButtonDengLu);
            this.mediPanelControl1.Controls.Add(this.textBox_YongHuMing);
            this.mediPanelControl1.Controls.Add(this.textBox_MiMa);
            this.mediPanelControl1.Controls.Add(this.mediPanelControlTips);
            this.mediPanelControl1.Controls.Add(this.gridLookUpEdit1);
            this.mediPanelControl1.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.IsShowBorderColor = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(470, 100);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(390, 293);
            this.mediPanelControl1.TabIndex = 0;
            // 
            // mediButtonExit
            // 
            this.mediButtonExit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.mediButtonExit.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediButtonExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.mediButtonExit.Appearance.Options.UseBackColor = true;
            this.mediButtonExit.Appearance.Options.UseFont = true;
            this.mediButtonExit.Appearance.Options.UseForeColor = true;
            this.mediButtonExit.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(175)))), ((int)(((byte)(246)))));
            this.mediButtonExit.AppearanceDisabled.Options.UseBackColor = true;
            this.mediButtonExit.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
            this.mediButtonExit.AppearanceHovered.Options.UseBackColor = true;
            this.mediButtonExit.BackgroundImage = global::Mediinfo.WinForm.HIS.Main.Properties.Resources.button_blue021;
            this.mediButtonExit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonExit.Location = new System.Drawing.Point(21, 190);
            this.mediButtonExit.Name = "mediButtonExit";
            this.mediButtonExit.Size = new System.Drawing.Size(160, 45);
            this.mediButtonExit.TabIndex = 5;
            this.mediButtonExit.Text = "退出 (&Q)";
            this.mediButtonExit.UnboundExpression = null;
            this.mediButtonExit.Click += new System.EventHandler(this.mediButtonExit_Click);
            // 
            // mediButtonDengLu
            // 
            this.mediButtonDengLu.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(156)))), ((int)(((byte)(250)))));
            this.mediButtonDengLu.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediButtonDengLu.Appearance.ForeColor = System.Drawing.Color.White;
            this.mediButtonDengLu.Appearance.Options.UseBackColor = true;
            this.mediButtonDengLu.Appearance.Options.UseFont = true;
            this.mediButtonDengLu.Appearance.Options.UseForeColor = true;
            this.mediButtonDengLu.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(175)))), ((int)(((byte)(246)))));
            this.mediButtonDengLu.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.mediButtonDengLu.AppearanceDisabled.Options.UseBackColor = true;
            this.mediButtonDengLu.AppearanceDisabled.Options.UseForeColor = true;
            this.mediButtonDengLu.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
            this.mediButtonDengLu.AppearanceHovered.Options.UseBackColor = true;
            this.mediButtonDengLu.BackgroundImage = global::Mediinfo.WinForm.HIS.Main.Properties.Resources.button_blue021;
            this.mediButtonDengLu.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediButtonDengLu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButtonDengLu.Location = new System.Drawing.Point(210, 190);
            this.mediButtonDengLu.Name = "mediButtonDengLu";
            this.mediButtonDengLu.Size = new System.Drawing.Size(160, 45);
            this.mediButtonDengLu.TabIndex = 4;
            this.mediButtonDengLu.Text = "登录 (&L)";
            this.mediButtonDengLu.UnboundExpression = null;
            this.mediButtonDengLu.Click += new System.EventHandler(this.mediButtonDengLu_Click);
            // 
            // gridLookUpEdit1
            // 
            this.gridLookUpEdit1.developerHelper = null;
            this.gridLookUpEdit1.EnterMoveNextControl = true;
            this.gridLookUpEdit1.Location = new System.Drawing.Point(21, 130);
            this.gridLookUpEdit1.Margin = new System.Windows.Forms.Padding(0, 20, 0, 24);
            this.gridLookUpEdit1.MinimumSize = new System.Drawing.Size(0, 26);
            this.gridLookUpEdit1.Name = "gridLookUpEdit1";
            this.gridLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.gridLookUpEdit1.Properties.AutoComplete = false;
            this.gridLookUpEdit1.Properties.AutoHeight = false;
            this.gridLookUpEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEdit1.Properties.CacheDataSource = null;
            this.gridLookUpEdit1.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridLookUpEdit1.Properties.ContextImageOptions.Image")));
            this.gridLookUpEdit1.Properties.DataSource = this.eGYYINGYONGBindingSource;
            this.gridLookUpEdit1.Properties.developerHelper = null;
            this.gridLookUpEdit1.Properties.DisplayMember = "YINGYONGMC";
            this.gridLookUpEdit1.Properties.ImmediatePopup = true;
            this.gridLookUpEdit1.Properties.KeyMember = "YINGYONGID";
            this.gridLookUpEdit1.Properties.NullText = "";
            this.gridLookUpEdit1.Properties.NullValuePrompt = "请选择应用";
            this.gridLookUpEdit1.Properties.NullValuePromptShowForEmptyValue = true;
            this.gridLookUpEdit1.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.StartsWith;
            this.gridLookUpEdit1.Properties.PopupFormSize = new System.Drawing.Size(0, 240);
            this.gridLookUpEdit1.Properties.PopupView = this.gridLookUpEdit1View;
            this.gridLookUpEdit1.Properties.ReadOnly = true;
            this.gridLookUpEdit1.Properties.ShowFooter = false;
            gridLookUpEditConfig1.AddSelectItem = null;
            gridLookUpEditConfig1.BiaoGeUpDown = false;
            gridLookUpEditConfig1.BindDataSource = ((object)(resources.GetObject("gridLookUpEditConfig1.BindDataSource")));
            gridLookUpEditConfig1.BindKey = null;
            gridLookUpEditConfig1.BindValue = null;
            gridLookUpEditConfig1.CacheDataSource = null;
            gridLookUpEditConfig1.ColumnIndex = null;
            gridLookUpEditConfig1.CurrentKeyChar = "";
            gridLookUpEditConfig1.DangQianFangAn = null;
            gridLookUpEditConfig1.EditStyle = Mediinfo.WinForm.HIS.Controls.EditStyle.Default;
            gridLookUpEditConfig1.FangAnParm = null;
            gridLookUpEditConfig1.FilterField = new string[] {
        "YINGYONGID",
        "YINGYONGMC"};
            gridLookUpEditConfig1.FilterRowCount = 0;
            gridLookUpEditConfig1.FilterType = new string[] {
        "1"};
            gridLookUpEditConfig1.GridColumnList = null;
            gridLookUpEditConfig1.IsAddNullItem = false;
            gridLookUpEditConfig1.IsAllLoad = false;
            gridLookUpEditConfig1.IsCache = false;
            gridLookUpEditConfig1.IsClearProject = false;
            gridLookUpEditConfig1.IsFenYe = false;
            gridLookUpEditConfig1.IsGuoLv = false;
            gridLookUpEditConfig1.IsJiXuZX = Mediinfo.WinForm.HIS.Controls.ProcessText.YES;
            gridLookUpEditConfig1.IsNotClearValue = false;
            gridLookUpEditConfig1.IsPeiZhiCX = false;
            gridLookUpEditConfig1.IsShowIndexNumber = false;
            gridLookUpEditConfig1.IsUnionAll = false;
            gridLookUpEditConfig1.ItemValue = null;
            gridLookUpEditConfig1.OrderList = null;
            gridLookUpEditConfig1.PageSize = 0;
            gridLookUpEditConfig1.Param = null;
            gridLookUpEditConfig1.PopformHeight = 0;
            gridLookUpEditConfig1.PopformWidth = 0;
            gridLookUpEditConfig1.ProfixerText = null;
            gridLookUpEditConfig1.ProFixFangAn = null;
            gridLookUpEditConfig1.QuerySql = null;
            gridLookUpEditConfig1.ShowFangAnMc = true;
            gridLookUpEditConfig1.Sql = null;
            gridLookUpEditConfig1.UserDefinedChangingList = null;
            gridLookUpEditConfig1.XiangMuMing = null;
            gridLookUpEditConfig1.XianShiLie = null;
            gridLookUpEditConfig1.YanChiJG = 0;
            this.gridLookUpEdit1.Properties.Tag = gridLookUpEditConfig1;
            this.gridLookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.gridLookUpEdit1.Properties.ValueMember = "YINGYONGID";
            this.gridLookUpEdit1.QuerySql = null;
            this.gridLookUpEdit1.Size = new System.Drawing.Size(350, 45);
            this.gridLookUpEdit1.Sql = null;
            this.gridLookUpEdit1.TabIndex = 3;
            this.gridLookUpEdit1.UnboundExpression = null;
            this.gridLookUpEdit1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridLookUpEdit1_MouseUp);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsFilter.ColumnFilterPopupRowCount = 10;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.RowAutoHeight = true;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            this.gridLookUpEdit1View.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridLookUpEdit1View.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridLookUpEdit1View.RowHeight = 24;
            // 
            // lblHospital
            // 
            this.lblHospital.AutoSize = true;
            this.lblHospital.Font = new System.Drawing.Font("微软雅黑", 17.25F);
            this.lblHospital.Location = new System.Drawing.Point(570, 55);
            this.lblHospital.Name = "lblHospital";
            this.lblHospital.Size = new System.Drawing.Size(335, 46);
            this.lblHospital.TabIndex = 22;
            this.lblHospital.Text = "新一代医院信息系统";
            // 
            // lblIP
            // 
            this.lblIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblIP.ForeColor = System.Drawing.Color.Gray;
            this.lblIP.Location = new System.Drawing.Point(495, 397);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(164, 24);
            this.lblIP.TabIndex = 23;
            this.lblIP.Text = "本机：192.168.0.1";
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(657, 397);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(180, 17);
            this.lblVersion.TabIndex = 24;
            this.lblVersion.Text = "版本号：V6.1";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopyRight.AutoSize = true;
            this.lblCopyRight.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblCopyRight.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyRight.Location = new System.Drawing.Point(495, 421);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(523, 24);
            this.lblCopyRight.TabIndex = 25;
            this.lblCopyRight.Text = "Copyright © 1999~2020 联众智慧科技股份有限公司 版权所有";
            // 
            // picEdit_ErWm
            // 
            this.picEdit_ErWm.EditValue = global::Mediinfo.WinForm.HIS.Main.Properties.Resources.ErWeiMa;
            this.picEdit_ErWm.Location = new System.Drawing.Point(778, 5);
            this.picEdit_ErWm.Name = "picEdit_ErWm";
            this.picEdit_ErWm.Properties.AllowFocused = false;
            this.picEdit_ErWm.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picEdit_ErWm.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picEdit_ErWm.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picEdit_ErWm.Size = new System.Drawing.Size(100, 96);
            this.picEdit_ErWm.TabIndex = 14;
            this.picEdit_ErWm.Visible = false;
            this.picEdit_ErWm.Click += new System.EventHandler(this.picEdit_ErWm_Click);
            // 
            // mediTimer
            // 
            this.mediTimer.Interval = 1000;
            this.mediTimer.Tick += new System.EventHandler(this.mediTimer_Tick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.Caption = "应用ID";
            this.gridColumn1.FieldName = "YINGYONGID";
            this.gridColumn1.MinWidth = 50;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 100;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.Caption = "应用名称";
            this.gridColumn2.FieldName = "YINGYONGMC";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 300;
            // 
            // DengLu
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.None;
            this.BackgroundImageStore = global::Mediinfo.WinForm.HIS.Main.Properties.Resources.login_bg_halo;
            this.ClientSize = new System.Drawing.Size(880, 452);
            this.ControlBox = false;
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.lblHospital);
            this.Controls.Add(this.mediPanelControl1);
            this.Controls.Add(this.picEdit_ErWm);
            this.DoubleBuffered = true;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DengLu";
            this.ShowInTaskbar = true;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.DengLu_Load);
            this.Shown += new System.EventHandler(this.Denglu_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DengLu_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.textBox_YongHuMing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox_MiMa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eGYYINGYONGBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControlTips)).EndInit();
            this.mediPanelControlTips.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_ErWm.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker_XiTong;
        private WinForm.MediTextBox textBox_MiMa;
        private System.Windows.Forms.BindingSource eGYYINGYONGBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private WinForm.MediPanelControl mediPanelControlTips;
        private Common.MediWaitCircleControl progressPanelTips;
        private MediPanelControl mediPanelControl1;
        private MediGridLookUpEdit gridLookUpEdit1;
        private MediTextBox textBox_YongHuMing;
        private MediButton mediButtonExit;
        private MediButton mediButtonDengLu;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private System.Windows.Forms.Label lblHospital;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyRight;
        private PictureEdit picEdit_ErWm;
        private Common.MediTimer mediTimer;
    }
}

