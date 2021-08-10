using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YONGHUXX
	{
        /// <summary>
        /// 重置密码
        /// </summary>
        public void ResetPassword(string newPassword)
        {
            string errMsg = ""; 
             
            var canShu = this.GetCanShu( ServiceContext.YINGYONGID, "公用_用户密码是否加密", "0");

            if (canShu == "1")
                 MIMA = SHA256.Encrypt(newPassword);
            else
                MIMA = newPassword;

            MIMAXGSJ = GetSYSTime();

            UpdateYongHuXX(YONGHUID);
        }

        /// <summary>
        /// 对于重置密码和修改密码，此时用户可能还没有登录到系统里面ServiceContext没有
        /// </summary>
        /// <param name="zhiGongID"></param>
        public void UpdateYongHuXX(string zhiGongID = "")
        { 
            if (!string.IsNullOrWhiteSpace(zhiGongID))
                this.XIUGAIREN = zhiGongID;
            else
                this.XIUGAIREN = ServiceContext.USERID;

            this.XIUGAISJ = GetSYSTime(); 

        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YONGHUXX>(this);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="eYongHuXX"></param>
        public GY_YONGHUXX Update(E_GY_YONGHUXX eYongHuXX)
        {
            this.MargeDTO<GY_YONGHUXX, E_GY_YONGHUXX>(eYongHuXX);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
