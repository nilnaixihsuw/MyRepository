using Mediinfo.DTO.HIS.GY;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_HULIRWPZ
	{

        /// <summary>
        /// ������������һ������Ϣ,��������,�����������,����˳���,�Ƿ����ϵ�
        /// </summary>
        /// <param name="dto"></param>
        public GY_HULIRWPZ UpdateHuliRWPZInfo(E_GY_HULIRWPZ dto)
        {
            this.HULIRWFL = dto.HULIRWFL;
            this.HULIRWMC = dto.HULIRWMC;
            this.SHUNXUHAO = dto.SHUNXUHAO;
            this.BINGQUID = dto.BINGQUID;
            this.ZUOFEIBZ = dto.ZUOFEIBZ;
            this.SHURUMA1 = dto.SHURUMA1;
            this.SHURUMA2 = dto.SHURUMA2;
            this.SHURUMA3 = dto.SHURUMA3;
            this.RegisterUpdate(this);
            return this;



        }
	}
}
