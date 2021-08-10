using Mediinfo.APIGateway.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Mediinfo.Service.JCJG.GongYong.Route
{
    public class ServiceRoutePrefixAttribute : RoutePrefixAttribute
    {
        public ServiceRoutePrefixAttribute()
        {
            var assembly = this.GetType().Assembly;
            ServiceInfo serviceInfo = new ServiceInfo(assembly);

            Prefix = serviceInfo.GetServiceName();
        }
        public override string Prefix { get; }
    }
}
