namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOFANGKFSJFactory
	{
        /*
		 
		public static GY_YAOFANGKFSJ CreateIfNotExists(IGY_YAOFANGKFSJRepository irep, ServiceContext sContext, E_GY_YAOFANGKFSJ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOFANGKFSJ();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增药房开发时间
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static GY_YAOFANGKFSJ Create(IGY_YAOFANGKFSJRepository irep, ServiceContext sContext, E_GY_YAOFANGKFSJ dto)
        //{
        //    GY_YAOFANGKFSJ entity = dto.EToDB<E_GY_YAOFANGKFSJ, GY_YAOFANGKFSJ>();
        //    entity.Initialize(irep, sContext);

        //    entity.XIUGAIREN = sContext.USERID;
        //    entity.XIUGAISJ = irep.GetSYSTime();

        //    irep.RegisterAdd(entity);
        //    return entity;
        //}



    }
}
