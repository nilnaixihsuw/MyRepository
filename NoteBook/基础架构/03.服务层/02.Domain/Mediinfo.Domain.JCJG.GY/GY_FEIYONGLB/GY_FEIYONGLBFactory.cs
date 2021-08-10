using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_FEIYONGLBFactory
	{
        /*
		 
		public static GY_FEIYONGLB CreateIfNotExists(IGY_FEIYONGLBRepository irep, ServiceContext sContext, E_GY_FEIYONGLB dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_FEIYONGLB();
			}  
			return entity;
		}
		*/


        public static GY_FEIYONGLB Create(IGY_FEIYONGLBRepository irep, ServiceContext sContext, E_GY_FEIYONGLB dto)
        {
            GY_FEIYONGLB entity = dto.EToDB<E_GY_FEIYONGLB, GY_FEIYONGLB>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
