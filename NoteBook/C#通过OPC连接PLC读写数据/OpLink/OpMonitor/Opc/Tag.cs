using OpMonitor.FileRead;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OpMonitor
{
    /// <summary>
    /// Tag点数据项，提供通用数据项，以及扩展数据项Extra
    /// </summary>
    public class Tag :INotifyPropertyChanged
    {
        /// <summary>
        /// 值
        /// </summary>
        private string value;
        /// <summary>
        /// 附加值
        /// </summary>
        private object tagExtra;
        public Tag()
        {
            TagName = default(string);
            OpcTagName = default(string);
            GroupName = default(string);
            DataType = default(string);
            Qualities = default(string);
            TimeStamps = default(string);
            Message = default(string);
            Value = default(string);
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
        /// Tag值（二级使用）
        /// </summary>
        [Save]
        public string TagName { get; set; }
        /// <summary>
        /// OPC地址值
        /// </summary>
        [Save]
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
        public string TimeStamps { get; set; }
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
        public string Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
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
    }
}
