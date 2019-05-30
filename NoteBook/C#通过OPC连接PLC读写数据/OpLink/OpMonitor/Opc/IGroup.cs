using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace OpMonitor
{
    public interface IGroup : ITagsChangedListener,IDisposable
    {
        string GroupName { get; set; }
        //T GroupAs<T>();

        /// <summary>
        /// 新增点
        /// </summary>
        /// <returns></returns>
        bool AddItem(Tag bi);
        /// <summary>
        /// 批量新增点
        /// </summary>
        /// <returns></returns>
        bool AddItems(List<Tag> biList);
        /// <summary>
        /// 删除点(全部)
        /// </summary>
        /// <returns></returns>
        void RemoveItemsAll();
        /// <summary>
        /// 删除点(多个)
        /// </summary>
        /// <returns></returns>
        void RemoveItems(List<Tag> biList);
        /// <summary>
        /// 删除点（单个）
        /// </summary>
        /// <returns></returns>
        void RemoveItem(Tag bi);
        /// <summary>
        /// TAG点取值
        /// </summary>
        /// <typeparam name="T">BaseItem</typeparam>
        /// <param name="bi">Tag点</param>
        /// <param name="tagType">Tag点类型（0：过程数据，1：信号）</param>
        /// <returns></returns>
        Tag GetTagValue(Tag bi);
        /// <summary>
        /// TAG点取值(多个)
        /// </summary>
        /// <returns></returns>
        List<Tag> GetTagValues(List<Tag> biList);
    }
}
