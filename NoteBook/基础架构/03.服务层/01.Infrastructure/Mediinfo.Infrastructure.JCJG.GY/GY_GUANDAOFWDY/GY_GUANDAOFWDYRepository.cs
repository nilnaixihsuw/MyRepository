using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_GUANDAOFWDYRepository : RepositoryBase<GY_GUANDAOFWDY>, IGY_GUANDAOFWDYRepository
	{
		public GY_GUANDAOFWDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_GUANDAOFWDY> GetList(string guandaoid)
        {
            var list = this.Set<GY_GUANDAOFWDY>().Where(p => p.GUANDAOID == guandaoid).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
