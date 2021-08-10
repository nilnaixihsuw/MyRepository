using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.HIS.Infrastructure
{
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public class ServiceLocator : IServiceProvider
    {
        private readonly Autofac.IContainer _container;
        private static ServiceLocator _instance = new ServiceLocator();

        private ServiceLocator()
        {
            var builder = new ContainerBuilder();
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            List<Assembly> assemblys = new List<Assembly>();

            DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = TheFolder.GetFiles().Where(c => c.Extension.Contains("dll"));
                
            //遍历文件
            foreach (FileInfo NextDll in mediDlls)
            {
                var ass = Assembly.LoadFrom(NextDll.FullName);
                assemblys.Add(ass);
            }

            var types = assemblys.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDependency)))).ToArray();

            var impls = types.Where(t => !t.IsInterface).ToList();

            foreach (var impl in impls)
            {
                var services = impl.GetInterfaces().Where(m => m != typeof(IDependency));
                foreach (var service in services)
                {
                    builder.RegisterType(impl).As(service).InstancePerDependency();
                }
            }

            _container = builder.Build();
        }

        public static ServiceLocator Instance
        {
            get { return _instance; }
        }

        #region Public Methods

        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        public T GetService<T>(params object[] parameters)
        {
            if (parameters == null || parameters.Length <= 0)
            {
                return _container.Resolve<T>();
            }
            else
            {
                List<ResolvedParameter> paramList = new List<ResolvedParameter>();
                foreach (var item in parameters)
                {
                    paramList.Add(new ResolvedParameter((pi,ctx)=>pi.ParameterType==item.GetType(),(pi,ctx)=>item));
                }
                return _container.Resolve<T>(paramList.ToArray());
            }
        }

        public T[] GetAllService<T>()
        {
            return _container.Resolve<IEnumerable<T>>().ToArray();
        }

        public object[] GetAllService(Type type)
        {
            Type enumerableOfType = typeof(IEnumerable<>).MakeGenericType(type);
            return (object[])_container.ResolveService(new TypedService(enumerableOfType));
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
        #endregion
    }
}