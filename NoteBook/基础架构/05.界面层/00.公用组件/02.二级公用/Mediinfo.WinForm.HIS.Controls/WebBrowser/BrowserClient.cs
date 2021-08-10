using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class BrowserClient : CefClient
    {
        #region constructor

        public BrowserClient()
        {
            downloadHandler = new MediCefDownloadHandler();
        }

        #endregion

        #region fields

        private readonly CefDownloadHandler downloadHandler;

        #endregion

        #region override

        protected override CefDownloadHandler GetDownloadHandler()
        {
            return downloadHandler;
        }

        protected override CefKeyboardHandler GetKeyboardHandler()
        {
            return new MediCefKeyboardHandler();
        }

        //protected override CefContextMenuHandler GetContextMenuHandler()
        //{
        //    return new MediCefContextMenuHandler();
        //}

        //protected override CefLoadHandler GetLoadHandler()
        //{
        //    return base.GetLoadHandler();
        //}

        //protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        //{
        //    return base.OnProcessMessageReceived(browser, sourceProcess, message);
        //}

        #endregion
    }
}
