using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 系统信息处理类
    /// </summary>
    public class SystemInfoHelper : IDeveloperHelper
    {
        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 控件类名称
        /// </summary>
        public string ControlClassName { get; set; }

        /// <summary>
        /// 控件命名控件
        /// </summary>
        public string ControlNameSpace { get; set; }

        /// <summary>
        /// 控件程序集名称
        /// </summary>
        public string ControlAssemblyName { get; set; }

        /// <summary>
        /// 控件所在窗体名称
        /// </summary>
        public string ControlForFormName { get; set; }

        /// <summary>
        /// 控件所在类名称
        /// </summary>
        public string ControlFormClassName { get; set; }

        /// <summary>
        /// 控件窗体所在命名空间
        /// </summary>
        public string ControlFormNameSpace { get; set; }

        /// <summary>
        /// 控件所在窗体程序集名称
        /// </summary>
        public string ControlFormAssemblyName { get; set; }

        /// <summary>
        /// 控件所在窗体应用ID
        /// </summary>
        public string ControlFromYingYongID { get; set; }

        /// <summary>
        /// 控件所在窗体功能ID
        /// </summary>
        public string ControlFormGongNengID { get; set; }

        /// <summary>
        /// 控件窗体调用参数
        /// </summary>
        public string ControlFormDYCS { get; set; }

        /// <summary>
        /// 当前系统连接数据库信息
        /// </summary>
        public string CurrentSystemDBConnStr { get; set; }

        /// <summary>
        /// 当前控件得父窗口
        /// </summary>
        public Form CurrentControlParentFrm { get; set; }

        /// <summary>
        /// 当前窗口所使用得参数
        /// </summary>
        public string CurrentFormUsedParam { get; set; } = null;
        private System.Windows.Forms.Timer inputMethodTimer;
        private System.ComponentModel.IContainer components = null;
        private GridLookUpEdit currentGridLookUpEdit;
        /// <summary>
        /// 控件处理
        /// </summary>
        /// <param name="controlOrComponent"></param>
        public void DealRelativeControl(Component controlOrComponent)
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                try
                {
                    if (controlOrComponent is ColumnView gridviewBase)
                    {
                        gridviewBase.MouseDown -= GridviewBase_MouseDown;
                        gridviewBase.MouseDown += GridviewBase_MouseDown;
                    }
                    else if (controlOrComponent is Control controlBase)
                    {
                        controlBase.MouseDown -= ControlBase_MouseDown;
                        controlBase.MouseDown += ControlBase_MouseDown;
                        if (controlBase is TextEdit baseEdit)
                        {
                            baseEdit.GotFocus -= BaseEdit_GotFocus;
                            baseEdit.GotFocus += BaseEdit_GotFocus;
                            baseEdit.LostFocus -= BaseEdit_LostFocus;
                            baseEdit.LostFocus += BaseEdit_LostFocus;
                            CurrentControlParentFrm = baseEdit.FindForm();
                        }
                    }
                    else if (controlOrComponent is RepositoryItemTextEdit repositoryItemTextEditBase)
                    {
                        repositoryItemTextEditBase.MouseDown -= RepositoryItemTextEditBase_MouseDown;
                        repositoryItemTextEditBase.MouseDown += RepositoryItemTextEditBase_MouseDown;
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }
        }


        /// <summary>
        /// 失去光标时记下当前输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseEdit_LostFocus(object sender, EventArgs e)
        {
            SetInputMethod(sender);
            if (sender is GridLookUpEdit&& inputMethodTimer != null)
            {
                inputMethodTimer.Stop();
                inputMethodTimer.Dispose();
                inputMethodTimer = null;
            }
        }
        /// <summary>
        /// 获取输入法
        /// </summary>
        /// <param name="sender"></param>
        public void GetInputMethod(object sender)
        {
            if (!(sender is IInputIMEMode)) return;
            IInputIMEMode editor = (IInputIMEMode)sender;
            if (editor.MediinfoIMEMode == MediInfoImeMode.CHS)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                   
                }
                else
                {
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    if (sender is GridLookUpEdit gridLookUpEdit)
                    {
                        currentGridLookUpEdit = gridLookUpEdit;
                        this.components = new System.ComponentModel.Container();
                        this.inputMethodTimer = new System.Windows.Forms.Timer(this.components);
                        inputMethodTimer.Tick += InputMethodTimer_Tick;
                        inputMethodTimer.Interval = 200;
                        inputMethodTimer.Start();
                    }
                }
            }
            else if (editor.MediinfoIMEMode == MediInfoImeMode.EN)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                   
                }
                else
                {
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    if (sender is GridLookUpEdit gridLookUpEdit)
                    {
                        currentGridLookUpEdit = gridLookUpEdit;
                        this.components = new System.ComponentModel.Container();
                        this.inputMethodTimer = new System.Windows.Forms.Timer(this.components);
                        inputMethodTimer.Tick += InputMethodTimer_Tick;
                        inputMethodTimer.Interval = 200;
                        inputMethodTimer.Start();
                    }
                }
            }
            else
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                  
                }
                else
                {
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    if (sender is GridLookUpEdit gridLookUpEdit)
                    {
                        currentGridLookUpEdit = gridLookUpEdit;
                        this.components = new System.ComponentModel.Container();
                        this.inputMethodTimer = new System.Windows.Forms.Timer(this.components);
                        inputMethodTimer.Tick += InputMethodTimer_Tick;
                        inputMethodTimer.Interval = 200;
                        inputMethodTimer.Start();
                    }
                }
            }
        }
        /// <summary>
        /// 通过定时器设置输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputMethodTimer_Tick(object sender, EventArgs e)
        {
            if (currentGridLookUpEdit == null || currentGridLookUpEdit.IsDisposed || !currentGridLookUpEdit.Focused) return;
            if (OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows8 && OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows10)
            {
                int globalconversion = 0;
                int globalsentence = 0;
                IntPtr prt = IMMModeHelper.ImmGetContext(currentGridLookUpEdit.Handle);
                IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                IMMModeHelper.globalconversion = globalconversion;
                IMMModeHelper.globalsentence = globalsentence;
            }
        }

        /// <summary>
        /// 设置输入法
        /// </summary>
        /// <param name="sender"></param>
        public void SetInputMethod(object sender)
        {
            if (!(sender is IInputIMEMode)) return;
            IInputIMEMode editor = (IInputIMEMode)sender;
            if (editor.MediinfoIMEMode == MediInfoImeMode.CHS)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                 
                }
                else
                {
                    int globalconversion = 0;
                    int globalsentence = 0;
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                    IMMModeHelper.globalconversion = globalconversion;
                    IMMModeHelper.globalsentence = globalsentence;
                }
            }
            else if (editor.MediinfoIMEMode == MediInfoImeMode.EN)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                    
                }
                else
                {
                    int globalconversion = 0;
                    int globalsentence = 0;
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                    IMMModeHelper.globalconversion = globalconversion;
                    IMMModeHelper.globalsentence = globalsentence;
                }
            }
            else
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                    
                }
                else
                {
                    int globalconversion = 0;
                    int globalsentence = 0;
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                    IMMModeHelper.globalconversion = globalconversion;
                    IMMModeHelper.globalsentence = globalsentence;
                }
            }
        }


        /// <summary>
        /// 获得光标时设置输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseEdit_GotFocus(object sender, EventArgs e)
        {
            GetInputMethod(sender);
          
        }     

        private void RepositoryItemTextEditBase_MouseDown(object sender, MouseEventArgs e)
        {
            InitDangQianCK(sender);
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Right)
            {
                if (sender is Control controlBase)
                {
                    CurrentControlParentFrm = controlBase.FindForm();

                    ControlName = controlBase.Name;
                    ControlClassName = controlBase.GetType().ToString();
                    ControlNameSpace = controlBase.GetType().Namespace;
                    ControlAssemblyName = controlBase.GetType().Assembly.GetName().FullName;
                    CurrentControlParentFrm = controlBase.FindForm();

                    ControlForFormName = CurrentControlParentFrm == null ? "" : controlBase.FindForm().Name;
                    ControlFormClassName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().ToString();
                    ControlFormNameSpace = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Namespace;
                    ControlFormAssemblyName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Assembly.FullName;

                    if (CurrentControlParentFrm != null)
                    {
                        Type formType = CurrentControlParentFrm.GetType();
                        System.Reflection.PropertyInfo gongNengpropertyInfo = formType.GetProperty("GongNengId");
                        if (gongNengpropertyInfo != null)
                        {
                            ControlFormGongNengID = gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }

                        System.Reflection.PropertyInfo diaoYongCSpropertyInfo = formType.GetProperty("DiaoYongCS");
                        if (diaoYongCSpropertyInfo != null)
                        {
                            ControlFormDYCS = diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }
                    }
                    ShowInfo();
                }
                else if (sender is RepositoryItemTextEdit repositoryItemTextEdit)
                {
                    CurrentControlParentFrm = repositoryItemTextEdit.OwnerEdit.FindForm();
                    ControlName = repositoryItemTextEdit.Name;
                    ControlClassName = repositoryItemTextEdit.GetType().ToString();
                    ControlNameSpace = repositoryItemTextEdit.GetType().Namespace;
                    ControlAssemblyName = repositoryItemTextEdit.GetType().Assembly.GetName().FullName;

                    ControlForFormName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.Name;
                    ControlFormClassName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().ToString();
                    ControlFormNameSpace = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Namespace;
                    ControlFormAssemblyName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Assembly.FullName;
                    if (CurrentControlParentFrm != null)
                    {
                        Type formType = CurrentControlParentFrm.GetType();
                        System.Reflection.PropertyInfo gongNengpropertyInfo = formType.GetProperty("GongNengId");
                        if (gongNengpropertyInfo != null)
                        {
                            ControlFormGongNengID = gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }

                        System.Reflection.PropertyInfo diaoYongCSpropertyInfo = formType.GetProperty("DiaoYongCS");
                        if (diaoYongCSpropertyInfo != null)
                        {
                            ControlFormDYCS = diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }
                    }
                    ShowInfo();
                }
            }
        }

        /// <summary>
        /// 展示信息
        /// </summary>
        private void ShowInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("当前控件信息:");
            sb.AppendLine("控件名称：" + ControlName);
            sb.AppendLine("控件类名称：" + ControlClassName);
            sb.AppendLine("控件命名空间：" + ControlNameSpace);
            if (ControlAssemblyName.Contains(","))
            {
                int firstdh = ControlAssemblyName.IndexOf(',');
                sb.AppendLine("控件程序集名称：" + ControlAssemblyName.Remove(firstdh));
            }
            else
            {
                sb.AppendLine("控件程序集名称：" + ControlAssemblyName);
            }
            //sb.AppendLine("控件程序集名称：" + ControlAssemblyName);
            sb.AppendLine("");
            sb.AppendLine("当前控件窗体信息:");
            sb.AppendLine("控件窗体名称：" + ControlForFormName);
            sb.AppendLine("控件窗体类名称: " + ControlFormClassName);
            sb.AppendLine("控件窗体命名空间：" + ControlFormNameSpace);
            if (ControlFormAssemblyName.Contains(","))
            {
                int firstdh = ControlFormAssemblyName.IndexOf(',');
                sb.AppendLine("控件窗体程序集名称：" + ControlFormAssemblyName.Remove(firstdh));
            }
            else
            {
                sb.AppendLine("控件窗体程序集名称：" + ControlFormAssemblyName);
            }

            sb.AppendLine("控件窗体应用ID：" + HISClientHelper.YINGYONGID);
            sb.AppendLine("控件窗体功能ID：" + ControlFormGongNengID);
            sb.AppendLine("控件窗体调用参数：" + ControlFormDYCS);
            sb.AppendLine("控件窗体所使用的参数集：" + CurrentFormUsedParam);
            using (DevHelperFrm devHelperFrm = new DevHelperFrm(sb.ToString(), ControlName, ControlForFormName))
            {
                devHelperFrm.ShowDialog();
            }
        }

        private void ControlBase_MouseDown(object sender, MouseEventArgs e)
        {
            InitDangQianCK(sender);
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Right)
            {
                if (sender is Control controlBase)
                {
                    CurrentControlParentFrm = controlBase.FindForm();

                    ControlName = controlBase.Name;
                    ControlClassName = controlBase.GetType().ToString();
                    ControlNameSpace = controlBase.GetType().Namespace;
                    ControlAssemblyName = controlBase.GetType().Assembly.GetName().FullName;
                    CurrentControlParentFrm = controlBase.FindForm();

                    ControlForFormName = CurrentControlParentFrm == null ? "" : controlBase.FindForm()?.Name;
                    ControlFormClassName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().ToString();
                    ControlFormNameSpace = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Namespace;
                    ControlFormAssemblyName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Assembly.FullName;
                    if (CurrentControlParentFrm != null)
                    {
                        Type formType = CurrentControlParentFrm.GetType();
                        System.Reflection.PropertyInfo gongNengpropertyInfo = formType.GetProperty("GongNengId");
                        if (gongNengpropertyInfo != null)
                        {
                            ControlFormGongNengID = gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }

                        System.Reflection.PropertyInfo diaoYongCSpropertyInfo = formType.GetProperty("DiaoYongCS");
                        if (diaoYongCSpropertyInfo != null)
                        {
                            ControlFormDYCS = diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }

                        List<string> zuoYongYuList = new List<string> { ControlForFormName };
                        var list = new ServiceClient("JCJG-GongYong", "V1").Invoke<List<E_GY_CANSHU_ZUOYONGYU>>("JCJGCanSu", "GetWindowParamByZuoYongYu", new ServiceParm("zuoYongYuList", zuoYongYuList)).Return;
                        if(list!=null && list.Count > 0){
                            CurrentFormUsedParam = list.Aggregate("", (current, t) => current + "," + t.CANSHUID);
                        }
                        
                    }

                    ShowInfo();
                }
            }
        }

        private void GridviewBase_MouseDown(object sender, MouseEventArgs e)
        {
            InitDangQianCK(sender);
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Right)
            {
                if (sender is ColumnView gridviewBase)
                {
                    ControlName = gridviewBase.Name;
                    ControlClassName = gridviewBase.GetType().ToString();
                    ControlNameSpace = gridviewBase.GetType().Namespace;
                    ControlAssemblyName = gridviewBase.GetType().Assembly.GetName().FullName;
                    CurrentControlParentFrm = gridviewBase.GridControl.FindForm();

                    ControlForFormName = CurrentControlParentFrm == null ? "" : gridviewBase.GridControl.FindForm().Name;
                    ControlFormClassName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().ToString();
                    ControlFormNameSpace = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Namespace;
                    ControlFormAssemblyName = CurrentControlParentFrm == null ? "" : CurrentControlParentFrm.GetType().Assembly.FullName;
                    if (CurrentControlParentFrm != null)
                    {
                        Type formType = CurrentControlParentFrm.GetType();
                        System.Reflection.PropertyInfo gongNengpropertyInfo = formType.GetProperty("GongNengId");
                        if (gongNengpropertyInfo != null)
                        {
                            ControlFormGongNengID = gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : gongNengpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }

                        System.Reflection.PropertyInfo diaoYongCSpropertyInfo = formType.GetProperty("DiaoYongCS");
                        if (diaoYongCSpropertyInfo != null)
                        {
                            ControlFormDYCS = diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null) == null ? "" : diaoYongCSpropertyInfo.GetValue(CurrentControlParentFrm, null).ToString();
                        }
                    }
                    ShowInfo();
                }
            }
        }
        /// <summary>
        /// 初始化当前窗口
        /// </summary>
        private void InitDangQianCK(object sender)
        {
            Form controlForm;
            if (sender is Control controlBase)
            {
                controlForm = controlBase.FindForm();
                if (controlForm != null) HISClientHelper.DANGQIANCKMC = controlForm.Name;
            }
            else if (sender is RepositoryItemTextEdit repositoryItemTextEdit)
            {
                controlForm = repositoryItemTextEdit.OwnerEdit.FindForm();
                if (controlForm != null) HISClientHelper.DANGQIANCKMC = controlForm.Name;
            }
            else if (sender is ColumnView columnView)
            {
                controlForm = columnView.GridControl.FindForm();
                if (controlForm != null) HISClientHelper.DANGQIANCKMC = controlForm.Name;
            }
        }
    }

    /// <summary>
    /// 输入法切换(winxp,win7,win10)
    /// </summary>
    public enum MediInfoImeMode
    {
        /// <summary>
        /// 默认值根据操作系统取输入法
        /// </summary>
        Default = 0,

        /// <summary>
        /// 中文输入法
        /// </summary>
        CHS = 1,

        /// <summary>
        /// 英文输入法
        /// </summary>
        EN = 2
    }

    /// <summary>
    /// 输入法全局帮助类
    /// </summary>
    public static class IMMModeHelper
    {
        public static IntPtr intPtr = IntPtr.Zero;
        /// <summary>
        /// 是否是搜狗输入法
        /// </summary>
        public static bool IsSouGouInputMethod = false;
        /// <summary>
        /// Pointer to a variable in which the function retrieves a combination of conversion mode values. For more information
        /// </summary>
        public static int globalconversion = 1025;

        /// <summary>
        /// Pointer to a variable in which the function retrieves a sentence mode value. For more information,
        /// </summary>
        public static int globalsentence = 0;

        /// <summary>
        ///获取当前窗口的输入法上下文
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        /// <summary>
        ///获取当前输入法的状态
        /// </summary>
        /// <param name="hIMC"></param>
        /// <param name="conversion"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC,
            ref int conversion, ref int sentence);
        /// <summary>
        /// 创建输入上下文
        /// </summary>
        /// <returns></returns>
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern int ImmCreateContext();

        /// <summary>
        ///
        /// </summary>
        /// <param name="hIMC"></param>
        /// <param name="conversion"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);
        /// <summary>
        /// 销毁上下文对象
        /// </summary>
        /// <param name="hIMC"></param>
        /// <returns></returns>
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmDestroyContext(IntPtr hIMC);
        /// <summary>
        /// 注册输入法
        /// </summary>
        /// <param name="mediForm"></param>
        public static void RegisterIMMMode(Form mediForm)
        {
            if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
            {

            }
            else
            {
                foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
                {
                    if (iL.LayoutName == "中文(简体) - 搜狗拼音输入法")
                    {
                        InputLanguage.CurrentInputLanguage = iL;
                        IsSouGouInputMethod = true;
                        break;
                    }
                }
                if (IsSouGouInputMethod)
                {
                    IntPtr prt = ImmGetContext(mediForm.Handle);
                    ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                }

            }
        }
    }

    /// <summary>
    /// 操作系统判断类
    /// </summary>
    public class OSHelper
    {
        /// <summary>
        /// 获取操作系统ID
        /// </summary>
        /// <returns></returns>
        public static System.PlatformID GetPlatformID()
        {
            //获取系统信息
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            //获取操作系统ID
            System.PlatformID platformID = osInfo.Platform;

            return platformID;
        }

        /// <summary>
        /// 获取主版本号
        /// </summary>
        /// <returns></returns>
        public static int GetVersionMajor()
        {
            // 获取系统信息
            OperatingSystem osInfo = Environment.OSVersion;

            // 获取主版本号
            int versionMajor = osInfo.Version.Major;

            return versionMajor;
        }

        /// <summary>
        /// 获取副版本号
        /// </summary>
        /// <returns></returns>
        public static int GetVersionMinor()
        {
            // 获取系统信息
            OperatingSystem osInfo = Environment.OSVersion;

            // 获取副版本号
            int versionMinor = osInfo.Version.Minor;

            return versionMinor;
        }

        /// <summary>
        /// 操作系统版本号
        /// </summary>
        public static OSVersionNo GetOSType()
        {
            Version currentVersion = Environment.OSVersion.Version;
            if ((Environment.OSVersion.Platform == PlatformID.Win32Windows) && (Environment.OSVersion.Version.Minor == 10) && (Environment.OSVersion.Version.Revision.ToString() != "2222A"))
            {
                return OSVersionNo.Windows98;
            }
            else if ((Environment.OSVersion.Platform == PlatformID.Win32Windows) && (Environment.OSVersion.Version.Minor == 10) && (Environment.OSVersion.Version.Revision.ToString() == "2222A"))
            {
                return OSVersionNo.Windows982;
            }
            else if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor == 0))
            {
                return OSVersionNo.Windows2000;
            }
            else if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor == 2))
            {
                return OSVersionNo.Windows2003;
            }
            else if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor == 1))
            {
                return OSVersionNo.WindowsXP;
            }
            else if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1))
            {
                return OSVersionNo.Windows7;
            }
            else if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 0))
            {
                return OSVersionNo.WindowsVista;
            }
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return OSVersionNo.Unix;
            }
            else
            {
                Version compareToVersion8 = new Version("6.2");
                if (currentVersion.CompareTo(compareToVersion8) >= 0 && currentVersion.CompareTo(compareToVersion8) < 3.8)
                    return OSVersionNo.Windows8;
                Version compareToVersion10 = new Version("10.0");
                if (currentVersion.CompareTo(compareToVersion10) >= 0)
                    return OSVersionNo.Windows8;
            }
            return OSVersionNo.Windows7;
        }

        /// <summary>
        /// 操作系统版本号枚举类
        /// </summary>
        public enum OSVersionNo
        {
            /// <summary>
            /// Windows98
            /// </summary>
            Windows98 = 0,

            /// <summary>
            /// Windows982
            /// </summary>
            Windows982 = 1,

            /// <summary>
            /// Windows2000
            /// </summary>
            Windows2000 = 2,

            /// <summary>
            /// WindowsXP
            /// </summary>
            WindowsXP = 3,

            /// <summary>
            /// Windows2003
            /// </summary>
            Windows2003 = 4,

            /// <summary>
            /// WindowsVista
            /// </summary>
            WindowsVista = 5,

            /// <summary>
            /// Windows7
            /// </summary>
            Windows7 = 6,

            /// <summary>
            /// Unix
            /// </summary>
            Unix = 7,

            /// <summary>
            /// Windows8
            /// </summary>
            Windows8 = 8,

            /// <summary>
            /// Windows10
            /// </summary>
            Windows10 = 9,
        }
    }
}