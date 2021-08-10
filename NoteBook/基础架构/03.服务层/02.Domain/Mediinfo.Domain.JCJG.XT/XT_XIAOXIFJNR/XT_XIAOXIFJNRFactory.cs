using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_XIAOXIFJNRFactory
	{
        /*
		 
		public static XT_XIAOXIFJNR CreateIfNotExists(IXT_XIAOXIFJNRRepository irep, ServiceContext sContext, E_XT_XIAOXIFJNR dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_XIAOXIFJNR();
			}  
			return entity;
		}
		*/


        public static XT_XIAOXIFJNR Create(IXT_XIAOXIFJNRRepository irep,long fuJianID, int shunXuHao, string fuJianNR)
        {
            XT_XIAOXIFJNR entity = new XT_XIAOXIFJNR();
            entity.FUJIANID =fuJianID;
            entity.FUJIANNR = fuJianNR;
            entity.SHUNXUHAO = shunXuHao;
            return entity;
        }



    }
}
