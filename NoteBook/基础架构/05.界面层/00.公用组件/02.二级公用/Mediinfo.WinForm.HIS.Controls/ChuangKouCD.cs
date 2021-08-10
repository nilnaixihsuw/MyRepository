using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 窗口菜单相关属性类
    /// </summary>
    public class ChuangKouCD
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string CAIDANID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string CAIDANMC { get; set; }

        /// <summary>
        /// 窗口键
        /// </summary>
        public KeyValuePair<string, string> KEY { get; set; }

        /// <summary>
        /// 窗体
        /// </summary>
        public  MediForm XForm { get; set; }

        /// <summary>
        /// 是否是选择窗体
        /// </summary>
        public bool SELECTED { get; set; }
    }

    /// <summary>
    /// 按钮方式打开窗口类
    /// </summary>
    public static class ButtonForOpenChuangKou
    {
        /// <summary>
        /// 全局客户端主窗体(用于按钮打开窗体时调用打开窗口方法时用)
        /// </summary>
        public static MediUniversalMFBase GlobalClientMainForm { get; set; }
    }
    /// <summary>
    /// 登录窗体对象
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// 登录窗体
        /// </summary>
        public static MediForm MediForm { get; set; }
    }

    /// <summary>
    /// 异常主窗体类
    /// </summary>
    public static class GlobalExceptionParentForm
    {
        /// <summary>
        /// 异常主窗体
        /// </summary>
        public static MediForm MediForm { get; set; }
    }
}