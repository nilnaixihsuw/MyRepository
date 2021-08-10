using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core.RuleEntity
{
    public class RuleResult
    {
        public string FANHUIMA { get; set; }

        public List<FanHuiXx> FanHuiXx { get; set; }
    }

    public class FanHuiXx
    {
        public string JUECEFS { get; set; }
        public string XIAOXINR { get; set; }
    }
}
