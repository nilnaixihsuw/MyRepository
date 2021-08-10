using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity; 

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZHANGHUJYMXRepository : RepositoryBase<GY_ZHANGHUJYMX>, IGY_ZHANGHUJYMXRepository
	{
		public GY_ZHANGHUJYMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 检查账户交易存在未确认数据
        /// </summary>
        /// <param name="shouFeiRen"></param>
        /// <param name="moRenSFR"></param>
        /// <returns></returns>
        public bool CheckZhangHuJYMX(string shouFeiRen, string moRenSFR)
        {
            GY_ZHANGHUJYMX domain = new GY_ZHANGHUJYMX();
            if (shouFeiRen == moRenSFR)
            {
                domain = (from a in Set<GY_ZHANGHUJYMX>()
                          where
                              (a.CAOZUOYUAN == shouFeiRen ||
                              (!a.YINGYONGID.StartsWith("23") &&
                              !a.YINGYONGID.StartsWith("01"))) &&
                              a.QUERENBZ == 0 &&
                              (a.YEWULX == "消费" ||
                              a.YEWULX == "退费") &&
                              a.RIBAOID == "NULL" &&
                              a.TUIFEIBZ == 0
                          select a).FirstOrDefault();
            }
            else
            {
                domain = (from a in Set<GY_ZHANGHUJYMX>()
                          where a.QUERENBZ == 0
                          && (a.YEWULX == "消费" || a.YEWULX == "退费")
                          && a.RIBAOID == "NULL"
                          && a.TUIFEIBZ == 0
                          && a.CAOZUOYUAN == shouFeiRen
                          select a).FirstOrDefault();
            }
            if (domain!=null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 获取已提交日报账户交易信息
        /// </summary>
        /// <param name="riBaoID">日报id</param>
        /// <returns></returns>
        public List<GY_ZHANGHUJYMX> GetYiTiJRB(string riBaoID)
        {
            var zhangHuJYMX = this.Set<GY_ZHANGHUJYMX>().Where(w => w.RIBAOID == riBaoID).ToList().WithContext(this, ServiceContext);
            return zhangHuJYMX;
        }
        /// <summary>
        /// 根据门诊ID 获取门诊结算正交易信息
        /// </summary>
        /// <param name="menZhenID"></param>
        /// <returns></returns>
        public List<GY_ZHANGHUJYMX> GetMenZhenJSZJYXX(string menZhenID)
        {
            var zhangHuJYMX = this.Set<GY_ZHANGHUJYMX>().Where(o => o.MENZHENID == menZhenID && o.GUAHAOBZ == 0 && o.ZUOFEIBZ == 0 && o.TUIFEIBZ == 0 ).ToList().WithContext(this, ServiceContext);
            return zhangHuJYMX;
        }


        /// <summary>
        /// 根据病人id取交易金额总和
        /// </summary>
        /// <param name="bingRenID">病人id</param>
        /// <returns></returns>
        public decimal? GetJiaoYiJE(string bingRenID)
        {
            var jiaoYiJE = this.Set<GY_ZHANGHUJYMX>().Where(o => o.BINGRENID == bingRenID && o.ZHANGHUID == "4").ToList().Select(o => o.JIAOYIJE).Sum()??0;
            return jiaoYiJE;
        }
    }
}
