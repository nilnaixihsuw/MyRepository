using Mediinfo.Utility.Util;
using Mediinfo.WinForm.HIS.Core.RuleEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 规则引擎
    /// </summary>
    public class RuleEngine
    {
        [DllImport(@"CDSSLibrary.dll", EntryPoint = "jueCe", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int jueCe(string serviceUrl, string bingRenXx, ref string result);
        public static BingRenXx bingRenXx = new BingRenXx();
        /// <summary>
        /// 把事实应用到规则
        /// </summary>
        /// <param name="yaoPinXxList"></param>
        /// <param name="jianYanXxList"></param>
        /// <param name="jianChaXxList"></param>
        /// <param name="zhenDuanXxList"></param>
        /// <param name="guoMinShiList"></param>
        /// <returns></returns>
        public static int Act(List<YaoPinXx> yaoPinXxList, List<JianYanXx> jianYanXxList,
            List<JianChaXx> jianChaXxList, List<ZhenDuanXx> zhenDuanXxList, List<GuoMinShi> guoMinShiList)
        {
            List<YaoPinXx> yaoPinXx = new List<YaoPinXx>();
            yaoPinXx.AddRange(yaoPinXxList);

            List<JianChaXx> jianChaXx = new List<JianChaXx>();
            if (jianChaXxList != null)
            {
                jianChaXx.AddRange(jianChaXxList);
            }


            List<JianYanXx> jianYanXx = new List<JianYanXx>();
            if (jianYanXxList != null)
            {
                jianYanXx.AddRange(jianYanXxList);
            }


            List<GuoMinShi> guoMinShi = new List<GuoMinShi>();
            if (guoMinShiList != null)
            {
                guoMinShi.AddRange(guoMinShiList);
            }


            List<ZhenDuanXx> zhenDuanXx = new List<ZhenDuanXx>();
            if (zhenDuanXxList != null)
            {
                zhenDuanXx.AddRange(zhenDuanXxList);
            }


            BingRenXx bingRenXx = new BingRenXx()
            {
                BINGRENID = "02000232323",
                CHUSHENGRQ = DateTime.Now.AddYears(-10),
                JIUZHENSJ = DateTime.Now,
                NIANLING = 10,
                XINGBIEMC = "男",
                XINGMING = "张三",
                YAOPINXX = yaoPinXx,
                GUOMINSHI = guoMinShi,
                JIANCHAXX = jianChaXx,
                JIANYANXX = jianYanXx,
                ZHENDUANXX = zhenDuanXx
            };

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Accept", "*,*/*");
                client.Headers.Add("Accept-Language", "zh-cn");
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Accept-Encoding", "gzip, deflate");
                try
                {
                    // 请求规则引擎
                    var rbyte = client.UploadData("http://localhost:8080/medicdss-ui/cdr",
                        System.Text.Encoding.UTF8.GetBytes(Convert.ToBase64String(
                            System.Text.Encoding.UTF8.GetBytes(JsonUtil.SerializeObject(bingRenXx, new JsonSerializerSettings()
                            {
                                DateFormatString = "yyyy/MM/dd HH:mm:ss"
                            })))));

                    var rstr = System.Text.Encoding.UTF8.GetString(rbyte);

                    var jresult = JsonUtil.DeserializeToObject<RuleResult>(rstr);

                    // 入参不合法
                    if (jresult.FANHUIMA == "2000")
                    {
                        return -2;
                    }

                    // 判断是否出现错误
                    if (jresult.FANHUIMA != "0000")
                    {
                        return -1;
                    }

                    // 判断是否有决策结果
                    if (!jresult.FanHuiXx.Any())
                    {
                        return 1;
                    }

                    // 展示用户操作界面
                    using (ShiShiDialog dialog = new ShiShiDialog(bingRenXx, jresult))
                    {
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }
    }
}
