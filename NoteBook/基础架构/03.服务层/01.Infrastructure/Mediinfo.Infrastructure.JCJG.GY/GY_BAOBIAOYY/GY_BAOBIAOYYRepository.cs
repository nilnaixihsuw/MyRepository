using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BAOBIAOYYRepository : RepositoryBase<GY_BAOBIAOYY>, IGY_BAOBIAOYYRepository
	{
		public GY_BAOBIAOYYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
