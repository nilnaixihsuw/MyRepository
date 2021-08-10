using Autofac.Core;

using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Cache;
using Mediinfo.Infrastructure.Core.DomainService;
using Mediinfo.Infrastructure.Core.Filter;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Infrastructure.Core
{
    /// <summary>
    /// service的基类
    /// </summary>
    [ApiActionFilter]
    [ApiExceptionFilter]
    public class ApiBaseController : ApiController
    {
        /// <summary>
        /// 检验服务层，并抛出异常
        /// </summary>
        public ICheck Check => new ServiceCheck();

        /// <summary>
        /// 上下文缓存
        /// </summary>
        protected ContextCache _RequestContextCache = null;

        /// <summary>
        /// 上下文缓存
        /// </summary>
        protected ContextCache RequestContextCache
        {
            get
            {
                if (_RequestContextCache == null)
                {
                    _RequestContextCache = new ContextCache();
                }
                return _RequestContextCache;
            }
        }

        /// <summary>
        /// 服务调用开始时间
        /// </summary>
        protected long _ServiceStartTime = DateTime.Now.Ticks;

        /// <summary>
        /// UnitOfWork集合
        /// </summary>
        protected Dictionary<string, IUnitOfWork> _UnitOfWorks = new Dictionary<string, IUnitOfWork>();

        /// <summary>
        /// 工作单元，注意：在实现工作单元的时候，必须把获取的unitofwork添加到_UnitOfWorks字典
        /// </summary>
        protected virtual IUnitOfWork UnitOfWork
        {
            get {
                throw new NotImplementedException("使用了未实现的UnitOfWork");
            }
        }
        /// <summary>
        /// 连接SQL
        /// </summary>
        protected virtual IUnitOfWork SqlServerUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的SqlServerUnitOfWork");
            }
        }

        /// <summary>
        /// 连接BCSQL
        /// </summary>
        protected virtual IUnitOfWork SqlServerBCUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的SqlServerBCUnitOfWork");
            }
        }

        /// <summary>
        /// 连接FSSQL
        /// </summary>
        protected virtual IUnitOfWork SqlServerFSUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的SqlServerFSUnitOfWork");
            }
        }

        /// <summary>
        /// 连接BLSQL
        /// </summary>
        protected virtual IUnitOfWork SqlServerBLUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的SqlServerBLUnitOfWork");
            }
        }

        /// <summary>
        /// 连接XDSQL
        /// </summary>
        protected virtual IUnitOfWork SqlServerXDUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的SqlServerXDUnitOfWork");
            }
        }
        /// <summary>
        /// 连接包药机BYJSQL
        /// </summary>
        protected virtual IUnitOfWork SqlServerBYJUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的SqlServerXDUnitOfWork");
            }
        }
        public virtual IUnitOfWork TreadUnitOfWork
        {
            get
            {
                throw new NotImplementedException("使用了未实现的TreadUnitOfWork");
            }
        }
        
        /// <summary>
        /// 获取指定类型的IUnitOfWork
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork<T>() where T : IUnitOfWork
        {            
            string uName = typeof(T).FullName;

            if (!_UnitOfWorks.ContainsKey(uName) || _UnitOfWorks[uName] == null)
            {
                var uw =  ServiceLocator.Instance.GetService<T>(RequestContextCache);
                uw.CreateTime = DateTime.Now;
                if (uw.MessagePlugin != null)
                {
                    uw.MessagePlugin.apiController = this;
                    uw.UnitOfWorkID = Guid.NewGuid();
                }
                _UnitOfWorks.Add(uName, uw);
            }
            return _UnitOfWorks[uName];
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        protected string GetToken()
        {
           var token = this.Request.Headers.Where(m => m.Key.ToLower() == "token");
            if (token.Count() > 0)
            {
                var value = token.FirstOrDefault().Value;
            }
            return "";
        }

        /// <summary>
        /// 服务上下文
        /// </summary>
        protected ServiceContext ServiceContext { get; set; } = new ServiceContext();
        
        /// <summary>
        /// 服务上下文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        protected ServiceResult<T> ServiceContent<T>(T content)
        {
            return new ServiceResult<T>(content);
        }

        /// <summary>
        /// 获取仓储接口
        /// </summary>
        /// <typeparam name="T">仓储类型</typeparam>
        /// <param name="uow">工作单元（Unit Of Work）</param>
        /// <returns></returns>
        protected virtual T GetRepository<T>(IUnitOfWork unitOfWork)
        {
            var list = new List<ResolvedParameter>();

            list.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IRepositoryContext),
                                           (pi, ctx) => (IRepositoryContext)unitOfWork));

            list.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(ServiceContext),
                                                     (pi, ctx) => (ServiceContext)ServiceContext));

            return ServiceLocator.Instance.GetService<T>(list);
        }

        /// <summary>
        /// 获取仓储接口
        /// </summary>
        /// <typeparam name="T">仓储类型</typeparam>
        /// <returns></returns>
        protected virtual T GetRepository<T>()
        {
            var list = new List<ResolvedParameter>();

            list.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IRepositoryContext),
                                           (pi, ctx) => (IRepositoryContext)UnitOfWork));

            list.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(ServiceContext),
                                                     (pi, ctx) => (ServiceContext)ServiceContext));

            return ServiceLocator.Instance.GetService<T>(list);
        }

        protected virtual object DomainServiceInvoke(string domainServiceName, string methodName, params object[] parameters)
        {
            return DomainServicePlugin.Instance.Invoke(UnitOfWork, ServiceContext, domainServiceName, methodName, parameters);
        }
    }
}