using System;
using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core.RuleEntity
{
    /// <summary>
    /// 病人信息
    /// </summary>
    public class BingRenXx
    {
        public string BINGRENID { get; set; }

        public DateTime? JIUZHENSJ { get; set; }

        public string XINGMING { get; set; }

        public string XINGBIEMC { get; set; }

        public DateTime CHUSHENGRQ { get; set; }

        public double? NIANLING { get; set; }

        public List<JianYanXx> JIANYANXX { get; set; }

        public List<JianChaXx> JIANCHAXX { get; set; }

        public List<ZhenDuanXx> ZHENDUANXX { get; set; }

        public List<YaoPinXx> YAOPINXX { get; set; }

        public List<GuoMinShi> GUOMINSHI { get; set; }
    }
}
