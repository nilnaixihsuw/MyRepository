using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANXMXBDZRepository : RepositoryBase<GY_JIANYANXMXBDZ>, IGY_JIANYANXMXBDZRepository
	{
		public GY_JIANYANXMXBDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
