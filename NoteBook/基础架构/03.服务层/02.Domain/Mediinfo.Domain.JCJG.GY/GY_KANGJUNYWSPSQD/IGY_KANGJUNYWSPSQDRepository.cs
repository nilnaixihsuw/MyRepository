using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_KANGJUNYWSPSQDRepository : IRepository<GY_KANGJUNYWSPSQD>, IDependency
	{
        /// <summary>
        /// g根据医嘱ID获取公用_抗菌药物审批申请单
        /// </summary>
        /// <param name="yiZhuID">医嘱ID</param>
        /// <returns></returns>
        List<GY_KANGJUNYWSPSQD> GetList(string yiZhuID);
        /// <summary>
        /// 根据病人住院id获取公用_抗菌药物审批申请单
        /// </summary>
        /// <param name="bingRenZYID"></param>
        /// <returns></returns>
        List<GY_KANGJUNYWSPSQD> GetListByBingRenZYID(string bingRenZYID);
    }
}
