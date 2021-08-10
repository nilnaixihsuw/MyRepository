using Mediinfo.DTO.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 有状态的实体DataLayout对象
    /// </summary>
    public abstract class ClientCustomObjectBase : PropertyGridBase, INotifyPropertyChanged
    {
        /// <summary>
        /// 属性改变触发该事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 构造函数初始化对象
        /// </summary>
        public ClientCustomObjectBase(DTOState dTOState)
        {
            _State = dTOState;
        }

        /// <summary>
        /// clientOject状态
        /// </summary>
        private DTOState _State;

        /// <summary>
        /// 获取clientOject状态
        /// </summary>
        /// <returns></returns>
        public DTOState GetState()
        {
            return _State;
        }

        /// <summary>
        /// 是否新增状态删除
        /// </summary>
        [BrowsableAttribute(false)]
        public bool IsNewStateDelete { get; set; } = false;

        /// <summary>
        /// 是否新增状态更新
        /// </summary>
        [BrowsableAttribute(false)]
        public bool IsNewStateUpdate { get; set; } = false;

        /// <summary>
        /// 获取属性改变的个数
        /// </summary>
        /// <returns></returns>
        public int GetPropertyChangedCount()
        {
            return propertyNameList.Count;
        }

        /// <summary>
        /// 属性改变记录集合
        /// </summary>
        protected List<string> propertyNameList = new List<string>();

        /// <summary>
        /// 设置DTO状态
        /// </summary>
        /// <param name="state"></param>
        public void SetState(DTOState state)
        {
            if (state == DTOState.UnChange)
                _State = DTOState.UnChange;
            if (_State == DTOState.UnChange)
                _State = state;
            if (_State == DTOState.New && state == DTOState.Delete) IsNewStateDelete = true;
        }

        /// <summary>
        /// 初始化对象默认值
        /// </summary>
        public virtual void InitialPropertyDefault() { }

        /// <summary>
        /// 属性值改变触发事件
        /// </summary>
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public delegate void PropertyChanged(object Value);

    /// <summary>
    /// 主要是实现中文化属性显示
    /// </summary>
    public class PropertyGridBase : ICustomTypeDescriptor
    {
        #region ICustomTypeDescriptor 显式接口定义

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return null;
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            ArrayList props = new ArrayList();
            Type thisType = this.GetType();
            PropertyInfo[] pis = thisType.GetProperties();
            foreach (PropertyInfo p in pis)
            {
                if (p.DeclaringType == thisType)
                {
                    attributes = Attribute.GetCustomAttributes(p);
                    // 判断属性是否显示
                    BrowsableAttribute Browsable = (BrowsableAttribute)Attribute.GetCustomAttribute(p, typeof(BrowsableAttribute));

                    if (Browsable != null)
                    {
                        if (Browsable.Browsable == true)
                        {
                            CustomPropertyGridDescriptor psd = new CustomPropertyGridDescriptor(p, attributes);
                            props.Add(psd);
                        }
                    }
                    else
                    {
                        CustomPropertyGridDescriptor psd = new CustomPropertyGridDescriptor(p, attributes);
                        props.Add(psd);
                    }
                }
            }
            PropertyDescriptor[] propArray = (PropertyDescriptor[])props.ToArray(typeof(PropertyDescriptor));
            return new PropertyDescriptorCollection(propArray);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }

    public class CustomPropertyGridDescriptor : PropertyDescriptor
    {
        private PropertyInfo info;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="attrs"></param>
        public CustomPropertyGridDescriptor(PropertyInfo propertyInfo, Attribute[] attrs)
        : base(propertyInfo.Name, attrs)
        {
            this.info = propertyInfo;
        }

        /// <summary>
        /// 组件类型
        /// </summary>
        public override Type ComponentType
        {
            get { return this.info.ReflectedType; }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public override bool IsReadOnly
        {
            get { return this.info.CanWrite == false; }
        }

        /// <summary>
        /// 属性类型
        /// </summary>
        public override Type PropertyType
        {
            get { return this.info.PropertyType; }
        }

        /// <summary>
        /// 是否重置默认值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool CanResetValue(object component)
        {
            return false;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override object GetValue(object component)
        {
            try
            {
                return this.info.GetValue(component, null);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 重设置
        /// </summary>
        /// <param name="component"></param>
        public override void ResetValue(object component)
        {
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public override void SetValue(object component, object value)
        {
            this.info.SetValue(component, value, null);
        }

        /// <summary>
        /// 是否序列化值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        /// <summary>
        /// 显示值设置
        /// </summary>
        public override string DisplayName
        {
            get
            {
                if (info != null)
                {
                    CustomPropertyGridAttibute uicontrolattibute = (CustomPropertyGridAttibute)Attribute.GetCustomAttribute(info, typeof(CustomPropertyGridAttibute));
                    if (uicontrolattibute != null)
                        return uicontrolattibute.PropertyName;
                    else
                    {
                        return info.Name;
                    }
                }
                else
                    return "";
            }
        }
    }

    /// <summary>
    /// 自定义类型
    /// </summary>
    public class CustomPropertyGridAttibute : Attribute
    {
        private string _PropertyName;
        private string _PropertyDescription;
        private object _DefaultValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="DefalutValue"></param>
        public CustomPropertyGridAttibute(string Name, string Description, object DefalutValue)
        {
            this._PropertyName = Name;
            this._PropertyDescription = Description;
            this._DefaultValue = DefalutValue;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        public CustomPropertyGridAttibute(string Name, string Description)
        {
            this._PropertyName = Name;
            this._PropertyDescription = Description;
            this._DefaultValue = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name"></param>
        public CustomPropertyGridAttibute(string Name)
        {
            this._PropertyName = Name;
            this._PropertyDescription = "";
            this._DefaultValue = "";
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName
        {
            get { return this._PropertyName; }
        }

        /// <summary>
        /// 属性描述
        /// </summary>
        public string PropertyDescription
        {
            get { return this._PropertyDescription; }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue
        {
            get { return this._DefaultValue; }
        }
    }
}