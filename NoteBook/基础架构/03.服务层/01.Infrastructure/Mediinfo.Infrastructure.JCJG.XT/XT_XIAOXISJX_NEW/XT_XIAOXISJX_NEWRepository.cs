using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_XIAOXISJX_NEWRepository : RepositoryBase<XT_XIAOXISJX_NEW>, IXT_XIAOXISJX_NEWRepository
	{
		public XT_XIAOXISJX_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<XT_XIAOXISJX_NEW> GetList(long xiaoXiID)
        {
            var xiaoXiList = this.Set<XT_XIAOXISJX_NEW>().Where(o => o.XIAOXIID == xiaoXiID).ToList().WithContext(this, ServiceContext);
            return xiaoXiList;
        }
        public List<XT_XIAOXISJX_NEW> GetAllXiaoXiSJ(List<long?> xiaoXiID, string zhiGongID)
        {
            var result = this.QuerySet<XT_XIAOXISJX_NEW>().Where(p => xiaoXiID.Contains(p.XIAOXIID) && p.SHOUJIANRID == zhiGongID);
            if (result == null)
            {
                return null;
            }
            return result.ToList();
        }
        public List<XT_XIAOXISJX_NEW> GetAllXiaoXiSJ(List<long?> xiaoXiID)
        {
            var result = this.QuerySet<XT_XIAOXISJX_NEW>().Where(p => xiaoXiID.Contains(p.XIAOXIID));
            if (result == null)
            {
                return null;
            }
            return result.ToList();
        }
    }
}
