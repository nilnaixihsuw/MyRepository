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
    public partial class JCJGBuJuQXService
    {
        public ServiceClient serviceClient = null;
        public JCJGBuJuQXService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Int32> IsBuJuQX(System.String yonghuID)
        {
            return serviceClient.Invoke<System.Int32>("JCJGBuJuQX", "IsBuJuQX",new ServiceParm(nameof(yonghuID), yonghuID));
        }
        public async Task<Result<System.Int32>> IsBuJuQXAsync(System.String yonghuID)
        {
            return await serviceClient.InvokeAsync<System.Int32>("JCJGBuJuQX", "IsBuJuQX",new ServiceParm(nameof(yonghuID), yonghuID));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

