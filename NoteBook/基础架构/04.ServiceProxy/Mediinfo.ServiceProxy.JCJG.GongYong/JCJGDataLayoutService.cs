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
    public partial class JCJGDataLayoutService
    {
        public ServiceClient serviceClient = null;
        public JCJGDataLayoutService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO> GetDataLayoutInfo(System.String controlName,System.String formName,System.String nameSpace,System.String yingYongID)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>("JCJGDataLayout", "GetDataLayoutInfo",new ServiceParm(nameof(controlName), controlName),new ServiceParm(nameof(formName), formName),new ServiceParm(nameof(nameSpace), nameSpace),new ServiceParm(nameof(yingYongID), yingYongID));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>> GetDataLayoutInfoAsync(System.String controlName,System.String formName,System.String nameSpace,System.String yingYongID)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>("JCJGDataLayout", "GetDataLayoutInfo",new ServiceParm(nameof(controlName), controlName),new ServiceParm(nameof(formName), formName),new ServiceParm(nameof(nameSpace), nameSpace),new ServiceParm(nameof(yingYongID), yingYongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>> GetDataLayoutInfoByYingYongId(System.String yingYongId)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>>("JCJGDataLayout", "GetDataLayoutInfoByYingYongId",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>>> GetDataLayoutInfoByYingYongIdAsync(System.String yingYongId)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUTDTO>>("JCJGDataLayout", "GetDataLayoutInfoByYingYongId",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT1>> GetDataLayout1ByPara(System.String controlName,System.String formName,System.String yingYongID,System.String nameSpace)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT1>>("JCJGDataLayout", "GetDataLayout1ByPara",new ServiceParm(nameof(controlName), controlName),new ServiceParm(nameof(formName), formName),new ServiceParm(nameof(yingYongID), yingYongID),new ServiceParm(nameof(nameSpace), nameSpace));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT1>>> GetDataLayout1ByParaAsync(System.String controlName,System.String formName,System.String yingYongID,System.String nameSpace)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT1>>("JCJGDataLayout", "GetDataLayout1ByPara",new ServiceParm(nameof(controlName), controlName),new ServiceParm(nameof(formName), formName),new ServiceParm(nameof(yingYongID), yingYongID),new ServiceParm(nameof(nameSpace), nameSpace));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT2>> GetDataLayout2ByID(System.String dataLayoutID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT2>>("JCJGDataLayout", "GetDataLayout2ByID",new ServiceParm(nameof(dataLayoutID), dataLayoutID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT2>>> GetDataLayout2ByIDAsync(System.String dataLayoutID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT2>>("JCJGDataLayout", "GetDataLayout2ByID",new ServiceParm(nameof(dataLayoutID), dataLayoutID));
        }
        public Result<System.Boolean> SaveDataLayoutInfo(Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT1 eDataLayout1,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT2> eDataLayout2)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGDataLayout", "SaveDataLayoutInfo",new ServiceParm(nameof(eDataLayout1), eDataLayout1),new ServiceParm(nameof(eDataLayout2), eDataLayout2));
        }
        public async Task<Result<System.Boolean>> SaveDataLayoutInfoAsync(Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT1 eDataLayout1,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DATALAYOUT2> eDataLayout2)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGDataLayout", "SaveDataLayoutInfo",new ServiceParm(nameof(eDataLayout1), eDataLayout1),new ServiceParm(nameof(eDataLayout2), eDataLayout2));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

