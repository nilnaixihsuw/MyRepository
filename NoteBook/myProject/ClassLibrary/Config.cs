using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassLibrary
{
    /// <summary>
    /// 配置文件操作类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 设置appSetting的add子项
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void SaveAppSetting(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径  
            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(configPath);
            //找出名称为“add”的所有元素  
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性  
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素  
                if(att!=null)
                {
                    if (att.Value == key)
                    { 
                        att = nodes[i].Attributes["value"];
                        att.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改  
            doc.Save(configPath);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 读取appSetting的add子项
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string ReadAppSetting(string key)
        {
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(configPath);
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlAttribute att = nodes[i].Attributes["key"];
                if (att != null)
                {
                    if (att.Value==key)
                    {
                        att = nodes[i].Attributes["value"];
                        return att.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 设置connectionStrings的add子项
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="connectionString">connectionString</param>
        public static void SaveConnectionStrings(string name, string connectionString)
        {
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(configPath); 
            XmlNodeList nodes = doc.GetElementsByTagName("add"); 
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlAttribute att = nodes[i].Attributes["name"];
                if (att != null)
                {
                    if (att.Value == name)
                    {
                        att = nodes[i].Attributes["connectionString"];
                        att.Value = connectionString;
                        break;
                    }
                }
            }
            doc.Save(configPath);
            ConfigurationManager.RefreshSection("connectionString");
        }

        /// <summary>
        /// 读取connectionStrings的add子项
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static string ReadConnectionStrings(string name)
        {
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(configPath);
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlAttribute att = nodes[i].Attributes["name"];
                if (att != null)
                {
                    if (att.Value == name)
                    {
                        att = nodes[i].Attributes["connectionString"];
                        return att.Value;
                    }
                }
            }
            return null;
        }
    }
}
