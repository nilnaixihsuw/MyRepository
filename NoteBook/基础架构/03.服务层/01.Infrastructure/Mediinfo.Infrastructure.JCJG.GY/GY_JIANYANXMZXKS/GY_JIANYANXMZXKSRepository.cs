using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANXMZXKSRepository : RepositoryBase<GY_JIANYANXMZXKS>, IGY_JIANYANXMZXKSRepository
	{
		public GY_JIANYANXMZXKSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
