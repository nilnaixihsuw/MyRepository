using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANCHAXMSFFactory
	{
        /*
		 
		public static GY_JIANCHAXMSF CreateIfNotExists(IGY_JIANCHAXMSFRepository irep, ServiceContext sContext, E_GY_JIANCHAXMSF dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANCHAXMSF();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIANCHAXMSF Create(IGY_JIANCHAXMSFRepository irep,ServiceContext sContext,E_GY_JIANCHAXMSF dto )
		{
			GY_JIANCHAXMSF entity = new GY_JIANCHAXMSF();
			return entity;
		}
		 
		*/
        public static GY_JIANCHAXMSF Create(IGY_JIANCHAXMSFRepository irep, ServiceContext sContext, E_GY_JIANCHAXMSF dto)
        {
            GY_JIANCHAXMSF entity = dto.EToDB<E_GY_JIANCHAXMSF, GY_JIANCHAXMSF>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIANCHAXMSFID))
            {
                entity.JIANCHAXMSFID = irep.GetOrder("GY_JIANCHAXMSF", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
