using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediCefContextMenuHandler: CefContextMenuHandler
    {
        protected override void OnBeforeContextMenu(CefBrowser browser, CefFrame frame, CefContextMenuParams state, CefMenuModel model)
        {
            model.Clear();
        }
    }
}
