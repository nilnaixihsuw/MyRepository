using Mediinfo.Cloud.Service.SelfHost.Starter.Middleware;
using Mediinfo.Cloud.Service.SelfHost.Starter.Provider;
using Mediinfo.Infrastructure.Core.Settings;

using Owin;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Mediinfo.Cloud.Service.SelfHost.Starter.Configuration
{
    /// <summary>
    /// host 启动配置
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

            config.ParameterBindingRules.Add(p =>
            {
                return SettingContainer.Instance.Get<HttpParameterBinding>(p);
            });
            
            appBuilder.UseWebApi(config);

            appBuilder.Use(typeof(RequestSizeLimitingMiddleware), (long)2147483648);
            appBuilder.Use(typeof(ClearMiddleware), (long)500000);

            try
            {
                config.EnsureInitialized();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Enterprise.Log.LogHelper.Intance.Error("系统日志", "服务EnsureInitialized校验的过程中发现问题", "服务EnsureInitialized校验的过程中发现问题：" + ex.ToString());
            }
        }
    }
}
