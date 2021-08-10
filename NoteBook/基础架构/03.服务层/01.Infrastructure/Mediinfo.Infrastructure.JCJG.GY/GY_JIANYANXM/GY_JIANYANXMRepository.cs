using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANXMRepository : RepositoryBase<GY_JIANYANXM>, IGY_JIANYANXMRepository
	{
		public GY_JIANYANXMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 根据检验项目id取导医数据
        /// </summary>
        /// <param name="jianYanXMID"></param>
        /// <returns></returns>
        public List<GY_JIANYANXM> GetJianYanXM(string jianYanXMID)
        {
            var JianYanXMList = this.Set<GY_JIANYANXM>().Where(o => o.JIANYANXMID == jianYanXMID).ToList().WithContext(this, ServiceContext);
            return JianYanXMList;
        }
    }
}
