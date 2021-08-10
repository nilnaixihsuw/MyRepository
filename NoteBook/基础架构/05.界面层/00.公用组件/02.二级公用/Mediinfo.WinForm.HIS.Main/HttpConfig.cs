using Mediinfo.Enterprise.Config;
using Mediinfo.HIS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class HttpConfig : Form
    {
        private string configAdress { get; }
        private string httpConfigFile { get; }
        public HttpConfig()
        {
            InitializeComponent();
            configAdress = Path.Combine(Application.StartupPath, "DownLoadAddress.xml");
            httpConfigFile = Path.Combine(Application.StartupPath, "HISGlobalSettingHttp.xml");
        }

        private void HttpConfig_Load(object sender, EventArgs e)
        {
            if (File.Exists(configAdress))
            {
                string ipadress = MediinfoConfig.GetValue("DownLoadAddress.xml", "ipAddress");
                string[] ipAdress = ipadress.Split(':');
                this.ipadress.Text = ipAdress[0];
                this.port.Text = ipAdress[1];
            }
        }
        /// <summary>
        /// 保存http配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValidIP(this.ipadress.Text))
            {
                MessageBox.Show(this,"IP不合法，请重新输入!", "联众智慧提示", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.ipadress.Text = string.Empty;
                this.ipadress.Focus();
                return;
            }
            if (!IsValidPort(this.port.Text))
            {
                MessageBox.Show(this,"端口不合法，请重新输入!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.port.Text = string.Empty;
                this.port.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.ipadress.Text)) return;
            if (string.IsNullOrWhiteSpace(this.port.Text)) return;
            string downloadurl = this.ipadress.Text + ":" + this.port.Text;
            if (!File.Exists(configAdress))
            {
                CreatXmlFile(configAdress, downloadurl, "AssemblyClient", "HTTP");
            }
            else
            {
                MediinfoConfig.SetValue("DownLoadAddress.xml", "ipAddress", downloadurl);
                //ModifyAttribute(configAdress, "ipAddress", downloadurl);
            }

            if (!File.Exists(httpConfigFile))
            {
                CreatHttpXmlFile(httpConfigFile, "AssemblyClient_JCJG_Dev", "1", "");
            }

            HISGlobalSetting.IsHttp = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// 验证端口是否合法
        /// </summary>
        /// <returns></returns>
        public bool IsValidPort(string port)
        {
            string pattern = @"^[0-9]*$";
            if (!(System.Text.RegularExpressions.Regex.IsMatch(port,pattern))) return false;
            return true;
        }
        public bool IsValidIP(string ip)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(ip, @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$"))
                {
                    string[] ips = ip.Split('.');
                    if (ips.Length == 4 || ips.Length == 6)
                    {
                        if (Int32.Parse(ips[0]) < 256 && Int32.Parse(ips[1]) < 256 & Int32.Parse(ips[2]) < 256 & Int32.Parse(ips[3]) < 256)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建XML配置文件
        /// </summary>
        /// <returns></returns>
        private bool CreatXmlFile(string configadress, string ipadress, string starturl, string updatemode)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                XmlElement config = xml.CreateElement("Config");
                xml.AppendChild(config);

                XmlElement Ipadress = xml.CreateElement("ipAddress");
                Ipadress.InnerText = ipadress;
                config.AppendChild(Ipadress);

                XmlElement Starturl = xml.CreateElement("startUrl");
                Starturl.InnerText = starturl;
                config.AppendChild(Starturl);

                XmlElement Updatemode = xml.CreateElement("upDateMode");
                Updatemode.InnerText = updatemode;
                config.AppendChild(Updatemode);

                xml.Save(configadress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
        /// <summary>
        /// 创建Http配置文件
        /// </summary>
        /// <param name="configadress"></param>
        /// <param name="ipadress"></param>
        /// <param name="starturl"></param>
        /// <param name="updatemode"></param>
        /// <returns></returns>
        private bool CreatHttpXmlFile(string configadress, string jixianmc, string banbenhao, string localpath)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                XmlElement config1 = xml.CreateElement("HISGlobalSetting");
                xml.AppendChild(config1);

                XmlElement config2 = xml.CreateElement(@"客户端更新HTTP配置信息");
                config1.AppendChild(config2);

                XmlElement JiXianMc = xml.CreateElement("JIXIANMC");
                JiXianMc.InnerText = jixianmc;
                config2.AppendChild(JiXianMc);

                XmlElement BanBenHao = xml.CreateElement("BanBenHao");
                BanBenHao.InnerText = banbenhao;
                config2.AppendChild(BanBenHao);

                XmlElement LocalPath = xml.CreateElement("localConfigPath");
                LocalPath.InnerText = localpath;
                config2.AppendChild(LocalPath);

                xml.Save(configadress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
        /// <summary>
        /// 更新XML文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="elements"></param>
        /// <param name="s"></param>
        private bool ModifyAttribute(string path, string elements, string s)
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(path);
                System.Xml.XmlElement element = (System.Xml.XmlElement)xmlDoc.SelectSingleNode("Config/" + elements);
                element.InnerText = s;
                xmlDoc.Save(path);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
