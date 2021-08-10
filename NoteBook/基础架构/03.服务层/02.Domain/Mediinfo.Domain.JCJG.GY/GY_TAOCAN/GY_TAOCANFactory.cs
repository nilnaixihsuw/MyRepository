using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_TAOCANFactory
	{
        /*
		 
		public static GY_TAOCAN CreateIfNotExists(IGY_TAOCANRepository irep, ServiceContext sContext, E_GY_TAOCAN dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_TAOCAN();
			}  
			return entity;
		}
		*/

        /*
		public static GY_TAOCAN Create(IGY_TAOCANRepository irep,ServiceContext sContext,E_GY_TAOCAN dto )
		{
			GY_TAOCAN entity = new GY_TAOCAN();
			return entity;
		}
		 
		*/

        public static GY_TAOCAN Create(IGY_TAOCANRepository irep, ServiceContext sContext, E_GY_TAOCAN dto)
        {
            GY_TAOCAN entity = dto.EToDB<E_GY_TAOCAN, GY_TAOCAN>();
            entity.Initialize(irep, sContext);

            entity.TAOCANID = irep.GetOrder("GY_TAOCAN", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
