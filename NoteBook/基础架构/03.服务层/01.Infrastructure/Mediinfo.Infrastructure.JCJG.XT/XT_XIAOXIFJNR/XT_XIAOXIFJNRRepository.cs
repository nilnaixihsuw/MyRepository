using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_XIAOXIFJNRRepository : RepositoryBase<XT_XIAOXIFJNR>, IXT_XIAOXIFJNRRepository
	{
		public XT_XIAOXIFJNRRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
