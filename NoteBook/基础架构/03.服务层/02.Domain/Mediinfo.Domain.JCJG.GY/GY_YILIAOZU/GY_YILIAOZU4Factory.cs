using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YILIAOZU4Factory
	{
        /*
		 
		public static GY_YILIAOZU4 CreateIfNotExists(IGY_YILIAOZU4Repository irep, ServiceContext sContext, E_GY_YILIAOZU4 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YILIAOZU4();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_医疗组4
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_YILIAOZU4 Create(IGY_YILIAOZU4Repository irep, ServiceContext sContext, E_GY_YILIAOZU4 dto)
        {
            GY_YILIAOZU4 entity = dto.EToDB<E_GY_YILIAOZU4, GY_YILIAOZU4>();
            entity.Initialize(irep, sContext);


            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
