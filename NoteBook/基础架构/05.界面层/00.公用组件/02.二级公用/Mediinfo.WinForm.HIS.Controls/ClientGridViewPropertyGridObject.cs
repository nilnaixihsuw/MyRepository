using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// GridView个性化窗体中的PropertyGridD对象
    /// </summary>
    public class ClientGridViewPropertyGridObject : ClientCustomObjectBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientGridViewPropertyGridObject(E_GY_DATALAYOUT1 dataLayout1, DTOState dTOState) : base(dTOState)
        {
            _dataLayout1 = dataLayout1;
            InitialPropertyDefault();
        }

        private E_GY_DATALAYOUT1 _dataLayout1 = null;

        private bool rowLine;

        /// <summary>
        /// 行号
        /// </summary>
        [CategoryAttribute("行属性"), CustomPropertyGridAttibute("行号", "是否显示行号", true)]
        [TypeConverter(typeof(BoolValueConverter))]
        public bool RowLine { get { return rowLine; } set { rowLine = value; OnPropertyChanged("RowLine"); } }

        private int? rowFont = 9;

        /// <summary>
        /// 行字体
        /// </summary>
        [CategoryAttribute("行属性"), CustomPropertyGridAttibute("行字体大小", "行字体", 9)]
        public int? RowFont { get { return rowFont; } set { rowFont = value; OnPropertyChanged("RowFont"); } }

        private string rowBackColor;

        /// <summary>
        ///行背景色
        /// </summary>
        [Browsable(true)]
        [CategoryAttribute("行属性"), CustomPropertyGridAttibute("行背景色", "根据表达式设置行背景色", "")]
        [Editor(typeof(ExpressionCustomControlEditor), typeof(UITypeEditor))]
        public string RowBackColor { get { return rowBackColor; } set { rowBackColor = value; OnPropertyChanged("RowBackColor"); } }

        private string rowBackColorDescription;

        /// <summary>
        /// 背景色描述
        /// </summary>     
        [CategoryAttribute("行属性"), CustomPropertyGridAttibute("行背景色描述", "根据表达式设置背景色描述", "")]
        public string RowBackColorDescription { get { return rowBackColorDescription; } set { rowBackColorDescription = value; OnPropertyChanged("RowBackColorDescription"); } }

        private string sortColumnSet;

        /// <summary>
        /// 排序列设置
        /// </summary>
        [Browsable(true)]
        [CategoryAttribute("其他属性"), CustomPropertyGridAttibute("排序列设置", "排序列设置", "")]
        public string SortColumnSet { get { return sortColumnSet; } set { sortColumnSet = value; OnPropertyChanged("SortColumnSet"); } }

        private bool isVisibleGroupPanel;

        /// <summary>
        /// 是否允许显示分组面板
        /// </summary>
        [CategoryAttribute("其他属性"), CustomPropertyGridAttibute("是否允许显示分组面板", "是否允许显示分组面板", true)]
        [BrowsableAttribute(false)]
        public bool IsVisibleGroupPanel { get { return isVisibleGroupPanel; } set { isVisibleGroupPanel = value; OnPropertyChanged("IsVisibleGroupPanel"); } }

        private bool isAllowSort;

        /// <summary>
        /// 是否允许排序
        /// </summary>
        [CategoryAttribute("其他属性"), CustomPropertyGridAttibute("是否允许排序", "是否允许排序", true)]
        [BrowsableAttribute(false)]
        public bool IsAllowSort { get { return isAllowSort; } set { isAllowSort = value; OnPropertyChanged("IsAllowSort"); } }

        private bool isAllowFilter;

        /// <summary>
        /// 是否允许过滤
        /// </summary>
        [CategoryAttribute("其他属性"), CustomPropertyGridAttibute("是否允许过滤", "是否允许过滤", true)]
        [BrowsableAttribute(false)]
        public bool IsAllowFilter { get { return isAllowFilter; } set { isAllowFilter = value; OnPropertyChanged("IsAllowFilter"); } }

        private bool isAllowVisibleMenu;

        /// <summary>
        /// 是否显示菜单
        /// </summary>
        [CategoryAttribute("其他属性"), CustomPropertyGridAttibute("是否显示菜单", "是否显示菜单", true)]
        [BrowsableAttribute(false)]
        public bool IsAllowVisibleMenu { get { return isAllowVisibleMenu; } set { isAllowVisibleMenu = value; OnPropertyChanged("IsAllowVisibleMenu"); } }

        /// <summary>
        /// 子类实现初始化对象默认值
        /// </summary>
        public new void InitialPropertyDefault()
        {
            if (_dataLayout1 == null)
                return;
            RowLine = _dataLayout1.LINENUMBER == 1 ? true : false;
            RowFont = _dataLayout1.ROWFONTSIZE;
            RowBackColor = _dataLayout1.ROWBACKCOLOREXPRESSION;
            RowBackColorDescription = _dataLayout1.ROWBACKCOLORDESCRIBE;
            SortColumnSet = _dataLayout1.ORDERBYFIELD;
            //IsVisibleGroupPanel = _dataLayout1.SHOWGROUPPANEL == 1 ? true : false;
            //IsAllowSort = _dataLayout1.ALLOWSORT == 1 ? true : false;
            //IsAllowFilter = _dataLayout1.ALLOWFILTER == 1 ? true : false;
            //IsAllowVisibleMenu = _dataLayout1.ENABLECOLUMNMENU == 1 ? true : false;
            PropertyChanged += ClientCustomObjectBase_PropertyChanged;
        }

        /// <summary>
        /// 属性改变触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientCustomObjectBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (GetState() != DTOState.New)
                if (!propertyNameList.Contains(e.PropertyName))
                    propertyNameList.Add(e.PropertyName);
        }

        /// <summary>
        /// DTO对象返回
        /// </summary>
        /// <returns></returns>
        public E_GY_DATALAYOUT1 ConvertToDataLayout1()
        {
            _dataLayout1.LINENUMBER = RowLine == true ? 1 : 0;
            _dataLayout1.ROWFONTSIZE = RowFont;
            _dataLayout1.ROWBACKCOLORDESCRIBE = RowBackColorDescription;
            _dataLayout1.ROWBACKCOLOREXPRESSION = RowBackColor;

            _dataLayout1.ORDERBYFIELD = SortColumnSet;

            _dataLayout1.SHOWGROUPPANEL = IsVisibleGroupPanel == true ? 1 : 0;
            _dataLayout1.ALLOWSORT = IsAllowSort == true ? 1 : 0;
            _dataLayout1.ALLOWFILTER = IsAllowFilter == true ? 1 : 0;
            _dataLayout1.ENABLECOLUMNMENU = IsAllowVisibleMenu == true ? 1 : 0;
            if (GetPropertyChangedCount() > 0 && GetState() == DTOState.UnChange)
            {
                _dataLayout1.SetState(DTOState.Update);
            }
            else if (GetState() == DTOState.New || IsNewStateUpdate)
            {
                _dataLayout1.SetState(DTOState.New);
            }
            else if (GetState() == DTOState.Delete && IsNewStateDelete)
            {
                return null;
            }
            else if (GetState() == DTOState.Delete && !IsNewStateDelete)
            {
                _dataLayout1.SetState(DTOState.Delete);
            }
            return _dataLayout1;
        }
    }

    public class BoolValueConverter : TypeConverter
    {
        private readonly bool[] values;

        public BoolValueConverter()
        {
            values = new bool[2] { true, false };
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(values);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if ((bool)value)
                    return "是";
                else
                    return "否";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string s)
            {
                return s == "是";
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}