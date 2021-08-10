using Mediinfo.HIS.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediWebFromControl : UserControl
    {
        /// <summary>
        /// 需要展示的页面地址(1、固定地址；2、浏览地址；)
        /// </summary>
        public string StartURL { get; set; }

        /// <summary>
        /// 网址拼接数据
        /// </summary>
        public Dictionary<string, string> DictryWhere = new Dictionary<string, string>();

        /// <summary>
        /// 浏览器类型
        /// </summary>
        [DefaultValue(0)]
        public WebBrowserType WebBrowserType { get; set; } = WebBrowserType.Default;

        /// <summary>
        /// 
        /// <summary>
        public delegate void KongJianFHXX(string RefMsg);

        /// <summary>
        /// 控件返回消息
        /// </summary>
        public event KongJianFHXX KongJianFHXXEvent;

        public MediWebFromControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控件自定义事件
        /// </summary>
        /// <param name="refMsg"></param>
        public void KongJianSJ(string refMsg)
        {
            KongJianFHXXEvent?.Invoke(refMsg);
        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        public void InitControls()
        {
            foreach (KeyValuePair<string, string> kvp in DictryWhere)
            {
                if (StartURL.Contains(kvp.Key))
                {
                    StartURL = StartURL.Replace("[" + kvp.Key + "]", kvp.Value);
                }
            }
            switch (WebBrowserType)
            {
                case WebBrowserType.Default:
                    if (WebBrowserHelper.GetDefaultWebBrowserName() == "iexplore.exe")
                    {
                        WebBrowser defaultWebBrowser = new WebBrowser();
                        defaultWebBrowser.DocumentCompleted += DefaultWebBrowser_DocumentCompleted;
                        this.Controls.Add(defaultWebBrowser);
                        defaultWebBrowser.Dock = DockStyle.Fill;
                        defaultWebBrowser.Url = new Uri(StartURL);
                    }
                    else
                    {
                        CefWindowInfo cwi0 = CefWindowInfo.Create();
                        cwi0.SetAsChild(this.Handle, new CefRectangle(8, 8, this.Width - 13, this.Height - 13));
                        if (!string.IsNullOrEmpty(StartURL))
                            CefBrowserHost.CreateBrowser(cwi0, new BrowserClient(), new CefBrowserSettings(), StartURL);
                    }
                    break;
                case WebBrowserType.Chrome:
                    CefWindowInfo cwi1 = CefWindowInfo.Create();
                    cwi1.SetAsChild(this.Handle, new CefRectangle(8, 8, this.Width - 13, this.Height - 13));
                    if (!string.IsNullOrEmpty(StartURL))
                        CefBrowserHost.CreateBrowser(cwi1, new BrowserClient(), new CefBrowserSettings(), StartURL);
                    break;
                case WebBrowserType.IE:
                    WebBrowser webBrowser = new WebBrowser();
                    webBrowser.DocumentCompleted += DefaultWebBrowser_DocumentCompleted;
                    this.Controls.Add(webBrowser);
                    webBrowser.Dock = DockStyle.Fill;
                    webBrowser.Url = new Uri(StartURL);
                    break;
                default:
                    CefWindowInfo cwi2 = CefWindowInfo.Create();
                    cwi2.SetAsChild(this.Handle, new CefRectangle(8, 8, this.Width - 13, this.Height - 13));
                    if (!string.IsNullOrEmpty(StartURL))
                        CefBrowserHost.CreateBrowser(cwi2, new BrowserClient(), new CefBrowserSettings(), StartURL);
                    break;
            }

            //在DBA用户下弹出网站地址提示
            if (HISClientHelper.USERID == "DBA")
            {
                MessageBox.Show(StartURL);
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediWebFromControl_Load(object sender, EventArgs e)
        {
            //InitControls();
        }

        /// <summary>
        /// 当WebBrowser控件完成文档加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
        }

        /// <summary>
        /// Html错误异常监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // 忽略该错误并抑制错误对话框 
            e.Handled = true;
        }
    }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    public enum WebBrowserType
    {
        /// <summary>
        /// default
        /// </summary>
        Default = 0,
        /// <summary>
        /// IE
        /// </summary>
        IE = 1,
        /// <summary>
        /// googlebrowser
        /// </summary>
        Chrome = 2,

    }
}
