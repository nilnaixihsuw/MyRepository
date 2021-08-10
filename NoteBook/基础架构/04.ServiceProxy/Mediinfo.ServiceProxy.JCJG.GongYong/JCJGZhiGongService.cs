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
    public partial class JCJGZhiGongService
    {
        public ServiceClient serviceClient = null;
        public JCJGZhiGongService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>> GetZhiGongXX()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetZhiGongXX");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>> GetZhiGongXXAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetZhiGongXX");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>> GetZhiGongXXByZhiGongLB(System.String zhiGongLB)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetZhiGongXXByZhiGongLB",new ServiceParm(nameof(zhiGongLB), zhiGongLB));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>> GetZhiGongXXByZhiGongLBAsync(System.String zhiGongLB)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetZhiGongXXByZhiGongLB",new ServiceParm(nameof(zhiGongLB), zhiGongLB));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGCFED>> GetZhiGongCFXE(System.String keShiID,System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGCFED>>("JCJGZhiGong", "GetZhiGongCFXE",new ServiceParm(nameof(keShiID), keShiID),new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGCFED>>> GetZhiGongCFXEAsync(System.String keShiID,System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGCFED>>("JCJGZhiGong", "GetZhiGongCFXE",new ServiceParm(nameof(keShiID), keShiID),new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGSYXL>> GetZhiGongSYXL(System.String keShiID,System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGSYXL>>("JCJGZhiGong", "GetZhiGongSYXL",new ServiceParm(nameof(keShiID), keShiID),new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGSYXL>>> GetZhiGongSYXLAsync(System.String keShiID,System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGSYXL>>("JCJGZhiGong", "GetZhiGongSYXL",new ServiceParm(nameof(keShiID), keShiID),new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>> GetZhiGongXXByZhiGongID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetZhiGongXXByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>> GetZhiGongXXByZhiGongIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetZhiGongXXByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>> GetKangJunZGXXByZGID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetKangJunZGXXByZGID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>> GetKangJunZGXXByZGIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetKangJunZGXXByZGID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>> GetKeShiHSZByKSID(System.String keShiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetKeShiHSZByKSID",new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>> GetKeShiHSZByKSIDAsync(System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX>>("JCJGZhiGong", "GetKeShiHSZByKSID",new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>> GetZhiGongKS()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>>("JCJGZhiGong", "GetZhiGongKS");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>>> GetZhiGongKSAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>>("JCJGZhiGong", "GetZhiGongKS");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>> GetZhiGongKSByZhiGongID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>>("JCJGZhiGong", "GetZhiGongKSByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>>> GetZhiGongKSByZhiGongIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS>>("JCJGZhiGong", "GetZhiGongKSByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>> GetYiLiaoZu1()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>>("JCJGZhiGong", "GetYiLiaoZu1");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>>> GetYiLiaoZu1Async()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>>("JCJGZhiGong", "GetYiLiaoZu1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>> GetYiLiaoZu1ByYiLiaoZID(System.String yiLiaoZID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>>("JCJGZhiGong", "GetYiLiaoZu1ByYiLiaoZID",new ServiceParm(nameof(yiLiaoZID), yiLiaoZID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>>> GetYiLiaoZu1ByYiLiaoZIDAsync(System.String yiLiaoZID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1>>("JCJGZhiGong", "GetYiLiaoZu1ByYiLiaoZID",new ServiceParm(nameof(yiLiaoZID), yiLiaoZID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>> GetYiLiaoZu2ByZhiGongID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>>("JCJGZhiGong", "GetYiLiaoZu2ByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>>> GetYiLiaoZu2ByZhiGongIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>>("JCJGZhiGong", "GetYiLiaoZu2ByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>> GetYiLiaoZu2ByYiLiaoZID(System.String yiLiaoZID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>>("JCJGZhiGong", "GetYiLiaoZu2ByYiLiaoZID",new ServiceParm(nameof(yiLiaoZID), yiLiaoZID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>>> GetYiLiaoZu2ByYiLiaoZIDAsync(System.String yiLiaoZID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2>>("JCJGZhiGong", "GetYiLiaoZu2ByYiLiaoZID",new ServiceParm(nameof(yiLiaoZID), yiLiaoZID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU3>> GetYiLiaoZu3ByZhiGongID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU3>>("JCJGZhiGong", "GetYiLiaoZu3ByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU3>>> GetYiLiaoZu3ByZhiGongIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU3>>("JCJGZhiGong", "GetYiLiaoZu3ByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU4>> GetYiLiaoZu4ByYiLiaoZID(System.String yiLiaoZID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU4>>("JCJGZhiGong", "GetYiLiaoZu4ByYiLiaoZID",new ServiceParm(nameof(yiLiaoZID), yiLiaoZID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU4>>> GetYiLiaoZu4ByYiLiaoZIDAsync(System.String yiLiaoZID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU4>>("JCJGZhiGong", "GetYiLiaoZu4ByYiLiaoZID",new ServiceParm(nameof(yiLiaoZID), yiLiaoZID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_HUIZHENZU>> GetHuZhenZuByZGID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_HUIZHENZU>>("JCJGZhiGong", "GetHuZhenZuByZGID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_HUIZHENZU>>> GetHuZhenZuByZGIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_HUIZHENZU>>("JCJGZhiGong", "GetHuZhenZuByZGID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGHSKSYS>> GetZhiGongHSKSYSByZhiGongID(System.String zhiGongID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGHSKSYS>>("JCJGZhiGong", "GetZhiGongHSKSYSByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGHSKSYS>>> GetZhiGongHSKSYSByZhiGongIDAsync(System.String zhiGongID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGHSKSYS>>("JCJGZhiGong", "GetZhiGongHSKSYSByZhiGongID",new ServiceParm(nameof(zhiGongID), zhiGongID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>> GetJueSe()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>>("JCJGZhiGong", "GetJueSe");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>>> GetJueSeAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>>("JCJGZhiGong", "GetJueSe");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>> GetJueSeCKQX()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGZhiGong", "GetJueSeCKQX");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>> GetJueSeCKQXAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGZhiGong", "GetJueSeCKQX");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>> GetJueSeCKQXByID(System.String jueSeID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGZhiGong", "GetJueSeCKQXByID",new ServiceParm(nameof(jueSeID), jueSeID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>> GetJueSeCKQXByIDAsync(System.String jueSeID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGZhiGong", "GetJueSeCKQXByID",new ServiceParm(nameof(jueSeID), jueSeID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>> GetYongHuCKQXByID(System.String quanXianID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGZhiGong", "GetYongHuCKQXByID",new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>> GetYongHuCKQXByIDAsync(System.String quanXianID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGZhiGong", "GetYongHuCKQXByID",new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH>> GetJueSeYHByYongHuID(System.String yongHuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH>>("JCJGZhiGong", "GetJueSeYHByYongHuID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH>>> GetJueSeYHByYongHuIDAsync(System.String yongHuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH>>("JCJGZhiGong", "GetJueSeYHByYongHuID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>> GetJueSeYHEXByYongHuID(System.String yongHuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>>("JCJGZhiGong", "GetJueSeYHEXByYongHuID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>>> GetJueSeYHEXByYongHuIDAsync(System.String yongHuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>>("JCJGZhiGong", "GetJueSeYHEXByYongHuID",new ServiceParm(nameof(yongHuID), yongHuID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>> GetJueSeCKQXNEWByID(System.String jueSeID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>("JCJGZhiGong", "GetJueSeCKQXByID", new ServiceParm(nameof(jueSeID), jueSeID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>> GetJueSeCKQXNEWByIDAsync(System.String jueSeID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>("JCJGZhiGong", "GetJueSeCKQXByID", new ServiceParm(nameof(jueSeID), jueSeID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>> GetYongHuCKQXByIDNEW(System.String quanXianID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>("JCJGZhiGong", "GetYongHuCKQXByIDNEW", new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>> GetYongHuCKQXByID_NEWAsync(System.String quanXianID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>("JCJGZhiGong", "GetYongHuCKQXByID_NEW", new ServiceParm(nameof(quanXianID), quanXianID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>> GetZhiGongUserRootAllXX()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>>("JCJGZhiGong", "GetZhiGongUserRootAllXX");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>>> GetZhiGongUserRootAllXXAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>>("JCJGZhiGong", "GetZhiGongUserRootAllXX");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>> GetZhiGongUserRoot(System.String quanxianid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>>("JCJGZhiGong", "GetZhiGongUserRoot",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>>> GetZhiGongUserRootAsync(System.String quanxianid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX>>("JCJGZhiGong", "GetZhiGongUserRoot",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public Result<System.Boolean> BaoCunYongHuXinXi(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX> yonghuList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYongHuXinXi",new ServiceParm(nameof(yonghuList), yonghuList));
        }
        public async Task<Result<System.Boolean>> BaoCunYongHuXinXiAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGYHQX> yonghuList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYongHuXinXi",new ServiceParm(nameof(yonghuList), yonghuList));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>> GetYongHuXX()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>("JCJGZhiGong", "GetYongHuXX");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>> GetYongHuXXAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>("JCJGZhiGong", "GetYongHuXX");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>> GetYongHuXXByYongHuID(System.String yonghuid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>("JCJGZhiGong", "GetYongHuXXByYongHuID",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>> GetYongHuXXByYongHuIDAsync(System.String yonghuid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>>("JCJGZhiGong", "GetYongHuXXByYongHuID",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX>> GetYongHuYYEXByYongHuID(System.String yonghuid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX>>("JCJGZhiGong", "GetYongHuYYEXByYongHuID",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX>>> GetYongHuYYEXByYongHuIDAsync(System.String yonghuid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX>>("JCJGZhiGong", "GetYongHuYYEXByYongHuID",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>> GetJueSeByID(System.String jueseid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>>("JCJGZhiGong", "GetJueSeByID",new ServiceParm(nameof(jueseid), jueseid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>>> GetJueSeByIDAsync(System.String jueseid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE>>("JCJGZhiGong", "GetJueSeByID",new ServiceParm(nameof(jueseid), jueseid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>> GetJueSeYHEXByID(System.String jueseid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>>("JCJGZhiGong", "GetJueSeYHEXByID",new ServiceParm(nameof(jueseid), jueseid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>>> GetJueSeYHEXByIDAsync(System.String jueseid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX>>("JCJGZhiGong", "GetJueSeYHEXByID",new ServiceParm(nameof(jueseid), jueseid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>> GetQuanXian(System.String quanxianid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>>("JCJGZhiGong", "GetQuanXian",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>>> GetQuanXianAsync(System.String quanxianid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN>>("JCJGZhiGong", "GetQuanXian",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>> GetJueSeQXByJueSeID(System.String jueseid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>("JCJGZhiGong", "GetJueSeQXByJueSeID",new ServiceParm(nameof(jueseid), jueseid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>> GetJueSeQXByJueSeIDAsync(System.String jueseid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>("JCJGZhiGong", "GetJueSeQXByJueSeID",new ServiceParm(nameof(jueseid), jueseid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>> GetJueSeQXByQuanXianID(System.String quanxianid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>("JCJGZhiGong", "GetJueSeQXByQuanXianID",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>> GetJueSeQXByQuanXianIDAsync(System.String quanxianid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX>>("JCJGZhiGong", "GetJueSeQXByQuanXianID",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public Result<System.Boolean> GetJueSeYHQXBYQuanXianID(System.String quanxianid)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "GetJueSeYHQXBYQuanXianID",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public async Task<Result<System.Boolean>> GetJueSeYHQXBYQuanXianIDAsync(System.String quanxianid)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "GetJueSeYHQXBYQuanXianID",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public Result<System.Boolean> ResetPassword(System.String zhiGongID,System.String newPassword)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "ResetPassword",new ServiceParm(nameof(zhiGongID), zhiGongID),new ServiceParm(nameof(newPassword), newPassword));
        }
        public async Task<Result<System.Boolean>> ResetPasswordAsync(System.String zhiGongID,System.String newPassword)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "ResetPassword",new ServiceParm(nameof(zhiGongID), zhiGongID),new ServiceParm(nameof(newPassword), newPassword));
        }
        public Result<System.Boolean> BaoCunZhiGongXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX> eZhiGongXXList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunZhiGongXX",new ServiceParm(nameof(eZhiGongXXList), eZhiGongXXList));
        }
        public async Task<Result<System.Boolean>> BaoCunZhiGongXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX> eZhiGongXXList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunZhiGongXX",new ServiceParm(nameof(eZhiGongXXList), eZhiGongXXList));
        }
        public Result<System.Boolean> BaoCunZhiGongXX2(Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX eZhiGongXX,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS> eZhiGongKSList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunZhiGongXX2",new ServiceParm(nameof(eZhiGongXX), eZhiGongXX),new ServiceParm(nameof(eZhiGongKSList), eZhiGongKSList));
        }
        public async Task<Result<System.Boolean>> BaoCunZhiGongXX2Async(Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX eZhiGongXX,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS> eZhiGongKSList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunZhiGongXX2",new ServiceParm(nameof(eZhiGongXX), eZhiGongXX),new ServiceParm(nameof(eZhiGongKSList), eZhiGongKSList));
        }
        public Result<System.Boolean> BaoCunZhiGongXX3(Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX eZhiGongXX,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH> eJueSeYHList,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS> eZhiGongKSList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunZhiGongXX3",new ServiceParm(nameof(eZhiGongXX), eZhiGongXX),new ServiceParm(nameof(eJueSeYHList), eJueSeYHList),new ServiceParm(nameof(eZhiGongKSList), eZhiGongKSList));
        }
        public async Task<Result<System.Boolean>> BaoCunZhiGongXX3Async(Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX eZhiGongXX,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH> eJueSeYHList,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGKS> eZhiGongKSList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunZhiGongXX3",new ServiceParm(nameof(eZhiGongXX), eZhiGongXX),new ServiceParm(nameof(eJueSeYHList), eJueSeYHList),new ServiceParm(nameof(eZhiGongKSList), eZhiGongKSList));
        }
        public Result<System.Boolean> BaoCunYiLiaoZuXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1> eYiLiaoZu1List)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYiLiaoZuXX",new ServiceParm(nameof(eYiLiaoZu1List), eYiLiaoZu1List));
        }
        public async Task<Result<System.Boolean>> BaoCunYiLiaoZuXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1> eYiLiaoZu1List)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYiLiaoZuXX",new ServiceParm(nameof(eYiLiaoZu1List), eYiLiaoZu1List));
        }
        public Result<System.Boolean> BaoCunYiLiaoZuXX2(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1> eYiLiaoZu1List,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2> eYiLiaoZu2List,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU4> eYiLiaoZu4List)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYiLiaoZuXX2",new ServiceParm(nameof(eYiLiaoZu1List), eYiLiaoZu1List),new ServiceParm(nameof(eYiLiaoZu2List), eYiLiaoZu2List),new ServiceParm(nameof(eYiLiaoZu4List), eYiLiaoZu4List));
        }
        public async Task<Result<System.Boolean>> BaoCunYiLiaoZuXX2Async(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU1> eYiLiaoZu1List,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU2> eYiLiaoZu2List,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YILIAOZU4> eYiLiaoZu4List)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYiLiaoZuXX2",new ServiceParm(nameof(eYiLiaoZu1List), eYiLiaoZu1List),new ServiceParm(nameof(eYiLiaoZu2List), eYiLiaoZu2List),new ServiceParm(nameof(eYiLiaoZu4List), eYiLiaoZu4List));
        }
        public Result<System.Boolean> BaoCunYongHuXX(Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX eYongHuXX)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYongHuXX",new ServiceParm(nameof(eYongHuXX), eYongHuXX));
        }
        public async Task<Result<System.Boolean>> BaoCunYongHuXXAsync(Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX eYongHuXX)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYongHuXX",new ServiceParm(nameof(eYongHuXX), eYongHuXX));
        }
        public Result<System.Boolean> BaoCunYongHuXXList(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX> eYongHuXXList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYongHuXXList",new ServiceParm(nameof(eYongHuXXList), eYongHuXXList));
        }
        public async Task<Result<System.Boolean>> BaoCunYongHuXXListAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX> eYongHuXXList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYongHuXXList",new ServiceParm(nameof(eYongHuXXList), eYongHuXXList));
        }
        public Result<System.Boolean> BaoCunYongHuZGXX(Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX eYongHuXX,Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX eZhiGongXX)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYongHuZGXX",new ServiceParm(nameof(eYongHuXX), eYongHuXX),new ServiceParm(nameof(eZhiGongXX), eZhiGongXX));
        }
        public async Task<Result<System.Boolean>> BaoCunYongHuZGXXAsync(Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX eYongHuXX,Mediinfo.DTO.HIS.GY.E_GY_ZHIGONGXX eZhiGongXX)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYongHuZGXX",new ServiceParm(nameof(eYongHuXX), eYongHuXX),new ServiceParm(nameof(eZhiGongXX), eZhiGongXX));
        }
        public Result<System.Boolean> BaoCunJueSe(Mediinfo.DTO.HIS.GY.E_GY_JUESE eJueSe)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunJueSe",new ServiceParm(nameof(eJueSe), eJueSe));
        }
        public async Task<Result<System.Boolean>> BaoCunJueSeAsync(Mediinfo.DTO.HIS.GY.E_GY_JUESE eJueSe)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunJueSe",new ServiceParm(nameof(eJueSe), eJueSe));
        }
        public Result<System.Boolean> BaoCunJueSeList(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE> eJueSeList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunJueSeList",new ServiceParm(nameof(eJueSeList), eJueSeList));
        }
        public async Task<Result<System.Boolean>> BaoCunJueSeListAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESE> eJueSeList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunJueSeList",new ServiceParm(nameof(eJueSeList), eJueSeList));
        }
        public Result<System.Boolean> BaoCunJueSeYH(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX> eJueSeYHEXList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunJueSeYH",new ServiceParm(nameof(eJueSeYHEXList), eJueSeYHEXList));
        }
        public async Task<Result<System.Boolean>> BaoCunJueSeYHAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX> eJueSeYHEXList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunJueSeYH",new ServiceParm(nameof(eJueSeYHEXList), eJueSeYHEXList));
        }
        public Result<System.Boolean> BaoCunYongHuJSYY(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX> eJueSeYHEXList,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX> eYongHuYYEXList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunYongHuJSYY",new ServiceParm(nameof(eJueSeYHEXList), eJueSeYHEXList),new ServiceParm(nameof(eYongHuYYEXList), eYongHuYYEXList));
        }
        public async Task<Result<System.Boolean>> BaoCunYongHuJSYYAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEYH_EX> eJueSeYHEXList,System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX> eYongHuYYEXList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunYongHuJSYY",new ServiceParm(nameof(eJueSeYHEXList), eJueSeYHEXList),new ServiceParm(nameof(eYongHuYYEXList), eYongHuYYEXList));
        }
        public Result<System.Boolean> BaoCunJueSeQX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX> eJueSeQXList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunJueSeQX",new ServiceParm(nameof(eJueSeQXList), eJueSeQXList));
        }
        public async Task<Result<System.Boolean>> BaoCunJueSeQXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESEQX> eJueSeQXList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunJueSeQX",new ServiceParm(nameof(eJueSeQXList), eJueSeQXList));
        }
        public Result<System.Boolean> BaoCunQuanXian(Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN eQuanXian)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunQuanXian",new ServiceParm(nameof(eQuanXian), eQuanXian));
        }
        public async Task<Result<System.Boolean>> BaoCunQuanXianAsync(Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN eQuanXian)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunQuanXian",new ServiceParm(nameof(eQuanXian), eQuanXian));
        }
        public Result<System.Boolean> BaoCunQuanXianList(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN> eQuanXianList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "BaoCunQuanXianList",new ServiceParm(nameof(eQuanXianList), eQuanXianList));
        }
        public async Task<Result<System.Boolean>> BaoCunQuanXianListAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIAN> eQuanXianList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "BaoCunQuanXianList",new ServiceParm(nameof(eQuanXianList), eQuanXianList));
        }
        public Result<System.Boolean> SaveZhiGongHSKS(System.Int32 prmYeWuLX,System.String prmZhiGongID,System.String prmNewHeSuanKS,System.String prmNewHeSuanKSMC,System.String prmOLDHeSunKS)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGZhiGong", "SaveZhiGongHSKS",new ServiceParm(nameof(prmYeWuLX), prmYeWuLX),new ServiceParm(nameof(prmZhiGongID), prmZhiGongID),new ServiceParm(nameof(prmNewHeSuanKS), prmNewHeSuanKS),new ServiceParm(nameof(prmNewHeSuanKSMC), prmNewHeSuanKSMC),new ServiceParm(nameof(prmOLDHeSunKS), prmOLDHeSunKS));
        }
        public async Task<Result<System.Boolean>> SaveZhiGongHSKSAsync(System.Int32 prmYeWuLX,System.String prmZhiGongID,System.String prmNewHeSuanKS,System.String prmNewHeSuanKSMC,System.String prmOLDHeSunKS)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGZhiGong", "SaveZhiGongHSKS",new ServiceParm(nameof(prmYeWuLX), prmYeWuLX),new ServiceParm(nameof(prmZhiGongID), prmZhiGongID),new ServiceParm(nameof(prmNewHeSuanKS), prmNewHeSuanKS),new ServiceParm(nameof(prmNewHeSuanKSMC), prmNewHeSuanKSMC),new ServiceParm(nameof(prmOLDHeSunKS), prmOLDHeSunKS));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

