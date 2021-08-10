using Autofac;
using Autofac.Features.OwnedInstances;

using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Log;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 获取Token的实现
    /// </summary>
    public class ContextLocator
    {
        private readonly Autofac.IContainer _container;
        private static readonly ContextLocator _instance = new ContextLocator();
        private IServiceContextCreater serviceContextCreater = null;

        /// <summary>
        /// 单例构造函数
        /// </summary>
        private ContextLocator()
        {
            var builder = new ContainerBuilder();
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            if (HttpContext.Current != null)
            {
                assemblyPath += "bin";
            }

            List<Assembly> assemblys = new List<Assembly>();

            DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = TheFolder.GetFiles().Where(c => c.Extension.Contains("dll") && c.FullName.Contains("Mediinfo.") && (c.FullName.Contains(".Core")));
            
            // 遍历文件
            foreach (FileInfo NextDll in mediDlls)
            {
                var ass = Assembly.LoadFrom(NextDll.FullName);
                assemblys.Add(ass);
            }

            var types = assemblys.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IServiceContextCreater)))).ToArray();

            var impls = types.Where(t => !t.IsInterface).ToList();

            foreach (var impl in impls)
            {
                var services = impl.GetInterfaces();
                foreach (var service in services)
                {
                    builder.RegisterType(impl).As(service).InstancePerDependency();
                }
            }

            _container = builder.Build();
        }

        public static ContextLocator Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public ServiceContext GetServiceContext()
        {
            try
            {
                var ownedT = _container.Resolve<Owned<IServiceContextCreater>>();
                return ownedT.Value.GetServiceContext();
            }
            catch (Exception ex)
            {
                LogHelper.Intance.Error("ServiceContext", "代理加载ServiceContext出现错误！", ex.ToString());
                return new ServiceContext();
            }
        }
    }
}