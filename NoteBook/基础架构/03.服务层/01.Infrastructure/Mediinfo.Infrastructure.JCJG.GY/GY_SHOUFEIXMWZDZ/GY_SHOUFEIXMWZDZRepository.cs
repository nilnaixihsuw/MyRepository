using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_SHOUFEIXMWZDZRepository : RepositoryBase<GY_SHOUFEIXMWZDZ>, IGY_SHOUFEIXMWZDZRepository
	{
		public GY_SHOUFEIXMWZDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_SHOUFEIXMWZDZ> GetList(string jiaGeID)
        {
            var list = this.Set<GY_SHOUFEIXMWZDZ>().Where(o=>o.JIAGEID==jiaGeID).ToList();
            return list;
        }

        public List<GY_SHOUFEIXMWZDZ> GetList()
        {
            var list = this.Set<GY_SHOUFEIXMWZDZ>().Where(o => o.ZUOFEIBZ == 0).ToList();
            return list;
        }
    }
}
