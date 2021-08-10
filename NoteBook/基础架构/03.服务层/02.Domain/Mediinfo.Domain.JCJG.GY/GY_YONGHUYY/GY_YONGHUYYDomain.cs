using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YONGHUYY
	{
        /// <summary>
        /// ɾ�� ����_�û�Ӧ��
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YONGHUYY>(this);
        }
        /// <summary>
        /// ���� ����_�û�Ӧ��
        /// </summary>
        /// <param name="eYongHuXX"></param>
        public GY_YONGHUYY Update(E_GY_YONGHUYY eYongHuYY)
        {
            this.MargeDTO<GY_YONGHUYY, E_GY_YONGHUYY>(eYongHuYY);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
