using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_FEIYONGXZRepository : RepositoryBase<GY_FEIYONGXZ>, IGY_FEIYONGXZRepository
	{
		public GY_FEIYONGXZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 获取全部费用性质数据
        /// </summary>
        /// <returns></returns>
	    public List<GY_FEIYONGXZ> GetFeiYongXZList()
        {
            var list = this.Set<GY_FEIYONGXZ>().ToList().WithContext(this, ServiceContext);
            return list;
        }
	}
}
