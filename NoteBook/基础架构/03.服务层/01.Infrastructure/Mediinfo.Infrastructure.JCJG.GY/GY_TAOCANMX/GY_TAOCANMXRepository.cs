using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_TAOCANMXRepository : RepositoryBase<GY_TAOCANMX>, IGY_TAOCANMXRepository
	{
		public GY_TAOCANMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// �����ײ�ID,��ĿID,��������ȡ�����ײ���ϸ
        /// </summary>
        /// <param name="TaoCanID">�ײ�ID</param>
        /// <param name="XiangMuID">��ĿID</param>
        /// <param name="FeiYongLB">�������</param>
        /// <returns></returns>
        public GY_TAOCANMX GetTaoCanMX(string TaoCanID, string XiangMuID, int? FeiYongLB)
        {
            GY_TAOCANMX doMain = this.Set<GY_TAOCANMX>().Where(w => w.TAOCANID == TaoCanID && w.XIANGMUID == XiangMuID && w.FEIYONGLB == FeiYongLB).FirstOrDefault().WithContext(this, ServiceContext); 
            return doMain;
        }
    }
}
