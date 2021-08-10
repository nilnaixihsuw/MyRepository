using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_MAZUIQXJBDZRepository : RepositoryBase<GY_MAZUIQXJBDZ>, IGY_MAZUIQXJBDZRepository
	{
		public GY_MAZUIQXJBDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 取职工手术权限
        /// </summary>
        /// <returns></returns>
        public List<GY_MAZUIQXJBDZ> GetMaZuiQX(string prmZhiChengID, string prmKeShiID)
        {
            var list = this.Set<GY_MAZUIQXJBDZ>().Where(o => o.MAZUIZCID == prmZhiChengID && o.KESHIID == prmKeShiID && o.ZUOFEIBZ == 0).ToList().WithContext(this, ServiceContext);
            return list;
        }        
    }
}
