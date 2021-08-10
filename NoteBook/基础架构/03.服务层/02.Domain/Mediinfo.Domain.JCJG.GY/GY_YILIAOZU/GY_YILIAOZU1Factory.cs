using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YILIAOZU1Factory
	{
        /*
		 
		public static GY_YILIAOZU1 CreateIfNotExists(IGY_YILIAOZU1Repository irep, ServiceContext sContext, E_GY_YILIAOZU1 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YILIAOZU1();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_医疗组1
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_YILIAOZU1 Create(IGY_YILIAOZU1Repository irep, ServiceContext sContext, E_GY_YILIAOZU1 dto)
        {

            GY_YILIAOZU1 entity = dto.EToDB<E_GY_YILIAOZU1, GY_YILIAOZU1>();
            entity.Initialize(irep, sContext);

           
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
