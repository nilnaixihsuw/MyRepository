using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcClient
{
    /// <summary>
    /// 组内Tags值变化通知
    /// </summary>
    public interface ITagsChangedListener
    {
        /// <summary>
        /// tag变化通知，使用时绑定action
        /// </summary>
        Action<Tag> ValueChangedHandle { get; set; }
    }
}
