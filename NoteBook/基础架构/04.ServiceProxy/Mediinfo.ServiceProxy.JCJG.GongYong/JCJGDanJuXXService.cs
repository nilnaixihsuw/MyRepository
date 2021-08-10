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
    public partial class JCJGDanJuXXService
    {
        public ServiceClient serviceClient = null;
        public JCJGDanJuXXService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>> GetDanJuByDanJuID(System.String danJuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetDanJuByDanJuID",new ServiceParm(nameof(danJuID), danJuID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>> GetDanJuByDanJuIDAsync(System.String danJuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetDanJuByDanJuID",new ServiceParm(nameof(danJuID), danJuID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>> GetDanJuByDanJuMC(System.String danJuMC)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetDanJuByDanJuMC",new ServiceParm(nameof(danJuMC), danJuMC));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>> GetDanJuByDanJuMCAsync(System.String danJuMC)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetDanJuByDanJuMC",new ServiceParm(nameof(danJuMC), danJuMC));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>> GetAll()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetAll");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>> GetAllAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetAll");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>> GetALLNotDanJuNR()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetALLNotDanJuNR");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>> GetALLNotDanJuNRAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>>("JCJGDanJuXX", "GetALLNotDanJuNR");
        }
        public Result<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX> SaveDanJuXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX> eDanJu)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>("JCJGDanJuXX", "SaveDanJuXX",new ServiceParm(nameof(eDanJu), eDanJu));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>> SaveDanJuXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX> eDanJu)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.E_GY_DANJUXX>("JCJGDanJuXX", "SaveDanJuXX",new ServiceParm(nameof(eDanJu), eDanJu));
        }
        public Result<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX> CheckLogIn(System.String UserName,System.String Pwd)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>("JCJGDanJuXX", "CheckLogIn",new ServiceParm(nameof(UserName), UserName),new ServiceParm(nameof(Pwd), Pwd));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>> CheckLogInAsync(System.String UserName,System.String Pwd)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>("JCJGDanJuXX", "CheckLogIn",new ServiceParm(nameof(UserName), UserName),new ServiceParm(nameof(Pwd), Pwd));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

