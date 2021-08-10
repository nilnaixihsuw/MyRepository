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
    public partial class JCJGXiaoXiService
    {
        public ServiceClient serviceClient = null;
        public JCJGXiaoXiService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Boolean> ChuFaXiaoXi(System.Int64 xiaoXiID,System.Nullable<System.DateTime> youXiaoSj,System.Int32 yiCiXBZ)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGXiaoXi", "ChuFaXiaoXi",new ServiceParm(nameof(xiaoXiID), xiaoXiID),new ServiceParm(nameof(youXiaoSj), youXiaoSj),new ServiceParm(nameof(yiCiXBZ), yiCiXBZ));
        }
        public async Task<Result<System.Boolean>> ChuFaXiaoXiAsync(System.Int64 xiaoXiID,System.Nullable<System.DateTime> youXiaoSj,System.Int32 yiCiXBZ)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGXiaoXi", "ChuFaXiaoXi",new ServiceParm(nameof(xiaoXiID), xiaoXiID),new ServiceParm(nameof(youXiaoSj), youXiaoSj),new ServiceParm(nameof(yiCiXBZ), yiCiXBZ));
        }
        public Result<System.Boolean> YueDuXiaoXi(System.Int64 xiaoXiID,System.String userID)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGXiaoXi", "YueDuXiaoXi",new ServiceParm(nameof(xiaoXiID), xiaoXiID),new ServiceParm(nameof(userID), userID));
        }
        public async Task<Result<System.Boolean>> YueDuXiaoXiAsync(System.Int64 xiaoXiID,System.String userID)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGXiaoXi", "YueDuXiaoXi",new ServiceParm(nameof(xiaoXiID), xiaoXiID),new ServiceParm(nameof(userID), userID));
        }
        public Result<System.Boolean> JieShouXiaoXi(System.Int64 xiaoXiID)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGXiaoXi", "JieShouXiaoXi",new ServiceParm(nameof(xiaoXiID), xiaoXiID));
        }
        public async Task<Result<System.Boolean>> JieShouXiaoXiAsync(System.Int64 xiaoXiID)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGXiaoXi", "JieShouXiaoXi",new ServiceParm(nameof(xiaoXiID), xiaoXiID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>> GetLiXianXX(System.String zaiXianZTID,System.DateTime faSongSJ)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>("JCJGXiaoXi", "GetLiXianXX",new ServiceParm(nameof(zaiXianZTID), zaiXianZTID),new ServiceParm(nameof(faSongSJ), faSongSJ));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>> GetLiXianXXAsync(System.String zaiXianZTID,System.DateTime faSongSJ)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>("JCJGXiaoXi", "GetLiXianXX",new ServiceParm(nameof(zaiXianZTID), zaiXianZTID),new ServiceParm(nameof(faSongSJ), faSongSJ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>> GetYiDuXX(System.String zaiXianZTID,System.DateTime faSongSJ)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>("JCJGXiaoXi", "GetYiDuXX",new ServiceParm(nameof(zaiXianZTID), zaiXianZTID),new ServiceParm(nameof(faSongSJ), faSongSJ));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>> GetYiDuXXAsync(System.String zaiXianZTID,System.DateTime faSongSJ)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>("JCJGXiaoXi", "GetYiDuXX",new ServiceParm(nameof(zaiXianZTID), zaiXianZTID),new ServiceParm(nameof(faSongSJ), faSongSJ));
        }
        public Result<System.Collections.Generic.Dictionary<System.String,System.String>> GetXiaoXiChuLiCKDZ()
        {
            return serviceClient.Invoke<System.Collections.Generic.Dictionary<System.String,System.String>>("JCJGXiaoXi", "GetXiaoXiChuLiCKDZ");
        }
        public async Task<Result<System.Collections.Generic.Dictionary<System.String,System.String>>> GetXiaoXiChuLiCKDZAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.Dictionary<System.String,System.String>>("JCJGXiaoXi", "GetXiaoXiChuLiCKDZ");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXIBM>> GetZiDingYiXXBM()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXIBM>>("JCJGXiaoXi", "GetZiDingYiXXBM");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXIBM>>> GetZiDingYiXXBMAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXIBM>>("JCJGXiaoXi", "GetZiDingYiXXBM");
        }
        public Result<System.String> GetBingRenXMByID(System.Int32 menZhenZyBz,System.String bingRenID)
        {
            return serviceClient.Invoke<System.String>("JCJGXiaoXi", "GetBingRenXMByID",new ServiceParm(nameof(menZhenZyBz), menZhenZyBz),new ServiceParm(nameof(bingRenID), bingRenID));
        }
        public async Task<Result<System.String>> GetBingRenXMByIDAsync(System.Int32 menZhenZyBz,System.String bingRenID)
        {
            return await serviceClient.InvokeAsync<System.String>("JCJGXiaoXi", "GetBingRenXMByID",new ServiceParm(nameof(menZhenZyBz), menZhenZyBz),new ServiceParm(nameof(bingRenID), bingRenID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SHOUJIANREN_NEW>> GetShouJianRen(System.String shouJianRenID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SHOUJIANREN_NEW>>("JCJGXiaoXi", "GetShouJianRen",new ServiceParm(nameof(shouJianRenID), shouJianRenID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SHOUJIANREN_NEW>>> GetShouJianRenAsync(System.String shouJianRenID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_SHOUJIANREN_NEW>>("JCJGXiaoXi", "GetShouJianRen",new ServiceParm(nameof(shouJianRenID), shouJianRenID));
        }
        public Result<System.Boolean> FaSongXX(System.String xiaoXiBT,System.String xiaoXiZY,System.String xiaoXiNR,System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXIDY_NEW> xiaoXiList,System.Collections.Generic.Dictionary<System.String,System.String> XiaoXiFJ)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGXiaoXi", "FaSongXX",new ServiceParm(nameof(xiaoXiBT), xiaoXiBT),new ServiceParm(nameof(xiaoXiZY), xiaoXiZY),new ServiceParm(nameof(xiaoXiNR), xiaoXiNR),new ServiceParm(nameof(xiaoXiList), xiaoXiList),new ServiceParm(nameof(XiaoXiFJ), XiaoXiFJ));
        }
        public async Task<Result<System.Boolean>> FaSongXXAsync(System.String xiaoXiBT,System.String xiaoXiZY,System.String xiaoXiNR,System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXIDY_NEW> xiaoXiList,System.Collections.Generic.Dictionary<System.String,System.String> XiaoXiFJ)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGXiaoXi", "FaSongXX",new ServiceParm(nameof(xiaoXiBT), xiaoXiBT),new ServiceParm(nameof(xiaoXiZY), xiaoXiZY),new ServiceParm(nameof(xiaoXiNR), xiaoXiNR),new ServiceParm(nameof(xiaoXiList), xiaoXiList),new ServiceParm(nameof(XiaoXiFJ), XiaoXiFJ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>> GetYiDuXX_YH(System.String zaiXianZTID, System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody> messAgeBodyList, System.Collections.Generic.List<System.String> wjzBM)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>("JCJGXiaoXi", "GetYiDuXX_YH", new ServiceParm(nameof(zaiXianZTID), zaiXianZTID), new ServiceParm(nameof(messAgeBodyList), messAgeBodyList), new ServiceParm(nameof(wjzBM), wjzBM));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>> GetYiDuXX_YHAsync(System.String zaiXianZTID, System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody> messAgeBodyList, System.Collections.Generic.List<System.String> wjzBM)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.HIS.Core.HISMessageBody>>("JCJGXiaoXi", "GetYiDuXX_YH", new ServiceParm(nameof(zaiXianZTID), zaiXianZTID), new ServiceParm(nameof(messAgeBodyList), messAgeBodyList), new ServiceParm(nameof(wjzBM), wjzBM));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXI_NEW>> GetXiaoXiSJ(System.String xiaoXiID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXI_NEW>>("JCJGXiaoXi", "GetXiaoXiSJ", new ServiceParm(nameof(xiaoXiID), xiaoXiID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXI_NEW>>> GetXiaoXiSJAsync(System.String xiaoXiID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.XT.E_XT_XIAOXI_NEW>>("JCJGXiaoXi", "GetXiaoXiSJ", new ServiceParm(nameof(xiaoXiID), xiaoXiID));
        }
    }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

