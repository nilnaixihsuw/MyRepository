using Mediinfo.Enterprise.Config;
using Mediinfo.Utility;

using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class MediJsEvent
    {
        public Object MyParam { get; set; }

        public Object GetMyParam()
        {
            if (MyParam.GetType().IsArray)
            {
                String s = "[";
                Object[] o = (Object[])MyParam;
                for (int i = 0; i < o.Length; i++)
                {
                    s += "'" + o[i].ToString() + "'";
                    if (i < (o.Length - 1))
                    {
                        s += ",";
                    }
                }
                s += "]";
                return s;
            }
            return MyParam;
        }

        public string GetToken()
        {
            var auth = DESHelper.Decrypt(IOHelper.Read(AppDomain.CurrentDomain.BaseDirectory + "webauthdata"), "@MEDIHRP");
            return auth.Split(new string[] { "@#mediinfo#@" }, StringSplitOptions.None)[0];
        }

        public string GetServiceContext()
        {
            var auth = DESHelper.Decrypt(IOHelper.Read(AppDomain.CurrentDomain.BaseDirectory + "webauthdata"), "@MEDIHRP");
            return auth.Split(new string[] { "@#mediinfo#@" }, StringSplitOptions.None)[1];
        }

        public string GetServerHost()
        {
            var url = MediinfoConfig.GetValue("HRPConfig.xml", "HOST");
            return url;
        }

        public void Min(string name)
        {
            string strText = "min";     // 发送的消息//遍历系统中运行的进程，获取接收消息的进程
            Process[] processes = Process.GetProcesses();
            Process process = null;
            foreach (Process p in processes)
            {
                try
                {
                    // 接收端的窗口句柄  
                    //IntPtr hwndRecvWindow = process.MainWindowHandle;
                    IntPtr hwndRecvWindow = ImportFromDLL.FindWindow(null, name);
                    if (hwndRecvWindow == IntPtr.Zero)
                    {
                        continue;
                    }

                    // 自己的进程句柄
                    IntPtr hwndSendWindow = Process.GetCurrentProcess().Handle;

                    // 填充COPYDATA结构
                    ImportFromDLL.COPYDATASTRUCT copydata = new ImportFromDLL.COPYDATASTRUCT();
                    copydata.cbData = Encoding.Default.GetBytes(strText).Length; //长度 注意不要用strText.Length;  
                    copydata.lpData = strText;      // 内容  

                    // 发送消息
                    ImportFromDLL.SendMessage(hwndRecvWindow, ImportFromDLL.WM_COPYDATA, hwndSendWindow, ref copydata);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Close(string name)
        {
            string strText = "close";       // 发送的消息//遍历系统中运行的进程，获取接收消息的进程
            Process[] processes = Process.GetProcesses();
            Process process = null;
            foreach (Process p in processes)
            {
                try
                {
                    // 接收端的窗口句柄  
                    //IntPtr hwndRecvWindow = process.MainWindowHandle;
                    IntPtr hwndRecvWindow = ImportFromDLL.FindWindow(null, name);
                    if (hwndRecvWindow == IntPtr.Zero)
                    {
                        continue;
                    }

                    // 自己的进程句柄
                    IntPtr hwndSendWindow = Process.GetCurrentProcess().Handle;

                    // 填充COPYDATA结构
                    ImportFromDLL.COPYDATASTRUCT copydata = new ImportFromDLL.COPYDATASTRUCT();
                    copydata.cbData = Encoding.Default.GetBytes(strText).Length; //长度 注意不要用strText.Length;  
                    copydata.lpData = strText;      // 内容  

                    // 发送消息
                    ImportFromDLL.SendMessage(hwndRecvWindow, ImportFromDLL.WM_COPYDATA, hwndSendWindow, ref copydata);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Loaded(string name)
        {
            string strText = "loaded";      // 发送的消息//遍历系统中运行的进程，获取接收消息的进程
            Process[] processes = Process.GetProcesses();
            Process process = null;
            foreach (Process p in processes)
            {
                try
                {
                    // 接收端的窗口句柄  
                    //IntPtr hwndRecvWindow = process.MainWindowHandle;
                    IntPtr hwndRecvWindow = ImportFromDLL.FindWindow(null, name);
                    if (hwndRecvWindow == IntPtr.Zero)
                    {
                        continue;
                    }

                    // 自己的进程句柄
                    IntPtr hwndSendWindow = Process.GetCurrentProcess().Handle;

                    // 填充COPYDATA结构
                    ImportFromDLL.COPYDATASTRUCT copydata = new ImportFromDLL.COPYDATASTRUCT();
                    copydata.cbData = Encoding.Default.GetBytes(strText).Length; //长度 注意不要用strText.Length;  
                    copydata.lpData = strText;      // 内容  

                    // 发送消息
                    ImportFromDLL.SendMessage(hwndRecvWindow, ImportFromDLL.WM_COPYDATA, hwndSendWindow, ref copydata);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
