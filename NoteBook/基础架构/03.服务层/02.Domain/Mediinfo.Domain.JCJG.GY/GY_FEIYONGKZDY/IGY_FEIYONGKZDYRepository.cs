using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	public interface IGY_FEIYONGKZDYRepository : IRepository<GY_FEIYONGKZDY>, IDependency
	{
        /// <summary>
        /// 根据未作废费用控制对应
        /// </summary>
        /// <param name="yingYongID">应用ID</param>
        /// <param name="feiYongXZ">费用性质</param>
        /// <returns></returns>
        GY_FEIYONGKZDY GetWeiZuoFei(string yingYongID, string feiYongXZ);
	}
}
