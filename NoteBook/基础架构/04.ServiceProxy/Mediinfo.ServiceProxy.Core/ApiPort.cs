using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// api端口号
    /// </summary>
    public class ApiPort
    {
        /// <summary>
        /// 关闭开发模式
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static int CloseServicePort(string basePath)
        {
            basePath = basePath.Replace("AssemblyClient", "AssemblyServer");
            int port = 0;
            if (File.Exists(basePath + "ServicePort"))
            {
                port = Convert.ToInt32(File.ReadAllText(basePath + "ServicePort").Trim().Split('@')[0]);
                TextWriter tw = new StreamWriter(basePath + "ServicePort", false);
                tw.Write(port + "@off");
                tw.Close();
            }
            return port;
        }

        /// <summary>
        /// 开启开发模式并获取一个端口号
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static int OpenServicePort(string basePath)
        {
            basePath = basePath.Replace("AssemblyClient", "AssemblyServer");
            List<int> usedList = PortIsUsedList();

            if (File.Exists(basePath + "ServicePort"))
            {
                int port = Convert.ToInt32(File.ReadAllText(basePath + "ServicePort").Trim().Split('@')[0]);
                TextWriter tw = new StreamWriter(basePath + "ServicePort", false);
                tw.Write(port + "@on");
                tw.Close();
                return port;
            }
            else
            {
                for (int i = 1024; i < 65535; i++)
                {
                    if (!usedList.Contains(i))
                    {
                        TextWriter tw = new StreamWriter(basePath + "ServicePort", false);
                        tw.Write(i + "@on");
                        tw.Close();
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取可用的服务端口号
        /// </summary>
        /// <returns></returns>
        public static int GetServicePort()
        {
            List<int> usedList = PortIsUsedList();
            for (int i = 0; i < 1024; i++)
            {
                // 随机端口号，范围 40000~65535 之间
                int port = new Random().Next(40000, 65535);
                if (!usedList.Contains(port))
                {
                    return port;
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取服务端口号列表
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static Tuple<int, bool> GetServicePort(string basePath)
        {
            basePath = basePath.Replace("AssemblyClient", "AssemblyServer");
            Tuple<int, bool> result = null;
            if (File.Exists(basePath + "ServicePort"))
            {
                string servicePort = File.ReadAllText(basePath + "ServicePort").Trim();
                result = new Tuple<int, bool>(int.Parse(servicePort.Split('@')[0]), servicePort.Split('@')[1] == "on" ? true : false);
            }
            return result;
        }

        /// <summary>
        /// 获取不可用端口号列表
        /// </summary>
        /// <returns></returns>
        private static List<int> PortIsUsedList()
        {
            // 获取本地计算机的网络连接和通信统计数据的信息            
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            // 返回本地计算机上的所有Tcp监听程序            
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
            // 返回本地计算机上的所有UDP监听程序            
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
            // 返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。            
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            List<int> allPorts = new List<int>();
            foreach (IPEndPoint ep in ipsTCP)
            {
                allPorts.Add(ep.Port);
            }
            foreach (IPEndPoint ep in ipsUDP)
            {
                allPorts.Add(ep.Port);
            }
            foreach (TcpConnectionInformation conn in tcpConnInfoArray)
            {
                allPorts.Add(conn.LocalEndPoint.Port);
            }

            return allPorts;
        }
    }
}
