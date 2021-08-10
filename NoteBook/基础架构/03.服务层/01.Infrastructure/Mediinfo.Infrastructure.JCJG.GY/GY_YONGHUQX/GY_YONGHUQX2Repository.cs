using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YONGHUQX2Repository : RepositoryBase<GY_YONGHUQX2>, IGY_YONGHUQX2Repository
	{
		public GY_YONGHUQX2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        public GY_YONGHUQX2 GetByID(string quanxianid,string yonghuid)
        {
            var dto = (from qx in this.Set<GY_YONGHUQX2>()
                       where qx.QUANXIANID == quanxianid && qx.YONGHUID == yonghuid
                       select qx).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }
    }
}
