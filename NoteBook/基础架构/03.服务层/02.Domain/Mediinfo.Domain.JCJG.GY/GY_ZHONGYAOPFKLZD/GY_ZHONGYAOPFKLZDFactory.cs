using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZHONGYAOPFKLZDFactory
	{
        /*
		 
		public static GY_ZHONGYAOPFKLZD CreateIfNotExists(IGY_ZHONGYAOPFKLZDRepository irep, ServiceContext sContext, E_GY_ZHONGYAOPFKLZD dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZHONGYAOPFKLZD();
			}  
			return entity;
		}
		*/

        /*
		public static GY_ZHONGYAOPFKLZD Create(IGY_ZHONGYAOPFKLZDRepository irep,ServiceContext sContext,E_GY_ZHONGYAOPFKLZD dto )
		{
			GY_ZHONGYAOPFKLZD entity = new GY_ZHONGYAOPFKLZD();
			return entity;
		}
		 
		*/
        public static GY_ZHONGYAOPFKLZD Create(IGY_ZHONGYAOPFKLZDRepository irep, ServiceContext sContext, E_GY_ZHONGYAOPFKLZD dto)
        {
            GY_ZHONGYAOPFKLZD entity = dto.EToDB<E_GY_ZHONGYAOPFKLZD, GY_ZHONGYAOPFKLZD>();
            entity.Initialize(irep, sContext);
            entity.ZHONGYAOPFKLID = irep.GetOrder("GY_ZHONGYAOPFKLZD", sContext.YUANQUID)[0].ToString();

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
