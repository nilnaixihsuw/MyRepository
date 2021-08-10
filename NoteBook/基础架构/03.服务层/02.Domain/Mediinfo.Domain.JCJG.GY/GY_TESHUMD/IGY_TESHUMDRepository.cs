using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_TESHUMDRepository : IRepository<GY_TESHUMD>, IDependency
	{
        /// <summary>
        ///���ݲ���ID,������𣬵�ǰ״̬��ȡ��������Ϣ
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="mingDanLB"></param>
        /// <param name="dangQianZT"></param>
        /// <returns></returns>
        List<GY_TESHUMD> GetList(string bingRenID, int mingDanLB, int dangQianZT);

    }
}
