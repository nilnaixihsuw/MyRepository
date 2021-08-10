using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DANWEIFactory
	{
        /*
		 
		public static GY_DANWEI CreateIfNotExists(IGY_DANWEIRepository irep, ServiceContext sContext, E_GY_DANWEI dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DANWEI();
			}  
			return entity;
		}
		*/
        /// <summary>
        /// 新增公用单位信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_DANWEI Create(IGY_DANWEIRepository irep, ServiceContext sContext, E_GY_DANWEI dto)
        {
            GY_DANWEI entity = dto.EToDB<E_GY_DANWEI, GY_DANWEI>();
            entity.Initialize(irep, sContext);

            entity.DANWEIID = irep.GetOrder("GY_DANWEI", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
