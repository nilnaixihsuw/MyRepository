using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DAIMARepository : IRepository<GY_DAIMA>, IDependency
	{
        List<GY_DAIMA> GetList(string daiMaLB, string daiMaId);

        /// <summary>
        /// 根据代码类别获取代码信息
        /// </summary>
        /// <param name="daiMaLB"></param>
        /// <returns></returns>
        List<GY_DAIMA> GetList(string daiMaLB);

        /// <summary>
        /// 获取公用频次类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int? GetPinCiLX(string key);


        /// <summary>
        /// 公用控件取代码数据
        /// </summary>
        /// <returns></returns>
        List<GY_DAIMA> GetGYList(string DAIMALB, int MENZHENSY, int ZHUYUANSY, int ZUOFEIBZ);

	    string GetGuaHaoLB(string leiBieGL);

	    int GetZiFu1(string S_GUAHAOLB);

        /// <summary>
        /// 取长期医嘱的频次类型
        /// </summary>
        /// <param name="pinCiLX"></param>
        /// <returns></returns>
        List<string> GetYiZhuLXList(int pinCiLX);

        /// <summary>
        /// 由频次ID取频次信息
        /// </summary>
        /// <param name="pinCiLX"></param>
        /// <returns></returns>
        List<GY_DAIMA> GetPinCiList(string pinCiID);

        /// <summary>
        /// 根据代码类别获取未作废代码信息 --xieyz 2019-05-31
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        List<GY_DAIMA> GetListBydaiMaLB(string daiMaLB);

        /// <summary>
        /// 获取手术代码
        /// </summary>
        /// <returns></returns>
         List<GY_DAIMA> GetShuHouDMList();

        int GetShunXuHao(string daimalb);

        bool CheckDaiMaID(string daimaid, string daimalb);
    }
}
