using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZIFUBLFactory
	{
        /*
		 
		public static GY_ZIFUBL CreateIfNotExists(IGY_ZIFUBLRepository irep, ServiceContext sContext, E_GY_ZIFUBL dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZIFUBL();
			}  
			return entity;
		}
		*/


        public static GY_ZIFUBL Create(IGY_ZIFUBLRepository irep, ServiceContext sContext, E_GY_ZIFUBL dto)
        {
            GY_ZIFUBL entity = dto.EToDB<E_GY_ZIFUBL, GY_ZIFUBL>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
