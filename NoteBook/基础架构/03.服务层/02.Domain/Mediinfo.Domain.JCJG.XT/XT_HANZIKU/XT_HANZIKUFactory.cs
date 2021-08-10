using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_HANZIKUFactory
	{
        /*
		 
		public static XT_HANZIKU CreateIfNotExists(IXT_HANZIKURepository irep, ServiceContext sContext, E_XT_HANZIKU dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_HANZIKU();
			}  
			return entity;
		}
		*/

        public static XT_HANZIKU Create(IXT_HANZIKURepository irep, ServiceContext sContext, E_XT_HanZiKu e_HanZiKu)
        { 
            var hanZiKu = e_HanZiKu.EToDB<E_XT_HanZiKu, XT_HANZIKU>();
            hanZiKu.Initialize(irep, sContext);
            irep.RegisterAdd(hanZiKu);
            return hanZiKu;
        }

    }
}
