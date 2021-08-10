using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_QINGLINGDAN1Factory
	{
        /*
		 
		public static GY_QINGLINGDAN1 CreateIfNotExists(IGY_QINGLINGDAN1Repository irep, ServiceContext sContext, E_GY_QINGLINGDAN1 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_QINGLINGDAN1();
			}  
			return entity;
		}
		*/
        //public static GY_QINGLINGDAN1 CreateIfNotExists(IGY_QINGLINGDAN1Repository irep, ServiceContext sContext, E_GY_GONGYONGQL dto)
        //{
        //    var entity = irep.GetByKey(dto.QINGLINGDID);
        //    if (entity == null)
        //    {
        //        entity = dto.EToDB<E_GY_GONGYONGQL, GY_QINGLINGDAN1>();
        //        entity.QINGLINGDID = irep.GetOrder("GY_QINGLINGDAN1", sContext.YUANQUID)[0].ToString();
        //        entity.QINGLINGDH = irep.GetOrder(sContext.YINGYONGID, sContext.KUCUNYYID)[0].ToString();
        //        entity.YINGYONGID = sContext.KUCUNYYID;
        //        entity.ZHIDANREN = sContext.USERID;
        //        entity.DANJUZT = "1";
        //        entity.ZHIDANRQ = irep.GetSYSTime();
        //        entity.YUANQUID = sContext.YUANQUID;
        //        //entity.BEIQINGLYYID = string.IsNullOrEmpty(entity.BEIQINGLYYID) ? sContext.KUCUNYYID : entity.BEIQINGLYYID;
        //        //entity.BEIQINGLKS = string.IsNullOrEmpty(entity.BEIQINGLKS) ? sContext.KESHIID : entity.BEIQINGLKS;
        //        //entity.QINGLINGKS = string.IsNullOrEmpty(entity.QINGLINGKS) ? sContext.y : entity.QINGLINGKS;
        //        entity.Initialize(irep, sContext);
        //        irep.RegisterAdd<GY_QINGLINGDAN1>(entity);
        //    }
        //    return entity;
        //}

  //      public static GY_QINGLINGDAN1 Create(IGY_QINGLINGDAN1Repository irep,ServiceContext sContext, E_GY_GONGYONGQL dto )
		//{
  //          GY_QINGLINGDAN1 entity = dto.EToDB<E_GY_GONGYONGQL, GY_QINGLINGDAN1>();
  //          entity.QINGLINGDID = irep.GetOrder("GY_QINGLINGDAN1", sContext.YUANQUID)[0].ToString();
  //          entity.YINGYONGID = sContext.KUCUNYYID;
  //          entity.ZHIDANREN = sContext.USERID;
  //          entity.DANJUZT = "1";
  //          entity.ZHIDANRQ = irep.GetSYSTime();
  //          entity.YUANQUID = sContext.YUANQUID;
  //          //entity.BEIQINGLYYID = string.IsNullOrEmpty(entity.BEIQINGLYYID) ? sContext.KUCUNYYID : entity.BEIQINGLYYID;
  //          //entity.BEIQINGLKS = string.IsNullOrEmpty(entity.BEIQINGLKS) ? sContext.KESHIID : entity.BEIQINGLKS;
  //          //entity.QINGLINGKS = string.IsNullOrEmpty(entity.QINGLINGKS) ? sContext.y : entity.QINGLINGKS;
  //          entity.Initialize(irep, sContext);
  //          irep.RegisterAdd<GY_QINGLINGDAN1>(entity);
  //          return entity;
		//}
	}
}
