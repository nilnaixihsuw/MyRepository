using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_GUANDAOBWDYRepository : RepositoryBase<GY_GUANDAOBWDY>, IGY_GUANDAOBWDYRepository
	{
		public GY_GUANDAOBWDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        public List<GY_GUANDAOBWDY> GetList(string guandaoid)
        {
            var list = this.Set<GY_GUANDAOBWDY>().Where(p => p.GUANDAOID == guandaoid).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
