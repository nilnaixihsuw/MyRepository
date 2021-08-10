﻿using System;

namespace Mediinfo.Utility
{
    /// <summary>  
    /// 验证身份证号码类  
    /// </summary>
    public class IDCardValidation
    {     
        /// <summary>
        /// 验证身份证号码的合理性 以及返回出生日期 性别
        /// </summary>
        /// <param name="idNumber">身份证号码</param>
        /// <param name="birth">出生日期</param>
        /// <param name="xingbie">性别</param>
        /// <returns></returns>
        public static bool CheckIDCard(string idNumber, ref string birth, ref string xingbie,ref string errMsg )
        {
            if (string.IsNullOrEmpty(idNumber))
            {
                errMsg = "身份证号为空";
                return false;
            }
            if (idNumber.Length == 18)
            {
                //大陆身份证号码校验
                bool check = CheckIDCard18(idNumber, ref birth, ref xingbie,ref errMsg);
                //返回为false,判断是否为港澳台居住证
                if (!check)
                {
                    //港澳台居住证校验-正则表达式
                    string gat_ZhengZe = @"^8[123]0000(?:19|20)\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])\d{3}[\dX]$";
                    check = System.Text.RegularExpressions.Regex.IsMatch(idNumber, gat_ZhengZe, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = CheckIDCard15(idNumber, ref birth, ref xingbie, ref errMsg);
                return check;
            }
            else
            {
                errMsg = "身份证号长度不对";
                return false;
            }
        }  
        
        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard18(string idNumber,ref string birth,ref string xingbie,ref string errMsg)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                errMsg = "数字验证错误";
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                errMsg = "省份验证错误";
                return false;//省份验证  
            }
            birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                errMsg = "出生日期验证错误";
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                errMsg = "校验码验证错误";
                return false;//校验码验证  
            }
            int x = -1;
            Math.DivRem(int.Parse(idNumber.Substring(16, 1)), 2, out x);
            if (  x== 0)
            {
                xingbie = "女";
            }
            else
            {
                xingbie = "男";
            }
            return true;//符合GB11643-1999标准  
        }
        
        /// <summary>  
        /// 15位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard15(string idNumber,ref string birth,ref string xingbie,ref string errMsg)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                errMsg = "数字验证错误";
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                errMsg = "省份验证错误";
                return false;//省份验证  
            }
            birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                errMsg = "出生日期验证错误";
                return false;//生日验证  
            }
            long x = -1;
            Math.DivRem(long.Parse(idNumber.Substring(14, 1)), 2, out x);
            if (x == 0)
            {
                xingbie = "女";
            }
            else
            {
                xingbie = "男";
            }            
            return true;
        }
    }
}
