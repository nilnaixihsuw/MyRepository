using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_FANGJIANFactory
	{
        /*
		 
		public static GY_FANGJIAN CreateIfNotExists(IGY_FANGJIANRepository irep, ServiceContext sContext, E_GY_FANGJIAN dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_FANGJIAN();
			}  
			return entity;
		}
		*/

        /*
		public static GY_FANGJIAN Create(IGY_FANGJIANRepository irep,ServiceContext sContext,E_GY_FANGJIAN dto )
		{
			GY_FANGJIAN entity = new GY_FANGJIAN();
			return entity;
		}
		 
		*/

        public static GY_FANGJIAN Create(IGY_FANGJIANRepository irep, ServiceContext sContext, E_GY_FANGJIAN dto)
        {
            GY_FANGJIAN entity = dto.EToDB<E_GY_FANGJIAN, GY_FANGJIAN>();
            entity.Initialize(irep, sContext);

            entity.FANGJIANID = irep.GetOrder("GY_FANGJIAN", sContext.YUANQUID)[0].ToString();
            //jieSuan2Entity.JIESUANMXID = irep.GetOrder("ZY_JIESUAN2", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
