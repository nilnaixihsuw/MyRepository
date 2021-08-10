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
    public partial class JCJGYingYongCDService
    {
        public ServiceClient serviceClient = null;
        public JCJGYingYongCDService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_DINGYI>> GetXiTong()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_DINGYI>>("JCJGYingYongCD", "GetXiTong");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_DINGYI>>> GetXiTongAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_DINGYI>>("JCJGYingYongCD", "GetXiTong");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>> GetYingYongCD()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetYingYongCD");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>> GetYingYongCDAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetYingYongCD");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>> GetGongYongCD(System.String YINGYONGID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetGongYongCD",new ServiceParm(nameof(YINGYONGID), YINGYONGID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>> GetGongYongCDAsync(System.String YINGYONGID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetGongYongCD",new ServiceParm(nameof(YINGYONGID), YINGYONGID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGCD>> GetyYongHuYYCD()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGCD>>("JCJGYingYongCD", "GetyYongHuYYCD");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGCD>>> GetyYongHuYYCDAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YINGYONGCD>>("JCJGYingYongCD", "GetyYongHuYYCD");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>> GetXiTongGNNewByXTID(System.String xitongid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>>("JCJGYingYongCD", "GetXiTongGNNewByXTID",new ServiceParm(nameof(xitongid), xitongid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>>> GetXiTongGNNewByXTIDAsync(System.String xitongid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>>("JCJGYingYongCD", "GetXiTongGNNewByXTID",new ServiceParm(nameof(xitongid), xitongid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>> GetXiTongGNNewByGNID(System.String gongnengid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>>("JCJGYingYongCD", "GetXiTongGNNewByGNID",new ServiceParm(nameof(gongnengid), gongnengid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>>> GetXiTongGNNewByGNIDAsync(System.String gongnengid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_GONGNENG_NEW>>("JCJGYingYongCD", "GetXiTongGNNewByGNID",new ServiceParm(nameof(gongnengid), gongnengid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>> GetYingYongCDNewByYYID(System.String yingyongid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetYingYongCDNewByYYID",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>> GetYingYongCDNewByYYIDAsync(System.String yingyongid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetYingYongCDNewByYYID",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>> GetYingYongCDNewByGNID(System.String gongnengid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetYingYongCDNewByGNID",new ServiceParm(nameof(gongnengid), gongnengid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>> GetYingYongCDNewByGNIDAsync(System.String gongnengid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDAN_NEW>>("JCJGYingYongCD", "GetYingYongCDNewByGNID",new ServiceParm(nameof(gongnengid), gongnengid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL>> GetYingYongGJLByYYID(System.String yingyongid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL>>("JCJGYingYongCD", "GetYingYongGJLByYYID",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL>>> GetYingYongGJLByYYIDAsync(System.String yingyongid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL>>("JCJGYingYongCD", "GetYingYongGJLByYYID",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL_NEW>> GetYingYongGJLNewByYYID(System.String yingyongid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL_NEW>>("JCJGYingYongCD", "GetYingYongGJLNewByYYID",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL_NEW>>> GetYingYongGJLNewByYYIDAsync(System.String yingyongid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_CAIDANGJL_NEW>>("JCJGYingYongCD", "GetYingYongGJLNewByYYID",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX>> GetYongHuQX(System.String yonghuid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX>>("JCJGYingYongCD", "GetYongHuQX",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX>>> GetYongHuQXAsync(System.String yonghuid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX>>("JCJGYingYongCD", "GetYongHuQX",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>> GetYongHuJSCKQX(System.String yonghuid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGYingYongCD", "GetYongHuJSCKQX",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>> GetYongHuJSCKQXAsync(System.String yonghuid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX>>("JCJGYingYongCD", "GetYongHuJSCKQX",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>> GetYongHuJSCKQX_NEW(System.String yingyongid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>("JCJGYingYongCD", "GetYongHuJSCKQX_NEW",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>> GetYongHuJSCKQX_NEWAsync(System.String yingyongid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JUESECKQX_NEW>>("JCJGYingYongCD", "GetYongHuJSCKQX_NEW",new ServiceParm(nameof(yingyongid), yingyongid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX2>> GetYongHuQX2(System.String yonghuid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX2>>("JCJGYingYongCD", "GetYongHuQX2",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX2>>> GetYongHuQX2Async(System.String yonghuid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YONGHUQX2>>("JCJGYingYongCD", "GetYongHuQX2",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>> GetSencondLevelRoot(System.String yonghuid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>("JCJGYingYongCD", "GetSencondLevelRoot",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>> GetSencondLevelRootAsync(System.String yonghuid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>("JCJGYingYongCD", "GetSencondLevelRoot",new ServiceParm(nameof(yonghuid), yonghuid));
        }
        public Result<System.Boolean> BaoCunErJiQX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX> erjiquanxianList)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGYingYongCD", "BaoCunErJiQX",new ServiceParm(nameof(erjiquanxianList), erjiquanxianList));
        }
        public async Task<Result<System.Boolean>> BaoCunErJiQXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX> erjiquanxianList)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGYingYongCD", "BaoCunErJiQX",new ServiceParm(nameof(erjiquanxianList), erjiquanxianList));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

