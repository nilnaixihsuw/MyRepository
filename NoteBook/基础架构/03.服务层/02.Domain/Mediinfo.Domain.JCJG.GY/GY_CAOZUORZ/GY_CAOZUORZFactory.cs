using Mediinfo.Enterprise;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CAOZUORZFactory
	{
        /*
		 
		public static GY_CAOZUORZ CreateIfNotExists(IGY_CAOZUORZRepository irep, ServiceContext sContext, E_GY_CAOZUORZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CAOZUORZ();
			}  
			return entity;
		}
		*/
         

        /// <summary>
        /// 创建登陆日志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sConText"></param>
        public static GY_CAOZUORZ Create(IGY_CAOZUORZRepository irep, ServiceContext sContext)
        {
           
            var caoZuoRZEntity = new GY_CAOZUORZ();
            caoZuoRZEntity.RIZHIID = Convert.ToInt64(irep.GetOrder("GY_CAOZUORZ", null)[0]);
            caoZuoRZEntity.YINGYONGID = sContext.YINGYONGID;
            caoZuoRZEntity.GONGNENGID = "未知";
            caoZuoRZEntity.CAOZUOXX = "登录成功";
            caoZuoRZEntity.CAOZUORQ = irep.GetSYSTime();
            caoZuoRZEntity.YONGHUID = sContext.USERID;
            caoZuoRZEntity.YONGHUXM = sContext.USERNAME;
            caoZuoRZEntity.IP = sContext.IP;
            caoZuoRZEntity.JISUANJM = sContext.COMPUTERNAME;
            caoZuoRZEntity.WANGKADZ = sContext.MAC;
            caoZuoRZEntity.BANBENHAO = sContext.VERSION;
            caoZuoRZEntity.GONGZUOZID = sContext.GONGZUOZID;
            caoZuoRZEntity.RIZHILX = 1;
            caoZuoRZEntity.YICHANGTCBZ = 0;

            irep.RegisterAdd(caoZuoRZEntity);
            return caoZuoRZEntity;
        }


    }
}
