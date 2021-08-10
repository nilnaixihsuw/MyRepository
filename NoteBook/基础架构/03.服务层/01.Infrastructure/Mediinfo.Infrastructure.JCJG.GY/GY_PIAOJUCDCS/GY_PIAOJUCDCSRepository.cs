using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_PIAOJUCDCSRepository : RepositoryBase<GY_PIAOJUCDCS>, IGY_PIAOJUCDCSRepository
	{
		public GY_PIAOJUCDCSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 根据票据业务id，及票据类型id获取票据打印次数信息
        /// </summary>
        /// <param name="piaoJuYWID"></param>
        /// <param name="piaoJuLXID"></param>
        /// <returns></returns>
        public List<GY_PIAOJUCDCS> GetList(string piaoJuYWID,string piaoJuLXID )
        {
            var list = this.Set<GY_PIAOJUCDCS>().Where(o => o.PIAOJUYWID == piaoJuYWID && o.PIAOJULXID==piaoJuLXID).ToList().WithContext(this,ServiceContext);
            return list;
        }
    }
}
