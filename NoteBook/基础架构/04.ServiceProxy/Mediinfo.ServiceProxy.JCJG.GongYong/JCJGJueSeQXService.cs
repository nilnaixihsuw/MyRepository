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
    public partial class JCJGJueSeQXService
    {
        public ServiceClient serviceClient = null;
        public JCJGJueSeQXService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<System.String>> Get()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<System.String>>("JCJGJueSeQX", "Get");
        }
        public async Task<Result<System.Collections.Generic.List<System.String>>> GetAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<System.String>>("JCJGJueSeQX", "Get");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>> GetListByQXID(System.String quanXianID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>("JCJGJueSeQX", "GetListByQXID",new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>> GetListByQXIDAsync(System.String quanXianID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>("JCJGJueSeQX", "GetListByQXID",new ServiceParm(nameof(quanXianID), quanXianID));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

