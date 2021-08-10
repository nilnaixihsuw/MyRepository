using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_MAZUIQXKZRepository : RepositoryBase<GY_MAZUIQXKZ>, IGY_MAZUIQXKZRepository
	{
		public GY_MAZUIQXKZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public string GetZhiChengID(string prmZhiGongID, int prmMenZhenZYBZ)
        {
            var dbMaZuiQXKZ = this.Set<GY_MAZUIQXKZ>();
            var dbDaiMa = this.Set<GY_DAIMA>();
            string zhichengid = (from dbMaZuiQXKZ1 in dbMaZuiQXKZ
                                 join dbDaiMa2 in dbDaiMa
                                 on dbMaZuiQXKZ1.MAZUIQXJB equals dbDaiMa2.DAIMAID
                                 where dbMaZuiQXKZ1.ZUOFEIBZ == 0
                                 && dbDaiMa2.ZUOFEIBZ == 0
                                 && dbDaiMa2.DAIMALB == "0199"
                                 && dbMaZuiQXKZ1.ZHIGONGID == prmZhiGongID
                                 && ((dbDaiMa2.ZHUYUANSY == 1 && 1 == prmMenZhenZYBZ) ||
                                 (dbDaiMa2.MENZHENSY == 1 && 0 == prmMenZhenZYBZ))
                                 select dbMaZuiQXKZ1.MAZUIQXJB).FirstOrDefault(); 
            return zhichengid;
        }

        public int? GetShenHeTZ(string prmShouShuMCID,string prmZhiGongID)
        {
            int? shenHeTZ = (from dbMaZuiQXKZ1 in this.Set<GY_MAZUIQXKZ>()
                                 where dbMaZuiQXKZ1.ZUOFEIBZ == 0
                                 && dbMaZuiQXKZ1.SHOUSHUMCID == prmShouShuMCID
                                 && dbMaZuiQXKZ1.ZHIGONGID == prmZhiGongID
                             select dbMaZuiQXKZ1.SHENHETZ).FirstOrDefault();
            return shenHeTZ;
        }

        public bool IsAny(string prmShouShuJB, string prmZhiGongID)
        {
            bool IsAny = (from dbMaZuiQXKZ1 in this.Set<GY_MAZUIQXKZ>()
                             where dbMaZuiQXKZ1.ZUOFEIBZ == 0
                             && dbMaZuiQXKZ1.SHOUSHUJB == prmShouShuJB
                             && dbMaZuiQXKZ1.ZHIGONGID == prmZhiGongID
                             && dbMaZuiQXKZ1.SHENHETZ == 1
                             select dbMaZuiQXKZ1.SHENHETZ).Any();
            return IsAny;
        }
    }
}
