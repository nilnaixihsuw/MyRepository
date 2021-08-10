using Autofac;
using Autofac.Core;
using Autofac.Features.OwnedInstances;

using DevExpress.XtraEditors;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 窗口外部调用定位器
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public class WinFormLocator : IServiceProvider
    {
        #region constructors

        /// <summary>
        /// 单例构造函数
        /// </summary>
        private WinFormLocator()
        {
            var builder = new ContainerBuilder();
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            List<Assembly> assemblys = new List<Assembly>();

            DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = TheFolder.GetFiles().Where(c => c.Extension.Contains("dll") && c.Name.Contains("Mediinfo.WinForm"));

            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (!ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys.ContainsKey(path))
            {
                ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys.TryAdd(path, new ConcurrentDictionary<string, KeyValuePair<string, Assembly>>());
            }

            mediDlls.AsParallel().WithDegreeOfParallelism(16).ForAll(d =>
            {
                var ass = Assembly.LoadFrom(d.FullName);
            });

            // 遍历文件
            foreach (FileInfo NextDll in mediDlls)
            {
                var ass = Assembly.LoadFrom(NextDll.FullName);
                assemblys.Add(ass);
            }

            var types = assemblys.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IFormInterface)))).ToArray();
            var impls = types.Where(t => !t.IsInterface).ToList();
            foreach (var impl in impls)
            {
                var services = impl.GetInterfaces().Where(m => m != typeof(IFormInterface));
                foreach (var service in services)
                {
                    builder.RegisterType(impl).As(service).InstancePerDependency();
                }
            }

            _container = builder.Build();
        }

        /// <summary>
        /// 单例构造函数
        /// </summary>
        private WinFormLocator(object args)
        {
            var builder = new ContainerBuilder();
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = TheFolder.GetFiles().Where(c => c.Extension.Contains("dll") && c.Name.Contains("Mediinfo.WinForm"));

            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (!ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys.ContainsKey(path))
            {
                ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys.TryAdd(path, new ConcurrentDictionary<string, KeyValuePair<string, Assembly>>());
            }

            mediDlls.AsParallel().WithDegreeOfParallelism(16).ForAll(d =>
            {
                var ass = Assembly.LoadFrom(d.FullName);
            });

            // 遍历文件
            foreach (FileInfo NextDll in mediDlls)
            {
                var ass = Assembly.LoadFrom(NextDll.FullName);

                ass.GetTypes().ToList().Where(p => p.BaseType == typeof(Form) || GetBaseType(p)).ToList().ForEach(p =>
                {
                    try
                    {
                        if (p.Name.ToUpper() != "MAINFORM")
                        {
                            if (!ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys[path].ContainsKey(p.Name.ToUpper()))
                            {
                                ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys[path].TryAdd(p.Name.ToUpper(), new KeyValuePair<string, Assembly>(p.FullName, ass));
                            }
                            else
                            {
                                string fullName = ButtonForOpenChuangKou.GlobalClientMainForm.Assemblys[path][p.Name.ToUpper()].Value.FullName;
                                //MediMsgBox.Warn(Login.MediForm, "窗体有重名,请联系管理员!!!\n窗体名称:" + p.Name.ToUpper() + "\n程序集名称:" + NextDll.Name + "\n程序集名称:" + fullName.Substring(0, fullName.IndexOf(',')) + ".dll");
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
        }

        #endregion

        #region fields

        private readonly IContainer _container;

        /// <summary>
        /// 按钮打开窗体
        /// </summary>
        public static Dictionary<string, dynamic> buttonOpenFrm = new Dictionary<string, dynamic>();

        #endregion

        #region properties

        public static WinFormLocator Instance { get; } = new WinFormLocator();

        public static WinFormLocator LoginInstance { get; } = new WinFormLocator(null);

        #endregion

        #region public methods

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

        #endregion

        #region public methods

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadWindow<T>()
        {
            var ownedT = _container.Resolve<Owned<T>>();
            //if (buttonOpenFrm.ContainsKey(ownedT.Value.GetType().FullName))
            //{
            //    return buttonOpenFrm[ownedT.Value.GetType().FullName];
            //}

            //buttonOpenFrm.Add(ownedT.Value.GetType().FullName, ownedT.Value);
            return ownedT.Value;
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterList"></param>
        /// <returns></returns>
        public T LoadWindow<T>(List<ResolvedParameter> parameterList)
        {
            if (parameterList == null || parameterList.Count <= 0)
            {
                var ownedT = _container.Resolve<Owned<T>>();
                //if (buttonOpenFrm.ContainsKey(ownedT.Value.GetType().FullName))
                //{
                //    return buttonOpenFrm[ownedT.Value.GetType().FullName];
                //}

                //buttonOpenFrm.Add(ownedT.Value.GetType().FullName, ownedT.Value);
                return ownedT.Value;
            }
            else
            {
                var ownedT = _container.Resolve<Owned<T>>(parameterList.ToArray());
                //if (buttonOpenFrm.ContainsKey(ownedT.Value.GetType().FullName))
                //{
                //    return buttonOpenFrm[ownedT.Value.GetType().FullName];
                //}

                //buttonOpenFrm.Add(ownedT.Value.GetType().FullName, ownedT.Value);
                return ownedT.Value;
            }
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T LoadWindow<T>(params object[] parameters)
        {
            if (parameters == null || parameters.Length <= 0)
            {
                var ownedT = _container.Resolve<Owned<T>>();
                //if (buttonOpenFrm.ContainsKey(ownedT.Value.GetType().FullName))
                //{
                //    return buttonOpenFrm[ownedT.Value.GetType().FullName];
                //}

                //buttonOpenFrm.Add(ownedT.Value.GetType().FullName, ownedT.Value);
                return ownedT.Value;
            }
            else
            {
                List<ResolvedParameter> paramList = new List<ResolvedParameter>();
                foreach (var item in parameters)
                {
                    paramList.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == item.GetType(), (pi, ctx) => item));
                }

                var ownedT = _container.Resolve<Owned<T>>(paramList.ToArray());
                //if (buttonOpenFrm.ContainsKey(ownedT.Value.GetType().FullName))
                //{
                //    return buttonOpenFrm[ownedT.Value.GetType().FullName];
                //}

                //buttonOpenFrm.Add(ownedT.Value.GetType().FullName, ownedT.Value);
                return ownedT.Value;
            }
        }

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