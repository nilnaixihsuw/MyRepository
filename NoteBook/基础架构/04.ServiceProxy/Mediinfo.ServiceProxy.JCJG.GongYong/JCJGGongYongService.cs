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
    public partial class JCJGGongYongService
    {
        public ServiceClient serviceClient = null;
        public JCJGGongYongService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GetGYDaiMa(System.String daimalb)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGGongYong", "GetGYDaiMa",new ServiceParm(nameof(daimalb), daimalb));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GetGYDaiMaAsync(System.String daimalb)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGGongYong", "GetGYDaiMa",new ServiceParm(nameof(daimalb), daimalb));
        }
        public Result<System.String> GetKeShiMCByKeShiID(System.String keShiID)
        {
            return serviceClient.Invoke<System.String>("JCJGGongYong", "GetKeShiMCByKeShiID",new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.String>> GetKeShiMCByKeShiIDAsync(System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.String>("JCJGGongYong", "GetKeShiMCByKeShiID",new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>> GetGongYongKS()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGGongYong", "GetGongYongKS");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>> GetGongYongKSAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHI>>("JCJGGongYong", "GetGongYongKS");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>> GetShouFeiXM()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>>("JCJGGongYong", "GetShouFeiXM");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>>> GetShouFeiXMAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>>("JCJGGongYong", "GetShouFeiXM");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>> GetGuaHaoFXM()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>>("JCJGGongYong", "GetGuaHaoFXM");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>>> GetGuaHaoFXMAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_SHOUFEIXM>>("JCJGGongYong", "GetGuaHaoFXM");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQU>> GetGYBingQu()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQU>>("JCJGGongYong", "GetGYBingQu");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQU>>> GetGYBingQuAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_BINGQU>>("JCJGGongYong", "GetGYBingQu");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>> GetBingQuByKSID(System.String keShiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>("JCJGGongYong", "GetBingQuByKSID",new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>> GetBingQuByKSIDAsync(System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>("JCJGGongYong", "GetBingQuByKSID",new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.DateTime> GetSysDate()
        {
            return serviceClient.Invoke<System.DateTime>("JCJGGongYong", "GetSysDate");
        }
        public async Task<Result<System.DateTime>> GetSysDateAsync()
        {
            return await serviceClient.InvokeAsync<System.DateTime>("JCJGGongYong", "GetSysDate");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>> GetYuanQu()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>("JCJGGongYong", "GetYuanQu");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>> GetYuanQuAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>("JCJGGongYong", "GetYuanQu");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>> GetYuanQuByZuoFei(System.String zuoFeiBZ)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>("JCJGGongYong", "GetYuanQuByZuoFei",new ServiceParm(nameof(zuoFeiBZ), zuoFeiBZ));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>> GetYuanQuByZuoFeiAsync(System.String zuoFeiBZ)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>("JCJGGongYong", "GetYuanQuByZuoFei",new ServiceParm(nameof(zuoFeiBZ), zuoFeiBZ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetYingYong()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGGongYong", "GetYingYong");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetYingYongAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGGongYong", "GetYingYong");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetYingYongSJ(System.String jiaGeID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGGongYong", "GetYingYongSJ",new ServiceParm(nameof(jiaGeID), jiaGeID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetYingYongSJAsync(System.String jiaGeID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGGongYong", "GetYingYongSJ",new ServiceParm(nameof(jiaGeID), jiaGeID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>> GetYingYongDZ()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGGongYong", "GetYingYongDZ");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>> GetYingYongDZAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONG>>("JCJGGongYong", "GetYingYongDZ");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGDY>> GetYingYongDY()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGDY>>("JCJGGongYong", "GetYingYongDY");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGDY>>> GetYingYongDYAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGDY>>("JCJGGongYong", "GetYingYongDY");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>> GetKeShiBQ(System.String bingQuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>("JCJGGongYong", "GetKeShiBQ",new ServiceParm(nameof(bingQuID), bingQuID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>> GetKeShiBQAsync(System.String bingQuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>("JCJGGongYong", "GetKeShiBQ",new ServiceParm(nameof(bingQuID), bingQuID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>> GetBingQukS(System.String keShiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>("JCJGGongYong", "GetBingQukS",new ServiceParm(nameof(keShiID), keShiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>> GetBingQukSAsync(System.String keShiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_KESHIBQ>>("JCJGGongYong", "GetBingQukS",new ServiceParm(nameof(keShiID), keShiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_TUPIANXX>> GetTuPianXX(System.String tuPianID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_TUPIANXX>>("JCJGGongYong", "GetTuPianXX",new ServiceParm(nameof(tuPianID), tuPianID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_TUPIANXX>>> GetTuPianXXAsync(System.String tuPianID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_TUPIANXX>>("JCJGGongYong", "GetTuPianXX",new ServiceParm(nameof(tuPianID), tuPianID));
        }
        public Result<System.Collections.Generic.List<System.String>> GetOrder(System.String xuHaoMC,System.String yuanQuID,System.Int32 Count = 1)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<System.String>>("JCJGGongYong", "GetOrder",new ServiceParm(nameof(xuHaoMC), xuHaoMC),new ServiceParm(nameof(yuanQuID), yuanQuID),new ServiceParm(nameof(Count), Count));
        }
        public async Task<Result<System.Collections.Generic.List<System.String>>> GetOrderAsync(System.String xuHaoMC,System.String yuanQuID,System.Int32 Count = 1)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<System.String>>("JCJGGongYong", "GetOrder",new ServiceParm(nameof(xuHaoMC), xuHaoMC),new ServiceParm(nameof(yuanQuID), yuanQuID),new ServiceParm(nameof(Count), Count));
        }
        public Result<System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.String>>> GetOrderList(System.Collections.Generic.Dictionary<System.String,System.Int32> dicXuHao,System.String yuanQuID)
        {
            return serviceClient.Invoke<System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.String>>>("JCJGGongYong", "GetOrderList",new ServiceParm(nameof(dicXuHao), dicXuHao),new ServiceParm(nameof(yuanQuID), yuanQuID));
        }
        public async Task<Result<System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.String>>>> GetOrderListAsync(System.Collections.Generic.Dictionary<System.String,System.Int32> dicXuHao,System.String yuanQuID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.String>>>("JCJGGongYong", "GetOrderList",new ServiceParm(nameof(dicXuHao), dicXuHao),new ServiceParm(nameof(yuanQuID), yuanQuID));
        }
        public Result<System.Boolean> SaveYuanQuXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU> listYuanQu)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGGongYong", "SaveYuanQuXX",new ServiceParm(nameof(listYuanQu), listYuanQu));
        }
        public async Task<Result<System.Boolean>> SaveYuanQuXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU> listYuanQu)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGGongYong", "SaveYuanQuXX",new ServiceParm(nameof(listYuanQu), listYuanQu));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

