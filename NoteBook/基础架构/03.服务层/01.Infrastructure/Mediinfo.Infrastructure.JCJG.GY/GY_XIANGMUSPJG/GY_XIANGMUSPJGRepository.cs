using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_XIANGMUSPJGRepository : RepositoryBase<GY_XIANGMUSPJG>, IGY_XIANGMUSPJGRepository
	{
		public GY_XIANGMUSPJGRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_XIANGMUSPJG> QueryList(string bingRenID, string xiangMuID, DateTime shiYongRQ, int menZhenZYBZ)
        {
            var time = shiYongRQ.Date.AddDays(1).AddMinutes(-1); 
            var list = this.QuerySet<GY_XIANGMUSPJG>().Where(w => w.BINGRENID == bingRenID && w.XIANGMUID == xiangMuID &&
           w.SHENPIJG == 1&&w.ZUOFEIBZ==0&&w.SHENPISL>0 && ((w.MENZHENSY == 1 && menZhenZYBZ == 0) || (w.ZHUYUANSY == 1 && menZhenZYBZ == 1))
           &&  w.KAISHIRQ <= time && shiYongRQ.Date < w.JIESHURQ).ToList(); 
            return list;
        }
        public List<GY_XIANGMUSPJG> QueryList(string bingRenID, List<string> xiangMuID)
        {
            var list = this.QuerySet<GY_XIANGMUSPJG>().Where(w => w.BINGRENID == bingRenID && xiangMuID.Contains(w.XIANGMUID) &&
           w.SHENPIJG == 1 && w.ZUOFEIBZ == 0 && w.SHENPISL > 0).ToList();
            return list;
        }

    }
}
