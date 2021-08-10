using Mediinfo.Enterprise.PagedResult;
using Mediinfo.Infrastructure.Core.DBContext;

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Mediinfo.Infrastructure.Oracle.Core
{
    /// <summary>
    /// 分页查询的实现
    /// </summary>
    public class PagedQuery : IPagedQuery
    {
        private DBContextBase dBContextBase = null;
        public PagedQuery(DBContextBase dBContextBase)
        {
            this.dBContextBase = dBContextBase;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql">sql</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public IPagedResult<T> Query<T>(string strSql, Dictionary<string, object> parameters, int pageIndex = 1, int pageSize = 20, string orderBy = "") where T : class
        {
            string sql = "SELECT * FROM (SELECT A.*, ROWNUM R FROM (" + strSql;
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy;
            }
            sql += " )A where ROWNUM <=" + pageIndex * pageSize + ") WHERE R> " + (pageIndex - 1) * pageSize;
            
            List<DbParameter> paraList = new List<DbParameter>();

            var cmd = dBContextBase.Database.Connection.CreateCommand();

            parameters.ToList().ForEach(o => {
                var parm = cmd.CreateParameter();
                parm.ParameterName = o.Key;
                parm.Value = o.Value;
                paraList.Add(parm);
            });

            int totalRecords = dBContextBase.Database.SqlQuery<int>("Select Count(1) From (" + strSql + ")", paraList.ToArray()).FirstOrDefault();
            var entityResult = dBContextBase.Database.SqlQuery<T>(sql, paraList.ToArray()).ToList();

            // 计算余页
            int pages = 0;
            if (totalRecords % pageSize != 0)
                pages = 1;
            else
                pages = 0;
            // 获取页数（pages用于考虑有余数的情况）
            long totalPages = totalRecords / pageSize + pages;

            PagedResultBuilder<T> builder = new PagedResultBuilder<T>(totalRecords, totalPages, pageSize, pageIndex, entityResult);
            return builder.Build();
        }
    }
}
