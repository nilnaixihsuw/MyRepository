using Autofac;

using DevExpress.XtraEditors;

using Mediinfo.Enterprise.Log;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 窗口外部调用定位器
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public class ButtonForOpenChuangKouInit : IServiceProvider
    {
        private readonly IContainer _container;

        private static readonly ButtonForOpenChuangKouInit _Logininstance = new ButtonForOpenChuangKouInit();

        /// <summary>
        /// 按钮打开窗体
        /// </summary>
        public static Dictionary<string, dynamic> buttonOpenFrm = new Dictionary<string, dynamic>();

        /// <summary>
        /// 单例构造函数
        /// </summary>
        private ButtonForOpenChuangKouInit()
        {
            var builder = new ContainerBuilder();
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = TheFolder.GetFiles().Where(c => c.Extension.Contains("dll") && c.Name.Contains("Mediinfo.WinForm"));
            if (!ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys.ContainsKey(assemblyPath)) ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys.TryAdd(assemblyPath, new ConcurrentDictionary<string, KeyValuePair<string, Assembly>>());
            List<Assembly> assemblys = new List<Assembly>();
            // 遍历文件
            foreach (FileInfo NextDll in mediDlls)
            {
                try
                {
                    var ass = Assembly.LoadFrom(NextDll.FullName);
                    assemblys.Add(ass);
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder("装载DLL文件类型异常 : ");
                    if (ex is ReflectionTypeLoadException reflectionTypeLoadException)
                    {
                        foreach (var exception in reflectionTypeLoadException.LoaderExceptions)
                        {
                            sb.Append(exception.Message);
                            sb.Append(Environment.NewLine);
                        }
                    }
                    LogHelper.Intance.Error("系统日志", "装载DLL文件类型异常", "异常原因:" + sb.ToString());
                }
               
            }
            //assemblys.ForEach(a =>
            foreach (var a in assemblys)
            {
                try
                {
                    var _types = a.GetTypes();
                }
                catch (Exception ex)
                {
                    if (a.FullName.Contains("Mediinfo.WinForm.XNZXT"))
                    {                        
                        StringBuilder sb = new StringBuilder("装载DLL文件类型异常 : ");
                        if (ex is ReflectionTypeLoadException reflectionTypeLoadException)
                        {
                            foreach (var exception in reflectionTypeLoadException.LoaderExceptions)
                            {
                                sb.Append(exception.Message);
                                sb.Append(Environment.NewLine);
                            }
                        }
                        LogHelper.Intance.Error("系统日志", "装载DLL文件类型异常", "异常原因:" + sb.ToString());
                        continue;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder("装载DLL文件类型异常 : ");
                        if (ex is ReflectionTypeLoadException reflectionTypeLoadException)
                        {
                            foreach (var exception in reflectionTypeLoadException.LoaderExceptions)
                            {
                                sb.Append(exception.Message);
                                sb.Append(Environment.NewLine);
                            }
                        }
                        LogHelper.Intance.Error("系统日志", "装载DLL文件类型异常", "异常原因:" + sb.ToString());
                    }
                }
            }
            //});
            foreach (FileInfo NextDll in mediDlls)
            {
                
                try
                {
                    var ass = Assembly.LoadFrom(NextDll.FullName);
                    ass.GetTypes().ToList().Where(p => p.BaseType == typeof(Form) || GetBaseType(p)).ToList().ForEach(p =>
                    {
                        try
                        {
                            if (p.Name.ToUpper() != "MAINFORM")
                            {
                                if (!ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys[assemblyPath].ContainsKey(p.Name.ToUpper()))
                                {
                                    ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys[assemblyPath].TryAdd(p.Name.ToUpper(), new KeyValuePair<string, Assembly>(p.FullName, ass));
                                }
                                else
                                {
                                    string fullName = ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys[assemblyPath][p.Name.ToUpper()].Value.FullName;
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MediMsgBox.Warn(Login.MediForm, NextDll.FullName + "文件夹有的DLL中有重名的类" + ex.ToString());
                        }
                    });
                }
                catch (Exception ex)
                {
                    if (NextDll.FullName.Contains("Mediinfo.WinForm.XNZXT"))
                        continue;
                    else
                    {
                        StringBuilder sb = new StringBuilder("装载DLL文件类型异常 : ");
                        if (ex is ReflectionTypeLoadException reflectionTypeLoadException)
                        {
                            foreach (var exception in reflectionTypeLoadException.LoaderExceptions)
                            {
                                sb.Append(exception.Message);
                                sb.Append(Environment.NewLine);
                            }
                        }
                    }
                }
            }
        }

        private bool GetBaseType(Type type)
        {
            if (type.BaseType == null)
            {
                return false;
            }
            else
            {
                if (type.BaseType == typeof(XtraForm))
                {
                    return true;
                }
                else
                {
                    return GetBaseType(type.BaseType);
                }
            }
        }

        public static ButtonForOpenChuangKouInit LoginInstance
        {
            get { return _Logininstance; }
        }

        #region Public Methods

        /// <summary>
        /// 注入所有服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        #endregion
    }
}
