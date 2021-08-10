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
    public partial class JCJGPiaoJuCDCSService
    {
        public ServiceClient serviceClient = null;
        public JCJGPiaoJuCDCSService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>> GetPiaoJuCDCS(System.String piaoJuYWID,System.String piaoJuLXID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>>("JCJGPiaoJuCDCS", "GetPiaoJuCDCS",new ServiceParm(nameof(piaoJuYWID), piaoJuYWID),new ServiceParm(nameof(piaoJuLXID), piaoJuLXID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>>> GetPiaoJuCDCSAsync(System.String piaoJuYWID,System.String piaoJuLXID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>>("JCJGPiaoJuCDCS", "GetPiaoJuCDCS",new ServiceParm(nameof(piaoJuYWID), piaoJuYWID),new ServiceParm(nameof(piaoJuLXID), piaoJuLXID));
        }
        public Result<System.Boolean> BaoCunPiaoJuChongDaXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS> piaoJuCDCSlist)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGPiaoJuCDCS", "BaoCunPiaoJuChongDaXX",new ServiceParm(nameof(piaoJuCDCSlist), piaoJuCDCSlist));
        }
        public async Task<Result<System.Boolean>> BaoCunPiaoJuChongDaXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS> piaoJuCDCSlist)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGPiaoJuCDCS", "BaoCunPiaoJuChongDaXX",new ServiceParm(nameof(piaoJuCDCSlist), piaoJuCDCSlist));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>> GetPiaoJuCDCSByYeWuID(System.String piaoJuYWID,System.String piaoJuLXID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>>("JCJGPiaoJuCDCS", "GetPiaoJuCDCSByYeWuID",new ServiceParm(nameof(piaoJuYWID), piaoJuYWID),new ServiceParm(nameof(piaoJuLXID), piaoJuLXID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>>> GetPiaoJuCDCSByYeWuIDAsync(System.String piaoJuYWID,System.String piaoJuLXID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_PIAOJUCDCS>>("JCJGPiaoJuCDCS", "GetPiaoJuCDCSByYeWuID",new ServiceParm(nameof(piaoJuYWID), piaoJuYWID),new ServiceParm(nameof(piaoJuLXID), piaoJuLXID));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

