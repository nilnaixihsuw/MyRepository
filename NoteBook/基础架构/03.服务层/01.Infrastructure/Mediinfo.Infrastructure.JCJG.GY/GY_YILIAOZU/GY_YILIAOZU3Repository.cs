using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YILIAOZU3Repository : RepositoryBase<GY_YILIAOZU3>, IGY_YILIAOZU3Repository
	{
		public GY_YILIAOZU3Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
