using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_JIZHENPAIBANRepository : RepositoryBase<GY_JIZHENPAIBAN>, IGY_JIZHENPAIBANRepository
    {
        public GY_JIZHENPAIBANRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public int GetCount(string jiuZhenKS, int zuoZhenLX, string jiuZhenYS)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string tempLX = zuoZhenLX.ToString();
            return this.Set<GY_JIZHENPAIBAN>().Count(o => o.KESHIID == jiuZhenKS &&
                                                          o.GUAHAOLB == tempLX  &&
                                                          o.YISHENGID == jiuZhenYS &&
                                                          o.ZUOFEIBZ == 0 &&
                                                          o.KAISHISJ < dateTime &&
                                                          o.JIESHUSJ > dateTime);
        }

        public int GetCount(string jiuZhenKS, int zuoZhenLX)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string tempLX = zuoZhenLX.ToString();
            return this.Set<GY_JIZHENPAIBAN>().Count(o => o.KESHIID == jiuZhenKS &&
                                                         o.GUAHAOLB == tempLX &&
                                                         o.YISHENGID == null &&
                                                          o.ZUOFEIBZ == 0 &&
                                                          o.KAISHISJ < dateTime &&
                                                          o.JIESHUSJ > dateTime);
        }

        public int GetCount(int zuoZhenLX)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string tempLX = zuoZhenLX.ToString();
            return this.Set<GY_JIZHENPAIBAN>().Count(o => o.KESHIID == null &&
                                                          o.GUAHAOLB == tempLX &&
                                                          o.YISHENGID == null &&
                                                          o.ZUOFEIBZ == 0 &&
                                                          o.KAISHISJ < dateTime &&
                                                          o.JIESHUSJ > dateTime);
        }

        public void GetString(string jiuZhenKS, int zuoZhenLX, string jiuZhenYS, out string S_JIZHENPBID, out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string tempLX = zuoZhenLX.ToString();
            var entity = this.Set<GY_JIZHENPAIBAN>().Where(o => o.KESHIID == jiuZhenKS &&
                                                                o.GUAHAOLB == tempLX &&
                                                                o.YISHENGID == jiuZhenYS &&
                                                                o.ZUOFEIBZ == 0 &&
                                                                o.KAISHISJ < dateTime &&
                                                                o.JIESHUSJ > dateTime)
                .Select(p => new { p.JIZHENPBID, p.GUAOHAOXM, p.ZHENLIAOXM, p.GUAHAOLB })
                .FirstOrDefault();
            S_JIZHENPBID = entity.JIZHENPBID;
            S_GUAHAOSFXM2 = entity.GUAOHAOXM;
            S_ZHENLIAOSFXM2 = entity.ZHENLIAOXM;
            S_GUAHAOLB = entity.GUAHAOLB;
        }

        public void GetString(string jiuZhenKS, int zuoZhenLX, out string S_JIZHENPBID, out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string tempLX = zuoZhenLX.ToString();
            var entity = this.Set<GY_JIZHENPAIBAN>().Where(o => o.KESHIID == jiuZhenKS &&
                                                                o.GUAHAOLB == tempLX &&
                                                                o.YISHENGID == null &&
                                                                o.ZUOFEIBZ == 0 &&
                                                                o.KAISHISJ < dateTime &&
                                                                o.JIESHUSJ > dateTime)
                .Select(p => new { p.JIZHENPBID, p.GUAOHAOXM, p.ZHENLIAOXM, p.GUAHAOLB })
                .FirstOrDefault();
            S_JIZHENPBID = entity.JIZHENPBID;
            S_GUAHAOSFXM2 = entity.GUAOHAOXM;
            S_ZHENLIAOSFXM2 = entity.ZHENLIAOXM;
            S_GUAHAOLB = entity.GUAHAOLB;
        }

        public void GetString(int zuoZhenLX, out string S_JIZHENPBID, out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string tempLX = zuoZhenLX.ToString();
            var entity = this.Set<GY_JIZHENPAIBAN>().Where(o => o.KESHIID == null &&
                                                                o.GUAHAOLB == tempLX &&
                                                                o.YISHENGID == null &&
                                                                o.ZUOFEIBZ == 0 &&
                                                                o.KAISHISJ < dateTime &&
                                                                o.JIESHUSJ > dateTime)
                .Select(p => new { p.JIZHENPBID, p.GUAOHAOXM, p.ZHENLIAOXM, p.GUAHAOLB })
                .FirstOrDefault();
            S_JIZHENPBID = entity.JIZHENPBID;
            S_GUAHAOSFXM2 = entity.GUAOHAOXM;
            S_ZHENLIAOSFXM2 = entity.ZHENLIAOXM;
            S_GUAHAOLB = entity.GUAHAOLB;
        }
    }
}
