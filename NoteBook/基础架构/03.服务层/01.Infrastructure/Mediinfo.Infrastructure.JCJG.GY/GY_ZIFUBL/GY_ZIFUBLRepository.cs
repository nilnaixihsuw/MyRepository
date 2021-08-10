using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZIFUBLRepository : RepositoryBase<GY_ZIFUBL>, IGY_ZIFUBLRepository
	{
		public GY_ZIFUBLRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 获取自负比例
        /// </summary>
        /// <param name="feiYongKZID">费用控制Id</param>
        /// <param name="xiangMuID">项目ID</param>
        /// <param name="xiangMuLX">项目类型</param>
        /// <param name="faShengRQ">费用的发生日期</param>
        /// <param name="menZhenZYBZ">门诊住院标志（0：门诊，1：住院）</param>
        /// <returns>true：表示取到了自负比例，false：表示未取到自负比例</returns>
        public bool GetZiFuBL(string feiYongKZID, string xiangMuID, string xiangMuLX, 
                              DateTime faShengRQ, int menZhenZYBZ,out decimal ziFuBL)
        {
            ziFuBL = 1.0m;

            var query = this.QuerySet<GY_ZIFUBL>()
                            .Where(c => c.FEIYONGKZID == feiYongKZID
                                 && c.XIANGMUID == xiangMuID
                                 && c.XIANGMULX == xiangMuLX);

            if (menZhenZYBZ == 0)
                query = query.Where(c => c.MENZHENSY == 1);
            else
                query = query.Where(c => c.ZHUYUANSY == 1);

            //根据开始、结束日期进行判断
            DateTime dtBegin = faShengRQ.AddDays(-1);
            DateTime dtEnd = faShengRQ.AddDays(1);
            query = query.Where(c =>( c.KAISHIRQ.HasValue ? c.KAISHIRQ : dtBegin) <= faShengRQ 
                                  &&( c.JIESHURQ.HasValue ? c.JIESHURQ : dtEnd) >= faShengRQ);

            var ziFuBLEntity = query.OrderByDescending(c => c.XIUGAISJ).Select(c => c.ZIFUBL).FirstOrDefault();

            if (null == ziFuBLEntity)
            {
                return false;
            }
            else
            {
                //如果为空，则返回自负比例为1
                ziFuBL = ziFuBLEntity.HasValue ? ziFuBLEntity.Value : 1.0m;
                return true;
            }
        }

        public bool GetZiFuBL(string xiangMuID,string feiYongXZ, out decimal ziFuBL)
        {
            ziFuBL = 1.0m;
            var ziFuBLEntity = this.QuerySet<GY_ZIFUBL>().FirstOrDefault(c => c.FEIYONGKZID == feiYongXZ
                                && c.XIANGMUID == xiangMuID);
             
            if (null == ziFuBLEntity)
            {
                return false;
            }
            else
            {
                //如果为空，则返回自负比例为1
                ziFuBL = ziFuBLEntity.ZIFUBL?? 1.0m;
                return true;
            }
        }

        public List<GY_ZIFUBL> GetList(List<string> xiangMuIDS, string feiYongXZ)
        {
            return this.QuerySet<GY_ZIFUBL>().Where(w => xiangMuIDS.Contains(w.XIANGMUID) && w.FEIYONGKZID == feiYongXZ).ToList();
        }
    }
}
