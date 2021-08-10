using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_KANGJUNYWSPSQDRepository : RepositoryBase<GY_KANGJUNYWSPSQD>, IGY_KANGJUNYWSPSQDRepository
	{
		public GY_KANGJUNYWSPSQDRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_KANGJUNYWSPSQD> GetList(string yiZhuID)
        {
            var list = this.QuerySet<GY_KANGJUNYWSPSQD>().Where(o => o.YIZHUID == yiZhuID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 根据病人住院id获取抗菌药物审批申请单信息
        /// </summary>
        /// <param name="bingRenZYID"></param>
        /// <returns></returns>
        public List<GY_KANGJUNYWSPSQD> GetListByBingRenZYID(string bingRenZYID)
        {
            var list = this.QuerySet<GY_KANGJUNYWSPSQD>().Where(o => o.BINGRENZYID == bingRenZYID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
