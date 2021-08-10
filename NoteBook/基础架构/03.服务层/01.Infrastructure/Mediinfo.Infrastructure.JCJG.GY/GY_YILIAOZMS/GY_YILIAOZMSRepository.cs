using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YILIAOZMSRepository : RepositoryBase<GY_YILIAOZMS>, IGY_YILIAOZMSRepository
	{
		public GY_YILIAOZMSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
