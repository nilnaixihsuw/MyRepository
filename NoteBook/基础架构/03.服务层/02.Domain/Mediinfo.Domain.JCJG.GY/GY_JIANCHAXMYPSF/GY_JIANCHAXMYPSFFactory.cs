using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANCHAXMYPSFFactory
	{
        /*
		 
		public static GY_JIANCHAXMYPSF CreateIfNotExists(IGY_JIANCHAXMYPSFRepository irep, ServiceContext sContext, E_GY_JIANCHAXMYPSF dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANCHAXMYPSF();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIANCHAXMYPSF Create(IGY_JIANCHAXMYPSFRepository irep,ServiceContext sContext,E_GY_JIANCHAXMYPSF dto )
		{
			GY_JIANCHAXMYPSF entity = new GY_JIANCHAXMYPSF();
			return entity;
		}
		 
		*/
        public static GY_JIANCHAXMYPSF Create(IGY_JIANCHAXMYPSFRepository irep, ServiceContext sContext, E_GY_JIANCHAXMYPSF dto)
        {
            GY_JIANCHAXMYPSF entity = dto.EToDB<E_GY_JIANCHAXMYPSF, GY_JIANCHAXMYPSF>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIANCHAXMYPSFID))
            {
                entity.JIANCHAXMYPSFID = irep.GetOrder("GY_JIANCHAXMYPSF", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
