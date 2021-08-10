using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_CAIDAN_NEWRepository : RepositoryBase<GY_CAIDAN_NEW>, IGY_CAIDAN_NEWRepository
    {
        public GY_CAIDAN_NEWRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public List<GY_CAIDAN_NEW> GetList(string caiDanID)
        {
            var list = this.Set<GY_CAIDAN_NEW>().Where(p => p.CAIDANID == caiDanID && p.YINGYONGID == ServiceContext.KUCUNYYID && p.SHANGJICDID.Substring(0, 1) == "1").ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
