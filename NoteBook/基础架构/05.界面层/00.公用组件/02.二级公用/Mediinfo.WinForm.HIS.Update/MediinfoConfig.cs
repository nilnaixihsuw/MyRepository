using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Mediinfo.WinForm.HIS.Update
{
    /// <summary>
    /// Mediinfo配置
    /// </summary>
    public class MediinfoConfig
    {
        private static Dictionary<string, XmlElement> configures = new Dictionary<string, XmlElement>();
        private static Dictionary<string, XElement> dicConfig = new Dictionary<string, XElement>();

        private MediinfoConfig()
        {
        }

        private static readonly object _lock = new object();

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetValue(string configName, string node)
        {
            if (!configures.ContainsKey(configName))
            {
                lock (_lock)
                {
                    if (!configures.ContainsKey(configName))
                    {
                        lock (_lock)
                        {
                            string config = IOHelper.Read(AppDomain.CurrentDomain.BaseDirectory + configName);
                            // 如果未找到文件则返回空
                            if (config == null)
                                return "";
                            XmlDocument xml = new XmlDocument();
                            // 打开现有的一个xml文件
                            xml.LoadXml(config);
                            // 获得xml的根节点
                            var configure = xml.DocumentElement;
                            configures.Add(configName, configure);
                        }
                    }
                }
            }
            var snode = configures[configName].SelectSingleNode(node);
            if (snode != null)
            {
                return snode.InnerText;
            }
            else
            {
                return "";
            }
        }
        public static bool SetValue(string path, string elements, string s)
        {
            if (configures.Count != 0)
            {
                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.Load(path);
                    System.Xml.XmlElement element = (System.Xml.XmlElement)xmlDoc.SelectSingleNode("Config/" + elements);
                    element.InnerText = s;
                    xmlDoc.Save(path);
                    //重写缓存
                    var configure = xmlDoc.DocumentElement;
                    configures[path] = configure;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            return true;
        }
        public static string GetAttribute(string configName, string node, string attribute)
        {
            if (!dicConfig.ContainsKey(configName))
            {
                lock (_lock)
                {
                    if (!dicConfig.ContainsKey(configName))
                    {
                        lock (_lock)
                        {
                            // 打开现有的一个xml文件
                            XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + configName);
                            // 获得xml的根节点
                            var configure = doc.Element("config");
                            dicConfig.Add(configName, configure);
                        }
                    }
                }
            }
            var locNode = dicConfig[configName].Element("customer")?.Elements(node)?.FirstOrDefault();
            if (locNode != null)
            {
                return locNode.Attribute(attribute).Value;
            }
            else
            {
                return "";
            }
        }

        public static string BaseDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(baseDirectory + "WinFormMain.xml"))
            {
                baseDirectory = Directory.GetCurrentDirectory() + "\\";
            }
            return baseDirectory;
        }
    }
    public class IOHelper
    {
        /// <summary>
        /// 读文件内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string Read(string fileName)
        {
            if (!Exists(fileName))
            {
                return null;
            }
            // 将文件信息读入流中
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                var str = new StreamReader(fs).ReadToEnd();
                // 去除字符顺序标记（BOM）,防止解析xml文件失败
                if (str.Length > 0 && (int)str[0] == 65279)
                    str = str.Remove(0, 1);
                return str;
            }
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool Exists(string fileName)
        {
            if (fileName == null || fileName.Trim() == "")
            {
                return false;
            }

            if (File.Exists(fileName))
            {
                return true;
            }

            return false;
        }
    }
}
