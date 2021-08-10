using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{

    /// <summary>
    /// 权限表
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGQuanXian2N/{action}")]
    public class JCJGQuanXian2NController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {



        /// <summary>
        /// 通过权限ID获取该窗体下的权限数据
        /// </summary>
        /// <param name="quanxianid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ERJIYHQX>> GetQuanXianNewByQXID(string quanxianid)
        {
            E_GY_ERJIYHQX entry = new E_GY_ERJIYHQX();
            entry.Where(" WHERE GONGNENGID = :GONGNENGID", quanxianid);
            var list = new QueryService(UnitOfWork).Get<E_GY_ERJIYHQX>(entry);
            return ServiceContent(list);
        }



        /// <summary>
        /// 获取所有赋权信息数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ERJIYHQX>> GetAllQuanXianSJ()
        {
            E_GY_ERJIYHQX entry = new E_GY_ERJIYHQX();
            //entry.Where(" WHERE QIYONGBZ = :QIYONGBZ", 1);
            var list = new QueryService(UnitOfWork).Get<E_GY_ERJIYHQX>(entry);
          
            return ServiceContent(list.Distinct().ToList());
        }


        /// <summary>
        /// yonghuid
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_QUANXIANYHKZ>> GetYONGHUQXXX(string yonghuid,string gongnengid,string mingmingkj,string kongjianmc,string chuangtimc)
        {
            E_GY_QUANXIANYHKZ entity = new E_GY_QUANXIANYHKZ();
            entity.Where("WHERE YONGHUID = :YONGHUID AND MINGMINGKJ = :MINGMINGKJ AND GONGNENGID = :GONGNENGID AND CHUANGKOUMC = :CHUANGKOUMC AND KONGJIANMC = :KONGJIANMC", yonghuid, mingmingkj, gongnengid, chuangtimc, kongjianmc);
            var list = new QueryService(UnitOfWork).Get<E_GY_QUANXIANYHKZ>(entity);
            return ServiceContent(list.Distinct().ToList());
        }
    }
}
