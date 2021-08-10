using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediCefDownloadHandler : CefDownloadHandler
    {
        protected override void OnBeforeDownload(CefBrowser browser, CefDownloadItem downloadItem, string suggestedName, CefBeforeDownloadCallback callback)
        {
            callback.Continue(string.Empty, true);
        }

        protected override void OnDownloadUpdated(CefBrowser browser, CefDownloadItem downloadItem, CefDownloadItemCallback callback)
        {
            if (downloadItem.IsComplete)
            {
                MediMsgBox.Show("下载成功");
                if (browser.IsPopup && !browser.HasDocument)
                {
                    browser.GetHost().ParentWindowWillClose();
                    browser.GetHost().CloseBrowser();
                }
            }
        }
    }
}
