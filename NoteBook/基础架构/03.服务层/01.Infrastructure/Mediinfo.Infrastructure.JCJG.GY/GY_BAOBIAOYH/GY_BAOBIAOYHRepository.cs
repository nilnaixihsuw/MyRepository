using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BAOBIAOYHRepository : RepositoryBase<GY_BAOBIAOYH>, IGY_BAOBIAOYHRepository
	{
		public GY_BAOBIAOYHRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
