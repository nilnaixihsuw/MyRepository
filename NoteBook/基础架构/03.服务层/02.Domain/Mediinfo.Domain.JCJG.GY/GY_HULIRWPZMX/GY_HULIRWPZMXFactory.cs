using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_HULIRWPZMXFactory
	{
        /*
		 
		public static GY_HULIRWPZMX CreateIfNotExists(IGY_HULIRWPZMXRepository irep, ServiceContext sContext, E_GY_HULIRWPZMX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_HULIRWPZMX();
			}  
			return entity;
		}
		*/

        /*
		public static GY_HULIRWPZMX Create(IGY_HULIRWPZMXRepository irep,ServiceContext sContext,E_GY_HULIRWPZMX dto )
		{
			GY_HULIRWPZMX entity = new GY_HULIRWPZMX();
			return entity;
		}
		 
		*/


        public static GY_HULIRWPZMX Create(IGY_HULIRWPZMXRepository irep, ServiceContext sContext, 
                            E_GY_HULIRWPZMX dto)
        {
            GY_HULIRWPZMX entity = new GY_HULIRWPZMX();
            entity.HULIRWID = dto.HULIRWID;
            entity.SHUJULY = dto.SHUJULY;
            entity.XIANGMUID = dto.XIANGMUID;
            entity.XIANGMUMC = dto.XIANGMUMC;
            return entity;
        }

        ///// <summary>
        ///// 创建用于删除的就
        ///// </summary>
        ///// <returns></returns>
        //public static GY_HULIRWPZMX GreateForDeleteOld(IGY_HULIRWPZMXRepository irep, ServiceContext sContext,
        //                    E_GY_HULIRWPZMX dto)
        //{


        //}


    }
}
