using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YAOPINFL
	{
        /// <summary>
        /// ���� ����_ҩƷ����
        /// </summary>
        /// <param name="yaoPinFLDTO"></param>
        /// <returns></returns>
        public GY_YAOPINFL Update(E_GY_YAOPINFL yaoPinFLDTO)
        {
            this.MargeDTO<GY_YAOPINFL, E_GY_YAOPINFL>(yaoPinFLDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.RegisterUpdate(this);
            return this;
        }       
        /// <summary>
        /// ����_ҩƷ���� ɾ����������ɾ����ֻ�ǽ���������
        /// </summary>
        /// <returns></returns>
        public GY_YAOPINFL ZuoFei()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.RegisterUpdate(this);
            return this;
        }
    }
}
