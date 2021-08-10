using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_TAOCANMXFactory
	{
        /*
		 
		public static GY_TAOCANMX CreateIfNotExists(IGY_TAOCANMXRepository irep, ServiceContext sContext, E_GY_TAOCANMX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_TAOCANMX();
			}  
			return entity;
		}
		*/

        /*
		public static GY_TAOCANMX Create(IGY_TAOCANMXRepository irep,ServiceContext sContext,E_GY_TAOCANMX dto )
		{
			GY_TAOCANMX entity = new GY_TAOCANMX();
			return entity;
		}
		 
		*/


        public static GY_TAOCANMX Create(IGY_TAOCANMXRepository irep, ServiceContext sContext, E_GY_TAOCANMX dto)
        {
            GY_TAOCANMX entity = dto.EToDB<E_GY_TAOCANMX, GY_TAOCANMX>();
            entity.Initialize(irep, sContext);

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
