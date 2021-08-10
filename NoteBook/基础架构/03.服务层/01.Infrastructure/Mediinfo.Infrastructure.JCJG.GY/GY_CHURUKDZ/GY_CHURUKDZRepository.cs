using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_CHURUKDZRepository : RepositoryBase<GY_CHURUKDZ>, IGY_CHURUKDZRepository
    {
        public GY_CHURUKDZRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public List<GY_CHURUKDZ> GetList(string yingYongID2)
        {
            var list = this.Set<GY_CHURUKDZ>().Where(o => o.YINGYONGID2 == yingYongID2).ToList().WithContext(this, ServiceContext);
            return list;
        } 

        public List<GY_CHURUKDZ> GetList()
        {
            var list = this.Set<GY_CHURUKDZ>().ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<ChuRuKYYDZ> GetChuRuKYYDZList(List<string> yingYongIDList)
        {
            List<ChuRuKYYDZ> list = (from churkdz in this.Set<GY_CHURUKDZ>()
                            join yingyongdy in this.Set<GY_YINGYONG>()
                            on churkdz.YINGYONGID1 equals yingyongdy.YINGYONGID
                            where yingYongIDList.Contains(churkdz.YINGYONGID1)
                            select new ChuRuKYYDZ
                            {
                                YINGYONGID = churkdz.YINGYONGID1,
                                KESHIID = yingyongdy.KESHIID
                            }).Distinct().ToList();
            return list;
        }

        /// <summary>
        /// 获取出入库对照
        /// </summary>
        /// <param name="yingYongID2">出库应用ID</param>
        /// <param name="fangShiID2">出库方式ID</param>
        /// <returns></returns>
        public List<GY_CHURUKDZ> GetList(string yingYongID2, string fangShiID2)
        {
            var list = this.Set<GY_CHURUKDZ>().Where(o => o.YINGYONGID2 == yingYongID2 && o.CHUKUFSID2==fangShiID2).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
    
}
