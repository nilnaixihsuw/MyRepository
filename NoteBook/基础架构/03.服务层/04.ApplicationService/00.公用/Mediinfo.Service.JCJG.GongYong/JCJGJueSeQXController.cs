using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGJueSeQX/{action}")]
    public class JCJGJueSeQXController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<string>> Get()
        {
            return ServiceContent(new List<string>());
        }



        /// <summary>
        /// 根据权限id取角色权限信息
        /// </summary>
        /// <param name="quanXianID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESEQX>> GetListByQXID(string quanXianID)
        {
            Check.NotEmpty(quanXianID, "权限ID不能为空！");

            E_GY_JUESEQX jueSeQX = new E_GY_JUESEQX();
            jueSeQX.Where(" WHERE QUANXIANID =:QUANXIANID", quanXianID);
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESEQX>(jueSeQX);
            return ServiceContent(list);
        }
    }
}
