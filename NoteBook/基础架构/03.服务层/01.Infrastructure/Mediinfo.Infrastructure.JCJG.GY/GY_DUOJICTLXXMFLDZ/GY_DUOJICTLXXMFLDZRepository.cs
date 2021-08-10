using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;
using System.Data;
using Mediinfo.Infrastructure.Core;
using System.Dynamic;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DUOJICTLXXMFLDZRepository : RepositoryBase<GY_DUOJICTLXXMFLDZ>, IGY_DUOJICTLXXMFLDZRepository
	{
		public GY_DUOJICTLXXMFLDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        public List<GY_DUOJICTLXXMFLDZ> GetList(string xiangMuID,string xiangMuLX)
        {
            var list = this.Set<GY_DUOJICTLXXMFLDZ>().Where(o =>o.XIANGMUID==xiangMuID&&o.XIANGMULX==xiangMuLX).ToList().WithContext(this, ServiceContext);
            return list;
        }
        public List<GY_DUOJICTLXXMFLDZ> GetList(string fenLeiID, decimal? shouFeiJC)
        {
            var list = this.Set<GY_DUOJICTLXXMFLDZ>().Where(o=>o.FENLEIID==fenLeiID&&o.SHOUFEIJC<shouFeiJC).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<dynamic> GetShuLList( string bingRenZYID,decimal? shouFeiJC)
        {
            string chaXunSQL = string.Format(
                    @" Nvl(Sum(ShuLiang), 0) As ShuLiang
                From ZY_FeiYong1 a, GY_DuoJiCTLXXMFLDZ b
               Where a.XiangMuID = b.XiangMuID
                 And a.XiangMuLX = b.XiangMuLX
                 And BingRenZYID ='{0}'
                 And b.FenLeiID = s_FenLeiID
                 And b.ShouFeiJC <{1}
               Group By Trunc(a.FaShengRQ)
              Union All
              Select Nvl(Sum(ShuLiang), 0) As Shuliang
                From ZY_JiFeiJK1 a, GY_DuoJiCTLXXMFLDZ b
               Where a.XiangMuID = b.XiangMuID
                 And a.XiangMuLX = b.XiangMuLX
                 And BingRenZYID = '{0}'
                 And b.FenLeiID = s_FenLeiID
                 And b.ShouFeiJC <{1}", bingRenZYID, shouFeiJC);

            DataTable ziDongJFXM = new QueryService(IRepoContext).Get(new Parm<string>(chaXunSQL), false);
            List<dynamic> result = new List<dynamic>();

            foreach (DataRow item in ziDongJFXM.Rows)
            {
                dynamic d = new ExpandoObject();
                d.SHULIANG = item[0].ToString();
                
                result.Add(d);
            }
            return result;


        }
    }
}
