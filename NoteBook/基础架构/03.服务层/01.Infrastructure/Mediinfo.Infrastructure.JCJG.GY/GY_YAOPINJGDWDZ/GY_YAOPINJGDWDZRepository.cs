using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINJGDWDZRepository : RepositoryBase<GY_YAOPINJGDWDZ>, IGY_YAOPINJGDWDZRepository
	{
		public GY_YAOPINJGDWDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YAOPINJGDWDZ> GetList(string jiaGeID)
        {
            var list = this.Set<GY_YAOPINJGDWDZ>().Where(o => o.JIAGEID == jiaGeID).ToList();
            return list;

        }
    }
}
