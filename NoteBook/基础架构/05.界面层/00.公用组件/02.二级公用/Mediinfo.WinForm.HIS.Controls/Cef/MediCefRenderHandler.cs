using System;

using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediCefRenderHandler : CefRenderHandler
    {
        protected override bool GetScreenInfo(CefBrowser browser, CefScreenInfo screenInfo)
        {
            throw new NotImplementedException();
        }

        protected override void OnCursorChange(CefBrowser browser, IntPtr cursorHandle)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr buffer, int width, int height)
        {
            throw new NotImplementedException();
        }

        protected override void OnPopupSize(CefBrowser browser, CefRectangle rect)
        {
            throw new NotImplementedException();
        }

        protected override void OnScrollOffsetChanged(CefBrowser browser)
        {
            throw new NotImplementedException();
        }
    }
}
