using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public partial class XT_XIAOXISJX_NEW
	{
        /// <summary>
        /// �Ķ��ռ���
        /// </summary>
        /// <returns></returns>
        public XT_XIAOXISJX_NEW YueDu()
        {
            this.YUEDUBZ = 1;
            this.YUEDUSJ = DateTime.Now;
            return this;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public XT_XIAOXISJX_NEW JieShou()
        {
            this.JIESHOUBZ = 1;
            this.JIESHOUSJ = DateTime.Now;
            return this;
        }

        public bool YiDu()
        {
            return this.YUEDUBZ == 1;
        }
    }
}
