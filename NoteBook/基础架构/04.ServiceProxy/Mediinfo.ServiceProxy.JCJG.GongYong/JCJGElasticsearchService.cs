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
    public partial class JCJGElasticsearchService
    {
        public ServiceClient serviceClient = null;
        public JCJGElasticsearchService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Boolean> PutLog(System.String id,System.Int32 status)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGElasticsearch", "PutLog",new ServiceParm(nameof(id), id),new ServiceParm(nameof(status), status));
        }
        public async Task<Result<System.Boolean>> PutLogAsync(System.String id,System.Int32 status)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGElasticsearch", "PutLog",new ServiceParm(nameof(id), id),new ServiceParm(nameof(status), status));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

