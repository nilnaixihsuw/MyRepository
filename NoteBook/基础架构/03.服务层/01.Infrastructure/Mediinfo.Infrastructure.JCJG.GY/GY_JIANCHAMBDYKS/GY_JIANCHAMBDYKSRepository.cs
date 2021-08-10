using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAMBDYKSRepository : RepositoryBase<GY_JIANCHAMBDYKS>, IGY_JIANCHAMBDYKSRepository
	{
		public GY_JIANCHAMBDYKSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
