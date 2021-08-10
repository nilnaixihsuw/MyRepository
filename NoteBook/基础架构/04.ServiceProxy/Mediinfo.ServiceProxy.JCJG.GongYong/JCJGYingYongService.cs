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
    public partial class JCJGYingYongService
    {
        public ServiceClient serviceClient = null;
        public JCJGYingYongService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetAll()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetAll");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetAllAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetAll");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGKS>> GetYingYongKS()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGKS>>("JCJGYingYong", "GetYingYongKS");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGKS>>> GetYingYongKSAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGKS>>("JCJGYingYong", "GetYingYongKS");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetGuaHaoSFYY()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetGuaHaoSFYY");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetGuaHaoSFYYAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetGuaHaoSFYY");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetChuRuKuYY(System.String chuRukBZ)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetChuRuKuYY",new ServiceParm(nameof(chuRukBZ), chuRukBZ));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetChuRuKuYYAsync(System.String chuRukBZ)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetChuRuKuYY",new ServiceParm(nameof(chuRukBZ), chuRukBZ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetListByYYID(System.String yingYongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetListByYYID",new ServiceParm(nameof(yingYongID), yingYongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetListByYYIDAsync(System.String yingYongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetListByYYID",new ServiceParm(nameof(yingYongID), yingYongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetGYList()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetGYList");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetGYListAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetGYList");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetLinChuangKJList()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetLinChuangKJList");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetLinChuangKJListAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetLinChuangKJList");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetLinChuangKJ2List()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetLinChuangKJ2List");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetLinChuangKJ2ListAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGYingYong", "GetLinChuangKJ2List");
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

