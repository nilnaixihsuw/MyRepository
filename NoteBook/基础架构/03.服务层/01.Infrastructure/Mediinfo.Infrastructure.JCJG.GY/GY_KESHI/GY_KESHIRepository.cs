using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_KESHIRepository : RepositoryBase<GY_KESHI>, IGY_KESHIRepository
    {
        public GY_KESHIRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        // Add by songxl on 2019-01-25 10:05   
        public string GetShangJiYWKS(string jiuZhenKS)
        {
            return this.Set<GY_KESHI>().Where(w => w.KESHIID == jiuZhenKS).FirstOrDefaultWithContext(this, ServiceContext).SHANGJIYWKS;
        }

        // Add by songxl on 2019-01-25 10:05
        public List<string> GetKeShiID(string s_ShangJiYWKS)
        {
            return this.Set<GY_KESHI>().Where(o => o.SHANGJIYWKS == s_ShangJiYWKS).Select(p => p.KESHIID).ToList();
        }


        public string GetKeShiMC(string keShiID)
        {
            return Set<GY_KESHI>().FirstOrDefault(o => o.KESHIID == keShiID)?.KESHIMC;
        }

        public List<GY_KESHI> GetKeShiXX(string zuoFeiBZ)
        {
            return this.Set<GY_KESHI>().Where(o => o.ZUOFEIBZ == 0).ToList();
        }

        public List<GY_KESHI> GetYouXiaoKS()
        {
            var r = (from a in this.Set<GY_KESHI>()
                      join b in this.Set<GY_KESHIBQ>() on a.KESHIID equals b.KESHIID
                      where
                      a.ZUOFEIBZ == 0
                      select a).Distinct();
            return r.ToList();
        }

        public List<GY_KESHI> GetKeShi(string keShiID)
        {
            return this.Set<GY_KESHI>().Where(o => o.KESHIID == keShiID&&o.ZUOFEIBZ==0).ToList();
        }

        public List<GY_KESHI> GetKeShiS(string[] keShiIDS)
        {
            return this.Set<GY_KESHI>().Where(o => keShiIDS.Contains(o.KESHIID) && o.ZUOFEIBZ == 0).ToList();
        }

        public List<GY_KESHI> QueryKeShi(string keShiID)
        {
            return this.QuerySet<GY_KESHI>().Where(o => o.KESHIID == keShiID && o.ZUOFEIBZ == 0).ToList();
        }
        public List<string> GetZhuYuanKS()
        {
            var k = (from a in this.Set<GY_KESHI>()
                     join b in this.Set<GY_KESHIBQ>() on a.KESHIID equals b.KESHIID
                     join c in this.Set<GY_BINGQU>() on a.YUANQUID equals c.YUANQUID
                     where a.ZHUYUANBZ == 1 
                     &&  a.YUANQUID == c.YUANQUID 
                     && a.ZUOFEIBZ==0
                     select  b.KESHIID
                     ).Distinct();
            return k.ToList();
        }
    }
}
