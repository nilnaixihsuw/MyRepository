using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_FEIYONGKZFactory
	{
        /*
		 
		public static GY_FEIYONGKZ CreateIfNotExists(IGY_FEIYONGKZRepository irep, ServiceContext sContext, E_GY_FEIYONGKZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_FEIYONGKZ();
			}  
			return entity;
		}
		*/


        public static GY_FEIYONGKZ Create(IGY_FEIYONGKZRepository irep, ServiceContext sContext, E_GY_FEIYONGKZ dto)
        {
            GY_FEIYONGKZ entity = dto.EToDB<E_GY_FEIYONGKZ, GY_FEIYONGKZ>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
