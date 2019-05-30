using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpMonitor.FileRead
{
    /// <summary>
    /// 属性需要保存至配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class SaveAttribute : Attribute
    {
    }
}
