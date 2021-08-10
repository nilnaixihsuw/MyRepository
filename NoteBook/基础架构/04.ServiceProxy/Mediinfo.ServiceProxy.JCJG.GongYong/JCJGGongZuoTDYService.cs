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
    public partial class JCJGGongZuoTDYService
    {
        public ServiceClient serviceClient = null;
        public JCJGGongZuoTDYService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<System.String>> Get()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<System.String>>("JCJGGongZuoTDY", "Get");
        }
        public async Task<Result<System.Collections.Generic.List<System.String>>> GetAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<System.String>>("JCJGGongZuoTDY", "Get");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOTDY>> GetList()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOTDY>>("JCJGGongZuoTDY", "GetList");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOTDY>>> GetListAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOTDY>>("JCJGGongZuoTDY", "GetList");
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

