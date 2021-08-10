using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.ZJ
{
	 public interface IZJ_JIUZHENXXRepository : IRepository<ZJ_JIUZHENXX>, IDependency
	{
        List<ZJ_JIUZHENXX> GetList(List<string> jiuZhengID);
        ZJ_JIUZHENXX GetByJiuZhenID(string jiuZhenID);
        ZJ_JIUZHENXX Get(string guaHaoID);
        /// <summary>
        /// 获取病人当天科室就诊记录数量
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="jiuZhenKS"></param>
        /// <returns></returns>
	    int GetBingRenDTKSJZJLCount(string bingRenID, string jiuZhenKS);
        /// <summary>
        /// 获取病人所有上级科室就诊记录数量
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
	    int GetBingRenSYSJKSJZJLCount(string bingRenID, List<string> list);
        /// <summary>
        /// 返回上次就诊信息
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="jiuZhenID"></param>
        /// <param name="keShiID"></param>
        /// <returns></returns>
        ZJ_JIUZHENXX GetShangJiJZXX(string bingRenID, string jiuZhenID, string keShiID);

        List<ZJ_JIUZHENXX> GetList(List<string> bingRenID, List<string> jiuZhenID, List<string> keShiID);
        /// <summary>
        /// 获取省医保费用合计金额
        /// </summary>
        /// <param name="sfsb"></param>
        /// <returns></returns>
        string GetShengYiBaoFeiYongHeJiJinE(string sfsb);
        /// <summary>
        /// 根据病人ID获取数据
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        List<ZJ_JIUZHENXX> GetAllZJJZXX(List<string> bingRenID);
    }
}
