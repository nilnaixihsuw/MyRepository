using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediCefApp: CefApp
    {
        private CefRenderProcessHandler _renderProcessHandler = new MediRenderProcessHandler();

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {

        }

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }
    }
}
