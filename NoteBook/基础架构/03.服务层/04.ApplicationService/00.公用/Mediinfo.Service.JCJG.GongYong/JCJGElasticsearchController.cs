using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Log;
using Mediinfo.Service.JCJG.GongYong.Route;

using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    /// <summary>
    /// 公用日志处理
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGElasticsearch/{action}")]
    public class JCJGElasticsearchController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 推送ES日志
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> PutLog(string id, int status)
        {
            Check.NotEmpty(id, "消息ID不得为空");
            dynamic obj = new System.Dynamic.ExpandoObject(); //动态类型字段 可读可写
            obj.ID = id;
            obj.Status = status;
            obj.ChuLiSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            LogHelper.Intance.Info("消息处理", "消息处理完毕", JsonConvert.SerializeObject(obj), id);
            return ServiceContent(true);
        }

        /// <summary>
        /// 清除全部日志数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> ClearAll()
        {
            ESLog eSLog = new ESLog();
            eSLog.ClearAll();

            return ServiceContent(true);
        }
    }
}
