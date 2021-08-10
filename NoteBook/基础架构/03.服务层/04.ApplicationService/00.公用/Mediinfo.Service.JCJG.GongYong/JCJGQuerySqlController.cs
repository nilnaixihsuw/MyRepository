using Mediinfo.Enterprise;
using Mediinfo.Enterprise.PagedResult;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    /// <summary>
    /// 公用sql查询处理类
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGQuerySql/{action}")]
    public class JCJGQuerySqlController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 根据传入的SQL查询数据库，并返回对应的DataTable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<JsonDataTable> GetDataTableBySql(string sql)
        {
            Parm<string> parm = new Parm<string>(sql);
            DataTable data = new QueryService(UnitOfWork).Get(parm);
            return ServiceContent(new JsonDataTable(data));
        }

        /// <summary>
        /// 根据传入的SQL查询数据库，并返回对应的DataTable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<PagedTableResult> GetPagedDataTableBySql(string sql,int pageIndex,int pageSize )
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();
            var data = new QueryService(UnitOfWork).GetPagedTable(sql, parms, pageIndex, pageSize, "");
            return ServiceContent(new PagedTableResult(data.TotalRecords,data.TotalPages,pageSize,pageIndex,data.PageData));
        }
    }
}
