using System;
using System.Diagnostics;

namespace Mediinfo.Utility.Extensions
{
    /// <summary>
    /// 性能调试时间的扩展方法
    /// </summary>
    public static class StopWatchExtensions
    {
        public static string ElapsedTimeString(this Stopwatch stopWatch)
        {
            return String.Format("{0} Hours:{1:00} Minutes:{2:00} Seconds.{3:00} Milliseconds",
                                stopWatch.Elapsed.Hours, 
                                stopWatch.Elapsed.Minutes, 
                                stopWatch.Elapsed.Seconds,
                                stopWatch.Elapsed.Milliseconds);
        }
    }
}
