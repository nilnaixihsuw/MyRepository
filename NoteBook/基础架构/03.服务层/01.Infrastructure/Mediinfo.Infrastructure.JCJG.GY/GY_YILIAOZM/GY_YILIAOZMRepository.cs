using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YILIAOZMRepository : RepositoryBase<GY_YILIAOZM>, IGY_YILIAOZMRepository
	{
		public GY_YILIAOZMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
