using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_TESHUMDRepository : RepositoryBase<GY_TESHUMD>, IGY_TESHUMDRepository
	{
		public GY_TESHUMDRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_TESHUMD> GetList(string bingRenID,int mingDanLB ,int dangQianZT)
        {
            var list = this.Set<GY_TESHUMD>().Where(o=>o.BINGRENID == bingRenID && o.MINGDANLB == mingDanLB && o.DANGQIANZT == dangQianZT).ToList().WithContext(this, ServiceContext);
            return list;
        }
	}
}
