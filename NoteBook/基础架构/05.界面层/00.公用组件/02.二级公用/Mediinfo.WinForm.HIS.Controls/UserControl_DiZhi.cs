using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mediinfo.WinForm.HIS.Core;
using DevExpress.Utils;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class UserControl_DiZhi : UserControl
    {
        private DataTable ShengDT;
        private DataTable ShiDT;
        private DataTable XianDT;
        private  List<DataRow> ShengList;
        private  List<DataRow> ShiList;
        private  List<DataRow> XianList;
        private  List<string> Sheng;
        private  List<string> Shi;
        private  List<string> Xian;

        private bool firstFocus = false;
        private int FocusRow = 0;

        public string ShengMC;
        public string ShengID;
        public string ShiMC;
        public string ShiID;
        public string XianMC;
        public string XianID;
        public event Action<object, EventArgs> EditValueChanged;
        
        /// <summary>
        /// 控件为空默认值
        /// </summary>
        public string NullValue { get; set; }
        /// <summary>
        /// 只显示省市
        /// </summary>
        public bool IsShengShiVisable { get; set; }

        public string Text
        {
            get
            {
                return this.mediTextBoxDiZhi.Text;
            }
            set
            {
                this.mediTextBoxDiZhi.Text = value;
            }
        }

        private MediFlyoutPanel mediFlyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private MediTabControl mediTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private MediGridControl mediGridControl1;
        private MediGridView mediGridView1;
        private DevExpress.XtraGrid.Columns.GridColumn sheng;
        private MediGridControl mediGridControl2;
        private MediGridView mediGridView2;
        private DevExpress.XtraGrid.Columns.GridColumn shi;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private MediGridControl mediGridControl3;
        private MediGridView mediGridView3;
        private DevExpress.XtraGrid.Columns.GridColumn xian;
        private System.Windows.Forms.BindingSource ShengBindingSource;
        private System.Windows.Forms.BindingSource ShiBindingSource;
        private System.Windows.Forms.BindingSource XianBindingSource;

        public UserControl_DiZhi()
        {
            InitializeComponent();
        }

        private void mediTextBoxDiZhi_EditValueChanged(object sender, EventArgs e)
        {
            if(EditValueChanged!=null)
            {
                EditValueChanged(sender, e);
            }
        }

        private void UserControl_DiZhi_Load(object sender, EventArgs e)
        {
          
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            ShengDT = new DataTable();
            ShiDT = new DataTable();
            XianDT = new DataTable();
            ShengList = new List<DataRow>();
            ShiList = new List<DataRow>();
            XianList = new List<DataRow>();
            Sheng = new List<string>();
            Shi = new List<string>();
            Xian = new List<string>();
            if (IsShengShiVisable)
                this.CreatShenShiFlyPanel();
            else
                CreatFlyPanel();

            if (!string.IsNullOrEmpty(NullValue))
                this.mediTextBoxDiZhi.Properties.NullValuePrompt = NullValue;

            InitData();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="shengID">省id</param>
        /// <param name="shiID">市id</param>
        /// <param name="xianID">县id</param>
        public void Init(string shengID,string shiID,string xianID)
        {
            if (string.IsNullOrWhiteSpace(shengID)||string.IsNullOrWhiteSpace(shiID)||string.IsNullOrWhiteSpace(xianID))
                return;

            ShengDT = new DataTable();
            ShiDT = new DataTable();
            XianDT = new DataTable();
            ShengList = new List<DataRow>();
            ShiList = new List<DataRow>();
            XianList = new List<DataRow>();
            Sheng = new List<string>();
            Shi = new List<string>();
            Xian = new List<string>();
            CreatFlyPanel();
            InitData();

            DataTable dtDiQu = new DataTable();
            dtDiQu = GYShuJuZDHelper.GetShuJuZD("公用地区名称");
            var sheng = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString() == shengID).ToList();
            var shi = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString() == shiID).ToList();
            var xian = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString() == xianID).ToList();

            if (sheng.Count > 0 && shi.Count > 0 && xian.Count > 0)
            {
                this.Text = sheng[0]["DAIMAMC"].ToString() + "/" + shi[0]["DAIMAMC"].ToString() + "/" + xian[0]["DAIMAMC"].ToString();
            }
            else
            {
                MediMsgBox.Failure(this, "读取地址信息失败");
            }
        }
    

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="xianID">县id</param>
        public void Init(string xianID)
        {
            if (string.IsNullOrWhiteSpace(xianID))
                return;

            ShengDT = new DataTable();
            ShiDT = new DataTable();
            XianDT = new DataTable();
            ShengList = new List<DataRow>();
            ShiList = new List<DataRow>();
            XianList = new List<DataRow>();
            Sheng = new List<string>();
            Shi = new List<string>();
            Xian = new List<string>();
            CreatFlyPanel();
            InitData();

            DataTable dtDiQu = new DataTable();
            dtDiQu = GYShuJuZDHelper.GetShuJuZD("公用地区名称");
            var sheng = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6&& o["DAIMAID"].ToString().Substring(2, 4) == "0000" && o["DAIMAID"].ToString().Substring(0, 2) == xianID.Substring(0,2)).ToList();
            var shi= dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString().Substring(4, 2) == "00" && o["DAIMAID"].ToString().Substring(0, 4) == xianID.Substring(0,4)).ToList();
            var xian= dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString()== xianID).ToList();

            if(sheng.Count>0&&shi.Count>0&&xian.Count>0)
            {
                this.Text = sheng[0]["DAIMAMC"].ToString() + "/" + shi[0]["DAIMAMC"].ToString() + "/" + xian[0]["DAIMAMC"].ToString();
            }
            else
            {
                MediMsgBox.Failure(this, "读取地址信息失败");
            }
        }

        /// <summary>
        /// 初始化省市县数据
        /// </summary>
        private void InitData()
        {
            DataTable dtDiQu = new DataTable();
            dtDiQu = GYShuJuZDHelper.GetShuJuZD("公用地区名称");
            ShengList = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString().Substring(2, 4) == "0000").ToList();
            
            ShiList = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString().Substring(4, 2) == "00" && o["DAIMAID"].ToString().Substring(2, 2) != "00").ToList();

            XianList = dtDiQu.Select().Where(o => o["DAIMAID"].ToString().Length == 6 && o["DAIMAID"].ToString().Substring(4, 2) != "00" && o["DAIMAID"].ToString().Substring(2, 2) != "00").ToList();

            
            foreach(var item in ShengList)
            {
                Sheng.Add(item["DAIMAMC"].ToString());
            }

            foreach(var item in ShiList)
            {
                Shi.Add(item["DAIMAMC"].ToString());
            }

            foreach (var item in XianList)
            {
                Xian.Add(item["DAIMAMC"].ToString());
            }

        }

        /// <summary>
        /// 创建漂浮面板
        /// </summary>
        private void CreatFlyPanel()
        {
            //this.components = new System.ComponentModel.Container();
            //this.ShengBindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.ShiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.XianBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.ShengBindingSource = new System.Windows.Forms.BindingSource();
            this.ShiBindingSource = new System.Windows.Forms.BindingSource();
            this.XianBindingSource = new System.Windows.Forms.BindingSource();
            this.mediFlyoutPanel1 = new Mediinfo.WinForm.MediFlyoutPanel();
            this.flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            this.mediTabControl1 = new Mediinfo.WinForm.MediTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.mediGridControl1 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView1 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.mediGridControl2 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView2 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.mediGridControl3 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView3 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.sheng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.shi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xian = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mediFlyoutPanel1)).BeginInit();
            this.mediFlyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).BeginInit();
            this.flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediTabControl1)).BeginInit();
            this.mediTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShengBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XianBindingSource)).BeginInit();
            this.SuspendLayout();


           // this.ShengBindingSource.DataSource = typeof(Mediinfo.DTO.HIS.GY.E_GY_DAIMA);
            //this.eGYHUIZHENDANBindingSource.DataSource = typeof(Mediinfo.DTO.HIS.GY.E_GY_HUIZHENDAN);
            // 
            // mediFlyoutPanel1
            // 
            this.mediFlyoutPanel1.Controls.Add(this.flyoutPanelControl1);
            this.mediFlyoutPanel1.Location = new System.Drawing.Point(91, 62);
            this.mediFlyoutPanel1.Name = "mediFlyoutPanel1";
            this.mediFlyoutPanel1.Size = new System.Drawing.Size(397, 207);
            this.mediFlyoutPanel1.TabIndex = 0;
            // 
            // flyoutPanelControl1
            // 
            this.flyoutPanelControl1.Controls.Add(this.mediTabControl1);
            this.flyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl1.FlyoutPanel = this.mediFlyoutPanel1;
            this.flyoutPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.flyoutPanelControl1.Name = "flyoutPanelControl1";
            this.flyoutPanelControl1.Size = new System.Drawing.Size(397, 207);
            this.flyoutPanelControl1.TabIndex = 0;
            this.flyoutPanelControl1.Appearance.BorderColor = Color.White;
            // 
            // mediTabControl1
            // 
            this.mediTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediTabControl1.IsAdsSide = false;
            this.mediTabControl1.Location = new System.Drawing.Point(2, 2);
            this.mediTabControl1.Name = "mediTabControl1";
            this.mediTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.mediTabControl1.Size = new System.Drawing.Size(393, 203);
            this.mediTabControl1.TabIndex = 0;
            this.mediTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3
    });
            this.mediTabControl1.TabPageWidth = 80;
            this.mediTabControl1.MediTabControlTheme = MediTabControlStyle.LineTabHeaderTrans;
            this.mediTabControl1.LeftDistance = 0;
            this.mediTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            this.mediTabControl1.AppearancePage.Header.ForeColor = Color.Black;//具体颜色问薛菲
            this.mediTabControl1.AppearancePage.HeaderActive.ForeColor = Color.Blue;//
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.mediGridControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(387, 162);
            this.xtraTabPage1.Text = "省";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.mediGridControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(387, 162);
            this.xtraTabPage2.Text = "市";
            this.xtraTabPage2.PageEnabled = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.mediGridControl3);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(387, 162);
            this.xtraTabPage3.Text = "县";
            this.xtraTabPage3.PageEnabled = false;
            // 
            // mediGridControl1
            // 
            this.mediGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl1.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl1.MainView = this.mediGridView1;
            this.mediGridControl1.Name = "mediGridControl1";
            this.mediGridControl1.Size = new System.Drawing.Size(387, 162);
            this.mediGridControl1.TabIndex = 0;
            this.mediGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView1});
            this.mediGridControl1.DataSource = this.ShengBindingSource;
            // 
            // mediGridView1
            // 
            this.mediGridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView1.ColumnPanelRowHeight = 24;
            this.mediGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.sheng});
            //this.mediGridView1.DataLayoutCustomValue = null;
            //this.mediGridView1.DataLayoutDefaultValue = null;
            this.mediGridView1.EditableState = false ;
            //this.mediGridView1.GetDataRowInfo = null;
            //this.mediGridView1.GetDataRowList = null;
            //this.mediGridView1.GetList = null;
            this.mediGridView1.GridControl = this.mediGridControl1;
            //this.mediGridView1.GridviewColumnDefaultValuedt = null;
            //this.mediGridView1.GridviewOtherTypeExpressiondt = null;
            //this.mediGridView1.GridviewTabIndexdt = null;
            this.mediGridView1.Name = "mediGridView1";
            this.mediGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView1.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView1.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView1.OptionsView.ColumnAutoWidth = false;
            this.mediGridView1.OptionsView.EnableAppearanceEvenRow = false;
            this.mediGridView1.OptionsView.EnableAppearanceOddRow = false;
            this.mediGridView1.IsShowLine = false;//不显示网格线
            this.mediGridView1.OptionsView.ShowGroupPanel = false;
            this.mediGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.OptionsView.ShowIndicator = false;
            this.mediGridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.RequiredFieldItem = "";
            this.mediGridView1.RowSpace = 8;
            this.mediGridView1.OptionsView.ShowColumnHeaders = false;
            this.mediGridView1.RowClick += mediGridView1_RowClick;

            //this.sheng.FieldName = "DAIMAMC";
            //this.sheng.Caption = "省";
            //this.colHUIZHENDID.Name = "colHUIZHENDID";
            //this.colHUIZHENDID.Visible = true;
            //this.colHUIZHENDID.VisibleIndex = 0;

            // 
            // mediGridControl2
            // 
            this.mediGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl2.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl2.MainView = this.mediGridView2;
            this.mediGridControl2.Name = "mediGridControl2";
            this.mediGridControl2.Size = new System.Drawing.Size(387, 162);
            this.mediGridControl2.TabIndex = 0;
            this.mediGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView2});
            this.mediGridControl2.DataSource = this.ShiBindingSource;
            // 
            // mediGridView2
            // 
            this.mediGridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView2.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView2.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView2.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView2.ColumnPanelRowHeight = 24;
            this.mediGridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.shi});
            //this.mediGridView2.DataLayoutCustomValue = null;
            //this.mediGridView2.DataLayoutDefaultValue = null;
            this.mediGridView2.EditableState = false;
            //this.mediGridView2.GetDataRowInfo = null;
            //this.mediGridView2.GetDataRowList = null;
            //this.mediGridView2.GetList = null;
            this.mediGridView2.GridControl = this.mediGridControl2;
            //this.mediGridView2.GridviewColumnDefaultValuedt = null;
            //this.mediGridView2.GridviewOtherTypeExpressiondt = null;
            //this.mediGridView2.GridviewTabIndexdt = null;
            this.mediGridView2.Name = "mediGridView2";
            this.mediGridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView2.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView2.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView2.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView2.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView2.OptionsView.ColumnAutoWidth = false;
            this.mediGridView2.OptionsView.EnableAppearanceEvenRow = false;
            this.mediGridView2.OptionsView.EnableAppearanceOddRow = false;
            this.mediGridView2.IsShowLine = false;
            this.mediGridView2.OptionsView.ShowGroupPanel = false;
            this.mediGridView2.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView2.OptionsView.ShowIndicator = false;
            this.mediGridView2.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView2.RequiredFieldItem = "";
            this.mediGridView2.RowSpace = 8;
            this.mediGridView2.OptionsView.ShowColumnHeaders = false;
            this.mediGridView2.RowClick += mediGridView2_RowClick;



            // 
            // mediGridControl3
            // 
            this.mediGridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl3.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl3.MainView = this.mediGridView3;
            this.mediGridControl3.Name = "mediGridControl3";
            this.mediGridControl3.Size = new System.Drawing.Size(387, 162);
            this.mediGridControl3.TabIndex = 0;
            this.mediGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView3});
            this.mediGridControl3.DataSource = this.XianBindingSource;
            // 
            // mediGridView3
            // 
            this.mediGridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView3.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView3.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView3.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView3.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView3.ColumnPanelRowHeight = 24;
            this.mediGridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.xian});
            //this.mediGridView3.DataLayoutCustomValue = null;
            //this.mediGridView3.DataLayoutDefaultValue = null;
            this.mediGridView3.EditableState = false;
            //this.mediGridView3.GetDataRowInfo = null;
            //this.mediGridView3.GetDataRowList = null;
            //this.mediGridView3.GetList = null;
            this.mediGridView3.GridControl = this.mediGridControl3;
            //this.mediGridView3.GridviewColumnDefaultValuedt = null;
            //this.mediGridView3.GridviewOtherTypeExpressiondt = null;
            //this.mediGridView3.GridviewTabIndexdt = null;
            this.mediGridView3.Name = "mediGridView3";
            this.mediGridView3.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView3.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView3.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView3.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView3.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView3.OptionsView.ColumnAutoWidth = false;
            this.mediGridView3.OptionsView.EnableAppearanceEvenRow = false;
            this.mediGridView3.OptionsView.EnableAppearanceOddRow = false;
            this.mediGridView3.IsShowLine = false;
            this.mediGridView3.OptionsView.ShowGroupPanel = false;
            this.mediGridView3.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView3.OptionsView.ShowIndicator = false;
            this.mediGridView3.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView3.RequiredFieldItem = "";
            this.mediGridView3.RowSpace = 8;
            this.mediGridView3.OptionsView.ShowColumnHeaders = false;
            this.mediGridView3.RowClick += mediGridView3_RowClick;

            //this.sheng.FieldName = "DAIMAMC";
            //this.sheng.Caption = "省";
            //// 
            //// shi
            //// 
            //this.shi.Caption = "市";
            //this.shi.FieldName = "DAIMAMC";
            //this.shi.Name = "DAIMAMC";
            //this.shi.Visible = true;
            //this.shi.VisibleIndex = 0;
            //// 
            //// xian
            //// 
            //this.xian.Caption = "县";
            //this.xian.FieldName = "DAIMAMC";
            //this.xian.Name = "DAIMAMC";
            //this.xian.Visible = true;
            //this.xian.VisibleIndex = 0;
            // 
            // Form2
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Controls.Add(this.mediFlyoutPanel1);


            ((System.ComponentModel.ISupportInitialize)(this.mediFlyoutPanel1)).EndInit();
            this.mediFlyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).EndInit();
            this.flyoutPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediTabControl1)).EndInit();
            this.mediTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShengBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XianBindingSource)).EndInit();
            this.ResumeLayout(false);
    }

        private void CreatShenShiFlyPanel()
        {
            //this.components = new System.ComponentModel.Container();
            //this.ShengBindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.ShiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.XianBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.ShengBindingSource = new System.Windows.Forms.BindingSource();
            this.ShiBindingSource = new System.Windows.Forms.BindingSource();
            this.XianBindingSource = new System.Windows.Forms.BindingSource();
            this.mediFlyoutPanel1 = new Mediinfo.WinForm.MediFlyoutPanel();
            this.flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            this.mediTabControl1 = new Mediinfo.WinForm.MediTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.mediGridControl1 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView1 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.mediGridControl2 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView2 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.mediGridControl3 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView3 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.sheng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.shi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xian = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mediFlyoutPanel1)).BeginInit();
            this.mediFlyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).BeginInit();
            this.flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediTabControl1)).BeginInit();
            this.mediTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShengBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XianBindingSource)).BeginInit();
            this.SuspendLayout();


            // this.ShengBindingSource.DataSource = typeof(Mediinfo.DTO.HIS.GY.E_GY_DAIMA);
            //this.eGYHUIZHENDANBindingSource.DataSource = typeof(Mediinfo.DTO.HIS.GY.E_GY_HUIZHENDAN);
            // 
            // mediFlyoutPanel1
            // 
            this.mediFlyoutPanel1.Controls.Add(this.flyoutPanelControl1);
            this.mediFlyoutPanel1.Location = new System.Drawing.Point(91, 62);
            this.mediFlyoutPanel1.Name = "mediFlyoutPanel1";
            this.mediFlyoutPanel1.Size = new System.Drawing.Size(397, 207);
            this.mediFlyoutPanel1.TabIndex = 0;
            this.mediFlyoutPanel1.LostFocus += new EventHandler(this.mediFlyoutPanel1_lostfocus);
            // 
            // flyoutPanelControl1
            // 
            this.flyoutPanelControl1.Controls.Add(this.mediTabControl1);
            this.flyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl1.FlyoutPanel = this.mediFlyoutPanel1;
            this.flyoutPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.flyoutPanelControl1.Name = "flyoutPanelControl1";
            this.flyoutPanelControl1.Size = new System.Drawing.Size(397, 207);
            this.flyoutPanelControl1.TabIndex = 0;
            this.flyoutPanelControl1.Appearance.BorderColor = Color.White;//边框不要蓝色
            // 
            // mediTabControl1
            // 
            this.mediTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediTabControl1.IsAdsSide = false;
            this.mediTabControl1.Location = new System.Drawing.Point(2, 2);
            this.mediTabControl1.Name = "mediTabControl1";
            this.mediTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.mediTabControl1.Size = new System.Drawing.Size(393, 203);
            this.mediTabControl1.TabIndex = 0;
            this.mediTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2
    });
            this.mediTabControl1.TabPageWidth = 80;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.mediGridControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(387, 162);
            this.xtraTabPage1.Text = "省";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.mediGridControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(387, 162);
            this.xtraTabPage2.Text = "市";
            this.xtraTabPage2.PageEnabled = false;
          
            // 
            // mediGridControl1
            // 
            this.mediGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl1.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl1.MainView = this.mediGridView1;
            this.mediGridControl1.Name = "mediGridControl1";
            this.mediGridControl1.Size = new System.Drawing.Size(387, 162);
            this.mediGridControl1.TabIndex = 0;
            this.mediGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView1});
            this.mediGridControl1.DataSource = this.ShengBindingSource;
            // 
            // mediGridView1
            // 
            this.mediGridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView1.ColumnPanelRowHeight = 24;
            this.mediGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.sheng});
            //this.mediGridView1.DataLayoutCustomValue = null;
            //this.mediGridView1.DataLayoutDefaultValue = null;
            this.mediGridView1.EditableState = false;
            //this.mediGridView1.GetDataRowInfo = null;
            //this.mediGridView1.GetDataRowList = null;
            //this.mediGridView1.GetList = null;
            this.mediGridView1.GridControl = this.mediGridControl1;
            //this.mediGridView1.GridviewColumnDefaultValuedt = null;
            //this.mediGridView1.GridviewOtherTypeExpressiondt = null;
            //this.mediGridView1.GridviewTabIndexdt = null;
            this.mediGridView1.Name = "mediGridView1";
            this.mediGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView1.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView1.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView1.OptionsView.ColumnAutoWidth = false;
            this.mediGridView1.OptionsView.EnableAppearanceEvenRow = false;
            this.mediGridView1.OptionsView.EnableAppearanceOddRow = false;
            this.mediGridView1.IsShowLine = false;
            this.mediGridView1.OptionsView.ShowGroupPanel = false;
            this.mediGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.OptionsView.ShowIndicator = false;
            this.mediGridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.RequiredFieldItem = "";
            this.mediGridView1.RowSpace = 8;
            this.mediGridView1.OptionsView.ShowColumnHeaders = false;
            this.mediGridView1.RowClick += mediGridView1_RowClick;

            //this.sheng.FieldName = "DAIMAMC";
            //this.sheng.Caption = "省";
            //this.colHUIZHENDID.Name = "colHUIZHENDID";
            //this.colHUIZHENDID.Visible = true;
            //this.colHUIZHENDID.VisibleIndex = 0;

            // 
            // mediGridControl2
            // 
            this.mediGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl2.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl2.MainView = this.mediGridView2;
            this.mediGridControl2.Name = "mediGridControl2";
            this.mediGridControl2.Size = new System.Drawing.Size(387, 162);
            this.mediGridControl2.TabIndex = 0;
            this.mediGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView2});
            this.mediGridControl2.DataSource = this.ShiBindingSource;
            // 
            // mediGridView2
            // 
            this.mediGridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView2.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView2.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView2.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView2.ColumnPanelRowHeight = 24;
            this.mediGridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.shi});
            //this.mediGridView2.DataLayoutCustomValue = null;
            //this.mediGridView2.DataLayoutDefaultValue = null;
            this.mediGridView2.EditableState = false;
            //this.mediGridView2.GetDataRowInfo = null;
            //this.mediGridView2.GetDataRowList = null;
            //this.mediGridView2.GetList = null;
            this.mediGridView2.GridControl = this.mediGridControl2;
            //this.mediGridView2.GridviewColumnDefaultValuedt = null;
            //this.mediGridView2.GridviewOtherTypeExpressiondt = null;
            //this.mediGridView2.GridviewTabIndexdt = null;
            this.mediGridView2.Name = "mediGridView2";
            this.mediGridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView2.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView2.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView2.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView2.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView2.OptionsView.ColumnAutoWidth = false;
            this.mediGridView2.OptionsView.EnableAppearanceEvenRow = false;
            this.mediGridView2.OptionsView.EnableAppearanceOddRow = false;
            this.mediGridView2.IsShowLine = false;
            this.mediGridView2.OptionsView.ShowGroupPanel = false;
            this.mediGridView2.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView2.OptionsView.ShowIndicator = false;
            this.mediGridView2.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView2.RequiredFieldItem = "";
            this.mediGridView2.RowSpace = 8;
            this.mediGridView2.OptionsView.ShowColumnHeaders = false;
            this.mediGridView2.RowClick += mediGridView2_RowClick;



            // 
            // mediGridControl3
            // 
            this.mediGridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl3.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl3.MainView = this.mediGridView3;
            this.mediGridControl3.Name = "mediGridControl3";
            this.mediGridControl3.Size = new System.Drawing.Size(387, 162);
            this.mediGridControl3.TabIndex = 0;
            this.mediGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView3});
            this.mediGridControl3.DataSource = this.XianBindingSource;
            // 
            // mediGridView3
            // 
            this.mediGridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView3.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView3.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView3.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView3.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView3.ColumnPanelRowHeight = 24;
            this.mediGridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.xian});
            //this.mediGridView3.DataLayoutCustomValue = null;
            //this.mediGridView3.DataLayoutDefaultValue = null;
            this.mediGridView3.EditableState = false;
            //this.mediGridView3.GetDataRowInfo = null;
            //this.mediGridView3.GetDataRowList = null;
            //this.mediGridView3.GetList = null;
            this.mediGridView3.GridControl = this.mediGridControl3;
            //this.mediGridView3.GridviewColumnDefaultValuedt = null;
            //this.mediGridView3.GridviewOtherTypeExpressiondt = null;
            //this.mediGridView3.GridviewTabIndexdt = null;
            this.mediGridView3.Name = "mediGridView3";
            this.mediGridView3.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView3.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView3.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView3.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView3.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView3.OptionsView.ColumnAutoWidth = false;
            this.mediGridView3.OptionsView.EnableAppearanceEvenRow = false;
            this.mediGridView3.OptionsView.EnableAppearanceOddRow = false;
            this.mediGridView3.IsShowLine = false;
            this.mediGridView3.OptionsView.ShowGroupPanel = false;
            this.mediGridView3.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView3.OptionsView.ShowIndicator = false;
            this.mediGridView3.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView3.RequiredFieldItem = "";
            this.mediGridView3.RowSpace = 8;
            this.mediGridView3.OptionsView.ShowColumnHeaders = false;
            this.mediGridView3.RowClick += mediGridView3_RowClick;

            //this.sheng.FieldName = "DAIMAMC";
            //this.sheng.Caption = "省";
            //// 
            //// shi
            //// 
            //this.shi.Caption = "市";
            //this.shi.FieldName = "DAIMAMC";
            //this.shi.Name = "DAIMAMC";
            //this.shi.Visible = true;
            //this.shi.VisibleIndex = 0;
            //// 
            //// xian
            //// 
            //this.xian.Caption = "县";
            //this.xian.FieldName = "DAIMAMC";
            //this.xian.Name = "DAIMAMC";
            //this.xian.Visible = true;
            //this.xian.VisibleIndex = 0;
            // 
            // Form2
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Controls.Add(this.mediFlyoutPanel1);


            ((System.ComponentModel.ISupportInitialize)(this.mediFlyoutPanel1)).EndInit();
            this.mediFlyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).EndInit();
            this.flyoutPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediTabControl1)).EndInit();
            this.mediTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShengBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XianBindingSource)).EndInit();
            this.ResumeLayout(false);
        }
        private void mediFlyoutPanel1_lostfocus(object sender, EventArgs e)
        {
            mediFlyoutPanel1.HidePopup();
        }
        private void mediTextBoxDiZhi_Click(object sender, EventArgs e)
        {
            mediFlyoutPanel1.Size = new Size(this.Width, 300);
            mediFlyoutPanel1.OwnerControl = this.mediTextBoxDiZhi;

            if (mediFlyoutPanel1.FlyoutPanelState.IsActive)
            {
                return;
            }
            Rectangle rec = this.RectangleToScreen(this.ClientRectangle);
            this.mediFlyoutPanel1.Location = new Point(rec.X, rec.Y + this.Height);
            mediFlyoutPanel1.OptionsBeakPanel.BeakLocation = BeakPanelBeakLocation.Top;

            mediFlyoutPanel1.ShowBeakForm();
            this.mediGridControl1.DataSource = Sheng;

            this.mediGridView1.PopulateColumns();
            mediTabControl1.SelectedTabPageIndex = 0;
            if (sender != null )//&& firstFocus
            {
                mediGridView1.FocusedRowHandle = 0;
                string strSheng = (sender as Mediinfo.WinForm.MediTextBox).Text;
                string sheng = string.Empty;
                if (strSheng.Contains("/"))
                {
                    sheng = strSheng.Substring(0, strSheng.IndexOf('/'));
                }
                else
                {
                    sheng = strSheng;
                }

                int i = 0;
                foreach (var item in Sheng)
                {
                    if (!sheng.Contains(item))
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                FocusRow = i;
                this.mediGridView1.FocusedRowHandle = i;
                try
                {
                    mediGridView1.RowCellStyle += MediGridView1_RowCellStyle;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            firstFocus = true;

            this.mediGridView1.PopulateColumns();
        }

        private void MediGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == FocusRow)
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 115, 195, 100);
                e.Appearance.Font = new Font(Font, FontStyle.Bold);
            }
        }
        private void MediGridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == FocusRow)
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 115, 195, 100);
                e.Appearance.Font = new Font(Font, FontStyle.Bold);
            }
        }

        private void MediGridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == FocusRow)
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 115, 195, 100);
                e.Appearance.Font = new Font(Font, FontStyle.Bold);
            }
        }

        /// <summary>
        /// 选择省
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediGridView1_RowClick(object sender ,EventArgs e)
        {
            if (mediGridView1.FocusedRowHandle < 0)
                return;

            ShengMC = ShengList[mediGridView1.FocusedRowHandle]["DAIMAMC"].ToString();

            ShengID = ShengList[mediGridView1.FocusedRowHandle]["DAIMAID"].ToString();

            var ShiListtemp = ShiList.Where(o => o["DAIMAID"].ToString().Substring(4, 2) == "00"&& o["DAIMAID"].ToString().Substring(0, 2) == ShengID.Substring(0,2)).ToList();
            Shi.Clear();
            foreach (var item in ShiListtemp)
            {              
                Shi.Add(item["DAIMAMC"].ToString());
            }

            this.mediGridControl2.DataSource = Shi;
            this.mediGridView2.PopulateColumns();


            //如果省底下有市，就跳转到市
            if (Shi.Count > 0)
            {
                this.xtraTabPage2.PageEnabled = true;
                this.mediTabControl1.SelectedTabPageIndex = 1;
                mediTextBoxDiZhi.Text = ShengMC;
            }//直辖市没有市的直接跳转到县
            else
            {
                Xian.Clear();
                var XianListtemp = XianList.Where(o => o["DAIMAID"].ToString().Substring(4, 2) != "00" && o["DAIMAID"].ToString().Substring(0, 2) == ShengID.Substring(0, 2)).ToList();
                foreach (var item in XianListtemp)
                {
                    Xian.Add(item["DAIMAMC"].ToString());
                }

                this.xtraTabPage3.PageEnabled = true;
                this.mediGridControl3.DataSource = Xian;
                this.mediGridView3.PopulateColumns();
                this.mediTabControl1.SelectedTabPageIndex = 2;
     
                mediTextBoxDiZhi.Text = ShengMC + " /" + ShengMC;
            }
            if (sender != null)
            {
                FocusRow = 0;
                this.mediGridView2.FocusedRowHandle = 0;
                try
                {
                    mediGridView2.RowCellStyle += MediGridView2_RowCellStyle;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }


        /// <summary>
        /// 选择市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediGridView2_RowClick(object sender, EventArgs e)
        {
            if (mediGridView2.FocusedRowHandle < 0)
                return;

            ShiMC = mediGridView2.GetRowCellValue(mediGridView2.FocusedRowHandle, "Column").ToString();

            ShiID = ShiList.Where(o => o["DAIMAMC"].ToString() == ShiMC).ToList().FirstOrDefault()["DAIMAID"].ToString();
            Xian.Clear();
            if (IsShengShiVisable)
            {
                mediTextBoxDiZhi.Text = mediTextBoxDiZhi.Text + " /" + ShiMC;
                return;
            }
            var XianListtemp = XianList.Where(o => o["DAIMAID"].ToString().Substring(4, 2) != "00" && o["DAIMAID"].ToString().Substring(0, 4) == ShiID.Substring(0, 4)).ToList();
            foreach (var item in XianListtemp)
            {
                Xian.Add(item["DAIMAMC"].ToString());
            }

            this.xtraTabPage3.PageEnabled = true;
            this.mediGridControl3.DataSource = Xian;
            this.mediGridView3.PopulateColumns();
            this.mediTabControl1.SelectedTabPageIndex = 2;
            mediGridView3.RowCellStyle += MediGridView3_RowCellStyle;

            string replace = mediTextBoxDiZhi.Text.Replace(" /", "");
            int count = mediTextBoxDiZhi.Text.Length - replace.Length;
            if (count < 1)
            {
                mediTextBoxDiZhi.Text = mediTextBoxDiZhi.Text + " /" + ShiMC;
            }
            else
            {
                string[] diZhi = mediTextBoxDiZhi.Text.Split('/');
                mediTextBoxDiZhi.Text = diZhi[0] + " /" + ShiMC + " /";
            }
            if (sender != null)
            {
                FocusRow = 0;
                this.mediGridView3.FocusedRowHandle = 0;
                try
                {
                    mediGridView3.RowCellStyle += MediGridView3_RowCellStyle;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// 选择县
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediGridView3_RowClick(object sender, EventArgs e)
        {
            XianMC = mediGridView3.GetRowCellValue(mediGridView3.FocusedRowHandle, "Column").ToString();

            XianID = XianList.Where(o => o["DAIMAMC"].ToString() == XianMC).ToList().FirstOrDefault()["DAIMAID"].ToString();

            if (mediTextBoxDiZhi.Text.Contains("/"))
            {
                string replace = mediTextBoxDiZhi.Text.Replace(" /", "");
                int count = (mediTextBoxDiZhi.Text.Length - replace.Length)/2;
                if(count<2)
                {
                    mediTextBoxDiZhi.Text = mediTextBoxDiZhi.Text +" /"+ XianMC;
                }
                else if(count==2)
                {
                    string[] diZhi = mediTextBoxDiZhi.Text.Split('/');
                    mediTextBoxDiZhi.Text = diZhi[0] + "/" + diZhi[1] + "/" + XianMC;
                }

                mediFlyoutPanel1.HideBeakForm();
            }
            
        }

        /// <summary>
        /// 不能输入任何值，相当于只读，背景色为白色的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediTextBoxDiZhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar>0)
            {
                if (e.KeyChar.ToString()=="A")
                {
                    this.xtraTabPage2.PageEnabled = true;
                    this.mediTabControl1.SelectedTabPageIndex = 1;
                    mediTextBoxDiZhi.Text = "安徽省";
                }
            }
            e.Handled = true;
        }
    }
}
