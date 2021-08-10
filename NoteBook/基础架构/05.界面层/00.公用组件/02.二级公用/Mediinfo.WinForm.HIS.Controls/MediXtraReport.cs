using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Serialization;
using DevExpress.XtraReports.UI;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediXtraReport : XtraReport
    {
        #region 属性、字段定义

        /// <summary>
        /// 自定义指定打印机
        /// </summary>
        public string setPrinterName { set; get; } = null;
        /// <summary>
        /// 系统参数列表
        /// </summary>
        public Dictionary<string, string> systemParamDic = new Dictionary<string, string>();
        private EnumShiFou _IsShowPreview = EnumShiFou.否;
        /// <summary>
        /// 是否预览
        /// </summary>
        [DisplayName("是否预览"), DescriptionAttribute("是否预览"), CategoryAttribute("自定义属性"), DefaultValueAttribute(EnumShiFou.是), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public EnumShiFou IsShowPreview { get { return _IsShowPreview; } set { _IsShowPreview = value; } }

        /// <summary>
        /// 点击列名称
        /// </summary>
        [DisplayName("自定义事件列名称"), DescriptionAttribute("自定义事件列名称，为空时整行控件都会触发事件，输入为控件名，多个以|分隔"), CategoryAttribute("自定义属性"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public string ClickCellName { get { return _ClickCellName; } set { _ClickCellName = value; } }
        private string _ClickCellName = "";

        /// <summary>
        /// 是否点击
        /// </summary>
        [DisplayName("注册自定义事件"), DescriptionAttribute("注册自定义事件，选择为是后支持 鼠标移动到控件显示提示及双击事件"), CategoryAttribute("自定义属性"), DefaultValueAttribute(EnumShiFou.否), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public EnumShiFou IsDoubleClickCell { get { return _IsDoubleClickCell; } set { _IsDoubleClickCell = value; } }
        private EnumShiFou _IsDoubleClickCell = EnumShiFou.否;

        /// <summary>
        /// 是否动态生成列
        /// </summary>
        [DisplayName("是否动态生成列"), DescriptionAttribute("是否动态生成列"), CategoryAttribute("自定义属性"), DefaultValueAttribute(EnumShiFou.否), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public EnumShiFou IsDynamics { get; set; } = EnumShiFou.否;

        /// <summary>
        /// 指定动态列宽度
        /// </summary>
        [DisplayName("指定动态列宽度"), DescriptionAttribute("指定动态列宽度"), CategoryAttribute("自定义属性"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public string CellWeight { get; set; } = "";

        private int _CHONGDAXZCS = 0;
        /// <summary>
        /// 重打限制次数
        /// </summary>
        [DisplayName("重打限制次数"), DescriptionAttribute("重打限制次数"), CategoryAttribute("自定义属性"), DefaultValueAttribute(0), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public int CHONGDAXZCS { get { return _CHONGDAXZCS; } set { _CHONGDAXZCS = value; } }


        private int _DAYINCDBZCS = 0;
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        private Dictionary<string, string> sumValueDic = new Dictionary<string, string>();
        private bool isPrint = false;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        private int currentIndex = 1;

        /// <summary>
        /// 打印重打标志次数 重打几次算重新打印
        /// </summary>
        [DisplayName("打印重打标志次数"), DescriptionAttribute("打印重打标志次数"), CategoryAttribute("自定义属性"), DefaultValueAttribute(0), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public int DAYINCDBZCS { get { return _DAYINCDBZCS; } set { _DAYINCDBZCS = value; } }
        /// <summary>
        /// 当天重打标志 是否开启重打标志
        /// </summary>
        [DisplayName("当天重打标志"), DescriptionAttribute("当天重打标志"), CategoryAttribute("自定义属性"), DefaultValueAttribute(EnumShiFou.是), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public EnumShiFou DANGTIANCDBZ { get; set; } = EnumShiFou.是;

        //private EnumShiFou _DINGCHANGDY = EnumShiFou.否;
        /// <summary>
        /// 连接数据库类型
        /// </summary>
        [DisplayName("数据库类型"), DescriptionAttribute("连接数据库类型"), CategoryAttribute("自定义属性"), DefaultValueAttribute(EnumDBType.HIS), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public EnumDBType DBTYPE { get { return _DBTYPE; } set { _DBTYPE = value; } }
        private EnumDBType _DBTYPE = EnumDBType.HIS;
        /// <summary>
        /// 打印指定从哪页开始
        /// </summary>
        private int FromPage = -1;

        /// <summary>
        /// 打印指定到哪一页为止
        /// </summary>
        private int ToPage = -1;

        /// <summary>
        /// 单据ID
        /// </summary>
        [DisplayName("单据ID"), DescriptionAttribute("单据ID"), CategoryAttribute("自定义属性"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(false)]
        public string DanJuID { get; set; } = "";

        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间"), DescriptionAttribute("修改时间"), CategoryAttribute("自定义属性"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(false)]
        public string XiuGaiSJ { get; set; }

        /// <summary>
        /// 对应应用ID
        /// </summary>
        [DisplayName("对应应用ID"), DescriptionAttribute("对应应用ID"), CategoryAttribute("自定义属性"), DefaultValueAttribute(""), ReadOnlyAttribute(true), BrowsableAttribute(false)]
        public string YingYongID { get; set; }

        /// <summary>
        /// 单据分类
        /// </summary>
        [DisplayName("单据分类"), DescriptionAttribute("单据分类"), CategoryAttribute("自定义属性"), DefaultValueAttribute(""), ReadOnlyAttribute(true), BrowsableAttribute(false)]
        public string DanJuFL { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        private string dispalyName;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MediXtraReport() : base()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            this.BeforePrint += MediXtraReport_BeforePrint;
        }

        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        /// <summary>
        /// 实例化时指定报表的名字
        /// </summary>
        /// <param name="sReportName"></param>
        public MediXtraReport(string sReportName) : base()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            this.Name = sReportName;
            this.BeforePrint += MediXtraReport_BeforePrint;
        }

        /// <summary>
        /// 实例化时根据路径打开报表，并绑定数据源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="datasource"></param>
        public MediXtraReport(string path, object datasource)
            : base()
        {
            if (developerHelper == null)
            {
                developerHelper = new SystemInfoHelper();
            }
            developerHelper.DealRelativeControl(this);
            path = AppDomain.CurrentDomain.BaseDirectory + "XtraReport\\" + @path;
            this.LoadLayout(path);
            this.DataSource = datasource;
        }
        /// <summary>
        /// 实例化时根据路径打开报表;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCreate">区分方法，无实质用处</param>
        public MediXtraReport(string path, bool isCreate) : base()
        {
            if (developerHelper == null)
            {
                developerHelper = new SystemInfoHelper();
            }
            developerHelper.DealRelativeControl(this);
            this.LoadLayout(path);
        }
        /// <summary>
        /// 实例化时绑定传入单据信息与数据源
        /// </summary>
        /// <param name="DanJuXX"></param>
        /// <param name="datasource"></param>
        public MediXtraReport(Stream DanJuXX, object datasource)
            : base()
        {
            if (developerHelper == null)
            {
                developerHelper = new SystemInfoHelper();
            }
            developerHelper.DealRelativeControl(this);
            this.DataSource = datasource;
        }

        #region 重写原方法

        /// <summary>
        /// 序列化属性
        /// </summary>
        /// <param name="serializer"></param>
        protected override void SerializeProperties(XRSerializer serializer)
        {
            base.SerializeProperties(serializer);
            serializer.SerializeEnum("IsShowPreview", _IsShowPreview);
            serializer.SerializeInteger("CHONGDAXZCS", _CHONGDAXZCS);
            serializer.SerializeInteger("DAYINCDBZCS", _DAYINCDBZCS);
            serializer.SerializeEnum("DANGTIANCDBZ", DANGTIANCDBZ);
            //serializer.SerializeEnum("DINGCHANGDY", _DINGCHANGDY);
            serializer.SerializeEnum("IsDoubleClickCell", _IsDoubleClickCell);
            serializer.SerializeString("ClickCellName", _ClickCellName);
            serializer.SerializeEnum("DBTYPE", _DBTYPE);
            serializer.SerializeString("DanJuID", DanJuID);
            serializer.SerializeString("XiuGaiSJ", XiuGaiSJ);
            serializer.SerializeString("YingYongID", YingYongID);
            serializer.SerializeString("DanJuFL", DanJuFL);
            serializer.SerializeEnum("IsDYAMIC", IsDynamics);
            serializer.SerializeString("CellWeight", CellWeight);
        }

        /// <summary>
        /// 反序列化属性
        /// </summary>
        /// <param name="serializer"></param>
        protected override void DeserializeProperties(XRSerializer serializer)
        {
            base.DeserializeProperties(serializer);
            IsShowPreview = (EnumShiFou)serializer.DeserializeEnum("IsShowPreview", typeof(EnumShiFou), _IsShowPreview);
            CHONGDAXZCS = serializer.DeserializeInteger("CHONGDAXZCS", _CHONGDAXZCS);
            DAYINCDBZCS = serializer.DeserializeInteger("DAYINCDBZCS", _DAYINCDBZCS);
            DANGTIANCDBZ = (EnumShiFou)serializer.DeserializeEnum("DANGTIANCDBZ", typeof(EnumShiFou), DANGTIANCDBZ);
            //DINGCHANGDY= (EnumShiFou)serializer.DeserializeEnum("DINGCHANGDY", typeof(EnumShiFou), _DINGCHANGDY);
            IsDoubleClickCell = (EnumShiFou)serializer.DeserializeEnum("IsDoubleClickCell", typeof(EnumShiFou), _IsDoubleClickCell);
            ClickCellName = serializer.DeserializeString("ClickCellName", _ClickCellName);
            DBTYPE = (EnumDBType)serializer.DeserializeEnum("DBTYPE", typeof(EnumDBType), _DBTYPE);
            DanJuID = serializer.DeserializeString("DanJuID", DanJuID);
            XiuGaiSJ = serializer.DeserializeString("XiuGaiSJ", XiuGaiSJ);
            YingYongID = serializer.DeserializeString("YingYongID", YingYongID);
            DanJuFL = serializer.DeserializeString("DanJuFL", DanJuFL);
            IsDynamics = (EnumShiFou)serializer.DeserializeEnum("IsDYAMIC", typeof(EnumShiFou), IsDynamics);
            CellWeight = serializer.DeserializeString("CellWeight", CellWeight);
        }

        #endregion

        #region 提供对外调用方法

        /// <summary>
        /// 调用打印
        /// </summary>
        public virtual int MediPrint()
        {
            return MediPrint("");
        }

        public virtual int MediPrint(string printerName)
        {
            try
            {
                this.CreateDocument();
                //不提示名称
                this.PrintingSystem.ShowMarginsWarning = false;
                // 1.判断本地是否存在该打印机(不区分名称大小写)
                List<string> list = LocalPrinter.GetLocalPrinters();
                bool exists = false;
                foreach (string p in list)
                {
                    //当有共享打印机时候，名字会带上IP，如果不指定IP也应打印
                    if (!string.IsNullOrEmpty(p) && p.Contains(printerName))
                    {
                        printerName = p;
                        exists = true;
                        break;
                    }
                    //if (String.Compare(p, printerName, true) == 0)
                    //{
                    //    exists = true;
                    //    break;
                    //}
                }

                // 2.如果为找到本地打印机或者打印机为空则取本地默认打印机
                if (String.IsNullOrEmpty(printerName) || !exists)
                {
                    printerName = LocalPrinter.DefaultPrinter;
                }

                setPrinterName = printerName;
                if (IsShowPreview == EnumShiFou.是)
                {
                    isPrint = false;
                    ReportPrintTool reportPrintTool = new ReportPrintTool(this);
                    reportPrintTool.PreviewForm.MinimizeBox = false;
                    reportPrintTool.PreviewForm.PrintingSystem.EndPrint += PrintingSystem_EndPrint;
                    reportPrintTool.ShowPreviewDialog();
                    return isPrint ? 0 : -1;
                }
                else
                {
                    // 采用异步打印
                    new TaskFactory().StartNew(() =>
                    {
                        /*
                        // 1.判断本地是否存在该打印机(不区分名称大小写)
                        List<string> list = LocalPrinter.GetLocalPrinters();
                        bool exists = false;
                        foreach (string p in list)
                        {
                            if (String.Compare(p, printerName, true) == 0)
                            {
                                exists = true;
                                break;
                            }
                        }

                        // 2.如果为找到本地打印机或者打印机为空则取本地默认打印机
                        if (String.IsNullOrEmpty(printerName) || !exists)
                        {
                            printerName = LocalPrinter.DefaultPrinter;
                        }*/

                        this.Print(printerName);
                    }).LogExceptions();     // 打印时添加异常处理

                    return 0;
                }
            }
            catch (Exception ex)
            {
                this.Print(LocalPrinter.DefaultPrinter);
                return 0;
            }
        }

        public virtual int MediPrintHtml(string printerPath)
        {
            try
            {
                this.ExportToHtml(printerPath);
            }
            catch (Exception ex)
            {
                //this.Print(LocalPrinter.DefaultPrinter);
                return 0;
            }
            return 0;
        }

        public virtual int MediPrintHtml(ref string strReport)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                //Stream stream = null;
                this.ExportToHtml(stream);
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8))
                {
                    strReport = sr.ReadToEnd();
                }
                //this.CreateHtmlDocument("report.html");
            }
            catch (Exception ex)
            {
                //this.Print(LocalPrinter.DefaultPrinter);
                return 0;
            }
            return 0;
        }
        private void PrintingSystem_EndPrint(object sender, EventArgs e)
        {
            isPrint = true;
        }

        /// <summary>
        /// 获取统计列的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetSumValue(string name, ref string value)
        {
            if (sumValueDic.ContainsKey(name))
            {
                value = sumValueDic[name];
                return true;
            }
            return false;
        }

        /// <summary>
        /// 设置报表打印页面
        /// </summary>
        /// <param name="fromPage"></param>
        /// <param name="toPage"></param>
        public void SetPrintPage(int fromPage, int toPage = -1)
        {
            this.FromPage = fromPage;
            this.ToPage = toPage;
        }

        #endregion

        #region 内部处理事件、 方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 100F;
            this.topMarginBand1.HeightF = 100F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.Dpi = 100F;
            this.detailBand1.HeightF = 100F;
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 100F;
            this.bottomMarginBand1.HeightF = 100F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // MediXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            this.Version = "16.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        /// <summary>
        /// 报表的打印前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediXtraReport_BeforePrint(object sender, PrintEventArgs e)
        {
            AddSystemParam();
            RepalceParms();
            AddPreviewEvent();
            this.PrintingSystem.ShowMarginsWarning = false;
            this.PrintingSystem.StartPrint += PrintingSystem_StartPrint;
            this.PrintProgress += MediXtraReport_PrintProgress;
            this.ShowPrintStatusDialog = false;
        }

        private void MediXtraReport_PrintProgress(object sender, PrintProgressEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #region 注册点击行事件

        /// <summary>
        /// 点击后执行事件
        /// </summary>
        public Action<object> doActionParm;

        /// <summary>
        /// 鼠标移动到控件上的小提示事件
        /// </summary>
        public Action<object, object> doActionMouseMove;
        /// <summary>
        /// 判断是否注册点击事件
        /// </summary>
        private void AddPreviewEvent()
        {
            if (IsDoubleClickCell == EnumShiFou.是)
            {
                if (string.IsNullOrEmpty(ClickCellName))
                {
                    GetXRTableCell();
                }
                else
                {
                    GetXRTableCell(ClickCellName);
                }
            }
        }

        private void Item_BeforePrint(object sender, PrintEventArgs e)
        {
            MemoryStream ms = null;
            try
            {
                XRPictureBox pictureBox = ((XRPictureBox)sender);
                pictureBox.Sizing = ImageSizeMode.Squeeze;
                if (pictureBox.ExpressionBindings != null && pictureBox.ExpressionBindings.Count > 0)
                {
                    string BindName = pictureBox.ExpressionBindings[0].Expression.Replace("[", "").Replace("]", "");
                    object value = GetCurrentColumnValue(BindName);
                    byte[] byteArray = Convert.FromBase64String(value.ToString());
                    ms = new MemoryStream(byteArray);
                    ms.Position = 0;
                    Image img = Image.FromStream(ms);
                    pictureBox.Image = img;
                    ms.Close();
                }
            }
            catch
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }
        }

        /// <summary>
        /// 注册点击事件，指定列
        /// </summary>
        /// <param name="CellName">需要设置点击得列名</param>
        private void GetXRTableCell(string CellName)
        {
            foreach (var Band in this.Report.Bands)
            {
                if (Band.GetType() == typeof(DetailBand))
                {
                    RegisterDetailBand((DetailBand)Band, CellName);

                }
                if (Band.GetType() == typeof(DetailReportBand))
                {
                    foreach (var item in ((DetailReportBand)Band).Controls)
                    {
                        RegisterDetailBand((DetailBand)item, CellName);
                    }
                }
            }
        }
        /// <summary>
        /// 注册点击事件，整行都可以点击
        /// </summary>
        private void GetXRTableCell()
        {
            foreach (var Band in this.Report.Bands)
            {
                if (Band.GetType() == typeof(DetailBand))
                {
                    RegisterDetailBand((DetailBand)Band);
                }
                if (Band.GetType() == typeof(DetailReportBand))
                {
                    foreach (var item in ((DetailReportBand)Band).Controls)
                    {
                        if (item.GetType() == typeof(DetailBand))
                        {
                            RegisterDetailBand((DetailBand)item);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 注册双击事件
        /// </summary>
        /// <param name="detailBand"></param>
        /// <param name="CellName"></param>
        private void RegisterDetailBand(DetailBand detailBand, string CellName = null)
        {
            foreach (var item in detailBand.Controls)
            {
                if (item.GetType() == typeof(XRTable))
                {
                    foreach (XRTableRow row in ((XRTable)item).Rows)
                    {
                        foreach (XRTableCell cell in row.Cells)
                        {
                            if (CellName == null)
                            {
                                cell.BeforePrint += Cell_BeforePrint1;
                                cell.PreviewDoubleClick += Cell_PreviewDoubleClick;
                                cell.PreviewMouseMove += Cell_PreviewMouseMove;
                            }
                            else
                            {
                                if (CellName == cell.Name)
                                {
                                    cell.BeforePrint += Cell_BeforePrint1;
                                    cell.PreviewDoubleClick += Cell_PreviewDoubleClick;
                                    cell.PreviewMouseMove += Cell_PreviewMouseMove;
                                }
                            }

                        }
                    }
                }

            }
        }

        private void Cell_BeforePrint1(object sender, PrintEventArgs e)
        {
            if (((XRLabel)sender).Tag.ToString().StartsWith("公式"))
            {
                var result = ZhuanHuanXMZ((XRLabel)sender);
                ((XRLabel)sender).Text = result;

            }
            ((XRTableCell)sender).Tag = ((XRControl)sender).Report.GetCurrentRow();
        }

        private void Cell_PreviewMouseMove(object sender, PreviewMouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
            object obj = e.Brick.Value;
            if (doActionMouseMove != null)
            {
                doActionMouseMove.Invoke(obj, e);
            }
        }
        private void Cell_PreviewDoubleClick(object sender, PreviewMouseEventArgs e)
        {
            object obj = e.Brick.Value;
            //左键双击事件
            if (e.MouseButton == MouseButtons.Left && doActionParm != null)
            {
                doActionParm.Invoke(obj);
            }
        }

        #endregion

        /// <summary>
        /// 开始打印事件，设置打印指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            if (FromPage != -1)
            {
                if (ToPage == -1)
                {
                    ToPage = FromPage;
                }
                e.PrintDocument.PrinterSettings.FromPage = FromPage;
                e.PrintDocument.PrinterSettings.ToPage = ToPage;

                FromPage = -1;
                ToPage = -1;
            }

            dispalyName = e.PrintDocument.DocumentName;
            if (setPrinterName != null)
            {
                e.PrintDocument.PrinterSettings.PrinterName = setPrinterName;
            }
            e.PrintDocument.DocumentName = e.PrintDocument.PrinterSettings.ToPage.ToString();

        }

        /// <summary>
        /// 增加系统参数
        /// </summary>
        private void AddSystemParam()
        {
            if (systemParamDic != null && !systemParamDic.ContainsKey("病区ID"))
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
                            if (!systemParamDic.ContainsKey(v[0].Description))
                            {
                                if (value != null)
                                {
                                    systemParamDic.Add(v[0].Description, value.ToString());
                                }
                                else
                                {
                                    systemParamDic.Add(v[0].Description, "");
                                }
                            }
                        }
                    }
                }
                DateTime dateTime = DateTime.Now;
                try
                {
                    JCJGGongYongService gongYongService = new JCJGGongYongService();
                    dateTime = gongYongService.GetSysDate().Return;
                }
                catch (Exception ex)
                {
                }
                systemParamDic.Add("统计日期", dateTime.Date.ToString("yyyy-MM-dd"));
                systemParamDic.Add("统计时间", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            }
        }

        /// <summary>
        /// 替换报表中的系统参数
        /// </summary>
        protected void RepalceParms()
        {
            var mediReport = (MediXtraReport)this;
            if (this.systemParamDic != null && this.systemParamDic.Count > 0)
            {
                Console.WriteLine("开始" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                foreach (var item in mediReport.Bands)
                {
                    if (item.GetType() == typeof(DetailReportBand))
                    {
                        //处理子报表参数替换
                        var detailReportBand = (DetailReportBand)item;
                        foreach (var dreportBand in detailReportBand.Bands)
                        {
                            replaceBand((Band)dreportBand);
                        }
                    }
                    else
                    {
                        replaceBand((Band)item);
                    }
                }
                Console.WriteLine("完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            }
        }

        /// <summary>
        /// 替换Band中的参数
        /// </summary>
        /// <param name="band"></param>
        private void replaceBand(Band band)
        {
            foreach (XRControl control in band.Controls)
            {
                if (control.GetType().Name == "XRLabel")
                {
                    var label = (XRLabel)control;
                    if (label.Text.Contains("{"))
                    {

                        foreach (var itemdic in systemParamDic)
                        {
                            label.Text = label.Text.Replace("{" + itemdic.Key + "}", itemdic.Value);
                        }
                    }

                    //由于自定义数据源的名字跟实际赋值的数据源名可能不同，找不到对应数据源时，汇总会默认取主数据源的进行汇总，这个将数据源设为空 &&(label.Summary.Func== SummaryFunc.Sum || label.Summary.Func == SummaryFunc.Count)
                    if (label.DataBindings.ToList().Count > 0)
                    {
                        var dataMember = label.DataBindings.ToList()[0].DataMember;
                        var formatStr = label.DataBindings[0].FormatString;
                        label.DataBindings.Clear();
                        label.DataBindings.Add("Text", null, dataMember, formatStr);
                    }

                    ////自定义统计表达式处理
                    //if (label.Summary.Func == SummaryFunc.Custom)
                    //{
                    //    label.SummaryGetResult += Label_SummaryGetResult;
                    //}

                    //行号的特殊记录处理

                    if (label.Text == "{行号}" || label.Text.TrimStart().StartsWith("公式"))
                    {
                        //由于Text会在单行中被替换掉，所以这里把他存到Tag里
                        label.Tag = label.Text;
                        label.BeforePrint += Label_BeforePrint;
                    }
                }
                else if (control.GetType() == typeof(XRTable))
                {
                    ProcessXRTable((XRTable)control);
                }
                else if (control.GetType() == typeof(XRPivotGrid))
                {
                    ((XRPivotGrid)(control)).FieldValueDisplayText += MediXtraReport_FieldValueDisplayText;
                }
                if (control.GetType() == typeof(XRPictureBox))
                {
                    control.BeforePrint += Item_BeforePrint;
                }
            }
        }

        /// <summary>
        /// 字段显示值事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediXtraReport_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            string huiZongText = "总计";
            if (((XRPivotGrid)(sender)).Fields == null || ((XRPivotGrid)(sender)).Fields.Count < 2)
            {
                return;
            }
            if (!string.IsNullOrEmpty(((XRPivotGrid)(sender)).Fields[0].GrandTotalText))
            {
                huiZongText = ((XRPivotGrid)(sender)).Fields[0].GrandTotalText;
            }
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)//总计
            {
                if (e.IsColumn && (e.DisplayText.Trim() == "Grand Total" || e.DisplayText.Trim() == "总计"))//第一层列总计标题
                {
                    e.DisplayText = huiZongText;
                }
                else
                if (e.IsColumn)//其他层列总计标题
                {
                    if (!e.DisplayText.Contains(huiZongText))
                    {
                        e.DisplayText = e.DisplayText + huiZongText;
                    }
                }
                else if (e.IsColumn == false && (e.DisplayText.Trim() == "Grand Total" || e.DisplayText.Trim() == "总计"))//第一层行总计标题
                {
                    if (!string.IsNullOrEmpty(((XRPivotGrid)(sender)).Fields[1].GrandTotalText))
                    {
                        e.DisplayText = ((XRPivotGrid)(sender)).Fields[1].GrandTotalText;
                    }
                }
                //else//这种情况似乎不会发生
                //{
                //    e.DisplayText = "";
                //}
            }
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)//小计
            {
                if (e.IsColumn && e.Value != null)//第一层列小计标题
                {
                    // e.DisplayText = e.DisplayText.Replace("Total", "").Trim() + "小计";
                    e.DisplayText = e.Value + "小计";
                }
                else if (e.IsColumn)//其他层列小计标题
                {
                    e.DisplayText = e.DisplayText.Replace("Total", "").Trim() + "合计";
                }
            }
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.CustomTotal)//其他自定义合计类型
            {

            }
        }

        /// <summary>
        ///处理XRTable 序号处理
        /// </summary>
        /// <param name="xRTable"></param>
        private void ProcessXRTable(XRTable xRTable)
        {
            foreach (XRTableRow row in xRTable.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.DataBindings.ToList().Count > 0)
                    {

                        var dataMember = cell.DataBindings.ToList()[0].DataMember;
                        if (cell.Report.DataSource.GetType().Name.IndexOf("DataSet") != -1) //如果数据源是DataSet数据集
                        {
                            if (((System.Data.DataSet)cell.Report.DataSource).DefaultViewManager.DataSet.Relations.Count > 0)
                            {
                                var relationName = ((System.Data.DataSet)cell.Report.DataSource).DefaultViewManager.DataSet.Relations[0].RelationName;
                                if (dataMember.IndexOf(relationName) != 0)
                                {
                                    dataMember = relationName + "." + dataMember;
                                }
                            }
                        }

                        var formatStr = cell.DataBindings[0].FormatString;
                        cell.DataBindings.Clear();
                        cell.DataBindings.Add("Text", null, dataMember, formatStr);
                    }
                    if (cell.Text == "{行号}" || cell.Text.TrimStart().StartsWith("公式"))
                    {
                        cell.Tag = cell.Text;
                        cell.BeforePrint += Cell_BeforePrint;
                        // break;//一般是有一个序号列
                    }
                    if (cell.Summary != null)
                    {
                        cell.SummaryCalculated += Cell_SummaryCalculated;
                    }
                    if (cell.Text.Contains("{"))
                    {
                        foreach (var itemdic in systemParamDic)
                        {
                            cell.Text = cell.Text.Replace("{" + itemdic.Key + "}", itemdic.Value);
                        }
                    }
                    foreach (XRControl item in cell.Controls)
                    {
                        if (item.GetType() == typeof(XRPictureBox))
                        {
                            item.BeforePrint += Item_BeforePrint;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 保存计算列值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cell_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value != null)
            {
                var control = (XRControl)sender;
                if (sumValueDic.ContainsKey(control.Name))
                {
                    sumValueDic[control.Name] = e.Value.ToString();
                }
                else
                {
                    sumValueDic.Add(control.Name, e.Value.ToString());
                }
            }
        }

        /// <summary>
        /// 单元格打印前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cell_BeforePrint(object sender, PrintEventArgs e)
        {
            if (IsDoubleClickCell == EnumShiFou.是)
            {
                if (string.IsNullOrEmpty(ClickCellName)) return;
                if (((XRTableCell)sender).Text != ClickCellName)
                {
                    if (((XRTableCell)sender).Tag != null)
                    {
                        if (((XRTableCell)sender).Tag.ToString() == "{行号}")
                        {
                            ((XRTableCell)sender).Text = currentIndex.ToString();
                            currentIndex++;
                        }
                        else if (((XRTableCell)sender).Tag.ToString().StartsWith("公式"))
                        {
                            var result = ZhuanHuanXMZ((XRTableCell)sender);
                            ((XRTableCell)sender).Text = result;
                        }
                    }
                }
            }
            else
            {
                if (((XRTableCell)sender).Tag != null)
                {
                    if (((XRTableCell)sender).Tag.ToString() == "{行号}")
                    {
                        ((XRTableCell)sender).Text = currentIndex.ToString();
                        currentIndex++;
                    }
                    else if (((XRTableCell)sender).Tag.ToString().StartsWith("公式"))
                    {
                        var result = ZhuanHuanXMZ((XRTableCell)sender);
                        ((XRTableCell)sender).Text = result;
                    }
                }
            }
        }

        /// <summary>
        /// 标签页打印前处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_BeforePrint(object sender, PrintEventArgs e)
        {
            if (((XRLabel)sender).Tag != null)
            {
                if (((XRLabel)sender).Tag.ToString() == "{行号}")
                {
                    ((XRLabel)sender).Text = currentIndex.ToString();
                    currentIndex++;
                }
                else if (((XRLabel)sender).Tag.ToString().StartsWith("公式"))
                {
                    var result = ZhuanHuanXMZ((XRLabel)sender);

                    ((XRLabel)sender).Text = result;

                }
            }
        }

        /// <summary>
        /// 转换项目值
        /// </summary>
        /// <param name="label">传入控件</param>
        /// <returns></returns>
        private string ZhuanHuanXMZ(XRControl label)
        {
            string labelText = label.Tag == null ? label.Text : label.Tag.ToString();
            string result = "";
            if (labelText.IndexOf("公式") != -1)
            {
                string[] gongShi = labelText.Replace("公式(", "").Trim().TrimEnd(')').Split(',');
                if (gongShi[0] == "this")
                {
                    var dataSource = label.Report.DataSource;
                    result = ProcessData(dataSource, gongShi, label);
                }
                else if (gongShi[0].ToUpper().StartsWith("XT_SELECTSQL3"))
                {
                    var biaoMing = gongShi[0].Split('.');
                    var dataSource = GYShuJuZDHelper.GetShuJuZD(biaoMing[1]);
                    result = ProcessData(dataSource, gongShi, label);
                }
                else
                {
                    var dataSource = this.DataSource;
                    if (dataSource.GetType() == typeof(DataSet))
                    {
                        DataSet dataSet = (DataSet)dataSource;
                        if (dataSet.Tables.Contains(gongShi[0]))
                        {
                            result = ProcessData(dataSet.Tables[gongShi[0]], gongShi, label);
                        }
                    }
                    else if (dataSource.GetType() == typeof(DataTable))
                    {
                        result = ProcessData(dataSource, gongShi, label);
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 处理表数据
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="gongShi"></param>
        /// <returns></returns>
        private string ProcessData(object dataSource, string[] gongShi, XRControl label)
        {


            DataTable dtSource = new DataTable();
            if (dataSource.GetType() == typeof(DataSet))
            {
                DataSet dataSet = (DataSet)dataSource;
                if (dataSet.Tables.Contains(gongShi[0]))
                {
                    dtSource = dataSet.Tables[gongShi[0]];
                }
            }
            else if (dataSource.GetType() == typeof(DataTable))
            {
                dtSource = (DataTable)dataSource;
            }
            else if (dtSource.GetType().Name == "List`1")
            {
                IEnumerable<object> list = dataSource as IEnumerable<object>;
                foreach (var dtItem in list)
                {
                    //foreach (var item in dtItem.GetType().GetProperties())
                    //{
                    //    var content = item.GetValue(dtItem, null);
                    //    //if (item.PropertyType.Name == "List`1")
                    //    //{
                    //    //    ProcessList(item.GetValue(dtItem, null) as IEnumerable<object>);
                    //    //}
                    //    //else
                    //    //{
                    //    //    if (ReplaceParm.ContainsKey(item.Name))
                    //    //    {
                    //    //        if (((Hashtable)ReplaceParm[item.Name].DataSource).ContainsKey(item.GetValue(dtItem, null).ToString()))
                    //    //        {
                    //                item.SetValue(dtItem, ((Hashtable)ReplaceParm[item.Name].DataSource)[item.GetValue(dtItem, null).ToString()], null);
                    //    //        }
                    //    //    }
                    //    //}
                    //}
                }
            }

            string result = "";


            DataRow[] dataRows;
            if (!string.IsNullOrEmpty(gongShi[3]))
            {
                string where = gongShi[3];
                if (where.IndexOf("{") != -1 && where.IndexOf("}") != -1)
                {
                    int startWeiZhi = where.IndexOf("{");
                    int endWeiZhi = where.IndexOf("}");
                    string colName = where.Substring(startWeiZhi, endWeiZhi - startWeiZhi + 1);
                    var colValue = label.Report.GetCurrentColumnValue(colName.Replace("{", "").Replace("}", "")) ?? "";
                    result = colValue.ToString();
                    where = where.Replace(colName, colValue.ToString());
                }
                dataRows = dtSource.Select(where);
            }
            else
            {
                dataRows = dtSource.Select();
            }

            string[] field = gongShi[1].Split('*');

            if (gongShi[2].ToUpper() == "SUM")
            {
                if (field.Length == 1)
                {
                    //result= dataSet.Tables[gongShi[0]].Compute(string.Format("SUM({0})", field[0]), "true").ToString();
                    decimal sum = 0;
                    foreach (DataRow dr in dataRows)
                    {
                        sum += Convert.ToDecimal(dr[field[0]].ToString());
                    }
                    result = sum.ToString();
                }
            }
            else if (gongShi[2] == "SUMSTR")
            {
                string split = ",";
                if (gongShi.Length > 4)
                {
                    split = gongShi[4];
                }

                foreach (DataRow dr in dataRows)
                {
                    result += dr[field[0]].ToString() + split;
                }
                result = result.TrimEnd(Convert.ToChar(split));
            }
            else if (gongShi[2] == "REPLACE")
            {
                if (dataRows.Count() > 0)
                {
                    result = dataRows[0][field[0]].ToString();
                }
            }

            return result;
        }

        #endregion
    }

    public enum EnumShiFou
    {
        是 = 1,
        否 = 0
    }
    /// <summary>
    /// 连接数据库类型
    /// </summary>
    public enum EnumDBType
    {
        SIIM,
        HIS
    }
}
