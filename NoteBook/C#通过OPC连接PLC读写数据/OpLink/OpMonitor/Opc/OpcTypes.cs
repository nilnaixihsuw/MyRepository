using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpMonitor
{
    public class OpcTypes
    {
        /// <summary>
        /// DA OPC服务端标签格式定义
        /// </summary>
        public enum CanonicalDataTypesForDa : short
        {
            CanonDtByte = 17,
            CanonDtChar = 16,
            CanonDtWord = 18,
            CanonDtShort = 2,
            CanonDtDWord = 19,
            CanonDtLong = 3,
            CanonDtFloat = 4,
            CanonDtDouble = 5,
            CanonDtBool = 11,
            CanonDtString = 8,
        }
    }
}
