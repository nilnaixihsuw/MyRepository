using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_LVSETDYPSZ
	{
        /// <summary>
        /// ���� ����_��ɫͨ��ҩƷ����
        /// </summary>
        /// <param name="eLvSeTDYPSZ_EX"></param>
        /// <returns></returns>
        public GY_LVSETDYPSZ Update(E_GY_LVSETDYPSZ_EX eLvSeTDYPSZ_EX)
        {
            this.MargeDTO<GY_LVSETDYPSZ, E_GY_LVSETDYPSZ_EX>(eLvSeTDYPSZ_EX);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }
        /// <summary>
        /// ���� ����_��ɫͨ��ҩƷ����
        /// </summary>
        /// <param name="eLvSeTDYPSZ"></param>
        /// <returns></returns>
        public GY_LVSETDYPSZ Update(E_GY_LVSETDYPSZ eLvSeTDYPSZ)
        {
            this.MargeDTO<GY_LVSETDYPSZ, E_GY_LVSETDYPSZ>(eLvSeTDYPSZ);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }
        /// <summary>
        /// ɾ�� ���� ����_��ɫͨ��ҩƷ����
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_LVSETDYPSZ>(this);
        }
    }
}
