using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINZKZYSZFactory
	{
        /*
		 
		public static GY_YAOPINZKZYSZ CreateIfNotExists(IGY_YAOPINZKZYSZRepository irep, ServiceContext sContext, E_GY_YAOPINZKZYSZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINZKZYSZ();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_药品专科专用设置
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static GY_YAOPINZKZYSZ Create(IGY_YAOPINZKZYSZRepository irep, ServiceContext sContext, E_GY_YAOPINZKZYSZ dto)
        //{
        //    GY_YAOPINZKZYSZ entity = dto.EToDB<E_GY_YAOPINZKZYSZ, GY_YAOPINZKZYSZ>();
        //    entity.Initialize(irep, sContext);
        //    entity.YAOPINZKZYID = irep.GetOrder("GY_YAOPINZKZYSZ", sContext.YUANQUID)[0].ToString();
        //    entity.XIUGAIREN = sContext.USERID;
        //    entity.XIUGAISJ = irep.GetSYSTime();

        //    irep.RegisterAdd(entity);
        //    return entity;
        //}



    }
}
