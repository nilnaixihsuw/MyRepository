using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcClient.Opc
{     
    /// <summary>
    /// 对应类型备注特性
    /// </summary>
    public class TypeRemarkAttribute : Attribute
    {
        /// <summary>
        /// 备注
        /// </summary>
        public Type Remark { get; set; }
        /// <summary>
        /// 类型全名
        /// </summary>
        public string FullName { get { return Remark.FullName; } }

        public TypeRemarkAttribute(Type remark)
        {
            this.Remark = remark;
        }
    }
}
