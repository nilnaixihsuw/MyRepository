using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.HIS.GongYong;
using Mediinfo.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class NewFrmDataLayOutSet : MediDialog
    {
        /// <summary>
        /// 触发窗体布局
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sControlName"></param>
        /// <param name="sNameSpace"></param>
        /// <param name="triggerControl"></param>
        public NewFrmDataLayOutSet(string sFormName, string sControlName, string sNameSpace, MediGridView triggerControl)
        {
            InitializeComponent();
            _FormName = sFormName;
            _ControlName = sControlName;
            _NameSpace = sNameSpace;
            _GridView = triggerControl;
            this.mediDataLayOutTitleBar.LabelText = string.Format("{0}.{1}", _FormName, _ControlName);
            _GYDataLayoutService = new GYDataLayoutService();
          
        }

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

        #endregion 属性

        public NewFrmDataLayOutSet()

        {
            InitializeComponent();
        }

        public NewFrmDataLayOutSet(string sFormName, string sControlName, string sNameSpace, MediDataLayoutControl control)
        {
            InitializeComponent();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            _FormName = sFormName;
            _ControlName = sControlName;
            _MediDataLayoutControl = control;
            _NameSpace = sNameSpace;

            //this.labelName.Text = string.Format("{0}.{1}", _FormName, _ControlName);

            //mediTabControl1.TabPages[0].Text = "项属性";
            //mediTabControl1.TabPages[1].PageVisible = false;
            //mediTabControl1.TabPages[2].PageVisible = false;

            //this.mediGridControlSource.DataSource = _MediDataLayoutControl.DataSource;

            _GYDataLayoutService = new GYDataLayoutService();

            //stopwatch.Stop();
            //Console.WriteLine("ElapsedMilliseconds ---1  " + stopwatch.ElapsedMilliseconds);
        }
        /// <summary>
        /// 列固定类
        /// </summary>
        public class ColumnFix
        {
            /// <summary>
            /// 列固定方式名称
            /// </summary>
            public DevExpress.XtraGrid.Columns.FixedStyle ColumnFixName { get; set; }
            /// <summary>
            /// 列固定方式代码
            /// </summary>
            public int ColumnFixCode { get; set; }
        }


        /// <summary>
        /// 列排序方式类
        /// </summary>
        public class ColumnSort
        {
            /// <summary>
            /// 列固定方式名称
            /// </summary>
            public DevExpress.Data.ColumnSortOrder ColumnSortName { get; set; }
            /// <summary>
            /// 列固定方式代码
            /// </summary>
            public int ColumnSortCode { get; set; }
        }



        /// <summary>
        /// 单元格文本对齐方式类
        /// </summary>
        public class CellTextHAlignment
        {
            /// <summary>
            /// 列固定方式名称
            /// </summary>
            public DevExpress.Utils.HorzAlignment CellTextHAlignmentName { get; set; }
            /// <summary>
            /// 列固定方式代码
            /// </summary>
            public int CellTextHAlignmentCode { get; set; }
        }


        /// <summary>
        /// 字符串格式类
        /// </summary>
        public class StringFormat
        {
            /// <summary>
            /// 字符串格式名称
            /// </summary>
            public DevExpress.Utils.FormatType StringFormatName { get; set; }
            /// <summary>
            /// 字符串格式代码
            /// </summary>
            public int StringFormatCode { get; set; }
        }

        /// <summary>
        /// 列头文本对其方式类
        /// </summary>
        public class ColumnHeaderHAlignment
        {
            /// <summary>
            /// 字符串格式名称
            /// </summary>
            public DevExpress.Utils.HorzAlignment ColumnHeaderHAlignmentName { get; set; }
            /// <summary>
            /// 字符串格式代码
            /// </summary>
            public int ColumnHeaderHAlignmentCode { get; set; }
        }

        //输入法模式
        public class ImeMode
        {
            /// <summary>
            /// 输入法名称
            /// </summary>
             public System.Windows.Forms.ImeMode ImeModeName { get; set; }
            /// <summary>
            /// 输入法代码
            /// </summary>
             public int ImeModeCode { get; set; }
        }
            
        /// 获取数据布局信息从数据库
        /// </summary>
        private void GetDataLayoutForDB()
        {
            var ret = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                _EDataLayout1 = ret.Return.DataLayout1;
                _EDataLayout2 = ret.Return.DataLayout2;

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
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFrmDataLayOutSet_Load(object sender, EventArgs e)
        {
            InitialLookUpeditControlBindDataSource();


            //根据当前gridview来获取当前页面的布局信息



            //1.获取行的属性信息---行号--行字体大小---行背景色==行背景色描述


            //2.获取列属性信息 ----字段名--中文名称--显示标志--列宽度--列头固定--头字体大小--头标题对齐方式==跳转顺序--保护标志--初始值--单元格字体大小--单元格文本对其方式--输入法模式--有效性检查--有效性说明--显示格式--显示格式类型


            //3.其他属性--是否显示分组面板--是否允许过滤--是否允许排序--是否显示列菜单--特定排序设置


            MediGriviewAttribute gridViewAttribute = null;
            GetDataLayoutForDB();
            if (_EDataLayout1 != null && !string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID) && _EDataLayout2 != null && _EDataLayout2.Count() > 0)
            {
                gridViewAttribute = LoadDBGridViewAttribute();
            }
            else
            {
                gridViewAttribute = LoadDoubleObserverGridViewAttribute();
                if (gridViewAttribute == null)
                    return;


            }

            //绑定左边面板所有列的名称和对应的字段名
            // this.columnGridControl.DataSource = gridViewAttribute.mediGridviewColumnList;


            List<MediGriviewRowAttribute> mediGriviewRowAttribute = new List<HIS.Controls.MediGriviewRowAttribute>();
            mediGriviewRowAttribute.Add(gridViewAttribute.mediGriviewRowAttribute);
            //绑定行属性
            //this.rowAttributeGridControl.DataSource = mediGriviewRowAttribute;
            //绑定列属性

            
            this.columnAttributeGridControl.DataSource = gridViewAttribute.mediGridviewColumnList;


            List<MediGridviewOtherAttribute> mediGridviewOtherAttributeList = new List<HIS.Controls.MediGridviewOtherAttribute>();
            mediGridviewOtherAttributeList.Add(gridViewAttribute.mediGridviewOtherAttribute);
            //绑定其他属性
            //this.otherAttributeGridControl.DataSource = mediGridviewOtherAttributeList;





            //this.mediPropertyGrid1.SelectedObject = new TableViewAttribute();


            ////从数据库加载布局表
            //GetDataLayoutForDB();
            ////判断当前触发布局窗体的gridview
            //if (_GridView != null)
            //{

            //    if (_EDataLayout1 != null && _EDataLayout2 != null)//如果从数据库未获取到自定义布局则获取当前窗体原始的布局数据
            //    {
            //        MediGriviewAttribute gridViewAttribute = LoadDBGridViewAttribute();
            //        if (gridViewAttribute == null)
            //            return;
            //        //绑定左边面板所有列的名称和对应的字段名
            //        this.columnGridControl.DataSource = gridViewAttribute.mediGridviewColumnList;


            //        List<MediGriviewRowAttribute> mediGriviewRowAttribute = new List<HIS.Controls.MediGriviewRowAttribute>();
            //        mediGriviewRowAttribute.Add(gridViewAttribute.mediGriviewRowAttribute);
            //        //绑定行属性
            //        this.rowAttributeGridControl.DataSource = mediGriviewRowAttribute;
            //        //绑定列属性
            //        this.columnAttributeGridControl.DataSource = gridViewAttribute.mediGridviewColumnList;


            //        List<MediGridviewOtherAttribute> mediGridviewOtherAttributeList = new List<HIS.Controls.MediGridviewOtherAttribute>();
            //        mediGridviewOtherAttributeList.Add(gridViewAttribute.mediGridviewOtherAttribute);
            //        //绑定其他属性
            //        this.otherAttributeGridControl.DataSource = mediGridviewOtherAttributeList;
            //    }
            //    else
            //    {

            //    }
            //}

        }

        private  void InitialLookUpeditControlBindDataSource()
        {
            //列头固定方式数据源
            List<ColumnFix> columnfixed = new List<HIS.Controls.NewFrmDataLayOutSet.ColumnFix>();
            columnfixed.Add(new ColumnFix() { ColumnFixName = FixedStyle.None, ColumnFixCode = 0 });
            columnfixed.Add(new ColumnFix() { ColumnFixName = FixedStyle.Left,ColumnFixCode = 1} );
            columnfixed.Add(new ColumnFix() { ColumnFixName = FixedStyle.Right, ColumnFixCode = 2 });
            this.rpiMediColumFixedGridLookUpEdit.DataSource = columnfixed;
            rpiMediColumFixedGridLookUpEdit.DisplayMember = "ColumnFixName";
            rpiMediColumFixedGridLookUpEdit.ValueMember = "ColumnFixCode";
            rpiMediColumFixedGridLookUpEdit.View.OptionsView.ShowIndicator = false;

            //列头对齐方式
            List<ColumnHeaderHAlignment> columnHeaderHAlignment = new List<HIS.Controls.NewFrmDataLayOutSet.ColumnHeaderHAlignment>();
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Default, ColumnHeaderHAlignmentCode = 0 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Near, ColumnHeaderHAlignmentCode = 1 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Center, ColumnHeaderHAlignmentCode = 2 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Far, ColumnHeaderHAlignmentCode = 3 });
            this.rpiMediGridHeadHAlignmentLookUpEdit.DataSource = columnHeaderHAlignment;
            rpiMediGridHeadHAlignmentLookUpEdit.DisplayMember = "ColumnHeaderHAlignmentName";
            rpiMediGridHeadHAlignmentLookUpEdit.ValueMember = "ColumnHeaderHAlignmentCode";
            rpiMediGridHeadHAlignmentLookUpEdit.View.OptionsView.ShowIndicator = false;

            //单元格对齐方式
            List<CellTextHAlignment> cellTextHAlignment = new List<HIS.Controls.NewFrmDataLayOutSet.CellTextHAlignment>();
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = HorzAlignment.Default, CellTextHAlignmentCode = 0 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = HorzAlignment.Near, CellTextHAlignmentCode = 1 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = HorzAlignment.Center, CellTextHAlignmentCode = 2 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = HorzAlignment.Far, CellTextHAlignmentCode = 3 });
            this.rpiMediGridCellHAlignmentLookUpEdit.DataSource = cellTextHAlignment;
            rpiMediGridCellHAlignmentLookUpEdit.DisplayMember = "CellTextHAlignmentName";
            rpiMediGridCellHAlignmentLookUpEdit.ValueMember = "CellTextHAlignmentCode";
            rpiMediGridCellHAlignmentLookUpEdit.View.OptionsView.ShowIndicator = false;

            //显示格式类型

            List<StringFormat> stringFormat = new List<HIS.Controls.NewFrmDataLayOutSet.StringFormat>();
            stringFormat.Add(new StringFormat() { StringFormatName = FormatType.None, StringFormatCode = 0 });
            stringFormat.Add(new StringFormat() { StringFormatName = FormatType.Numeric, StringFormatCode = 1 });
            stringFormat.Add(new StringFormat() { StringFormatName = FormatType.DateTime, StringFormatCode = 2 });
            stringFormat.Add(new StringFormat() { StringFormatName = FormatType.Custom, StringFormatCode = 3 });
            this.rpiMediGridFormatTypeLookUpEdit.DataSource = stringFormat;
            rpiMediGridFormatTypeLookUpEdit.DisplayMember = "StringFormatName";
            rpiMediGridFormatTypeLookUpEdit.ValueMember = "StringFormatCode";
            rpiMediGridFormatTypeLookUpEdit.View.OptionsView.ShowIndicator = false;

            //输入法模式
            List<HIS.Controls.NewFrmDataLayOutSet.ImeMode> imeMode = new List<HIS.Controls.NewFrmDataLayOutSet.ImeMode>();
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Inherit, ImeModeCode = -1 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.NoControl, ImeModeCode = 0 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.On, ImeModeCode = 1 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Off, ImeModeCode = 2 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Disable, ImeModeCode = 3 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Hiragana, ImeModeCode = 4 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Katakana, ImeModeCode = 5 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.KatakanaHalf, ImeModeCode = 6 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.AlphaFull, ImeModeCode = 7 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Alpha, ImeModeCode = 8 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.HangulFull, ImeModeCode = 9 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Hangul, ImeModeCode = 10 });


            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Close, ImeModeCode = 11 });

            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.OnHalf, ImeModeCode = 12 });
            this.rpiMediGridImodeLookUpEdit.DataSource = imeMode;
            rpiMediGridImodeLookUpEdit.DisplayMember = "ImeModeName";
            rpiMediGridImodeLookUpEdit.ValueMember = "ImeModeCode";
            rpiMediGridImodeLookUpEdit.View.OptionsView.ShowIndicator = false;

        }

        /// <summary>
        /// 表格属性类
        /// </summary>
        public class TableViewAttribute
        {
            /// <summary>
            /// 行号
            /// </summary>
            [CategoryAttribute("行属性"), DescriptionAttribute("是否显示行号")]
            public bool RowLine { get; set; }
            /// <summary>
            /// 行字体
            /// </summary>
            [CategoryAttribute("行属性"), DescriptionAttribute("行字体")]
            public int? RowFont { get; set; }
            /// <summary>
            ///行背景色
            /// </summary>
            [Browsable(true)]
            [Editor(typeof(ExpressionCustomControlEditor), typeof(UITypeEditor))]
            [CategoryAttribute("行属性"), DescriptionAttribute("行背景色")]
            public string RowBackColor { get; set; }
            /// <summary>
            /// 背景色描述
            /// </summary>
            [CategoryAttribute("行属性"), DescriptionAttribute("背景色描述")]
            public string RowBackColorDescription { get; set; }

            /// <summary>
            /// 排序列设置
            /// </summary>
            [CategoryAttribute("其他属性"), DescriptionAttribute("排序列设置")]
            [Browsable(true)]

            public string SortColumnSet { get; set; }
            /// <summary>
            /// 是否允许显示分组面板
            /// </summary>
            [CategoryAttribute("其他属性"), DescriptionAttribute("是否允许显示分组面板")]
            public bool IsVisibleGroupPanel { get; set; }
            /// <summary>
            /// 是否允许排序
            /// </summary>
            [CategoryAttribute("其他属性"), DescriptionAttribute("是否允许排序")]
            public bool IsAllowSort { get; set; }

            /// <summary>
            /// 是否允许过滤
            /// </summary>
            [CategoryAttribute("其他属性"), DescriptionAttribute("是否允许过滤")]
            public bool IsAllowFilter { get; set; }
            /// <summary>
            /// 是否显示菜单
            /// </summary>
            [CategoryAttribute("其他属性"), DescriptionAttribute("是否显示菜单")]
            public bool IsAllowVisibleMenu { get; set; }
         

            /// <summary>
            /// 列排序类型
            /// </summary>
            public enum ColumnSortType
            {
                /// <summary>
                /// 不排序
                /// </summary>
                None = 0,
                /// <summary>
                /// 升序
                /// </summary>
                Ascending = 1,
                /// <summary>
                /// 降序
                /// </summary>
                Descending = 2
            }

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
        /// 获取观察者的相关属性值
        /// </summary>
        /// <returns></returns>
        private MediGriviewAttribute LoadDoubleObserverGridViewAttribute()
        {
            if (_GridView!=null)
            {
                if (_GridView is MediGridView)
                {
                    MediGriviewAttribute mediGriviewAttribute = new MediGriviewAttribute();

                    MediGridView observerMediGridview = (MediGridView)_GridView;
                    MediGriviewRowAttribute mediGriviewRowAttribute = new MediGriviewRowAttribute(); ;
                    
                  
                    //列属性
                    List<MediGridviewColumn> mediGridviewColumnList = new List<MediGridviewColumn>();
                   
                    for (int i = 0; i < observerMediGridview.Columns.Count; i++)
                    {
                        MediGridviewColumn nediGridviewColumn = new MediGridviewColumn();
                        //nediGridviewColumn.DATALAYOUTID = observerMediGridview.Columns[i].
                        nediGridviewColumn.FIELDNAME = observerMediGridview.Columns[i].FieldName;
                        nediGridviewColumn.CAPTION = observerMediGridview.Columns[i].Caption;
                        nediGridviewColumn.VISIBLE = observerMediGridview.Columns[i].Visible;
                        nediGridviewColumn.WIDTH = observerMediGridview.Columns[i].Width;
                        nediGridviewColumn.FIXED = (int)observerMediGridview.Columns[i].Fixed;
                        nediGridviewColumn.HEADERFONTSIZE = observerMediGridview.Columns[i].AppearanceHeader.Font.Size;
                        nediGridviewColumn.HEADERHALIGNMENT = (int)observerMediGridview.Columns[i].AppearanceHeader.TextOptions.HAlignment;
                        nediGridviewColumn.TABINDEX = 0;//跳转顺序----待定
                        nediGridviewColumn.READONLY = observerMediGridview.Columns[i].ReadOnly;
                        nediGridviewColumn.DEFAULTVALUE = "";//默认值待定
                        nediGridviewColumn.CELLFONTSIZE = observerMediGridview.Columns[i].AppearanceCell.Font.Size;
                        nediGridviewColumn.CELLHALIGNMENT = (int)observerMediGridview.Columns[i].AppearanceCell.TextOptions.HAlignment;
                        nediGridviewColumn.IMEMODE = "";//输入法模式待定
                        nediGridviewColumn.VALIDATEEXPRISSION = "";//表达式待定
                        nediGridviewColumn.VALIDATEDESCRIBE = "";//有效性说明
                        nediGridviewColumn.FORMATSTRING = "";//显示格式
                        nediGridviewColumn.FORMATTYPE = 0;//显示格式类型
                        nediGridviewColumn.SORTNO = observerMediGridview.Columns[i].VisibleIndex;
                        mediGridviewColumnList.Add(nediGridviewColumn);
                    }

                  
                    mediGriviewAttribute.mediGridviewColumnList = mediGridviewColumnList;
                    this.mediPropertyGrid1.SelectedObject = new TableViewAttribute();

                    return mediGriviewAttribute;
                }
                return null;

            }
            return null;
        }



        /// <summary>
        /// 加载数据库的GridView属性
        /// </summary>
        /// <returns></returns>
        private MediGriviewAttribute LoadDBGridViewAttribute()
        {
            if (_EDataLayout1 == null|| _EDataLayout2 == null || _EDataLayout2.Count == 0) return null;

            //propertygrid赋值

            this.mediPropertyGrid1.SelectedObject = new TableViewAttribute()
            {
                RowLine = Convert.ToBoolean(_EDataLayout1.LINENUMBER),
                RowFont = _EDataLayout1.ROWFONTSIZE,
                RowBackColor = _EDataLayout1.ROWBACKCOLOREXPRESSION,
                RowBackColorDescription = _EDataLayout1.ROWBACKCOLORDESCRIBE,
                SortColumnSet = _EDataLayout1.ORDERBYFIELD,
                IsVisibleGroupPanel = Convert.ToBoolean(_EDataLayout1.SHOWGROUPPANEL),
                IsAllowSort = Convert.ToBoolean(_EDataLayout1.ALLOWSORT),
                IsAllowFilter = Convert.ToBoolean(_EDataLayout1.ALLOWFILTER),
                IsAllowVisibleMenu = Convert.ToBoolean(_EDataLayout1.ENABLECOLUMNMENU)

            };


            MediGriviewAttribute mediGriviewAttribute = new MediGriviewAttribute();
            mediGriviewAttribute.NameSpace = _EDataLayout1.NAMESPACE;
            mediGriviewAttribute.YingYongID = _EDataLayout1.YINGYONGID;
            mediGriviewAttribute.ControlName = _EDataLayout1.CONTROLNAME;
            mediGriviewAttribute.DatalayoutID = _EDataLayout1.DATALAYOUTID;
            mediGriviewAttribute.FormName = _EDataLayout1.FORMNAME;
            mediGriviewAttribute.DatalayoutID = _EDataLayout1.DATALAYOUTID;

            List<MediGridviewColumn> gridViewColumnAttributList = new List<MediGridviewColumn>();
            int index = 1;
            //遍历数据集
            _EDataLayout2.OrderBy(b => b.VISIBLE).OrderBy(o => o.SORTNO).ToList().ForEach(o =>
            {
                #region 遍历

                //新定义的列属性
                MediGridviewColumn mediGridviewColumnAttribute = new MediGridviewColumn();

                mediGridviewColumnAttribute.DATALAYOUTID = o.DATALAYOUTID;
                mediGridviewColumnAttribute.DATALAYOUTMXID = o.DATALAYOUTMXID;
                mediGridviewColumnAttribute.CAPTION = o.CAPTION;
                mediGridviewColumnAttribute.FIXED = o.FIXED.ToInt(0);
                mediGridviewColumnAttribute.WIDTH = o.WIDTH.ToInt(100);
                mediGridviewColumnAttribute.HEADERFONTSIZE = o.HEADERFONTSIZE.ToInt(10);
                mediGridviewColumnAttribute.HEADERHALIGNMENT = o.HEADERHALIGNMENT.ToInt(0);
                mediGridviewColumnAttribute.FIELDNAME = o.FIELDNAME;
                mediGridviewColumnAttribute.VISIBLE = o.VISIBLE.ToInt(1) == 0 ? false : true;
                mediGridviewColumnAttribute.READONLY = o.READONLY.ToInt(0).ToBool();
                mediGridviewColumnAttribute.DEFAULTVALUE = o.DEFAULTVALUE;
                mediGridviewColumnAttribute.CELLFONTSIZE = o.CELLFONTSIZE.ToInt(9);
                mediGridviewColumnAttribute.CELLHALIGNMENT = o.CELLHALIGNMENT.ToInt(0);
                mediGridviewColumnAttribute.FORMATSTRING = o.FORMATSTRING;
                mediGridviewColumnAttribute.FORMATTYPE = o.FORMATTYPE;
                mediGridviewColumnAttribute.VALIDATEEXPRISSION = o.VALIDATEEXPRISSION;
                mediGridviewColumnAttribute.VALIDATEDESCRIBE = o.VALIDATEDESCRIBE;
                mediGridviewColumnAttribute.TABINDEX = o.TABINDEX.ToInt(index);
                mediGridviewColumnAttribute.IMEMODE = o.IMEMODE;
                mediGridviewColumnAttribute.READONLY = o.READONLY.ToInt(0).ToBool();
                mediGridviewColumnAttribute.DEFAULTVALUE = o.DEFAULTVALUE;
                mediGridviewColumnAttribute.CELLFONTSIZE = o.CELLFONTSIZE.ToInt(9);
                mediGridviewColumnAttribute.CELLHALIGNMENT = o.CELLHALIGNMENT.ToInt(0);             
                mediGridviewColumnAttribute.FORMATSTRING = o.FORMATSTRING;
                mediGridviewColumnAttribute.FORMATTYPE = o.FORMATTYPE;
                mediGridviewColumnAttribute.VALIDATEEXPRISSION = o.VALIDATEEXPRISSION;
                mediGridviewColumnAttribute.VALIDATEDESCRIBE = o.VALIDATEDESCRIBE;             
                mediGridviewColumnAttribute.TABINDEX = o.TABINDEX.ToInt(index);
                mediGridviewColumnAttribute.IMEMODE = o.IMEMODE;
                mediGridviewColumnAttribute.SORTNO = o.SORTNO;
                gridViewColumnAttributList.Add(mediGridviewColumnAttribute);
                index++;

                #endregion 遍历
            });
            //GridView属性
            MediGriviewRowAttribute mediGridViewRowAttribute = new MediGriviewRowAttribute();
            mediGridViewRowAttribute.DATALAYOUTID = _EDataLayout1.DATALAYOUTID;
            mediGridViewRowAttribute.LINENUMBER = _EDataLayout1.LINENUMBER;
            mediGridViewRowAttribute.ROWFONTSIZE = _EDataLayout1.ROWFONTSIZE.ToInt(9);
            mediGridViewRowAttribute.ROWBACKCOLOREXPRESSION = _EDataLayout1.ROWBACKCOLOREXPRESSION;
            mediGridViewRowAttribute.ROWBACKCOLORDESCRIBE = _EDataLayout1.ROWBACKCOLORDESCRIBE;

            MediGridviewOtherAttribute mediGridViewOtherAttribute = new MediGridviewOtherAttribute();
            mediGridViewOtherAttribute.DATALAYOUTID = _EDataLayout1.DATALAYOUTID;
            mediGridViewOtherAttribute.ALLOWSORT = _EDataLayout1.ALLOWSORT.ToInt(1) == 0 ? false : true;
            mediGridViewOtherAttribute.ALLOWFILTER = _EDataLayout1.ALLOWFILTER.ToInt(1) == 0 ? false : true;
            mediGridViewOtherAttribute.SHOWGROUPPANEL = _EDataLayout1.SHOWGROUPPANEL.ToInt(0) == 0 ? false : true; ;
            mediGridViewOtherAttribute.ENABLECOLUMNMENU = _EDataLayout1.ENABLECOLUMNMENU.ToInt(1) == 0 ? false : true; ;
            mediGridViewOtherAttribute.ORDERBYFIELD = _EDataLayout1.ORDERBYFIELD;
          


            mediGriviewAttribute.mediGridviewColumnList = gridViewColumnAttributList;

            mediGriviewAttribute.mediGriviewRowAttribute = mediGridViewRowAttribute;
            mediGriviewAttribute.mediGridviewOtherAttribute = mediGridViewOtherAttribute;
      
            return mediGriviewAttribute;
        }

        /// <summary>
        /// 复位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonReset_Click(object sender, EventArgs e)
        {

            if (_EDataLayout1!=null&& _EDataLayout2!=null)
            {
                DTOChaXunDuiXiangToEntity(ref _EDataLayout1, ref _EDataLayout2);
                if (!string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID))
                {
                    _EDataLayout1.SetState(DTOState.Delete);
                    Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);

                    if (result.Return)
                        MediMsgBox.Success("重置成功！");
                    else
                        MediMsgBox.Failure("重置失败！");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MediMsgBox.Warn("未启用自定义布局！");
                }


            }
            else
            {
                MediMsgBox.Warn("未启用自定义布局！");
            }

            if (_GridView != null)
            {
                Form currentFrm = _GridView.GridControl.FindForm();
                if (currentFrm != null && currentFrm is MediFormWithQX)
                {
                    MediFormWithQX mediFormWithQX = (MediFormWithQX)currentFrm;
                    if (mediFormWithQX.RefreshCurrentResetFormFun != null)
                    {
                        mediFormWithQX.RefreshCurrentResetFormFun();
                    }
                }


            }

        }
        /// <summary>
        /// DTO对象转化为实体对象
        /// </summary>
        /// <param name="eDataLayout1"></param>
        /// <param name="eDatalayout2List"></param>
        private void DTOChaXunDuiXiangToEntity(ref E_GY_DATALAYOUT1 eDataLayout1, ref List<E_GY_DATALAYOUT2> eDatalayout2List)
        {
            
            //把行数据转换成对应的行属性集合
            //命名空间
            eDataLayout1.NAMESPACE = _NameSpace;
            eDataLayout1.FORMNAME = _FormName;
            eDataLayout1.CONTROLNAME = _ControlName;
            eDataLayout1.YINGYONGID = (radioGroupLevel.SelectedIndex == 0 ? HISClientHelper.YINGYONGID : (radioGroupLevel.SelectedIndex == 1 ? HISClientHelper.XITONGID : "00"));
            //行背景色描述
            eDataLayout1.ROWBACKCOLORDESCRIBE = ((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).RowBackColorDescription;

            eDataLayout1.ROWBACKCOLOREXPRESSION = ((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).RowBackColor; 

            eDataLayout1.ROWFONTSIZE = ((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).RowFont;
            eDataLayout1.LINENUMBER = ((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).RowLine == true ? 1 : 0;
            if (((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).SortColumnSet!=null)
                eDataLayout1.ORDERBYFIELD = Convert.ToString(((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).SortColumnSet).ToString();

            eDataLayout1.SHOWGROUPPANEL = Convert.ToInt32(((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).IsVisibleGroupPanel);
            eDataLayout1.ALLOWFILTER = Convert.ToInt32(((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).IsAllowFilter);
            eDataLayout1.ALLOWSORT = Convert.ToInt32(((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).IsAllowSort);
            eDataLayout1.ENABLECOLUMNMENU = Convert.ToInt32(((TableViewAttribute)this.mediPropertyGrid1.SelectedObject).IsAllowVisibleMenu);

            //把列数据转化成对应的列属性集合
        
            if (string.IsNullOrEmpty(eDataLayout1.DATALAYOUTID))
            {
                eDatalayout2List.Clear();
                foreach (MediGridviewColumn item in ((List<MediGridviewColumn>)columnAttributeGridControl.DataSource))
                {

                  
                    E_GY_DATALAYOUT2 egydatalay2 = new E_GY_DATALAYOUT2();
                    egydatalay2.DATALAYOUTID = item.DATALAYOUTID;
                    egydatalay2.DATALAYOUTMXID = item.DATALAYOUTMXID;
                    egydatalay2.CAPTION = item.CAPTION;
                    egydatalay2.CELLFONTSIZE = (int)item.CELLFONTSIZE;
                    egydatalay2.CELLHALIGNMENT = (int)item.CELLHALIGNMENT;
                    egydatalay2.FIELDNAME = item.FIELDNAME;
                    egydatalay2.FORMATSTRING = item.FORMATSTRING;
                    egydatalay2.FORMATTYPE = item.FORMATTYPE;
                    egydatalay2.FIXED = (int)item.FIXED;
                    egydatalay2.HEADERFONTSIZE = (int)item.HEADERFONTSIZE;
                    egydatalay2.HEADERHALIGNMENT = (int)item.HEADERHALIGNMENT;
                    egydatalay2.IMEMODE = item.IMEMODE;
                    egydatalay2.BACKCOLOREXPRISSION = item.BACKCOLOREXPRISSION;
                    egydatalay2.BACKCOLORDESCRIBE = item.BACKCOLORDESCRIBE;
                    egydatalay2.READONLY = Convert.ToInt32(item.READONLY);
                    egydatalay2.IMEMODE = item.IMEMODE;
                    egydatalay2.WIDTH = item.WIDTH;
                    egydatalay2.VISIBLE = Convert.ToInt32(item.VISIBLE);
                    egydatalay2.CELLFORECOLOREXPRISSION = item.CELLFORECOLOREXPRISSION;
                    egydatalay2.CELLFORECOLORDESCRIBE = item.CELLFORECOLORDESCRIBE;
                    egydatalay2.VALIDATEEXPRISSION = item.VALIDATEEXPRISSION;
                    egydatalay2.VALIDATEDESCRIBE = item.VALIDATEDESCRIBE;
                    egydatalay2.TABINDEX = item.TABINDEX;
                    egydatalay2.NONEMPTYEXPRESSION = item.NONEMPTYEXPRESSION;
                    egydatalay2.NONEMPTYEDESCRIBE = item.NONEMPTYEDESCRIBE;
                    egydatalay2.DEFAULTVALUE = item.DEFAULTVALUE;
                    egydatalay2.SORTNO = item.SORTNO;
                    




                    eDatalayout2List.Add(egydatalay2);
                }
            }else
            {


                List<MediGridviewColumn> medigridcolumnsList = (List<MediGridviewColumn>)columnAttributeGridControl.DataSource;
                eDatalayout2List.Clear();
                foreach (MediGridviewColumn item in medigridcolumnsList)
                        {
                                E_GY_DATALAYOUT2 egydatalay2 = new E_GY_DATALAYOUT2();
                                egydatalay2.SetTraceChange(true);
                                egydatalay2.DATALAYOUTID = item.DATALAYOUTID;
                                egydatalay2.DATALAYOUTMXID = item.DATALAYOUTMXID;
                                egydatalay2.CAPTION = item.CAPTION;
                                egydatalay2.CELLFONTSIZE = (int)item.CELLFONTSIZE;
                                egydatalay2.CELLHALIGNMENT = (int)item.CELLHALIGNMENT;
                                egydatalay2.FIELDNAME = item.FIELDNAME;
                                egydatalay2.FORMATSTRING = item.FORMATSTRING;
                                egydatalay2.FORMATTYPE = item.FORMATTYPE;
                                egydatalay2.FIXED = (int)item.FIXED;
                                egydatalay2.HEADERFONTSIZE = (int)item.HEADERFONTSIZE;
                                egydatalay2.HEADERHALIGNMENT = (int)item.HEADERHALIGNMENT;
                                egydatalay2.IMEMODE = item.IMEMODE;
                                egydatalay2.BACKCOLOREXPRISSION = item.BACKCOLOREXPRISSION;
                                egydatalay2.BACKCOLORDESCRIBE = item.BACKCOLORDESCRIBE;
                                egydatalay2.READONLY = Convert.ToInt32(item.READONLY);
                                egydatalay2.IMEMODE = item.IMEMODE;
                                egydatalay2.WIDTH = item.WIDTH;
                                egydatalay2.VISIBLE = Convert.ToInt32(item.VISIBLE);
                                egydatalay2.CELLFORECOLOREXPRISSION = item.CELLFORECOLOREXPRISSION;
                                egydatalay2.CELLFORECOLORDESCRIBE = item.CELLFORECOLORDESCRIBE;
                                egydatalay2.VALIDATEEXPRISSION = item.VALIDATEEXPRISSION;
                                egydatalay2.VALIDATEDESCRIBE = item.VALIDATEDESCRIBE;
                                egydatalay2.TABINDEX = item.TABINDEX;
                                egydatalay2.NONEMPTYEXPRESSION = item.NONEMPTYEXPRESSION;
                                egydatalay2.NONEMPTYEDESCRIBE = item.NONEMPTYEDESCRIBE;
                                egydatalay2.DEFAULTVALUE = item.DEFAULTVALUE;
                                egydatalay2.SORTNO = item.SORTNO;
                                eDatalayout2List.Add(egydatalay2);
                            
                               
                        



                    }
                
            }

            //把其他数据转化成对应的其他属性集合

            //eDataLayout1.SHOWGROUPPANEL = ((List<MediGridviewOtherAttribute>)otherAttributeGridControl.DataSource)[0].SHOWGROUPPANEL == false ? 0 : 1;

            //eDataLayout1.ENABLECOLUMNMENU = ((List<MediGridviewOtherAttribute>)otherAttributeGridControl.DataSource)[0].ENABLECOLUMNMENU == false ? 0 : 1;
            //eDataLayout1.ALLOWSORT = ((List<MediGridviewOtherAttribute>)otherAttributeGridControl.DataSource)[0].ALLOWSORT == false ? 0 : 1;
            //eDataLayout1.ALLOWFILTER = ((List<MediGridviewOtherAttribute>)otherAttributeGridControl.DataSource)[0].ALLOWFILTER == false ? 0 : 1;
            //eDataLayout1.ORDERBYFIELD = ((List<MediGridviewOtherAttribute>)otherAttributeGridControl.DataSource)[0].ORDERBYFIELD;
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSave_Click(object sender, EventArgs e)
        {
          
            
            
            Result<bool> result = null;
          
            if (_EDataLayout1!=null&&_EDataLayout2!=null)
            {


                DTOChaXunDuiXiangToEntity(ref _EDataLayout1, ref _EDataLayout2);
                if (!string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID))
                {
                    _EDataLayout1.SetState(DTOState.Delete);
                   _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);
                }
                //如果datalayout1 id为空则为新增否则为修改
                //如果复位则先判断id是否为空如果为空则不需要去数据库删除否则去数据库删除

                    _EDataLayout1.SetState(DTOState.New);

                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);

                if (result.Return)

                    MediMsgBox.Success(this, "保存成功！");
                else
                    MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
                //this.DialogResult = DialogResult.OK;
            }
            else
            {
                _EDataLayout1 = new E_GY_DATALAYOUT1();
                _EDataLayout2 = new List<E_GY_DATALAYOUT2>();
                //如果datalayout1 id为空则为新增否则为修改
                //如果复位则先判断id是否为空如果为空则不需要去数据库删除否则去数据库删除
                DTOChaXunDuiXiangToEntity(ref _EDataLayout1, ref _EDataLayout2);

               
                    _EDataLayout1.SetState(DTOState.New);
                

                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);

                if (result.Return)

                    MediMsgBox.Success(this, "保存成功！");
                else
                    MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
                //this.DialogResult = DialogResult.OK;
            }







        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 聚焦行改变刷新对应的行、列、其他的属性值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //多选判断
            //if (this.gridView1.FocusedRowHandle < 0) return;

        }
        /// <summary>
        /// 窗体关闭触发主窗体回刷事件
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFrmDataLayOutSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult==DialogResult.OK)
            {

                if (_GridView!=null)
                {
                    Form currentFrm = _GridView.GridControl.FindForm();
                    if (currentFrm !=null&& currentFrm is MediFormWithQX)
                    {
                        MediFormWithQX mediFormWithQX = (MediFormWithQX)currentFrm;
                        if (mediFormWithQX.RefreshCurrentFormFun!=null)
                        {
                            mediFormWithQX.RefreshCurrentFormFun(true);
                        }
                    }

                        
                }
                
            }
        }
        /// <summary>
        /// 关闭并保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton1_Click(object sender, EventArgs e)
        {
            Result<bool> result = null;

            if (_EDataLayout1 != null && _EDataLayout2 != null)
            {


                DTOChaXunDuiXiangToEntity(ref _EDataLayout1, ref _EDataLayout2);
                if (!string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID))
                {
                    _EDataLayout1.SetState(DTOState.Delete);
                    _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);
                }
                //如果datalayout1 id为空则为新增否则为修改
                //如果复位则先判断id是否为空如果为空则不需要去数据库删除否则去数据库删除

                _EDataLayout1.SetState(DTOState.New);

                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);

                if (result.Return)

                    MediMsgBox.Success(this, "保存成功！");
                else
                    MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                _EDataLayout1 = new E_GY_DATALAYOUT1();
                _EDataLayout2 = new List<E_GY_DATALAYOUT2>();
                //如果datalayout1 id为空则为新增否则为修改
                //如果复位则先判断id是否为空如果为空则不需要去数据库删除否则去数据库删除
                DTOChaXunDuiXiangToEntity(ref _EDataLayout1, ref _EDataLayout2);


                _EDataLayout1.SetState(DTOState.New);


                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);

                if (result.Return)

                    MediMsgBox.Success(this, "保存成功！");
                else
                    MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
        /// <summary>
        /// 快捷键功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFrmDataLayOutSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                mediButtonSave_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                mediButtonReset_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                mediButtonClose_Click(null, null);
            }
        }
    }


    /// <summary>
    /// 表格类
    /// </summary>
    public class MediGriviewAttribute
    {


        /// <summary>
        /// 列属性集合
        /// </summary>
        public List<MediGridviewColumn> mediGridviewColumnList { get; set; }
        /// <summary>
        /// 行属性
        /// </summary>
        public MediGriviewRowAttribute mediGriviewRowAttribute { get; set; }
        /// <summary>
        /// 其他属性
        /// </summary>
        public MediGridviewOtherAttribute mediGridviewOtherAttribute { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }



        /// <summary>
        /// 应用程序ID
        /// </summary>
        public string YingYongID { get; set; }


        /// <summary>
        /// 被点击控件名称（Datalayout,gridview）
        /// </summary>
        public string ControlName { get; set; }


        /// <summary>
        /// 布局ID
        /// </summary>
        public string DatalayoutID { get; set; }


        /// <summary>
        /// 窗体名称
        /// </summary>
        public string FormName { get; set; }




     
    }

    /// <summary>
    /// 行属性
    /// </summary>
    public class MediGriviewRowAttribute
    {
        /// <summary>
        /// 主键用于修改
        /// </summary>
        public string DATALAYOUTID { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int? LINENUMBER { get; set; }


        /// <summary>
        /// 行字体大小
        /// </summary>
        public int? ROWFONTSIZE { get; set; }



        /// <summary>
        /// 行背景色
        /// </summary>
        
        public string ROWBACKCOLOREXPRESSION { get; set; }



        /// <summary>
        /// 行背景色描述
        /// </summary>
        public string ROWBACKCOLORDESCRIBE { get; set; }

    }

   

    /// <summary>
    /// 列属性
    /// </summary>
    public class MediGridviewColumn 
    {

        /// <summary>
        /// 主键用于修改
        /// </summary>
        public string DATALAYOUTMXID { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int? SORTNO { get; set; }

        /// <summary>
        /// 主键用于修改
        /// </summary>
        public string DATALAYOUTID { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string FIELDNAME { get; set; }
        /// <summary>
        /// 字体颜色表达式
        /// </summary>
        public string CELLFORECOLOREXPRISSION { get; set; }

        /// <summary>
        /// 字体颜色描述
        /// </summary>
        public string CELLFORECOLORDESCRIBE { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CAPTION { get; set; }



        /// <summary>
        /// 显示标志
        /// </summary>
        public bool VISIBLE { get; set; }




        /// <summary>
        /// 列宽度
        /// </summary>
        public int? WIDTH { get; set; }



        /// <summary>
        /// 列头固定
        /// </summary>
        public int? FIXED { get; set; }


        /// <summary>
        /// 头字体大小
        /// </summary>
        public float HEADERFONTSIZE { get; set; }

        /// <summary>
        /// 头标题对齐方式
        /// </summary>
        public int? HEADERHALIGNMENT { get; set; }




        /// <summary>
        /// 跳转顺序
        /// </summary>
        public int? TABINDEX { get; set; }




        /// <summary>
        /// 保护标志
        /// </summary>
        public bool READONLY { get; set; }

        /// <summary>
        /// 背景颜色表达式
        /// </summary>
        public string BACKCOLOREXPRISSION { get; set; }

        /// <summary>
        /// 背景颜色描述
        /// </summary>
        public string BACKCOLORDESCRIBE { get; set; }
        /// <summary>
        /// 初始值
        /// </summary>
        public string DEFAULTVALUE { get; set; }
        /// <summary>
        /// 非空表达式
        /// </summary>
        public string NONEMPTYEXPRESSION { get; set; }

        /// <summary>
        /// 非空表达式说明
        /// </summary>
        public string NONEMPTYEDESCRIBE { get; set; }
        /// <summary>
        /// 单元格字体大小
        /// </summary>
        public float CELLFONTSIZE { get; set; }

        /// <summary>
        /// 单元格对齐方式
        /// </summary>
        public int? CELLHALIGNMENT { get; set; }



        /// <summary>
        /// 输入法模式
        /// </summary>
        public string IMEMODE { get; set; }



        /// <summary>
        /// 有效性检查
        /// </summary>
        public string VALIDATEEXPRISSION { get; set; }



        /// <summary>
        /// 有效性说明方式
        /// </summary>
        public string VALIDATEDESCRIBE { get; set; }



        /// <summary>
        /// 显示格式
        /// </summary>
        public string FORMATSTRING { get; set; }


        /// <summary>
        /// 显示格式类型
        /// </summary>
        public int? FORMATTYPE { get; set; }
    }

    /// <summary>
    /// 其他属性
    /// </summary>
    public class MediGridviewOtherAttribute
    {
        /// <summary>
        /// 主键用于修改
        /// </summary>
        public string DATALAYOUTID { get; set; }
        /// <summary>
        /// 是否显示分组面板
        /// </summary>
        public bool SHOWGROUPPANEL { get; set; }



        /// <summary>
        /// 是否允许过滤
        /// </summary>
        public bool ALLOWFILTER { get; set; }




        /// <summary>
        /// 是否允许排序
        /// </summary>
        public bool ALLOWSORT { get; set; }



        /// <summary>
        /// 是否显示列菜单
        /// </summary>
        public bool ENABLECOLUMNMENU { get; set; }



        /// <summary>
        /// 特定排序列
        /// </summary>
        public string ORDERBYFIELD { get; set; }

    }
}