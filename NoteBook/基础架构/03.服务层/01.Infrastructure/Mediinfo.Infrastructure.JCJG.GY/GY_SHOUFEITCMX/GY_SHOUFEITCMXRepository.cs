using System;
using System.Data.Entity.ModelConfiguration;
using Mediinfo.Enterprise;
using System.Linq.Expressions;
using System.Linq;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.Repository;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_SHOUFEITCMXRepository : RepositoryBase<GY_SHOUFEITCMX>, IGY_SHOUFEITCMXRepository
	{
		public GY_SHOUFEITCMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public void SetCache()
        {
            var shouFeiTCMXList = GetFromCache<List<GY_SHOUFEITCMX>>("ShouFeiTCMX");
            if (shouFeiTCMXList == null)
            {
                shouFeiTCMXList = this.QuerySet<GY_SHOUFEITCMX>().ToList();
                AddToCache<List<GY_SHOUFEITCMX>>("ShouFeiTCMX", shouFeiTCMXList);
            }
        }

        public List<GY_SHOUFEITCMX> GetList()
        {
            var list = this.Set<GY_SHOUFEITCMX>().ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_SHOUFEITCMX> GetListFromCache(string shouFeiTCID)
        {
            var shouFeiTCMX = new List<GY_SHOUFEITCMX>();

            var shouFeiTCMXList = GetFromCache<List<GY_SHOUFEITCMX>>("ShouFeiTCMX");

            if (shouFeiTCMXList == null)
            {
                shouFeiTCMX = this.Set<GY_SHOUFEITCMX>().Where(o => o.SHOUFEITCID == shouFeiTCID).ToList().WithContext(this, ServiceContext);
            }
            else
            {
                shouFeiTCMX = shouFeiTCMXList.Where(o => o.SHOUFEITCID == shouFeiTCID).ToList().WithContext(this, ServiceContext);
            }
            return shouFeiTCMX;
        }
    }
}
