using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMBWDYRepository : RepositoryBase<GY_JIANCHAXMBWDY>, IGY_JIANCHAXMBWDYRepository
	{
		public GY_JIANCHAXMBWDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
