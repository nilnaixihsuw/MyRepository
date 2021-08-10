using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CHUANGKOUZY_NEWFactory
	{


        public static GY_CHUANGKOUZY_NEW CreateIfNotExists(IGY_CHUANGKOUZY_NEWRepository irep, ServiceContext sContext, E_GY_CHUANGKOUZY_NEW dto)
        {
            var entity = irep.GetByKey(dto.CHUANGKOUZYID);
            if (entity == null)
            {
                entity = new GY_CHUANGKOUZY_NEW();
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }
		
		public static GY_CHUANGKOUZY_NEW Create(IGY_CHUANGKOUZY_NEWRepository irep,ServiceContext sContext,E_GY_CHUANGKOUZY_NEW dto )
		{
			GY_CHUANGKOUZY_NEW entity = dto.EToDB<E_GY_CHUANGKOUZY_NEW, GY_CHUANGKOUZY_NEW>();
            entity.Initialize(irep, sContext);
            entity.CHUANGKOUZYID = irep.GetOrder("GY_CHUANGKOUZY_NEW")[0];
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
		}

    }
}
