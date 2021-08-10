using Mediinfo.Enterprise;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YEWUDJFactory
	{
        /*
		 
		public static GY_YEWUDJ CreateIfNotExists(IGY_YEWUDJRepository irep, ServiceContext sContext, E_GY_YEWUDJ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YEWUDJ();
			}  
			return entity;
		}
		*/
         

        public static GY_YEWUDJ Create(IGY_YEWUDJRepository irep, ServiceContext sContext, string yeWuID, string yeWuLX)
        {  
             var entity  = new GY_YEWUDJ();
            entity.Initialize(irep, sContext);

            entity.WANGKADZ = sContext.MAC;
            entity.YEWUID = yeWuID;
            entity.YEWULX = yeWuLX;
            entity.YUANQUID = sContext.YUANQUID;
            entity.CAOZUORQ = irep.GetSYSTime();
            entity.CAOZUOYUAN = sContext.USERID;
            entity.JISUANJM = sContext.COMPUTERNAME;
            entity.YEWUDJID = Convert.ToInt64(irep.GetOrder("GY_YEWUDJ", sContext.YUANQUID, 1)[0]); 
            entity.KUOZHANXX = entity.GetKuoZhanXX();

            irep.RegisterAdd(entity); 
            return entity;
        }

    }
}
