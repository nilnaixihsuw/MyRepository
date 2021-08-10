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
        /// ���ݲ���סԺid��ȡ����ҩ���������뵥��Ϣ
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
