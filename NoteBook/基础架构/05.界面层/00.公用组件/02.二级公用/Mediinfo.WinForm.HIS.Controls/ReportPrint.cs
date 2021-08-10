using Autofac;
using Autofac.Features.OwnedInstances;

using DevExpress.XtraReports.UI;

using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 单据打印
    /// </summary>
    public class ReportPrint
    {
        #region 提供外部调用

        /// <summary>
        /// 外部打印统一调用入口
        /// </summary>
        /// <param name="danJuDXMC">打印单据对象名称</param>
        /// <param name="dataSource">单据数据源</param>
        /// <param name="dicParm">单据内部使用的参数</param>
        /// <param name="piaoJuYWID">票据业务ID  需要重打，及显示重打标志的必须要传入 </param>
        /// <returns>0 、打印成功 -1 、未打印或打印失败</returns>
        public static int Print(string danJuDXMC, object dataSource, Dictionary<string, string> dicParm = null, string piaoJuYWID = null, bool? isPreview = null)
        {
            try
            {
                var danJuDX = GetDanJuDX(danJuDXMC);
                if (string.IsNullOrEmpty(danJuDX.DANJUDXLM))
                {
                    danJuDX.DANJUDXLM = "RRTBase";
                }
                var daYinDX = PrintLocator.Instance.GetPrintService<PRTBase>(danJuDX.DANJUDXLM);
                daYinDX.DanJuDX = danJuDX;
                daYinDX.DanJuID = danJuDX.DANJUID;
                MethodInfo mi = daYinDX.GetType().GetMethod("Print", new Type[] { typeof(E_GY_DANJUDXXX), typeof(object), typeof(string), typeof(Dictionary<string, string>), typeof(bool?) });
                var result = (int)mi.Invoke(daYinDX, new object[] { danJuDX, dataSource, piaoJuYWID, dicParm, isPreview });
                return result;
            }
            catch (Exception ex)
            {
                MediMsgBox.Failure("打印异常！" + ex.Message + ex.InnerException?.Message);
                return -1;
            }
        }

        /// <summary>
        /// 外部打印统一调用入口
        /// </summary>
        /// <param name="danJuDXMC">打印单据对象名称</param>
        /// <param name="dataSource">单据数据源</param>
        /// <param name="dicParm">单据内部使用的参数</param>
        /// <param name="piaoJuYWID">票据业务ID  需要重打，及显示重打标志的必须要传入 </param>
        /// <returns>0 、打印成功 -1 、未打印或打印失败</returns>
        public static int PrintHtml(string danJuDXMC, object dataSource, string Name, E_GY_DAYINJK daYinJK, Dictionary<string, string> dicParm = null, string piaoJuYWID = null, bool? isPreview = null)
        {
            try
            {
                var danJuDX = GetDanJuDX(danJuDXMC);
                if (string.IsNullOrEmpty(danJuDX.DANJUDXLM))
                {
                    danJuDX.DANJUDXLM = "RRTBase";
                }
                string fileName = GetFileName(Name);
                var daYinDX = PrintLocator.Instance.GetPrintService<PRTBase>(danJuDX.DANJUDXLM);
                daYinDX.DanJuID = danJuDX.DANJUID;
                MethodInfo mi = daYinDX.GetType().GetMethod("PrintHtml", new Type[] { typeof(E_GY_DANJUDXXX), typeof(object), typeof(string), typeof(string), typeof(Dictionary<string, string>), typeof(bool?) });
                var result = (int)mi.Invoke(daYinDX, new object[] { danJuDX, dataSource, fileName, piaoJuYWID, dicParm, isPreview });

                if (result == 0)
                {
                    List<E_GY_DAYINJK> lstDaYinJK = new List<E_GY_DAYINJK>();
                    //string fileName = Environment.CurrentDirectory + "\\print" + "\\" + Name + ".html";

                    if (File.Exists(fileName))
                    {
                        StreamReader stream = new StreamReader(fileName, Encoding.UTF8);
                        daYinJK.DAYINNR = stream.ReadToEnd();
                    }

                    //var ret = daYinJKService.SaveDaYinJK(lstDaYinJK);
                    //if (ret.ReturnCode != Enterprise.ReturnCode.SUCCESS)
                    //{
                    //    MediMsgBox.Failure("打印导出HTML保存打印接口报错！", ret);
                    //    return -1;
                    //}
                }

                return result;
            }
            catch (Exception ex)
            {
                MediMsgBox.Failure("打印异常！" + ex.Message + ex.InnerException?.Message);
                return -1;
            }
        }

        /// <summary>
        /// 外部打印统一调用入口
        /// </summary>
        /// <param name="danJuDXMC">打印单据对象名称</param>
        /// <param name="dataSource">单据数据源</param>
        /// <param name="dicParm">单据内部使用的参数</param>
        /// <param name="piaoJuYWID">票据业务ID  需要重打，及显示重打标志的必须要传入 </param>
        /// <returns>0 、打印成功 -1 、未打印或打印失败</returns>
        public static int PrintHCHtml( string danJuDXMC, object dataSource, ref E_GY_DAYINJK daYinJK, Dictionary<string, string> dicParm = null, string piaoJuYWID = null, bool? isPreview = null)
        {
            try
            {
                var danJuDX = GetDanJuDX(danJuDXMC);
                if (string.IsNullOrEmpty(danJuDX.DANJUDXLM))
                {
                    danJuDX.DANJUDXLM = "RRTBase";
                }
                //string fileName = GetFileName(Name);
                var daYinDX = PrintLocator.Instance.GetPrintService<PRTBase>(danJuDX.DANJUDXLM);
                daYinDX.DanJuID = danJuDX.DANJUID;
                daYinDX.BaoCunLSBBZ = 2;
                //MethodInfo mi = daYinDX.GetType().GetMethod("PrintHtml", new Type[] { typeof(E_GY_DANJUDXXX), typeof(object), typeof(string), typeof(string), typeof(Dictionary<string, string>), typeof(bool?) });
                //var result = (int)mi.Invoke(daYinDX, new object[] { danJuDX, dataSource, fileName, piaoJuYWID, dicParm, isPreview });
                var item =((List<E_GY_DAYINJKLSB>)dataSource)?.FirstOrDefault();

                if (item == null)
                    return -1;

                var tuPian = item.TUPIAN;
                string strReport = string.Empty;
                string htmlTuPian = string.Empty;
                var result = daYinDX.PrintHCHtml(danJuDX, dataSource, ref strReport ,piaoJuYWID, dicParm, isPreview);
                if (!string.IsNullOrWhiteSpace(tuPian))
                {
                    //htmlTuPian =$"<p align=\"right\"><img src=\"data: image / bmp; base64,"+ tuPian+ "\" width =\"50\" height =\"30\"></p> ";

                    //strReport= strReport.Replace("<div style=\"overflow:hidden;width:118px;height:15px;\">", htmlTuPian);

                    htmlTuPian = $"<div style=\"width:118px; height:14px; text-align:center; vertical-align:center;\" ><img style=\"margin-top:-6px\" src=\"data: image / bmp; base64," + tuPian + "\" width =\"50\" height =\"30\"></div> ";

                    int index = strReport.LastIndexOf(@"overflow");
                    int startIndex = 0;
                    int endIndex = 0;
                    for (int i = index - 1; i >= 0; i--)
                    {
                        if (strReport[i] == '<')
                        {
                            startIndex = i;
                            break;
                        }
                    }
                    for (int i = index + 1; i < strReport.Length; i++)
                    {
                        if (strReport[i] == '>')
                        {
                            endIndex = i;
                            break;
                        }
                    }
                    string oldString = strReport.Substring(startIndex, endIndex - startIndex + 1);
                    strReport = strReport.Replace(oldString, htmlTuPian);
                }
                daYinJK.DAYINNR = strReport;
                //if (!string.IsNullOrWhiteSpace(strReport))
                //{
                //    JCJGDaYinJKService daYinJKService = new JCJGDaYinJKService();
                //    List<E_GY_DAYINJK> lstDaYinJK = new List<E_GY_DAYINJK>();
                //    daYinJK.DAYINNR = strReport;
                //    lstDaYinJK.Add(daYinJK);

                //    var ret = daYinJKService.SaveDaYinJK(lstDaYinJK);
                //    if (ret.ReturnCode != Enterprise.ReturnCode.SUCCESS)
                //    {
                //        MediMsgBox.Failure("打印导出HTML保存打印接口报错！", ret);
                //        return -1;
                //    }
                //}

                return result;
            }
            catch (Exception ex)
            {
                //MediMsgBox.Failure("打印异常！" + ex.Message + ex.InnerException?.Message);
                return -1;
            }
        }


        /// <summary>
        /// 保存临时表
        /// </summary>
        /// <param name="danJuDXMC">打印单据对象名称</param>
        /// <param name="dataSource">单据数据源</param>
        /// <param name="dicParm">单据内部使用的参数</param>
        /// <param name="piaoJuYWID">票据业务ID  需要重打，及显示重打标志的必须要传入 </param>
        /// <returns>0 、打印成功 -1 、未打印或打印失败</returns>
        public static int BaoCunLSB(string danJuDXMC, object dataSource, Dictionary<string, string> dicParm = null, string piaoJuYWID = null, bool? isPreview = null)
        {
            try
            {
                var danJuDX = GetDanJuDX(danJuDXMC);
                if (string.IsNullOrEmpty(danJuDX.DANJUDXLM))
                {
                    danJuDX.DANJUDXLM = "RRTBase";
                }
    
                var daYinDX = PrintLocator.Instance.GetPrintService<PRTBase>(danJuDX.DANJUDXLM);
                daYinDX.DanJuID = danJuDX.DANJUID;
                daYinDX.BaoCunLSBBZ = 1;
         
                string strReport = string.Empty;
                var result = daYinDX.BaoCunLSB(danJuDX, dataSource, piaoJuYWID, dicParm, isPreview);

                return result;
            }
            catch (Exception ex)
            {
                //MediMsgBox.Failure("打印异常！" + ex.Message + ex.InnerException?.Message);
                return -1;
            }
        }

        private static string GetFileName(string WenDangName)
        {
            string fileName = string.Empty;
            string path = Environment.CurrentDirectory + "\\print";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName = path + "\\" + WenDangName + ".html";

            return fileName;
        }

        /// <summary>
        /// 加载报表内容
        /// </summary>
        /// <param name="danJuDXMC">打印单据对象名称</param>
        /// <param name="dataSource">单据数据源</param>
        /// <param name="dicParm">单据内部使用的参数</param>
        /// <param name="piaoJuYWID">票据业务ID  需要重打，及显示重打标志的必须要传入 </param>
        public static MediXtraReport LoadReport(string danJuDXMC, object dataSource, Dictionary<string, string> dicParm = null, string piaoJuYWID = null)
        {
            var danJuDX = GetDanJuDX(danJuDXMC);
            if (string.IsNullOrEmpty(danJuDX.DANJUDXLM))
            {
                danJuDX.DANJUDXLM = "RRTBase";
            }
            var daYinDX = PrintLocator.Instance.GetPrintService<PRTBase>(danJuDX.DANJUDXLM);
            MethodInfo mi = daYinDX.GetType().GetMethod("ViewReport");
            var report = mi.Invoke(daYinDX, new object[] { danJuDX, dataSource, piaoJuYWID, dicParm });
            return (MediXtraReport)report;
        }

        /// <summary>
        /// 根据单据对象名称获取单据对象基类
        /// </summary>
        /// <param name="danJuDXMC"></param>
        /// <returns></returns>
        public static PRTBase GetReportPRT(string danJuDXMC)
        {
            var danJuDX = GetDanJuDX(danJuDXMC);
            if (string.IsNullOrEmpty(danJuDX.DANJUDXLM))
            {
                danJuDX.DANJUDXLM = "RRTBase";
            }
            var daYinDX = PrintLocator.Instance.GetPrintService<PRTBase>(danJuDX.DANJUDXLM);
            daYinDX.UserProcessing();//取包对应报表
            return daYinDX;
        }

        /// <summary>
        /// 根据单据ID,获取报表对象
        /// </summary>
        /// <param name="danJuID">单据ID</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public static MediXtraReport GetXtraReport(string danJuID, ref string message)
        {
            var danJuxx = GetXtraReportStream(danJuID, ref message);
            if (danJuxx == null)
            {
                return null;
            }
            return (MediXtraReport)XtraReport.FromStream(danJuxx, true);
        }

        /// <summary>
        /// 根据单据ID,获取报表对象数据流
        /// </summary>
        /// <param name="danJuID">单据ID</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public static Stream GetXtraReportStream(string danJuID, ref string message)
        {
            JCJGDanJuXXService danjuxx = new JCJGDanJuXXService();
            var returnVal = danjuxx.GetDanJuByDanJuID(danJuID);
            if (returnVal.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                message = "取单据信息出错：" + returnVal.ReturnMessage;
                return null;
            }
            var danju = returnVal.Return;
            if (danju == null || danju.Count < 1)
            {
                message = string.Format("未取到单据ID为{0}的单据信息，请确认！", danJuID);
                return null;
            }
            var danJuxx = StringToStream(danju[0].DANJUNR);
            return danJuxx;
        }

        /// <summary>
        /// 将String 转为Stream
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
        /// 记录需要缓存的报表ID
        /// </summary>
        /// <param name="danJuID"></param>
        public static void JiLuHCBBID(string danJuID)
        {
            if (danJuID != null && danJuID.Length < 2)
            {
                return;
            }
            ReportSetting reportSetting = new ReportSetting();
            var huanCunBBID = reportSetting.GetConfigItemValue("需要缓存报表ID", "报表ID");
            if (huanCunBBID != "")
            {
                if (!huanCunBBID.Contains(danJuID + "|"))
                {
                    string[] baoBiaoIDS = huanCunBBID.Split('|');
                    // 最多缓存20个
                    if (baoBiaoIDS.Length > 20)
                    {
                        int qiShi = baoBiaoIDS.Length - 19;
                        var newID = "";
                        for (int i = qiShi; i < baoBiaoIDS.Length; i++)
                        {
                            newID += baoBiaoIDS[i] + "|";
                        }
                        newID += danJuID + "|";
                        huanCunBBID = newID;
                    }
                    try
                    {
                        reportSetting.SetConfigItemValue("需要缓存报表ID", "报表ID", huanCunBBID + danJuID + "|");
                    }
                    catch
                    {
                      throw new Exception(System.Windows.Forms.Application.StartupPath + "\\ReportSetting.xml"+"配置文件格式不合法,请联系管理员!");
                    }
                    reportSetting.Save();
                }
            }
            else
            {
                try
                {
                    reportSetting.SetConfigItemValue("需要缓存报表ID", "报表ID", danJuID + "|");
                }
                catch (Exception)
                {
                    throw new Exception(System.Windows.Forms.Application.StartupPath + "\\ReportSetting.xml" + "配置文件格式不合法,请联系管理员!");
                }    
                reportSetting.Save();
            }
        }

        /// <summary>
        /// 由于报表打印需要加载dev的相应dll，第一次加载速度较慢，所以在登陆的时候，先启用另外线程加载最近20个常使用的报表
        /// </summary>
        public static void InitializeCache()
        {
            try
            {
                ReportSetting reportSetting = new ReportSetting();
                var baoBiaoID = reportSetting.GetConfigItemValue("需要缓存报表ID", "报表ID");
                if (!string.IsNullOrEmpty(baoBiaoID))
                {
                    string[] baoBiaoIDArr = baoBiaoID.TrimEnd('|').Split('|');
                    for (int i = baoBiaoIDArr.Length - 1; i >= 0; i--)
                    {
                        string message = "";
                        //这个只要取过对应报表就会记录到内存后面会快
                        if (!string.IsNullOrEmpty(baoBiaoIDArr[i]))
                        {
                            ReportPrint.GetXtraReport(baoBiaoIDArr[i], ref message);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //缓存加载报表内容出问题不做处理
            }
        }

        #endregion

        #region 内部处理

        /// <summary>
        /// 根据单据对象名称获取对应单据对象
        /// </summary>
        /// <param name="danJuDXMC"></param>
        /// <returns></returns>
        private static E_GY_DANJUDXXX GetDanJuDX(string danJuDXMC)
        {
            JCJGDanJuDXXXService danJuDXService = new JCJGDanJuDXXXService();
            var result = danJuDXService.GetDanJuDXByDanJuDXMC(danJuDXMC);
            if (result.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                throw new Exception("根据单据对象名【" + danJuDXMC + "】获取单据对象失败。错误明细：" + result.ReturnMessage);
            }
            var resultDX = result.Return.Where(o => o.ZUOFEIBZ != 1).ToList();

            E_GY_DANJUDXXX danjudxxx;
            if (resultDX.Count == 1)
            {
                danjudxxx = resultDX[0];
            }
            else if (resultDX.Count == 0)
            {
                throw new Exception("未找到单据对象名为【" + danJuDXMC + "】的单据对象记录，请确认！");
            }
            else
            {
                var XiTongJG = resultDX.Where(o => o.YINGYONGID == HISClientHelper.YINGYONGID).ToList();
                danjudxxx = XiTongJG.Count > 0 ? XiTongJG[0] : resultDX.Where(o => o.YINGYONGID == "00").ToList()[0];
            }
            JiLuHCBBID(danjudxxx.DANJUID);
            return danjudxxx;
        }

        #endregion
    }

    /// <summary>
    /// 服务定位器
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public class PrintLocator
    {
        private readonly IContainer _container;
        private static PrintLocator _instance = new PrintLocator();

        /// <summary>
        /// 单例构造函数
        /// </summary>
        private PrintLocator()
        {
            try
            {
                var builder = new ContainerBuilder();
                var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

                List<Assembly> assemblys = new List<Assembly>();

                DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
                var mediDlls = TheFolder.GetFiles().Where(c => c.Extension.Contains("dll") && c.FullName.Contains("Mediinfo.WinForm."));

                // 遍历文件
                foreach (FileInfo NextDll in mediDlls)
                {
                    var ass = Assembly.LoadFrom(NextDll.FullName);
                    assemblys.Add(ass);
                }

                var types = assemblys.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IReportPRT)))).ToArray();

                var impls = types.Where(t => !t.IsInterface).ToList();
                foreach (var impl in impls)
                {
                    builder.RegisterType(impl).As(typeof(PRTBase)).InstancePerDependency();
                }
                _container = builder.Build();
            }
            catch (Exception ex)
            {
                Mediinfo.Enterprise.Log.LogHelper.Intance.Error("打印异常", "[PrintLocator]打印错误", ex.Message);
                throw new Exception("打印异常。");
            }
            
        }

        public static PrintLocator Instance
        {
            get { return _instance; }
        }

        #region Public Methods

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPrintService<T>(string waiSheMing)
        {
            var ownedT = _container.Resolve<Owned<IEnumerable<T>>>();
            return ownedT.Value.ToList().Where(m => m.GetType().Name == waiSheMing).FirstOrDefault();
        }

        #endregion
    }
}
