using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BINGRENBLHBRepository : RepositoryBase<GY_BINGRENBLHB>, IGY_BINGRENBLHBRepository
	{
		public GY_BINGRENBLHBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 根据原病案号获取信息
        /// </summary>
        /// <param name="YuanbingAH"></param>
        /// <returns></returns>
        public List<GY_BINGRENBLHB> GetList(string YuanbingAH)
        {
            var list = this.Set<GY_BINGRENBLHB>().Where(w=>w.YUANBINGAH==YuanbingAH).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
 