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
    public partial class JCJGQuanXianService
    {
        public ServiceClient serviceClient = null;
        public JCJGQuanXianService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<System.String>> Get()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<System.String>>("JCJGQuanXian", "Get");
        }
        public async Task<Result<System.Collections.Generic.List<System.String>>> GetAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<System.String>>("JCJGQuanXian", "Get");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>> GetQuanXianByQXID(System.String quanXianID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>>("JCJGQuanXian", "GetQuanXianByQXID",new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>>> GetQuanXianByQXIDAsync(System.String quanXianID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>>("JCJGQuanXian", "GetQuanXianByQXID",new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public Result<System.Boolean> XinJianQX(System.String quanXianID)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGQuanXian", "XinJianQX",new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public async Task<Result<System.Boolean>> XinJianQXAsync(System.String quanXianID)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGQuanXian", "XinJianQX",new ServiceParm(nameof(quanXianID), quanXianID));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

