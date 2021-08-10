using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMYPSFRepository : RepositoryBase<GY_JIANCHAXMYPSF>, IGY_JIANCHAXMYPSFRepository
	{
		public GY_JIANCHAXMYPSFRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
