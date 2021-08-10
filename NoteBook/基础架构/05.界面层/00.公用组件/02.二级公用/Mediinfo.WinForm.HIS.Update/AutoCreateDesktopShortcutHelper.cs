using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Mediinfo.WinForm.HIS.Update
{
    public class AutoCreateDesktopShortcutHelper
    {
        /// <summary>
        /// 创建当前正在运行程序的快捷方式
        /// </summary>
        public static void CreateShortcutOnDesktop()
        {
            string shortcutFullFile = AppDomain.CurrentDomain.BaseDirectory + "HISGlobalSettingHttp.xml";
            string shortcutname = "联众医院信息系统V6.0";
            string description = "Copyright © 1999-2017 联众智慧科技股份有限公司 版权所有";
            if (System.IO.File.Exists(shortcutFullFile))
            {
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.Load(shortcutFullFile);
                if (xmlDocument.SelectSingleNode("HISGlobalSetting") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name") != null)
                {
                    shortcutname = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name").InnerText;
                    if (string.IsNullOrWhiteSpace(shortcutname))
                        xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name").InnerText = "联众医院信息系统V6.0";
                }
                else//创建节点
                {
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting") == null)
                    {
                        XmlElement xmlElement = xmlDocument.CreateElement("HISGlobalSetting");
                        xmlDocument.AppendChild(xmlElement);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting");
                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "shortcutinfo", null);
                        parentXmlNode.AppendChild(xmlNode);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo");

                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "name", null);
                        xmlNode.InnerText = shortcutname;
                        parentXmlNode.AppendChild(xmlNode);
                    }
                }
               
                if (xmlDocument.SelectSingleNode("HISGlobalSetting") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description") != null)
                {
                    description = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description").InnerText;
                    if (string.IsNullOrWhiteSpace(description))
                        xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description").InnerText = "Copyright © 1999-2017 联众智慧科技股份有限公司 版权所有";
                }
                else//创建节点
                {
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting") == null)
                    {
                        XmlElement xmlElement = xmlDocument.CreateElement("HISGlobalSetting");
                        xmlDocument.AppendChild(xmlElement);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting");
                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "shortcutinfo", null);
                        parentXmlNode.AppendChild(xmlNode);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo");

                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "description", null);
                        xmlNode.InnerText = description;
                        parentXmlNode.AppendChild(xmlNode);
                    }
                }

                xmlDocument.Save(shortcutFullFile);


            }
           
            String shortcutPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), shortcutname + ".lnk");
            if (!System.IO.File.Exists(shortcutPath))
            {
                String exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                IWshRuntimeLibrary.IWshShell shell = new IWshRuntimeLibrary.WshShell();
                foreach (var item in System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*.lnk"))
                {
                    IWshRuntimeLibrary.WshShortcut tempShortcut = (IWshRuntimeLibrary.WshShortcut)shell.CreateShortcut(item);
                    if (tempShortcut.TargetPath == exePath) return;
                }
                IWshRuntimeLibrary.WshShortcut shortcut = (IWshRuntimeLibrary.WshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = exePath;
                shortcut.Arguments = "";
                shortcut.Description = description;
                shortcut.WorkingDirectory = Environment.CurrentDirectory;
                shortcut.IconLocation = exePath;
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
        }
    }
}
