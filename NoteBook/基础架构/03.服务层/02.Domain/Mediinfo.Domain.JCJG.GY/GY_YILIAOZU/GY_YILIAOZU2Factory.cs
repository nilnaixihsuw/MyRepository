using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YILIAOZU2Factory
	{
        /*
		 
		public static GY_YILIAOZU2 CreateIfNotExists(IGY_YILIAOZU2Repository irep, ServiceContext sContext, E_GY_YILIAOZU2 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YILIAOZU2();
			}  
			return entity;
		}
		*/


        public static GY_YILIAOZU2 Create(IGY_YILIAOZU2Repository irep, ServiceContext sContext, E_GY_YILIAOZU2 dto)
        {
            GY_YILIAOZU2 entity = dto.EToDB<E_GY_YILIAOZU2, GY_YILIAOZU2>();
            entity.Initialize(irep, sContext);


            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
