using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_KANGJUNYWSPSQDRepository : IRepository<GY_KANGJUNYWSPSQD>, IDependency
	{
        /// <summary>
        /// g����ҽ��ID��ȡ����_����ҩ���������뵥
        /// </summary>
        /// <param name="yiZhuID">ҽ��ID</param>
        /// <returns></returns>
        List<GY_KANGJUNYWSPSQD> GetList(string yiZhuID);
        /// <summary>
        /// ���ݲ���סԺid��ȡ����_����ҩ���������뵥
        /// </summary>
        /// <param name="bingRenZYID"></param>
        /// <returns></returns>
        List<GY_KANGJUNYWSPSQD> GetListByBingRenZYID(string bingRenZYID);
    }
}
