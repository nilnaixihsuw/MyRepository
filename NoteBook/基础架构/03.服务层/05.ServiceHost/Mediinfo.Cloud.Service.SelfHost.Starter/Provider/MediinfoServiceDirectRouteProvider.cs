using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Mediinfo.Cloud.Service.SelfHost.Starter.Provider
{
    /// <summary>
    /// 设置特性
    /// </summary>
    public class MediinfoServiceDirectRouteProvider : DefaultDirectRouteProvider
    {
        /// <summary>
        /// 设置特性可继承
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        protected override IReadOnlyList<IDirectRouteFactory>
            GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>
            (inherit: true);
        }
    }
}
