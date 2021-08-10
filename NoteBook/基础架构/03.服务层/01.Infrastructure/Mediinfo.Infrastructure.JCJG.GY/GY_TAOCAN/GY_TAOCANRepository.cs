using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_TAOCANRepository : RepositoryBase<GY_TAOCAN>, IGY_TAOCANRepository
	{
		public GY_TAOCANRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
