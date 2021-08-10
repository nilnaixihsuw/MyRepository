using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZUIGAOPLJXGRZRepository : RepositoryBase<GY_ZUIGAOPLJXGRZ>, IGY_ZUIGAOPLJXGRZRepository
	{
		public GY_ZUIGAOPLJXGRZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_ZUIGAOPLJXGRZ GetByID(string yingyongid, string jiageid,DateTime xiugaisj)
        {
            var dto = (from zgpljxgrz in this.Set<GY_ZUIGAOPLJXGRZ>()
                       where zgpljxgrz.YINGYONGID == yingyongid && zgpljxgrz.JIAGEID == jiageid && zgpljxgrz.XIUGAISJ == xiugaisj
                       select zgpljxgrz).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }
    }
}
