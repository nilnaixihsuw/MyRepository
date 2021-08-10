using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Mediinfo.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediinfo.Utility.Extensions;
using System.Windows.Forms.Design;
using System.Resources;
using DevExpress.XtraEditors.Design;
using System.ComponentModel.Design;
using DevExpress.Utils.Design;
using DevExpress.XtraReports.Native;
using DevExpress.Data.ExpressionEditor;
using System.Collections;
using DevExpress.Utils.Drawing;
using DevExpress.Skins;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Views.Base;
using System.Diagnostics;
using Mediinfo.Enterprise;
using DevExpress.XtraGrid;
using Mediinfo.HIS.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.Core;
using Mediinfo.ServiceProxy.HIS.GongYong;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 自定义数据展示
    /// </summary>
    public partial class FrmDataLayoutSet : MediDialog
    {
        #region 属性

        /// <summary>
        /// 窗体名称
        /// </summary>
        private string _FormName;

        /// <summary>
        /// 控件名称
        /// </summary>
        private string _ControlName;

        /// <summary>
        /// 命名空间
        /// </summary>
        private string _NameSpace;

        /// <summary>
        /// GridView控件
        /// </summary>
        private GridView _GridView = null;

        /// <summary>
        /// 数据布局
        /// </summary>
        private MediDataLayoutControl _MediDataLayoutControl = null;

        /// <summary>
        /// 列设置
        /// </summary>
        private Dictionary<string, ColumnsAttribute> _DictionaryLieSheZ = new Dictionary<string, ColumnsAttribute>();

        /// <summary>
        /// 项设置
        /// </summary>
        private Dictionary<string, ItemLayoutAttribute> _DictionaryItem = new Dictionary<string, ItemLayoutAttribute>();

        /// <summary>
        /// 存储布局信息
        /// </summary>
        private E_GY_DATALAYOUT1 _EDataLayout1 = null;

        /// <summary>
        /// 存储布局信息详情
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;       

        /// <summary>
        /// 创建服务实例
        /// </summary>
        private GYDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// 数据源
        /// </summary>
        private GridColumnInfo gridColumnInfo = null;

        #endregion  

        #region 构造函数
        public FrmDataLayoutSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 根据DataLayOutControl获取字段
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sControlName"></param>
        /// <param name="MediDataLayoutControl"></param>
        public FrmDataLayoutSet(string sFormName, string sControlName,string sNameSpace, MediDataLayoutControl control)
        {
            InitializeComponent();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            _FormName = sFormName;
            _ControlName = sControlName;
            _MediDataLayoutControl = control;
            _NameSpace = sNameSpace;

            this.labelName.Text = string.Format("{0}.{1}", _FormName, _ControlName);

            mediTabControl1.TabPages[0].Text = "项属性";
            mediTabControl1.TabPages[1].PageVisible = false;
            mediTabControl1.TabPages[2].PageVisible = false;

            this.mediGridControlSource.DataSource = _MediDataLayoutControl.DataSource;

            _GYDataLayoutService = new GYDataLayoutService();

            //stopwatch.Stop();
            //Console.WriteLine("ElapsedMilliseconds ---1  " + stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// 根据GridView获取字段
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sControlName"></param>
        /// <param name="gridView"></param>
        public FrmDataLayoutSet(string sFormName, string sControlName, string sNameSpace, GridView gridView)
        {
            InitializeComponent();

            _FormName = sFormName;
            _ControlName = sControlName;
            _GridView = gridView;
            _NameSpace = sNameSpace;

            this.mediGridControlSource.DataSource = gridView.DataSource;
                            
            this.labelName.Text = string.Format("{0}.{1}", _FormName, _ControlName);
            mediTabControl1.TabPages[0].Text = "列属性";
            mediTabControl1.TabPages[1].PageVisible = true;
            mediTabControl1.TabPages[2].PageVisible = true;

            _GYDataLayoutService = new  GYDataLayoutService();
        }

        #endregion
        
        #region 数据源处理

        #region 公共方法
        
        /// <summary>
        /// 获取数据布局信息从数据库
        /// </summary>
        private void GetDataLayoutForDB()
        {
            var ret = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                _EDataLayout1 = ret.Return.DataLayout1;
                _EDataLayout2 =  ret.Return.DataLayout2;

                if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                {
                    this.radioGroupLevel.EditValue = "2";
                }
                else if (_EDataLayout1.YINGYONGID.Length == 2)
                {
                    this.radioGroupLevel.EditValue = "1";
                }
                else
                {
                    this.radioGroupLevel.EditValue = "0";
                }
            }
        }

        //private IDataColumnInfoProvider _DataColumnInfoProvider = null;
        private GridColumn GetDataColumnInfo(string sFieldName)
        {
            if (_GridView != null)
            {
                GridColumnCollection colColl = _GridView.Columns;

                foreach (GridColumn col in colColl)
                {
                    //GridColumn gridColumn = col;
                    //gridColumn.Caption = gridColumn.FieldName;
                    if (sFieldName.ToUpper() == col.FieldName.ToUpper())
                        return col;// as IDataColumnInfoProvider;
                }
            }
            return null;
        }
        #endregion

        #region GridView 数据源处理

        /// <summary>
        /// 设置GridView属性
        /// </summary>
        private void GridViewAttributeSet()
        {
            GridViewAttribute gridViewAttribute = GetGridViewDataSource();
            if (gridViewAttribute == null)
                return;

            InitDictionary(gridViewAttribute.GridViewColumnAttribute);

            propertyGridHangShuX.SelectedObject = gridViewAttribute.GridViewRowAttribute;

            propertyGridQiTaSX.SelectedObject = gridViewAttribute.GridViewOtherAttribute;

            BindFieldList();
        }

        /// <summary>
        /// 初始化字典
        /// </summary>
        /// <param name="details"></param>
        private void InitDictionary(List<ColumnsAttribute> columnsAttributelst)
        {
            if (columnsAttributelst == null || columnsAttributelst.Count == 0) return;

            //把数据源放入缓存字典中
            foreach (ColumnsAttribute col in columnsAttributelst)
            {
                string sFieldName = col.列头设置.字段名;

                if (_DictionaryLieSheZ.ContainsKey(sFieldName)) continue;

                _DictionaryLieSheZ.Add(sFieldName, col);
            }
        }

        /// <summary>
        /// 获取GridView数据源
        /// </summary>
        private GridViewAttribute GetGridViewDataSource()
        {
            GridViewAttribute gridViewAttribute = new GridViewAttribute();

            //从数据库中查询值
            gridViewAttribute = LoadDBGridViewAttribute();

            //数据库加载没有 则加载默认数据
            if (gridViewAttribute == null)
            {
                //若是查询不到 则从列表中加载默认值
                gridViewAttribute = LoadDefaultGridViewAttribute();
            }

            return gridViewAttribute;
        }

        /// <summary>
        /// 加载默认GridView数据
        /// </summary>
        /// <returns></returns>
        private GridViewAttribute LoadDefaultGridViewAttribute()
        {
            //GridView属性
            GridViewAttribute gridViewAttribute = new GridViewAttribute();

            #region 列属性
            List<ColumnsAttribute> gridViewColumnAttribute = new List<ColumnsAttribute>();          

            float fontSize = 9;

            if (_GridView != null)
            {
                GridColumnCollection colColl = _GridView.Columns;//.OrderBy(o=>o.VisibleIndex) as GridColumnCollection;
                List<GridColumn> listGridColumn = colColl.OrderBy(o => o.VisibleIndex).ToList();

                int i = 1;
                //FieldItems fields = new FieldItems();
                //fields._SourceObject = new GridColumnInfo(colColl);// _GridView.Columns); 
                foreach (GridColumn col in listGridColumn)
                {                   
                    if (col.FieldName == "") continue;

                    #region 初始化缓存
                    ColumnsAttribute columnAttribute = new ColumnsAttribute();
                    columnAttribute.SortNo = i;
                    columnAttribute.DatalayoutMXID = "";
                    columnAttribute.DatalayoutID = "";

                    HeaderAttribute headerAttribute = new HeaderAttribute();
                    headerAttribute.中文名称 = col.Caption.IsNullOrEmpty() ? col.FieldName : col.Caption;
                    headerAttribute.列头固定 = FixedStyle.None;
                    headerAttribute.列宽度 = col.Width;
                    headerAttribute.头字体大小 = col.AppearanceHeader.Font.Size;
                    headerAttribute.头标题对齐方式 = col.AppearanceHeader.TextOptions.HAlignment;
                    headerAttribute.字段名 = col.FieldName;
                    headerAttribute.显示标志 = col.Visible;

                    fontSize= col.AppearanceHeader.Font.Size;
                    columnAttribute.列头设置 = headerAttribute;

                    CellAttribute cellAttribute = new CellAttribute();
                    cellAttribute.ColumnInfo = this.gridColumnInfo;
                    cellAttribute._MediGridView = this.mediGridView1;
                    cellAttribute.保护标志 = false;
                    cellAttribute.初始值 = "";
                    cellAttribute.单元格字体大小 = col.AppearanceCell.Font.Size;
                    cellAttribute.单元格文本对齐方式 = col.AppearanceCell.HAlignment;
                    cellAttribute.跳转顺序 = i;
                    cellAttribute.输入法模式 = ImeMode.NoControl;
                    //cellAttribute.字体颜色表达式 = "";
                    //cellAttribute.字体颜色说明 = "";
                    cellAttribute.显示格式 = "";
                    cellAttribute.有效性检查 = "";
                    cellAttribute.有效性说明 = "";
                    cellAttribute.显示格式类型 = DevExpress.Utils.FormatType.None;
                    //cellAttribute.背景颜色表达式 = "";
                    //cellAttribute.背景颜色说明 = "";
                    //cellAttribute.非空表达式 = "";
                    //cellAttribute.非空表达式说明 = "";        
                              
                    //cellAttribute._objectWrapper = fields;

                    columnAttribute.单元格设置 = cellAttribute;
                    gridViewColumnAttribute.Add(columnAttribute);
                    #endregion
                    i++;
                }

                gridViewAttribute.GridViewColumnAttribute = gridViewColumnAttribute;
            }
            #endregion

            #region 行属性
            GridViewRowAttribute gridViewRowAttribute = new GridViewRowAttribute();
            gridViewRowAttribute.行号 = true;
            gridViewRowAttribute.行字体大小 = fontSize;
            gridViewRowAttribute.行背景色 = "";
            gridViewRowAttribute.行背景色描述 = "";

            //FieldItems field = new FieldItems();
            //field._SourceObject =new GridColumnInfo(_GridView.Columns);
            //gridViewRowAttribute._objectWrapper = field;

            gridViewAttribute.GridViewRowAttribute = gridViewRowAttribute;
            #endregion

            #region 其他属性
            GridViewOtherAttribute gridViewOtherAttribute = new GridViewOtherAttribute();
            gridViewOtherAttribute.是否允许排序 = true;
            gridViewOtherAttribute.是否允许过滤 = true;
            gridViewOtherAttribute.是否显示分组面板 = false;
            gridViewOtherAttribute.是显示列菜单 = true;
            gridViewOtherAttribute.特定排序列设置 = "";
            gridViewAttribute.GridViewOtherAttribute = gridViewOtherAttribute;
            #endregion

            return gridViewAttribute;
        }

        /// <summary>
        /// 加载数据库的GridView属性
        /// </summary>
        /// <returns></returns>
        private GridViewAttribute LoadDBGridViewAttribute()
        {
            
            if (_EDataLayout1 == null) return null;

            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) return null;

            GridViewAttribute gridViewAttribute = new GridViewAttribute();
            gridViewAttribute.NameSpace = _EDataLayout1.NAMESPACE;
            gridViewAttribute.YingYongID = _EDataLayout1.YINGYONGID;
            gridViewAttribute.ControlName = _EDataLayout1.CONTROLNAME;
            gridViewAttribute.DatalayoutID = _EDataLayout1.DATALAYOUTID;
            gridViewAttribute.FormName = _EDataLayout1.FORMNAME;            

            List<ColumnsAttribute> gridViewColumnAttributge = new List<ColumnsAttribute>();

            int index = 1;
            //遍历数据集
            _EDataLayout2.OrderBy(b=>b.VISIBLE).OrderBy(o=>o.SORTNO).ToList().ForEach(o=> {
                #region 遍历
                ColumnsAttribute columsAttribute = new ColumnsAttribute();
                columsAttribute.DatalayoutID = o.DATALAYOUTID;
                columsAttribute.DatalayoutMXID = o.DATALAYOUTMXID;
                columsAttribute.SortNo = o.SORTNO.ToInt(index);

                HeaderAttribute headerAttribute = new HeaderAttribute();
                headerAttribute.中文名称 = o.CAPTION;
                headerAttribute.列头固定 = o.FIXED.ToInt(0).ToEnum<FixedStyle>();
                headerAttribute.列宽度 = o.WIDTH.ToInt(100);
                headerAttribute.头字体大小 = o.HEADERFONTSIZE.ToInt(10);
                headerAttribute.头标题对齐方式 = o.HEADERHALIGNMENT.ToInt(0).ToEnum<HorzAlignment>();
                headerAttribute.字段名 = o.FIELDNAME;
                headerAttribute.显示标志 = o.VISIBLE.ToInt(1) == 0 ? false : true;                

                CellAttribute cellAttribute = new CellAttribute();
                cellAttribute.ColumnInfo = this.gridColumnInfo;
                cellAttribute._MediGridView = this.mediGridView1;
                cellAttribute.保护标志 = o.READONLY.ToInt(0) == 0 ? false : true;
                cellAttribute.初始值 = o.DEFAULTVALUE;
                cellAttribute.单元格字体大小 = o.CELLFONTSIZE.ToInt(9);
                cellAttribute.单元格文本对齐方式 = o.CELLHALIGNMENT.ToInt(0).ToEnum<HorzAlignment>();
                //cellAttribute.字体颜色表达式 = o.CELLFORECOLOREXPRISSION;
                //cellAttribute.字体颜色说明 = o.CELLFORECOLORDESCRIBE;
                cellAttribute.显示格式 = o.FORMATSTRING;
                cellAttribute.显示格式类型 = o.FORMATTYPE.ToInt(0).ToEnum<FormatType>();
                cellAttribute.有效性检查 = o.VALIDATEEXPRISSION;
                cellAttribute.有效性说明 = o.VALIDATEDESCRIBE;
                //cellAttribute.背景颜色表达式 = o.BACKCOLOREXPRISSION;
                //cellAttribute.背景颜色说明 = o.BACKCOLORDESCRIBE;
                cellAttribute.跳转顺序 = o.TABINDEX.ToInt(index);
                cellAttribute.输入法模式 = o.IMEMODE.ToEnum<ImeMode>();
                //cellAttribute.非空表达式 = o.NONEMPTYEXPRESSION;
                //cellAttribute.非空表达式说明 = o.NONEMPTYEDESCRIBE;
                //FieldItems fields = new FieldItems();
                //fields._SourceObject = GetDataColumnInfo(o.FIELDNAME);
                //fields._SourceObject = new GridColumnInfo(_GridView.Columns);
               // cellAttribute._objectWrapper = fields;

                columsAttribute.列头设置 = headerAttribute;
                columsAttribute.单元格设置 = cellAttribute;

               
                gridViewColumnAttributge.Add(columsAttribute);
                index++;
                #endregion
            });
            //GridView属性
            GridViewRowAttribute gridViewRowAttribute = new GridViewRowAttribute();
            gridViewRowAttribute.行号 = _EDataLayout1.LINENUMBER.ToInt(1) == 0 ? false : true;
            gridViewRowAttribute.行字体大小 = _EDataLayout1.ROWFONTSIZE.ToInt(9);
            gridViewRowAttribute.行背景色 = _EDataLayout1.ROWBACKCOLOREXPRESSION;
            gridViewRowAttribute.行背景色描述 = _EDataLayout1.ROWBACKCOLORDESCRIBE;

            //FieldItems field = new FieldItems();
            //GridColumnInfo gridColumnInfo = new GridColumnInfo(_GridView.Columns);
            //gridColumnInfo.UnboundExpression = _EDataLayout1.ROWBACKCOLOREXPRESSION;
            //field._SourceObject = gridColumnInfo;

            //gridViewRowAttribute._objectWrapper = field;

            GridViewOtherAttribute gridViewOtherAttribute = new GridViewOtherAttribute();
            gridViewOtherAttribute.是否允许排序 = _EDataLayout1.ALLOWSORT.ToInt(1)==0?false:true;
            gridViewOtherAttribute.是否允许过滤 = _EDataLayout1.ALLOWFILTER.ToInt(1) == 0 ? false : true;
            gridViewOtherAttribute.是否显示分组面板 = _EDataLayout1.SHOWGROUPPANEL.ToInt(0) == 0 ? false : true; ;
            gridViewOtherAttribute.是显示列菜单 = _EDataLayout1.ENABLECOLUMNMENU.ToInt(1) == 0 ? false : true; ;
            gridViewOtherAttribute.特定排序列设置 = _EDataLayout1.ORDERBYFIELD;

            gridViewAttribute.GridViewColumnAttribute = gridViewColumnAttributge;
            gridViewAttribute.GridViewOtherAttribute = gridViewOtherAttribute;
            gridViewAttribute.GridViewRowAttribute = gridViewRowAttribute;
            return gridViewAttribute;
        }
        #endregion

        #region DataLayoutAttribute 数据源处理
        /// <summary>
        /// 设置DataLayout属性
        /// </summary>
        private void DataLayoutAttributeSet()
        {
            RecursionLayoutControl(this._MediDataLayoutControl.Root);

            BindFieldList();
        }

        /// <summary>
        /// 获取DataLayout数据源
        /// </summary>
        private void GetDataLayoutDataSource()
        {
            //CreateGridColumnCollectionClass();

            if (!LoadDBDataLayoutAttribute())
                LoadDefaultDataLayoutAttribute();            
        }

        private List<GridColumn> CreateGridColumnCollectionClass()
        {
            List<GridColumn> gridColumnList = new List<GridColumn>();

            foreach (BaseLayoutItem item in _MediDataLayoutControl.Items)
            {
                if (item is LayoutControlItem)
                {
                    Control c = (item as LayoutControlItem).Control;
                    if (c == null) continue;

                    string caption = (item as LayoutControlItem).Text;
                    GridColumn gridColumn = new GridColumn();

                    if (c is BaseEdit)
                    {
                        BaseEdit baseEdit = c as BaseEdit;

                        if (c.DataBindings.Count == 0) continue;
                        string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                        gridColumn.FieldName = fieldName;

                        gridColumn.Caption = caption;

                        gridColumnList.Add(gridColumn);
                    }
                }
            }
            return gridColumnList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LoadDefaultDataLayoutAttribute()
        {
            if (_MediDataLayoutControl != null)
            {
                //最外层的Group
                LayoutControlGroup layGroup1 = _MediDataLayoutControl.Root;

                RecursionLayoutControl(layGroup1);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LoadDBDataLayoutAttribute()
        {
            if (_EDataLayout1 == null ) return false;

            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) return false;

            int index = 1;
            //FieldItems field = new FieldItems();
            //field._SourceObject = CreateGridColumnCollectionClass();

            _EDataLayout2.ToList().ForEach(o=> {
                string feildName = o.FIELDNAME;
                if (!_DictionaryItem.ContainsKey(feildName))
                {
                    ItemLayoutAttribute itemLayoutAttribute = new ItemLayoutAttribute();
                    itemLayoutAttribute.中文名称 =o.CAPTION;
                    itemLayoutAttribute.字段名 = feildName;
                    itemLayoutAttribute.保护标志 = o.READONLY.ToInt(0)==0 ? false:true;
                    itemLayoutAttribute.初始值 = o.DEFAULTVALUE;
                    itemLayoutAttribute.字体大小 = o.HEADERFONTSIZE.ToInt(9);
                    itemLayoutAttribute.显示标志 = o.VISIBLE.ToInt(1)==0?false:true;
                    itemLayoutAttribute.有效性检查 = o.VALIDATEEXPRISSION;
                    itemLayoutAttribute.有效性说明 = o.VALIDATEDESCRIBE;
                    itemLayoutAttribute.标题对齐方式 = o.HEADERHALIGNMENT.ToInt(0).ToEnum<HorzAlignment>();
                    itemLayoutAttribute.跳转顺序 = o.TABINDEX.ToInt(index);
                    itemLayoutAttribute.输入法模式 = o.IMEMODE.ToEnum<ImeMode>();
                    //itemLayoutAttribute.非空表达式 = o.NONEMPTYEXPRESSION;
                    //itemLayoutAttribute.非空表达式说明 = o.NONEMPTYEDESCRIBE;
                    itemLayoutAttribute.DatalayoutID = o.DATALAYOUTID;
                    itemLayoutAttribute.DatalayoutmxID = o.DATALAYOUTMXID;
                    //itemLayoutAttribute._objectWrapper = field;
                    _DictionaryItem.Add(feildName, itemLayoutAttribute);
                }
            });           

            return true;
        }

        /// <summary>
        /// 遍历DataLayoutControl控件中的输入框
        /// </summary>
        /// <param name="layGroup"></param>
        /// <param name="details"></param>
        private void RecursionLayoutControl(LayoutControlGroup layGroup)
        {
            if (layGroup == null) return;

            if (layGroup.Items == null || layGroup.Items.Count == 0) return;

            LayoutGroupItemCollection layoutGroupColl = layGroup.Items;
            int i = 0;

            //FieldItems field = new FieldItems();
            //field._SourceObject = CreateGridColumnCollectionClass();

            foreach (BaseLayoutItem item in layoutGroupColl)
            {
                if (item is LayoutControlGroup)
                {
                    RecursionLayoutControl(item as LayoutControlGroup);
                }
                else
                {
                    if (item is LayoutControlItem)
                    {
                        #region

                        //获取数据条
                        LayoutControlItem layItem = item as LayoutControlItem;

                        //获取ControlItem包含的控件
                        BaseEdit control = layItem.Control as BaseEdit;

                        //获取该控件绑定的数据字段
                        if (null == control || control.DataBindings.Count == 0)
                            continue;
                        
                        string sFeildName = control.DataBindings[0].BindingMemberInfo.BindingField;

                        #region  初始化缓存

                        if (_DictionaryItem.ContainsKey(sFeildName)) continue;

                        ItemLayoutAttribute itemLayout = new ItemLayoutAttribute();

                        E_GY_DATALAYOUT2 layout2 = null;

                        //先从数据库中查找
                        if (_EDataLayout2 != null)
                            layout2 = this._EDataLayout2.Where(c => c.FIELDNAME == sFeildName).FirstOrDefault();
                       
                        itemLayout.ColumnInfo = this.gridColumnInfo;
                        itemLayout._MediGridView = this.mediGridView1;
                        itemLayout.DatalayoutID = (layout2 != null ? layout2.DATALAYOUTID : string.Empty);
                        itemLayout.DatalayoutmxID = (layout2 != null ? layout2.DATALAYOUTMXID : string.Empty);

                        itemLayout.中文名称 = (layout2 != null ? layout2.CAPTION  : layItem.Text);
                        itemLayout.字段名 = sFeildName;
                        itemLayout.保护标志 = (layout2 != null ? layout2.READONLY == 1 ? true : false : control.ReadOnly);
                        itemLayout.初始值 = (layout2 != null ? layout2.DEFAULTVALUE : string.Empty);
                        itemLayout.字体大小 = (layout2 != null ? layout2.HEADERFONTSIZE.Value : layItem.AppearanceItemCaption.Font.Size);
                        itemLayout.显示标志 = (layout2 != null ? layout2.VISIBLE.ToBool(control.Visible) : control.Visible);
                        itemLayout.有效性检查 = (layout2 != null ? layout2.VALIDATEEXPRISSION : string.Empty); 
                        itemLayout.有效性说明 = (layout2 != null ? layout2.VALIDATEDESCRIBE : string.Empty);
                        itemLayout.标题对齐方式 = layItem.AppearanceItemCaption.TextOptions.HAlignment;
                        itemLayout.跳转顺序 = (layout2 != null ? layout2.TABINDEX.ToInt(control.TabIndex) : control.TabIndex);
                        itemLayout.输入法模式 = (layout2 != null ? layout2.IMEMODE.ToEnum<ImeMode>() : control.ImeMode);
                        //itemLayout.非空表达式 = (layout2 != null ? layout2.NONEMPTYEXPRESSION : string.Empty);
                        //itemLayout.非空表达式说明 = (layout2 != null ? layout2.NONEMPTYEDESCRIBE : string.Empty);
                        
                        _DictionaryItem.Add(sFeildName, itemLayout);
                      
                        #endregion

                        i++;
                        #endregion

                    }
                }
            }
        }
        /// <summary>
        /// 重新设置跳转顺序
        /// </summary>
        private void ResetTabindex()
        {
            int i = 1;
            foreach (var item in ListBoxControlColumn.Items)
            {
                string fieldName = item.ToString().Substring(item.ToString().IndexOf('<') + 1).Replace(">","");
                if (_EDataLayout2 != null)
                {
                    var ret = _EDataLayout2.Where(o => o.FIELDNAME == fieldName).FirstOrDefault();
                    if (ret != null)
                    {
                        _DictionaryItem[fieldName].跳转顺序 = i;
                        ret.TABINDEX = i;
                        
                    }
                }
                else
                {
                    _DictionaryItem[fieldName].跳转顺序 = i;
                }
                i++;
            }
            //_DictionaryItem.Clear();
            RecursionLayoutControl(this._MediDataLayoutControl.Root);
            lstBoxColumn_SelectedIndexChanged(null, null);
        }
        #endregion

        #endregion

        #region 初始化控件内容
        /// <summary>
        /// 窗体初始化
        /// </summary>
        //private void InitForm()
        //{
        //    //初始化数据集
        //    GetDataLayoutForDB();

        //    //绑定属性值
        //    BindAttribute();
        //}

        /// <summary>
        /// 绑定显示列
        /// </summary>
        private void BindFieldList(int iDefaultSelectedIndex = 0)
        {
            if (_GridView != null)
            {
                if (_DictionaryLieSheZ == null || _DictionaryLieSheZ.Count <= 0) return;

                ListBoxControlColumn.Items.Clear();

                foreach (string key in _DictionaryLieSheZ.Keys)
                {
                    if (string.IsNullOrEmpty(key)) continue;

                    ColumnsAttribute field = _DictionaryLieSheZ[key];
                   
                    string sFieldName = string.Format("{0}<{1}>", field.列头设置.中文名称, key);

                    ListBoxControlColumn.Items.Add(sFieldName);
                }
            }
            else if(_MediDataLayoutControl!=null)
            {
                if (_DictionaryItem == null || _DictionaryItem.Count <= 0) return;

                ListBoxControlColumn.Items.Clear();

               // foreach (string key in _DictionaryItem.Keys.OrderBy(c=>c.)
               foreach(var item in _DictionaryItem.OrderBy(c=>c.Value.跳转顺序).ToList())
                {
                    if (string.IsNullOrEmpty(item.Key)) continue;

                   // ItemLayoutAttribute item = _DictionaryItem[key];

                    string sFieldName = string.Format("{0}<{1}>", item.Value.中文名称, item.Key);

                    ListBoxControlColumn.Items.Add(sFieldName);
                }
            }
           
            ListBoxControlColumn.SelectedIndex = iDefaultSelectedIndex;
        }
        
        #endregion

        #region 上移 下移

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (ListBoxControlColumn.SelectedIndex < 0) return;

            //最后一列 不能再下移了
            if (ListBoxControlColumn.SelectedIndex == ListBoxControlColumn.Items.Count - 1) return;

            int iSelectedIndex = ListBoxControlColumn.SelectedIndex;

            //获取当前行的值
            object oCurrentValue = ListBoxControlColumn.SelectedItem;

            //获取下一行的值
            object oNextValue = ListBoxControlColumn.Items[iSelectedIndex + 1];

            //当前行的值赋给下一行
            ListBoxControlColumn.Items[iSelectedIndex + 1] = oCurrentValue;

            //下一行的值赋给当前行
            ListBoxControlColumn.Items[iSelectedIndex] = oNextValue;

            //移动字典
            //MoveDictionary(oCurrentValue.ToString(), oNextValue.ToString());

            //选中下一行
            ListBoxControlColumn.SelectedIndex = iSelectedIndex + 1;
            //重新设置跳转顺序
            ResetTabindex();
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            //首行和未选中的行都不能上移
            if (ListBoxControlColumn.SelectedIndex <= 0) return;

            int iSelectedIndex = ListBoxControlColumn.SelectedIndex;

            //获取当前行的值
            object oCurrentValue = ListBoxControlColumn.Items[iSelectedIndex];

            //获取上一行数据值
            object oLastValue = ListBoxControlColumn.Items[iSelectedIndex - 1];

            //赋值
            ListBoxControlColumn.Items[iSelectedIndex - 1] = oCurrentValue;

            ListBoxControlColumn.Items[iSelectedIndex] = oLastValue;

            //移动字段值
            //MoveDictionary(oCurrentValue.ToString(), oLastValue.ToString());

            ListBoxControlColumn.SelectedIndex = iSelectedIndex - 1;

            //重新设置跳转顺序
            ResetTabindex();
        }

        /// <summary>
        /// 移动字典值
        /// </summary>
        /// <param name="sCurrentValue">当前行的值</param>
        /// <param name="sUpOrDownValue">上一行或下一行的值</param>
        /// <param name="bUp">true:上移 false:下移</param>
        private void MoveDictionary(string sCurrentValue, string sUpOrDownValue)
        {
            string sCurrentFieldName = sCurrentValue.Remove(0, sCurrentValue.IndexOf("<") + 1).Replace(">", "").Trim();
            string sUpOrDownFieldName = sUpOrDownValue.Remove(0, sUpOrDownValue.IndexOf("<") + 1).Replace(">", "").Trim();

            if (_GridView != null)
            {
                //移动
                ColumnsAttribute fieldUpDown = _DictionaryLieSheZ[sUpOrDownFieldName];
                ColumnsAttribute fieldCurrent = _DictionaryLieSheZ[sCurrentFieldName];
                _DictionaryLieSheZ[sUpOrDownFieldName] = fieldCurrent;
                _DictionaryLieSheZ[sCurrentFieldName] = fieldUpDown;
            }
            else if(_MediDataLayoutControl!=null)
            {
                //移动
                ItemLayoutAttribute fieldUpDown = _DictionaryItem[sUpOrDownFieldName];
                ItemLayoutAttribute fieldCurrent = _DictionaryItem[sCurrentFieldName];
                _DictionaryItem[sUpOrDownFieldName] = fieldCurrent;
                _DictionaryItem[sCurrentFieldName] = fieldUpDown;
            }

            
        }
        #endregion

        #region ListBox事件
        /// <summary>
        /// 改变选项的字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstBoxColumn_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (_GridView != null)
            {
                if (_DictionaryLieSheZ == null || _DictionaryLieSheZ.Count <= 0) return;

                foreach (string key in _DictionaryLieSheZ.Keys)
                {
                    if (string.IsNullOrEmpty(key)) continue;

                    ColumnsAttribute field = _DictionaryLieSheZ[key];

                    //不显示的列 字体改为斜体
                    if (!field.列头设置.显示标志 && e.Item.ToString().IndexOf(field.列头设置.字段名) > -1)
                        e.Appearance.FontStyleDelta = FontStyle.Italic;
                }
            }
            else if(_MediDataLayoutControl!=null)
            {                
                if (_DictionaryItem == null || _DictionaryItem.Count <= 0) return;

                foreach (string key in _DictionaryItem.Keys)
                {
                    if (string.IsNullOrEmpty(key)) continue;

                    ItemLayoutAttribute field = _DictionaryItem[key];

                    //不显示的列 字体改为斜体
                    if (!field.显示标志 && e.Item.ToString().IndexOf(field.字段名) > -1)
                        e.Appearance.FontStyleDelta = FontStyle.Italic;
                }
            }
            e.AllowDrawSkinBackground = false;
           
            if (ListBoxControlColumn.SelectedItems.IndexOf(e.Item) > -1)
            {
                //e.Appearance.Options.UseBackColor = true;
                //e.Appearance.BackColor = ColorTranslator.FromHtml("#199ed8");
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstBoxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //多选判断
            if (ListBoxControlColumn.SelectedItems == null || ListBoxControlColumn.SelectedItems.Count == 0) return;

            BaseListBoxControl.SelectedItemCollection selectedItemCollection = ListBoxControlColumn.SelectedItems;

            //判断用户选择是多行还是一行
            if (selectedItemCollection.Count == 1)
            {
                string sSelectedItem = ListBoxControlColumn.SelectedItem.ToString();

                string sFieldName = sSelectedItem.Remove(0, sSelectedItem.IndexOf("<") + 1).Replace(">", "").Trim();

                if (_GridView != null)
                {
                    ColumnsAttribute field = _DictionaryLieSheZ[sFieldName];

                    propertyGridLieShuX.SelectedObject = field;
                }
                else if (_MediDataLayoutControl != null)
                {
                    ItemLayoutAttribute field = _DictionaryItem[sFieldName];

                    propertyGridLieShuX.SelectedObject = field;
                }

                //重新绘制控件
                ListBoxControlColumn.Invalidate();
            }
            else
            {
                //选择多行 需要重新初始化属性集
                object[] obj = new object[selectedItemCollection.Count];
                int index = 0;
                foreach (object item in selectedItemCollection)
                {
                    string sFieldName = item.ToString().Remove(0, item.ToString().IndexOf("<") + 1).Replace(">", "").Trim();
                    if (_GridView != null)
                    {
                        ColumnsAttribute field = _DictionaryLieSheZ[sFieldName];

                        obj[index] = field;
                    }
                    else if(_MediDataLayoutControl!=null)
                    {
                        ItemLayoutAttribute field = _DictionaryItem[sFieldName];

                        obj[index] = field;
                    }
                    index++;
                }
                propertyGridLieShuX.SelectedObjects = obj;
            }


        }      
        #endregion

        #region 属性事件
        /// <summary>
        /// 常用属性
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propGridCommon_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (ListBoxControlColumn.SelectedItems == null || ListBoxControlColumn.SelectedItems.Count == 0) return;

            if (ListBoxControlColumn.SelectedItems.Count == 1)
            {
                string sSelectedItem = ListBoxControlColumn.SelectedItem.ToString();

                string sFieldName = sSelectedItem.Remove(0, sSelectedItem.IndexOf("<") + 1).Replace(">", "").Trim();

                if (_GridView != null)
                {
                    ColumnsAttribute field = _DictionaryLieSheZ[sFieldName];

                    field = propertyGridLieShuX.SelectedObject as ColumnsAttribute;

                    _DictionaryLieSheZ[sFieldName] = field;
                }
                else if (_MediDataLayoutControl != null)
                {
                    ItemLayoutAttribute field = _DictionaryItem[sFieldName];

                    field = propertyGridLieShuX.SelectedObject as ItemLayoutAttribute;

                    _DictionaryItem[sFieldName] = field;
                }
            }
            else
            {
                object[] objectList = propertyGridLieShuX.SelectedObjects;

                if (objectList == null || objectList.Length == 0) return;

                foreach (object item in ListBoxControlColumn.SelectedItems)
                {
                    string sFieldName = item.ToString().Remove(0, item.ToString().IndexOf("<") + 1).Replace(">", "").Trim();

                    if (_GridView != null)
                    {
                        ColumnsAttribute field = _DictionaryLieSheZ[sFieldName];

                        foreach (object obj in objectList)
                        {
                            if (field.列头设置.字段名 == (obj as ColumnsAttribute).列头设置.字段名)
                            {
                                _DictionaryLieSheZ[sFieldName] = obj as ColumnsAttribute;
                            }
                        }
                    }
                    else if (_MediDataLayoutControl != null)
                    {
                        ItemLayoutAttribute field = _DictionaryItem[sFieldName];
                        foreach (object obj in objectList)
                        {
                            if (field.字段名 == (obj as ItemLayoutAttribute).字段名)
                            {
                                _DictionaryItem[sFieldName] = obj as ItemLayoutAttribute;
                            }
                        }
                    }
                }
            }
            //重新加载
            BindFieldList(ListBoxControlColumn.SelectedIndex);
            //重新绘制控件
            ListBoxControlColumn.Invalidate();

        }

        /// <summary>
        /// 规则属性设置
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propGridRelue_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// 其他属性
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propGridOrther_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

        }
        #endregion

        #region 保存返回
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();
            List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            if (_GridView!=null)
            {
                GetGridViewAttribute(ref eDataLayout1, ref eDatalayout2List);
            }
            else if(_MediDataLayoutControl!=null)
            {

                GetDataLayoutAttribute(ref eDataLayout1,ref eDatalayout2List);
            }
            Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(eDataLayout1, eDatalayout2List);

            if (result.Return)
            {

                MediMsgBox.Success(this, "保存成功！");
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleButtomReset_Click(object sender,EventArgs e)
        {
            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();
            List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            if (_GridView != null)
            {
                GetGridViewAttribute(ref eDataLayout1, ref eDatalayout2List);
            }
            else if (_MediDataLayoutControl != null)
            {
                GetDataLayoutAttribute(ref eDataLayout1, ref eDatalayout2List);
            }
            eDataLayout1.SetState(DTOState.Delete);

            eDatalayout2List.ForEach(o=> {
                o.SetState(DTOState.Delete);
            });
            Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(eDataLayout1, eDatalayout2List);

            if (result.Return)
                MediMsgBox.Success("重置成功!");
            else
                MediMsgBox.Failure("重置失败!");

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 获取DataLayou数据源
        /// </summary>
        /// <param name="eDataLayout1"></param>
        /// <param name="eDatalayout2List"></param>
        private void GetDataLayoutAttribute(ref E_GY_DATALAYOUT1 eDataLayout1,ref List<E_GY_DATALAYOUT2> eDatalayout2List)
        {
            if (_DictionaryItem == null || _DictionaryItem.Count == 0) return;

            #region 处理DataLayout1数据
            //E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();

            if (_EDataLayout1 == null )
            {
                //等于空
                eDataLayout1.SetState(DTOState.New);
            }
            else
            {
                //不等空
                eDataLayout1.DATALAYOUTID = _EDataLayout1.DATALAYOUTID;
                eDataLayout1.SetState(DTOState.Update);
            }

            eDataLayout1.YINGYONGID = (radioGroupLevel.SelectedIndex == 0 ? HISClientHelper.YINGYONGID : (radioGroupLevel.SelectedIndex == 1 ? HISClientHelper.XITONGID:"00"));

            eDataLayout1.FORMNAME = _FormName;
            eDataLayout1.CONTROLNAME = _ControlName;
            eDataLayout1.NAMESPACE = _NameSpace;
            eDataLayout1.ALLOWFILTER = 0;
            eDataLayout1.ALLOWSORT =0;
            eDataLayout1.ENABLECOLUMNMENU = 0;
            eDataLayout1.ORDERBYFIELD ="";
            eDataLayout1.SHOWGROUPPANEL = 0;
            eDataLayout1.LINENUMBER = 0;
            eDataLayout1.ROWBACKCOLOREXPRESSION = "";
            eDataLayout1.ROWBACKCOLORDESCRIBE ="";
            eDataLayout1.ROWFONTSIZE = 9;
            #endregion

            #region 处理DataLayout2数据
            //List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            bool update = true;
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) update = false;

            //foreach (string key in _DictionaryItem.Keys)
            //{
            //    E_GY_DATALAYOUT2 eDataLayout2 = DataLayoutAttributeToE(_DictionaryItem[key], update);

            //    eDatalayout2List.Add(eDataLayout2);
            //}
            ListBoxItemCollection listBoxItemCollection = ListBoxControlColumn.Items;

            foreach (object obj in listBoxItemCollection)
            {
                string key = obj.ToString().Remove(0, obj.ToString().IndexOf("<") + 1).Replace(">", "").Trim();
                E_GY_DATALAYOUT2 eDataLayout2 = null;
                if (_EDataLayout2 == null)
                {
                    eDataLayout2 = DataLayoutAttributeToE(_DictionaryItem[key], false);
                }
                else
                {
                    var count = _EDataLayout2.Where(o => o.FIELDNAME.ToUpper() == key.ToUpper()).Count();
                    
                    if (count > 0)
                    {
                        eDataLayout2 = DataLayoutAttributeToE(_DictionaryItem[key], update);
                    }
                    else
                    {
                        eDataLayout2 = DataLayoutAttributeToE(_DictionaryItem[key], false);
                    }
                }        
                eDatalayout2List.Add(eDataLayout2);
            }


            #endregion

        }

        /// <summary>
        /// 保存GridView属性
        /// </summary>
        private void GetGridViewAttribute(ref E_GY_DATALAYOUT1 eDataLayout1,ref List<E_GY_DATALAYOUT2> eDatalayout2List)
        {
            if (_DictionaryLieSheZ == null || _DictionaryLieSheZ.Count == 0) return;           

            #region 处理DataLayout1数据
            GridViewRowAttribute gridViewRowAttribute = propertyGridHangShuX.SelectedObject as GridViewRowAttribute;
            GridViewOtherAttribute gridViewOtherAttribute = propertyGridQiTaSX.SelectedObject as GridViewOtherAttribute;

            //E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();

            if(_EDataLayout1==null )
            {
                //等于空
                eDataLayout1.SetState(DTOState.New);
            }
            else
            {
                //不等空
                eDataLayout1.DATALAYOUTID = _EDataLayout1.DATALAYOUTID;
                eDataLayout1.SetTraceChange(true);
                eDataLayout1.SetState(DTOState.Update);
            }
            eDataLayout1.YINGYONGID = (radioGroupLevel.SelectedIndex == 0 ? HISClientHelper.YINGYONGID : (radioGroupLevel.SelectedIndex == 1 ? HISClientHelper.XITONGID : "00"));
            eDataLayout1.FORMNAME = _FormName;
            eDataLayout1.CONTROLNAME = _ControlName;
            eDataLayout1.NAMESPACE = _NameSpace;
            eDataLayout1.ALLOWFILTER = gridViewOtherAttribute.是否允许过滤 ? 1 : 0;
            eDataLayout1.ALLOWSORT = gridViewOtherAttribute.是否允许排序 ? 1 : 0;
            eDataLayout1.ENABLECOLUMNMENU = gridViewOtherAttribute.是显示列菜单 ? 1 : 0;
            eDataLayout1.ORDERBYFIELD = gridViewOtherAttribute.特定排序列设置;
            eDataLayout1.SHOWGROUPPANEL = gridViewOtherAttribute.是否显示分组面板 ? 1 : 0;
            eDataLayout1.LINENUMBER = gridViewRowAttribute.行号 ? 1 : 0;
            eDataLayout1.ROWBACKCOLOREXPRESSION = gridViewRowAttribute.行背景色;
            eDataLayout1.ROWBACKCOLORDESCRIBE = gridViewRowAttribute.行背景色描述;
            eDataLayout1.ROWFONTSIZE = Convert.ToInt32(gridViewRowAttribute.行字体大小);
            #endregion

            #region 处理DataLayout2数据
            //List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            bool update = true;
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) update = false;

            ListBoxItemCollection listBoxItemCollection = ListBoxControlColumn.Items;
            int index = 0;
            foreach (object obj in listBoxItemCollection)
            {
                string key = obj.ToString().Remove(0, obj.ToString().IndexOf("<") + 1).Replace(">", "").Trim();

                ColumnsAttribute bute = _DictionaryLieSheZ[key];
                bute.SortNo = index;

                E_GY_DATALAYOUT2 eDatalayout2 = null;
                if (_EDataLayout2 == null)
                {
                    eDatalayout2 = GridViewAttributeToE(bute, false);
                }
                else
                {
                    var count = _EDataLayout2.Where(o => o.FIELDNAME.ToUpper() == key.ToUpper()).Count();

                    if (count > 0)
                    {
                        eDatalayout2 = GridViewAttributeToE(bute, update);
                    }
                    else
                    {
                        eDatalayout2 = GridViewAttributeToE(bute, false);
                    }
                }
                //E_GY_DATALAYOUT2 eDatalayout2 = GridViewAttributeToE(bute, update);

                eDatalayout2List.Add(eDatalayout2);
                index++;
            }

            //foreach (string key in _DictionaryLieSheZ.Keys)
            //{
            //    ColumnsAttribute bute = _DictionaryLieSheZ[key];
            //    bute.SortNo = index;                

            //    E_GY_DATALAYOUT2 eDatalayout2 = GridViewAttributeToE(bute, update);

            //    eDatalayout2List.Add(eDatalayout2);
            //    index++;
            //}
            #endregion           
        }

        /// <summary>
        /// 属性信息转化为实体
        /// </summary>
        /// <param name="columnsAttribute"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        private E_GY_DATALAYOUT2 GridViewAttributeToE(ColumnsAttribute columnsAttribute,bool update)
        {
            E_GY_DATALAYOUT2 eDataLayout2 = new E_GY_DATALAYOUT2();
            eDataLayout2.SetTraceChange(true);
            eDataLayout2.DATALAYOUTID = columnsAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = columnsAttribute.DatalayoutMXID;
            eDataLayout2.SORTNO = columnsAttribute.SortNo;

            if (update) eDataLayout2.SetState(DTOState.Update);
            else eDataLayout2.SetState(DTOState.New);

            //eDataLayout2.BACKCOLORDESCRIBE = columnsAttribute.单元格设置.背景颜色说明;
            //eDataLayout2.BACKCOLOREXPRISSION = columnsAttribute.单元格设置.背景颜色表达式;
            eDataLayout2.CAPTION = columnsAttribute.列头设置.中文名称;
            eDataLayout2.CELLFONTSIZE = Convert.ToInt32(columnsAttribute.单元格设置.单元格字体大小);
            //eDataLayout2.CELLFORECOLORDESCRIBE = columnsAttribute.单元格设置.字体颜色说明;
            //eDataLayout2.CELLFORECOLOREXPRISSION = columnsAttribute.单元格设置.字体颜色表达式;
            eDataLayout2.CELLHALIGNMENT = Convert.ToInt32(columnsAttribute.单元格设置.单元格文本对齐方式);
            eDataLayout2.DATALAYOUTID = columnsAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = columnsAttribute.DatalayoutMXID;
            eDataLayout2.DEFAULTVALUE = columnsAttribute.单元格设置.初始值;
            eDataLayout2.FIELDNAME = columnsAttribute.列头设置.字段名;
            eDataLayout2.FIXED = Convert.ToInt32(columnsAttribute.列头设置.列头固定);
            eDataLayout2.FORMATSTRING = columnsAttribute.单元格设置.显示格式;
            eDataLayout2.FORMATTYPE = Convert.ToInt32(columnsAttribute.单元格设置.显示格式类型);
            eDataLayout2.HEADERFONTSIZE = Convert.ToInt32(columnsAttribute.列头设置.头字体大小);
            eDataLayout2.HEADERHALIGNMENT = Convert.ToInt32(columnsAttribute.列头设置.头标题对齐方式);
            eDataLayout2.IMEMODE = Convert.ToInt32(columnsAttribute.单元格设置.输入法模式).ToString();
            //eDataLayout2.NONEMPTYEDESCRIBE = columnsAttribute.单元格设置.非空表达式说明;
            //eDataLayout2.NONEMPTYEXPRESSION = columnsAttribute.单元格设置.非空表达式;
            eDataLayout2.READONLY = columnsAttribute.单元格设置.保护标志 ? 1 : 0;
            eDataLayout2.SORTNO = columnsAttribute.SortNo;
            eDataLayout2.TABINDEX = columnsAttribute.单元格设置.跳转顺序;
            eDataLayout2.VALIDATEDESCRIBE = columnsAttribute.单元格设置.有效性说明;
            eDataLayout2.VALIDATEEXPRISSION = columnsAttribute.单元格设置.有效性检查;
            eDataLayout2.VISIBLE = columnsAttribute.列头设置.显示标志?1:0;
            eDataLayout2.WIDTH = columnsAttribute.列头设置.列宽度;

            return eDataLayout2;
        }

        /// <summary>
        /// 属性信息转化为实体
        /// </summary>
        /// <param name="itemLayoutAttribute"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        private E_GY_DATALAYOUT2 DataLayoutAttributeToE(ItemLayoutAttribute itemLayoutAttribute,bool update)
        {
            E_GY_DATALAYOUT2 eDataLayout2 = new E_GY_DATALAYOUT2();
            eDataLayout2.SetTraceChange(true);
            eDataLayout2.DATALAYOUTID = itemLayoutAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = itemLayoutAttribute.DatalayoutmxID;
            eDataLayout2.SORTNO = 0;

            if (update) eDataLayout2.SetState(DTOState.Update);
            else eDataLayout2.SetState(DTOState.New);

            eDataLayout2.BACKCOLORDESCRIBE = "";
            eDataLayout2.BACKCOLOREXPRISSION = "";
            eDataLayout2.CAPTION = itemLayoutAttribute.中文名称;
            eDataLayout2.CELLFONTSIZE =9;
            eDataLayout2.CELLFORECOLORDESCRIBE ="";
            eDataLayout2.CELLFORECOLOREXPRISSION = "";
            eDataLayout2.CELLHALIGNMENT =0;
            eDataLayout2.DATALAYOUTID = itemLayoutAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = itemLayoutAttribute.DatalayoutmxID;
            eDataLayout2.DEFAULTVALUE = itemLayoutAttribute.初始值;
            eDataLayout2.FIELDNAME = itemLayoutAttribute.字段名;
            eDataLayout2.FIXED =0;
            eDataLayout2.FORMATSTRING = "";
            eDataLayout2.FORMATTYPE = 0;
            eDataLayout2.HEADERFONTSIZE = Convert.ToInt32(itemLayoutAttribute.字体大小);
            eDataLayout2.HEADERHALIGNMENT = Convert.ToInt32(itemLayoutAttribute.标题对齐方式);
            eDataLayout2.IMEMODE = Convert.ToInt32(itemLayoutAttribute.输入法模式).ToString();
            //eDataLayout2.NONEMPTYEDESCRIBE = itemLayoutAttribute.非空表达式说明;
            //eDataLayout2.NONEMPTYEXPRESSION = itemLayoutAttribute.非空表达式;
            eDataLayout2.READONLY = itemLayoutAttribute.保护标志 ? 1 : 0;
            

            eDataLayout2.SORTNO = 0;
            eDataLayout2.TABINDEX = itemLayoutAttribute.跳转顺序;
            eDataLayout2.VALIDATEDESCRIBE = itemLayoutAttribute.有效性说明;
            eDataLayout2.VALIDATEEXPRISSION = itemLayoutAttribute.有效性检查;
            eDataLayout2.VISIBLE = itemLayoutAttribute.显示标志 ? 1 : 0;
            eDataLayout2.WIDTH = 99;
                    

            return eDataLayout2;
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 搜索框相关
        /// <summary>
        /// 搜索框显示与隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnEditSearch.Visible)
                btnEditSearch.Visible = false;
            else
                btnEditSearch.Visible = true;
        }

        /// <summary>
        /// 搜索框功能按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSearch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            btnEditSearch.Text = "";
        }

        /// <summary>
        /// 搜索框按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(btnEditSearch.Text))
            {
                this.btnEditSearch.Properties.Buttons.Clear();
                this.btnEditSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
            }
            else
            {
                this.btnEditSearch.Properties.Buttons.Clear();
                this.btnEditSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            }

            //遍历列表框中的数据
            string sSearchText = btnEditSearch.Text;

            SearchListBox(sSearchText);
        }

        /// <summary>
        /// 查询搜索信息
        /// </summary>
        /// <param name="sSearchText"></param>
        private void SearchListBox(string sSearchText)
        {
            if (_GridView != null)
            {
                if (_DictionaryLieSheZ == null || _DictionaryLieSheZ.Count == 0) return;

                ListBoxControlColumn.Items.Clear();

                if (string.IsNullOrEmpty(sSearchText))
                {
                    BindFieldList();
                    return;
                }

                foreach (string key in _DictionaryLieSheZ.Keys)
                {
                    if (key.ToUpper().IndexOf(sSearchText.ToUpper()) > -1)
                    {
                        ColumnsAttribute field = _DictionaryLieSheZ[key];

                        string sFieldName = string.Format("{0}<{1}>", field.列头设置.中文名称, key);

                        ListBoxControlColumn.Items.Add(sFieldName);
                    }

                }
            }
            else if(_MediDataLayoutControl!=null)
            {
                if (_DictionaryItem == null || _DictionaryItem.Count == 0) return;

                ListBoxControlColumn.Items.Clear();

                if (string.IsNullOrEmpty(sSearchText))
                {
                    BindFieldList();
                    return;
                }

                foreach (string key in _DictionaryItem.Keys)
                {
                    if (key.ToUpper().IndexOf(sSearchText.ToUpper()) > -1)
                    {
                        ItemLayoutAttribute field = _DictionaryItem[key];

                        string sFieldName = string.Format("{0}<{1}>", field.中文名称, key);

                        ListBoxControlColumn.Items.Add(sFieldName);
                    }

                }
            }

        }

        #endregion

        private void FrmDataLayoutSet_Load(object sender, EventArgs e)
        {
            //初始化数据集
            GetDataLayoutForDB();

            //绑定数据源，用来设置表达式
            this.mediGridView1.PopulateColumns();
            this.gridColumnInfo = new GridColumnInfo(this.mediGridView1.Columns);
            

            //绑定属性值
            if (_GridView != null)
            {
                GridViewAttributeSet();
            }
            else if (_MediDataLayoutControl != null)
            {
                DataLayoutAttributeSet();
            }
        }
    }

    #region GridColumnInfo
    /// <summary>
    /// 存储列信息
    /// </summary>
    public class GridColumnInfo : IDataColumnInfo
    {
        private GridColumnCollection columns;
        private GridColumn column;
       
        public GridColumnInfo(GridColumnCollection columns)
        {
            this.columns = columns;
        }

        private GridColumnInfo(GridColumn column)
        {
            this.column = column;
        }

        #region IDataColumnInfo Members

        public string Caption
        {
            get
            {
                if (column == null) return string.Empty;
                return string.IsNullOrEmpty(column.Caption) ? column.FieldName : column.Caption;
            }
        }

        public List<IDataColumnInfo> Columns { get { return GetColumns(); } }

        private List<IDataColumnInfo> GetColumns()
        {
            if (column != null) return null;
            List<IDataColumnInfo> result = new List<IDataColumnInfo>();
            foreach (GridColumn col in columns)
                result.Add(new GridColumnInfo(col));
            return result;
        }

        public DataControllerBase Controller { get { return null; } }

        public string ExpressionValue { get; set; }

        public string FieldName { get { return column == null ? string.Empty : column.FieldName; } }


        public Type FieldType { get { return column == null ? null : column.ColumnType; } }

        public string Name { get { return Caption; } }

        public string UnboundExpression { get; set; }

        #endregion
    }
    
    #endregion

    #region 列属性

    /// <summary>
    /// 
    /// </summary>
    public class GridViewAttribute
    {
        /// <summary>
        /// 列属性
        /// </summary>
        public List<ColumnsAttribute> GridViewColumnAttribute { get; set; }

        /// <summary>
        /// 行属性
        /// </summary>
        public GridViewRowAttribute GridViewRowAttribute { get; set; }

        /// <summary>
        /// 其他属性
        /// </summary>
        public GridViewOtherAttribute GridViewOtherAttribute { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string DatalayoutID { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string YingYongID { get; set; }

        /// <summary>
        /// 窗体名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }
    }

    /// <summary>
    /// 列属性
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class ColumnsAttribute
    {
        #region 表格属性
        [XtraSerializableProperty]
        [DescriptionAttribute("列头相关属性设置"), CategoryAttribute("表格属性"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public HeaderAttribute 列头设置 { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("单元格相关属性设置"), CategoryAttribute("表格属性"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public CellAttribute 单元格设置 { get; set; }
        #endregion

        #region 不显示属性
        /// <summary>
        /// 排序号
        /// </summary>
        [BrowsableAttribute(false)]
        [XtraSerializableProperty]
        public int SortNo
        { get; set; }

        /// <summary>
        /// 外键ID
        /// </summary>
        [BrowsableAttribute(false)]
        [XtraSerializableProperty]
        public string DatalayoutID
        { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        [BrowsableAttribute(false)]
        [XtraSerializableProperty]
        public string DatalayoutMXID
        { get; set; }
        #endregion
        
    }

    /// <summary>
    /// 头属性
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class HeaderAttribute
    {
        #region 表头属性
        [XtraSerializableProperty]
        [DescriptionAttribute("字段名称"), CategoryAttribute("列头设置"), ReadOnlyAttribute(true), BrowsableAttribute(true)]
        public string 字段名
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("字段名称"), CategoryAttribute("列头设置"), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public string 中文名称
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("是否在列中显示该字段"), CategoryAttribute("列头设置"), DefaultValueAttribute(true), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 显示标志
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置该列的宽度"), CategoryAttribute("列头设置"), DefaultValueAttribute(100), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public int 列宽度
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置一个值，该值指定列是否参与水平视图滚动，或锚定到视图边缘。"), CategoryAttribute("列头设置"), DefaultValueAttribute(HorizontalAlignment.Center), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public FixedStyle 列头固定
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置列头的字体大小"), CategoryAttribute("列头设置"), DefaultValueAttribute(9), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public float 头字体大小
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("列头文本对齐方式：Default默认,Near左对齐,Center居中,Far右对齐"), CategoryAttribute("列头设置"), DefaultValueAttribute(HorizontalAlignment.Center), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public HorzAlignment 头标题对齐方式
        { get; set; }
        #endregion
    }

    /// <summary>
    /// 单元格属性
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CellAttribute /*: IDXObjectWrapper*/
    {
        #region 主键列
        /// <summary>
        /// 主键
        /// </summary>
        [BrowsableAttribute(false)]
        public string DatalayoutmxID { get; set; }

        /// <summary>
        /// 外键ID
        /// </summary>
        [BrowsableAttribute(false)]
        public string DatalayoutID { get; set; }

        /// <summary>
        /// 对应的数据源信息
        /// </summary>
        [Browsable(false)]
        public GridColumnInfo ColumnInfo { get; set; }
        [Browsable(false)]
        public MediGridView _MediGridView { get; set; }
        #endregion
        #region 单元格属性
        [XtraSerializableProperty]
        [DescriptionAttribute("按下回车跳转顺序"), CategoryAttribute("单元格属性"), DefaultValueAttribute(0), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public int 跳转顺序
        { get; set; }

        //[XtraSerializableProperty]
        //[DescriptionAttribute("是否是必填项目，true-必填 False-非必填"), CategoryAttribute("单元格属性"), DefaultValueAttribute(false), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //public bool 必输标志
        //{ get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("受保护后，不能修改该字段值"), CategoryAttribute("单元格属性"), DefaultValueAttribute(false), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 保护标志
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("初始值"), CategoryAttribute("单元格属性"), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public string 初始值
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置该列单元格的字体大小"), CategoryAttribute("单元格属性"), DefaultValueAttribute(9), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public float 单元格字体大小
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("该列单元格文本对齐方式：Default默认,Near左对齐,Center居中,Far右对齐"), CategoryAttribute("单元格属性"), DefaultValueAttribute(HorizontalAlignment.Center), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public HorzAlignment 单元格文本对齐方式
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置控件的输入法编辑器 (IME) 模式"), CategoryAttribute("单元格属性"), DefaultValueAttribute(HorizontalAlignment.Center), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public ImeMode 输入法模式
        { get; set; }
        #endregion

        //#region 颜色
        //[XtraSerializableProperty]
        //[DescriptionAttribute("符合特定条件时字体颜色，支持表达式"), CategoryAttribute("颜色"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //[DXCategory(CategoryName.Data), Editor(typeof(ExpressionEditorBaseEx), typeof(System.Drawing.Design.UITypeEditor))]
        //[DevExpressXtraGridLocalizedDescriptionAttributeEx("GridColumnUnboundExpression")]
        //public string 字体颜色表达式
        //{ get; set; }

        //[XtraSerializableProperty]
        //[DescriptionAttribute("字体颜色表达式的说明"), CategoryAttribute("颜色"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //[Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        //public string 字体颜色说明
        //{ get; set; }

        //[XtraSerializableProperty]
        //[DescriptionAttribute("符合特定条件时该单元格或输入项的背景颜色，支持表达式"), CategoryAttribute("颜色"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //[Editor(typeof(ExpressionEditorBaseEx), typeof(System.Drawing.Design.UITypeEditor))]
        //public string 背景颜色表达式
        //{ get; set; }

        //[XtraSerializableProperty]
        //[DescriptionAttribute("背景颜色表达式的说明"), CategoryAttribute("颜色"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //[Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        //public string 背景颜色说明
        //{ get; set; }
        //#endregion

        #region 有效性验证
        [XtraSerializableProperty]
        [DescriptionAttribute("支持标准的正则表达，根据表达式检查数据有效性"), CategoryAttribute("有效性验证"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 有效性检查
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("正则表达式说明"), CategoryAttribute("有效性验证"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 有效性说明
        { get; set; }

        #endregion

        #region 格式
        [XtraSerializableProperty]
        [DescriptionAttribute("支持标准的C#中StringFormat的格式化表达式"), CategoryAttribute("显示格式"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 显示格式
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置格式类型"), CategoryAttribute("显示格式"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public FormatType 显示格式类型 { get; set; }

        #endregion

 //       #region 非空表达式
 //       [DescriptionAttribute("符合特定条件非空判断，支持表达式"), CategoryAttribute("非空表达式"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
 //       [
 //DXCategory(CategoryName.Data), XtraSerializableProperty(),
 //       Editor(typeof(ExpressionEditorBaseEx), typeof(System.Drawing.Design.UITypeEditor))
 //       ]
 //       [DevExpressXtraGridLocalizedDescriptionAttributeEx("GridColumnUnboundExpression")]

 //       public string 非空表达式
 //       { get; set; }

 //       [XtraSerializableProperty]
 //       [DescriptionAttribute("符合特定条件非空判断，支持表达式"), CategoryAttribute("非空表达式"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
 //       [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
 //       public string 非空表达式说明
 //       { get; set; }

 //       /// <summary>
 //       /// 设置字段列表
 //       /// </summary>
 //       [BrowsableAttribute(false)]
 //       public FieldItems _objectWrapper = null;
 //       [BrowsableAttribute(false)]
 //       public object SourceObject
 //       {
 //           get
 //           {
 //               object[] objectWrapper = new object[1];

 //               objectWrapper[0] = _objectWrapper;

 //               return objectWrapper;
 //           }
 //       }
 //       #endregion
    }

    /// <summary>
    /// 定义属性项
    /// </summary>
    public class FieldItems : IDXObjectWrapper
    {
        public FieldItems() { }
        public object _SourceObject = null;
        public object SourceObject
        {
            get
            {
                return _SourceObject;
            }
        }
    }
    #endregion

    #region 行属性
    /// <summary>
    /// 行属性
    /// </summary>
    public class GridViewRowAttribute : IDXObjectWrapper
    {       
        #region 行背景色
        [XtraSerializableProperty]
        [DescriptionAttribute("符合特定条件时行的背景颜色，支持表达式"), CategoryAttribute("颜色"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(ExpressionEditorBaseEx), typeof(System.Drawing.Design.UITypeEditor))]
        [DevExpressXtraGridLocalizedDescriptionAttributeEx("GridColumnUnboundExpression")]
        public string 行背景色
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("行背景颜色表达式的说明"), CategoryAttribute("颜色"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 行背景色描述
        { get; set; }
        #endregion

        #region 行号
        [XtraSerializableProperty]
        [DescriptionAttribute("是否显示行号"), CategoryAttribute("行号"), DefaultValueAttribute(true), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 行号
        {
            get; set;
        }
        #endregion

        #region 行字体
        [XtraSerializableProperty]
        [DescriptionAttribute("设置每行的字体大小"), CategoryAttribute("行字体"), DefaultValueAttribute(true), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public float 行字体大小 { get; set; }

        /// <summary>
        /// 设置字段列表
        /// </summary>
        [BrowsableAttribute(false)]
        public FieldItems _objectWrapper = null;
        [BrowsableAttribute(false)]
        public object SourceObject
        {
            get
            {
                object[] objectWrapper = new object[1];

                objectWrapper[0] = _objectWrapper;

                return objectWrapper;
            }
        }
        #endregion
    }
    #endregion

    #region 其他属性
    /// <summary>
    /// 其他属性
    /// </summary>
    public class GridViewOtherAttribute
    {
        [XtraSerializableProperty]
        [DescriptionAttribute("设置按那些字段进行排序,以逗号格式，格式：字段名|排序方式（ASC-升序或者DESC-降序）,字段名|排序方式（ASC或者DESC）"), CategoryAttribute("其他设置"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 特定排序列设置 { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("是否显示分组面板，true-显示，false-不显示"), CategoryAttribute("其他设置"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 是否显示分组面板 { get; set; }


        [XtraSerializableProperty]
        [DescriptionAttribute("是否允许过滤，true-允许，false-不允许"), CategoryAttribute("其他设置"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 是否允许过滤 { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("点击列头，是否允许排序，true-允许，false-不允许"), CategoryAttribute("其他设置"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 是否允许排序 { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("右键点击列头，是否弹出菜单"), CategoryAttribute("其他设置"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 是显示列菜单 { get; set; }
    }
    #endregion

    #region DataLayOutControl属性

    /// <summary>
    /// DataLayoutAttribute属性
    /// </summary>
    public class DataLayoutAttribute
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        public List<ItemLayoutAttribute> ItemLayoutAttributeList { get; set; }
    }

    /// <summary>
    /// DataLayoutControl项属性
    /// </summary>
    public class ItemLayoutAttribute /*: IDXObjectWrapper*/
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BrowsableAttribute(false)]
        public string DatalayoutmxID { get; set; }

        /// <summary>
        /// 外键ID
        /// </summary>
        [BrowsableAttribute(false)]
        public string DatalayoutID { get; set; }
        
        /// <summary>
        /// 对应的数据源信息
        /// </summary>
        [Browsable(false)]
        public GridColumnInfo ColumnInfo { get; set; }
        [Browsable(false)]
        public MediGridView _MediGridView { get; set; }

        #region 标题属性
        [XtraSerializableProperty]
        [DescriptionAttribute("字段名称"), CategoryAttribute("标题设置"), ReadOnlyAttribute(true), BrowsableAttribute(true)]
        public string 字段名
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("字段名称"), CategoryAttribute("标题设置"), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public string 中文名称
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("是否在列中显示该字段"), CategoryAttribute("标题设置"), DefaultValueAttribute(true), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 显示标志
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("设置列头的字体大小"), CategoryAttribute("标题设置"), DefaultValueAttribute(9), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public float 字体大小
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("列头文本对齐方式：Default默认,Near左对齐,Center居中,Far右对齐"), CategoryAttribute("标题设置"), DefaultValueAttribute(HorizontalAlignment.Center), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public HorzAlignment 标题对齐方式
        { get; set; }

        #endregion

        #region 编辑项属性
        [XtraSerializableProperty]
        [DescriptionAttribute("按下回车跳转顺序"), CategoryAttribute("编辑项属性"), DefaultValueAttribute(0), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public int 跳转顺序
        { get; set; }

        //[XtraSerializableProperty]
        //[DescriptionAttribute("是否是必填项目，true-必填 False-非必填"), CategoryAttribute("编辑项属性"), DefaultValueAttribute(false), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //public bool 必输标志
        //{ get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("受保护后，不能修改该字段值"), CategoryAttribute("编辑项属性"), DefaultValueAttribute(false), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public bool 保护标志
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("初始值"), CategoryAttribute("编辑项属性"), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public string 初始值
        { get; set; }


        [XtraSerializableProperty]
        [DescriptionAttribute("设置控件的输入法编辑器 (IME) 模式"), CategoryAttribute("编辑项属性"), DefaultValueAttribute(HorizontalAlignment.Center), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        public ImeMode 输入法模式
        { get; set; }
        #endregion       

        #region 有效性验证
        [XtraSerializableProperty]
        [DescriptionAttribute("支持标准的正则表达，根据表达式检查数据有效性"), CategoryAttribute("有效性验证"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 有效性检查
        { get; set; }

        [XtraSerializableProperty]
        [DescriptionAttribute("正则表达式说明"), CategoryAttribute("有效性验证"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        [Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public string 有效性说明
        { get; set; }

        #endregion      

        //#region 非空表达式
        //[DescriptionAttribute("符合特定条件非空判断，支持表达式"), CategoryAttribute("非空表达式"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //[DXCategory(CategoryName.Data), XtraSerializableProperty(),
        //Editor(typeof(ExpressionEditorBaseEx), typeof(System.Drawing.Design.UITypeEditor))
        //]
        //[DevExpressXtraGridLocalizedDescriptionAttributeEx("GridColumnUnboundExpression")]
        //public string 非空表达式
        //{ get; set; }

        //[XtraSerializableProperty]
        //[DescriptionAttribute("符合特定条件非空判断，支持表达式"), CategoryAttribute("非空表达式"), DefaultValueAttribute(""), ReadOnlyAttribute(false), BrowsableAttribute(true)]
        //[Editor(typeof(Editor), typeof(System.Drawing.Design.UITypeEditor))]
        //public string 非空表达式说明
        //{ get; set; }

        ///// <summary>
        ///// 设置字段列表
        ///// </summary>
        //[BrowsableAttribute(false)]
        //public FieldItems _objectWrapper = null;
        //[BrowsableAttribute(false)]
        //public object SourceObject
        //{
        //    get
        //    {
        //        object[] objectWrapper = new object[1];

        //        objectWrapper[0] = _objectWrapper;

        //        return objectWrapper;
        //    }
        //}
        //#endregion
    }

    #endregion

    #region 文本扩展控件
    /// <summary>
    /// 文本控件
    /// </summary>    
    public class EditorControl : UserControl
    {
        public EditorControl(string sValue)
        {
            this.textBox1 = new TextBox();
            this.textBox1.Name = "textBox1";
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Multiline = true;
            this.textBox1.LostFocus += TextBox1_LostFocus;
            this.textBox1.Text = sValue;

            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Name = "EditorControl";
            this.Size = new System.Drawing.Size(210, 64);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            result = this.textBox1.Text;
        }

        public string result = "";
        private TextBox textBox1;
    }

    /// <summary>
    /// 
    /// </summary>    
    public class Editor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // 编辑属性值时，在右侧显示...更多按钮  
            return UITypeEditorEditStyle.Modal;//DropDown;//.Modal;
        }
        protected virtual ExpressionEditorForm CreateForm(object instance, IDesignerHost designerHost, object value)
        {
            if (instance is StyleFormatConditionBase) return new ConditionExpressionEditorForm(instance, designerHost);
            if (instance is FormatConditionRuleBase) return new FormatRuleExpressionEditorForm(instance, designerHost, value);
            if (value != null)
            {
                if (instance is GridColumn)
                {
                    (instance as GridColumn).UnboundExpression = value.ToString();
                }
                else
                {
                    if (instance is GridColumnInfo)
                    {
                        (instance as GridColumnInfo).UnboundExpression = value.ToString();
                    }
                }
            }
            else
            {
                if (instance is GridColumn)
                {
                    (instance as GridColumn).UnboundExpression = "";
                }
                else
                {
                    if (instance is GridColumnInfo)
                    {
                        (instance as GridColumnInfo).UnboundExpression = "";
                    }
                }

            }
            return new UnboundColumnExpressionEditorForm(instance, designerHost);

            //return null;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            var edSvc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (edSvc != null)
            {
                string sValue = "";
                if (value != null)
                    sValue = value.ToString();

                var popedControl = new EditorControl(sValue);
                // 还有ShowDialog这种方式，可以弹出一个窗体来进行编辑  
                edSvc.DropDownControl(popedControl);

                value = popedControl.result;

            }
            return base.EditValue(context, provider, value);
        }
    }

    #endregion

    #region 表达式基类

    public class DXObjectWrapperEx
    {
        public static object GetInstance(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null) return null;
            object val = context.Instance;
            IDXObjectWrapper wrapper = val as IDXObjectWrapper;
            if (wrapper != null) val = wrapper.SourceObject;
            if (val is Array)
            {
                Array res = (Array)val;
                if (res.Length > 0)
                {
                    val = res.GetValue(0);
                    wrapper = val as IDXObjectWrapper;
                    if (wrapper != null) return wrapper.SourceObject;
                }
            }
            return val;
        }
    }

    /// <summary>
    /// 表达式基类
    /// </summary>    
    public class ExpressionEditorBaseEx : UITypeEditor
    {
        protected virtual ExpressionEditorForm CreateForm(object instance, IDesignerHost designerHost, object value)
        {
            //if (instance is StyleFormatConditionBase) return new ConditionExpressionEditorForm(instance, designerHost);
            //if (instance is FormatConditionRuleBase) return new FormatRuleExpressionEditorForm(instance, designerHost, value);
            //if (value != null)
            //{
            //    if (instance is GridColumn)
            //    {
            //        (instance as GridColumn).UnboundExpression = value.ToString();
            //    }
            //    else
            //    {
            //        if(instance is GridColumnInfo)
            //        {
            //            (instance as GridColumnInfo).UnboundExpression= value.ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    if (instance is GridColumn)
            //    {
            //        (instance as GridColumn).UnboundExpression = "";
            //    }
            //    else
            //    {
            //        if (instance is GridColumnInfo)
            //        {
            //            (instance as GridColumnInfo).UnboundExpression = "";
            //        }
            //    }

            //}
            // return new UnboundColumnExpressionEditorForm(instance, designerHost);

            return null;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null) return null;

            //if (!(context.Instance is ItemLayoutAttribute)) return null;

            //IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            //if (edSvc != null)
            //{
            //    IDesignerHost designerHost = provider.GetService(typeof(IDesignerHost)) as IDesignerHost;
            //    using (ExpressionEditorForm form = CreateForm(DXObjectWrapperEx.GetInstance(context), designerHost, value))
            //    {
            //        if (edSvc.ShowDialog(form) == DialogResult.OK)
            //        {
            //            return form.Expression;
            //        }
            //    }
            //}
            if ((context.Instance is CellAttribute))
            {

                GridFormatRule condition = new GridFormatRule();
                condition.Rule = new FormatConditionRuleExpression();
                (context.Instance as CellAttribute)._MediGridView.FormatRules.Add(condition);
                FormatConditionRuleExpression rule = condition.Rule as FormatConditionRuleExpression;


                using (ExpressionEditorForm form = new FormatRuleExpressionEditorForm(rule, null,value))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        return form.Expression;
                    }
                }
            }
            
            if ((context.Instance is ItemLayoutAttribute))
            {
                GridFormatRule condition = new GridFormatRule();
                condition.Rule = new FormatConditionRuleExpression();
                (context.Instance as ItemLayoutAttribute)._MediGridView.FormatRules.Add(condition);
                FormatConditionRuleExpression rule = condition.Rule as FormatConditionRuleExpression;
                using (ExpressionEditorForm form = new FormatRuleExpressionEditorForm(rule, null, value))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        return form.Expression;
                    }
                }
            }    
            return base.EditValue(context, provider, value);
        }
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }

    #endregion


    [AttributeUsage(AttributeTargets.All)]
    class DevExpressXtraGridLocalizedDescriptionAttributeEx : DescriptionAttribute
    {
        static ResourceManager rm;
        bool loaded;
        public DevExpressXtraGridLocalizedDescriptionAttributeEx(string name)
            : base(name)
        {
        }
        public override string Description
        {
            get
            {
                if (!loaded)
                {
                    loaded = true;
                    if (rm == null)
                    {
                        lock (typeof(DevExpressXtraGridLocalizedDescriptionAttributeEx))
                        {
                            if (rm == null)
                                rm = new ResourceManager("DevExpress.XtraGrid.Descriptions", typeof(DevExpressXtraGridLocalizedDescriptionAttributeEx).Assembly);
                        }
                    }
                    DescriptionValue = rm.GetString(base.Description);
                }
                return base.Description;
            }
        }
    }
}
