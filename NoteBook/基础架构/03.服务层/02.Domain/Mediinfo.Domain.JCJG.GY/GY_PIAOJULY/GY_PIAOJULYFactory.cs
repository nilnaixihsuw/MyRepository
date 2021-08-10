using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_PIAOJULYFactory
	{
        /*
		 
		public static GY_PIAOJULY CreateIfNotExists(IGY_PIAOJULYRepository irep, ServiceContext sContext, E_GY_PIAOJULY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_PIAOJULY();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增票据已领用信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_PIAOJULY Create(IGY_PIAOJULYRepository irep, ServiceContext sContext, E_GY_PIAOJULY dto)
        {
            var entity = dto.EToDB<E_GY_PIAOJULY, GY_PIAOJULY>();
            entity.Initialize(irep, sContext);

            entity.PIAOJULYID = irep.GetOrder("GY_PIAOJULYID", sContext.YUANQUID)[0];

            entity.PIAOJUID = "0";
            //entity.TINGYONGRQ = irep.GetSYSTime();

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
