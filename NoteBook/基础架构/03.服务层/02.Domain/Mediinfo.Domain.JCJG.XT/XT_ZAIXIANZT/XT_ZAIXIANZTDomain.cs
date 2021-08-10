using Mediinfo.HIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Domain.JCJG.XT
{
    /// <summary>
    /// ����״̬
    /// </summary>
	public partial class XT_ZAIXIANZT
	{
        /// <summary>
        /// ��ǰ�Ľ�ɫȨ��
        /// </summary>
        /// <returns></returns>
        public List<string> DangQianJsQxIDList()
        {
            if(string.IsNullOrWhiteSpace(this.JUESEQX))
            {
                return new List<string>();
            }
            else
            {
                return this.JUESEQX.Split('|').ToList();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public XT_ZAIXIANZT XuZu(XT_ZAIXIANZT xT_ZAIXIANZT)
        {
            this.BINGQUID = xT_ZAIXIANZT.BINGQUID;
            this.KESHIID = xT_ZAIXIANZT.KESHIID;
            this.JIESHUSJ = DateTime.Now.AddMinutes(30);
            return this;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <returns></returns>
        public bool ZaiXian()
        {
            return this.JIESHUSJ >= DateTime.Now;
        }
    }
}
