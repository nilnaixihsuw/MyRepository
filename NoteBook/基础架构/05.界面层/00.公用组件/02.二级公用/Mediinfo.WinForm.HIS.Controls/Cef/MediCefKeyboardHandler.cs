using System;

using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediCefKeyboardHandler : CefKeyboardHandler
    {
        protected override bool OnKeyEvent(CefBrowser browser, CefKeyEvent keyEvent, IntPtr osEvent)
        {
            // 对按键的处理
            switch (keyEvent.WindowsKeyCode)
            {
                case 123:           // 功能键 F12 的KeyCode，弹出开发者工具界面
                    if (keyEvent.EventType.Equals(CefKeyEventType.RawKeyDown))
                    {
                        string devToolsUrl = browser.GetHost().GetDevToolsUrl(true);
                        var frame = browser.GetMainFrame();
                        frame.ExecuteJavaScript(string.Format("window.open('{0}');", devToolsUrl), "about:blank", 0);
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
