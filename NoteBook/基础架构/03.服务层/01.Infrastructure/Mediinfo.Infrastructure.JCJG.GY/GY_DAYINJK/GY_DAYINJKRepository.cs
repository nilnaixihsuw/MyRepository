using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DAYINJKRepository : RepositoryBase<GY_DAYINJK>, IGY_DAYINJKRepository
	{
		public GY_DAYINJKRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_DAYINJK> GetMZList(int laiYuanLX, string jiuZhenID, string laiYuanID)
        {
            List<GY_DAYINJK> lst = new List<GY_DAYINJK>();
            if (string.IsNullOrWhiteSpace(laiYuanID))
            {
                lst = this.Set<GY_DAYINJK>().Where(o => o.JIUZHENID == jiuZhenID && o.JILULY == laiYuanLX ).ToList();
            }
            else
            {
                lst = this.Set<GY_DAYINJK>().Where(o => o.JIUZHENID == jiuZhenID && o.JILULY == laiYuanLX && o.LAIYUANID == laiYuanID).ToList();
            }
            return lst;
        }
    }
}
