using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.Utility.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 报表预览控件
    /// </summary>
    public partial class MediReportView : MediUserControl
    {
        #region 属性变量定义
        /// <summary>
        /// 报表对象
        /// </summary>
        private MediXtraReport report = new MediXtraReport();

        /// <summary>
        /// 单据服务
        /// </summary>
        JCJGDanJuXXService gyDanJuXXService;

        /// <summary>
        /// 查询条件集合
        /// </summary>
        List<E_GY_DANJUCXTJ> listCXTJ = new List<E_GY_DANJUCXTJ>();

        /// <summary>
        /// 数据源集合
        /// </summary>
        List<E_GY_DANJUSJY> listSJY = new List<E_GY_DANJUSJY>();

        /// <summary>
        /// HIS查询服务
        /// </summary>
        JCJGQuerySqlService query;

        /// <summary>
        /// 报表数据流
        /// </summary>
        private Stream reportStream = new MemoryStream();

        /// <summary>
        /// 参数字典
        /// </summary>
        private Dictionary<string, string> systemParamDic = new Dictionary<string, string>();


        /// <summary>
        /// 是否在后台进程建立报表页面  默认不是 
        /// </summary>
        public bool BuildPagesInBackground = false;

        /// <summary>
        /// 是否是报表
        /// </summary>
        private bool isBaoBiao = false;

        /// <summary>
        /// 单据id
        /// </summary>
        private string danJuID;

        /// <summary>
        /// 是否是勾选
        /// </summary>
        public bool IsGouXuan = false;

        /// <summary>
        /// 报表内容是否加载完成
        /// </summary>
        bool isJiaZaiCG = false;

        /// <summary>
        /// 加载时是否显示等待界面 默认显示
        /// </summary>
        public bool IsShowWaitFrom = true;

        /// <summary>
        /// 勾选页面打印时，选择页面记录的数据源
        /// </summary>
        public object PrintDataSoure;
        /// <summary>
        /// 是否显示上一页下一页
        /// </summary>
        public bool IsShowPage { set; get; } = false;



        ToolTipController toolTipController = new ToolTipController();
        /// <summary>
        /// 报表单据ID
        /// </summary>
        [Browsable(false)]
        public string DanJuID
        {
            get { return danJuID; }
            set
            {
                danJuID = value;
                if (danJuID != null)
                {
                    if (gyDanJuXXService == null)
                    {
                        gyDanJuXXService = new JCJGDanJuXXService();
                    }
                    if (query == null)
                    {
                        query = new JCJGQuerySqlService();
                    }
                    BindData();
                    ReportPrint.JiLuHCBBID(DanJuID);
                }
            }
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        private Dictionary<string, object> queryParam = new Dictionary<string, object>();


        /// <summary>
        /// 数据源
        /// </summary>
        private dynamic dataSourece = null;

        /// <summary>
        /// 查询参数
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, object> QueryParam { get { return queryParam; } set { queryParam = value; } }


        /// <summary>
        /// 报表数据源
        /// </summary>
        [Browsable(false)]
        public dynamic DataSource { get { return dataSourece; } set { dataSourece = value; } }


        /// <summary>
        /// 明细报表数据源
        /// </summary>
        private dynamic detailDataSource = null;

        /// <summary>
        /// 明细报表数据源报表数据源
        /// </summary>
        [Browsable(false)]
        public dynamic DetailDataSource
        {
            get => detailDataSource;
            set => detailDataSource = value;
        }

        /// <summary>
        /// 子报表数据源字典，key为子报表名字，value 为对应数据源
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, dynamic> detailDSDic = new Dictionary<string, dynamic>();
        private bool showPrintBar = true;
        /// <summary>
        /// 是否显示打印工具栏
        /// </summary>
        public bool ShowPrintBar
        {
            get { return showPrintBar; }
            set
            {
                if (value == false)
                {
                    mediPictureEdit1.Visible = false;
                }
                showPrintBar = value;
            }
        }

        #endregion

        #region 对外提供方法
        /// <summary>
        /// 实例化控件
        /// </summary>
        public MediReportView()
        {
            InitializeComponent();
            report.doActionParm += doActionParm;
            report.doActionMouseMove += doActionMouseMoveEvent;
        }


        /// <summary>
        /// 报表击事件
        /// </summary>
        public Action<object> doActionCilkCell;

        /// <summary>
        /// 鼠标移动到控件事件, 第一个参数为选中行的内容，第二个参数为鼠标所在控件
        /// </summary>
        public Action<object, object> doActionMouseMove;
        /// <summary>
        /// 报表击事件
        /// </summary>
        /// <param name="obj"></param>
        private void doActionParm(object obj)
        {
            doActionCilkCell?.Invoke(obj);
        }

        /// <summary>
        /// 光标移动到控件时触发事件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mouseEvent"></param>
        private void doActionMouseMoveEvent(object obj, object mouseEvent)
        {
            var control = ((PreviewMouseEventArgs)mouseEvent).PreviewControl;
            doActionMouseMove?.Invoke(obj, control);
        }


        /// <summary>
        /// 清除报表
        /// </summary>
        public void ClearReport()
        {
            mediPanelControl_Main.Controls.Clear();
            MediPictureEdit mediPictureEdit = new MediPictureEdit();
            mediPictureEdit.Dock = DockStyle.Fill;
            mediPictureEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            mediPictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            mediPictureEdit.Properties.Appearance.Options.UseBackColor = true;
            mediPictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            mediPictureEdit.Properties.RelativeImagePath = "";
            mediPictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            mediPictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            mediPictureEdit.Properties.UnboundExpression = null;
            mediPictureEdit.RelativeImagePath = "..\\PIC\\reportView.png";
            mediPanelControl_Main.Controls.Add(mediPictureEdit);
        }
        /// <summary>
        /// 刷新点击后字体颜色
        /// </summary>
        public void RefForeColor()
        {
            foreach (Control item in mediPanelControl_Main.Controls)
            {
                if (item.GetType() == typeof(PrintControl))
                {
                    ((PrintControl)item).Refresh();
                }
            }
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void SetsystemParamDic(string Key, string Value)
        {
            if (this.systemParamDic.ContainsKey(Key))
            {
                this.systemParamDic[Key] = Value;
            }
            else
            {
                this.systemParamDic.Add(Key, Value);
            }
        }
        /// <summary>
        /// 载入报表内容
        /// </summary>       
        public void LoadReport()
        {
            if (IsShowWaitFrom)
            {
                AsynWaitForm.CreateWaitForm(this.ParentForm as MediForm, "", "正在加载报表，请稍候......");
                AsynWaitForm.SetPercentVisible(false);
            }
            try
            {
                report.LoadLayout(reportStream);
                if (report.doActionMouseMove == null)
                {
                    report.doActionMouseMove += doActionMouseMove;
                }
                if (QueryParam != null && QueryParam.Count > 0)
                {
                    foreach (var item in QueryParam)
                    {
                        bool cunZai = false;
                        foreach (var p in report.Parameters)
                        {
                            if (p.Name == item.Key)
                            {
                                p.Value = item.Value;
                                cunZai = true;
                                break;
                            }
                        }
                        if (!cunZai)
                        {
                            report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Name = item.Key, Value = item.Value, Visible = false });
                        }
                    }
                }
                if (detailDataSource != null && report.FindControl("DetailReport", true) != null)
                {
                    ((DetailReportBand)report.FindControl("DetailReport", true)).DataSource = detailDataSource;
                }

                if (detailDSDic.Count > 0)
                {
                    foreach (var item in detailDSDic)
                    {
                        if (item.Value != null && report.FindControl(item.Key, true) != null)
                        {
                            ((DetailReportBand)report.FindControl(item.Key, true)).DataSource = item.Value;
                        }
                    }
                }
                report.DataSource = DataSource;
                if (report.IsDynamics == EnumShiFou.是)
                {
                    new PRTReport(report).InitDetailXRTable();
                }
                else
                {
                    //加载数据源                
                    if (!IsGouXuan)
                    {
                        GetDataSoucre();
                    }
                }
                if (mediPanelControl_Main.Controls.Count > 0 && mediPanelControl_Main.Controls[0].GetType() == typeof(PrintControl))
                {
                    ((PrintControl)mediPanelControl_Main.Controls[0]).PrintingSystem = report.PrintingSystem;
                }
                else
                {
                    PrintControl printControl1 = new PrintControl();
                    printControl1.DoubleClick += PrintControl1_DoubleClick;
                    printControl1.Dock = DockStyle.Fill;
                    printControl1.PrintingSystem = report.PrintingSystem;
                    printControl1.Document.RemoveService(typeof(IWaitIndicator));
                    //展示

                    //操作要显示什么按钮
                    if (ShowPrintBar)
                    {
                        PrintBarManager printBarManager = new PrintBarManager();
                        printBarManager.Form = printControl1;
                        printBarManager.Initialize(printControl1);
                        printBarManager.MainMenu.Visible = false;
                        printBarManager.StatusBar.Visible = false;
                        if (!IsShowPage)//是否显示上一页/下一页
                        {
                            printControl1.PrintingSystem.SetCommandVisibility(new PrintingSystemCommand[]{
                                 PrintingSystemCommand.Open,
                                 PrintingSystemCommand.Save,
                                 PrintingSystemCommand.ClosePreview,
                                 PrintingSystemCommand.Customize,
                                 PrintingSystemCommand.SendCsv,
                                 PrintingSystemCommand.SendFile,
                                 PrintingSystemCommand.SendGraphic,
                                 PrintingSystemCommand.SendMht,
                                 PrintingSystemCommand.SendPdf,
                                 PrintingSystemCommand.SendRtf,
                                 PrintingSystemCommand.SendTxt
                             }, CommandVisibility.None);
                        }
                        else //add by hujian@2021/1/14 报表自定义展示添加上一页下一页
                        {
                            printControl1.PrintingSystem.SetCommandVisibility(new PrintingSystemCommand[]{
                                 PrintingSystemCommand.Open,
                                 PrintingSystemCommand.Save,
                                 PrintingSystemCommand.ClosePreview,
                                 PrintingSystemCommand.Customize,
                                 PrintingSystemCommand.SendCsv,
                                 PrintingSystemCommand.SendFile,
                                 PrintingSystemCommand.SendGraphic,
                                 PrintingSystemCommand.SendMht,
                                 PrintingSystemCommand.SendPdf,
                                 PrintingSystemCommand.SendRtf,
                                 PrintingSystemCommand.SendTxt,
                                 PrintingSystemCommand.ShowFirstPage,
                                 PrintingSystemCommand.ShowLastPage,
                                 PrintingSystemCommand.ShowNextPage,
                                 PrintingSystemCommand.ShowPrevPage
                             }, CommandVisibility.None);
                        }
                    }
                    mediPanelControl_Main.Controls.Clear();
                    mediPanelControl_Main.Controls.Add(printControl1);
                }
                //处理计算字段
                foreach (var item in report.CalculatedFields)
                {
                    //设置数据源后计算字段即可正常显示
                    item.DataSource = report.DataSource;
                }
                report.CreateDocument(BuildPagesInBackground);

                if (IsShowWaitFrom)
                {
                    AsynWaitForm.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (IsShowWaitFrom)
                {
                    AsynWaitForm.CloseWaitForm();
                }
                MediMsgBox.Show(ex.Message);
            }
        }

        private void PrintControl1_DoubleClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                MediMsgBox.Show("当前报表单据ID为【" + this.DanJuID + "】");
            }
        }



        /// <summary>
        /// 载入单据内容（针对单据的即界面传入数据源或参数，需在PRT做对应单据处理的）
        /// </summary>
        /// <param name="danJuDXMC">单据对象名称</param>
        /// <param name="dataSource">数据源对象</param>
        /// <param name="dicParm">需替换系统参数内容</param>
        /// <param name="piaoJuYWID">票据业务id，对应票据重打使用</param> 
        /// <param name="clear">是否清除控件中的报表对象，多张报表展示使用</param>
        public void LoadReport(string danJuDXMC, object dataSource, Dictionary<string, string> dicParm = null, string piaoJuYWID = null, bool clear = true)
        {
            Console.WriteLine("报表-1" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            if (IsShowWaitFrom)
            {
                AsynWaitForm.CreateWaitForm(this.ParentForm as MediForm, "", "正在加载报表，请稍候......");
                AsynWaitForm.SetPercentVisible(false);
            }
            try
            {
                Console.WriteLine("报表0" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                PrintControl printControl1 = new PrintControl();
                Console.WriteLine("报表1" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                report = ReportPrint.LoadReport(danJuDXMC, dataSource, dicParm, piaoJuYWID);
                Console.WriteLine("报表2" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                report.PrintingSystem.ProgressReflector.CanAutocreateRange = false;
                report.ReportUnit = ReportUnit.HundredthsOfAnInch;
                printControl1.Dock = DockStyle.Fill;
                printControl1.PrintingSystem = report.PrintingSystem;
                printControl1.DoubleClick += PrintControl1_DoubleClick;


                ((PrintingSystemBase)printControl1.Document).Watermark.ImageAlign = ContentAlignment.TopLeft;
                //展示
                if (clear)
                {
                    printControl1.BorderStyle = BorderStyle.None;
                    printControl1.BackColor = Color.White;//多报表预览使用白底
                    mediPanelControl_Main.Controls.Clear();
                }
                if (ShowPrintBar)
                {
                    PrintBarManager printBarManager = new PrintBarManager();
                    printBarManager.Form = printControl1;
                    printBarManager.Initialize(printControl1);
                    printBarManager.MainMenu.Visible = false;
                    printBarManager.AllowCustomization = true;
                    printBarManager.StatusBar.Visible = false;
                    //操作要显示什么按钮
                    printControl1.PrintingSystem.SetCommandVisibility(new[]{
                        PrintingSystemCommand.Open,
                        PrintingSystemCommand.Save,
                        PrintingSystemCommand.ClosePreview,
                        PrintingSystemCommand.Customize,
                        PrintingSystemCommand.SendCsv,
                        PrintingSystemCommand.SendFile,
                        PrintingSystemCommand.SendGraphic,
                        PrintingSystemCommand.SendMht,
                        PrintingSystemCommand.SendPdf,
                        PrintingSystemCommand.SendRtf,
                        PrintingSystemCommand.SendTxt,
                        PrintingSystemCommand.SendXls
                    }, CommandVisibility.None);
                }
                Console.WriteLine("报表3" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                mediPanelControl_Main.Controls.Add(printControl1);
                report.Watermark.ImageAlign = ContentAlignment.TopLeft;
                Console.WriteLine("报表3.5" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));

                report.CreateDocument(BuildPagesInBackground);

                Console.WriteLine("报表4" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                if (IsShowWaitFrom)
                {
                    AsynWaitForm.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (IsShowWaitFrom)
                {
                    AsynWaitForm.CloseWaitForm();
                }
                MediMsgBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// 获取报表中某个控件
        /// </summary>
        /// <param name="controlName">要取的对应控件名</param>
        /// <returns></returns>
        public XRControl GetControl(string controlName)
        {
            XRControl control = report.FindControl(controlName, true);
            return control;
        }

        /// <summary>
        /// 获取报表中某个控件的值
        /// </summary>
        /// <param name="controlName">要取值的对应控件名</param>
        /// <returns></returns>
        public string GetControlValue(string controlName)
        {
            string controlValue = "";
            if (report.GetSumValue(controlName, ref controlValue))
            {
                return controlValue;
            }
            XRControl control = report.FindControl(controlName, true);
            if (control != null)
            {
                controlValue = control.Text;
            }

            return controlValue;
        }

        /// <summary>
        /// 获取报表的数据源
        /// </summary>
        /// <returns></returns>
        public object GetReportSource()
        {
            return report.DataSource;
        }
        /// <summary>
        /// 获取报表对象
        /// </summary>
        /// <returns></returns>
        public MediXtraReport GetReport()
        {
            return report;
        }
        /// <summary>
        /// 设置某个控件的显示文本值
        /// </summary>
        /// <param name="controlName">要设置的对应控件名</param>
        /// <param name="controlText">要显示的控件文本</param>
        /// <returns></returns>
        public bool SetControlText(string controlName, string controlText)
        {
            XRControl control = report.FindControl(controlName, true);
            if (control == null)
            {
                return false;
            }
            control.Text = controlText;
            return true;
        }


        /// <summary>
        /// 打印报表
        /// </summary>
        public void Print(bool isShowMarginsWarning = true)
        {
            report.PrintingSystem.ShowMarginsWarning = isShowMarginsWarning;
            report.Print();
        }

        private int printFromPage = -1;
        private int printToPage = -1;
        /// <summary>
        /// 打印指定页内容
        /// </summary>
        /// <param name="fromPage">开始页</param>
        /// <param name="toPage">结束页，结束页不传默认只打印开始页一页</param>
        public void Print(int fromPage, int toPage = -1)
        {
            report.SetPrintPage(fromPage, toPage);
            report.Print();
        }

        /// <summary>
        /// 调用打印设置页
        /// </summary>
        public void PrintDialog()
        {
            report.PrintDialog();
        }

        /// <summary>
        /// 打印取界面上使用checkbox后的选中行
        /// </summary>
        /// <param name="controlName">CheckBox对应列名</param>
        /// <returns>0、打印成功，-1 、未打印或失败</returns>
        public int PrintCheckedRow(string controlName)
        {
            List<int> checkRowIndex = new List<int>();
            var daYinZT = PrintCheckedRow(controlName, out checkRowIndex);
            return daYinZT;
        }

        /// <summary>
        /// 获取报表选择行索引列表
        /// </summary>
        /// <param name="controlName">选择控件名</param>
        /// <returns></returns>
        public List<int> GetCheckRow(string controlName)
        {
            List<int> xuanZhong = new List<int>();
            for (int i = 0; i < report.PrintingSystem.EditingFields.Count; i++)
            {
                var item = report.PrintingSystem.EditingFields[i];
                if (item.EditValue is CheckState)
                {
                    if ((CheckState)item.EditValue == CheckState.Checked)
                    {
                        xuanZhong.Add(i);
                    }
                }
            }
            return xuanZhong;
        }

        /// <summary>
        /// 打印取界面上使用checkbox后的选中行
        /// </summary>
        /// <param name="controlName">CheckBox对应列名</param>
        /// <param name="checkRowIndex">选中行索引</param>
        /// <returns>0、打印成功，-1 、未打印或失败</returns>
        public int PrintCheckedRow(string controlName, out List<int> checkRowIndex)
        {
            PrintDataSoure = null;
            List<int> UnXuanZhong = new List<int>();
            checkRowIndex = new List<int>();
            for (int i = 0; i < report.PrintingSystem.EditingFields.Count; i++)
            {
                var item = report.PrintingSystem.EditingFields[i];
                if (item.EditValue is CheckState)
                {
                    if ((CheckState)item.EditValue == CheckState.Unchecked)
                    {
                        UnXuanZhong.Add(i);
                    }
                    else if ((CheckState)item.EditValue == CheckState.Checked)
                    {
                        checkRowIndex.Add(i);
                    }
                }
            }
            int daYinZT = -1;
            var dataSource = report.DataSource;
            if (dataSource.GetType().Name == "List`1")
            {
                if (dataSource is IEnumerable<object> list)
                {
                    var newlist = list.ToList();
                    if (UnXuanZhong.Count > 0)
                    {
                        for (int i = UnXuanZhong.Count - 1; i >= 0; i--)
                        {
                            newlist.RemoveAt(UnXuanZhong[i]);
                        }
                    }
                    MemoryStream stream = new MemoryStream();
                    MediXtraReport xtraReport = new MediXtraReport();
                    report.SaveLayout(stream);
                    xtraReport.LoadLayout(stream);
                    xtraReport.DataSource = newlist;
                    PrintDataSoure = newlist;
                    if (xtraReport.FindControl(controlName, true) != null)
                    {
                        xtraReport.FindControl(controlName, true).Visible = false;
                    }
                    xtraReport.CreateDocument();
                    daYinZT = xtraReport.MediPrint();
                    xtraReport.DataSource = list;
                }
            }
            else if (dataSource.GetType().Name == "DataTable")
            {
                DataTable dt = dataSource as DataTable;
                DataTable tempdt = dt.Copy();
                if (UnXuanZhong.Count > 0)
                {
                    for (int i = UnXuanZhong.Count - 1; i >= 0; i--)
                    {
                        if (tempdt.Rows.Count >= UnXuanZhong[i])
                        {
                            tempdt.Rows.RemoveAt(UnXuanZhong[i]);
                        }
                    }
                }
                MemoryStream stream = new MemoryStream();
                MediXtraReport xtraReport = new MediXtraReport();
                report.SaveLayout(stream);
                xtraReport.LoadLayout(stream);
                xtraReport.DataSource = tempdt;
                PrintDataSoure = tempdt;
                if (xtraReport.FindControl(controlName, true) != null)
                {
                    xtraReport.FindControl(controlName, true).Visible = false;
                }
                xtraReport.CreateDocument();
                daYinZT = xtraReport.MediPrint();
                xtraReport.DataSource = dt;
            }
            return daYinZT;
        }

        /// <summary>
        /// 打印取界面上使用checkbox后的选中行  指定打印机名 设置是否打印预览
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="printname">打印机名称</param>
        /// <param name="isPrivew"></param>
        /// <param name="checkRowIndex">选中的下标</param>
        /// <returns></returns>
        public int PrintCheckedRow(string controlName, string printname, out List<int> checkRowIndex, EnumShiFou isPreiew = EnumShiFou.是)
        {
            PrintDataSoure = null;
            List<int> UnXuanZhong = new List<int>();
            checkRowIndex = new List<int>();
            for (int i = 0; i < report.PrintingSystem.EditingFields.Count; i++)
            {
                var item = report.PrintingSystem.EditingFields[i];
                if (item.EditValue is CheckState)
                {
                    if ((CheckState)item.EditValue == CheckState.Unchecked)
                    {
                        UnXuanZhong.Add(i);
                    }
                    else if ((CheckState)item.EditValue == CheckState.Checked)
                    {
                        checkRowIndex.Add(i);
                    }
                }
            }
            int daYinZT = -1;
            var dataSource = report.DataSource;
            if (dataSource.GetType().Name == "List`1")
            {
                if (dataSource is IEnumerable<object> list)
                {
                    var newlist = list.ToList();
                    if (UnXuanZhong.Count > 0)
                    {
                        for (int i = UnXuanZhong.Count - 1; i >= 0; i--)
                        {
                            newlist.RemoveAt(UnXuanZhong[i]);
                        }
                    }
                    MemoryStream stream = new MemoryStream();
                    MediXtraReport xtraReport = new MediXtraReport();
                    report.SaveLayout(stream);
                    xtraReport.LoadLayout(stream);
                    xtraReport.IsShowPreview = isPreiew;
                    xtraReport.DataSource = newlist;
                    PrintDataSoure = newlist;
                    if (xtraReport.FindControl(controlName, true) != null)
                    {
                        xtraReport.FindControl(controlName, true).Visible = false;
                    }
                    xtraReport.CreateDocument();
                    daYinZT = xtraReport.MediPrint(printname);
                    xtraReport.DataSource = list;
                }
            }
            else if (dataSource.GetType().Name == "DataTable")
            {
                DataTable dt = dataSource as DataTable;
                DataTable tempdt = dt.Copy();
                if (UnXuanZhong.Count > 0)
                {
                    for (int i = UnXuanZhong.Count - 1; i >= 0; i--)
                    {
                        if (tempdt.Rows.Count >= UnXuanZhong[i])
                        {
                            tempdt.Rows.RemoveAt(UnXuanZhong[i]);
                        }
                    }
                }
                MemoryStream stream = new MemoryStream();
                MediXtraReport xtraReport = new MediXtraReport();
                report.SaveLayout(stream);
                xtraReport.LoadLayout(stream);
                xtraReport.DataSource = tempdt;
                xtraReport.IsShowPreview = isPreiew;
                PrintDataSoure = tempdt;
                if (xtraReport.FindControl(controlName, true) != null)
                {
                    xtraReport.FindControl(controlName, true).Visible = false;
                }
                xtraReport.CreateDocument();
                daYinZT = xtraReport.MediPrint(printname);
                xtraReport.DataSource = dt;
            }
            return daYinZT;
        }

        /// <summary>
        /// 打印取界面上使用checkbox后的选中行  指定打印机名 设置是否打印预览
        /// </summary>
        /// <param name="controlName">checkbox控件名</param>
        /// <param name="printname">打印机名</param>
        /// <param name="columnNames">dataset主键列明</param>
        /// <param name="checkRowIndex">返回选中的行索引</param>
        /// <param name="isPreiew">是否预览</param>
        /// <returns></returns>
        public int PrintCheckedRow(string controlName, string printname, List<string> columnNames, out List<int> checkRowIndex, EnumShiFou isPreiew = EnumShiFou.是)
        {
            PrintDataSoure = null;
            List<int> UnXuanZhong = new List<int>();
            checkRowIndex = new List<int>();
            for (int i = 0; i < report.PrintingSystem.EditingFields.Count; i++)
            {
                var item = report.PrintingSystem.EditingFields[i];
                if (item.EditValue is CheckState)
                {
                    if ((CheckState)item.EditValue == CheckState.Unchecked)
                    {
                        UnXuanZhong.Add(i);
                    }
                    else if ((CheckState)item.EditValue == CheckState.Checked)
                    {
                        checkRowIndex.Add(i);
                    }
                }
            }
            int daYinZT = -1;
            var dataSource = report.DataSource;
            if (dataSource.GetType().Name == "DataSet")
            {
                DataSet ds= dataSource as DataSet;
                if(ds==null||ds.Tables.Count<2)
                {
                    return daYinZT;
                }
                DataTable zhuBiaodt = ds.Tables[0];
                DataTable mingXidt = ds.Tables[1];
                DataTable tempdt = zhuBiaodt.Copy();
                DataTable tempMXdt = mingXidt.Copy();
                if (UnXuanZhong != null && UnXuanZhong.Count > 0)
                {
                    for (int i = UnXuanZhong.Count - 1; i >= 0; i--)
                    {
                        if (tempdt.Rows.Count >= UnXuanZhong[i])
                        {
                            DataRow nn = tempdt.Rows[UnXuanZhong[i]];
                            string filter = string.Empty;
                            for(int j=0;j< columnNames.Count;j++)
                            {
                                if(string.IsNullOrEmpty(filter))
                                {
                                    filter = string.Format(" {0}='{1}'", columnNames[j], nn[columnNames[j]].ToStringEx());
                                }
                                else
                                {
                                    filter += string.Format(" AND {0}='{1}'", columnNames[j], nn[columnNames[j]].ToStringEx());
                                }
                            }
                            DataRow[] shaiXuanRows = tempMXdt.Select(filter);
                            if (shaiXuanRows != null && shaiXuanRows.Length > 0)
                            {
                                foreach (var item in shaiXuanRows)
                                {
                                    tempMXdt.Rows.Remove(item);
                                }
                            }
                            tempdt.Rows.RemoveAt(UnXuanZhong[i]);
                        }
                    }
                }
                DataSet tempSet = new DataSet();
                tempSet.Tables.Add(tempdt);
                tempSet.Tables.Add(tempMXdt);

                //给数据集建立主外键关系
                DataColumn[] parentdataColumns = new DataColumn[columnNames.Count];
                DataColumn[] childdataColumns = new DataColumn[columnNames.Count];
                for(int i=0;i< columnNames.Count;i++)
                {
                    DataColumn parentColumn = tempSet.Tables["ZB"].Columns[columnNames[i]];
                    DataColumn childColumn = tempSet.Tables["MXB"].Columns[columnNames[i]];
                    parentdataColumns[i] = parentColumn;
                    childdataColumns[i] = childColumn;
                }
                DataRelation R1 = new DataRelation("R1", parentdataColumns, childdataColumns);
                tempSet.Relations.Add(R1);
                
                MemoryStream stream = new MemoryStream();
                MediXtraReport xtraReport = new MediXtraReport();
                report.SaveLayout(stream);
                xtraReport.LoadLayout(stream);
                xtraReport.DataSource = tempSet;
                //明细表
                var detailReport = xtraReport.FindControl("DetailReport", false) as DetailReportBand;
                if(detailReport!=null)
                {
                    detailReport.DataSource = tempSet;
                    detailReport.DataMember = "R1";
                }
                xtraReport.IsShowPreview = isPreiew;
                PrintDataSoure = tempSet;
                if (xtraReport.FindControl(controlName, true) != null)
                {
                    xtraReport.FindControl(controlName, true).Visible = false;
                }
                xtraReport.CreateDocument();
                daYinZT = xtraReport.MediPrint(printname);
                xtraReport.DataSource = ds;
            }
            return daYinZT;
        }

        /// <summary>
        /// 设置界面上所有的checkbox的选择状态 
        /// </summary>
        /// <param name="checkState"></param>
        /// <returns></returns>
        public void SetChecked(CheckState checkState)
        {
            for (int i = 0; i < report.PrintingSystem.EditingFields.Count; i++)
            {
                var item = report.PrintingSystem.EditingFields[i];
                if (item.EditValue is CheckState)
                {
                    item.EditValue = checkState;
                }
            }
            RefForeColor();
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="path">导出文件路径，不传或传空默认弹出选择路径</param>
        public void ExportToXls(string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog fileDialog = new SaveFileDialog())
                {
                    fileDialog.FileName = report.Name + ".xls";
                    fileDialog.Title = @"请选择文件";
                    fileDialog.Filter = @"所有文件(*xls*)|*.xls*"; //设置要选择的文件的类型
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        path = fileDialog.FileName;//返回文件的完整路径                
                    }
                    else
                    {
                        return;
                    }
                }
            }
            try
            {
                report.ExportToXls(path);
                MediMsgBox.Success("导出成功！导出文件路径为【" + path + "】");
            }
            catch (Exception e)
            {
                MediMsgBox.Show(e.Message);
            }

        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="title">提示信息标题</param>
        /// <param name="content">提示信息内容</param>
        /// <param name="showTime">多少时间后关闭 以毫秒计</param>
        /// <param name="superTipMaxWidth">提示框最大宽度</param>
        public void ShowTips(string title, string content, int showTime = 3000, int superTipMaxWidth = 400)
        {
            ToolTipControllerShowEventArgs args = CreateShowArgs(content);
            // 设置ToolTip标题
            args.Title = title;
            toolTipController.AutoPopDelay = showTime;
            toolTipController.Active = true;
            toolTipController.ShowBeak = true;
            toolTipController.SuperTipMaxWidth = superTipMaxWidth;
            toolTipController.ToolTipLocation = ToolTipLocation.BottomRight;
            toolTipController.ToolTipStyle = ToolTipStyle.Windows7;
            toolTipController.ShowHint(args, MousePosition);
        }

        /// <summary>
        /// 创建显示ToolTip事件实例
        /// </summary>
        /// <param name="tooltipText"></param>
        /// <returns></returns>
        private ToolTipControllerShowEventArgs CreateShowArgs(string tooltipText)
        {
            ToolTipControllerShowEventArgs args = toolTipController.CreateShowArgs();
            args.ToolTip = tooltipText;
            return args;
        }
        #endregion


        #region 内部处理
        /// <summary>
        ///根据单据ID绑定单据
        /// </summary>
        private void BindData()
        {
            string message = "";
            reportStream = ReportPrint.GetXtraReportStream(DanJuID, ref message);
            if (reportStream == null && message != "")
            {
                MediMsgBox.Failure(message);
                return;
            }
            report.LoadLayout(reportStream);
            //加载配置
            GetReportPeiZhi(report.Tag.ToString());
        }


        /// <summary>
        /// 获取报表配置
        /// </summary>
        /// <param name="peiZhiContent"></param>
        private void GetReportPeiZhi(string peiZhiContent)
        {
            string[] peizhi = peiZhiContent.Split('~');
            if (peizhi[0].Length > 10)
            {
                listCXTJ = JsonUtil.DeserializeToObject<List<E_GY_DANJUCXTJ>>(Base64Util.Base64Decode(peizhi[0]));
            }
            if (peizhi[1].Length > 10)
            {
                listSJY = JsonUtil.DeserializeToObject<List<E_GY_DANJUSJY>>(Base64Util.Base64Decode(peizhi[1]));
                isBaoBiao = true;
            }
        }


        /// <summary>
        /// 获取报表数据源
        /// </summary>
        private void GetDataSoucre()
        {
            if (isBaoBiao)
            {
                var querySql = "";
                try
                {
                    DataSet ds = new DataSet();
                    foreach (var itemSjy in listSJY)
                    {
                        DataTable dt = new DataTable();
                        string sql = itemSjy.SQL.Substring(itemSjy.SQL.ToUpper().IndexOf("SELECT", StringComparison.Ordinal));

                        #region 处理替换条件语句
                        //替换条件的格式为[@01]=and yingyongid='[#YINGYONGID]';
                        var tiaoJian = itemSjy.SQL.Substring(0, itemSjy.SQL.ToUpper().IndexOf("SELECT", StringComparison.Ordinal)).Split(Environment.NewLine.ToCharArray()).Where(o => o.StartsWith("[@")).ToList();
                        if (tiaoJian.Count > 0)
                        {
                            foreach (var str in tiaoJian)
                            {
                                string key = str.Substring(0, 5);
                                string value = str.Substring(6).TrimEnd(';');
                                //参数内容中对应的条件参数为空，默认为是全选的，sql语句中对应内容置为空
                                var quanXuan = QueryParam.Where(o => (o.Value == null || string.IsNullOrWhiteSpace(o.Value.ToString()))).ToList();
                                bool isQuanXuan = false;
                                if (quanXuan.Count > 0)
                                {
                                    if (quanXuan.Count(o => value.IndexOf("[#" + o.Key + "]", StringComparison.Ordinal) != -1) > 0)
                                    {
                                        isQuanXuan = true;
                                    }
                                }
                                sql = sql.Replace(key, !isQuanXuan ? value : "");
                            }

                        }
                        #endregion

                        //替换对应条件参数
                        sql = QueryParam.Aggregate(sql,
                            (current1, item) => current1.Replace("[#" + item.Key + "]",
                                (item.Value == null ? "" : item.Value.ToString())));
                        //获取并替换系统参数
                        GetXiTongCS();
                        sql = systemParamDic.Aggregate(sql,
                            (current1, item) => current1.Replace("[#" + item.Key + "]",
                                item.Value == null ? "" : item.Value.ToString()));
                        Console.WriteLine(sql);
                        querySql = sql;
                        if (report.DBTYPE == EnumDBType.HIS)
                        {
                            dt = query.GetDataTableBySql(sql.Replace(";", "")).Return.TableContent;
                        }
                        dt.TableName = itemSjy.SHUJUYMC;
                        ds.Tables.Add(dt);
                    }
                    DataSource = ds;
                }
                catch (Exception ex)
                {
                    throw new Exception("查询数据源异常，查询Sql【" + querySql + "】" + ex.Message);
                }
            }

            //界面查询参数替换处理
            foreach (var item in QueryParam)
            {
                if (report.systemParamDic.ContainsKey(item.Key))
                {
                    report.systemParamDic[item.Key] = item.Value.ToStringEx();
                }
                else if (item.Value != null)
                {
                    report.systemParamDic.Add(item.Key, item.Value.ToStringEx());
                }
            }
            report.DataSource = DataSource;
        }


        /// <summary>
        /// 获取系统参数
        /// </summary>
        private void GetXiTongCS()
        {
            if (systemParamDic.Count < 2)
            {
                var param = typeof(HISClientHelper).GetProperties().ToList();
                if (param.Count > 0)
                {
                    foreach (var item in param)
                    {
                        var v = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (v.Length > 0 && !string.IsNullOrEmpty(v[0].Description))
                        {
                            var value = item.GetValue(null, null);
                            systemParamDic.Add(item.Name, value != null ? value.ToString() : "");
                        }
                    }
                }
            }
        }


        #endregion

    }
}
