using System;

namespace Mediinfo.WinForm.HIS.Core.RuleEntity
{
    /// <summary>
    /// 诊断信息
    /// </summary>
    public class ZhenDuanXx
    {
        public string BINGRENID { get; set; }

        public string ZHENDUANLB { get; set; }

        public string ICD10 { get; set; }

        public string ZHENDUANMC { get; set; }

        public DateTime? ZHENDUANRQ { get; set; }

        public bool? ZHUZHENDBZ { get; set; }

        public bool? LISHISJBZ { get; set; }
    }
}
