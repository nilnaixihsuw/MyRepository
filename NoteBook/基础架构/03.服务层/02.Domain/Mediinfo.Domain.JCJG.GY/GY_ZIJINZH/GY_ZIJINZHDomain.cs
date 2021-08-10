using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZIJINZH
	{
        /// <summary>
        /// �����ʽ��˻���Ϣ���������ӽ����ٽ�����ĩ���
        /// </summary>
        /// <param name="eGYZiJinZHDTO"></param>
        /// <returns></returns>
        public GY_ZIJINZH Update(E_GY_ZIJINZH eGYZiJinZHDTO)
        {
            this.MargeDTO<GY_ZIJINZH, E_GY_ZIJINZH>(eGYZiJinZHDTO);
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// �������ӽ�����ĩ���
        /// </summary>
        /// <param name="zengJiaJE">���ӽ��</param>
        /// <param name="qiMoJE">��ĩ���</param>
        /// <returns></returns>
        public GY_ZIJINZH ZengJiaJE(decimal? zengJiaJE,decimal? qiMoJE)
        {
            this.ZENGJIAJE = zengJiaJE;
            this.QIMOJE = qiMoJE;
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���¼��ٽ�����ĩ���
        /// </summary>
        /// <param name="jianShaoJE">���ٽ��</param>
        /// <param name="qiMoJE">��ĩ���</param>
        /// <returns></returns>
        public GY_ZIJINZH JianShaoJE(decimal? jianShaoJE, decimal? qiMoJE)
        {
            this.JIANSHAOJE = jianShaoJE;
            this.QIMOJE = qiMoJE;
            this.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// �����˻����ơ��˻��ȼ����˻�����
        /// </summary>
        /// <param name="zhangHuMC">�˻�����</param>
        /// <param name="zhangHuDJ">�˻��ȼ�</param>
        /// <param name="zhangHuLX">�˻�����1������2����λ</param>
        /// <returns></returns>
        public GY_ZIJINZH UpdateZhuangHuMCDELX(string zhangHuMC,string zhangHuDJ,int? zhangHuLX)
        {
            this.ZHANGHUMC = zhangHuMC;
            this.ZHANGHUDJ = zhangHuDJ;
            this.GERENDWBZ = zhangHuLX;
            this.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// ����ֱ���ʽ��˻����ʺ�
        /// </summary>
        /// <param name="jieZhiHao"></param>
        /// <returns></returns>
        public GY_ZIJINZH UpdateJieZhiHao(string jieZhiHao)
        {
            this.JIEZHIHAO = jieZhiHao; 
            return this.RegisterUpdate(this);
        }
    }
}
