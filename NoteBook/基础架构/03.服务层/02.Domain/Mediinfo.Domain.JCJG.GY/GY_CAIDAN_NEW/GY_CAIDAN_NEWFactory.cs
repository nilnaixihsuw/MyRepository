using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CAIDAN_NEWFactory
	{
        /*
		 
		public static GY_CAIDAN_NEW CreateIfNotExists(IGY_CAIDAN_NEWRepository irep, ServiceContext sContext, E_GY_CAIDAN_NEW dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CAIDAN_NEW();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增应用菜单
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_CAIDAN_NEW Create(IGY_CAIDAN_NEWRepository irep, ServiceContext sContext, E_GY_CAIDAN_NEW dto)
        {
            

            GY_CAIDAN_NEW entity = dto.EToDB<E_GY_CAIDAN_NEW, GY_CAIDAN_NEW>();
            entity.Initialize(irep, sContext);
           
            entity.CAIDANROWID = irep.GetOrder("GY_CAIDAN_NEW", sContext.YUANQUID)[0].ToString();
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
