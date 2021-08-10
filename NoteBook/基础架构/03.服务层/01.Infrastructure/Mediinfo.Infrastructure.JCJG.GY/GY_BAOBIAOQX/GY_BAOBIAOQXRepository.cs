using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BAOBIAOQXRepository : RepositoryBase<GY_BAOBIAOQX>, IGY_BAOBIAOQXRepository
	{
		public GY_BAOBIAOQXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
