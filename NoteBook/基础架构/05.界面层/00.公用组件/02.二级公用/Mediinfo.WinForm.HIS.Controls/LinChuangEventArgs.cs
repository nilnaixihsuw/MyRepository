using System;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class LinChuangEventArgs : EventArgs
    {
        /// <summary>
        /// 业务参数对象
        /// </summary>
        public object LinChuangArgs { get; set; }
    }

    /// <summary>
    /// 自定义头部按钮事件参数
    /// </summary>
    public class MediLCTabControlCustomHeaderControlEventArgs : EventArgs
    {

    }

    public class ButtonOpenWindowEventArgs : EventArgs
    {

    }
}
