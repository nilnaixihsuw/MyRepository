using Mediinfo.DTO.HIS.ZJ;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.ZJ
{
	public static class ZJ_JIUZHENXXFactory
	{
        /*
		 
		public static ZJ_JIUZHENXX CreateIfNotExists(IZJ_JIUZHENXXRepository irep, ServiceContext sContext, E_ZJ_JIUZHENXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new ZJ_JIUZHENXX();
			}  
			return entity;
		}
		*/

        public static ZJ_JIUZHENXX Create(IZJ_JIUZHENXXRepository irep, ServiceContext sContext, E_ZJ_JIUZHENXX dto)
        {
            ZJ_JIUZHENXX entity = dto.EToDB<E_ZJ_JIUZHENXX, ZJ_JIUZHENXX>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIUZHENID))
            {
                entity.JIUZHENID = irep.GetOrder("ZJ_JIUZHENXX", sContext.YUANQUID)[0].ToString();
            }
            
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
