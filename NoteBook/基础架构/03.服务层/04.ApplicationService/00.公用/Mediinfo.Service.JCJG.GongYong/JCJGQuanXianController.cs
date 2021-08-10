using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGQuanXian/{action}")]
    public class JCJGQuanXianController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<string>> Get()
        {
            return ServiceContent(new List<string>());
        }


        /// <summary>
        /// 根据权限ID取权限
        /// </summary>
        /// <param name="quanXianID">权限ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_QUANXIAN>> GetQuanXianByQXID(string quanXianID)
        {
            Check.NotEmpty(quanXianID, "权限名称不能为空！");
            E_GY_QUANXIAN entry = new E_GY_QUANXIAN();
            entry.Where(" WHERE QUANXIANJB=2 AND QUANXIANID = :QUANXIANID", quanXianID);
            var list = new QueryService(UnitOfWork).Get<E_GY_QUANXIAN>(entry);
            return ServiceContent(list);
        }


        /// <summary>
        /// 新建权限数据
        /// </summary>
        /// <param name="quanXianID">权限ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> XinJianQX(string quanXianID)
        {
            Check.NotEmpty(quanXianID, "权限名称不能为空！");
            UnitOfWork.BeginTransaction();

            var quanXianRep = this.GetRepository<IGY_QUANXIANRepository>(UnitOfWork);

            E_GY_QUANXIAN quanXianDTO = new E_GY_QUANXIAN();
            quanXianDTO.QUANXIANID = quanXianID;
            quanXianDTO.QUANXIANMC = quanXianID+"(A)";
            quanXianDTO.GONGNENGID = "*";
            quanXianDTO.QUANXIANJB = 2;
            quanXianDTO.QUANXIANMS = "";
            quanXianDTO.QIYONGBZ = 0;   //默认使用启用标志为0
            var quanXianEntity = GY_QUANXIANFactory.Create(quanXianRep, ServiceContext, quanXianDTO);
            quanXianRep.RegisterAdd(quanXianEntity);

            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();


            return ServiceContent(true);

        }




    }
}
