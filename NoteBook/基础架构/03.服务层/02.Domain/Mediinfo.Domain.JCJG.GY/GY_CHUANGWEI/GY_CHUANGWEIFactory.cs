using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CHUANGWEIFactory
	{
        /*
		 
		public static GY_CHUANGWEI CreateIfNotExists(IGY_CHUANGWEIRepository irep, ServiceContext sContext, E_GY_CHUANGWEI dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHUANGWEI();
			}  
			return entity;
		}
		*/

        /*
		public static GY_CHUANGWEI Create(IGY_CHUANGWEIRepository irep,ServiceContext sContext,E_GY_CHUANGWEI dto )
		{
			GY_CHUANGWEI entity = new GY_CHUANGWEI();
			return entity;
		}
		 
		*/

        public static GY_CHUANGWEI Create(IGY_CHUANGWEIRepository irep, ServiceContext sContext, E_GY_CHUANGWEI dto)
        {
            GY_CHUANGWEI entity = dto.EToDB<E_GY_CHUANGWEI, GY_CHUANGWEI>();
            entity.Initialize(irep, sContext);

            //entity.CHUANGWEIID = irep.GetOrder("GY_CHUANGWEI", sContext.YUANQUID)[0].ToString();
            //jieSuan2Entity.JIESUANMXID = irep.GetOrder("ZY_JIESUAN2", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
