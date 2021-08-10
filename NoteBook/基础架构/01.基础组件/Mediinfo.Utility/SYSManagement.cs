﻿using System;
using System.Management;
using System.Net;
using System.Text;

namespace Mediinfo.Utility
{
    /// <summary>
    /// 获取主机相关信息
    /// </summary>
    public class SYSManagement
    {
        /// <summary>  
        /// 获取本地IP  
        /// </summary>  
        /// <returns></returns>  
        public static string GetUserIP()
        {
            string ip = string.Empty;
            string strHostName = Dns.GetHostName(); //得到本机的主机名  
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP  
            if (ipEntry.AddressList.Length > 0)
            {
                ip = ipEntry.AddressList[0].ToString();
            }
            return ip;
        }

        /// <summary>  
        /// 获取主机域名  
        /// </summary>  
        /// <returns></returns>  
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        /// <summary>  
        /// 获取CPU编号  
        /// </summary>  
        /// <returns>返回一个字符串类型</returns>  
        public static string GetCPUID()
        {
            try
            {
                // 需要在解决方案中引用System.Management.DLL文件  
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                string strCpuID = null;
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
#pragma warning disable S1751 // Loops with at most one iteration should be refactored
                    break;
#pragma warning restore S1751 // Loops with at most one iteration should be refactored
                }
                return strCpuID;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>  
        /// 获取第一分区硬盘编号  
        /// </summary>  
        /// <returns>返回一个字符串类型</returns>  
        public static string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string strHardDiskID = null;
                foreach (var o in searcher.Get())
                {
                    var mo = (ManagementObject) o;
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
#pragma warning disable S1751 // Loops with at most one iteration should be refactored
                    break;
#pragma warning restore S1751 // Loops with at most one iteration should be refactored
                }
                return strHardDiskID;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>  
        /// 获取网卡的MAC地址  
        /// </summary>  
        /// <returns>返回一个string</returns>  
        public static string GetNetCardMAC()
        {
            try
            {
                StringBuilder stringMAC = new StringBuilder();
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();
                foreach (var o in MOC)
                {
                    var MO = (ManagementObject) o;
                    if ((bool)MO["IPEnabled"])
                    {
                        stringMAC.Append(MO["MACAddress"]);
                    }
                }
                return stringMAC.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>  
        /// 获取当前网卡IP地址  
        /// </summary>  
        /// <returns></returns>  
        public static string GetNetCardIP()
        {
            try
            {
                string stringIP = string.Empty;
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();
                foreach (var o in MOC)
                {
                    var MO = (ManagementObject) o;
                    if ((bool)MO["IPEnabled"])
                    {
                        string[] IPAddresses = (string[])MO["IPAddress"];
                        if (IPAddresses.Length > 0)
                        {
                            stringIP = IPAddresses[0];
                        }
                    }
                }
                return stringIP;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取外网IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetOutIP(string strUrl)
        { 
            Uri uri = new Uri(strUrl);
            WebRequest wr = WebRequest.Create(uri);
            System.IO.Stream s = wr.GetResponse().GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(s, Encoding.Default);
            string all = sr.ReadToEnd();    // 读取网站的数据             
            int i = all.IndexOf("[", StringComparison.Ordinal) + 1;
            string tempip = all.Substring(i, 15);
            string ip = tempip.Replace("]", "").Replace(" ", string.Empty);
            return ip;
        }
    }
}
