using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_HULIRWPZFactory
	{
        /*
		 
		public static GY_HULIRWPZ CreateIfNotExists(IGY_HULIRWPZRepository irep, ServiceContext sContext, E_GY_HULIRWPZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_HULIRWPZ();
			}  
			return entity;
		}
		*/

        /*
		public static GY_HULIRWPZ Create(IGY_HULIRWPZRepository irep,ServiceContext sContext,E_GY_HULIRWPZ dto )
		{
			GY_HULIRWPZ entity = new GY_HULIRWPZ();
			return entity;
		}
		 
		*/

        public static GY_HULIRWPZ Create(IGY_HULIRWPZRepository irep, ServiceContext sContext, E_GY_HULIRWPZ dto)
        {
            GY_HULIRWPZ entity = new GY_HULIRWPZ();
            entity.HULIRWID = irep.GetOrder("GY_HULIRWPZ")[0];
            entity.HULIRWMC = dto.HULIRWMC;
            entity.HULIRWFL = dto.HULIRWFL;
            entity.SHUNXUHAO = dto.SHUNXUHAO;
            entity.SHURUMA1 = dto.SHURUMA1;
            entity.SHURUMA2 = dto.SHURUMA2;
            entity.SHURUMA3 = dto.SHURUMA3;
            return entity;
        }


    }
}
