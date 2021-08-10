using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_PIAOJULY
    {
        /// <summary>
        /// ����Ʊ��������Ϣ
        /// </summary>
        /// <param name="ePiaoJuLY"></param>
        /// <returns></returns>
        public GY_PIAOJULY Update(E_GY_PIAOJULY ePiaoJuLY)
        {
            this.MargeDTO<GY_PIAOJULY, E_GY_PIAOJULY>(ePiaoJuLY);
            return this;
        }

        /// <summary>
        /// ��Ʊ������
        /// </summary>
        /// <param name="ePiaoJuLY"></param>
        /// <returns></returns>
        public GY_PIAOJULY YiYongWan()
        {
            this.PIAOJUZT = "2";
            this.TINGYONGRQ = this.GetSYSTime();
            return this;
        }
        /// <summary>
        /// ɾ��Ʊ��������Ϣ
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_PIAOJULY>(this);
        }

        public GY_PIAOJULY TingYongPiaoJu(E_GY_PIAOJULY entity)
        {
            this.MargeDTO<GY_PIAOJULY, E_GY_PIAOJULY>(entity);
            return this;
        }
    }
}
