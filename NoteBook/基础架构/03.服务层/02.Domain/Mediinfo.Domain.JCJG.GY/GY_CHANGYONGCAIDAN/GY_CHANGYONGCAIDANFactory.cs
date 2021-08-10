using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_CHANGYONGCAIDANFactory
    {
        /*
		 
		public static GY_CHANGYONGCAIDAN CreateIfNotExists(IGY_CHANGYONGCAIDANRepository irep, ServiceContext sContext, E_GY_CHANGYONGCAIDAN dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHANGYONGCAIDAN();
			}  
			return entity;
		}
		*/

        public static GY_CHANGYONGCAIDAN Create(IGY_CHANGYONGCAIDANRepository irep, ServiceContext sContext, E_GY_CHANGYONGCAIDAN dto)
        {
            GY_CHANGYONGCAIDAN entity = dto.EToDB<E_GY_CHANGYONGCAIDAN, GY_CHANGYONGCAIDAN>();
            entity.Initialize(irep, sContext);

            entity.CHANGYONGCAIDANID = Int32.Parse(irep.GetOrder("GY_CHANGYONGCAIDAN")[0]);

            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
