using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class XT_SELECTSQL3Repository : RepositoryBase<XT_SELECTSQL3>, IXT_SELECTSQL3Repository
	{
		public XT_SELECTSQL3Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<XT_SELECTSQL3> GetList()
        {
            var list = this.Set<XT_SELECTSQL3>().ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<XT_SELECTSQL3> GetList(List<string> sqlIDs)
        {
            var list = this.Set<XT_SELECTSQL3>().Where(m=> sqlIDs.Contains(m.SQLID)).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }


}
