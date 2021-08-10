using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_GONGNENGFactory
	{
        public static XT_GONGNENG CreateIfNotExists(IXT_GONGNENGRepository irep, ServiceContext sContext, E_XT_GONGNENG dto)
        {
            var entity = irep.GetByKey("1");
            if (entity == null)
            {
                entity = new XT_GONGNENG();
                entity.Initialize(irep, sContext);
            }
            return entity;
        }

        public static XT_GONGNENG Create(IXT_GONGNENGRepository irep, ServiceContext sContext, E_XT_GONGNENG dto)
        {
            XT_GONGNENG entity = dto.EToDB<E_XT_GONGNENG, XT_GONGNENG>();
            entity.Initialize(irep, sContext);
            //entity.QUANXIANID = irep.GetOrder("GY_QUANXIAN")[0];
            entity.XITONGSJ = irep.GetSYSTime();
           // entity.XIUGAIREN = sContext.USERID;
            //entity.XIUGAISJ = irep.GetSYSTime();

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
