using System;
using Mediinfo.Enterprise;

namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_XIAOXIFJFactory
	{
        /*
		 
		public static XT_XIAOXIFJ CreateIfNotExists(IXT_XIAOXIFJRepository irep, ServiceContext sContext, E_XT_XIAOXIFJ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_XIAOXIFJ();
			}  
			return entity;
		}
		*/


        public static XT_XIAOXIFJ Create(IXT_XIAOXIFJRepository irep,long xiaoXiID, string fuJianMc)
        {
            XT_XIAOXIFJ entity = new XT_XIAOXIFJ();
            entity.FUJIANID =long.Parse(irep.GetOrder("XT_XIAOXIFJ")[0]);
            entity.XIAOXIID =xiaoXiID;
            entity.FUJIANLX = fuJianMc.Substring(fuJianMc.LastIndexOf(".") + 1);
            entity.FUJIANMC = fuJianMc;
            return entity;
        }



    }
}
