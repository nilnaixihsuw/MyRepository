using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CANSHURepository : RepositoryBase<GY_CANSHU>, IGY_CANSHURepository
	{
		public GY_CANSHURepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        ///// <summary>
        ///// 获取参数（按照应用ID->系统ID->00的顺序获取）
        ///// </summary>
        ///// <param name="Context">DBContext</param>
        ///// <param name="yingYongId">应用ID，如果为空则取“00”的参数</param>
        ///// <param name="canShuId">参数ID</param>
        ///// <param name="defaultValue">参数默认值</param>
        ///// <returns>参数值</returns>
        //public   string GetCanShu(  string yingYongId, string canShuId, string defaultValue)
        //{
        //    if (string.IsNullOrWhiteSpace(canShuId))
        //        throw new DBException("参数ID为空！");

        //    if (string.IsNullOrWhiteSpace(yingYongId))
        //        yingYongId = "00";

        //    //先从缓存里面取
        //    string canShuZhi = "";

        //    //if (HISCacheManager.GetCanShu(yingYongId, canShuId, ref canShuZhi))
        //    //    return canShuZhi;

        //    //取所有应用的参数
        //    var canShuList = this.Set<GY_CANSHU>() 
        //                        .Where(c => c.CANSHUID == canShuId)
        //                        .Select(c => (new
        //                        {
        //                            c.CANSHUID,
        //                            c.CANSHUZHI,
        //                            c.YINGYONGID
        //                        })).ToList(); 

        //    var canShu = canShuList.Where(c => c.YINGYONGID == yingYongId).FirstOrDefault();

        //    if (null != canShu)
        //    {
        //        return canShu.CANSHUZHI;
        //    }
        //    else if (yingYongId == "00") //如果应用ID为空，则表示应用ID没有传入
        //    {
        //        return defaultValue;
        //    } 

        //    canShu = canShuList.Where(c => c.YINGYONGID == yingYongId.Substring(0, 2)).FirstOrDefault();

        //    if (null != canShu)
        //    {
        //        return canShu.CANSHUZHI;
        //    }

        //    canShu = canShuList.Where(c => c.YINGYONGID == "00").FirstOrDefault();

        //    if (null != canShu)
        //    {
        //        return canShu.CANSHUZHI;
        //    }

        //    return defaultValue;
        //}

        /// <summary>
        /// 根据参数ID 取所有应用的参数信息
        /// </summary>
        /// <param name="canShuID"></param>
        /// <returns></returns>
        public List<GY_CANSHU> GetList(string canShuID)
        {
            var list = this.Set<GY_CANSHU>().Where(o => o.CANSHUID == canShuID).ToList();
            return list;
        }

        public GY_CANSHU GetParamByKey(string yingYongID, string canShuID)
        {
            var entity = this.Set<GY_CANSHU>()
                .FirstOrDefault(o => o.YINGYONGID == yingYongID && o.CANSHUID == canShuID);
            return entity;
        }
    }
}
