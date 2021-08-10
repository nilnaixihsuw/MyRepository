using System.Drawing;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 异步等待控件类
    /// </summary>
    public static class AsynWaitForm
    {
        /// <summary>
        /// 全局静态实例
        /// </summary>
        public static DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager = null;

        /// <summary>
        /// 创建等待控件(MediForm)
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="description">描述</param>
        /// <param name="parentform">父窗体</param>
        public static void CreateWaitForm(MediForm parentform, string caption, string description)
        {
            //if (splashScreenManager==null)
            //{
            //    splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            //    //splashScreenManager.ClosingDelay = 500;
            //    //splashScreenManager.ShowWaitForm();
            //    //splashScreenManager.SetWaitFormCaption(caption);
            //    //splashScreenManager.SetWaitFormDescription(description);
            //}
            //else
            //{
            //    splashScreenManager.CloseWaitForm();
            //}

            if (splashScreenManager != null)    // 修改模式,增加强壮性,每次create前都需要先清空内存,关闭窗口
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
                splashScreenManager.Dispose();

            }
            splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            splashScreenManager.ClosingDelay = 500;
            splashScreenManager.ShowWaitForm();
            splashScreenManager.SetWaitFormCaption(caption);
            splashScreenManager.SetWaitFormDescription(description);
        }

        /// <summary>
        /// 创建等待控件(XtraBaseForm)
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="description">描述</param>
        /// <param name="parentform">父窗体</param>
        public static void CreateWaitForm(DevExpress.XtraEditors.XtraForm parentform, string caption, string description)
        {
            //if (splashScreenManager == null)
            //{
            //    splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            //    splashScreenManager.ClosingDelay = 500;
            //    splashScreenManager.ShowWaitForm();
            //    splashScreenManager.SetWaitFormCaption(caption);
            //    splashScreenManager.SetWaitFormDescription(description);
            //}
            //else
            //{
            //    splashScreenManager.ClosingDelay = 500;
            //    splashScreenManager.ShowWaitForm();
            //    splashScreenManager.SetWaitFormCaption(caption);
            //    splashScreenManager.SetWaitFormDescription(description);
            //}

            if (splashScreenManager != null)    // 修改模式,增加强壮性,每次create前都需要先清空内存,关闭窗口
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
                splashScreenManager.Dispose();

            }
            splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            splashScreenManager.ClosingDelay = 500;
            splashScreenManager.ShowWaitForm();
            splashScreenManager.SetWaitFormCaption(caption);
            splashScreenManager.SetWaitFormDescription(description);
        }

        /// <summary>
        /// 创建等待控件(XtraBaseForm)
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="description">描述</param>
        /// <param name="parentform">父窗体</param>
        public static void CreateWaitForm(DevExpress.XtraBars.Ribbon.RibbonForm parentform, string caption, string description)
        {
            //if (splashScreenManager == null)
            //{
            //    splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            //    splashScreenManager.ClosingDelay = 500;
            //    splashScreenManager.ShowWaitForm();
            //    splashScreenManager.SetWaitFormCaption(caption);
            //    splashScreenManager.SetWaitFormDescription(description);
            //}
            //else
            //{
            //    splashScreenManager.ClosingDelay = 500;
            //    splashScreenManager.ShowWaitForm();
            //    splashScreenManager.SetWaitFormCaption(caption);
            //    splashScreenManager.SetWaitFormDescription(description);
            //}

            if (splashScreenManager != null)    // 修改模式,增加强壮性,每次create前都需要先清空内存,关闭窗口
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
                splashScreenManager.Dispose();
            }
            splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            splashScreenManager.ClosingDelay = 500;
            splashScreenManager.ShowWaitForm();
            splashScreenManager.SetWaitFormCaption(caption);
            splashScreenManager.SetWaitFormDescription(description);
        }
        /// <summary>
        /// 创建等待控件(MediForm)
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="description">描述</param>
        /// <param name="parentform">父窗体</param>
        ///<param name="point">自定义坐标位置</param>
        public static void CreateWaitForm(MediForm parentform, string caption, string description, Point point)
        {
            //if (splashScreenManager==null)
            //{
            //    splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true);
            //    //splashScreenManager.ClosingDelay = 500;
            //    //splashScreenManager.ShowWaitForm();
            //    //splashScreenManager.SetWaitFormCaption(caption);
            //    //splashScreenManager.SetWaitFormDescription(description);
            //}
            //else
            //{
            //    splashScreenManager.CloseWaitForm();
            //}

            if (splashScreenManager != null)    // 修改模式,增加强壮性,每次create前都需要先清空内存,关闭窗口
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
                splashScreenManager.Dispose();

            }                                                                        //(Form parentForm, Type splashFormType, bool useFadeIn, bool useFadeOut, SplashFormStartPosition startPos, Point location);
            splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(parentform, typeof(global::Mediinfo.WinForm.HIS.Controls.CustomProgressBarFrm), true, true, DevExpress.XtraSplashScreen.SplashFormStartPosition.Manual, point, false);
            splashScreenManager.ClosingDelay = 500;
            splashScreenManager.ShowWaitForm();
            splashScreenManager.SetWaitFormCaption(caption);
            splashScreenManager.SetWaitFormDescription(description);
        }
        /// <summary>
        /// 设置上传进度
        /// </summary>
        /// <param name="process"></param>
        public static void SetPercentProcess(string process)
        {
            splashScreenManager.SendCommand(WaitFormCommand.progressCount, process);
        }

        /// <summary>
        /// 设置百分数是否可见
        /// </summary>
        public static void SetPercentVisible(bool visible)
        {
            splashScreenManager.SendCommand(WaitFormCommand.IsVisiblePercent, visible);
        }

        /// <summary>
        /// 设置进度条内容
        /// </summary>
        /// <param name="Content"></param>
        public static void SetContent(string Content)
        {
            splashScreenManager.SendCommand(WaitFormCommand.progressContent, Content);
        }

        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public static void CloseWaitForm()
        {
            if (splashScreenManager != null && splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
        }

        /// <summary>
        /// 命令枚举
        /// </summary>
        public enum WaitFormCommand
        {
            /// <summary>
            /// 进度数
            /// </summary>
            progressCount = 0,
            /// <summary>
            /// 百分比是否可见
            /// </summary>
            IsVisiblePercent = 1,
            /// <summary>
            /// 进度类容设置
            /// </summary>
            progressContent = 2
        }
    }
}
