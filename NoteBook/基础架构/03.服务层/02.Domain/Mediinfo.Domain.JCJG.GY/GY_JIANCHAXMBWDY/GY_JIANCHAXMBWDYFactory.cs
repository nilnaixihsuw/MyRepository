using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANCHAXMBWDYFactory
	{
        /*
		 
		public static GY_JIANCHAXMBWDY CreateIfNotExists(IGY_JIANCHAXMBWDYRepository irep, ServiceContext sContext, E_GY_JIANCHAXMBWDY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANCHAXMBWDY();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIANCHAXMBWDY Create(IGY_JIANCHAXMBWDYRepository irep,ServiceContext sContext,E_GY_JIANCHAXMBWDY dto )
		{
			GY_JIANCHAXMBWDY entity = new GY_JIANCHAXMBWDY();
			return entity;
		}
		 
		*/
        public static GY_JIANCHAXMBWDY Create(IGY_JIANCHAXMBWDYRepository irep, ServiceContext sContext, E_GY_JIANCHAXMBWDY dto)
        {
            GY_JIANCHAXMBWDY entity = dto.EToDB<E_GY_JIANCHAXMBWDY, GY_JIANCHAXMBWDY>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIANCHAXMBWDYID))
            {
                entity.JIANCHAXMBWDYID = irep.GetOrder("GY_JIANCHAXMBWDY", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
