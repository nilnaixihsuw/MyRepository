using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YILIAOZU2Repository : IRepository<GY_YILIAOZU2>, IDependency
	{
        GY_YILIAOZU2 GetByID(string yiliaozid, string zhigongid);

        /// <summary>
        /// ����ҽ����IDȡҽ����List
        /// </summary>
        /// <param name="yiliaozid"></param>
        /// <returns></returns>
        List<GY_YILIAOZU2> GetList(string yiliaozid);

        /// <summary>
        /// ����ְ��id��ȡҽ������Ϣ xieyz 2019-06-22
        /// </summary>
        /// <param name="zhiGongID"></param>
        /// <returns></returns>
        List<GY_YILIAOZU2> GetListByZhiGongID(string zhiGongID);

        List<GY_YILIAOZU2> GetListByYiLiaoZu(List<string> yiliaozu);
    }
}
