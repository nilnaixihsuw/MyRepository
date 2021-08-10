using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_TAOCAN
	{

        /// <summary>
        /// �����ײ�
        /// </summary>
        /// <returns></returns>
        public GY_TAOCAN ZuoFeiTC()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// �ָ��ײ�
        /// </summary>
        /// <returns></returns>
        public GY_TAOCAN HuiFuTC()
        {
            this.ZUOFEIBZ = 0;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// �����ײͱ�
        /// </summary>
        /// <param name="eGongYongTC"></param>
        /// <returns></returns>
        public GY_TAOCAN Update(E_GY_TAOCAN eGongYongTC)
        {
            this.MargeDTO<GY_TAOCAN, E_GY_TAOCAN>(eGongYongTC);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
