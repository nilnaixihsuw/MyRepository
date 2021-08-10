using System;

namespace Mediinfo.WinForm.HIS.Core.RuleEntity
{
    /// <summary>
    /// 药品信息
    /// </summary>
    public class YaoPinXx
    {
        public string BINGRENID { get; set; }

        public string YAOPINMC { get; set; }

        public string JIXING { get; set; }

        public string PINCI { get; set; }

        public string GEIYAOFS { get; set; }

        public double? DANCIJL { get; set; }

        public string DANCIJLDW { get; set; }

        public DateTime? YONGYAOSJ { get; set; }

        public bool? LISHISJBZ { get; set; }
    }
}
