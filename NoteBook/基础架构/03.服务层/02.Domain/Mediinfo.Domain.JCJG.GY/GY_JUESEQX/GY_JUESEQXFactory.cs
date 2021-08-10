using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JUESEQXFactory
	{
        public static GY_JUESEQX CreateIfNotExists(IGY_JUESEQXRepository irep, ServiceContext sContext, E_GY_JUESEQX dto)
        {
            var entity = irep.GetByKey(dto.JUESEID,dto.QUANXIANID);
            if (entity == null)
            {
                entity = dto.EToDB<E_GY_JUESEQX, GY_JUESEQX>();
                entity.Initialize(irep, sContext);
                irep.RegisterAdd(entity);
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
           
            return entity;
        }

        /// <summary>
        /// 新增角色权限信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_JUESEQX Create(IGY_JUESEQXRepository irep, ServiceContext sContext, E_GY_JUESEQX dto)
        {

            GY_JUESEQX entity = dto.EToDB<E_GY_JUESEQX, GY_JUESEQX>();
            entity.Initialize(irep, sContext);         
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();

            irep.RegisterAdd(entity);
            return entity;
        }
        public static GY_JUESEQX CreateNewIfNotExists(IGY_JUESEQXRepository irep, ServiceContext sContext, E_GY_JUESEQX_NEW dto)
        {
            var entity = irep.GetByKey(dto.JUESEID, dto.QUANXIANID);
            if (entity == null)
            {
                entity = dto.EToDB<E_GY_JUESEQX_NEW, GY_JUESEQX>();
                entity.Initialize(irep, sContext);
                irep.RegisterAdd(entity);
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();

            return entity;
        }
    }
}
