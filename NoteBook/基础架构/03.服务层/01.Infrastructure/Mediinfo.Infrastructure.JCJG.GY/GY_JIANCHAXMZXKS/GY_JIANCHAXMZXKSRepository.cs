using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMZXKSRepository : RepositoryBase<GY_JIANCHAXMZXKS>, IGY_JIANCHAXMZXKSRepository
	{
		public GY_JIANCHAXMZXKSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
