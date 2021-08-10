using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YINGYONGRepository : IRepository<GY_YINGYONG>, IDependency
	{
        List<GY_YINGYONG> GetList(string xiTongID);

        /// <summary>
        /// 取药库房应用
        /// </summary>
        /// <returns></returns>
        List<GY_YINGYONG> GetList();

        List<GY_YINGYONG> GetListS(string[] yingYongIDS);

        List<GY_YINGYONG> GetByKeyFromCache(string yingYongID);

        string GetDanJuHao(string prmYingYongID, string prmXuHaoMC);

        /// <summary>
        /// 公用控件取应用数据
        /// </summary>
        /// <returns></returns>
        List<GY_YINGYONG> GetGYList();
    }
}
