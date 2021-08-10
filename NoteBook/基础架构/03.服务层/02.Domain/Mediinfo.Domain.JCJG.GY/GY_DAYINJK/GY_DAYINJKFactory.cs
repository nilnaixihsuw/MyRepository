using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DAYINJKFactory
	{
        /*
		 
		public static GY_DAYINJK CreateIfNotExists(IGY_DAYINJKRepository irep, ServiceContext sContext, E_GY_DAYINJK dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DAYINJK();
			}  
			return entity;
		}
		*/

        /*
		public static GY_DAYINJK Create(IGY_DAYINJKRepository irep,ServiceContext sContext,E_GY_DAYINJK dto )
		{
			GY_DAYINJK entity = new GY_DAYINJK();
			return entity;
		}
		 
		*/


        public static GY_DAYINJK Create(IGY_DAYINJKRepository irep, ServiceContext sContext, E_GY_DAYINJK dto)
        {
            GY_DAYINJK entity = dto.EToDB<E_GY_DAYINJK, GY_DAYINJK>();

            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JILUID))
            {
                entity.JILUID = irep.GetOrder("GY_DAYINJK", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
