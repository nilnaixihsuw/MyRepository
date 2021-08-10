using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 获取服务端时间
    /// </summary>
    public static class GYServerInfoHelper
    {
        /// <summary>
        /// 公用代理类
        /// </summary>
        public static JCJGGongYongService GongYongProxyService { get; set; }

        static GYServerInfoHelper()
        {
            GongYongProxyService = new JCJGGongYongService();
        }

        /// <summary>
        ///获取服务端时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerSysDate()
        {
            try
            {
                var dateTimeResult = GongYongProxyService.GetSysDate();
                if (dateTimeResult.ReturnCode == ReturnCode.SUCCESS)
                {
                    return dateTimeResult.Return;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }
    }
}
