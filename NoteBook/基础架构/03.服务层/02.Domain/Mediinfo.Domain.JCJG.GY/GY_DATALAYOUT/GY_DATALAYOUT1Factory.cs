using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DATALAYOUT1Factory
	{
        /*
		 
		public static GY_DATALAYOUT1 CreateIfNotExists(IGY_DATALAYOUT1Repository irep, ServiceContext sContext, E_GY_DATALAYOUT1 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DATALAYOUT1();
			}  
			return entity;
		}
		*/ 

        public static GY_DATALAYOUT1 Create(IGY_DATALAYOUT1Repository irep, ServiceContext sContext, E_GY_DATALAYOUT1 eDataLayout1)
        {
            GY_DATALAYOUT1 entity = new GY_DATALAYOUT1();

            entity = eDataLayout1.EToDB<E_GY_DATALAYOUT1, GY_DATALAYOUT1>();
            entity.Initialize(irep, sContext);
            entity.DATALAYOUTID = irep.GetOrder("GY_DATALAYOUT1")[0];
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();

            irep.RegisterAdd<GY_DATALAYOUT1>(entity); 
            return entity;
        } 
    }
}
