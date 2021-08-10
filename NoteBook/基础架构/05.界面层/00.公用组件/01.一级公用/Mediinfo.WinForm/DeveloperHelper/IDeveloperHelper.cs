using System.ComponentModel;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 开发者帮助接口
    /// </summary>
    public interface IDeveloperHelper
    {
        /// <summary>
        /// 相关控件处理类(包括窗体)
        /// </summary>
        /// <param name="controlOrComponent">控件或者组件</param>
        void DealRelativeControl(Component controlOrComponent);
    }
}