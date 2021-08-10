using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CAIDANGJL_NEWRepository : RepositoryBase<GY_CAIDANGJL_NEW>, IGY_CAIDANGJL_NEWRepository
	{
		public GY_CAIDANGJL_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 根据联合主键获取工具栏信息
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="gongNengID"></param>
        /// <param name="caiDanID"></param>
        /// <returns></returns>
        public GY_CAIDANGJL_NEW GetByID(string yingYongID,string gongNengID,string caiDanID)
        {
            var dto = (from gjl in this.Set<GY_CAIDANGJL_NEW>()
                       where gjl.YINGYONGID == yingYongID && gjl.GONGNENGID == gongNengID && gjl.CAIDANID == caiDanID
                       select gjl).FirstOrDefault()?.WithContext(this, ServiceContext);
            return dto;
        }
    }
}
