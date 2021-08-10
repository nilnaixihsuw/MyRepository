using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Mediinfo.WinForm.HIS.Update
{
    /// <summary>
    /// 客户端配置辅助类（只供四层，五层使用，三层的Service不要调用）
    /// </summary>
    public class HISClientHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        static HISClientHelper()
        {
            GlobalSetting = HISGlobalSetting.Load();
            ClientSetting = HISClientSetting.Load();
        }
     

        /// <summary>
        /// 全局配置文件帮助类（HISGlobalSetting.xml)
        /// </summary>
        public static HISGlobalSetting GlobalSetting { get; set; }

        /// <summary>
        /// 本地配置文件帮助类（HISClientSetting.xml)
        /// </summary>
        public static HISClientSetting ClientSetting { get; set; }

        /// <summary>
        /// 批处理命令
        /// </summary>
        public static void BatRunCmd(string batName, string batPath, out string errorException)
        {
            errorException = String.Empty;
            try
            {
                if (!String.IsNullOrWhiteSpace(batName) && !batName.Equals("hisstart.bat"))
                {
                    string hisstartpath = batPath + batName;

                    if (File.Exists(hisstartpath))
                    {
                        Process hisstartprocess = new Process();

                        // 
                        string argsment ="";
                        hisstartprocess.StartInfo = new ProcessStartInfo(hisstartpath, argsment);
                        hisstartprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        hisstartprocess.Start();
                    }
                    else
                    {
                        File.WriteAllText(hisstartpath, "", Encoding.Default);
                    }

                }
                else if (batName != null && batName.Equals("hisstart.bat"))
                {
                    string hisstartpath = batPath + "hisstart.bat";
                    if (File.Exists(hisstartpath))
                    {
                        Process hisstartprocess = new Process();
                        hisstartprocess.StartInfo = new ProcessStartInfo(hisstartpath);
                        hisstartprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        hisstartprocess.Start();
                    }
                    else
                    {
                        File.WriteAllText(hisstartpath, "%程序启动批命令%", Encoding.Default);
                    }
                }
            }
            catch (Exception ex)
            {
                errorException = ex.Message;
            }
        }

    }
}
