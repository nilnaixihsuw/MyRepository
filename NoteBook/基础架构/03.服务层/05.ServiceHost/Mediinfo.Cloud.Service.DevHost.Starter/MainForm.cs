using Mediinfo.APIGateway.Core.Generate;
using Mediinfo.APIGateway.Core.Services;
using Mediinfo.Cloud.Service.DevHost.Starter.Configuration;
using Mediinfo.Infrastructure.Core.UnitOfWork;
using Mediinfo.ServiceProxy.Core;

using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.Cloud.Service.DevHost.Starter
{
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            panelControl2.VerticalScroll.Value = panelControl2.VerticalScroll.Maximum;
        }

        /// <summary>
        /// 处理窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 当前处理请求的次数
        /// </summary>
        public static long CurrentRequestTimes { get; set; } = 0;

        /// <summary>
        /// 处理窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            var x = 10;
            var y = 30;

            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            // assemblyPath = @"D:\0000\0\636699191496133779";
            // 获取所有服务
            DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = TheFolder.GetFiles("Mediinfo.Service.*.dll", SearchOption.AllDirectories).ToList();

            // 遍历文件
            for (int i = 0; i < mediDlls.Count; i++)
            {
                Point point = new Point(x + ((i % 3) * 290), 30 + (30 * (i / 3)));

                CheckBox ck = new CheckBox();
                ck.Width = 285;
                ck.Name = mediDlls[i].FullName;
                ck.Text = mediDlls[i].Name;
                ck.Location = point;
                ck.Checked = false;
                //groupBox1.Controls.Add(ck);
                flowLayoutPanel1.Controls.Add(ck);
            }

            foreach (Control gbox in groupBox1.Controls)
            {
                if (gbox is VScrollBar) continue;
                gbox.Tag = gbox.Location.Y;
            }

            BtnGenerate.Enabled = false;

            BtnStart_Click(sender, e);
        }

        IDisposable server;
        /// <summary>
        /// 处理开始的按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnStart_Click(object sender, EventArgs e)
        {
            int port = ApiPort.OpenServicePort(AppDomain.CurrentDomain.BaseDirectory);

            List<Assembly> lists = new List<Assembly>();

            var groupControls = flowLayoutPanel1.Controls;
            foreach (var groupControl in groupControls)
            {
                if (groupControl is CheckBox)
                {
                    var control = groupControl as CheckBox;

                    // 加载服务dll
                    string dllPath = control.Name;
                    var assembly = Assembly.LoadFrom(dllPath);
                    lists.Add(assembly);
                    ServiceInfo serviceInfo = new ServiceInfo(assembly);
                }
            }

            // 开始参数
            StartOptions startOptions = new StartOptions();
            startOptions.Port = port;
            startOptions.Urls.Add("http://127.0.0.1:" + port);
            startOptions.Urls.Add("http://localhost:" + port);

            // 设置支持的IP
            string name = System.Net.Dns.GetHostName();
            System.Net.IPAddress[] ipadrlist = System.Net.Dns.GetHostAddresses(name);
            foreach (System.Net.IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    startOptions.Urls.Add("http://" + ipa.ToString() + ":" + port);
            }

            try
            {
                server = WebApp.Start<Startup>(startOptions);
            }
            catch (TargetInvocationException ex)
            {

                MessageBox.Show(ex.ToString());
                throw ex;
            }

            // 刷新缓存
            CacheController cacheController = new CacheController();

            // 刷新参数缓存
            cacheController.RefreshCanShuCache();

            txtPort.Text = port.ToString();
            BtnStart.Enabled = false;
            BtnGenerate.Enabled = true;
            txtPort.Enabled = false;
        }

        /// <summary>
        /// 处理生成代理类事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory + "ServiceClient";
            DelectDir(dirPath);

            GenerateController generate = new GenerateController();
            var groupControls = flowLayoutPanel1.Controls;
            foreach (var groupControl in groupControls)
            {
                if (groupControl is CheckBox)
                {
                    var control = groupControl as CheckBox;
                    //control.Enabled = false;
                    if (control.Checked)
                    {
                        string dllPath = control.Name;
                        var assembly = Assembly.LoadFrom(dllPath);
                        ServiceInfo serviceInfo = new ServiceInfo(assembly);
                        // 生成代理类
                        generate.Generate(assembly, serviceInfo.GetServiceName(), serviceInfo.GetServiceVersion());
                    }
                }
            }
            // 弹出资源管理器
            Process.Start("explorer.exe", dirPath);
        }

        /// <summary>
        /// 删除指定目录
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  // 返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            // 判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          // 删除子目录和文件

                    }
                    else
                    {
                        File.Delete(i.FullName);      // 删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                //throw;
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (Control gbox in groupBox1.Controls)
            {
                if (gbox is VScrollBar) continue;
                gbox.Location = new Point(gbox.Location.X, (int)gbox.Tag - e.NewValue);
            }
        }

        private void BtnGenerateJS_Click(object sender, EventArgs e)
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory + "ServiceClient";
            DelectDir(dirPath);

            GenerateJS generate = new GenerateJS();
            var groupControls = flowLayoutPanel1.Controls;
            foreach (var groupControl in groupControls)
            {
                if (groupControl is CheckBox)
                {
                    var control = groupControl as CheckBox;
                    //control.Enabled = false;
                    if (control.Checked)
                    {
                        string dllPath = control.Name;
                        var assembly = Assembly.LoadFrom(dllPath);
                        ServiceInfo serviceInfo = new ServiceInfo(assembly);
                        // 生成代理类
                        generate.Generate(assembly);
                    }
                }
            }
            // 弹出资源管理器
            Process.Start("explorer.exe", dirPath);
        }

        private void debugsql_CheckedChanged_1(object sender, EventArgs e)
        {
            if (debugsql.Checked)
            {
                UnitOfWorkBase.DebugSql = true;
            }
            else
            {
                UnitOfWorkBase.DebugSql = false;
            }
        }
    }
}
