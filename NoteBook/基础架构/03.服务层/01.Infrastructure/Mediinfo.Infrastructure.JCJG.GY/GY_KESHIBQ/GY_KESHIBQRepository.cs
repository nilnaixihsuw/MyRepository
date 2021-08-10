using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_KESHIBQRepository : RepositoryBase<GY_KESHIBQ>, IGY_KESHIBQRepository
	{
		public GY_KESHIBQRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
