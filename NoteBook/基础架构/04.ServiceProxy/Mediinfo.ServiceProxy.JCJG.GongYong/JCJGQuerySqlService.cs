//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.ServiceProxy.JCJG.GongYong
{
    public partial class JCJGQuerySqlService
    {
        public ServiceClient serviceClient = null;
        public JCJGQuerySqlService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<Mediinfo.Enterprise.JsonDataTable> GetDataTableBySql(System.String sql)
        {
            return serviceClient.Invoke<Mediinfo.Enterprise.JsonDataTable>("JCJGQuerySql", "GetDataTableBySql",new ServiceParm(nameof(sql), sql));
        }
        public async Task<Result<Mediinfo.Enterprise.JsonDataTable>> GetDataTableBySqlAsync(System.String sql)
        {
            return await serviceClient.InvokeAsync<Mediinfo.Enterprise.JsonDataTable>("JCJGQuerySql", "GetDataTableBySql",new ServiceParm(nameof(sql), sql));
        }
        public Result<Mediinfo.Enterprise.PagedResult.PagedTableResult> GetPagedDataTableBySql(System.String sql,System.Int32 pageIndex,System.Int32 pageSize)
        {
            return serviceClient.Invoke<Mediinfo.Enterprise.PagedResult.PagedTableResult>("JCJGQuerySql", "GetPagedDataTableBySql",new ServiceParm(nameof(sql), sql),new ServiceParm(nameof(pageIndex), pageIndex),new ServiceParm(nameof(pageSize), pageSize));
        }
        public async Task<Result<Mediinfo.Enterprise.PagedResult.PagedTableResult>> GetPagedDataTableBySqlAsync(System.String sql,System.Int32 pageIndex,System.Int32 pageSize)
        {
            return await serviceClient.InvokeAsync<Mediinfo.Enterprise.PagedResult.PagedTableResult>("JCJGQuerySql", "GetPagedDataTableBySql",new ServiceParm(nameof(sql), sql),new ServiceParm(nameof(pageIndex), pageIndex),new ServiceParm(nameof(pageSize), pageSize));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

