using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINCLKZFactory
	{
        /*
		 
		public static GY_YAOPINCLKZ CreateIfNotExists(IGY_YAOPINCLKZRepository irep, ServiceContext sContext, E_GY_YAOPINCLKZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINCLKZ();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_药品存量控制
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static GY_YAOPINCLKZ Create(IGY_YAOPINCLKZRepository irep, ServiceContext sContext, E_GY_YAOPINCLKZ dto)
        //{
        //    GY_YAOPINCLKZ entity = new GY_YAOPINCLKZ();
        //    entity.Initialize(irep, sContext);
        //    entity = dto.EToDB<E_GY_YAOPINCLKZ, GY_YAOPINCLKZ>();

        //    entity.XIUGAIREN = sContext.USERID;
        //    entity.XIUGAISJ = irep.GetSYSTime();

        //    irep.RegisterAdd(entity);
        //    return entity;
        //}



    }
}
