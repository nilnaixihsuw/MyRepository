using DevExpress.Skins;
using DevExpress.Skins.Info;
using DevExpress.XtraEditors;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls.FirstLevelFramework;
using Mediinfo.WinForm.HIS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 菜单方式打开窗体基类
    /// </summary>
    public partial class MediFormWithQX : MediForm
    {
       /// <summary>
        /// 病人ID
        /// </summary>
        public string bingRenId { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        public string CaiDanID { get; set; }

        //窗体调用参数赋值委托
        /// <summary>
        /// 是否关闭窗体
        /// </summary>
        public bool IsCloseQXForm { get; set; }
        /// <summary>
        /// 鼠标是否在关闭按钮上
        /// </summary>
        public bool IsMouseInCloseButton { get; set; }

        public MediTabControl MediLCTabControl { get; set; }

        /// <summary>
        /// 窗体关闭参数
        /// </summary>

        public bool IsExistFormClosingEventArgs = false;

        /// <summary>
        /// 菜单参数
        /// </summary>
        public List<object> GongNengCS { get; set; }

        /// <summary>
        /// 数据是否更改
        /// </summary>
        public bool DataModified { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediFormWithQX()
        {
            InitializeComponent();

            this.Shown -= MediFormWithQX_Shown;
            this.Load -= MediFormWithQX_Load;
            this.Shown += MediFormWithQX_Shown;
            this.Load += MediFormWithQX_Load;
            this.ResizeBegin += (s, e) => { this.SuspendLayout(); };
            this.ResizeEnd += (s, e) => { this.ResumeLayout(true); };
        }

        private void MediFormWithQX_Shown(object sender, EventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                IMMModeHelper.RegisterIMMMode(this);
                if (HISClientHelper.USERID == "DBA")
                    RecursionControl(null);
                else
                {
                    LoadChuangKouZY();
                }

            }
        }

        /// <summary>
        ///  验证数据修改状态
        /// </summary>
        /// <param name="ModifiedState">修改--true,未修改--false</param>
        /// <param name="e"></param>
        public void ValidateDataModify(bool ModifiedState, FormClosingEventArgs e)
        {
            if (!ModifiedState)
            {
                IsCloseQXForm = true;
                IsExistFormClosingEventArgs = false;
                e.Cancel = false;
                return;
            }

            if (MediMsgBox.YesNo("数据未保存，是否关闭?", MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                IsCloseQXForm = true;
                IsExistFormClosingEventArgs = false;
                e.Cancel = false;
            }
            else
            {
                IsCloseQXForm = false;
                IsExistFormClosingEventArgs = true;
                e.Cancel = true;
            }
        }



        private void MediFormWithQX_TextChanged(object sender, EventArgs e)
        {
            if (HISClientHelper.MainForm != null)
            {
                if (this is MediFormWithoutTitleQX)
                    return;
                //gxl   2019.9.18   只要应用名称即可，不需要后面的名字了
                HISClientHelper.MainForm.Text = HISClientHelper.YINGYONGMC;
                ButtonForOpenChuangKou.GlobalClientMainForm.ReFreshChuangkouText(this);
            }
        }

        #region 窗口通过菜单打开时传递的相关参数

        /// <summary>
        /// 窗口调用参数
        /// </summary>
        [Browsable(false)]
        public string DiaoYongCS { get; set; }

        /// <summary>
        /// 窗口名称,等于this.GetType().Name,this.Name
        /// </summary>
        [Browsable(false)]
        public string FormName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DiaoYongCS))
                    return this.Name;

                if (string.IsNullOrWhiteSpace(DiaoYongCS.Split('|')[0]))
                    return this.Name;
                else
                    return DiaoYongCS.Split('|')[0];
            }
        }

        /// <summary>
        /// 功能ID
        /// </summary>
        [Browsable(false)]
        public string GongNengId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DiaoYongCS))
                    return string.Empty;

                var canShu = DiaoYongCS.Split('|');

                if (canShu.Length >= 2)
                    return canShu[1];
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 窗口调用参数
        /// </summary>
        [Browsable(false)]
        public string ChuangKouCS
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DiaoYongCS))
                    return string.Empty;

                var canShu = DiaoYongCS.Split('|');

                if (canShu.Length >= 3)
                    return canShu[2];
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 窗口的打开方式
        /// </summary>
        [Browsable(false)]
        public string DaKaiFS
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DiaoYongCS))
                    return string.Empty;

                var canShu = DiaoYongCS.Split('|');

                if (canShu.Length >= 4)
                    return canShu[3];
                else
                    return string.Empty;
            }
        }

        #endregion 窗口通过菜单打开时传递的相关参数

        /// <summary>
        /// 窗口资源
        /// </summary>
        protected List<E_GY_CHUANGKOUZY_NEW> chuangKouZYList;

        /// <summary>
        /// gridview布局信息
        /// </summary>
        protected E_GY_DATALAYOUTDTO dataLayoutInfo;

        /// <summary>
        /// 用户权限信息
        /// </summary>
        protected List<E_GY_JUESECKQX> yongHuQXList;
        protected List<E_GY_JUESECKQX_NEW> yongHuQX_NEWList;
        /// <summary>
        /// 创建服务实例
        /// </summary>

        /// <summary>
        /// 自定义控件列表（按钮）
        /// </summary>
        protected List<Control> controlList = new List<Control>();

        /// <summary>
        /// 存储布局信息
        /// </summary>
        private E_GY_DATALAYOUT1 _EDataLayout1 = null;

        /// <summary>
        /// 存储布局信息详情
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;

        /// <summary>
        /// 重写加载窗体事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        //表达式属性不为空的控件集合
        private Dictionary<string, string> unBoundExpressionDic = new Dictionary<string, string>();

        /// <summary>
        /// TABINDEX TABLE
        /// </summary>
        public DataTable GridviewTabIndexdt { get; set; }

        /// <summary>
        /// 列默认值
        /// </summary>
        public DataTable GridviewColumnDefaultValuedt { get; set; }

        private DataTable UnBoundGridviewExpressiondt { get; set; }

        /// <summary>
        /// 列默认值
        /// </summary>
        public DataTable GridviewOtherTypeExpressiondt { get; set; }

        /// <summary>
        /// 创建跳转索引表
        /// </summary>
        private DataTable CreateGridviewTabIndexTable()
        {
            DataTable gridviewTabIndexdt = new DataTable();
            gridviewTabIndexdt.Columns.AddRange(new DataColumn[] { new DataColumn("GridviewName", typeof(string)), new DataColumn("ColumnName", typeof(string)), new DataColumn("TabIndex", typeof(int)) });
            return gridviewTabIndexdt;
        }

        #region 窗体自按钮自定义属性及权限控制

        /// <summary>
        ///  加载自定义窗口按钮
        /// </summary>
        private void LoadChuangKouZY()
        {
            if (SkinCat.Instance.IsDesignMode) return;

            if (this.FindForm() == null) return;
            // RecursionUnboundExpressionControls(this.FindForm());

            //1.获取按钮控件信息
            chuangKouZYList = GYChuangKouZYHelper.GetByForm(this.GetType().Namespace, this.Name);
            if (chuangKouZYList == null || chuangKouZYList.Count == 0)
                return;
            //需要控制的情况下才遍历查找按钮
            if (chuangKouZYList.Where(c => c.XIANSHIKZ == 1 || c.QUANXIANKZ == 1).Count() > 0)
            {
                RecursionMediButton(this.Controls);
            }

            //获取权限
            List<string> quanXian = new List<string>();
            foreach (var item in controlList)
            {
                quanXian.Add(string.Format("{0}.{1}.{2}.{3}.{4}", HISClientHelper.YINGYONGID, GongNengId, this.GetType().Namespace, this.Name, item.Name));
            }
            var quanXianDict = GYQuanXianHelper.GetQuanXian(quanXian);

            //设置按钮的属性

            //3.获取用户权限信息
            yongHuQX_NEWList = GYQuanXianHelper.GetJueSeYHQX();
            if (yongHuQX_NEWList != null)
            {
                foreach (var jsyhqxbutton in yongHuQX_NEWList)
                {
                    Control[] jsqxctr = this.FindForm().Controls.Find(jsyhqxbutton.CONTROLNAME, true);
                    foreach (var item in jsqxctr)
                    {
                        if (item is MediButton)
                        {
                            //文字
                            if (!jsyhqxbutton.WENZI.IsNullOrWhiteSpace())
                                item.Text = jsyhqxbutton.WENZI;

                            //显示标志
                            if (jsyhqxbutton.XIANSHIBZ == 1)
                                item.Visible = true;
                            else
                                item.Visible = false;
                        }
                    }
                }
            }

            if (controlList != null)
            {
                foreach (Control item in controlList)
                {
                    var chuangKouZY = chuangKouZYList.Where(c => c.CONTROLNAME == item.Name).FirstOrDefault();

                    if (chuangKouZY == null)
                        continue;

                    if (chuangKouZY.XIANSHIKZ == 1 || chuangKouZY.QUANXIANKZ == 1)
                    {
                        SimpleButton sbtn = (SimpleButton)item;

                        foreach (var jsyhqxbutton in yongHuQX_NEWList)
                        {
                            if (sbtn.Name == jsyhqxbutton.CONTROLNAME && chuangKouZY.XIANSHIKZ == 1)
                            {
                                // 文字
                                if (!chuangKouZY.WENZI.IsNullOrWhiteSpace())
                                    sbtn.Text = jsyhqxbutton.WENZI;

                                // 显示标志
                                if (jsyhqxbutton.XIANSHIBZ == 1)
                                    sbtn.Visible = true;
                                else
                                    sbtn.Visible = false;
                            }
                            if (sbtn.Name == jsyhqxbutton.CONTROLNAME && chuangKouZY.QUANXIANKZ == 1)
                            {
                                item.Enabled = quanXianDict.ContainsKey(string.Format("{0}.{1}.{2}", this.GetType().Namespace, this.Name, item.Name));
                            }
                        }

                        //颜色
                        if (!chuangKouZY.YANSE.IsNullOrWhiteSpace())
                        {
                            sbtn.Appearance.Options.UseForeColor = true;
                            Regex regex = new Regex(@"RGB\(\d*\,\d*,\d*\)");
                            if (regex.IsMatch(chuangKouZY.YANSE))
                            {
                                string[] rgbstrings = regex.Match(chuangKouZY.YANSE).Value.Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                int r = Convert.ToInt32(rgbstrings[1]);

                                int g = Convert.ToInt32(rgbstrings[2]);

                                int b = Convert.ToInt32(rgbstrings[3]);
                                sbtn.Appearance.ForeColor = Color.FromArgb(r, g, b);
                            }
                        }

                        //字体大小
                        if (chuangKouZY.ZITIDX.ToInt(2) != 2)
                            sbtn.Font = new Font("微软雅黑", float.Parse(chuangKouZY.ZITIDX.ToInt(11).ToString()));
                    }
                }
            }
        }

        private void Gridview_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (sender is DevExpress.XtraGrid.Views.Grid.GridView)
            {
                DevExpress.XtraGrid.Views.Grid.GridView gridView = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                if (gridView.GridControl == null)
                    return;
                DataView dv = GridviewColumnDefaultValuedt.DefaultView;
                DataTable DistTable = dv.ToTable("Dist", true, "GridviewName", "columnName", "DefaultValue");
                DataRow[] drs = DistTable.Select("GridviewName = '" + gridView.Name + "'and DefaultValue <> ''");
                for (int i = 0; i < drs.Length; i++)
                {
                    for (int j = 0; j < gridView.DataRowCount; j++)
                    {
                        if (gridView.GetRowCellValue(j, drs[i]["columnName"].ToString()) != null || gridView.GetRowCellValue(j, drs[i]["columnName"].ToString()).ToString() != "")
                            continue;
                        gridView.SetRowCellValue(j, drs[i]["columnName"].ToString(), drs[i]["DefaultValue"]);
                    }
                }
            }
        }

        private void RecursionUnboundExpressionControls(Control control)
        {
            if (control is WinForm.MediTitleBar)
            {
                WinForm.MediTitleBar MediTitleBar = (WinForm.MediTitleBar)control;
                MediTitleBar.MouseDoubleClick += MediTitleBar_MouseDoubleClick;
            }
            foreach (Control item in control.Controls)
            {
                if (item is MediButton)
                {
                    MediButton mediButton = (MediButton)item;
                    if (!unBoundExpressionDic.ContainsKey(mediButton.Name))
                        unBoundExpressionDic.Add(mediButton.Name, mediButton.UnboundExpression);
                }
                else if (item is MediTextBox)
                {
                    MediTextBox mediTextBox = (MediTextBox)item;
                    if (!unBoundExpressionDic.ContainsKey(mediTextBox.Name))
                        unBoundExpressionDic.Add(mediTextBox.Name, mediTextBox.UnboundExpression);
                }
                else if (item is MediComboBox)
                {
                    MediComboBox mediComboBox = (MediComboBox)item;
                    if (!unBoundExpressionDic.ContainsKey(mediComboBox.Name))
                        unBoundExpressionDic.Add(mediComboBox.Name, mediComboBox.UnboundExpression);
                }
                else if (item is MediGridLookUpEdit)
                {
                    MediGridLookUpEdit mediGridLookUpEdit = (MediGridLookUpEdit)item;
                    if (!unBoundExpressionDic.ContainsKey(mediGridLookUpEdit.Name))
                        unBoundExpressionDic.Add(mediGridLookUpEdit.Name, mediGridLookUpEdit.UnboundExpression);
                }
                else if (item is DevExpress.XtraGrid.GridControl)
                {
                    DevExpress.XtraGrid.GridControl gridcontrol = (DevExpress.XtraGrid.GridControl)item;
                    DevExpress.XtraGrid.Repository.ViewRepositoryCollection gridviewcolloction = gridcontrol.ViewCollection;
                    foreach (var tempgridview in gridviewcolloction)
                    {
                        if (tempgridview is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            DevExpress.XtraGrid.Views.Grid.GridView gridview = tempgridview as DevExpress.XtraGrid.Views.Grid.GridView;

                            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridview.Columns)
                            {
                                if (!unBoundExpressionDic.ContainsKey(gridcontrol.Name))
                                {
                                    UnBoundGridviewExpressiondt.Rows.Add(gridcontrol.Name, gridview.Name, column.Name, column.UnboundExpression);
                                }
                            }
                        }
                    }
                }

                RecursionUnboundExpressionControls(item);
            }
        }

        private void MediTitleBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            controlList.Clear();
            //判断按钮个数决定是否弹出按钮权限窗体
            int medButtonCount = 0;
            if (sender is Control)
                foreach (Control item in ((Control)sender).Controls)
                    if (item is MediButton)
                    {
                        medButtonCount++;
                        controlList.Add(item);
                    }

            //如果当前窗体是跳过Gridcontrol则
            if (sender is MediGridControl || medButtonCount < 1)
                return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //在不存在的情况下才遍历界面上的按钮
                if (controlList.Count() <= 0)
                    RecursionMediButton(this.Controls);

                //弹出管理界面
                //MediXiTongFZForm form = new MediXiTongFZForm(this.GetType().Namespace, this.Name, controlList, GongNengId);

                FrmButtonSetting form = new FrmButtonSetting(this.GetType().Namespace, this.Name, controlList, GongNengId);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    GYChuangKouZYHelper.RefreshChuangKouInfo(this.GetType().Namespace, this.Name);
                }
                form.Dispose();
            }
        }

        /// <summary>
        /// 递归遍历界面所有的Button按钮
        /// </summary>
        /// <param name="controls"></param>
        private void RecursionMediButton(Control.ControlCollection controls)
        {
            if (controls.Count == 0) return;

            foreach (Control ctl in controls)
            {
                if (ctl.Controls.Count > 0)
                {
                    RecursionMediButton(ctl.Controls);
                }
                else if (ctl is BaseButton)
                {
                    if (controlList.Where(o => o.Name == ctl.Name).ToList().Count > 0)
                        controlList.Remove(controlList.Where(o => o.Name == ctl.Name).FirstOrDefault());
                    controlList.Add(ctl as SimpleButton);
                }
            }
        }

        /// <summary>
        /// 设置Panel的双击事件
        /// </summary>
        private void RecursionControl(Control parentCtrl)
        {
            if (parentCtrl == null)
                parentCtrl = this;

            if (parentCtrl.Controls.Count == 0) return;

            foreach (Control ctr in parentCtrl.Controls)
            {
                if (ctr.Controls.Count <= 0)
                {
                    if (ctr is BaseButton)
                    {
                        if (controlList.Where(o => o.Name == ctr.Name).ToList().Count > 0)
                            controlList.Remove(controlList.FirstOrDefault(o => o.Name == ctr.Name));
                        controlList.Add(ctr as SimpleButton);
                    }
                }
                else
                {
                    ctr.MouseDoubleClick -= Ctr_MouseDoubleClick;
                    ctr.MouseDoubleClick += Ctr_MouseDoubleClick;
                    RecursionControl(ctr);
                }
            }
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MediTitleBar mediTitleBar = sender as MediTitleBar;
            MediPanelControl mediPanel = sender as MediPanelControl;
            if (mediTitleBar == null && mediPanel == null)
                return;
            //判断按钮个数决定是否弹出按钮权限窗体
            int medButtonCount = 0;
            if (sender is Control)
                foreach (var item in ((Control)sender).Controls)
                    if (item is MediButton)
                        medButtonCount++;
            //如果当前窗体是跳过Gridcontrol则
            if (sender is MediGridControl || medButtonCount < 1)
                return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //在不存在的情况下才遍历界面上的按钮
                if (controlList.Count() <= 0)
                    RecursionMediButton(this.Controls);

                if (string.IsNullOrEmpty(GongNengId))
                {
                    return;
                }
                FrmButtonSetting form = new FrmButtonSetting(this.GetType().Namespace, this.Name, controlList, GongNengId);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    GYChuangKouZYHelper.RefreshChuangKouInfo(this.GetType().Namespace, this.Name);
                }
                form.Dispose();
            }
        }

        #endregion 窗体自按钮自定义属性及权限控制

        /// <summary>
        /// 重写父窗体消息拦截机制
        /// </summary>
        /// <param name="msg"></param>
        //protected override void WndProc(ref Message msg)
        //{
        //    base.WndProc(ref msg);

        //    if (mediForm == null) return;

        //    RecursionMediButtonControls(mediForm);
        //}

        private void RecursionMediButtonControls(Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (control is WinForm.MediTitleBar)
                {
                    WinForm.MediTitleBar MediTitleBar = (WinForm.MediTitleBar)control;

                    MediTitleBar.MouseDoubleClick -= MediTitleBar_MouseDoubleClick;
                    MediTitleBar.MouseDoubleClick -= MediTitleBar_MouseDoubleClick;
                    MediTitleBar.MouseDoubleClick += MediTitleBar_MouseDoubleClick;
                }

                RecursionMediButtonControls(item);
            }
        }

        /// <summary>
        /// 通过接口加载窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T LoadWindow<T>()
        {
            T form = WinFormLocator.Instance.LoadWindow<T>();
            return form;
        }

        /// <summary>
        /// 加载帮助类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T LoadHelper<T>()
        {
            T form = HelperLocator.Instance.GetService<T>();
            return form;
        }

        /// <summary>
        /// 在主窗口中加载
        /// </summary>
        public void ShowInWindow()
        {
            //this.TopLevel = false;
            this.TopMost = true;
            //this.MdiParent = HISClientHelper.MainForm.MdiParent;
            //this.WindowState = FormWindowState.Normal;
            //this.StartPosition = FormStartPosition.CenterScreen;
            this.MdiParent = (HISClientHelper.MainForm as MainFormBase);
            this.Dock = DockStyle.Fill;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            foreach (Control item in (HISClientHelper.MainForm as MainFormBase).GetPanel().Controls)
            {
                MediFormWithQX mediFormWithQX = item as MediFormWithQX;

                if (mediFormWithQX != null)
                {
                    mediFormWithQX.Hide();
                }
            }
            (HISClientHelper.MainForm as MainFormBase).GetPanel().Controls.Add(this);
            if (!HISClientHelper.MainForm.Text.Contains(this.Text))
            {
                HISClientHelper.MainForm.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, this.Text);
                ButtonForOpenChuangKou.GlobalClientMainForm.ReFreshChuangkouText(this);
            }
            this.Show();
        }

        public bool IsPaint = false;

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (HISClientHelper.MainForm != null && IsPaint)
            {
                Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);

                if (this.StartPosition == FormStartPosition.CenterScreen)
                {
                    int frameHeight = HISClientHelper.MainForm.Location.Y;
                    int frameWidth = HISClientHelper.MainForm.Location.X;
                    int currentformheight = this.Size.Height;
                    int currentformwidth = this.Size.Width;
                    int newformx = frameWidth + HISClientHelper.MainForm.Width / 2 - currentformwidth / 2;
                    int newformy = frameHeight + HISClientHelper.MainForm.Height / 2 - currentformheight / 2;
                    if (ButtonForOpenChuangKou.GlobalClientMainForm.GetPanel() != null && !ButtonForOpenChuangKou.GlobalClientMainForm.GetPanel().Contains(this))
                        this.SetDesktopLocation(newformx, newformy);
                }
                IsPaint = false;
            }
        }

        //AutoSizeFormClass asc = new AutoSizeFormClass();

        private void MediFormWithQX_SizeChanged(object sender, EventArgs e)
        {
            //MediFormWithQX medi = (MediFormWithQX)sender;
            //object o = medi.StartPosition;
            //  asc.controlAutoSize(this);
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (ButtonForOpenChuangKou.GlobalClientMainForm != null
                    && ButtonForOpenChuangKou.GlobalClientMainForm.panelInnerFrmSort.Count > 0 && ButtonForOpenChuangKou.GlobalClientMainForm.panelInnerFrmSort[0].Value != null && ButtonForOpenChuangKou.GlobalClientMainForm.panelInnerFrmSort[0].Value.Visible)
                {
                    if (ButtonForOpenChuangKou.GlobalClientMainForm.GetPanel().Contains(this))
                        ((MediFormWithQX)ButtonForOpenChuangKou.GlobalClientMainForm.panelInnerFrmSort[0].Value).WindowState = FormWindowState.Maximized;
                }

                //if (!ButtonForOpenChuangKou.GlobalClientMainForm.GetPanel().Contains(this))
                //{
                //    this.StartPosition = FormStartPosition.CenterParent;
                //}

            }
            IsPaint = true;
        }

        private void MediFormWithQX_Load(object sender, EventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                this.TextChanged -= MediFormWithQX_TextChanged;

                this.TextChanged += MediFormWithQX_TextChanged;
                foreach (Control item in this.Controls)
                {
                    if (item is MediDataLayoutControl)
                    {
                        MediDataLayoutControl mediDataLayoutControl = item as MediDataLayoutControl;
                        mediDataLayoutControl.initialDataLayoutDel();
                    }

                    //DiGuiGridControl(item);
                }
                if (ButtonForOpenChuangKou.GlobalClientMainForm != null) ButtonForOpenChuangKou.GlobalClientMainForm.ReFreshChuangkouText(this);

            }
            else
            {
                //((DevExpress.LookAndFeel.Design.UserLookAndFeelDefault)DevExpress.LookAndFeel.Design.UserLookAndFeelDefault.Default).LoadSettings(() =>
                //{
                //    var skinCreator = new SkinBlobXmlCreator("mediskindevexpressstyle", this.GetType().Assembly.GetName().Name + ".SkinData.", typeof(MediFormWithQX).Assembly, null);
                //    SkinManager.Default.RegisterSkin(skinCreator);
                //});
                //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("mediskindevexpressstyle");
            }
        }

        /// <summary>
        /// 递归控件
        /// </summary>
        /// <param name="control"></param>
        private void DiGuiGridControl(Control control)
        {
            foreach (Control item in control.Controls)
            {
                //if (item is MediGridControl)
                //{
                //    MediGridControl mediGridControl = item as MediGridControl;
                //    DevExpress.XtraGrid.Repository.ViewRepositoryCollection gridviewcolloction = mediGridControl.ViewCollection;
                //    foreach (var gridview in gridviewcolloction)
                //    {
                //        if (gridview is MediGridView)
                //        {
                //            ((MediGridView)gridview).initialGridviewLayoutDel();
                //        }
                //    }
                //}

                if (item is MediDataLayoutControl)
                {
                    MediDataLayoutControl mediDataLayoutControl = item as MediDataLayoutControl;
                    mediDataLayoutControl.initialDataLayoutDel();
                }
                DiGuiGridControl(item);
            }
        }

        private void MediFormWithQX_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ButtonForOpenChuangKou.GlobalClientMainForm != null)
            {
                ////gxl  2019.9.18  关闭菜单时清除原来的窗体名，变成应用名
                //HISClientHelper.MainForm.Text = HISClientHelper.YINGYONGMC;
                ButtonForOpenChuangKou.GlobalClientMainForm.RemoveCloseButtonFireChuangKouCK(this);
            }
            FlushMemory();
            // this.Dispose();

        }


        //by panjh 2020.03.05 手动调用gc
        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        private static void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void MediFormWithQX_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!DataModified)
                {
                    IsCloseQXForm = true;
                    IsExistFormClosingEventArgs = false;
                    e.Cancel = false;
                }
                else
                {
                    if (MediMsgBox.YesNo("数据未保存，是否关闭?", MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        IsCloseQXForm = true;
                        IsExistFormClosingEventArgs = false;
                        e.Cancel = false;
                    }
                    else
                    {
                        IsCloseQXForm = false;
                        IsExistFormClosingEventArgs = true;
                        e.Cancel = true;
                        return;
                    }
                }

                if (IsCloseQXForm)
                {
                    ButtonForOpenChuangKou.GlobalClientMainForm?.RemoveCloseButtonFireChuangKouCK(this);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 关闭界面时检查数据是否更改(按需重写并设置DataModified)
        /// </summary>
        public virtual void ValidateDataModifiedBeforeClose()
        {
            //DataModified = 检查数据是否修改
        }
        /// <summary>
        /// 阿里病历质控窗体关闭控制
        /// </summary>
        /// <param name="visible"></param>
        public virtual void ALiBingLiZJVisible(bool visible) { }

        /// <summary>
        /// 刷新界面数据
        /// </summary>
        public virtual void RefreshFormData()
        {

        }
    }
}