using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Mediinfo.Cloud.Service.DevHost.Starter.Provider
{
    /// <summary>
    /// 设置特性可以继承
    /// </summary>
    public class MediinfoServiceDirectRouteProvider : DefaultDirectRouteProvider
    {
        /// <summary>
        /// 重写并设置特性可继承
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
