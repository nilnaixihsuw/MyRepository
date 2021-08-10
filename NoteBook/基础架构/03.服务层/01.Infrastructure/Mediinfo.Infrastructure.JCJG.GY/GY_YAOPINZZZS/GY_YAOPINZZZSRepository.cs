using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINZZZSRepository : RepositoryBase<GY_YAOPINZZZS>, IGY_YAOPINZZZSRepository
	{
		public GY_YAOPINZZZSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
