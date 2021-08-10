using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMSBRepository : RepositoryBase<GY_JIANCHAXMSB>, IGY_JIANCHAXMSBRepository
	{
		public GY_JIANCHAXMSBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
