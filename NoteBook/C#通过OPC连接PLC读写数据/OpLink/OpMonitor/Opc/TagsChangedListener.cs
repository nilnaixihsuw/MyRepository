using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpMonitor;

namespace OpMonitor
{
    /// <summary>
    /// 值发生变化时触发
    /// </summary>
    public class TagsChangedListener : ITagsChangedListener
    {
        /// <summary>
        /// tag值变化通知，使用时绑定action
        /// </summary>
        public Action<Tag> ValueChangedHandle { get; set; }

        /// <summary>
        /// 组下新增tag点时，都对事件进行绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TagsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (Tag obj in e.NewItems)
                    obj.PropertyChanged += obj_PropertyChanged;
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (Tag obj in e.NewItems)
                    obj.PropertyChanged -= obj_PropertyChanged;
            }
        }
        /// <summary>
        /// 对组下的tag值发生变化时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = (Tag)sender;
            if (ValueChangedHandle != null)
            {
                ValueChangedHandle(obj);
            }
        }
    }
}
