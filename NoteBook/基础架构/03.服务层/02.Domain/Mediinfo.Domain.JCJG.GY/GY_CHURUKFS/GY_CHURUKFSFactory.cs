using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_CHURUKFSFactory
	{
        /*
		 
		public static GY_CHURUKFS CreateIfNotExists(IGY_CHURUKFSRepository irep, ServiceContext sContext, E_GY_CHURUKFS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHURUKFS();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增出入库方式
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static GY_CHURUKFS Create(IGY_CHURUKFSRepository irep, ServiceContext sContext, E_GY_CHURUKFS dto)
        //{
        //    GY_CHURUKFS entity = dto.EToDB<E_GY_CHURUKFS, GY_CHURUKFS>();
        //    entity.Initialize(irep, sContext);
        //    if (string.IsNullOrEmpty(entity.FANGSHIID))
        //        entity.FANGSHIID = irep.GetOrder("GY_CHURUKFS", sContext.YUANQUID)[0].ToString();
        //    entity.XIUGAIREN = sContext.USERID;
        //    entity.XIUGAISJ = irep.GetSYSTime();
        //    irep.RegisterAdd(entity);
        //    return entity;
        //}



    }
}
