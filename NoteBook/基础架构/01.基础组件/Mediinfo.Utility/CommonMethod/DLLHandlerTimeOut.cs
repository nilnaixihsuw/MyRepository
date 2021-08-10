using System;
using Polly;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mediinfo.Utility.CommonMethod
{
    /// <summary>
    /// 
    /// </summary>
    public class DLLHandlerTimeOut
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">委托的方法</param>
        /// <param name="msgXml">委托的入参</param>
        /// <param name="timeSpan">超时时间</param>
        /// <param name="v">重试次数</param>
        /// <returns>返回委托的执行结果</returns>
        public static T Execute<T>(Func<T, T> func, T msgXml, TimeSpan timeSpan, int v)
        {
            TimeSpan[] timeSpans = new TimeSpan[v];
            for (int i = 0; i < v; i++)
            {
                timeSpans[i] = timeSpan;
            }

            var policy = Policy.Handle<Exception>().WaitAndRetry(
               timeSpans, (ex, time) =>
               {
                   throw new Exception("调用第三方DLL超时");
               });

            // 执行请求
            return policy.Execute(() => func(msgXml));
        }
    }
}
