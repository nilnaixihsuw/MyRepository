using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_GUANDAOYZXMDYRepository : RepositoryBase<GY_GUANDAOYZXMDY>, IGY_GUANDAOYZXMDYRepository
	{
		public GY_GUANDAOYZXMDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_GUANDAOYZXMDY> GetList()
        {
            var list = this.Set<GY_GUANDAOYZXMDY>().ToList().WithContext(this, ServiceContext);
            return list;
        }
        public List<GY_GUANDAOYZXMDY> GetList(string guandaoid)
        {
            var list = this.Set<GY_GUANDAOYZXMDY>().Where(p => p.GUANDAOID == guandaoid).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
