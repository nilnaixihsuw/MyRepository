using Autofac;
using Autofac.Features.OwnedInstances;

using Mediinfo.Enterprise.Token;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Mediinfo.Enterprise.Log;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 获取Token的实现
    /// </summary>
    public class TokenLocator
    {
        private readonly Autofac.IContainer _container;
        private static readonly TokenLocator _instance = new TokenLocator();
        private IAccessToken AccessToken = null;

        /// <summary>
        /// 单例构造函数
        /// </summary>
        private TokenLocator()
        {
            var builder = new ContainerBuilder();
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            if (HttpContext.Current != null)
            {
                assemblyPath += "bin";
            }

            List<Assembly> assemblys = new List<Assembly>();

            DirectoryInfo theFolder = new DirectoryInfo(assemblyPath);
            var mediDlls = theFolder.GetFiles().Where(c => c.Extension.Contains("dll") && c.FullName.Contains("Mediinfo.HIS.Core"));

            // 遍历文件
            foreach (FileInfo nextDll in mediDlls)
            {
                var ass = Assembly.LoadFrom(nextDll.FullName);
                assemblys.Add(ass);
            }

            try
            {
                var types = assemblys
                    .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IAccessToken))))
                    .ToArray();

                var impls = types.Where(t => !t.IsInterface).ToList();

                foreach (var impl in impls)
                {
                    var services = impl.GetInterfaces();
                    foreach (var service in services)
                    {
                        builder.RegisterType(impl).As(service).InstancePerDependency();
                    }
                }
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                StringBuilder sb = new StringBuilder("装载DLL文件类型异常 : ");
                foreach (var exception in reflectionTypeLoadException.LoaderExceptions)
                {
                    sb.Append(exception.Message);
                    sb.Append(Environment.NewLine);
                }

                throw new Exception(sb.ToString());
            }


            _container = builder.Build();
        }

        public static TokenLocator Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 设置token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void SetToken(string token)
        {
            var ownedT = _container.Resolve<Owned<IAccessToken>>();
            ownedT.Value.SetToken(token);
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var ownedT = _container.Resolve<Owned<IAccessToken>>();
            return ownedT.Value.GetToken();
        }
    }
}