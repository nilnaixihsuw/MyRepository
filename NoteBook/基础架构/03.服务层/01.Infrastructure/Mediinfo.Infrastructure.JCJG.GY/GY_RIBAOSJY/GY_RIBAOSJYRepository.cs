using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_RIBAOSJYRepository : RepositoryBase<GY_RIBAOSJY>, IGY_RIBAOSJYRepository
	{
		public GY_RIBAOSJYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 根据传入的SQl动态获取数据
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public List<dynamic> GetDYListBySQL(string SQL)
        {
            DataTable dtRiBao = new QueryService(IRepoContext).Get(new Parm<string>(SQL), false);

            List<dynamic> result = new List<dynamic>();

            foreach (DataRow item in dtRiBao.Rows)
            {
                dynamic d = new ExpandoObject(); 
                d.column1 = item[0].ToString();
                if (item.ItemArray.Length >= 2) d.column2 = item[1].ToString();
                if (item.ItemArray.Length >= 3) d.column3 = item[2].ToString();
                if (item.ItemArray.Length >= 4) d.column4 = item[3].ToString();
                if (item.ItemArray.Length >= 5) d.column5 = item[4].ToString();
                if (item.ItemArray.Length >= 6) d.column6 = item[5].ToString();
                result.Add(d);
            }

            return result;
        }
        /// <summary>
        /// 根据传入的SQl动态获取日报信息
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns> 
        //public List<E_MZ_RIBAOXX> GetListBySQL(string SQL)
        //{
        //    DataTable dtRiBao = new QueryService(IRepoContext).Get(new Parm<string>(SQL), false);
        //    List<E_MZ_RIBAOXX> result = new List<E_MZ_RIBAOXX>();

        //    foreach (DataRow item in dtRiBao.Rows)
        //    {
        //        E_MZ_RIBAOXX riBaoXX = new E_MZ_RIBAOXX();

        //        riBaoXX.JINE = decimal.Parse(item[0].ToString());
        //        riBaoXX.MOBANXMID = item[1].ToString();
                 
        //        result.Add(riBaoXX);
        //    } 
        //    return result;
        //}
        /// <summary>
        /// 根据sql更新已做日报数据
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int UpdateBySQL(string SQL)
        {
            var num = this.SqlQuery<dynamic>(SQL).ToList(); 
            return num.Count;
        }
    }
}
