using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	public interface IGY_YOUHUILBRepository : IRepository<GY_YOUHUILB>, IDependency
	{
        /// <summary>
        /// ��ȡȫԺ������Ż���𣨰��������ϵģ�
        /// </summary>
        /// <param name="menZhenZYBZ">0�����1��סԺ</param>
        /// <returns></returns>
        GY_YOUHUILB GetQuanYuanJM(int menZhenZYBZ);

        List<GY_YOUHUILB> GetList();

        GY_YOUHUILB GetGuaHaoSY(string youHuiLB,string danDuYH);

        List<GY_YOUHUILB> GetByKeyFromCache(string youHuiLBID);

    }
}
