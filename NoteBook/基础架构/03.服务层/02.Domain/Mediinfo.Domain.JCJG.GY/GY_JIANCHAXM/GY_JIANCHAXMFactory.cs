using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANCHAXMFactory
	{
        /*
		 
		public static GY_JIANCHAXM CreateIfNotExists(IGY_JIANCHAXMRepository irep, ServiceContext sContext, E_GY_JIANCHAXM dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANCHAXM();
			}  
			return entity;
		}
		*/

        
		public static GY_JIANCHAXM Create(IGY_JIANCHAXMRepository irep,ServiceContext sContext,E_GY_JIANCHAXM dto )
		{
			GY_JIANCHAXM entity = dto.EToDB<E_GY_JIANCHAXM, GY_JIANCHAXM>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIANCHAXMID))
            {
                entity.JIANCHAXMID = irep.GetOrder("GY_JIANCHAXM", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
			return entity;
		}
        
    }
}
