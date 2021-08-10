using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DAIMALBRepository : RepositoryBase<GY_DAIMALB>, IGY_DAIMALBRepository
	{
		public GY_DAIMALBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
