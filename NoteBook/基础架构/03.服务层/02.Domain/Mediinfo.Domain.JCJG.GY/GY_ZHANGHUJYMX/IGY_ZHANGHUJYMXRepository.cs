using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZHANGHUJYMXRepository : IRepository<GY_ZHANGHUJYMX>, IDependency
	{
        List<GY_ZHANGHUJYMX> GetYiTiJRB(string riBaoID);
        bool CheckZhangHuJYMX(string shouFeiRen,string moRenSFR);

        /// <summary>
        /// ��������ID ��ȡ���������������Ϣ
        /// </summary>
        /// <param name="menZhenID"></param>
        /// <returns></returns>
        List<GY_ZHANGHUJYMX> GetMenZhenJSZJYXX(string menZhenID);
        /// <summary>
        /// ���ݲ���idȡ���׽���ܺ�
        /// </summary>
        /// <param name="bingRenID">����id</param>
        /// <returns></returns>
        decimal? GetJiaoYiJE(string bingRenID);
    }
}
