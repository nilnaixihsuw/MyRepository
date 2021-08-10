using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINCDFactory
	{
        /*
		 
		public static GY_YAOPINCD CreateIfNotExists(IGY_YAOPINCDRepository irep, ServiceContext sContext, E_GY_YAOPINCD dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINCD();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_药品产地
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_YAOPINCD Create(IGY_YAOPINCDRepository irep, ServiceContext sContext, E_GY_YAOPINCD dto)
        {            
            GY_YAOPINCD entity = dto.EToDB<E_GY_YAOPINCD, GY_YAOPINCD>();
            entity.Initialize(irep, sContext);

            entity.CHANDIID = irep.GetOrder("GY_YAOPINCD", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
