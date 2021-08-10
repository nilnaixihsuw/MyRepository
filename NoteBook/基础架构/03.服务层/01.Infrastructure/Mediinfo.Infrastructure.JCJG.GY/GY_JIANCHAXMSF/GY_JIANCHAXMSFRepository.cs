using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMSFRepository : RepositoryBase<GY_JIANCHAXMSF>, IGY_JIANCHAXMSFRepository
	{
		public GY_JIANCHAXMSFRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
