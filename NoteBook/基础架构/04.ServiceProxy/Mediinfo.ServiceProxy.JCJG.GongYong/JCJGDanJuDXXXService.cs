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
    public partial class JCJGDanJuDXXXService
    {
        public ServiceClient serviceClient = null;
        public JCJGDanJuDXXXService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>> GetDanJuDXByDanJuDXID(System.String danJuDXID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>("JCJGDanJuDXXX", "GetDanJuDXByDanJuDXID",new ServiceParm(nameof(danJuDXID), danJuDXID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>> GetDanJuDXByDanJuDXIDAsync(System.String danJuDXID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>("JCJGDanJuDXXX", "GetDanJuDXByDanJuDXID",new ServiceParm(nameof(danJuDXID), danJuDXID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>> GetDanJuDXByDanJuDXMC(System.String danJuDXMC)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>("JCJGDanJuDXXX", "GetDanJuDXByDanJuDXMC",new ServiceParm(nameof(danJuDXMC), danJuDXMC));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>> GetDanJuDXByDanJuDXMCAsync(System.String danJuDXMC)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>("JCJGDanJuDXXX", "GetDanJuDXByDanJuDXMC",new ServiceParm(nameof(danJuDXMC), danJuDXMC));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>> GetAll()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>("JCJGDanJuDXXX", "GetAll");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>> GetAllAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX>>("JCJGDanJuDXXX", "GetAll");
        }
        public Result<System.String> SaveDanJuDXXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX> eDanJuDX)
        {
            return serviceClient.Invoke<System.String>("JCJGDanJuDXXX", "SaveDanJuDXXX",new ServiceParm(nameof(eDanJuDX), eDanJuDX));
        }
        public async Task<Result<System.String>> SaveDanJuDXXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUDXXX> eDanJuDX)
        {
            return await serviceClient.InvokeAsync<System.String>("JCJGDanJuDXXX", "SaveDanJuDXXX",new ServiceParm(nameof(eDanJuDX), eDanJuDX));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

