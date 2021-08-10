using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DATALAYOUT2Repository : RepositoryBase<GY_DATALAYOUT2>, IGY_DATALAYOUT2Repository
	{
		public GY_DATALAYOUT2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
