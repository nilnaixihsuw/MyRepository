using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_XIAOXI_NEWRepository : RepositoryBase<XT_XIAOXI_NEW>, IXT_XIAOXI_NEWRepository
	{
		public XT_XIAOXI_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<XT_XIAOXI_NEW> GetWeiGuoQi(DateTime faSongSJ)
        {
            DateTime now = this.GetSYSTime();
            if (faSongSJ.Year == now.Year && faSongSJ.Month == now.Month && faSongSJ.Day == now.Day)
                return this.QuerySet<XT_XIAOXI_NEW>().Where(m => m.YUEDUBZ == 0 && m.YOUXIAOQI >= now).ToList();
            else
                return this.QuerySet<XT_XIAOXI_NEW>().Where(m => m.YUEDUBZ == 0 && m.YOUXIAOQI >= now && m.FASONGSJ >= faSongSJ).Take(100).ToList();
        }
        public List<XT_XIAOXI_NEW> GetXiaoXiByID(List<long?> xiaoXiIDList)
        {
            return this.QuerySet<XT_XIAOXI_NEW>().Where(m => xiaoXiIDList.Contains(m.XIAOXIID)).ToList();
        }
        public XT_XIAOXI_NEW GetXiaoXiByXXBMAndXXLY(string xiaoxiBM, string xiaoxiLY)
        {
            return this.QuerySet<XT_XIAOXI_NEW>().Where(m => m.XIAOXIBM == xiaoxiBM && m.XIAOXILY == xiaoxiLY).FirstOrDefault();
        }        
    }
}
