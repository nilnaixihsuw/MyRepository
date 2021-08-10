using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_XIANGMUSPRepository : RepositoryBase<GY_XIANGMUSP>, IGY_XIANGMUSPRepository
	{
		public GY_XIANGMUSPRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_XIANGMUSP> QueryList(string xiangMuID, string feiYongKZID, int menZhenZYBZ, DateTime shiYongRQ)
        { 
            var list = this.QuerySet<GY_XIANGMUSP>().Where(w => w.XIANGMUID == xiangMuID && w.FEIYONGKZID == feiYongKZID
            && ((w.MENZHENSY == 1 && menZhenZYBZ == 0) || (w.ZHUYUANSY == 1 && menZhenZYBZ == 1))).ToList();

            return list;
        }

        public List<GY_XIANGMUSP> QueryList(List<string> xiangMuID, int yaoPinZLBZ)
        {
            var list = this.QuerySet<GY_XIANGMUSP>().Where(w => xiangMuID.Contains(w.XIANGMUID)&&w.YAOPINZLBZ==yaoPinZLBZ).ToList();

            return list;
        }
    }
}
