using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANCHAXMZXKSFactory
	{
        /*
		 
		public static GY_JIANCHAXMZXKS CreateIfNotExists(IGY_JIANCHAXMZXKSRepository irep, ServiceContext sContext, E_GY_JIANCHAXMZXKS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANCHAXMZXKS();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIANCHAXMZXKS Create(IGY_JIANCHAXMZXKSRepository irep,ServiceContext sContext,E_GY_JIANCHAXMZXKS dto )
		{
			GY_JIANCHAXMZXKS entity = new GY_JIANCHAXMZXKS();
			return entity;
		}
		 
		*/
        public static GY_JIANCHAXMZXKS Create(IGY_JIANCHAXMZXKSRepository irep, ServiceContext sContext, E_GY_JIANCHAXMZXKS dto)
        {
            GY_JIANCHAXMZXKS entity = dto.EToDB<E_GY_JIANCHAXMZXKS, GY_JIANCHAXMZXKS>();
            entity.Initialize(irep, sContext);
           
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
