using Mediinfo.HIS.Core;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 非业务功能菜单打开窗体基类（此窗体不包含调用参数和功能ID）
    /// </summary>
    public partial class MediForm : DevExpress.XtraEditors.XtraForm
    {
        #region properties

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDeveloperHelper developerHelper { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediForm()
        {
            InitializeComponent();

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            else
            {
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("微软雅黑", 11);
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = new Font("微软雅黑", 11);
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = new Font("微软雅黑", 11);
            }
        }

        #endregion

        #region methods

        /// <summary>      
        /// 释放内存      
        /// </summary>      
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        #endregion

        #region virtual

        /// <summary>
        /// 获取客户端时间(包含时分秒)
        /// </summary>
        /// <returns>返回时间</returns>
        protected virtual DateTime GetSYSTime()
        {
            return HISClientHelper.clientDateTime;
        }

        /// <summary>
        /// 释放当前进程(程序退出或者登录退出)
        /// </summary>
        protected virtual void DisposeCurrentProcess()
        {
            // 进程线程资源释放
            Process process = Process.GetCurrentProcess();
            if (process != null)
            {
                KillProcess(process.Id);
            }
        }

        /// <summary>
        /// 根据进程ID关闭当前进程
        /// </summary>
        /// <param name="id">进程ID</param>
        private void KillProcess(int id)
        {
            /*
             * 说明：此处根据进程ID关闭进程是由于客户端是可以多次启动，
             *      如果用名称关闭进程时可能存在关闭多个进程的问题
             */
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.Id == id)
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();

                    break;
                }
            }
        }

        #endregion

        #region override

        //去掉这个函数，输入法信息在各自控件里面实现
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected override void OnActivated(EventArgs e)
        //{
        //    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
        //    {

        //    }
        //    else
        //    {
        //        int globalconversion = IMMModeHelper.globalconversion;
        //        int globalsentence = IMMModeHelper.globalsentence;
        //        //IntPtr prt = IMMModeHelper.ImmGetContext(CurrentControlParentFrm.Handle);
        //        //FileStream fileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "shurufa.txt", FileMode.Append);
        //        //StreamWriter sw = new StreamWriter(fileStream);
        //        //sw.WriteLine("------" + DateTime.Now + "---ImmGetConversionStatus---");
        //        //sw.WriteLine("------gotfocusstart---ImmGetConversionStatus---");
        //        //sw.WriteLine(IMMModeHelper.intPtr);
        //        //sw.WriteLine(globalconversion);
        //        //sw.WriteLine(globalsentence);

        //        //sw.WriteLine("------gotfocusend---ImmGetConversionStatus----");
        //        //sw.Flush();
        //        //sw.Close();
        //        IMMModeHelper.ImmSetConversionStatus(IMMModeHelper.intPtr, globalconversion, globalsentence);
        //    }
        //    base.OnActivated(e);
        //}

        #endregion

        #region events

        private void MediForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                FieldInfo[] subfi = ((DevExpress.XtraEditors.XtraForm)sender).GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                foreach (FieldInfo pFieldInfo in subfi)
                {
                    if (pFieldInfo.FieldType == typeof(BindingSource))
                    {
                        BindingSource bindingSource = pFieldInfo.GetValue((DevExpress.XtraEditors.XtraForm)sender) as System.Windows.Forms.BindingSource;
                        bindingSource.Dispose();
                    }
                    if (pFieldInfo.FieldType.BaseType == typeof(DevExpress.XtraGrid.Views.Grid.GridView))
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView gridview = pFieldInfo.GetValue((DevExpress.XtraEditors.XtraForm)sender) as DevExpress.XtraGrid.Views.Grid.GridView;
                        gridview.Dispose();
                    }

                    if (pFieldInfo.FieldType.BaseType == typeof(DevExpress.XtraGrid.GridControl))
                    {
                        DevExpress.XtraGrid.GridControl gridcontrol = pFieldInfo.GetValue((DevExpress.XtraEditors.XtraForm)sender) as DevExpress.XtraGrid.GridControl;
                        gridcontrol.Dispose();
                    }
                    //if (pFieldInfo.FieldType == typeof(DevExpress.XtraGrid.GridControl))
                    //{
                    //    DevExpress.XtraGrid.GridControl gridcontrol = pFieldInfo.GetValue((DevExpress.XtraEditors.XtraForm)sender) as DevExpress.XtraGrid.GridControl;
                    //    gridcontrol.Dispose();
                    //}
                    //if (pFieldInfo.FieldType == typeof(DevExpress.XtraGrid.Views.Grid.GridView))
                    //{
                    //    DevExpress.XtraGrid.Views.Grid.GridView gridview = pFieldInfo.GetValue((DevExpress.XtraEditors.XtraForm)sender) as DevExpress.XtraGrid.Views.Grid.GridView;
                    //    gridview.Dispose();
                    //}
                }
            }
        }

        #endregion

        #region extern

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        #endregion
    }
}