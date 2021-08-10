using System;

namespace Mediinfo.Utility.CommonMethod
{
    /// <summary>
    /// 转换负数
    /// </summary>
    public class MyExamFormat : IFormatProvider, ICustomFormatter
    {
        public string NegativeToPositive(string num)
        {
            return Math.Abs(Convert.ToDouble(num)).ToString("f2");
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return NegativeToPositive(arg.ToString());
        }
    }
}
