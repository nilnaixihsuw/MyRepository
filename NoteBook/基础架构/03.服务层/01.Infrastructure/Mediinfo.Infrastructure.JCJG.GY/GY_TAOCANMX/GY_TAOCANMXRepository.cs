using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_TAOCANMXRepository : RepositoryBase<GY_TAOCANMX>, IGY_TAOCANMXRepository
	{
		public GY_TAOCANMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 根据套餐ID,项目ID,费用类别获取公用套餐明细
        /// </summary>
        /// <param name="TaoCanID">套餐ID</param>
        /// <param name="XiangMuID">项目ID</param>
        /// <param name="FeiYongLB">费用类别</param>
        /// <returns></returns>
        public GY_TAOCANMX GetTaoCanMX(string TaoCanID, string XiangMuID, int? FeiYongLB)
        {
            GY_TAOCANMX doMain = this.Set<GY_TAOCANMX>().Where(w => w.TAOCANID == TaoCanID && w.XIANGMUID == XiangMuID && w.FEIYONGLB == FeiYongLB).FirstOrDefault().WithContext(this, ServiceContext); 
            return doMain;
        }
    }
}
