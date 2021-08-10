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
        /// 根据门诊ID 获取门诊结算正交易信息
        /// </summary>
        /// <param name="menZhenID"></param>
        /// <returns></returns>
        List<GY_ZHANGHUJYMX> GetMenZhenJSZJYXX(string menZhenID);
        /// <summary>
        /// 根据病人id取交易金额总和
        /// </summary>
        /// <param name="bingRenID">病人id</param>
        /// <returns></returns>
        decimal? GetJiaoYiJE(string bingRenID);
    }
}
