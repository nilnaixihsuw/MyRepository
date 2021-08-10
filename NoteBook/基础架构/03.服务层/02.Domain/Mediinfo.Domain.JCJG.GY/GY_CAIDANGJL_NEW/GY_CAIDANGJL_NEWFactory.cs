using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CAIDANGJL_NEWFactory
	{
        /*
		 
		public static GY_CAIDANGJL_NEW CreateIfNotExists(IGY_CAIDANGJL_NEWRepository irep, ServiceContext sContext, E_GY_CAIDANGJL_NEW dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CAIDANGJL_NEW();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增应用菜单工具栏信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_CAIDANGJL_NEW Create(IGY_CAIDANGJL_NEWRepository irep, ServiceContext sContext, E_GY_CAIDANGJL_NEW dto)
        {            
            GY_CAIDANGJL_NEW entity = dto.EToDB<E_GY_CAIDANGJL_NEW, GY_CAIDANGJL_NEW>();
            entity.Initialize(irep, sContext);            

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
