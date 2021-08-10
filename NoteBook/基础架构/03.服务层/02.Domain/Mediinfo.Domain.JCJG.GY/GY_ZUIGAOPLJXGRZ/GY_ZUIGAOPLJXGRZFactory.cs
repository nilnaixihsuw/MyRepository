using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_ZUIGAOPLJXGRZFactory
	{
        /*
		 
		public static GY_ZUIGAOPLJXGRZ CreateIfNotExists(IGY_ZUIGAOPLJXGRZRepository irep, ServiceContext sContext, E_GY_ZUIGAOPLJXGRZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZUIGAOPLJXGRZ();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_最高批零价修改日志
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_ZUIGAOPLJXGRZ Create(IGY_ZUIGAOPLJXGRZRepository irep, ServiceContext sContext, E_GY_ZUIGAOPLJXGRZ dto)
        {           
            GY_ZUIGAOPLJXGRZ entity = dto.EToDB<E_GY_ZUIGAOPLJXGRZ, GY_ZUIGAOPLJXGRZ>();
            entity.Initialize(irep, sContext);
            
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
