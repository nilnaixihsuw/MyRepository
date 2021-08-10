using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YONGHUQX2Factory
	{
        public static GY_YONGHUQX2 CreateIfNotExists(IGY_YONGHUQX2Repository irep, ServiceContext sContext, E_GY_YONGHUQX2 dto)
        {
            var entity = irep.GetByKey(dto.QUANXIANID,dto.YONGHUID);
            if (entity == null)
            {
                entity = dto.EToDB<E_GY_YONGHUQX2, GY_YONGHUQX2>();

                entity.Initialize(irep, sContext);
                entity.XIUGAIREN = sContext.USERID;
                entity.XIUGAISJ = irep.GetSYSTime();
                irep.RegisterAdd(entity);
            }

           
            return entity;
        }

       

        public static GY_YONGHUQX2 Create(IGY_YONGHUQX2Repository irep, ServiceContext sContext, E_GY_ZHIGONGYHQX dto)
        {


            GY_YONGHUQX2 entity = dto.EToDB<E_GY_ZHIGONGYHQX, GY_YONGHUQX2>();
            entity.Initialize(irep, sContext);
            entity.YONGHUID = dto.ZHIGONGID;
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }


    }
}
