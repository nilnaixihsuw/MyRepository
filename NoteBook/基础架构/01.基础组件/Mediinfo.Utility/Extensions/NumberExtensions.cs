using System;
using System.Text;

namespace Mediinfo.Utility.Extensions
{
    /// <summary>
    /// 数字的扩展方法
    /// </summary>
    public static class NumberExtensions
    {
        #region 四舍五入处理
        /*
         * decimal a =1.275M;
         * Console.WriteLine("{0} {1} {2}",a,Math.Round(a,2),Math.Round(a,2,MidpointRounding.AwayFromZero));
         * var b =1.275;
         * Console.WriteLine("{0} {1} {2}",b,Math.Round(b,2),Math.Round(b,2,MidpointRounding.AwayFromZero));

         * 1.275 1.28 1.28
         * 1.275 1.27 1.27
         */
        #endregion

        /// <summary>
        /// （扩展方法）四舍五入，默认两位
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals">四舍五入的位数，默认两位</param>
        /// <returns>四舍五入后的数值</returns>
        public static decimal Round(this decimal d, int decimals = 2)
        {
            return Math.Round(d, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// （扩展方法）四舍五入，默认两位
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals">四舍五入的位数，默认两位</param>
        /// <returns>四舍五入后的数值</returns>
        public static double Round(this double d, int decimals = 2)
        {
            return (double)Math.Round((decimal)d, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 空值转化为decimal
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal NullToZero(this decimal? d, params int[] decimals)
        {
            if (d == null)
            {
                if (decimals.Length > 0)
                {
                    string decimalFormat = "f" + decimals[0];
                    return Convert.ToDecimal(Math.Round(0m).ToString(decimalFormat));
                }
                else
                    return Math.Round(0m);
            }
            else
            {
                if (decimals.Length > 0)
                {
                    string decimalFormat = "f" + decimals[0];
                    return Convert.ToDecimal(Math.Round((decimal)d).ToString(decimalFormat));
                }
                else
                    return (decimal)d;
            }

        }

        /// <summary>
        /// （扩展方法）将数值转换为大写的金额
        /// </summary>
        /// <param name="d"></param>
        /// <param name="padLast">在金额最后是否拼接“整”字</param>
        /// <returns>大写金额的字符串</returns>
        public static string ToChineseMoney(this double d, bool padLast = true)
        {
            return ToChineseMoney(Convert.ToDecimal(d), padLast);
        }

        /// <summary>
        /// （扩展方法）将数值转换为大写的金额
        /// </summary>
        /// <param name="d"></param>
        /// <param name="padLast">在金额最后是否拼接“整”字</param>
        /// <returns>大写金额的字符串</returns>
        public static string ToChineseMoney(this decimal d, bool padLast = true)
        {
            ChineseMoneyUtil moneyHelper = new ChineseMoneyUtil(Convert.ToDecimal(d));
            if (moneyHelper.OverFlow)
            {
                throw new OverflowException("超出最大的金额范围");
            }

            return moneyHelper.ToString();
        }

        /// <summary>
        /// （扩展方法）将数值转换为中文一/二/三
        /// add by songxl on 2019-05-07
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ToChineseNumber(this int i)
        {
            switch (i)
            {
                case 0:
                    return "零";
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "六";
                case 7:
                    return "七";
                case 8:
                    return "八";
                case 9:
                    return "九";
                default:
                    return "";
            }
        }

        /// <summary>
        /// int类型转化为枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int i)
        {
            return (T)Enum.Parse(typeof(T), i.ToString());
        }

        /// <summary>
        /// Int?型转为Int型
        /// </summary>
        /// <param name="i">可空Int型</param>
        /// <param name="defaultValue">为空时，返回的默认值</param>
        /// <returns></returns>
        public static int ToInt(this int? i, int defaultValue)
        {
            if (i.HasValue)
                return i.Value;
            else
                return defaultValue;
        }

        /// <summary>
        /// long?型转为long型
        /// </summary>
        /// <param name="i">可空long型</param>
        /// <param name="defaultValue">为空时，返回的默认值</param>
        /// <returns></returns>
        public static long ToLong(this long? i, int defaultValue = 0)
        {
            return i ?? defaultValue;
        }

        /// <summary>
        /// decimal? 转为 decimal型
        /// </summary>
        /// <param name="d">可为空decimal型</param>
        /// <param name="defaultValue">为空时，返回的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this decimal? d, decimal defaultValue = 0)
        {
            return d ?? defaultValue;
        }

        /// <summary>
        /// 转换为Bool，为空则返回默认值，非零则返回true
        /// </summary>
        /// <param name="i"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this int? i, bool defaultValue)
        {
            if (!i.HasValue)
                return defaultValue;

            return i.Value != 0;
        }

        /// <summary>
        /// 转换为Bool，非零则返回true
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ToBool(this int i)
        {
            return i != 0;
        }

        /// <summary>
        /// 将数字转化成中文
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string NumberToChinese(this int number)
        {
            string numStr = "0123456789";
            string chineseStr = "零一二三四五六七八九";
            char[] c = number.ToString().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = numStr.IndexOf(c[i]);
                if (index != -1) c[i] = chineseStr[index];
            }
            return new string(c);
        }
    }

    #region 小写金额转换为大写金额帮助类

    internal class ChineseMoneyUtil
    {
        protected string Yuan = "圆";   // “元”，可以改为“圆”、“卢布”之类
        protected string Jiao = "角";   // “角”，可以改为“拾”
        protected string Fen = "分";    // “分”，可以改为“美分”之类
        static string Digit = "零壹贰叁肆伍陆柒捌玖"; // 大写数字

        private bool isAllZero = true; // 片段内是否全零
        private bool isPreZero = true; // 低一位数字是否是零
        private readonly bool Overflow = false; // 溢出标志
        private readonly long money100;         // 金额*100，即以“分”为单位的金额
        private long value;            // money100的绝对值

        private readonly StringBuilder sb = new StringBuilder(); // 大写金额字符串，逆序

        // 只读属性: "零元"
        public string ZeroString => Digit[0] + Yuan;

        public bool OverFlow => Overflow;

        // 构造函数
        public ChineseMoneyUtil(decimal money)
        {
            try
            {
                money100 = (long)(money * 100m);
            }
            catch
            {
                Overflow = true;
            }
            if (money100 == long.MinValue) Overflow = true;
        }

        // 重载 ToString() 方法，返回大写金额字符串
        public override string ToString()
        {
            if (Overflow) return "金额超出范围";
            if (money100 == 0) return ZeroString;

            string[] Unit = {
                Yuan,
                "万",
                "亿",
                "万",
                "亿亿"
            };

            value = System.Math.Abs(money100);
            ParseSection(true);
            for (int i = 0; i < Unit.Length && value > 0; i++)
            {
                if (isPreZero && !isAllZero) sb.Append(Digit[0]);
                if (i == 4 && sb.ToString().EndsWith(Unit[2])) sb.Remove(sb.Length - Unit[2].Length, Unit[2].Length);
                sb.Append(Unit[i]);
                ParseSection(false);
                if ((i % 2) == 1 && isAllZero) sb.Remove(sb.Length - Unit[i].Length, Unit[i].Length);
            }
            if (money100 < 0) sb.Append("负");
            return Reverse();
        }

        // 解析“片段”: “角分(2位)”或“万以内的一段(4位)”
        void ParseSection(bool isJiaoFen)
        {
            string[] Unit = isJiaoFen ? new string[] {
                Fen,
                Jiao
            } : new string[] {
                "",
                "拾",
                "佰",
                "仟"
            };

            isAllZero = true;
            for (int i = 0; i < Unit.Length && value > 0; i++)
            {
                int d = (int)(value % 10);
                if (d != 0)
                {
                    if (isPreZero && !isAllZero) sb.Append(Digit[0]);
                    sb.AppendFormat("{0}{1}", Unit[i], Digit[d]);
                    isAllZero = false;
                }
                isPreZero = (d == 0);
                value /= 10;
            }
        }

        // 反转字符串
        string Reverse()
        {
            StringBuilder sbReversed = new StringBuilder();
            for (int i = sb.Length - 1; i >= 0; i--) sbReversed.Append(sb[i]);
            return sbReversed.ToString();
        }
    }

    #endregion
}