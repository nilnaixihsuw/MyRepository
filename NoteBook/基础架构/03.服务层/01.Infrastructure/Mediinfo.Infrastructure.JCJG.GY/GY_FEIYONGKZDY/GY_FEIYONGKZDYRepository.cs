using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_FEIYONGKZDYRepository : RepositoryBase<GY_FEIYONGKZDY>, IGY_FEIYONGKZDYRepository
	{
		public GY_FEIYONGKZDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 取未作废的费用控制对应信息（查询）
        /// </summary>
        /// <param name="yingYongID">应用ID</param>
        /// <param name="feiYongXZ">费用性质</param>
        /// <returns></returns>
        public GY_FEIYONGKZDY GetWeiZuoFei(string yingYongID, string feiYongXZ)
        {
            return this.Set()
                       .Where(c => c.FEIYONGXZ == feiYongXZ && c.YINGYONGID == yingYongID && c.ZUOFEIBZ != 1)
                       .FirstOrDefault()
                       .WithContext(this,this.ServiceContext);
        }
    }
}
