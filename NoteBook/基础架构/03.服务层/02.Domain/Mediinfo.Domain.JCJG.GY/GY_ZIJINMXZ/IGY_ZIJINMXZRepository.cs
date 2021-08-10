using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZIJINMXZRepository : IRepository<GY_ZIJINMXZ>, IDependency
	{
        /// <summary>
        /// 根据账户id取资金明细账信息
        /// </summary>
        /// <param name="zhangHuID">账户id</param>
        /// <returns>资金明细账list</returns>
        List<GY_ZIJINMXZ> GetList(string zhangHuID);

        /// <summary>
        /// 根据账户id取未结转资金明细账信息
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetWeiJieZMXZList(string zhangHuID);
        /// <summary>
        /// 根据账户id取所有的发生金额总和
        /// </summary>
        /// <param name="zhangHuID">账户id</param>
        /// <returns>发生金额总和</returns>
        decimal GetFaShengJE(string zhangHuID);


        /// <summary>
        /// 根据账户id取门诊预交款的发生金额总和（JIAOYIFS=="3"：门诊预交款   9：欠费管理）
        /// </summary>
        /// <param name="zhangHuID">账户id</param>
        /// <returns>门诊预交款的发生金额总和</returns>
        decimal GetMZYuJiaoKJE(string zhangHuID);

        /// <summary>
        /// 根据账户id 欠费标志 取充值明细账
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="qianFeiBZ"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetChongZhiMXZ(string zhangHuID, int qianFeiBZ);

        /// <summary>
        /// 根据账户id 支付方式，取现标志，欠费标志 取充值明细账
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="zhiFuFS"></param>
        /// <param name="quXianBZ"></param>
        /// <param name="qianFeiBZ"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetChongZhiMXZ(string zhangHuID, string zhiFuFS, int quXianBZ, int qianFeiBZ);

        /// <summary>
        /// 根据账户ID 结算ID,欠费标志获取要退费的 资金明细账
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="jieSuanID"></param>
        /// <param name="qianFeiZT"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetTuiFeiZJMXZ(string zhangHuID, string jieSuanID, int qianFeiZT);
    }
}
