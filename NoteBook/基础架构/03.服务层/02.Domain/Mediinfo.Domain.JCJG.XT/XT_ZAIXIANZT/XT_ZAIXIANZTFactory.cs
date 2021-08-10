using System;
using System.Linq;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;

namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_ZAIXIANZTFactory
	{
        /*
		 
		public static XT_ZAIXIANZT CreateIfNotExists(IXT_ZAIXIANZTRepository irep, ServiceContext sContext, E_XT_ZAIXIANZT dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_ZAIXIANZT();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 创建在线状态
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static XT_ZAIXIANZT Create(IXT_ZAIXIANZTRepository irep, E_XT_ZAIXIANZT e_XT_ZAIXIANZT)
        {
            DateTime now = irep.GetSYSTime();
            XT_ZAIXIANZT entity = new XT_ZAIXIANZT();
            entity.ZHUANGTAIID = irep.GetOrder("XT_ZAIXIANZT").First();
            entity.ZHIGONGID = e_XT_ZAIXIANZT.ZHIGONGID;
            entity.ZHIGONGGH = e_XT_ZAIXIANZT.ZHIGONGGH;
            entity.IP = e_XT_ZAIXIANZT.IP;
            entity.MAC = e_XT_ZAIXIANZT.MAC;
            entity.KAISHISJ = now;
            entity.JIESHUSJ = now;
            entity.KESHIID = e_XT_ZAIXIANZT.KESHIID;
            entity.BINGQUID = e_XT_ZAIXIANZT.BINGQUID;
            entity.YINGYONGID = e_XT_ZAIXIANZT.YINGYONGID;
            entity.XITONGID = e_XT_ZAIXIANZT.XITONGID;
            entity.YILIAOZID = e_XT_ZAIXIANZT.YILIAOZID;
            entity.JUESEQX = e_XT_ZAIXIANZT.JUESEQX;
            entity.YUANQUID = e_XT_ZAIXIANZT.YUANQUID;
            return entity;
        }



    }
}
