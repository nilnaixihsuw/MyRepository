using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_GONGNENGRepository : RepositoryBase<XT_GONGNENG>, IXT_GONGNENGRepository
	{
		public XT_GONGNENGRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
