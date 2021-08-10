using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMDYSMRepository : RepositoryBase<GY_JIANCHAXMDYSM>, IGY_JIANCHAXMDYSMRepository
	{
		public GY_JIANCHAXMDYSMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 取检查项目导医说明
        /// </summary>
        /// <param name="jianChaXMID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public List<GY_JIANCHAXMDYSM> GetJianChaXMDYSM(string jianChaXMID, string yingYongID)
        {
            var DaoYiDanList = this.Set<GY_JIANCHAXMDYSM>().Where(o => o.JIANCHAXMID == jianChaXMID && o.YINGYONGID == yingYongID).ToList().WithContext(this, ServiceContext);
            return DaoYiDanList;
        }
    }
}
