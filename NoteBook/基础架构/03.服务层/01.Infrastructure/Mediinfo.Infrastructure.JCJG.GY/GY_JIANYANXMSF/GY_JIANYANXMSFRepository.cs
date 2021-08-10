using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANXMSFRepository : RepositoryBase<GY_JIANYANXMSF>, IGY_JIANYANXMSFRepository
	{
		public GY_JIANYANXMSFRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 取检验项目收费信息，根据检验项目ID
        /// </summary>
        /// <param name="jianYanXMID">检验项目ID</param>
        /// <returns></returns>
        public List<GY_JIANYANXMSF> GetListByJYXMID(string jianYanXMID)
        {
          return  this.Set<GY_JIANYANXMSF>().Where(o => o.JIANYANXMID == jianYanXMID).ToList();
        }
    }
}
