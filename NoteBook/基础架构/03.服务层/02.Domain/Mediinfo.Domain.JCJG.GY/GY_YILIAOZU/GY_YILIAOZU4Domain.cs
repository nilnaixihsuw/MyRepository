using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZU4
	{
        /// <summary>
        /// ���� ����_ҽ����4
        /// </summary>
        /// <param name="eYiLiaoZu4"></param>
        public GY_YILIAOZU4 Update(E_GY_YILIAOZU4 eYiLiaoZu4)
        {
            this.MargeDTO<GY_YILIAOZU4, E_GY_YILIAOZU4>(eYiLiaoZu4);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        /// <summary>
        /// ɾ�� ����_ҽ����4
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YILIAOZU4>(this);
        }
    }
}
