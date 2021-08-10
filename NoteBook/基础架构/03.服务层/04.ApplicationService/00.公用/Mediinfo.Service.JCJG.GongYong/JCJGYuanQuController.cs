using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGYuanQu/{action}")]
    public class JCJGYuanQuController: Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取院区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YUANQU>> GetGYYuanQu()
        {
            E_GY_YUANQU eYuanQu = new E_GY_YUANQU();
            var list = new QueryService(UnitOfWork).Get<E_GY_YUANQU>(eYuanQu);
            return ServiceContent(list);
        }
    }
}
