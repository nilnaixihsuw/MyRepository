using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.DTO.HIS.GY;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YONGHUYYRepository : RepositoryBase<GY_YONGHUYY>, IGY_YONGHUYYRepository
	{
		public GY_YONGHUYYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_YONGHUYY GetByID(string yonghuid, string yingyongid)
        {
            var dto = (from yhyy in this.Set<GY_YONGHUYY>()
                       where yhyy.YONGHUID == yonghuid && yhyy.YINGYONGID == yingyongid
                       select yhyy).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }
    }
}
