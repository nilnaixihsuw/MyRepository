using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DAIMAFactory
	{
        /*
		 
		public static GY_DAIMA CreateIfNotExists(IGY_DAIMARepository irep, ServiceContext sContext, E_GY_DAIMA dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DAIMA();
			}  
			return entity;
		}
		*/

        public static GY_DAIMA Create(IGY_DAIMARepository irep, ServiceContext sContext, E_GY_DAIMA EDaiMa)
        { 
            var entity = EDaiMa.EToDB<E_GY_DAIMA, GY_DAIMA>();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            irep.RegisterAdd(entity);
            return entity;
        } 
    }
}
