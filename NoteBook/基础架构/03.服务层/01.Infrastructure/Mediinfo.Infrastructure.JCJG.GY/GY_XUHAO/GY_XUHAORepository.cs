using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_XUHAORepository : RepositoryBase<GY_XUHAO>, IGY_XUHAORepository
	{
		public GY_XUHAORepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
