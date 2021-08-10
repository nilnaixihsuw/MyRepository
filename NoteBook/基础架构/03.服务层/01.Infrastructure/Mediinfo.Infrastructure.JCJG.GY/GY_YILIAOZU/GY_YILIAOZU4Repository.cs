using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YILIAOZU4Repository : RepositoryBase<GY_YILIAOZU4>, IGY_YILIAOZU4Repository
	{
		public GY_YILIAOZU4Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_YILIAOZU4 GetByID(string yiliaozid, string keshiid)
        {
            var dto = (from ylz4 in this.Set<GY_YILIAOZU4>()
                       where ylz4.YILIAOZID == yiliaozid && ylz4.KESHIID == keshiid
                       select ylz4).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }
    }
}
