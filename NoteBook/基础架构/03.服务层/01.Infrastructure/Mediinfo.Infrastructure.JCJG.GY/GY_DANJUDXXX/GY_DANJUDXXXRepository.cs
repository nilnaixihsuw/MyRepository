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
        /// ���ݵ��ݶ������Ƽ�Ӧ��ID ��ȡ���ݶ���
        /// </summary>
        /// <param name="danJuDXMC">���ݶ�������</param>
        /// <param name="yingYongID">Ӧ��ID</param>
        /// <returns></returns>
        public List<GY_DANJUDXXX> GetList(string danJuDXMC, string yingYongID)
        {
            var list = this.Set<GY_DANJUDXXX>().Where(o => o.DANJUDXMC == danJuDXMC && o.YINGYONGID==yingYongID).ToList();
            return list;
        }
    }
}
