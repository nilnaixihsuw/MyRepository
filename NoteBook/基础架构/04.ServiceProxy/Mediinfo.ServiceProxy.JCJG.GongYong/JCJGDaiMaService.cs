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
    public partial class JCJGDaiMaService
    {
        public ServiceClient serviceClient = null;
        public JCJGDaiMaService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GETDaiMa()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMa");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GETDaiMaAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMa");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GETDaiMaByLB(System.String daiMaLB)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByLB",new ServiceParm(nameof(daiMaLB), daiMaLB));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GETDaiMaByLBAsync(System.String daiMaLB)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByLB",new ServiceParm(nameof(daiMaLB), daiMaLB));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GETDaiMaByJiJiaBW(System.String daiMaLB,System.String ZiFu3)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByJiJiaBW",new ServiceParm(nameof(daiMaLB), daiMaLB),new ServiceParm(nameof(ZiFu3), ZiFu3));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GETDaiMaByJiJiaBWAsync(System.String daiMaLB,System.String ZiFu3)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByJiJiaBW",new ServiceParm(nameof(daiMaLB), daiMaLB),new ServiceParm(nameof(ZiFu3), ZiFu3));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GetDaiMaByLBS(System.Collections.Generic.List<System.String> daiMaLB)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GetDaiMaByLBS",new ServiceParm(nameof(daiMaLB), daiMaLB));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GetDaiMaByLBSAsync(System.Collections.Generic.List<System.String> daiMaLB)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GetDaiMaByLBS",new ServiceParm(nameof(daiMaLB), daiMaLB));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GETDaiMaByLBZF1(System.String daiMaLB,System.String zifu1)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByLBZF1",new ServiceParm(nameof(daiMaLB), daiMaLB),new ServiceParm(nameof(zifu1), zifu1));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GETDaiMaByLBZF1Async(System.String daiMaLB,System.String zifu1)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByLBZF1",new ServiceParm(nameof(daiMaLB), daiMaLB),new ServiceParm(nameof(zifu1), zifu1));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GETDaiMaByDaiMaID(System.String daiMaLB,System.String daiMaID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByDaiMaID",new ServiceParm(nameof(daiMaLB), daiMaLB),new ServiceParm(nameof(daiMaID), daiMaID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GETDaiMaByDaiMaIDAsync(System.String daiMaLB,System.String daiMaID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GETDaiMaByDaiMaID",new ServiceParm(nameof(daiMaLB), daiMaLB),new ServiceParm(nameof(daiMaID), daiMaID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMALB>> GETDaiMaLB()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMALB>>("JCJGDaiMa", "GETDaiMaLB");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMALB>>> GETDaiMaLBAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMALB>>("JCJGDaiMa", "GETDaiMaLB");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSJFXM>> GETGeiYaoFSJFXM(System.String geiYaoFSID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSJFXM>>("JCJGDaiMa", "GETGeiYaoFSJFXM",new ServiceParm(nameof(geiYaoFSID), geiYaoFSID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSJFXM>>> GETGeiYaoFSJFXMAsync(System.String geiYaoFSID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSJFXM>>("JCJGDaiMa", "GETGeiYaoFSJFXM",new ServiceParm(nameof(geiYaoFSID), geiYaoFSID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSDZ>> GETGeiYaoFSDZ(System.String geiYaoFSID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSDZ>>("JCJGDaiMa", "GETGeiYaoFSDZ",new ServiceParm(nameof(geiYaoFSID), geiYaoFSID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSDZ>>> GETGeiYaoFSDZAsync(System.String geiYaoFSID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_GEIYAOFSDZ>>("JCJGDaiMa", "GETGeiYaoFSDZ",new ServiceParm(nameof(geiYaoFSID), geiYaoFSID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JIXINGDZ>> GETJiXingDz(System.String jiXingID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JIXINGDZ>>("JCJGDaiMa", "GETJiXingDz",new ServiceParm(nameof(jiXingID), jiXingID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JIXINGDZ>>> GETJiXingDzAsync(System.String jiXingID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JIXINGDZ>>("JCJGDaiMa", "GETJiXingDz",new ServiceParm(nameof(jiXingID), jiXingID));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_SHOUFEIXM_GUAHAO>> GetGuaHaoZLSFXM()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_SHOUFEIXM_GUAHAO>>("JCJGDaiMa", "GetGuaHaoZLSFXM");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_SHOUFEIXM_GUAHAO>>> GetGuaHaoZLSFXMAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_SHOUFEIXM_GUAHAO>>("JCJGDaiMa", "GetGuaHaoZLSFXM");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>> GetYuanQu()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>("JCJGDaiMa", "GetYuanQu");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>> GetYuanQuAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YUANQU>>("JCJGDaiMa", "GetYuanQu");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YOUHUILB>> GetYouHuiLB()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YOUHUILB>>("JCJGDaiMa", "GetYouHuiLB");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YOUHUILB>>> GetYouHuiLBAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_YOUHUILB>>("JCJGDaiMa", "GetYouHuiLB");
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_FEIYONGXZ>> GetFeiYongXZ()
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_FEIYONGXZ>>("JCJGDaiMa", "GetFeiYongXZ");
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_FEIYONGXZ>>> GetFeiYongXZAsync()
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_FEIYONGXZ>>("JCJGDaiMa", "GetFeiYongXZ");
        }
        public Result<System.Nullable<System.DateTime>> GetSysTime()
        {
            return serviceClient.Invoke<System.Nullable<System.DateTime>>("JCJGDaiMa", "GetSysTime");
        }
        public async Task<Result<System.Nullable<System.DateTime>>> GetSysTimeAsync()
        {
            return await serviceClient.InvokeAsync<System.Nullable<System.DateTime>>("JCJGDaiMa", "GetSysTime");
        }
        public Result<System.Boolean> BaoCunDaiMaLBXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMALB> eDaiMaLB)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGDaiMa", "BaoCunDaiMaLBXX",new ServiceParm(nameof(eDaiMaLB), eDaiMaLB));
        }
        public async Task<Result<System.Boolean>> BaoCunDaiMaLBXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMALB> eDaiMaLB)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGDaiMa", "BaoCunDaiMaLBXX",new ServiceParm(nameof(eDaiMaLB), eDaiMaLB));
        }
        public Result<System.Boolean> BaoCunDaiMaXX(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA> eDaiMa)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGDaiMa", "BaoCunDaiMaXX",new ServiceParm(nameof(eDaiMa), eDaiMa));
        }
        public async Task<Result<System.Boolean>> BaoCunDaiMaXXAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA> eDaiMa)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGDaiMa", "BaoCunDaiMaXX",new ServiceParm(nameof(eDaiMa), eDaiMa));
        }
        public Result<System.Boolean> BaoCunJiXingDZ(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JIXINGDZ> eJiXingDZ)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGDaiMa", "BaoCunJiXingDZ",new ServiceParm(nameof(eJiXingDZ), eJiXingDZ));
        }
        public async Task<Result<System.Boolean>> BaoCunJiXingDZAsync(System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_JIXINGDZ> eJiXingDZ)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGDaiMa", "BaoCunJiXingDZ",new ServiceParm(nameof(eJiXingDZ), eJiXingDZ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GetGYList(System.String DAIMALB,System.Nullable<System.Int32> MENZHENSY,System.Nullable<System.Int32> ZHUYUANSY,System.Int32 ZUOFEIBZ)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GetGYList",new ServiceParm(nameof(DAIMALB), DAIMALB),new ServiceParm(nameof(MENZHENSY), MENZHENSY),new ServiceParm(nameof(ZHUYUANSY), ZHUYUANSY),new ServiceParm(nameof(ZUOFEIBZ), ZUOFEIBZ));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GetGYListAsync(System.String DAIMALB,System.Nullable<System.Int32> MENZHENSY,System.Nullable<System.Int32> ZHUYUANSY,System.Int32 ZUOFEIBZ)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GetGYList",new ServiceParm(nameof(DAIMALB), DAIMALB),new ServiceParm(nameof(MENZHENSY), MENZHENSY),new ServiceParm(nameof(ZHUYUANSY), ZHUYUANSY),new ServiceParm(nameof(ZUOFEIBZ), ZUOFEIBZ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>> GetDaiMaList(System.String DAIMALB,System.Int32 ZUOFEIBZ,System.Int32 MOJIBZ)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GetDaiMaList",new ServiceParm(nameof(DAIMALB), DAIMALB),new ServiceParm(nameof(ZUOFEIBZ), ZUOFEIBZ),new ServiceParm(nameof(MOJIBZ), MOJIBZ));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>> GetDaiMaListAsync(System.String DAIMALB,System.Int32 ZUOFEIBZ,System.Int32 MOJIBZ)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_DAIMA>>("JCJGDaiMa", "GetDaiMaList",new ServiceParm(nameof(DAIMALB), DAIMALB),new ServiceParm(nameof(ZUOFEIBZ), ZUOFEIBZ),new ServiceParm(nameof(MOJIBZ), MOJIBZ));
        }
        public Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_JIANCHAXMSF>> GETJianChaXMSFDMByID(System.String daiMaID)
        {
            return serviceClient.Invoke<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_JIANCHAXMSF>>("JCJGDaiMa", "GETJianChaXMSFDMByID",new ServiceParm(nameof(daiMaID), daiMaID));
        }
        public async Task<Result<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_JIANCHAXMSF>>> GETJianChaXMSFDMByIDAsync(System.String daiMaID)
        {
            return await serviceClient.InvokeAsync<System.Collections.Generic.List<Mediinfo.DTO.HIS.GY.E_GY_V_JIANCHAXMSF>>("JCJGDaiMa", "GETJianChaXMSFDMByID",new ServiceParm(nameof(daiMaID), daiMaID));
        }
        public Result<System.Boolean> SaveDaiMaJCBW(Mediinfo.DTO.HIS.GY.E_GY_DAIMA eDaiMa)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGDaiMa", "SaveDaiMaJCBW",new ServiceParm(nameof(eDaiMa), eDaiMa));
        }
        public async Task<Result<System.Boolean>> SaveDaiMaJCBWAsync(Mediinfo.DTO.HIS.GY.E_GY_DAIMA eDaiMa)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGDaiMa", "SaveDaiMaJCBW",new ServiceParm(nameof(eDaiMa), eDaiMa));
        }


     }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

