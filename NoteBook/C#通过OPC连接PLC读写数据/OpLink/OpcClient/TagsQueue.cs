using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpcClient;
using System.Collections;
using System.Diagnostics;

namespace OpcClient
{
    /// <summary>
    /// tag点历史集合，可通过指定时间获取
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DebugView))]
    public class TagsQueue : Queue<Tag>
    {
        private int maxNum = 1;        
        public TagsQueue(int maxNum = 1)
        {
            this.maxNum = maxNum;              
        }        
        /// <summary>
        /// 向队列加入元素
        /// </summary>
        /// <param name="item"></param>
        public new void Enqueue(Tag item)
        {
            //超出最大数量，取出最先的元素，并删除，充分体现队列的先进先出的特性
            if (this.Count >= maxNum)
            {
                base.Dequeue();              
            }
            var tag = ((ICloneable<Tag>)item).Clone();
            base.Enqueue(tag);
        }
        /// <summary>
        /// 调试视图
        /// </summary>
        private class DebugView
        {
            /// <summary>
            /// 查看的对象
            /// </summary>
            private List<string> view;

            /// <summary>
            /// 调试视图
            /// </summary>
            /// <param name="view">查看的对象</param>
            public DebugView(Queue queue)
            {
                this.view = queue.ToArray().Select(p => "value:"+((Tag)p).Value + " || time:" + ((Tag)p).TimeStamps).ToList();                
            }

            /// <summary>
            /// 查看的内容
            /// </summary>
            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public string[] Values
            {
                get
                {
                    var array = new string[view.Count];
                    view.CopyTo(array);
                    return array;
                }
            }
        }
    }
}
