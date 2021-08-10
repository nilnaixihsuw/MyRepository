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
                case DingYueRLx.用户:
                case DingYueRLx.系统:
                case DingYueRLx.应用:
                case DingYueRLx.病区:
                case DingYueRLx.科室:
                case DingYueRLx.医疗组:
                    return SHOUJIANRID.Substring(2);
                default:
                    return SHOUJIANRID;
            }
        }
        /// <summary>
        /// 获取订阅人类型
        /// </summary>
        /// <returns></returns>
        public DingYueRLx GetDingYueRLx()
        {
            if (SHOUJIANRID == "QY")
            {
                return DingYueRLx.全院;
            }
            else if (SHOUJIANRID == "PU")
            {
                return DingYueRLx.病人主治医生;
            }
            else if (SHOUJIANRID == "PG")
            {
                return DingYueRLx.病人当前医疗组;
            }
            else if (SHOUJIANRID == "PW")
            {
                return DingYueRLx.病人当前病区;
            }
            else if (SHOUJIANRID == "PD")
            {
                return DingYueRLx.病人当前科室;
            }
            else if (SHOUJIANRID == "PY")
            {
                return DingYueRLx.病人当前院区;
            }
            else if (SHOUJIANRID.StartsWith("U-"))
            {
                return DingYueRLx.用户;
            }
            else if (SHOUJIANRID.StartsWith("S-"))
            {
                return DingYueRLx.系统;
            }
            else if (SHOUJIANRID.StartsWith("A-"))
            {
                return DingYueRLx.应用;
            }
            else if (SHOUJIANRID.StartsWith("W-"))
            {
                return DingYueRLx.病区;
            }
            else if (SHOUJIANRID.StartsWith("D-"))
            {
                return DingYueRLx.科室;
            }
            else if (SHOUJIANRID.StartsWith("G-"))
            {
                return DingYueRLx.医疗组;
            }
            else if (SHOUJIANRID.StartsWith("Y-"))
            {
                return DingYueRLx.院区;
            }
            else
            {
                return DingYueRLx.病人主治医生;
            }
        }
	}

    
}
