using Mediinfo.Cloud.Service.DevHost.Starter.Middleware;
using Mediinfo.Cloud.Service.DevHost.Starter.Provider;
using Mediinfo.Infrastructure.Core.Settings;

using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Mediinfo.Cloud.Service.DevHost.Starter.Configuration
{
    /// <summary>
    /// host配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes(new MediinfoServiceDirectRouteProvider());

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            // 默认返回 json  
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));
            // 返回格式选择 datatype 可以替换为任何参数   
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "xml", "application/xml"));

            //config.MessageHandlers.Add(new DecompressionHandler());

            config.ParameterBindingRules.Add(p =>
            {
                return SettingContainer.Instance.Get<HttpParameterBinding>(p);
            });

            config.EnsureInitialized();

            appBuilder.UseWebApi(config);

            appBuilder.Use(typeof(RequestSizeLimitingMiddleware), (long)2147483648);
            appBuilder.Use(typeof(ClearMiddleware), (long)5000);
        }
    }
}
