using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.Core;
using Mediinfo.Utility;
using Mediinfo.Utility.Util;

using System;
using System.Drawing;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediWebFormBase : MediUniversalMFBase
    {
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case ImportFromDLL.WM_COPYDATA:
                    ImportFromDLL.COPYDATASTRUCT copyData = (ImportFromDLL.COPYDATASTRUCT)m.GetLParam(typeof(ImportFromDLL.COPYDATASTRUCT));//获取数据
                    switch (copyData.lpData)
                    {
                        case "loaded":
                            this.mediProgressPanel1.Hide();
                            break;
                        case "close":
                            base.Close();
                            break;
                        case "min":
                            this.WindowState = FormWindowState.Minimized;
                            break;
                        case "max":
                            this.WindowState = FormWindowState.Maximized;
                            cwi.SetAsChild(this.mediPanelControl1.Handle, new CefRectangle(8, 8, this.mediPanelControl1.Width - 13, this.mediPanelControl1.Height - 13));
                            break;
                        case "normal":
                            this.WindowState = FormWindowState.Normal;
                            break;
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        protected virtual string StartURL
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "hrp.html";
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;
                return cp;
            }
        }

        public MediWebFormBase()
        {
            InitializeComponent();
        }

        private CefWindowInfo cwi = null;

        private void MediWebFormBase_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            this.BackColor = Color.White;
            string token = TokenLocator.Instance.GetToken();
            ServiceContext context = ContextLocator.Instance.GetServiceContext();
            IOHelper.DeleteFile(AppDomain.CurrentDomain.BaseDirectory + "webauthdata");
            IOHelper.CreateFile(AppDomain.CurrentDomain.BaseDirectory + "webauthdata");
            IOHelper.Write(AppDomain.CurrentDomain.BaseDirectory + "webauthdata", DESHelper.Encrypt(token + "@#mediinfo#@" + JsonUtil.SerializeObject(context), "@MEDIHRP"));

            cwi = CefWindowInfo.Create();
            cwi.SetAsChild(this.mediPanelControl1.Handle, new CefRectangle(8, 8, this.mediPanelControl1.Width - 13, this.mediPanelControl1.Height - 13));
            var bc = new BrowserClient();
            var bs = new CefBrowserSettings() { };
            CefBrowserHost.CreateBrowser(cwi, bc, bs, StartURL);
        }
    }
}
