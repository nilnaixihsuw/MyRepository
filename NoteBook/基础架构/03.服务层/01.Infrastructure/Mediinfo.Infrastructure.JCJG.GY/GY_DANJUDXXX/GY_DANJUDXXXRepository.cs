using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DANJUDXXXRepository : RepositoryBase<GY_DANJUDXXX>, IGY_DANJUDXXXRepository
	{
		public GY_DANJUDXXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 根据单据对象名称及应用ID 获取单据对象
        /// </summary>
        /// <param name="danJuDXMC">单据对象名称</param>
        /// <param name="yingYongID">应用ID</param>
        /// <returns></returns>
        public List<GY_DANJUDXXX> GetList(string danJuDXMC, string yingYongID)
        {
            var list = this.Set<GY_DANJUDXXX>().Where(o => o.DANJUDXMC == danJuDXMC && o.YINGYONGID==yingYongID).ToList();
            return list;
        }
    }
}
