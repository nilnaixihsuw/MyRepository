using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_LVSETDYPSZRepository : RepositoryBase<GY_LVSETDYPSZ>, IGY_LVSETDYPSZRepository
	{
		public GY_LVSETDYPSZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
