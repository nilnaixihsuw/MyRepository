using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_FEIYONGXZFactory
	{
        /*
		 
		public static GY_FEIYONGXZ CreateIfNotExists(IGY_FEIYONGXZRepository irep, ServiceContext sContext, E_GY_FEIYONGXZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_FEIYONGXZ();
			}  
			return entity;
		}
		*/


        public static GY_FEIYONGXZ Create(IGY_FEIYONGXZRepository irep, ServiceContext sContext, E_GY_FEIYONGXZ dto)
        {
            GY_FEIYONGXZ entity = dto.EToDB<E_GY_FEIYONGXZ, GY_FEIYONGXZ>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
