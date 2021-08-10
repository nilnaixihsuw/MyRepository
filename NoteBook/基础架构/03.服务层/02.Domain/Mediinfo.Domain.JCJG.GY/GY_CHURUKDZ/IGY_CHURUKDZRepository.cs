using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
using System.Linq;

namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CHURUKDZRepository : IRepository<GY_CHURUKDZ>, IDependency
	{
        List<GY_CHURUKDZ> GetList(string yingYongID2);

        List<GY_CHURUKDZ> GetList();

        List<ChuRuKYYDZ> GetChuRuKYYDZList(List<string> yingYongIDList);
       
        /// <summary>
        /// 获取出入库对照
        /// </summary>
        /// <param name="yingYongID2">出库应用ID</param>
        /// <param name="fangShiID2">出库方式ID</param>
        /// <returns></returns>
        List<GY_CHURUKDZ> GetList(string yingYongID2, string fangShiID2);
    }
    public class ChuRuKYYDZ
    {
        public string YINGYONGID { get; set; }
        public string KESHIID { get; set; }
    }
}
