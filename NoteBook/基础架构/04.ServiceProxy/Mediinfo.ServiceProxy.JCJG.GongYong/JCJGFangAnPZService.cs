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
    public partial class JCJGFangAnPZService
    {
        public ServiceClient serviceClient = null;
        public JCJGFangAnPZService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>> GetAllFangAn()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>>("JCJGFangAnPZ", "GetAllFangAn");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>>> GetAllFangAnAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>>("JCJGFangAnPZ", "GetAllFangAn");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>> GetFangAn(System.String xiangMu,System.String fangAnMC)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>>("JCJGFangAnPZ", "GetFangAn",new ServiceParm(nameof(xiangMu), xiangMu),new ServiceParm(nameof(fangAnMC), fangAnMC));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>>> GetFangAnAsync(System.String xiangMu,System.String fangAnMC)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SELECTSQL2_EX>>("JCJGFangAnPZ", "GetFangAn",new ServiceParm(nameof(xiangMu), xiangMu),new ServiceParm(nameof(fangAnMC), fangAnMC));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

