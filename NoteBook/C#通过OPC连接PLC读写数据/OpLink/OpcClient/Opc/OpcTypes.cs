using OpcClient.Opc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpcClient
{
    public class OpcTypes
    {
        /// <summary>
        /// DA OPC服务端标签格式定义
        /// </summary>
        public enum CanonicalDataTypesForDa : short
        {
            ////CanonDtByte = 17,
            ////CanonDtChar = 16,
            ////CanonDtWord = 18,
            ////CanonDtShort = 2,
            ////CanonDtDWord = 19,
            ////CanonDtLong = 3,
            ////CanonDtFloat = 4,
            ////CanonDtDouble = 5,
            ////CanonDtBool = 11,
            ////CanonDtString = 8,

            [TypeRemark(typeof(Byte))]
            CanonDtByte = 17,
            [TypeRemark(typeof(Char))]
            CanonDtChar = 16,
            [TypeRemark(typeof(UInt16))]
            CanonDtWord = 18,
            [TypeRemark(typeof(Int16))]
            CanonDtShort = 2,
            [TypeRemark(typeof(UInt32))]
            CanonDtDWord = 19,
            [TypeRemark(typeof(Int32))]
            CanonDtLong = 3,
            [TypeRemark(typeof(Single))]
            CanonDtFloat = 4,
            [TypeRemark(typeof(Double))]
            CanonDtDouble = 5,
            [TypeRemark(typeof(Boolean))]
            CanonDtBool = 11,
            [TypeRemark(typeof(String))]
            CanonDtString = 8,
        }

        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项值</param>        
        public static string GetEnumTypeRemarkByValue(short enumSubvalue)
        {
            var enumSubname = Enum.GetName(typeof(OpcTypes.CanonicalDataTypesForDa), enumSubvalue);
            return GetEnumTypeRemarkByName(enumSubname);
        }
        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子名称</param>        
        public static string GetEnumTypeRemarkByName(string enumSubname)
        {
            var enumSubitem = Enum.Parse(typeof(CanonicalDataTypesForDa), enumSubname, true);
            return GetEnumTypeRemark(enumSubitem);
        }

        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumTypeRemark(object enumSubitem)
        {
            enumSubitem = (Enum)enumSubitem;
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);

            if (fieldinfo != null)
            {

                Object[] objs = fieldinfo.GetCustomAttributes(typeof(TypeRemarkAttribute), false);

                if (objs == null || objs.Length == 0)
                {
                    return strValue;
                }
                else
                {
                    TypeRemarkAttribute da = (TypeRemarkAttribute)objs[0];
                    return da.FullName;
                }
            }
            else
            {
                return "不限";
            }
        }
    }
}
