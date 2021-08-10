using System;
using System.Security.Cryptography;
using System.Text;

namespace Mediinfo.Utility
{
    /// <summary>
    /// SHA256加密
    /// </summary>
    public class SHA256
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strIN">加密字符</param>
        /// <returns></returns>
        public static string Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256Managed sha256 = new SHA256Managed();
            
            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            sha256.Clear();

            return Convert.ToBase64String(tmpByte);
        }

        /// <summary>
        /// 字节与字符串的转换
        /// </summary>
        /// <param name="Byte"></param>
        /// <returns></returns>
        private static string GetStringValue(byte[] Byte)
        {
            string tmpString = "";
            ASCIIEncoding Asc = new ASCIIEncoding();
            tmpString = Asc.GetString(Byte);
            return tmpString;
        }

        /// <summary>
        /// 字符串与字节的转换
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        private static byte[] GetKeyByteArray(string strKey)
        {
            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];
            tmpByte = Asc.GetBytes(strKey);

            return tmpByte;
        }
    }
}
