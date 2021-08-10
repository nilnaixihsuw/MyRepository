using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CHUANGWEIRepository : IRepository<GY_CHUANGWEI>, IDependency
	{


        GY_CHUANGWEI GetByKey(string ChuangWeiID, string BingQuID);
        List<GY_CHUANGWEI> GetWeiZuoFFJCW(string FangJianID);
        List<GY_CHUANGWEI> GetTongFangJianCW(string FangJianID, string ChuangWeiID);
        List<GY_CHUANGWEI> GetBingRenBCList(string BingrenZYID, string ChuangWeiID);
        /// <summary>
        /// 取病人床位信息
        /// </summary>
        /// <param name="BingrenZYID"></param>
        /// <returns></returns>
        List<GY_CHUANGWEI> GetBingRenCWXX(string BingrenZYID);
        /// <summary>
        /// 取房间床位
        /// </summary>
        /// <param name="fangJianID"></param>
        /// <returns></returns>
        List<GY_CHUANGWEI> GetFangJianCW(string fangJianID);
        List<GY_CHUANGWEI> GetChuangWeiIDList(string fangJianID, string chuangWeiID);

        int JianChanCW(string keShiID,ref string message);

        List<GY_CHUANGWEI> GetChuangWeis(string keShiID,string bingQuID);
    }
}
