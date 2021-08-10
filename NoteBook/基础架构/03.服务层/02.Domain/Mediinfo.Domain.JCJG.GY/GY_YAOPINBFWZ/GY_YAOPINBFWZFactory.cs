using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINBFWZFactory
	{
        /*
		 
		public static GY_YAOPINBFWZ CreateIfNotExists(IGY_YAOPINBFWZRepository irep, ServiceContext sContext, E_GY_YAOPINBFWZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINBFWZ();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 药品摆放位置
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static GY_YAOPINBFWZ Create(IGY_YAOPINBFWZRepository irep, ServiceContext sContext, E_GY_YAOPINBFWZ dto)
        //{
        //    GY_YAOPINBFWZ entity = dto.EToDB<E_GY_YAOPINBFWZ, GY_YAOPINBFWZ>();
        //    entity.Initialize(irep, sContext);
        //    entity.WEIZHIID = irep.GetOrder("GY_YAOPINBFWZ", sContext.YUANQUID)[0].ToString();
        //    entity.XIUGAIREN = sContext.USERID;
        //    entity.XIUGAISJ = irep.GetSYSTime();

        //    irep.RegisterAdd(entity);
        //    return entity;
        //}



    }
}
