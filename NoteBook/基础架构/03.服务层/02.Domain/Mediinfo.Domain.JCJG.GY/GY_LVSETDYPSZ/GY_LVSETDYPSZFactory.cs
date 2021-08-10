using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_LVSETDYPSZFactory
	{
        /*
		 
		public static GY_LVSETDYPSZ CreateIfNotExists(IGY_LVSETDYPSZRepository irep, ServiceContext sContext, E_GY_LVSETDYPSZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_LVSETDYPSZ();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 更新 公用_绿色通道药品设置
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_LVSETDYPSZ Create(IGY_LVSETDYPSZRepository irep, ServiceContext sContext, E_GY_LVSETDYPSZ dto)
        {        
            GY_LVSETDYPSZ entity = dto.EToDB<E_GY_LVSETDYPSZ, GY_LVSETDYPSZ>();
            entity.Initialize(irep, sContext);

            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
