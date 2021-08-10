using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHABWZXKSRepository : RepositoryBase<GY_JIANCHABWZXKS>, IGY_JIANCHABWZXKSRepository
	{
		public GY_JIANCHABWZXKSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
