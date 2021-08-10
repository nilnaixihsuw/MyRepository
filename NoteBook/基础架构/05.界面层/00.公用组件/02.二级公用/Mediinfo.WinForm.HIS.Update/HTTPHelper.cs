using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    /// <summary>
    /// http下载帮助类
    /// </summary>
    public class HTTPHelper
    {
        /// <summary>
        /// 下载压缩包
        /// </summary>
        /// <param name="url"></param>
        /// <param name="localFilePath"></param>
        /// <param name="baoId"></param>
        /// <param name="jiXianMC"></param>
        /// <returns></returns>
        public static bool DownloadZipFiles(string url, string localFilePath, string baoId, string jiXianMC)
        {
            try
            {
                using (HttpWebResponse web_resp = (HttpWebResponse)GetRequestZip(url, baoId, jiXianMC).GetResponse())
                {
                    using (Stream responseStream = web_resp.GetResponseStream())
                    {
                        System.IO.FileStream writeStream = null; // 写入本地文件流对象
                        if (File.Exists(Path.Combine(localFilePath, jiXianMC + ".zip")))
                        {
                            writeStream = File.OpenWrite(Path.Combine(localFilePath, jiXianMC + ".zip")); // 存在则打开要下载的文件
                        }
                        else
                        {
                            writeStream = new FileStream(Path.Combine(localFilePath, jiXianMC + ".zip"), FileMode.Create);// 文件不保存创建一个文件
                        }
                        byte[] read_bytes = new Byte[2048];
                        int count = 0;
                        while (true)
                        {
                            count = responseStream.Read(read_bytes, 0, read_bytes.Length);
                            if (count > 0)
                                writeStream.Write(read_bytes, 0, count);// 先写入本地临时文件
                            else
                                break;
                        }
                        writeStream.Close();
                        responseStream.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("Date {0} ,Class: {1}, Error: {3}", DateTime.Now, "class:HTTPHelper",
                     ex.Message,
                    ex.InnerException));
                return false;
            }
        }
        public static bool DownloadZipFiles_Progress(string url, string localFilePath, string baoId, string jiXianMC, MediWaitCircleControl label, long totalFileSize, ref long downLoadFileSize)
        {
            try
            {
                long dowwnLoadFileSize = downLoadFileSize;
                try
                {
                    using (HttpWebResponse web_resp = (HttpWebResponse)GetRequestZip(url, baoId, jiXianMC).GetResponse())
                    {
                        using (Stream responseStream = web_resp.GetResponseStream())
                        {
                            FileStream writeStream = null; // 写入本地文件流对象
                            if (File.Exists(Path.Combine(localFilePath, jiXianMC + ".zip")))
                            {
                                writeStream = File.OpenWrite(Path.Combine(localFilePath, jiXianMC + ".zip")); // 存在则打开要下载的文件
                            }
                            else
                            {
                                writeStream = new FileStream(Path.Combine(localFilePath, jiXianMC + ".zip"), FileMode.Create);// 文件不保存创建一个文件
                            }
                            byte[] read_bytes = new Byte[2048];
                            int count = 0;
                            while (true)
                            {
                                count = responseStream.Read(read_bytes, 0, read_bytes.Length);
                                if (count > 0)
                                    writeStream.Write(read_bytes, 0, count);// 先写入本地临时文件
                                else
                                    break;

                                if (label != null && totalFileSize != 0)
                                {

                                    label.Invoke(
                                        (MethodInvoker)
                                        (() =>
                                        {
                                            double progressCount = 0;
                                            if (((dowwnLoadFileSize + writeStream.Position) * 1.0d / totalFileSize) * 100 > 100)
                                            {
                                                progressCount = 100;
                                            }
                                            else
                                            {
                                                progressCount = ((dowwnLoadFileSize + writeStream.Position) * 1.0d / totalFileSize) * 100;
                                                label.Description = "正在下载文件" + Convert.ToInt32(progressCount) + "%";
                                            }

                                        }));
                                }
                            }
                            downLoadFileSize += writeStream.Position;
                            writeStream.Close();
                            responseStream.Close();
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取服务器上的文件大小
        /// </summary>
        /// <param name="url"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static long GetFileSize(string url, string baoId, string jiXianMc, ref string errorMsg)
        {
            long length = 0;
            StringBuilder result = new StringBuilder();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://" + url + "/DaiMaKu_ZXT/DownLoad?baoid=" + baoId + "&jiXianMC=" + jiXianMc));
                request.Method = WebRequestMethods.Http.Head;

                var res = (HttpWebResponse)request.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    length = res.ContentLength; ;
                }

                res.Close();
                return length;
            }

            catch (Exception ex)
            {
                errorMsg = "获取文件大小出错：" + ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 下载配置文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="baoId">包ID 版本号</param>
        /// <param name="jiXianMC">基线名称</param>
        /// <returns></returns>
        public static string DownloadConfigFiles(string url, string baoId, string jiXianMC)
        {
            string ip = string.Empty;
            var ipavailable = NetworkHeler.GetAvailableNetwork();
            if (ipavailable != null)
            {
                var ipList = NetworkHeler.GetAvailableNetwork().Find(o => o.Name.Contains("以太网"));
                if (ipList == null)
                    ipList = NetworkHeler.GetAvailableNetwork().Find(o => o.Name == "WLAN");
                if (ipList == null)
                    ipList = NetworkHeler.GetAvailableNetwork()[0];
                ip = string.Format("{0}-{1}", ipList.Ip, ipList.Ip);
            }
            else
            {
                MessageBox.Show("没有可获取到的网络", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                using (HttpWebResponse web_resp = (HttpWebResponse)GetRequestConfigs(url, baoId, jiXianMC,ip).GetResponse())
                {
                    using (Stream responseStream = web_resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                        {
                            byte[] read_bytes = new Byte[2048];
                            string s = reader.ReadToEnd();
                            return s;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("Date {0} ,Class: {1}, Error: {3}", DateTime.Now, "class:HTTPHelper",
                     ex.Message,
                    ex.InnerException));
                return "";
            }
        }
        /// <summary>
        /// 写入文本文件
        /// </summary>
        /// <param name="value"></param>
        public static void WriteLog(string value)
        {
            IsExistDirectory();
            string path = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\errorLog.txt";
            FileStream f = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine(value);
            sw.Flush();
            sw.Close();
            f.Close();
        }
        /// <summary>
        /// 检查是否存在文件夹
        /// </summary>
        public static void IsExistDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\errorLog.txt";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\errorLog.txt");
                fs.Close();
            }
        }
        private static HttpWebRequest GetRequestConfigs(string url, string baoId, string jiXianMC,string ip)
        {
            HttpWebRequest result = (HttpWebRequest)WebRequest.Create(new Uri("http://" + url + "/DaiMaKu_ZXT/DownLoadConfigs?baoid=" + baoId + "&jiXianMC=" + jiXianMC + "&ipAdress=" + ip));
            return result;
        }
        private static HttpWebRequest GetRequestZip(string url, string baoId, string jiXianMC)
        {
            HttpWebRequest result = (HttpWebRequest)WebRequest.Create(new Uri("http://" + url + "/DaiMaKu_ZXT/DownLoad?baoid=" + baoId + "&jiXianMC=" + jiXianMC));
            return result;
        }
    }
    public class GlobalXmlHelper
    {
        public static void ModifyAttribute(string path, string elements, string s)
        {
            if (File.Exists(path))
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(path);
                System.Xml.XmlElement element = (System.Xml.XmlElement)xmlDoc.SelectSingleNode("HISGlobalSetting/客户端更新HTTP配置信息/" + elements);
                element.InnerText = s;
                //element.SetAttribute("Value", s)
                xmlDoc.Save(path);
            }
        }
    }
    public class FileHelper
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        public static void CreateDir(string dirName)
        {
            string path = dirName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        
        public static void DelectFile(string srcPath)
        {
            try
            {
                if (File.Exists(srcPath))
                {
                    File.Delete(srcPath);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
    /// <summary>
    /// 网络工具类
    /// </summary>
    public class NetworkHeler
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        private NetworkHeler()
        {

        }

        /// <summary>
        /// 设置一个静态变量。避免多次获取
        /// </summary>
        private static List<NetworkConfig> NetworkConfigList { get; set; }

        /// <summary>
        /// 获取当前可用的网络配置
        /// </summary>
        /// <returns></returns>
        public static List<NetworkConfig> GetAvailableNetwork()
        {

            if (NetworkConfigList != null)
            {
                return NetworkConfigList;
            }

            string computerName = System.Environment.MachineName;

            List<NetworkConfig> networkList = new List<NetworkConfig>();

            if (!NetworkInterface.GetIsNetworkAvailable())
                return networkList;

            List<NetworkInterface> interfaces = NetworkInterface.GetAllNetworkInterfaces()
                                                .Where(c => (c.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || c.NetworkInterfaceType == NetworkInterfaceType.Ethernet) && c.OperationalStatus == OperationalStatus.Up)
                                                .ToList();
            // 将本地网卡前移，虚拟网卡向后
            for (int i = 0; i < interfaces.Count; i++)
            {
                for (int j = 1; j < interfaces.Count; j++)
                {
                    // 获取注册表信息
                    string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + interfaces[j].Id + "\\Connection";
                    RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                    if (rk != null)
                    {
                        // 区分 PnpInstanceID   
                        // 如果前面有 PCI 就是本机的真实网卡  
                        string fPnpInstanceID = rk.GetValue("PnPInstanceId", "null").ToString();
                        if (fPnpInstanceID.Length > 3 &&
                            fPnpInstanceID.Substring(0, 3) == "PCI")
                        {
                            NetworkInterface temp = interfaces[j - 1];
                            interfaces[j - 1] = interfaces[j];
                            interfaces[j] = temp;

                        }
                    }
                }
            }

            foreach (NetworkInterface ni in interfaces)
            {
                string ip = String.Empty;

                if (ni.GetIPProperties().UnicastAddresses.Count > 0)
                {
                    foreach (UnicastIPAddressInformation ipadd in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                            ip = ipadd.Address.ToString();
                    }
                }
                else
                {
                    ip = string.Empty;
                }

                string physicalAddress;
                byte[] bytes = ni.GetPhysicalAddress().GetAddressBytes();

                if (bytes.Length > 0)
                    physicalAddress = string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}", bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5]);
                else
                    physicalAddress = string.Empty;

                networkList.Add(new NetworkConfig
                {
                    Id = ni.Id,
                    Name = ni.Name,
                    Description = ni.Description,
                    PhysicalAddress = physicalAddress,
                    NetworkInterfaceType = ni.NetworkInterfaceType,
                    OperationalStatus = ni.OperationalStatus,
                    Ip = ip,
                    IpVersion = ni.Supports(NetworkInterfaceComponent.IPv4) ? 0 : 1,
                    ComputerName = computerName
                });
            }

            NetworkConfigList = networkList;
            return networkList;
        }
    }

    public class NetworkConfig
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 物理（MAC）地址
        /// </summary>
        public string PhysicalAddress { get; set; }

        /// <summary>
        /// 网络接口类型
        /// </summary>
        public NetworkInterfaceType NetworkInterfaceType { get; set; }

        /// <summary>
        /// 网络接口的操作状态
        /// </summary>
        public OperationalStatus OperationalStatus { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 0:IPV4;1:IPV6
        /// </summary>
        public int IpVersion { get; set; }

        /// <summary>
        /// 计算机名
        /// </summary>
        public string ComputerName { get; set; }
    }

}
