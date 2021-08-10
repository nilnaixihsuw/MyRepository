using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DANWEIRepository : RepositoryBase<GY_DANWEI>, IGY_DANWEIRepository
	{
		public GY_DANWEIRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 获取供货单位
        /// </summary>
        /// <param name="yingYingID"></param>
        /// <param name="danWeiID"></param>
        /// <returns></returns>
        public List<GY_DANWEI> GetList(string yingYingID, string danWeiID)
        {
            var list = this.Set<GY_DANWEI>().Where(o => o.DANWEIID == danWeiID && o.YINGYONGID == yingYingID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
