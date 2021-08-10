using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YEWUPCRepository : RepositoryBase<GY_YEWUPC>, IGY_YEWUPCRepository
	{
		public GY_YEWUPCRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YEWUPC> GetList(string yeWuLX, string yuanQuID)
        {
            var list = this.Set<GY_YEWUPC>().Where(o => o.YEWULX == yeWuLX && o.YUANQUID == yuanQuID).ToList().WithContext(this, ServiceContext);
            return list;
        } 

    }
}
