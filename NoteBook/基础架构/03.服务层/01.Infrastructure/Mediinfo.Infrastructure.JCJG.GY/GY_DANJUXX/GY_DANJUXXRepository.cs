using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DANJUXXRepository : RepositoryBase<GY_DANJUXX>, IGY_DANJUXXRepository
	{
		public GY_DANJUXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
