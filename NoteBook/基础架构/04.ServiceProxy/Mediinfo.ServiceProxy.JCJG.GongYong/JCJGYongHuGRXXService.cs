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
    public partial class JCJGYongHuGRXXService
    {
        public ServiceClient serviceClient = null;
        public JCJGYongHuGRXXService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>> GetYongHuXXByID(System.String yongHuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>("JCJGYongHuGRXX", "GetYongHuXXByID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>> GetYongHuXXByIDAsync(System.String yongHuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>("JCJGYongHuGRXX", "GetYongHuXXByID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX_EX>> GetYongHuXXByZhiGongGH(System.String zhiGongGH)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX_EX>>("JCJGYongHuGRXX", "GetYongHuXXByZhiGongGH",new ServiceParm(nameof(zhiGongGH), zhiGongGH));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX_EX>>> GetYongHuXXByZhiGongGHAsync(System.String zhiGongGH)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX_EX>>("JCJGYongHuGRXX", "GetYongHuXXByZhiGongGH",new ServiceParm(nameof(zhiGongGH), zhiGongGH));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUPFXX>> GetYongHuPFXXByID(System.String yongHuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUPFXX>>("JCJGYongHuGRXX", "GetYongHuPFXXByID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUPFXX>>> GetYongHuPFXXByIDAsync(System.String yongHuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUPFXX>>("JCJGYongHuGRXX", "GetYongHuPFXXByID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public Result<System.Boolean> SaveYongHuGRXX(Mediinfo.DTO.HIS.GY.E_GY_YONGHUPFXX eYongHuPFXX,Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX eYongHuXX)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGYongHuGRXX", "SaveYongHuGRXX",new ServiceParm(nameof(eYongHuPFXX), eYongHuPFXX),new ServiceParm(nameof(eYongHuXX), eYongHuXX));
        }
        public async Task<Result<System.Boolean>> SaveYongHuGRXXAsync(Mediinfo.DTO.HIS.GY.E_GY_YONGHUPFXX eYongHuPFXX,Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX eYongHuXX)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGYongHuGRXX", "SaveYongHuGRXX",new ServiceParm(nameof(eYongHuPFXX), eYongHuPFXX),new ServiceParm(nameof(eYongHuXX), eYongHuXX));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

