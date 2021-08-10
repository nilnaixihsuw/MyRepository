using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 单据处理类的基类
    /// </summary>
    [Description("基础单据打印处理类")]
    public class PRTBase : IReportPRT
    {
        /// <summary>
        /// 报表业务处理
        /// </summary>
        public PRTBase()
        {
        }

        #region 属性定义

        private string danJuID;

        private E_GY_DANJUDXXX danJuDX;

        /// <summary>
        /// 1表示导出 2 上传
        /// </summary>
        public int BaoCunLSBBZ { get; set; }

        /// <summary>
        /// 临时报表，用以多次打印，缓存第一次取到的报表内容
        /// </summary>
        private Stream reportStream;

        private MediXtraReport report;

        /// <summary>
        /// 票据业务ID
        /// </summary>
        protected string piaoJuYeWuID = "";

        /// <summary>
        /// 需替换的数据对象，一般用于代码，名称转换
        /// </summary>
        protected Dictionary<string, RepacleOBJ> ReplaceParm = new Dictionary<string, RepacleOBJ>();

        /// <summary>
        /// 自定义参数字典
        /// </summary>
        protected Dictionary<string, string> UserParm = new Dictionary<string, string>();

        /// <summary>
        /// 数据源
        /// </summary>
        protected object DataSource { get; set; }

        /// <summary>
        /// 单据id对应GY_DANJUXX.DANJUID
        /// </summary>
        public string DanJuID
        {
            get { return danJuID; }
            set
            {
                danJuID = value;
                GetReportContent();
            }
        }

        /// <summary>
        /// 单据打印对象
        /// </summary>
        public E_GY_DANJUDXXX DanJuDX { get { return danJuDX; } set { danJuDX = value; } }

        /// <summary>
        /// 当前报表对象
        /// </summary>
        public MediXtraReport Report { get { return report; } }

        /// <summary>
        /// 数据源字典，用于取得prt后自己做处理时传入数据源，后续使用
        /// </summary>
        public Dictionary<string, dynamic> dataSourceDic = new Dictionary<string, dynamic>();

        #endregion

        #region 各业务处理类重写方法

        /// <summary>
        /// 打印前处理方法
        /// </summary>
        public virtual void BeforPrint()
        {

        }

        /// <summary>
        /// 用户处理,此时DataSource 已经获取到对应数据源，对数据源，或报表特殊赋值做处理
        /// </summary>
        public virtual void UserProcessing()
        {

        }

        #endregion

        #region 对外提供方法

        /// <summary>
        /// 单据打印,界面调用此打印方法打印单据
        /// </summary>
        /// <param name="danJuDXXX"></param>
        /// <param name="dataSource"></param>
        /// <param name="piaoJuYWID"></param>
        /// <param name="dicParm"></param>
        public int Print(E_GY_DANJUDXXX danJuDXXX, object dataSource, string piaoJuYWID = "", Dictionary<string, string> dicParm = null, bool? is_preview = null)
        {
            ProcessReport(danJuDXXX, dataSource, piaoJuYWID, dicParm, is_preview);

            return Print();
        }
        /// <summary>
        /// 保存打印临时表
        /// </summary>
        /// <param name="danJuDXXX"></param>
        /// <param name="dataSource"></param>
        /// <param name="piaoJuYWID"></param>
        /// <param name="dicParm"></param>
        public int BaoCunLSB(E_GY_DANJUDXXX danJuDXXX, object dataSource, string piaoJuYWID = "", Dictionary<string, string> dicParm = null, bool? is_preview = null)
        {
            ProcessReport(danJuDXXX, dataSource, piaoJuYWID, dicParm, is_preview);
            return 0;
        }


        /// <summary>
        /// 单据打印,界面调用此打印方法打印单据生成HTML格式
        /// </summary>
        /// <param name="danJuDXXX"></param>
        /// <param name="dataSource"></param>
        /// <param name="piaoJuYWID"></param>
        /// <param name="dicParm"></param>
        public int PrintHtml(E_GY_DANJUDXXX danJuDXXX, object dataSource, string fileName, string piaoJuYWID = "", Dictionary<string, string> dicParm = null, bool? is_preview = null)
        {
            ProcessReport(danJuDXXX, dataSource, piaoJuYWID, dicParm, is_preview);
            return PrintHtml(fileName);
        }

        /// <summary>
        /// 单据打印,界面调用此打印方法打印单据生成HTML格式
        /// </summary>
        /// <param name="danJuDXXX"></param>
        /// <param name="dataSource"></param>
        /// <param name="piaoJuYWID"></param>
        /// <param name="dicParm"></param>
        public int PrintHCHtml(E_GY_DANJUDXXX danJuDXXX, object dataSource, ref string strReport, string piaoJuYWID = "", Dictionary<string, string> dicParm = null, bool? is_preview = null)
        {
            ProcessReport(danJuDXXX, dataSource, piaoJuYWID, dicParm, is_preview);
            return PrintHtml(ref strReport);
        }


        /// <summary>
        /// 查看报表内容（仅查看不做打印）
        /// </summary>
        /// <param name="danJuDXXX"></param>
        /// <param name="dataSource"></param>
        /// <param name="piaoJuYWID"></param>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public XtraReport ViewReport(E_GY_DANJUDXXX danJuDXXX, object dataSource, string piaoJuYWID = "", Dictionary<string, string> dicParm = null)
        {
            ProcessReport(danJuDXXX, dataSource, piaoJuYWID, dicParm);
            return Report;
        }

        /// <summary>
        /// 设置某个控件的显示文本值
        /// </summary>
        /// <param name="controlName">要设置的对应控件名</param>
        /// <param name="controlText">要显示的控件文本</param>
        /// <returns></returns>
        public bool SetControlText(string controlName, string controlText)
        {
            XRControl control = Report.FindControl(controlName, true);
            if (control == null)
            {
                return false;
            }
            control.Text = controlText;
            return true;
        }

        /// <summary>
        /// 打印当前PRT对应的单据
        /// </summary>
        public int Print()
        {
            Report.CreateDocument();
            Report.PrintingSystem.ShowMarginsWarning = false;
            Report.systemParamDic = UserParm;
            Report.BeforePrint += report_BeforePrint;
            return Report.MediPrint(Report.PrinterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int PrintHtml(string fileName)
        {
            Report.CreateDocument();
            Report.PrintingSystem.ShowMarginsWarning = false;
            Report.systemParamDic = UserParm;
            Report.BeforePrint += report_BeforePrint;
            //string path = Environment.CurrentDirectory + "\\print";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //string fileName = path + "\\" + Name + ".html";
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                { }
            }

            return Report.MediPrintHtml(fileName);
        }


        public int PrintHtml(ref string strReport)
        {
            Report.CreateDocument();
            Report.PrintingSystem.ShowMarginsWarning = false;
            Report.systemParamDic = UserParm;
            Report.BeforePrint += report_BeforePrint;


            return Report.MediPrintHtml(ref strReport);
        }

        /// <summary>
        /// 重置报表,多次给同一个报表附数据源前使用
        /// </summary>
        /// <returns></returns>
        public bool ReSetReport()
        {
            report = (MediXtraReport)DevExpress.XtraReports.UI.XtraReport.FromStream(reportStream, true);
            return true;
        }

        #endregion

        #region 基类内部处理方法

        /// <summary>
        /// 处理报表内容
        /// </summary>
        /// <param name="danJuDXXX"></param>
        /// <param name="dataSource"></param>
        /// <param name="piaoJuYWID"></param>
        /// <param name="dicParm"></param>
        private void ProcessReport(E_GY_DANJUDXXX danJuDXXX, object dataSource, string piaoJuYWID = "", Dictionary<string, string> dicParm = null, bool? is_preview = null)
        {
            DanJuDX = danJuDXXX;
            DataSource = dataSource;
            if (dicParm != null)
            {
                UserParm = dicParm;
            }
            piaoJuYeWuID = piaoJuYWID;

            //处理对象具体的处理业务
            UserProcessing();
            if (string.IsNullOrEmpty(DanJuID))
            {
                MediMsgBox.Show("当前处理对象未设定对应单据ID!");
                return;
            }
            if (DataSource != null && DataSource.GetType().Name.IndexOf("List") == -1 && DataSource.GetType().Name != "DataTable" && DataSource.GetType().Name != "DataSet")
            {
                DataSource = new List<object>() { DataSource };
            }

            Report.DataSource = DataSource;

            //处理数据源替换
            ProcessDataSource();
            Report.systemParamDic = UserParm;
            //modified by chenzheng@20200817  [HR6-4692] （0708演示）工作台—打印业务——护理病历打印页面更改
            if (is_preview == null)
            {
                if (danJuDXXX.YULAN == 1)
                {
                    Report.IsShowPreview = EnumShiFou.是;
                }
            }
            else
            {
                if (is_preview == true)
                {
                    Report.IsShowPreview = EnumShiFou.是;
                }
                else if (is_preview == false)
                {
                    Report.IsShowPreview = EnumShiFou.否;
                }
            }
            //end modified
        }

        /// <summary>
        /// 报表显示打印界面前处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforPrint();
        }

        /// <summary>
        /// 处理数据源，主要对一些需要替换的数据做相应处理
        /// </summary>
        private void ProcessDataSource()
        {
            if (DataSource == null)
            {
                return;
            }

            var detailreport = Report.FindControl("DetailReport", true);
            if (detailreport != null && ((DetailReportBand)detailreport).DataSource != null)
            {
                foreach (var item in Report.CalculatedFields)
                {
                    item.DataSource = ((DetailReportBand)detailreport).DataSource;
                }
            }

            if (ReplaceParm.Count > 0)
            {
                foreach (var source in ReplaceParm)
                {
                    Hashtable hashTable = new Hashtable();
                    if (source.Value.DataSource.GetType().Name == "List`1")
                    {
                        IEnumerable<object> list = source.Value.DataSource as IEnumerable<object>;
                        foreach (var dtItem in list)
                        {
                            string text = dtItem.GetType().GetProperty(source.Value.KeyName).GetValue(dtItem, null).ToString();
                            if (!hashTable.ContainsKey(text))
                            {
                                hashTable.Add(text, dtItem.GetType().GetProperty(source.Value.ValueName).GetValue(dtItem, null).ToString());
                            }
                        }
                    }
                    else if (source.Value.DataSource.GetType().Name == "DataTable")
                    {
                        DataTable dt = source.Value.DataSource as DataTable;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!hashTable.ContainsKey(dr[source.Value.KeyName].ToString()))
                            {
                                hashTable.Add(dr[source.Value.KeyName].ToString(), dr[source.Value.ValueName].ToString());
                            }
                        }
                    }
                    source.Value.DataSource = hashTable;

                }
            }

            //处理主数据源替换
            ProcessData(DataSource);
            //处理子报表
            foreach (var band in report.Bands)
            {
                if (band.GetType() == typeof(DetailReportBand))
                {
                    var detailDataSource = ((DetailReportBand)band).DataSource;
                    ProcessData(detailDataSource);
                }
            }
        }

        /// <summary>
        /// 处理具体数据
        /// </summary>
        /// <param name="dataSource"></param>
        private void ProcessData(object dataSource)
        {
            if (dataSource.GetType().Name == "List`1")
            {
                IEnumerable<object> list = dataSource as IEnumerable<object>;
                ProcessList(list);
            }
            else if (dataSource.GetType().Name == "DataTable")
            {
                DataTable dt = dataSource as DataTable;
                ProcessDataTable(dt);
            }
            else if (dataSource.GetType().Name == "DataSet")
            {
                DataSet ds = dataSource as DataSet;
                foreach (DataTable dt in ds.Tables)
                {
                    ProcessDataTable(dt);
                }
            }
        }

        /// <summary>
        /// 处理List类型数据
        /// </summary>
        /// <param name="list"></param>
        private void ProcessList(IEnumerable<object> list)
        {
            if (list == null)
            {
                return;
            }
            foreach (var dtItem in list)
            {
                if (dtItem.GetType().Name.IndexOf("List") != -1)
                {
                    ProcessList(dtItem as IEnumerable<object>);
                    continue;
                }
                foreach (var item in dtItem.GetType().GetProperties())
                {
                    var content = item.GetValue(dtItem, null);
                    if (item.PropertyType.Name == "List`1")
                    {
                        ProcessList(item.GetValue(dtItem, null) as IEnumerable<object>);
                    }
                    else
                    {
                        if (ReplaceParm.ContainsKey(item.Name))
                        {
                            if (item.GetValue(dtItem, null) != null && ((Hashtable)ReplaceParm[item.Name].DataSource).ContainsKey(item.GetValue(dtItem, null).ToString()))
                            {
                                try
                                {
                                    item.SetValue(dtItem, ((Hashtable)ReplaceParm[item.Name].DataSource)[item.GetValue(dtItem, null).ToString()], null);
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理属性数据
        /// </summary>
        /// <param name="dtItem"></param>
        private void ProcessProperties(object dtItem)
        {
            foreach (var item in dtItem.GetType().GetProperties())
            {
                if (item.PropertyType.Name == "List`1")
                {
                    ProcessList(item.GetValue(dtItem, null) as IEnumerable<object>);
                }
                else
                {
                    if (ReplaceParm.ContainsKey(item.Name))
                    {
                        if (((Hashtable)ReplaceParm[item.Name].DataSource).ContainsKey(item.GetValue(dtItem, null).ToString()))
                        {
                            item.SetValue(dtItem, ((Hashtable)ReplaceParm[item.Name].DataSource)[item.GetValue(dtItem, null).ToString()], null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理datatable数据
        /// </summary>
        /// <param name="dt"></param>
        private void ProcessDataTable(DataTable dt)
        {
            bool CunZaiLie = false;
            List<string> ColumnNameList = new List<string>();
            foreach (var item in ReplaceParm)
            {
                if (dt.Columns.Contains(item.Key))
                {
                    CunZaiLie = true;
                    ColumnNameList.Add(item.Key);
                }
            }
            if (CunZaiLie)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    foreach (var name in ColumnNameList)
                    {
                        if (((Hashtable)ReplaceParm[name].DataSource).ContainsKey(dr[name]))
                        {
                            dr[name] = ((Hashtable)ReplaceParm[name].DataSource)[dr[name].ToString()];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取报表具体内容
        /// </summary>     
        private void GetReportContent()
        {
            JCJGDanJuXXService danjuxx = new JCJGDanJuXXService();
            var result = danjuxx.GetDanJuByDanJuID(DanJuID);
            if (result.ReturnCode == ReturnCode.SUCCESS)
            {
                var danju = result.Return;
                if (danju == null || danju.Count < 1)
                {
                    throw new Exception("未找到对应单据ID为【" + DanJuID + "】的单据信息，请确认！");
                }
                var danJuxx = StringToStream(danju[0].DANJUNR);
                reportStream = danJuxx;
                report = (MediXtraReport)XtraReport.FromStream(danJuxx, true);
                //取子报表
                GetSubReport();

                //设置单据配置
                SetDanJuPZ();
            }
        }

        /// <summary>
        /// 获取子报表，子报表需要存在本地
        /// </summary>
        private void GetSubReport()
        {
            for (int i = 0; i < Report.Bands.Count; i++)
            {
                var band = Report.Bands[i];
                foreach (var item in band.Controls)
                {
                    if (item.GetType() == typeof(XRSubreport))
                    {
                        string reportUrl = ((XRSubreport)item).ReportSourceUrl;

                        if (!string.IsNullOrEmpty(reportUrl))
                        {
                            string savePath = System.Environment.CurrentDirectory + reportUrl.Substring(1);
                            var fileName = savePath.Split('\\');
                            var DanJuService = new JCJGDanJuXXService();
                            var result = DanJuService.GetDanJuByDanJuMC(fileName[fileName.Length - 1]);
                            if (result.Return.ToList().Count < 1)
                            {
                                continue;
                            }
                            var danjuxx = result.Return.ToList()[0];

                            if (!File.Exists(savePath)) //不存在，直接下载
                            {
                                DevExpress.XtraReports.UI.XtraReport mediReport = DevExpress.XtraReports.UI.XtraReport.FromStream(StringToStream(danjuxx.DANJUNR), true);
                                var SJControl = mediReport.FindControl("hidXiuGaiSJ", true);
                                if (SJControl == null)
                                {
                                    DevExpress.XtraReports.UI.XRLabel label_XiuGaiSJ = new XRLabel();
                                    label_XiuGaiSJ.Name = "hidXiuGaiSJ";
                                    label_XiuGaiSJ.Text = danjuxx.XIUGAISJ.ToString();
                                    label_XiuGaiSJ.SizeF = new System.Drawing.SizeF(1F, 1F);
                                    label_XiuGaiSJ.Visible = false;
                                    mediReport.Report.Bands[0].Controls.Add(label_XiuGaiSJ);
                                }
                                else if (SJControl.Text != danjuxx.XIUGAISJ.ToString())
                                {
                                    mediReport.FindControl("hidXiuGaiSJ", true).Text = danjuxx.XIUGAISJ.ToString();
                                }

                                //文件夹不存在的，创建相应文件夹
                                if (!Directory.Exists(savePath.Substring(0, savePath.LastIndexOf("\\"))))
                                {
                                    Directory.CreateDirectory(savePath.Substring(0, savePath.LastIndexOf("\\")));
                                }
                                mediReport.SaveLayout(savePath);
                            }
                            else //存在判断是否与服务器的一致
                            {
                                DevExpress.XtraReports.UI.XtraReport Reportls = new DevExpress.XtraReports.UI.XtraReport();
                                Reportls.LoadLayout(savePath);
                                var SJControl = Reportls.FindControl("hidXiuGaiSJ", true);
                                if (SJControl == null)
                                {
                                    DevExpress.XtraReports.UI.XtraReport mediReport = DevExpress.XtraReports.UI.XtraReport.FromStream(StringToStream(danjuxx.DANJUNR), true);
                                    DevExpress.XtraReports.UI.XRLabel label_XiuGaiSJ = new XRLabel();
                                    label_XiuGaiSJ.Name = "hidXiuGaiSJ";
                                    label_XiuGaiSJ.Text = danjuxx.XIUGAISJ.ToString();
                                    label_XiuGaiSJ.SizeF = new System.Drawing.SizeF(1F, 1F);
                                    label_XiuGaiSJ.Visible = false;
                                    mediReport.Report.Bands[0].Controls.Add(label_XiuGaiSJ);
                                    mediReport.SaveLayout(savePath);
                                }
                                else if (SJControl.Text != danjuxx.XIUGAISJ.ToString())
                                {
                                    DevExpress.XtraReports.UI.XtraReport mediReport = DevExpress.XtraReports.UI.XtraReport.FromStream(StringToStream(danjuxx.DANJUNR), true);
                                    mediReport.FindControl("hidXiuGaiSJ", true).Text = danjuxx.XIUGAISJ.ToString();
                                    mediReport.SaveLayout(savePath);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置单据配置
        /// </summary>
        private void SetDanJuPZ()
        {
            try
            {
                decimal? shangBianJu = 0;
                decimal? xiaBianJu = 0;
                decimal? zuoBianJu = 0;
                decimal? youBianJu = 0;
                decimal? fangXiang = 0;
                decimal? gaoDu = 0;
                decimal? kuanDu = 0;
                string PrintName = "";
                // 首先通过本地设置标志字段配置是否本地优先还是数据库有心啊
                // 如果数据库优先的情况数据配置为空时，本地开启个性化配置还是以本地为标准
                ReportSetting reportSetting = new ReportSetting();
                if (DanJuDX.BENDIFW == 1)
                {
                    if (reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "单据对象ID", "") == "")
                    {
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "单据对象ID", DanJuDX.DANJUDXID);
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "单据对象名称", DanJuDX.DANJUDXMC);
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "上边距", DanJuDX.SHANGBIANJU.ToString());
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "下边距", DanJuDX.XIABIANJU.ToString());
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "左边距", DanJuDX.ZUOBIANJU.ToString());
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "右边距", DanJuDX.YOUBIANJU.ToString());
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "打印方向", DanJuDX.FANGXIANG.ToString());
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "打印机名", DanJuDX.DAYINJI);
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "高度", DanJuDX.GAODU.ToString());
                        reportSetting.SetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "宽度", DanJuDX.KUANDU.ToString());
                        shangBianJu = DanJuDX.SHANGBIANJU;
                        xiaBianJu = DanJuDX.XIABIANJU;
                        zuoBianJu = DanJuDX.ZUOBIANJU;
                        youBianJu = DanJuDX.YOUBIANJU;
                        fangXiang = DanJuDX.FANGXIANG;
                        PrintName = DanJuDX.DAYINJI;
                        gaoDu = DanJuDX.GAODU;
                        reportSetting.Save();
                    }
                    else
                    {
                        shangBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "上边距", "-99"));
                        xiaBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "下边距", "-99"));
                        zuoBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "左边距", "-99"));
                        youBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "右边距", "-99"));
                        fangXiang = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "打印方向", "-1"));
                        PrintName = reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "打印机名", "");
                        gaoDu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "高度", Report.PageHeight.ToString()));
                        kuanDu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "宽度", Report.PageWidth.ToString()));
                    }
                }
                else
                {
                    shangBianJu = DanJuDX.SHANGBIANJU;
                    xiaBianJu = DanJuDX.XIABIANJU;
                    zuoBianJu = DanJuDX.ZUOBIANJU;
                    youBianJu = DanJuDX.YOUBIANJU;
                    fangXiang = DanJuDX.FANGXIANG;
                    PrintName = DanJuDX.DAYINJI;
                    gaoDu = DanJuDX.GAODU;
                    kuanDu = DanJuDX.KUANDU;

                    // 是否使用个性化配置
                    if (reportSetting.GetCustomConfig())
                    {
                        shangBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "上边距", "-99"));
                        xiaBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "下边距", "-99"));
                        zuoBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "左边距", "-99"));
                        youBianJu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "右边距", "-99"));
                        fangXiang = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "打印方向", "-1"));
                        PrintName = reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "打印机名", "");
                        gaoDu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "高度", Report.PageHeight.ToString()));
                        kuanDu = decimal.Parse(reportSetting.GetConfigItemValue("单据配置" + DanJuDX.DANJUDXID, "宽度", Report.PageWidth.ToString()));
                    }
                }

                // 边距
                for (int i = 0; i < Report.Bands.Count; i++)
                {
                    if (Report.Bands[i].GetType().ToString() == "DevExpress.XtraReports.UI.TopMarginBand")
                    {
                        if (shangBianJu > 0)
                        {
                            Report.Bands[i].HeightF = Convert.ToInt16(shangBianJu);
                        }
                    }

                    if (Report.Bands[i].GetType().ToString() == "DevExpress.XtraReports.UI.BottomMarginBand")
                    {
                        if (xiaBianJu > 0)
                        {
                            Report.Bands[i].HeightF = Convert.ToInt16(xiaBianJu);
                        }
                    }
                }

                if (zuoBianJu > 0)
                {
                    Report.Margins.Left = Convert.ToInt16(zuoBianJu);
                }
                if (youBianJu > 0)
                {
                    Report.Margins.Right = Convert.ToInt16(youBianJu);
                }

                if (!string.IsNullOrEmpty(PrintName))
                {
                    Report.PrinterName = PrintName;
                }

                // 1.获取本地打印机列表与报表设置的打印机进行匹配，
                //   如果名称一样但大小写不一样则直接去本地打印机名称
                List<string> list = LocalPrinter.GetLocalPrinters();
                bool exists = false;
                foreach (string p in list)
                {
                    if (String.Compare(p, Report.PrinterName, true) == 0)
                    {
                        Report.PrinterName = p;
                        exists = true;
                        break;
                    }
                }
                // 2.判断报表设置的打印机在本地列表种是否存在或没有设置打印机，
                //   如果不存在或没有设置打印机则使用本地计算机中的默认打印机
                if (String.IsNullOrEmpty(Report.PrinterName) || !exists)
                {
                    Report.PrinterName = LocalPrinter.DefaultPrinter;
                }

                // 判断打印机中是否存在当前格式
                bool isExists = false;
                PrintDocument pd = new PrintDocument();
                foreach (PaperSize ps in pd.PrinterSettings.PaperSizes)
                {
                    if (ps.Kind == report.PaperKind)
                    {
                        isExists = true;
                    }
                }

                if (!isExists)
                {
                    report.PaperKind = PaperKind.Custom;

                    if (fangXiang == 0)
                    {
                        Report.Landscape = false;
                    }
                    else if (fangXiang == 1)
                    {
                        Report.Landscape = true;
                    }

                    if (gaoDu > 0)
                    {
                        Report.PageHeight = Convert.ToInt32(gaoDu);
                    }

                    if (kuanDu > 0)
                    {
                        Report.PageWidth = Convert.ToInt32(kuanDu);
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果有错误，则取默认打印机的默认纸张
            }
            ReportPrint.JiLuHCBBID(DanJuID);
            // 添加票据c重打打印标签
            PiaoJuCDCLBQCL();
        }

        /// <summary>
        /// 开始打印事件用户处理打印重打相关内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrint_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            string msg = PiaoJuChongDaCL();
            if (!string.IsNullOrEmpty(msg))
            {
                e.PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.FrmPrint_Print);
                MediMsgBox.Show(msg);
            }
        }

        /// <summary>
        /// 处理不做打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrint_Print(object sender, PrintEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 添加票据重打标签处理
        /// </summary>
        private void PiaoJuCDCLBQCL()
        {
            if (!string.IsNullOrEmpty(piaoJuYeWuID))
            {
                JCJGPiaoJuCDCSService gYPiaoJuCDCSService = new JCJGPiaoJuCDCSService();
                // 取票据打印记录
                var piaoJuCDCS = gYPiaoJuCDCSService.GetPiaoJuCDCS(piaoJuYeWuID, DanJuDX.DANJUDXID).Return;
                MediTraceList<E_GY_PIAOJUCDCS> mediTraceList;
                if (piaoJuCDCS.Count > 0 && DanJuDX.DAYINCDBZCS > 0)
                {
                    mediTraceList = new MediTraceList<E_GY_PIAOJUCDCS>(piaoJuCDCS);

                    #region 加重打内容

                    string chongDaCiShu = "";

                    if (DanJuDX.DAYINCDBZCS == 1)
                    {
                        chongDaCiShu = "(" + (decimal.Parse(mediTraceList[0].PIAOJUCDCS) - 1) + ")次";
                    }
                    var label_ChongDaBZ = Report.FindControl("hidChongDaBZ", true);
                    if (Report.FindControl("hidChongDaBZ", true) == null)
                    {
                        label_ChongDaBZ = new XRLabel();
                        label_ChongDaBZ.Name = "hidChongDaBZ";
                        label_ChongDaBZ.Text = "重打" + chongDaCiShu;
                        label_ChongDaBZ.SizeF = new System.Drawing.SizeF(100F, 23F);

                        label_ChongDaBZ.ForeColor = System.Drawing.Color.Red;
                        label_ChongDaBZ.LocationF = new System.Drawing.PointF() { X = Report.Bands[0].BoundsF.Width - 50, Y = 30 };
                        Report.Bands[0].Controls.Add(label_ChongDaBZ);
                    }
                    else
                    {
                        label_ChongDaBZ.Text = "重打" + chongDaCiShu;
                        label_ChongDaBZ.ForeColor = System.Drawing.Color.Red;
                    }

                    #endregion
                }
            }
            DevExpress.XtraPrinting.PrintingSystemBase mPSB = Report.PrintingSystem;
            mPSB.StartPrint += new PrintDocumentEventHandler(this.FrmPrint_StartPrint);
        }

        /// <summary>
        /// 票据重打处理
        /// </summary>
        /// <returns></returns>
        private string PiaoJuChongDaCL()
        {
            string returnMsg = "";
            if (!string.IsNullOrEmpty(piaoJuYeWuID))
            {
                JCJGPiaoJuCDCSService gYPiaoJuCDCSService = new JCJGPiaoJuCDCSService();
                //取票据打印记录
                var piaoJuCDCS = gYPiaoJuCDCSService.GetPiaoJuCDCS(piaoJuYeWuID, DanJuDX.DANJUDXID).Return;
                MediTraceList<E_GY_PIAOJUCDCS> mediTraceList;
                if (piaoJuCDCS.Count > 0)
                {
                    mediTraceList = new MediTraceList<E_GY_PIAOJUCDCS>(piaoJuCDCS);
                    if (decimal.Parse(mediTraceList[0].PIAOJUCDCS) == DanJuDX.CHONGDAXZCS)
                    {
                        return "本单据限制重打次数为【" + DanJuDX.CHONGDAXZCS + "】,当前已达最大重打次不允许重打！";
                    }
                    else if (DanJuDX.DANGTIANCDBZ == 1 && DateTime.Now.Date > mediTraceList[0].DAYINRQ.Value.Date)
                    {
                        return "此单据已设置为只能当天进行重打,上次打印时间为【" + mediTraceList[0].DAYINRQ.Value + "】！";
                    }
                    mediTraceList[0].SetTraceChange(true);
                    mediTraceList[0].PIAOJUCDCS = (decimal.Parse(mediTraceList[0].PIAOJUCDCS) + 1).ToString();
                    mediTraceList[0].SetState(DTOState.Update);
                }
                else
                {
                    E_GY_PIAOJUCDCS e_GY_PIAOJUCDCS = new E_GY_PIAOJUCDCS();
                    e_GY_PIAOJUCDCS.SetTraceChange(true);
                    e_GY_PIAOJUCDCS.PIAOJUYWID = piaoJuYeWuID;
                    e_GY_PIAOJUCDCS.PIAOJULXID = DanJuDX.DANJUDXID;
                    e_GY_PIAOJUCDCS.SetState(DTOState.New);
                    mediTraceList = new MediTraceList<E_GY_PIAOJUCDCS>();
                    mediTraceList.Add(e_GY_PIAOJUCDCS);
                }
                var result = gYPiaoJuCDCSService.BaoCunPiaoJuChongDaXX(mediTraceList.GetChanges());
                if (result.ReturnCode != ReturnCode.SUCCESS)
                {
                    returnMsg = ("保存票据重打信息异常：" + result.ReturnMessage);
                }
            }
            return returnMsg;
        }

        #endregion

        #region  数据转换通用方法

        /// <summary>
        /// 字符转流
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static Stream StringToStream(string inputString)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(inputString);
            writer.Flush();
            return stream;
        }

        /// <summary>
        /// 字符流转字符
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            return text;
        }

        /// <summary>  
        /// DataTable转化为List集合  
        /// </summary>  
        /// <typeparam name="T">实体对象</typeparam>  
        /// <param name="dt">datatable表</param>  
        /// <param name="isStoreDB">是否存入数据库datetime字段，date字段没事，取出不用判断</param>  
        /// <returns>返回list集合</returns>  
        protected static List<T> TableToList<T>(DataTable dt, bool isStoreDB = true)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] pArray = type.GetProperties(); //集合属性数组  
            foreach (DataRow row in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>(); //新建对象实例   
                foreach (PropertyInfo p in pArray)
                {
                    if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                    {
                        continue;  //DataTable列中不存在集合属性或者字段内容为空则，跳出循环，进行下个循环     
                    }
                    if (isStoreDB && p.PropertyType == typeof(DateTime) && Convert.ToDateTime(row[p.Name]) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    try
                    {
                        var obj = Convert.ChangeType(row[p.Name], p.PropertyType);//类型强转，将table字段类型转为集合字段类型    
                        p.SetValue(entity, obj, null);
                    }
                    catch (Exception)
                    {
                        // throw;  
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        /// <summary>  
        /// List集合转DataTable  
        /// </summary>  
        /// <typeparam name="T">实体类型</typeparam>  
        /// <param name="list">传入集合</param>  
        /// <param name="isStoreDB">是否存入数据库DateTime字段，date时间范围没事，取出展示不用设置TRUE</param>  
        /// <returns>返回datatable结果</returns>  
        protected static DataTable ListToTable<T>(List<T> list, bool isStoreDB = true)
        {
            Type tp = typeof(T);
            PropertyInfo[] proInfos = tp.GetProperties();
            DataTable dt = new DataTable();
            foreach (var item in proInfos)
            {
                Type colType = item.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                dt.Columns.Add(item.Name, colType); //添加列明及对应类型  
            }
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item, null);
                    if (obj == null)
                    {
                        continue;
                    }
                    if (isStoreDB && proInfo.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    dr[proInfo.Name] = obj;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>  
        /// table指定行转对象  
        /// </summary>  
        /// <typeparam name="T">实体</typeparam>  
        /// <param name="dt">传入的表格</param>  
        /// <param name="rowindex">table行索引，默认为第一行</param>  
        /// <returns>返回实体对象</returns>  
        protected static T TableToEntity<T>(DataTable dt, int rowindex = 0, bool isStoreDB = true)
        {
            Type type = typeof(T);
            T entity = Activator.CreateInstance<T>(); //创建对象实例  
            if (dt == null)
            {
                return entity;
            }
            //if (dt != null)  
            //{  
            DataRow row = dt.Rows[rowindex]; //要查询的行索引  
            PropertyInfo[] pArray = type.GetProperties();
            foreach (PropertyInfo p in pArray)
            {
                if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                {
                    continue;
                }

                if (isStoreDB && p.PropertyType == typeof(DateTime) && Convert.ToDateTime(row[p.Name]) < Convert.ToDateTime("1753-01-02"))
                {
                    continue;
                }
                try
                {
                    var obj = Convert.ChangeType(row[p.Name], p.PropertyType);//类型强转，将table字段类型转为对象字段类型  
                    p.SetValue(entity, obj, null);
                }
                catch (Exception)
                {
                    // throw;  
                }
                // p.SetValue(entity, row[p.Name], null);                     
            }
            //  }  
            return entity;
        }

        #endregion
    }

    #region 其他辅助类

    /// <summary>
    /// 内部用户替换的参数
    /// </summary>
    public class RepacleOBJ
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public object DataSource { get; set; }

        /// <summary>
        /// 键名列
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 键值列
        /// </summary>
        public string ValueName { get; set; }
    }

    /// <summary>
    /// 报表PRT接口
    /// </summary>
    public interface IReportPRT
    {

    }

    /// <summary>
    /// 本地配置文件处理类
    /// </summary>
    public class ReportSetting
    {
        private static ReportSetting _instance = null;
        private XmlDocument xmlDocument = null;

        /// <summary>
        /// 
        /// </summary>
        public ReportSetting() : base()
        {
            if (_instance == null)
            {
                Load();
            }
        }

        /// <summary>
        /// 加载客户端配置文件（外部调用的唯一方式）
        /// </summary>
        /// <returns></returns>
        public ReportSetting Load()
        {
            if (!File.Exists(Application.StartupPath + "\\ReportSetting.xml"))
            {
                CreateXmlFile();
            }
            if (xmlDocument == null)
            {
                try
                {
                    xmlDocument = new XmlDocument();
                    xmlDocument.Load(Application.StartupPath + "\\ReportSetting.xml");
                }
                catch (Exception ex)
                {

                }

            }
            return _instance;
        }

        /// <summary>
        /// 建立配置文件
        /// </summary>
        private void CreateXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点    
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点    
            XmlNode root = xmlDoc.CreateElement("单据配置");
            xmlDoc.AppendChild(root);
            try
            {
                xmlDoc.Save(Application.StartupPath + "\\ReportSetting.xml");
            }
            catch (Exception e)
            {
                //显示错误信息    
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// 读取配置文件（不区分大小写）,如果不存在则返回默认值
        /// </summary>
        /// <param name="sectionName">节点名称（不区分大小写）</param>
        /// <param name="itemName">配置项名称（不区分大小写）</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public virtual string GetConfigItemValue(string sectionName, string itemName, string defaultValue = "")
        {
            XmlNodeList schoolNodeList = xmlDocument.SelectNodes("单据配置/" + sectionName);
            if (schoolNodeList.Count < 1)
                return defaultValue;
            var returnVal = schoolNodeList[0].Attributes[itemName].Value;
            if (string.IsNullOrEmpty(returnVal))
            {
                return defaultValue;
            }
            return returnVal;
        }

        /// <summary>
        /// 是否开启个性化配置
        /// </summary>
        /// <returns></returns>
        public bool GetCustomConfig()
        {
            // 读取本地 ReportSetting.xml 的"单据配置"元素下是否包含"个性化"属性
            bool result = false;
            XmlNodeList schoolNodeList = xmlDocument.SelectNodes("单据配置");
            if (schoolNodeList != null && schoolNodeList.Count > 0)
            {
                var val = schoolNodeList[0].Attributes["个性化"]?.Value;
                if (val != null && !String.IsNullOrWhiteSpace(val.ToString()))
                {
                    if (String.Compare(val.ToString(), "是", true) == 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 写入配置文件（不区分大小写）
        /// </summary>
        /// <param name="sectionName">节点名称（不区分大小写）</param>
        /// <param name="itemName">配置项名称（不区分大小写）</param>
        /// <param name="itemValue">配置项的值</param>
        /// <returns></returns>
        public virtual int SetConfigItemValue(string sectionName, string itemName, string itemValue)
        {
            XmlNodeList nodeListParent = xmlDocument.SelectNodes("单据配置/" + sectionName);
            XmlNode xmlNode = null;
            if (nodeListParent.Count < 1)
            {
                xmlNode = xmlDocument.CreateElement(sectionName);
                XmlAttribute courseNameAttr = xmlDocument.CreateAttribute(itemName);
                courseNameAttr.Value = itemValue;
                xmlNode.Attributes.Append(courseNameAttr);
                xmlDocument.DocumentElement.AppendChild(xmlNode);
            }
            else
            {
                xmlNode = nodeListParent[0];
                if (xmlNode.Attributes[itemName] == null)
                {
                    XmlAttribute courseNameAttr = xmlDocument.CreateAttribute(itemName);
                    courseNameAttr.Value = itemValue;
                    xmlNode.Attributes.Append(courseNameAttr);
                }
                else
                {
                    XmlElement node = (XmlElement)xmlDocument.SelectSingleNode("单据配置/" + sectionName);
                    node.SetAttribute(itemName, itemValue);
                }

            }

            return 0;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public void Save()
        {
            xmlDocument.Save(Application.StartupPath + "\\ReportSetting.xml");
        }
    }

    /// <summary>
    /// 本地打印机类
    /// </summary>
    public class LocalPrinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();

        /// <summary>  
        /// 获取本机默认打印机名称  
        /// </summary>  
        public static String DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }

        /// <summary>  
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。  
        /// </summary>  
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<string>();
            fPrinters.Add(DefaultPrinter); // 默认打印机始终出现在列表的第一项  
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                    fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }

        /// <summary>
        /// 获取打印页设置
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        /// <returns></returns>
        public static PageSettings GetPrinterSettings(string printerName)
        {
            PageSettings pageSettings = new PageSettings();
            PrintDocument printDocument = new PrintDocument();
            // 指定打印机
            printDocument.PrinterSettings.PrinterName = printerName;
            PrinterSettings p = printDocument.PrinterSettings;
            pageSettings = p.DefaultPageSettings;

            return pageSettings;
        }
    }

    #endregion
}