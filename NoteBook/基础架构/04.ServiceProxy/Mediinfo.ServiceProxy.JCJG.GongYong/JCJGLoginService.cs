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
    public partial class JCJGLoginService
    {
        public ServiceClient serviceClient = null;
        public JCJGLoginService()
        {
            serviceClient = new ServiceClient("JCJG-GongYong","V1");
        }
        public Result<Mediinfo.DTO.HIS.GY.LoginDTO> GetYongHuXByGH(System.String gongHao,System.Collections.Generic.List<Mediinfo.Utility.NetworkConfig> networkList)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.LoginDTO>("JCJGLogin", "GetYongHuXByGH",new ServiceParm(nameof(gongHao), gongHao),new ServiceParm(nameof(networkList), networkList));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.LoginDTO>> GetYongHuXByGHAsync(System.String gongHao,System.Collections.Generic.List<Mediinfo.Utility.NetworkConfig> networkList)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.LoginDTO>("JCJGLogin", "GetYongHuXByGH",new ServiceParm(nameof(gongHao), gongHao),new ServiceParm(nameof(networkList), networkList));
        }
        public Result<Mediinfo.DTO.HIS.GY.LoginDTO> GetUserInfoByID(System.String userID)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.LoginDTO>("JCJGLogin", "GetUserInfoByID",new ServiceParm(nameof(userID), userID));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.LoginDTO>> GetUserInfoByIDAsync(System.String userID)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.LoginDTO>("JCJGLogin", "GetUserInfoByID",new ServiceParm(nameof(userID), userID));
        }
        public Result<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOZHAN> Login(System.String userId,System.String password,System.String yingYongId,System.Collections.Generic.List<Mediinfo.Utility.NetworkConfig> networkList)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOZHAN>("JCJGLogin", "Login",new ServiceParm(nameof(userId), userId),new ServiceParm(nameof(password), password),new ServiceParm(nameof(yingYongId), yingYongId),new ServiceParm(nameof(networkList), networkList));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOZHAN>> LoginAsync(System.String userId,System.String password,System.String yingYongId,System.Collections.Generic.List<Mediinfo.Utility.NetworkConfig> networkList)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.E_GY_GONGZUOZHAN>("JCJGLogin", "Login",new ServiceParm(nameof(userId), userId),new ServiceParm(nameof(password), password),new ServiceParm(nameof(yingYongId), yingYongId),new ServiceParm(nameof(networkList), networkList));
        }
        public Result<Mediinfo.DTO.HIS.XT.E_XT_ZAIXIANZT> ZaiXianZTXZ(Mediinfo.DTO.HIS.XT.E_XT_ZAIXIANZT e_XT_ZAIXIANZT)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.XT.E_XT_ZAIXIANZT>("JCJGLogin", "ZaiXianZTXZ",new ServiceParm(nameof(e_XT_ZAIXIANZT), e_XT_ZAIXIANZT));
        }
        public async Task<Result<Mediinfo.DTO.HIS.XT.E_XT_ZAIXIANZT>> ZaiXianZTXZAsync(Mediinfo.DTO.HIS.XT.E_XT_ZAIXIANZT e_XT_ZAIXIANZT)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.XT.E_XT_ZAIXIANZT>("JCJGLogin", "ZaiXianZTXZ",new ServiceParm(nameof(e_XT_ZAIXIANZT), e_XT_ZAIXIANZT));
        }
        public Result<System.Boolean> Logout(System.String userId,System.String gongZuoZhanID)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGLogin", "Logout",new ServiceParm(nameof(userId), userId),new ServiceParm(nameof(gongZuoZhanID), gongZuoZhanID));
        }
        public async Task<Result<System.Boolean>> LogoutAsync(System.String userId,System.String gongZuoZhanID)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGLogin", "Logout",new ServiceParm(nameof(userId), userId),new ServiceParm(nameof(gongZuoZhanID), gongZuoZhanID));
        }
        public Result<Mediinfo.DTO.HIS.GY.LoginDTO> GetUserInfo(System.String userID)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.LoginDTO>("JCJGLogin", "GetUserInfo",new ServiceParm(nameof(userID), userID));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.LoginDTO>> GetUserInfoAsync(System.String userID)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.LoginDTO>("JCJGLogin", "GetUserInfo",new ServiceParm(nameof(userID), userID));
        }
        public Result<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX> HRPLogin(System.String userID,System.String password)
        {
            return serviceClient.Invoke<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>("JCJGLogin", "HRPLogin",new ServiceParm(nameof(userID), userID),new ServiceParm(nameof(password), password));
        }
        public async Task<Result<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>> HRPLoginAsync(System.String userID,System.String password)
        {
            return await serviceClient.InvokeAsync<Mediinfo.DTO.HIS.GY.E_GY_YONGHUXX>("JCJGLogin", "HRPLogin",new ServiceParm(nameof(userID), userID),new ServiceParm(nameof(password), password));
        }
        public Result<System.Boolean> MiMaJY(System.String gongHao,System.String password)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGLogin", "MiMaJY",new ServiceParm(nameof(gongHao), gongHao),new ServiceParm(nameof(password), password));
        }
        public async Task<Result<System.Boolean>> MiMaJYAsync(System.String gongHao,System.String password)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGLogin", "MiMaJY",new ServiceParm(nameof(gongHao), gongHao),new ServiceParm(nameof(password), password));
        }
        public Result<System.Boolean> JieSuoJY(System.String userId,System.String password)
        {
            return serviceClient.Invoke<System.Boolean>("JCJGLogin", "JieSuoJY",new ServiceParm(nameof(userId), userId),new ServiceParm(nameof(password), password));
        }
        public async Task<Result<System.Boolean>> JieSuoJYAsync(System.String userId,System.String password)
        {
            return await serviceClient.InvokeAsync<System.Boolean>("JCJGLogin", "JieSuoJY",new ServiceParm(nameof(userId), userId),new ServiceParm(nameof(password), password));
        }

        public Result<bool> LoginBanBenHao(System.String ip, System.String yingYongId, System.String banBenHao)
        {
            return serviceClient.Invoke<bool>("JCJGLogin", "LoginBanBenHao", new ServiceParm(nameof(ip), ip), new ServiceParm(nameof(yingYongId), yingYongId), new ServiceParm(nameof(banBenHao), banBenHao));
        }
        public async Task<Result<bool>> LoginBanBenHaoAsync(System.String ip, System.String yingYongId, System.String banBenHao)
        {
            return await serviceClient.InvokeAsync<bool>("JCJGLogin", "LoginBanBenHao", new ServiceParm(nameof(ip), ip), new ServiceParm(nameof(yingYongId), yingYongId), new ServiceParm(nameof(banBenHao), banBenHao));
        }

    }
}
//注意：此代码由HIS6微服务自动生成的客户端代理类，在没有确保安全的情况下，请勿随便修改！

