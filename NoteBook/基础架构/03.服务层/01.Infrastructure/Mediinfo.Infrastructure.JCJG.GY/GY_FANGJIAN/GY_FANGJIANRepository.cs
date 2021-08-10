using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_FANGJIANRepository : RepositoryBase<GY_FANGJIAN>, IGY_FANGJIANRepository
	{
		public GY_FANGJIANRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_FANGJIAN> GetFangJianList(string FangJianID)
        {
            var list = this.Set<GY_FANGJIAN>().Where(o => o.FANGJIANID == FangJianID).ToList().WithContext(this, ServiceContext);
            return list;
        }


    }
}
