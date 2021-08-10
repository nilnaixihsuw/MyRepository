using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAMBNRRepository : RepositoryBase<GY_JIANCHAMBNR>, IGY_JIANCHAMBNRRepository
	{
		public GY_JIANCHAMBNRRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
