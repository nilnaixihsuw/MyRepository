using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JUESEFactory
	{
        /*
		 
		public static GY_JUESE CreateIfNotExists(IGY_JUESERepository irep, ServiceContext sContext, E_GY_JUESE dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JUESE();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_JUESE Create(IGY_JUESERepository irep, ServiceContext sContext, E_GY_JUESE dto)
        {
            GY_JUESE entity = dto.EToDB<E_GY_JUESE, GY_JUESE>();
            entity.Initialize(irep, sContext);

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
