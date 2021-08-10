using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_BAOBIAOQXFactory
    {
        /*
		 
		public static GY_BAOBIAOQX CreateIfNotExists(IGY_BAOBIAOQXRepository irep, ServiceContext sContext, E_GY_BAOBIAOQX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BAOBIAOQX();
			}  
			return entity;
		}
		*/


        public static GY_BAOBIAOQX Create(IGY_BAOBIAOQXRepository irep, ServiceContext sContext, E_GY_BAOBIAOQX dto)
        {
            GY_BAOBIAOQX entity = dto.EToDB<E_GY_BAOBIAOQX, GY_BAOBIAOQX>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
