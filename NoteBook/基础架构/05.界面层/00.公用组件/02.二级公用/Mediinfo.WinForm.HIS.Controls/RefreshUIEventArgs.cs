using System;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 窗口数据刷新参数类
    /// </summary>
    public class RefreshUIEventArgs: EventArgs
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public object ReturnValue { get; set; }
        /// <summary>
        /// 返回结果状态
        /// </summary>
        public RefreshRuslt ResultState { get; set; }
    }

    /// <summary>
    /// 刷新结果状态
    /// </summary>
    public enum RefreshRuslt
    {
        SUCCESS = 0,
        FAILURE = 1,
        OTHER = 2
    }
}
