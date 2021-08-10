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
    public partial class JCJGCaiDanService
    {
        public ServiceClient serviceClient = null;
        public JCJGCaiDanService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong", "V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>> GetCaiDanNew(System.String caiDanID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGCaiDan", "GetCaiDanNew", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>> GetCaiDanNewAsync(System.String caiDanID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGCaiDan", "GetCaiDanNew", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>> GetYingYongCD()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGCaiDan", "GetYingYongCD");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>> GetYingYongCDAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGCaiDan", "GetYingYongCD");
        }
        public Result<System.Boolean> EditCaiDan(System.String caiDanID, System.Int32 isOpen)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGCaiDan", "EditCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(isOpen), isOpen));
        }
        public async Task<Result<System.Boolean>> EditCaiDanAsync(System.String caiDanID, System.Int32 isOpen)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGCaiDan", "EditCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(isOpen), isOpen));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>> GetChangYongCaiDanList()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetChangYongCaiDanList");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>> GetChangYongCaiDanListAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetChangYongCaiDanList");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>> GetALLChangYongCaiDanList()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetALLChangYongCaiDanList");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>> GetALLChangYongCaiDanListAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetALLChangYongCaiDanList");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>> GetChangYongCaiDanByCaiDanID(System.String caiDanID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetChangYongCaiDanByCaiDanID", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>> GetChangYongCaiDanByCaiDanIDAsync(System.String caiDanID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetChangYongCaiDanByCaiDanID", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>> GetALLChangYongCaiDanByCaiDanID(System.String caiDanID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetALLChangYongCaiDanByCaiDanID", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>> GetALLChangYongCaiDanByCaiDanIDAsync(System.String caiDanID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetALLChangYongCaiDanByCaiDanID", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public Result<System.Boolean> EditChangYongCaiDan(System.String caiDanID, System.Int32 isFavorite, System.Int32 xuhao)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGCaiDan", "EditChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(isFavorite), isFavorite), new ServiceParm(nameof(xuhao), xuhao));
        }
        public async Task<Result<System.Boolean>> EditChangYongCaiDanAsync(System.String caiDanID, System.Int32 isFavorite, System.Int32 xuhao)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGCaiDan", "EditChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(isFavorite), isFavorite), new ServiceParm(nameof(xuhao), xuhao));
        }
        public Result<System.Boolean> EditALLChangYongCaiDan(System.String caiDanID, System.Int32 isALLFavorite, System.Int32 xuhao)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGCaiDan", "EditALLChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(isALLFavorite), isALLFavorite), new ServiceParm(nameof(xuhao), xuhao));
        }
        public async Task<Result<System.Boolean>> EditALLChangYongCaiDanAsync(System.String caiDanID, System.Int32 isALLFavorite, System.Int32 xuhao)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGCaiDan", "EditALLChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(isALLFavorite), isALLFavorite), new ServiceParm(nameof(xuhao), xuhao));
        }
        public Result<System.Int32> getChangYongCDXH()
        {
            return serviceClient.Invoke<System.Int32>("JCJGCaiDan", "getChangYongCDXH");
        }
        public async Task<Result<System.Int32>> getChangYongCDXHAsync()
        {
            return await serviceClient.InvokeAsync<System.Int32>("JCJGCaiDan", "getChangYongCDXH");
        }
        public Result<System.Int32> getQJChangYongCDXH()
        {
            return serviceClient.Invoke<System.Int32>("JCJGCaiDan", "getQJChangYongCDXH");
        }
        public async Task<Result<System.Int32>> getQJChangYongCDXHAsync()
        {
            return await serviceClient.InvokeAsync<System.Int32>("JCJGCaiDan", "getQJChangYongCDXH");
        }
        public Result<System.Boolean> AddChangYongCaiDan(System.String caiDanID, System.String caiDanMC, System.Int32 xuhao)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGCaiDan", "AddChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(caiDanMC), caiDanMC), new ServiceParm(nameof(xuhao), xuhao));
        }
        public async Task<Result<System.Boolean>> AddChangYongCaiDanAsync(System.String caiDanID, System.String caiDanMC, System.Int32 xuhao)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGCaiDan", "AddChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(caiDanMC), caiDanMC), new ServiceParm(nameof(xuhao), xuhao));
        }
        public Result<System.Boolean> AddALLChangYongCaiDan(System.String caiDanID, System.String caiDanMC, System.Int32 xuhao)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGCaiDan", "AddALLChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(caiDanMC), caiDanMC), new ServiceParm(nameof(xuhao), xuhao));
        }
        public async Task<Result<System.Boolean>> AddALLChangYongCaiDanAsync(System.String caiDanID, System.String caiDanMC, System.Int32 xuhao)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGCaiDan", "AddALLChangYongCaiDan", new ServiceParm(nameof(caiDanID), caiDanID), new ServiceParm(nameof(caiDanMC), caiDanMC), new ServiceParm(nameof(xuhao), xuhao));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>> GetALLQJChangYongCaiDanByCaiDanID(System.String caiDanID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetALLQJChangYongCaiDanByCaiDanID", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>> GetALLQJChangYongCaiDanByCaiDanIDAsync(System.String caiDanID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CHANGYONGCAIDAN>>("JCJGCaiDan", "GetALLQJChangYongCaiDanByCaiDanID", new ServiceParm(nameof(caiDanID), caiDanID));
        }
        //注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！
    }
}
