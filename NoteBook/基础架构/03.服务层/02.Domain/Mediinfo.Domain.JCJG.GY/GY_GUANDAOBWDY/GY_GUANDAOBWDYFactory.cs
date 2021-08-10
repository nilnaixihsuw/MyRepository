using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_GUANDAOBWDYFactory
	{
        /*
		 
		public static GY_GUANDAOBWDY CreateIfNotExists(IGY_GUANDAOBWDYRepository irep, ServiceContext sContext, E_GY_GUANDAOBWDY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_GUANDAOBWDY();
			}  
			return entity;
		}
		*/

        /*
		public static GY_GUANDAOBWDY Create(IGY_GUANDAOBWDYRepository irep,ServiceContext sContext,E_GY_GUANDAOBWDY dto )
		{
			GY_GUANDAOBWDY entity = new GY_GUANDAOBWDY();
			return entity;
		}
		 
		*/
        public static GY_GUANDAOBWDY Create(IGY_GUANDAOBWDYRepository irep, ServiceContext sContext, E_GY_GUANDAOBWDY_EX dto)
        {
            GY_GUANDAOBWDY entity = new GY_GUANDAOBWDY();
            entity = dto.EToDB<E_GY_GUANDAOBWDY_EX, GY_GUANDAOBWDY>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
