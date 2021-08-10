using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JUESEYHFactory
	{
        /*
		 
		public static GY_JUESEYH CreateIfNotExists(IGY_JUESEYHRepository irep, ServiceContext sContext, E_GY_JUESEYH dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JUESEYH();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增角色用户信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_JUESEYH Create(IGY_JUESEYHRepository irep, ServiceContext sContext, E_GY_JUESEYH dto)
        {
            GY_JUESEYH entity = dto.EToDB<E_GY_JUESEYH, GY_JUESEYH>();
            entity.Initialize(irep, sContext);

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
