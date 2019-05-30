using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcClient
{
    /// <summary>
    /// 提供IEnumerable的扩展
    /// </summary>
    public static class IEnumerableExtend
    {
        public static List<Tag> ByDateLargerThan(this IEnumerable<Tag> list, DateTime date)
        {
            if (list == null) return null;
            return list.Where(p => p.TimeStamps >= date).ToList();
        }
        /// <summary>
        /// 取指定时间周期内的tag集合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static List<Tag> ByDateBetweenThan(this IEnumerable<Tag> list, DateTime dateFrom, DateTime dateTo = default(DateTime))
        {
            if (list == null) return null;
            dateTo = dateTo != default(DateTime) ? dateTo : DateTime.Now;
            return    list.Where(p => p.TimeStamps >= dateFrom && p.TimeStamps <= dateTo).ToList();
        }
        public static object Average(this IEnumerable<Tag> list)
        {
            if (list == null) return null;
            return list.Average(p => Convert.ToInt32(p.Value));
        }
        public static object Sum(this IEnumerable<Tag> list)
        {
            if (list == null) return null;
            return list.Sum(p => Convert.ToInt32(p.Value));
        }
        public static object Min(this IEnumerable<Tag> list)
        {
            if (list == null) return null;
            return list.Min(p => Convert.ToInt32(p.Value));
        }
        public static object Max(this IEnumerable<Tag> list)
        {
            if (list == null) return null;
            return list.Max(p => Convert.ToInt32(p.Value));
        }
    }
}
