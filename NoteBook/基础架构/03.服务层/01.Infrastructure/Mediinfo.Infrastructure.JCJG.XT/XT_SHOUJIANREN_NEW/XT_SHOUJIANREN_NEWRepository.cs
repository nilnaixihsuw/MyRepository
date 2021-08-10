using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_SHOUJIANREN_NEWRepository : RepositoryBase<XT_SHOUJIANREN_NEW>, IXT_SHOUJIANREN_NEWRepository
    {
		public XT_SHOUJIANREN_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

    }
}
