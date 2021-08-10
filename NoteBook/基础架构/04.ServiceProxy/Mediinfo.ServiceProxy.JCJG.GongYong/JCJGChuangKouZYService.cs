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
    public partial class JCJGChuangKouZYService
    {
        public ServiceClient serviceClient = null;
        public JCJGChuangKouZYService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>> GetAll()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>("JCJGChuangKouZY", "GetAll");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>> GetAllAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>("JCJGChuangKouZY", "GetAll");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>> GeByID(System.String id)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>("JCJGChuangKouZY", "GeByID",new ServiceParm(nameof(id), id));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>> GeByIDAsync(System.String id)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>("JCJGChuangKouZY", "GeByID",new ServiceParm(nameof(id), id));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>> GetByFromName(System.String nameSpace,System.String formName)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>("JCJGChuangKouZY", "GetByFromName",new ServiceParm(nameof(nameSpace), nameSpace),new ServiceParm(nameof(formName), formName));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>> GetByFromNameAsync(System.String nameSpace,System.String formName)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW>>("JCJGChuangKouZY", "GetByFromName",new ServiceParm(nameof(nameSpace), nameSpace),new ServiceParm(nameof(formName), formName));
        }
        public Result<System.Boolean> Reset(System.String nameSpace,System.String formName)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGChuangKouZY", "Reset",new ServiceParm(nameof(nameSpace), nameSpace),new ServiceParm(nameof(formName), formName));
        }
        public async Task<Result<System.Boolean>> ResetAsync(System.String nameSpace,System.String formName)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGChuangKouZY", "Reset",new ServiceParm(nameof(nameSpace), nameSpace),new ServiceParm(nameof(formName), formName));
        }
        public Result<System.Boolean> Save(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW> chuangKouZYList,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW> jueSeQXList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGChuangKouZY", "Save",new ServiceParm(nameof(chuangKouZYList), chuangKouZYList),new ServiceParm(nameof(jueSeQXList), jueSeQXList));
        }
        public async Task<Result<System.Boolean>> SaveAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHUANGKOUZY_NEW> chuangKouZYList,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX> jueSeQXList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGChuangKouZY", "Save",new ServiceParm(nameof(chuangKouZYList), chuangKouZYList),new ServiceParm(nameof(jueSeQXList), jueSeQXList));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

