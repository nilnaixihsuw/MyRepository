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
    public partial class JCJGKeShiService
    {
        public ServiceClient serviceClient = null;
        public JCJGKeShiService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<System.String>> Get()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<System.String>>("JCJGKeShi", "Get");
        }
        public async Task<Result<System.Collections.Generic.List<System.String>>> GetAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<System.String>>("JCJGKeShi", "Get");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>> GetKeShi()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeShi");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>> GetKeShiAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeShi");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>> GetKeShiXX(System.String keShiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeShiXX",new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>> GetKeShiXXAsync(System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeShiXX",new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>> GetKeShiCFXE(System.String yuanQuID,System.String keShiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeShiCFXE",new ServiceParm(nameof(yuanQuID), yuanQuID),new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>> GetKeShiCFXEAsync(System.String yuanQuID,System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeShiCFXE",new ServiceParm(nameof(yuanQuID), yuanQuID),new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQUGUANLIANKS>> GetGuanLianBQList(System.String yuanQuID,System.String keShiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQUGUANLIANKS>>("JCJGKeShi", "GetGuanLianBQList",new ServiceParm(nameof(yuanQuID), yuanQuID),new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQUGUANLIANKS>>> GetGuanLianBQListAsync(System.String yuanQuID,System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQUGUANLIANKS>>("JCJGKeShi", "GetGuanLianBQList",new ServiceParm(nameof(yuanQuID), yuanQuID),new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>> GetKeiShiTree(System.Boolean onlyNotCancel = true)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeiShiTree",new ServiceParm(nameof(onlyNotCancel), onlyNotCancel));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>> GetKeiShiTreeAsync(System.Boolean onlyNotCancel = true)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetKeiShiTree",new ServiceParm(nameof(onlyNotCancel), onlyNotCancel));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>> GetHuLiDYTree()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetHuLiDYTree");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>> GetHuLiDYTreeAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGKeShi", "GetHuLiDYTree");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_JK_GY_KESHI>> GetKeShiLB_BingAnJK()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_JK_GY_KESHI>>("JCJGKeShi", "GetKeShiLB_BingAnJK");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_JK_GY_KESHI>>> GetKeShiLB_BingAnJKAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_JK_GY_KESHI>>("JCJGKeShi", "GetKeShiLB_BingAnJK");
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

