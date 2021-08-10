using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_GUANDAORepository : RepositoryBase<GY_GUANDAO>, IGY_GUANDAORepository
	{
		public GY_GUANDAORepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        //public List<GY_GUANDAO> GetList(string guandaojlID)
        //{

        //        var list = (from jl in this.Set<BL_GUANDAOJL>()
        //                    join gd in this.Set<GY_GUANDAO>() on jl.GUANDAOID equals gd.GUANDAOID
        //                    where jl.GUANDAOJLID == guandaojlID

        //                    select gd).ToList();
        //        return list;

        //}

        public List<GY_GUANDAO> GetList(string guandaojlID)
        {
            throw new NotImplementedException();
        }

        public List<GY_GUANDAO> GetList()
        {
            var list = this.Set<GY_GUANDAO>().ToList().WithContext(this, ServiceContext);
            return list;
        }        

        public int GetShunXuHao()
        {
            var list = this.Set<GY_GUANDAO>().ToList().WithContext(this, ServiceContext);
            int? Shunxuhao = list.Max(o => o.SHUNXUHAO);
            if (Shunxuhao != null && Shunxuhao > 0)
            {
                return Convert.ToInt32(Shunxuhao) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
