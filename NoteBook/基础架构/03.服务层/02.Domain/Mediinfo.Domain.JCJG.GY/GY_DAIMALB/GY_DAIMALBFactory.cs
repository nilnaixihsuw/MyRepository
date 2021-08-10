using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DAIMALBFactory
	{
        /*
		 
		public static GY_DAIMALB CreateIfNotExists(IGY_DAIMALBRepository irep, ServiceContext sContext, E_GY_DAIMALB dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DAIMALB();
			}  
			return entity;
		}
		*/

        public static GY_DAIMALB Create(IGY_DAIMALBRepository irep, ServiceContext sContext, E_GY_DAIMALB EDaiMaLB)
        {

            var entity = EDaiMaLB.EToDB<E_GY_DAIMALB, GY_DAIMALB>();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
