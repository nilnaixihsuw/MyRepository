using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIEZHI
	{
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="eJieZhi"></param>
        /// <returns></returns>
        public GY_JIEZHI Update(E_GY_JIEZHI eJieZhi)
        {
            this.MargeDTO<GY_JIEZHI, E_GY_JIEZHI>(eJieZhi);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate(this);
        }

        public GY_JIEZHI UpdateJieZhiHao(string jieZhiHao)
        {
            this.JIEZHIHAO = jieZhiHao;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate(this);
        }
         
        /// <summary>
        /// ���Ͻ���
        /// </summary>
        /// <param name="eJieZhi"></param>
        /// <returns></returns>
        public GY_JIEZHI ZuoFei()
        { 
            this.ZUOFEIBZ = 1;
            this.ZUOFEIREN = ServiceContext.USERID;
            this.ZUOFEISJ = GetSYSTime();
            return this.RegisterUpdate(this);
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="eJieZhi"></param>
        /// <returns></returns>
        public GY_JIEZHI QuXiaoZF()
        {
            this.ZUOFEIBZ = 0;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate(this);
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        public GY_JIEZHI Delete()
        {
            IRepositoyBase.RegisterDelete<GY_JIEZHI>(this);
            return this;
        }
	}
}
