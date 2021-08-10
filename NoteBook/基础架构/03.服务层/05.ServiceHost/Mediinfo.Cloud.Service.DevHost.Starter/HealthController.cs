using System.Web.Http;

namespace Mediinfo.Cloud.Service.DevHost.Starter
{
    /// <summary>
    /// 健康检查
    /// </summary>
    public class HealthController:ApiController
    {
        /// <summary>
        /// 健康检查接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Health")]
        public string Index()
        {
            return "OK";
        }
    }
}
