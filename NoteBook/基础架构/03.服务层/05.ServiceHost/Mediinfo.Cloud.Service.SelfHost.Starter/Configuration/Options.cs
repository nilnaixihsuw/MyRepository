using CommandLine;

namespace Mediinfo.Cloud.Service.SelfHost.Starter.Configuration
{
    /// <summary>
    /// 可接受的命令行参数
    /// </summary>
    public class Options
    {
        [Option('d', "duankouhao", Required = false, HelpText = "端口号")]
        public int DuanKouHao { get; set; }

        [Option('b', "baoid", Required = false, HelpText = "代码ID")]
        public string BaoId { get; set; }

        [Option('t', "zhuangtai", Required = false, HelpText = "当前状态")]
        public string ZhuangTai { get; set; }

        [Option('j', "jiedianid", Required = false, HelpText = "节点ID")]
        public string JieDianId { get; set; }

        [Option('m', "jiedianmc", Required = false, HelpText = "节点名称")]
        public string JieDianMc { get; set; }

        [Option('i', "jiedianip", Required = false, HelpText = "节点IP")]
        public string JieDianIp { get; set; }
    }
}
