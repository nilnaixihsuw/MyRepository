using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINLDRepository : RepositoryBase<GY_YAOPINLD>, IGY_YAOPINLDRepository
	{
		public GY_YAOPINLDRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YAOPINLD> GetList()
        {
            var list = this.Set<GY_YAOPINLD>().ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
