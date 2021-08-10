using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.Core.MessageQueue;

namespace Mediinfo.Infrastructure.JCJG
{
    public class HISMessage
    {
        /// <summary>
        /// 发送消息到队列
        /// </summary>
        /// <param name="receivers"></param>
        /// <param name="xiaoXiID"></param>
        /// <param name="xiaoXiBM"></param>
        /// <param name="jiuZhenID"></param>
        /// <param name="bingRenZyID"></param>
        /// <param name="xiaoXiBT"></param>
        /// <param name="xiaoXiZY"></param>
        /// <param name="xiaoXiNR"></param>
        /// <param name="tiXingLx"></param>
        /// <param name="youXiaoSj"></param>
        /// <param name="yiCiXBz"></param>
        /// <param name="baoMiXxBz"></param>
        /// <param name="youXianJi"></param>
        /// <param name="huiZhiBz"></param>
        /// <returns></returns>
        public static bool SendMessage(IEnumerable<string> receivers,
            string xiaoXiID,
            string xiaoXiBM, string xiaoXiMc, string xiaoXiJc, string bingRenID, int menZhenZyBz,
            string xiaoXiBT, string xiaoXiZY, object xiaoXiNR, DateTime faSongSj, string faSongRen,
            string tiXingLx = "", double? youXiaoSj = 0, int? yiCiXBz = 0,
            int? baoMiXxBz = 0, int? youXianJi = 2, int? huiZhiBz = 0, int fuJianBz = 0, string xiaoXiLY = "", string shouJianRen = "")
        {
            DateTime? yxSj = null;
            if (youXiaoSj != null)
            {
                yxSj = DateTime.Now.AddMinutes((double)youXiaoSj);
            }
            HISMessageBody hISMessageBody = new HISMessageBody()
            {
                Receivers = receivers.ToArray(),
                BaoMiXxBz = baoMiXxBz,
                BingRenID = bingRenID,
                HuiZhiBz = huiZhiBz,
                MenZhenZyBz = menZhenZyBz,
                TiXingLx = tiXingLx,
                XiaoXiBM = xiaoXiBM,
                XiaoXiBT = xiaoXiBT,
                XiaoXiID = xiaoXiID,
                XiaoXiNR = xiaoXiNR,
                XiaoXiZY = xiaoXiZY,
                YiCiXBZ = yiCiXBz,
                YouXianJi = youXianJi,
                YouXiaoSj = yxSj,
                FuJianBz = fuJianBz,
                XiaoXiMc = xiaoXiMc,
                XiaoXiJc = xiaoXiJc,
                FaSongSj = faSongSj,
                FaSongRen = faSongRen,
                XiaoXiLY = xiaoXiLY,
                ShouJianRen = shouJianRen
            };

            using (var client = MessageQueueClientFactory.CreateUserClient())
            {
                return client.PublishUserMsg(receivers, hISMessageBody);
            }
        }
    }

}
