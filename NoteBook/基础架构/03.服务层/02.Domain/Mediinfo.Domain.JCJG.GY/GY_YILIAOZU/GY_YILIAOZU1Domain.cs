using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZU1
	{
        /// <summary>
        /// ���� ����_ҽ����1
        /// </summary>
        /// <param name="eYiLiaoZu"></param>
        /// <returns></returns>
        public GY_YILIAOZU1 Update(E_GY_YILIAOZU1 eYiLiaoZu)
        {
            this.MargeDTO<GY_YILIAOZU1, E_GY_YILIAOZU1>(eYiLiaoZu);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        /// <summary>
        /// ɾ�� ����_ҽ����1
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YILIAOZU1>(this);
        }
    }
}
