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
    public partial class JCJGCanSuService
    {
        public ServiceClient serviceClient = null;
        public JCJGCanSuService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>> GetAll()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetAll");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>> GetAllAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetAll");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>> GetByYingYongId(System.String yingYongId)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetByYingYongId",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>> GetByYingYongIdAsync(System.String yingYongId)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetByYingYongId",new ServiceParm(nameof(yingYongId), yingYongId));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>> GetCanShu(System.String canShuId)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetCanShu",new ServiceParm(nameof(canShuId), canShuId));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>> GetCanShuAsync(System.String canShuId)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetCanShu",new ServiceParm(nameof(canShuId), canShuId));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>> GetCanShuZhi(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI> canShuList)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>("JCJGCanSu", "GetCanShuZhi",new ServiceParm(nameof(canShuList), canShuList));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>> GetCanShuZhiAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI> canShuList)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>("JCJGCanSu", "GetCanShuZhi",new ServiceParm(nameof(canShuList), canShuList));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>> GetAndSetCanShuZhi(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI> canShuList)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>("JCJGCanSu", "GetAndSetCanShuZhi",new ServiceParm(nameof(canShuList), canShuList));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>> GetAndSetCanShuZhiAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI> canShuList)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>("JCJGCanSu", "GetAndSetCanShuZhi",new ServiceParm(nameof(canShuList), canShuList));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>> GetChuangKouCanShuZhi(System.String chuangKouMc,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI> canShuList)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>("JCJGCanSu", "GetChuangKouCanShuZhi",new ServiceParm(nameof(chuangKouMc), chuangKouMc),new ServiceParm(nameof(canShuList), canShuList));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>> GetChuangKouCanShuZhiAsync(System.String chuangKouMc,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI> canShuList)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_QUZHI>>("JCJGCanSu", "GetChuangKouCanShuZhi",new ServiceParm(nameof(chuangKouMc), chuangKouMc),new ServiceParm(nameof(canShuList), canShuList));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>> GetParamsByKey(System.Collections.Generic.List<System.String> paramList)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetParamsByKey",new ServiceParm(nameof(paramList), paramList));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>> GetParamsByKeyAsync(System.Collections.Generic.List<System.String> paramList)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU>>("JCJGCanSu", "GetParamsByKey",new ServiceParm(nameof(paramList), paramList));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_ZUOYONGYU>> GetWindowParamByZuoYongYu(System.Collections.Generic.List<System.String> zuoYongYuList)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_ZUOYONGYU>>("JCJGCanSu", "GetWindowParamByZuoYongYu",new ServiceParm(nameof(zuoYongYuList), zuoYongYuList));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_ZUOYONGYU>>> GetWindowParamByZuoYongYuAsync(System.Collections.Generic.List<System.String> zuoYongYuList)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CANSHU_ZUOYONGYU>>("JCJGCanSu", "GetWindowParamByZuoYongYu",new ServiceParm(nameof(zuoYongYuList), zuoYongYuList));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

