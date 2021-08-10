using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_TAOCANMXRepository : IRepository<GY_TAOCANMX>, IDependency
    {
        /// <summary>
        /// 根据套餐ID,项目ID,费用类别获取公用套餐明细
        /// </summary>
        /// <param name="TaoCanID">套餐ID</param>
        /// <param name="XiangMuID">项目ID</param>
        /// <param name="FeiYongLB">费用类别</param>
        /// <returns></returns>
        GY_TAOCANMX GetTaoCanMX(string TaoCanID, string XiangMuID, int? FeiYongLB);
    }
}
