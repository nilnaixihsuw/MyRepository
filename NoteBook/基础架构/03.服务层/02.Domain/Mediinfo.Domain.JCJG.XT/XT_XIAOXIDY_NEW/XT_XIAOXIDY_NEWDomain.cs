using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Domain.JCJG.XT
{
	public partial class XT_XIAOXIDY_NEW
	{
        public bool AllowJueSeQx(List<string> jueSeIDList)
        {
            return string.IsNullOrWhiteSpace(this.JUESEQX) 
                || this.JUESEQX.Split('|').Any(m => jueSeIDList.Contains(m));
        }

        public bool AllowYingYongQx(string yingYongID)
        {
            return string.IsNullOrWhiteSpace(YINGYONGQX) 
                || this.YINGYONGQX.Split('|').Contains(yingYongID);
        }

        public string GetShouJianZuID()
        {
            switch (GetDingYueRLx())
            {
                case DingYueRLx.�û�:
                case DingYueRLx.ϵͳ:
                case DingYueRLx.Ӧ��:
                case DingYueRLx.����:
                case DingYueRLx.����:
                case DingYueRLx.ҽ����:
                    return SHOUJIANRID.Substring(2);
                default:
                    return SHOUJIANRID;
            }
        }
        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <returns></returns>
        public DingYueRLx GetDingYueRLx()
        {
            if (SHOUJIANRID == "QY")
            {
                return DingYueRLx.ȫԺ;
            }
            else if (SHOUJIANRID == "PU")
            {
                return DingYueRLx.��������ҽ��;
            }
            else if (SHOUJIANRID == "PG")
            {
                return DingYueRLx.���˵�ǰҽ����;
            }
            else if (SHOUJIANRID == "PW")
            {
                return DingYueRLx.���˵�ǰ����;
            }
            else if (SHOUJIANRID == "PD")
            {
                return DingYueRLx.���˵�ǰ����;
            }
            else if (SHOUJIANRID == "PY")
            {
                return DingYueRLx.���˵�ǰԺ��;
            }
            else if (SHOUJIANRID.StartsWith("U-"))
            {
                return DingYueRLx.�û�;
            }
            else if (SHOUJIANRID.StartsWith("S-"))
            {
                return DingYueRLx.ϵͳ;
            }
            else if (SHOUJIANRID.StartsWith("A-"))
            {
                return DingYueRLx.Ӧ��;
            }
            else if (SHOUJIANRID.StartsWith("W-"))
            {
                return DingYueRLx.����;
            }
            else if (SHOUJIANRID.StartsWith("D-"))
            {
                return DingYueRLx.����;
            }
            else if (SHOUJIANRID.StartsWith("G-"))
            {
                return DingYueRLx.ҽ����;
            }
            else if (SHOUJIANRID.StartsWith("Y-"))
            {
                return DingYueRLx.Ժ��;
            }
            else
            {
                return DingYueRLx.��������ҽ��;
            }
        }
	}

    
}
