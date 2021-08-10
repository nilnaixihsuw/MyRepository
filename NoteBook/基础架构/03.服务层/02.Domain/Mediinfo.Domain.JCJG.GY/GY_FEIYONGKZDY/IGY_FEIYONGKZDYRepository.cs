using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	public interface IGY_FEIYONGKZDYRepository : IRepository<GY_FEIYONGKZDY>, IDependency
	{
        /// <summary>
        /// ����δ���Ϸ��ÿ��ƶ�Ӧ
        /// </summary>
        /// <param name="yingYongID">Ӧ��ID</param>
        /// <param name="feiYongXZ">��������</param>
        /// <returns></returns>
        GY_FEIYONGKZDY GetWeiZuoFei(string yingYongID, string feiYongXZ);
	}
}
