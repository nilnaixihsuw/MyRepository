using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_JIARIPAIBANRepository : RepositoryBase<GY_JIARIPAIBAN>, IGY_JIARIPAIBANRepository
    {
        public GY_JIARIPAIBANRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        // Add by songxl on 2019-01-25 13:20   
        public int GetCount(string jiuZhenKS, int zuoZhenLX, string jiuZhenYS)
        {
            int year = GetSYSTime().Year;
            int month = GetSYSTime().Month;
            int day = GetSYSTime().Day;
            string tempLX = zuoZhenLX.ToString();
            return this.Set<GY_JIARIPAIBAN>().Where(o => o.KESHIID == jiuZhenKS &&
                                                       o.GUAHAOLB == tempLX &&
                                                       o.YISHENGID == jiuZhenYS &&
                                                       o.GUAHAORQ.Value.Year == year &&
                                                       o.GUAHAORQ.Value.Month == month &&
                                                         o.GUAHAORQ.Value.Day == day).Count();
        }

        // Add by songxl on 201901-25 15:58   
        public int GetCount(string jiuZhenKS, int zuoZhenLX)
        {
            int year = GetSYSTime().Year;
            int month = GetSYSTime().Month;
            int day = GetSYSTime().Day;
            string tempLX = zuoZhenLX.ToString();
            return this.Set<GY_JIARIPAIBAN>().Count(o => o.KESHIID == jiuZhenKS &&
                                                         o.GUAHAOLB == tempLX &&
                                                         o.YISHENGID == null &&
                                                         o.GUAHAORQ.Value.Year == year &&
                                                         o.GUAHAORQ.Value.Month == month &&
                                                         o.GUAHAORQ.Value.Day == day);
        }

        public int GetCount(int zuoZhenLX)
        {
            int year = GetSYSTime().Year;
            int month = GetSYSTime().Month;
            int day = GetSYSTime().Day;
            string tempLX = zuoZhenLX.ToString();
            return this.Set<GY_JIARIPAIBAN>().Count(o => o.KESHIID == null &&
                                                         o.GUAHAOLB == tempLX &&
                                                         o.YISHENGID == null &&
                                                         o.GUAHAORQ.Value.Year == year &&
                                                         o.GUAHAORQ.Value.Month == month &&
                                                         o.GUAHAORQ.Value.Day == day);
        }


        // Add by songxl on 2019-01-25 13:47   
        public void GetString(string jiuZhenKS, string jiuZhenYS, int zuoZhenLX, out string S_JIEJIARPBID,
            out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB)
        {
            int year = GetSYSTime().Year;
            int month = GetSYSTime().Month;
            int day = GetSYSTime().Day;
            string tempLX = zuoZhenLX.ToString();
            var entity = this.Set<GY_JIARIPAIBAN>().Where(o => o.KESHIID == jiuZhenKS &&
                                                               o.GUAHAOLB == tempLX &&
                                                               o.YISHENGID == jiuZhenYS &&
                                                               o.GUAHAORQ.Value.Year == year &&
                                                               o.GUAHAORQ.Value.Month == month &&
                                                               o.GUAHAORQ.Value.Day == day)
                .Select(p => new { p.JIEJIARPBID, p.GUAOHAOXM, p.ZHENLIAOXM, p.GUAHAOLB })
                .FirstOrDefault();
            S_JIEJIARPBID = entity.JIEJIARPBID;
            S_GUAHAOSFXM2 = entity.GUAOHAOXM;
            S_ZHENLIAOSFXM2 = entity.ZHENLIAOXM;
            S_GUAHAOLB = entity.GUAHAOLB;
        }

        // Add by songxl on 2019-01-25 13:47   
        public void GetString(string jiuZhenKS, int zuoZhenLX, out string S_JIEJIARPBID,
            out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2, out string S_GUAHAOLB)
        {
            int year = GetSYSTime().Year;
            int month = GetSYSTime().Month;
            int day = GetSYSTime().Day;
            string tempLX = zuoZhenLX.ToString();
            var entity = this.Set<GY_JIARIPAIBAN>().Where(o => o.KESHIID == jiuZhenKS &&
                                                               o.GUAHAOLB == tempLX &&
                                                               o.YISHENGID == null &&
                                                               o.GUAHAORQ.Value.Year == year &&
                                                               o.GUAHAORQ.Value.Month == month &&
                                                               o.GUAHAORQ.Value.Day == day)
                .Select(p => new { p.JIEJIARPBID, p.GUAOHAOXM, p.ZHENLIAOXM, p.GUAHAOLB })
                .FirstOrDefault();
            S_JIEJIARPBID = entity.JIEJIARPBID;
            S_GUAHAOSFXM2 = entity.GUAOHAOXM;
            S_ZHENLIAOSFXM2 = entity.ZHENLIAOXM;
            S_GUAHAOLB = entity.GUAHAOLB;
        }

        // Add by songxl on 2019-01-25 13:47   
        public void GetString(int zuoZhenLX, out string S_JIEJIARPBID, out string S_GUAHAOSFXM2, out string S_ZHENLIAOSFXM2,
            out string S_GUAHAOLB)
        {
            int year = GetSYSTime().Year;
            int month = GetSYSTime().Month;
            int day = GetSYSTime().Day;
            string tempLX = zuoZhenLX.ToString();
            var entity = this.Set<GY_JIARIPAIBAN>().Where(o => o.KESHIID == null &&
                                                               o.GUAHAOLB == tempLX &&
                                                               o.YISHENGID == null &&
                                                               o.GUAHAORQ.Value.Year == year &&
                                                               o.GUAHAORQ.Value.Month == month &&
                                                               o.GUAHAORQ.Value.Day == day)
                .Select(p => new { p.JIEJIARPBID, p.GUAOHAOXM, p.ZHENLIAOXM, p.GUAHAOLB })
                .FirstOrDefault();
            S_JIEJIARPBID = entity.JIEJIARPBID;
            S_GUAHAOSFXM2 = entity.GUAOHAOXM;
            S_ZHENLIAOSFXM2 = entity.ZHENLIAOXM;
            S_GUAHAOLB = entity.GUAHAOLB;
        }
    }
}
