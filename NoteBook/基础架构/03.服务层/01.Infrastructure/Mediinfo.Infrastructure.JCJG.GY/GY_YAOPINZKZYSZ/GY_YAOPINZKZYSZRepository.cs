using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINZKZYSZRepository : RepositoryBase<GY_YAOPINZKZYSZ>, IGY_YAOPINZKZYSZRepository
	{
		public GY_YAOPINZKZYSZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 获取药品专科专用设置
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZKZYSZ> GetListByJiaGeID(string jiaGeID)
        {
            var list = this.QuerySet<GY_YAOPINZKZYSZ>().Where(p => p.JIAGEID == jiaGeID).ToList().WithContext(this, ServiceContext);

            return list;
        }

        /// <summary>
        /// 获取药品专科专用设置
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="keShiID"></param>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINZKZYSZ> GetListByKeShiID(string jiaGeID, string keShiID, int menZhenZYBZ)
        {
            var list = this.QuerySet<GY_YAOPINZKZYSZ>().Where(p => p.JIAGEID == jiaGeID && p.KESHIID == keShiID && p.MENZHENZYBZ == menZhenZYBZ).ToList().WithContext(this, ServiceContext);

            return list;
        }

        /// <summary>
        /// 获取药品专科专用设置
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="keShiID"></param>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINZKZYSZ> GetListByZhiGongID(string jiaGeID, string zhiGongID, int menZhenZYBZ)
        {
            var list = this.QuerySet<GY_YAOPINZKZYSZ>().Where(p => p.JIAGEID == jiaGeID && p.ZHIGONGID == zhiGongID && p.MENZHENZYBZ == menZhenZYBZ).ToList().WithContext(this, ServiceContext);

            return list;
        }

        /// <summary>
        /// 获取药品专科专用设置
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="keShiID"></param>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINZKZYSZ> GetListByKeShiID(string keShiID, int menZhenZYBZ)
        {
            var list = this.QuerySet<GY_YAOPINZKZYSZ>().Where(p => p.KESHIID == keShiID && p.MENZHENZYBZ == menZhenZYBZ).ToList().WithContext(this, ServiceContext);

            return list;
        }

        /// <summary>
        /// 获取药品专科专用设置
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="keShiID"></param>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINZKZYSZ> GetListByZhiGongID(string zhiGongID, int menZhenZYBZ)
        {
            var list = this.QuerySet<GY_YAOPINZKZYSZ>().Where(p => p.ZHIGONGID == zhiGongID && p.MENZHENZYBZ == menZhenZYBZ).ToList().WithContext(this, ServiceContext);

            return list;
        }
    }
}
