using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace OpcClient
{
    public interface IGroup : ITagsChangedListener,IDisposable
    {
        string GroupName { get; set; }
        //T GroupAs<T>();
        /// <summary>
        /// 设置刷新频率
        /// </summary>
        /// <param name="updateRate">刷新频率</param>
        /// <returns></returns>
        IGroup SetUpdateRate(int updateRate);
        /// <summary>
        /// 启用历史记录功能,会以queue方式保存历史记录
        /// 在AddItems、AddItem后使用
        /// </summary>
        IGroup AddQueue(int queueMaxNum);
       
        /// <summary>
        /// 关闭并清空历史记录
        /// </summary>
        IGroup RemoveQueue();
       
        /// <summary>
        /// 新增点
        /// </summary>
        /// <returns></returns>
        bool AddItem(Tag bi);
        /// <summary>
        /// 批量新增点
        /// </summary>
        /// <returns></returns>
        IGroup AddItems(List<Tag> biList);
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
        /// TAG多点取值
        /// </summary>
        /// <param name="tagName">Tag点名集合</param>
        /// <returns></returns>
        List<Tag> GetTags(List<string> tagNames);
        /// <summary>
        /// TAG点取值
        /// </summary>
        /// <param name="tagName">Tag点名</param>
        /// <returns></returns>
        Tag GetTagValue(string tagName);
        /// <summary>
        /// TAG点取值
        /// </summary>
        /// <param name="bi">Tag点</param>
        /// <returns></returns>
        Tag GetTagValue(Tag bi);
        /// <summary>
        /// TAG点取值(多个)
        /// </summary>
        /// <returns></returns>
        List<Tag> GetTagValues(List<Tag> biList);
        /// <summary>
        /// 根据tag名称查询tag点历史集合
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        List<Tag> GetTagHistory(string tagName);
        
    }
}
