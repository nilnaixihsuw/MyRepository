using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_BILIYHFactory
	{
        /*
		 
		public static GY_BILIYH CreateIfNotExists(IGY_BILIYHRepository irep, ServiceContext sContext, E_GY_BILIYH dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BILIYH();
			}  
			return entity;
		}
		*/


        public static GY_BILIYH Create(IGY_BILIYHRepository irep, ServiceContext sContext, E_GY_BILIYH dto)
        {
            GY_BILIYH entity = dto.EToDB<E_GY_BILIYH, GY_BILIYH>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
