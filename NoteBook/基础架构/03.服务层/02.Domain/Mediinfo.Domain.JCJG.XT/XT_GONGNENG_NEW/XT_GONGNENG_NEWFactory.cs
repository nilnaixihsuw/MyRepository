using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_GONGNENG_NEWFactory
	{
        /*
		 
		public static XT_GONGNENG_NEW CreateIfNotExists(IXT_GONGNENG_NEWRepository irep, ServiceContext sContext, E_XT_GONGNENG_NEW dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_GONGNENG_NEW();
			}  
			return entity;
		}
		*/


        public static XT_GONGNENG_NEW Create(IXT_GONGNENG_NEWRepository irep, ServiceContext sContext, E_XT_GONGNENG_NEW dto)
        {
            
            var entity = dto.EToDB<E_XT_GONGNENG_NEW, XT_GONGNENG_NEW>();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            if (string.IsNullOrWhiteSpace(entity.GONGNENGROWID))
            {
                entity.GONGNENGROWID = irep.GetOrder("XT_GONGNENG_NEW", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
