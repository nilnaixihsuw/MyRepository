using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZHANGHUJYMXFactory
	{
        /*
		 
		public static GY_ZHANGHUJYMX CreateIfNotExists(IGY_ZHANGHUJYMXRepository irep, ServiceContext sContext, E_GY_ZHANGHUJYMX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZHANGHUJYMX();
			}  
			return entity;
		}
		*/

        /*
		public static GY_ZHANGHUJYMX Create(IGY_ZHANGHUJYMXRepository irep,ServiceContext sContext,E_GY_ZHANGHUJYMX dto )
		{
			GY_ZHANGHUJYMX entity = new GY_ZHANGHUJYMX();
			return entity;
		}
		 
		*/
        /// <summary>
        /// 新增账户交易明细
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>

        public static GY_ZHANGHUJYMX Create(IGY_ZHANGHUJYMXRepository irep, ServiceContext sContext, E_GY_ZHANGHUJYMX dto)
        {
            GY_ZHANGHUJYMX entity = dto.EToDB<E_GY_ZHANGHUJYMX, GY_ZHANGHUJYMX>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.ZHANGHUJYMXID))
            {
                entity.ZHANGHUJYMXID = irep.GetOrder("GY_ZHANGHUJYMX", sContext.YUANQUID)[0].ToString();
            }
            entity.YINGYONGID = sContext.YINGYONGID;
            entity.YUANQUID = sContext.YUANQUID;
            entity.CAOZUOYUAN = sContext.USERID; 
            entity.ZUOFEIBZ = 0;
            entity.MAC = sContext.MAC;
            entity.COMPUTENAME = sContext.COMPUTERNAME; 
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
