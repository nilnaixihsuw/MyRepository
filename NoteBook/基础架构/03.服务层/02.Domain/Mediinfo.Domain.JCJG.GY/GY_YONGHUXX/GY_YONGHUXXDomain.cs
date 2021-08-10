using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YONGHUXX
	{
        /// <summary>
        /// ��������
        /// </summary>
        public void ResetPassword(string newPassword)
        {
            string errMsg = ""; 
             
            var canShu = this.GetCanShu( ServiceContext.YINGYONGID, "����_�û������Ƿ����", "0");

            if (canShu == "1")
                 MIMA = SHA256.Encrypt(newPassword);
            else
                MIMA = newPassword;

            MIMAXGSJ = GetSYSTime();

            UpdateYongHuXX(YONGHUID);
        }

        /// <summary>
        /// ��������������޸����룬��ʱ�û����ܻ�û�е�¼��ϵͳ����ServiceContextû��
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
        /// ɾ��
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YONGHUXX>(this);
        }
        /// <summary>
        /// ����
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
