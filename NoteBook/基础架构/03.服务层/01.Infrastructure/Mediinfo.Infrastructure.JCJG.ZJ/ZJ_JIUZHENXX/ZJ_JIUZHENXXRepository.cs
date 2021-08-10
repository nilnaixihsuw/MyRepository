using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.ZJ;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.Infrastructure.JCJG.ZJ
{
    public class ZJ_JIUZHENXXRepository : RepositoryBase<ZJ_JIUZHENXX>, IZJ_JIUZHENXXRepository
    {
        public ZJ_JIUZHENXXRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }



        public List<ZJ_JIUZHENXX> GetList(List<string> jiuZhengID)
        {
            var list = (from p in this.Set<ZJ_JIUZHENXX>()
                        where jiuZhengID.Contains(p.JIUZHENID)
                        select p).ToList();
            return list;
        }
        public ZJ_JIUZHENXX GetByJiuZhenID(string jiuZhenID)
        {
            return this.Set<ZJ_JIUZHENXX>().Where(w => w.JIUZHENID == jiuZhenID).FirstOrDefaultWithContext(this, ServiceContext);
        }
        public ZJ_JIUZHENXX Get(string guaHaoID)
        {
            return this.Set<ZJ_JIUZHENXX>().Where(w => w.GUAHAOID == guaHaoID).FirstOrDefaultWithContext(this, ServiceContext);
        }
        /// <summary>
        /// 获取病人当天科室就诊记录数量
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="jiuZhenKS"></param>
        /// <returns></returns>
        public int GetBingRenDTKSJZJLCount(string bingRenID, string jiuZhenKS)
        {
            DateTime dateTime1 = Convert.ToDateTime(GetSYSTime().AddDays(-1).ToString("yyyy-MM-dd") + " " + "00:00:00");
            DateTime dateTime2 = Convert.ToDateTime(GetSYSTime().AddDays(-1).ToString("yyyy-MM-dd") + " " + "23:59:59");

            return this.Set<ZJ_JIUZHENXX>().Count(o => o.BINGRENID == bingRenID &&
                                                       o.ZUOFEIBZ == 0 &&
                                                       o.GUAHAOLBGL != "2" &&
                                                       o.JIUZHENKS == jiuZhenKS &&
                                                       (o.JILULY == "0" || o.JILULY == "1") &&
                                                       o.XITONGSJ > dateTime1 &&
                                                       o.XITONGSJ < dateTime2);
        }
        /// <summary>
        /// 获取病人所有上级科室就诊记录数量
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int GetBingRenSYSJKSJZJLCount(string bingRenID, List<string> list)
        {
            DateTime dateTime = Convert.ToDateTime(GetSYSTime().ToString("yyyy-MM-dd") + " " + "00:00:00");

            return this.Set<ZJ_JIUZHENXX>().Count(o => o.BINGRENID == bingRenID &&
                                                       o.ZUOFEIBZ == 0 &&
                                                       o.GUAHAOLBGL != "2" &&
                                                       o.JIUZHENRQ > dateTime &&
                                                       list.Contains(o.JIUZHENKS));
        }
        /// <summary>
        /// 返回上次就诊信息
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="jiuZhenID"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX GetShangJiJZXX(string bingRenID, string jiuZhenID, string keShiID)
        {
            return this.Set<ZJ_JIUZHENXX>().Where(o => o.BINGRENID == bingRenID && o.JIUZHENID != jiuZhenID && o.JIUZHENKS == keShiID).OrderByDescending(o => o.JIUZHENRQ).FirstOrDefault();
        }
        public List<ZJ_JIUZHENXX> GetList(List<string> bingRenID, List<string> jiuZhenID, List<string> keShiID)
        {
            return this.QuerySet<ZJ_JIUZHENXX>().Where(w => bingRenID.Contains(w.BINGRENID) && keShiID.Contains(w.JIUZHENKS)).ToList();
        }
        /// <summary>
        /// 获取省医保起付标准累计
        /// </summary>
        /// <param name="sfsb"></param>
        /// <returns></returns>
        public string GetShengYiBaoFeiYongHeJiJinE(string sfsb)
        {
            string heJiJE = "";
            var dataTable = this.GetDataTable("Select Flhjje From Mz_Sfflxx Where Sfsb = " + sfsb +
                                              " And Flywmc = '*QIFUBZLJ176'");
            if (dataTable.Rows.Count > 0)
            {
                heJiJE = dataTable.Rows[0]["FLHJJE"].ToStringEx();
            }

            return heJiJE;
        }

        /// <summary>
        /// 根据病人ID获取数据
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        public List<ZJ_JIUZHENXX> GetAllZJJZXX(List<string> bingRenID)
        {
            return this.QuerySet<ZJ_JIUZHENXX>().Where(w => bingRenID.Contains(w.JIUZHENID)).ToList();
        }
    }
}
