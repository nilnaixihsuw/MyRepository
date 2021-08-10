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
    public partial class JCJGQuanXian2NService
    {
        public ServiceClient serviceClient = null;
        public JCJGQuanXian2NService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>> GetQuanXianNewByQXID(System.String quanxianid)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>("JCJGQuanXian2N", "GetQuanXianNewByQXID",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>> GetQuanXianNewByQXIDAsync(System.String quanxianid)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>("JCJGQuanXian2N", "GetQuanXianNewByQXID",new ServiceParm(nameof(quanxianid), quanxianid));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>> GetAllQuanXianSJ()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>("JCJGQuanXian2N", "GetAllQuanXianSJ");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>> GetAllQuanXianSJAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_ERJIYHQX>>("JCJGQuanXian2N", "GetAllQuanXianSJ");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIANYHKZ>> GetYONGHUQXXX(System.String yonghuid,System.String gongnengid,System.String mingmingkj,System.String kongjianmc,System.String chuangtimc)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIANYHKZ>>("JCJGQuanXian2N", "GetYONGHUQXXX",new ServiceParm(nameof(yonghuid), yonghuid),new ServiceParm(nameof(gongnengid), gongnengid),new ServiceParm(nameof(mingmingkj), mingmingkj),new ServiceParm(nameof(kongjianmc), kongjianmc),new ServiceParm(nameof(chuangtimc), chuangtimc));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIANYHKZ>>> GetYONGHUQXXXAsync(System.String yonghuid,System.String gongnengid,System.String mingmingkj,System.String kongjianmc,System.String chuangtimc)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_QUANXIANYHKZ>>("JCJGQuanXian2N", "GetYONGHUQXXX",new ServiceParm(nameof(yonghuid), yonghuid),new ServiceParm(nameof(gongnengid), gongnengid),new ServiceParm(nameof(mingmingkj), mingmingkj),new ServiceParm(nameof(kongjianmc), kongjianmc),new ServiceParm(nameof(chuangtimc), chuangtimc));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

