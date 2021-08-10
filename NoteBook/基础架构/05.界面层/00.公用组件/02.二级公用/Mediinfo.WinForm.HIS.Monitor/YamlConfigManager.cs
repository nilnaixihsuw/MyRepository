using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace Mediinfo.WinForm.HIS.Monitor
{
    public class YamlConfigManager
    {

        private static YamlMappingNode monitorinfo;
        static YamlConfigManager()
        {
            string yamlFile = AppDomain.CurrentDomain.BaseDirectory + "HaloMonitorConf.yaml";
            string yamlFileString = System.IO.File.ReadAllText(yamlFile);
            var input = new StringReader(yamlFileString);
            var yaml = new YamlStream();
            yaml.Load(input);
            var yamlMappingNode = (YamlMappingNode)yaml.Documents[0].RootNode;
            monitorinfo = (YamlMappingNode)yamlMappingNode.Children["monitorinfo"];
        }

        /// <summary>
        /// 获取写入sqlite的时间间隔
        /// </summary>
        /// <returns></returns>
        public static int GetWriteInterval()
        {
            return Convert.ToInt32(monitorinfo.Children[new YamlScalarNode("writeinterval")].ToString());
        }

        /// <summary>
        /// 获取读取sqlite的时间间隔
        /// </summary>
        /// <returns></returns>
        public static int GetReadInterval()
        {
            return Convert.ToInt32(monitorinfo.Children[new YamlScalarNode("readinterval")].ToString());
        }
        /// <summary>
        /// 获取客户端最大线程数
        /// </summary>
        /// <returns></returns>
        public static int GetClientMaxThreadCount()
        {
            return Convert.ToInt32(monitorinfo.Children[new YamlScalarNode("clientMaxThreadCount")].ToString());
        }

        /// <summary>
        /// 获取服务端最大线程数
        /// </summary>
        /// <returns></returns>
        public static int GetServerMaxThreadCount()
        {
            return Convert.ToInt32(monitorinfo.Children[new YamlScalarNode("serverMaxThreadCount")].ToString());
        }

        /// <summary>
        /// 获取客户端最大上限内存
        /// </summary>
        /// <returns></returns>
        public static int GetClientMaxMemorySize()
        {
            return Convert.ToInt32(monitorinfo.Children[new YamlScalarNode("clientMaxMemorySize")].ToString());
        }
        /// <summary>
        /// 获取服务端最大上线内存
        /// </summary>
        /// <returns></returns>
        public static int GetServiceMaxMemorySize()
        {
            return Convert.ToInt32(monitorinfo.Children[new YamlScalarNode("serviceMaxMemorySize")].ToString());
        }
        /// <summary>
        /// 获取应用程序模式
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationMode()
        {
            return monitorinfo.Children[new YamlScalarNode("ApplicationMode")].ToString();
        }
        /// <summary>
        /// 获取服务器ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetHostIP()
        {
            return monitorinfo.Children[new YamlScalarNode("IP")].ToString();
        }
        /// <summary>
        /// 获取需要监控的进程名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetProcessNames()
        {
            List<string> processNames = new List<string>();
            YamlSequenceNode yamlSequenceNode = (YamlSequenceNode)monitorinfo.Children[new YamlScalarNode("processinfo")];
            foreach (var itemNode in yamlSequenceNode.Children)
            {
                if (itemNode != null && !processNames.Contains(itemNode.ToString()) && !string.IsNullOrWhiteSpace(itemNode.ToString()))
                    processNames.Add(itemNode.ToString());
            }
            return processNames;
        }
    }
}
