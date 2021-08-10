using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANCHAXMDYSMFactory
	{
        /*
		 
		public static GY_JIANCHAXMDYSM CreateIfNotExists(IGY_JIANCHAXMDYSMRepository irep, ServiceContext sContext, E_GY_JIANCHAXMDYSM dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANCHAXMDYSM();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIANCHAXMDYSM Create(IGY_JIANCHAXMDYSMRepository irep,ServiceContext sContext,E_GY_JIANCHAXMDYSM dto )
		{
			GY_JIANCHAXMDYSM entity = new GY_JIANCHAXMDYSM();
			return entity;
		}
		 
		*/
        public static GY_JIANCHAXMDYSM Create(IGY_JIANCHAXMDYSMRepository irep, ServiceContext sContext, E_GY_JIANCHAXMDYSM dto)
        {
            GY_JIANCHAXMDYSM entity = dto.EToDB<E_GY_JIANCHAXMDYSM, GY_JIANCHAXMDYSM>();
            entity.Initialize(irep, sContext);
           
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
