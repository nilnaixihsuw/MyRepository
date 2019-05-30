using OpcClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OpcClient
{
    /// <summary>
    /// Tag点数据项，提供通用数据项，以及扩展数据项Extra
    /// </summary>
    public class Tag : ICloneable<Tag>, INotifyPropertyChanged
    {
        /// <summary>
        /// 值
        /// </summary>
        private object value;
        /// <summary>
        /// 附加值
        /// </summary>
        private object tagExtra;
        /// <summary>
        /// 是否启用历史记录功能
        /// </summary>
        private bool queueEnable = false;
        /// <summary>
        /// tag点历史集合
        /// </summary>
        private TagsQueue queue;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queueEnable">是否启用历史记录功能,会以queue方式保存历史记录</param>
        /// <param name="queueMaxNum">历史记录最大数量</param>
        public Tag()
        {
            TagName = default(string);
            OpcTagName = default(string);
            GroupName = default(string);
            DataType = default(string);
            Qualities = default(string);
            TimeStamps = default(DateTime);
            Message = default(string);
            Value = null;          
        }
        /// <summary>
        /// 启用历史记录功能,会以queue方式保存历史记录
        /// </summary>
        /// <param name="queueMaxNum">历史记录最大数量</param>
        public void addQueue(int queueMaxNum = 100)
        {
            if (this.queueEnable == false)
            {
                this.queueEnable = true;
                queue = new TagsQueue(queueMaxNum);
            }
        }
        /// <summary>
        /// 关闭并清空历史记录
        /// </summary>
        public void RemoveQueue()
        {
            if (this.queueEnable == true)
            {
                this.queueEnable = false;
                queue.Clear();
            }           
        }

        /// <summary>
        /// 获取附加数据是否为NULL
        /// </summary>
        public bool ExtraIsNull
        {
            get
            {
                return this.tagExtra == null;
            }
        }
        override
        public string ToString()
        {
            return TagName;
        }

        /// <summary>
        /// 附加数据，并提供转换功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ExtraAs<T>() where T : new()
        {
            if (this.ExtraIsNull == true)
            {
                tagExtra = new T();
            }
            return (T)this.tagExtra;
        }

        /// <summary>
        /// 附加数据，并提供转换功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns></returns>
        public T ExtraAs<T>(T defaultValue) where T : new()
        {
            if (this.ExtraIsNull == true)
            {
                tagExtra = defaultValue;
            }
            return (T)this.tagExtra;
        }
        /// <summary>
        /// 查询tag点历史集合
        /// </summary>
        public List<Tag> TagHistory { get { return queue.ToList(); } }
        /// <summary>
        /// Tag值（二级使用）
        /// </summary>
        [Description("Save")]
        public string TagName { get; set; }
        /// <summary>
        /// OPC地址值
        /// </summary>
        [Description("Save")]
        public string OpcTagName { get; set; }
        /// <summary>
        /// 本地组名
        /// </summary>
        public string GroupName { get; set; }
        //数据类型
        public string DataType { get; set; }
        /// <summary>
        /// 质量
        /// </summary>
        public string Qualities { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime TimeStamps { get; set; }
        /// <summary>
        /// Message备注
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 事件监听值的变化
        /// </summary>
        public object Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    //值变化时，执行历史记录
                    if (queueEnable) queue.Enqueue(this);
                    RaisePropertyChangedEvent("Value");
                }
            }
        }
        /// <summary>
        /// 值变化事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 值变化时触发
        /// </summary>
        /// <param name="name"></param>
        private void RaisePropertyChangedEvent(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// 克隆构造器
        /// </summary>
        /// <returns></returns>
        Tag ICloneable<Tag>.Clone()
        {
            return new Tag
            {
                //浅副本只提供变量值的副本，防止浅副本中历史功能死循环，历史功能停用
                queueEnable = false,
                TagName = this.TagName,
                OpcTagName = this.OpcTagName,
                GroupName = this.GroupName,
                DataType = this.DataType,
                Qualities = this.Qualities,
                TimeStamps = this.TimeStamps,
                Message = this.Message,
                Value = this.Value

            };
        }
    }
}
