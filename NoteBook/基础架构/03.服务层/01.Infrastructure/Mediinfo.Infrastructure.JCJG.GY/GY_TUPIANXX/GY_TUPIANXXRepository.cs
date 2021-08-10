using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_TUPIANXXRepository : RepositoryBase<GY_TUPIANXX>, IGY_TUPIANXXRepository
	{
		public GY_TUPIANXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
