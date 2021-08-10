using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_QINGLINGDAN2Repository : IRepository<GY_QINGLINGDAN2>, IDependency
	{
        /// <summary>
        /// 联合请领单1查询请购药品
        /// </summary>
        /// <param name="qingLingDID"></param>
        /// <param name="shengQingKSSJ"></param>
        /// <param name="shengQingKSJS"></param>
        /// <returns></returns>
        List<GY_QINGLINGDAN2> GetList(string qingLingDID, DateTime shengQingKSSJ, DateTime shengQingKSJS);

        /// <summary>
        /// 联合请领单1和请领状态查询请购药品
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="beiQingLDYYID"></param>
        /// <param name="shengQingKSSJ"></param>
        /// <param name="shengQingKSJS"></param>
        /// <param name="qingLingZT"></param>
        /// <returns></returns>
        List<GY_QINGLINGDAN2> GetList(string jiaGeID,string beiQingLDYYID, DateTime shengQingKSSJ, DateTime shengQingKSJS, string qingLingZT);

        /// <summary>
        /// 根据价格ID获取请领单明细
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        List<GY_QINGLINGDAN2> GetList(string jiaGeID);
    }
}
