using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_QUANXIAN2_NEWFactory
	{
        public static GY_QUANXIAN2_NEW CreateIfNotExists(IGY_QUANXIAN2_NEWRepository irep, ServiceContext sContext, E_GY_QUANXIAN2_NEW dto)
        {
            var entity = irep.GetByKey(dto.QUANXIANID);
            if (entity == null)
            {
                entity = dto.EToDB<E_GY_QUANXIAN2_NEW, GY_QUANXIAN2_NEW>();
               
                entity.Initialize(irep, sContext);
                irep.RegisterAdd(entity);
            }
          
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
          
            return entity;
        }

        public static GY_QUANXIAN2_NEW Create(IGY_QUANXIAN2_NEWRepository irep, ServiceContext sContext, E_GY_QUANXIAN2_NEW dto)
        {
            GY_QUANXIAN2_NEW entity = dto.EToDB<E_GY_QUANXIAN2_NEW, GY_QUANXIAN2_NEW>();
            entity.Initialize(irep, sContext);
            //entity.QUANXIANID = irep.GetOrder("GY_QUANXIAN")[0];
            entity.XITONGSJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();

            irep.RegisterAdd(entity);
            return entity;
        }


        public static GY_QUANXIAN2_NEW Create(IGY_QUANXIAN2_NEWRepository irep, ServiceContext sContext, E_GY_ERJIYHQX dto)
        {


            GY_QUANXIAN2_NEW entity = dto.EToDB<E_GY_ERJIYHQX, GY_QUANXIAN2_NEW>();
            entity.Initialize(irep, sContext);
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
