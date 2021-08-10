using System;
using Mediinfo.Enterprise;

namespace Mediinfo.Domain.JCJG.XT
{
    public static class XT_SHUJUZDHCFactory
    {
        /*
		 
		public static XT_SHUJUZDHC CreateIfNotExists(IXT_SHUJUZDHCRepository irep, ServiceContext sContext, E_XT_SHUJUZDHC dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_SHUJUZDHC();
			}  
			return entity;
		}
		*/

        /*
		public static XT_SHUJUZDHC Create(IXT_SHUJUZDHCRepository irep,ServiceContext sContext,E_XT_SHUJUZDHC dto )
		{
			XT_SHUJUZDHC entity = new XT_SHUJUZDHC();
			return entity;
		}
		 
		*/
        public static XT_SHUJUZDHC Create(IXT_SHUJUZDHCRepository irep, ServiceContext sContext, string yingYongID, string sqlID)
        {
            XT_SHUJUZDHC item = new XT_SHUJUZDHC();
            item.HUANCUNID = Guid.NewGuid().ToString();
            item.YINGYONGID = yingYongID;
            item.SQLID = sqlID;
            item.XIUGAIREN = sContext.USERID;
            item.XIUGAISJ = irep.GetSYSTime();
            return item;
        }
    }
}
