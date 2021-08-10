using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINCDJG2Repository : IRepository<GY_YAOPINCDJG2>, IDependency
	{
        void SetCache();
        /// <summary>
        /// 根据规格ID及产地取产地价格信息
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string guiGeID,string chanDi);

        /// <summary>
        /// 根据价格ID获取数据
        /// </summary>
        /// <param name="jIAGEID"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string jIAGEID);
        List<GY_YAOPINCDJG2> GetList(List<string> jIAGEID);


        List<GY_YAOPINCDJG2> GetChanDi(string chanDi);

        List<GY_YAOPINCDJG2> GetList(int zfBZ);

        /// <summary>
        /// 药品信息
        /// </summary>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList();

        List<GY_YAOPINCDJG2> GetList(string xtGGID, string cd, int zfBZ = 0, int tyBZ = 0);

        /// <summary>
        /// 根据规格ID及产地取产地价格信息(通过缓存)
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></r
        List<GY_YAOPINCDJG2> GetListFromCache(string guiGeID, string chanDi);

        /// <summary>
        /// 获取同一大规格，同一产地的所有药品
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetXiangTongYPList(string daGuiGeID, string chanDi);

        /// <summary>
        /// 获取同一大规格所有药品
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>

        List<GY_YAOPINCDJG2> GetListByDaGuiGeID(string daGuiGeID);
        /// <summary>
        /// 通过缓存获取药品信息
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        GY_YAOPINCDJG2 GetByKeyFromCache(string jiaGeID);

        /// <summary>
        /// 通过规格获取药品产地信息
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetListByGuiGeID(string guiGeID);

        List<GY_YAOPINCDJG2> GetTongGuiGeList(string jiaGeID);

        /// <summary>
        /// 根据零差价标志获取产地价格2信息
        /// </summary>
        /// <param name="lingChaJBZ"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetListByLingChaJBZ(List<int?> lingChaJBZ);


        /// <summary>
        /// 根据产地及规格id获取产地价格2信息
        /// </summary>
        /// <param name="chanDi">产地</param>
        /// <param name="guiGeIDList">规格ID列表</param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetListByChanDiGGID(string chanDi, List<string> guiGeIDList);



        /// <summary>
        ///  联合YK_CHUKUDAN1、YK_CHUKUDAN2、GY_YAOPINMC和GY_YAOPINFL查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="danJuZT"></param>
        /// <param name="gongHuoBZ"></param>
        /// <param name="zuoFeiBZ"></param>
        /// <param name="tingYongBZ"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT, int gongHuoBZ, int zuoFeiBZ, int tingYongBZ);

        /// <summary>
        /// 联合YK_CHUKUDAN1、YK_CHUKUDAN2查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="danJuZT"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT);

        /// <summary>
        /// 联合GY_QINGLINGDAN1、GY_QINGLINGDAN2和请领类型查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="qingLingZT"></param>
        /// <param name="qingLingLX"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string qingLingZT, string qingLingLX);

        /// <summary>
        /// 联合YK_KUCUN1、GY_YAOPINMC和GY_YAOPINFL查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="gongHuoBZ"></param>
        /// <param name="tingYongBZ"></param>
        /// <param name="zuoFeiBZ"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, int gongHuoBZ, int tingYongBZ, int zuoFeiBZ);

        /// <summary>
        /// 联合GY_QINGLINGDAN1、GY_QINGLINGDAN2查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="qingLingZT"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetYaoPinCDJG2ByQL(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string qingLingZT);

        /// <summary>
        /// 根据价格id列表获取产地价格2信息
        /// </summary>
        /// <param name="jiaGeIDList"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(HashSet<string> jiaGeIDList);

        /// <summary>
        /// 根据药品类型和ID获取产地价格2信息
        /// </summary>
        /// <param name="weiShu"></param>
        /// <param name="yaoPinLX"></param>
        /// <param name="yaoPinID"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(int weiShu, List<string> yaoPinLX, List<string> yaoPinID);

        List<GY_YAOPINCDJG2> GetYaoPinLst(List<string> lstguiGeID);

        List<GY_YAOPINCDJG2> GetListByGuiGeIDQ(string guiGeID);
    }
}
