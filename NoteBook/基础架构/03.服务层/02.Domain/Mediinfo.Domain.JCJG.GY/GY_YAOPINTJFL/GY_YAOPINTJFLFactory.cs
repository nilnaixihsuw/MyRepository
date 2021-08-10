using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINTJFLFactory
	{
        /*
		 
		public static GY_YAOPINTJFL CreateIfNotExists(IGY_YAOPINTJFLRepository irep, ServiceContext sContext, E_GY_YAOPINTJFL dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINTJFL();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_药品统计分类
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static GY_YAOPINTJFL Create(IGY_YAOPINTJFLRepository irep, ServiceContext sContext, E_GY_YAOPINTJFL dto)
        //{
        //    GY_YAOPINTJFL entity = dto.EToDB<E_GY_YAOPINTJFL, GY_YAOPINTJFL>();
        //    entity.Initialize(irep,sContext);
        //    entity.TONGJIFLID = irep.GetOrder("GY_TONGJIFL", sContext.YUANQUID)[0].ToString();
        //    entity.XIUGAIREN = sContext.USERID;
        //    entity.XIUGAISJ = irep.GetSYSTime();

        //    irep.RegisterAdd(entity);
        //    return entity;
        //}        
    }
}
