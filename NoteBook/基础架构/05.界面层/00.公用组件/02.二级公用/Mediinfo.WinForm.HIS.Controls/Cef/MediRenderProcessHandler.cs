using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediRenderProcessHandler : CefRenderProcessHandler
    {
        public CefV8Handler Cef;

        /// <summary>
        /// 通过反射机制 注册c#函数到JS
        /// </summary>
        public void RegisterJs()
        {
            MediJsEvent js = new MediJsEvent();

            Cef = new MediAv8Handler(js);

            string javascriptCode = MediCefJavaScriptEx.CreateJsCodeByObject(js, "Cef");

            CefRuntime.RegisterExtension("Cef", javascriptCode, Cef);
        }


        protected override void OnWebKitInitialized()
        {
            // 注册JS函数
            RegisterJs();
        }
    }
}
