using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_GEIYAOFSDZRepository : RepositoryBase<GY_GEIYAOFSDZ>, IGY_GEIYAOFSDZRepository
	{
		public GY_GEIYAOFSDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
