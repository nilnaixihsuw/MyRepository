using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_GUANDAOFWDYFactory
	{
        /*
		 
		public static GY_GUANDAOFWDY CreateIfNotExists(IGY_GUANDAOFWDYRepository irep, ServiceContext sContext, E_GY_GUANDAOFWDY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_GUANDAOFWDY();
			}  
			return entity;
		}
		*/

        /*
		public static GY_GUANDAOFWDY Create(IGY_GUANDAOFWDYRepository irep,ServiceContext sContext,E_GY_GUANDAOFWDY dto )
		{
			GY_GUANDAOFWDY entity = new GY_GUANDAOFWDY();
			return entity;
		}
		 
		*/
        public static GY_GUANDAOFWDY Create(IGY_GUANDAOFWDYRepository irep, ServiceContext sContext, E_GY_GUANDAOFWDY_EX dto)
        {
            GY_GUANDAOFWDY entity = new GY_GUANDAOFWDY();
            entity = dto.EToDB<E_GY_GUANDAOFWDY_EX, GY_GUANDAOFWDY>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
