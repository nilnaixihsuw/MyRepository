using DevExpress.XtraGrid.Views.Grid;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 表格个性化类
    /// </summary>
    public partial class GridViewStyleSetFrm : MediForm
    {
        /// <summary>
        /// 存储布局信息
        /// </summary>
        private E_GY_DATALAYOUT1 _EDataLayout1 = null;

        /// <summary>
        /// 创建服务实例
        /// </summary>
        private JCJGDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// dataLayout信息
        /// </summary>
        private E_GY_DATALAYOUTDTO E_GY_DATALAYOUTDTO { get; set; }

        /// <summary>
        /// 存储布局信息详情
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;

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
        public GridView _GridView { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GridViewStyleSetFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 触发窗体布局
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sControlName"></param>
        /// <param name="sNameSpace"></param>
        /// <param name="triggerControl"></param>
        public GridViewStyleSetFrm(string sFormName, string sControlName, string sNameSpace, MediGridView triggerControl)
        {
            InitializeComponent();
            _FormName = sFormName;
            _ControlName = sControlName;
            _NameSpace = sNameSpace;
            _GridView = triggerControl;
            this.mediDataLayOutTitleBar.LabelText = string.Format("{0}.{1}", _FormName, _ControlName);
            _GYDataLayoutService = new JCJGDataLayoutService();
            E_GY_DATALAYOUTDTO = new E_GY_DATALAYOUTDTO();
        }

        /// <summary>
        /// 列排序
        /// </summary>
        public struct SortColum
        {
            /// <summary>
            /// 序号
            /// </summary>
            public int? SortNo;
            /// <summary>
            /// 字段名
            /// </summary>
            public string FileName;
        }
        private string path = AppDomain.CurrentDomain.BaseDirectory;//System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        /// <summary>
        /// 排序结构
        /// </summary>
        public List<SortColum> sortColums = new List<SortColum>();

        private void GridViewStyleSetFrm_Load(object sender, EventArgs e)
        {
            // 初始化lookupedit
            InitialLookUpeditControlBindDataSource();

            GetDataLayoutForDB();

            string GetValue = "";
            string pathconfig = $@"{path}Config.xml";
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            XmlDocument xmlDocument = new XmlDocument();
         
            if (File.Exists(pathconfig))
            {
                xmlDocument.Load(pathconfig);
            }
            //获取表格信息地址
            if (!string.IsNullOrWhiteSpace(this._FormName) && _EDataLayout2 != null && _EDataLayout2.Count > 0)
            {
                var readPath = $@"{path}{this._FormName}.xml";
                if (File.Exists(readPath) && File.Exists(pathconfig))
                {
                    for (int a = 0; a < _GridView.Columns.Count; a++)
                    {
                        _EDataLayout2.ForEach(r =>
                        {
                            if (r.FIELDNAME == _GridView.Columns[a].FieldName)
                            {
                                r.SHENGJIANGXH = _GridView.Columns[a].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? 1 : _GridView.Columns[a].SortOrder == DevExpress.Data.ColumnSortOrder.Descending ? 2 : 0;//升降序
                                if (File.Exists(pathconfig) && xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{_FormName}") != null)
                                {
                                    GetValue = "";
                                    IsSelectNode(pathconfig, $@"YOUXIANJIPZ/{_FormName}/{r.FIELDNAME}/YOUXIANJI", "Get", ref GetValue);
                                    r.YOUXIANJI = string.IsNullOrWhiteSpace(GetValue) ? 0 : Convert.ToInt32(GetValue);
                                }
                            }
                        });
                    }
                }
            }

            if (_EDataLayout1 != null && !string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID) && _EDataLayout2 != null && _EDataLayout2.Count() > 0)
            {
                ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(_EDataLayout1, DTOState.UnChange);
                if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                {
                    this.radioGroupLevel.EditValue = "0";
                }
                else if (_EDataLayout1.YINGYONGID.Length == 2)
                {
                    this.radioGroupLevel.EditValue = "1";
                }
                else
                {
                    this.radioGroupLevel.EditValue = "2";
                }
                mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                var list = _EDataLayout2.OrderBy<E_GY_DATALAYOUT2, int?>(o => o.SORTNO).ToList();
                eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(list);
            }
            else
            {
                if (_EDataLayout1 == null || _EDataLayout2 == null)
                {
                    _EDataLayout1 = new E_GY_DATALAYOUT1();
                    _EDataLayout2 = new List<E_GY_DATALAYOUT2>();
                }
                else
                {
                    _EDataLayout1 = new E_GY_DATALAYOUT1();
                    _EDataLayout2.Clear();
                }
                // radioGroupLevel.DataBindings.Add("EditValue", _EDataLayout1, "YINGYONGJB", true, DataSourceUpdateMode.OnPropertyChanged);
                //this.radioGroupLevel.EditValue = "2";
                GetDataLayoutAttribute(ref _EDataLayout1, ref _EDataLayout2);

                //获取表格信息地址
                if (!string.IsNullOrWhiteSpace(this._FormName))
                {
                    var readPath = $@"{path}{this._FormName}.xml";
                    if (File.Exists(readPath) && _EDataLayout2 != null && _EDataLayout2.Count > 0)
                    {
                        for (int a = 0; a < _GridView.Columns.Count; a++)
                        {
                            _EDataLayout2.ForEach(r =>
                            {
                                if (r.FIELDNAME == _GridView.Columns[a].FieldName)
                                {
                                    r.SHENGJIANGXH = _GridView.Columns[a].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? 1 : _GridView.Columns[a].SortOrder == DevExpress.Data.ColumnSortOrder.Descending ? 2 : 0;
                                    if (File.Exists(pathconfig) && xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{_FormName}") != null)
                                    {
                                        GetValue = "";
                                        IsSelectNode(pathconfig, $@"YOUXIANJIPZ/{_FormName}/{r.FIELDNAME}/YOUXIANJI", "Get", ref GetValue);
                                        r.YOUXIANJI = string.IsNullOrWhiteSpace(GetValue) ? 0 : Convert.ToInt32(GetValue);
                                    }
                                }
                            });
                        }
                    }
                }

                ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(_EDataLayout1, DTOState.New);
                mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;

                eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(_EDataLayout2);
            }

            this.mediGridView1.BestFitColumns();
        }

        /// <summary>
        /// 获取DataLayou数据源
        /// </summary>
        /// <param name="eDataLayout1"></param>
        /// <param name="eDatalayout2List"></param>
        private void GetDataLayoutAttribute(ref E_GY_DATALAYOUT1 eDataLayout1, ref List<E_GY_DATALAYOUT2> eDatalayout2List)
        {
            if (_GridView != null)
            {
                if (_GridView is MediGridView)
                {
                    MediGridView observerMediGridview = (MediGridView)_GridView;
                    //eDataLayout1.DATALAYOUTID
                    eDataLayout1.YINGYONGID = (radioGroupLevel.SelectedIndex == 0 ? HISClientHelper.YINGYONGID : (radioGroupLevel.SelectedIndex == 1 ? HISClientHelper.XITONGID : "00"));
                    eDataLayout1.FORMNAME = _FormName;
                    eDataLayout1.CONTROLNAME = _ControlName;
                    eDataLayout1.NAMESPACE = _NameSpace;
                    eDataLayout1.ALLOWFILTER = observerMediGridview.OptionsCustomization.AllowFilter == true ? 1 : 0;
                    eDataLayout1.ALLOWSORT = observerMediGridview.OptionsCustomization.AllowSort == true ? 1 : 0;
                    eDataLayout1.ENABLECOLUMNMENU = observerMediGridview.OptionsMenu.EnableColumnMenu == true ? 1 : 0;
                    eDataLayout1.ORDERBYFIELD = "";
                    eDataLayout1.SHOWGROUPPANEL = observerMediGridview.OptionsView.ShowGroupPanel == true ? 1 : 0;
                    eDataLayout1.LINENUMBER = observerMediGridview.IsShowLineNumber == true ? 1 : 0;
                    eDataLayout1.ROWBACKCOLOREXPRESSION = "";
                    eDataLayout1.ROWBACKCOLORDESCRIBE = "";
                    eDataLayout1.ROWFONTSIZE = Convert.ToInt32(observerMediGridview.Appearance.Row.Font.Size);
                    for (int i = 0; i < observerMediGridview.Columns.Count; i++)
                    {
                        E_GY_DATALAYOUT2 e_GY_DATALAYOUT2 = new E_GY_DATALAYOUT2();

                        //e_GY_DATALAYOUT2.DATALAYOUTID = observerMediGridview.Columns[i]
                        e_GY_DATALAYOUT2.FIELDNAME = observerMediGridview.Columns[i].FieldName;
                        e_GY_DATALAYOUT2.CAPTION = observerMediGridview.Columns[i].Caption;
                        e_GY_DATALAYOUT2.VISIBLE = observerMediGridview.Columns[i].Visible == true ? 1 : 0;
                        e_GY_DATALAYOUT2.WIDTH = observerMediGridview.Columns[i].Width;
                        e_GY_DATALAYOUT2.FIXED = (int)observerMediGridview.Columns[i].Fixed;
                        e_GY_DATALAYOUT2.HEADERFONTSIZE = Convert.ToInt32(observerMediGridview.Columns[i].AppearanceHeader.Font.Size);
                        e_GY_DATALAYOUT2.HEADERHALIGNMENT = (int)observerMediGridview.Columns[i].AppearanceHeader.TextOptions.HAlignment;
                        e_GY_DATALAYOUT2.TABINDEX = -1;//跳转顺序----待定
                        e_GY_DATALAYOUT2.READONLY = observerMediGridview.Columns[i].ReadOnly == true ? 1 : 0;
                        e_GY_DATALAYOUT2.DEFAULTVALUE = "";//默认值待定
                        e_GY_DATALAYOUT2.CELLFONTSIZE = Convert.ToInt32(observerMediGridview.Columns[i].AppearanceCell.Font.Size);
                        e_GY_DATALAYOUT2.CELLHALIGNMENT = (int)observerMediGridview.Columns[i].AppearanceCell.TextOptions.HAlignment;
                        e_GY_DATALAYOUT2.IMEMODE = "";//输入法模式待定
                        e_GY_DATALAYOUT2.VALIDATEEXPRISSION = "";//表达式待定
                        e_GY_DATALAYOUT2.VALIDATEDESCRIBE = "";//有效性说明
                        e_GY_DATALAYOUT2.FORMATSTRING = "";//显示格式
                        e_GY_DATALAYOUT2.FORMATTYPE = 0;//显示格式类型
                        e_GY_DATALAYOUT2.SORTNO = observerMediGridview.Columns[i].VisibleIndex;
                        eDatalayout2List.Add(e_GY_DATALAYOUT2);
                    }
                }
            }
        }

        /// 获取数据布局信息从数据库
        /// </summary>
        private void GetDataLayoutForDB()
        {
            var layoutResult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
            if (layoutResult == null || layoutResult.Return.DataLayout2 == null || layoutResult.Return.DataLayout1 == null)
            {
                layoutResult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                if (layoutResult == null || layoutResult.Return.DataLayout2 == null || layoutResult.Return.DataLayout1 == null)
                {
                    layoutResult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                    if (layoutResult != null && layoutResult.ReturnCode == ReturnCode.SUCCESS)
                    {
                        _EDataLayout1 = layoutResult.Return.DataLayout1;
                        _EDataLayout2 = layoutResult.Return.DataLayout2;

                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                    }
                }
                else
                {
                    if (layoutResult.ReturnCode == ReturnCode.SUCCESS)
                    {
                        _EDataLayout1 = layoutResult.Return.DataLayout1;
                        _EDataLayout2 = layoutResult.Return.DataLayout2;

                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                    }
                }
            }
            else
            {
                if (layoutResult.ReturnCode == ReturnCode.SUCCESS)
                {
                    _EDataLayout1 = layoutResult.Return.DataLayout1;
                    _EDataLayout2 = layoutResult.Return.DataLayout2;

                    if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                    {
                        this.radioGroupLevel.EditValue = "0";
                    }
                    else if (_EDataLayout1.YINGYONGID.Length == 2)
                    {
                        this.radioGroupLevel.EditValue = "1";
                    }
                    else
                    {
                        this.radioGroupLevel.EditValue = "2";
                    }
                }
            }
        }

        /// <summary>
        /// 单元格文本对齐方式类
        /// </summary>
        public class CellTextHAlignment
        {
            /// <summary>
            /// 列固定方式名称
            /// </summary>
            public string CellTextHAlignmentName { get; set; }

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
            public string StringFormatName { get; set; }

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
            public string ColumnHeaderHAlignmentName { get; set; }

            /// <summary>
            /// 字符串格式代码
            /// </summary>
            public int ColumnHeaderHAlignmentCode { get; set; }
        }

        // 输入法模式
        public class ImeMode
        {
            /// <summary>
            /// 输入法名称
            /// </summary>
            public string ImeModeName { get; set; }

            /// <summary>
            /// 输入法代码
            /// </summary>
            public int ImeModeCode { get; set; }
        }

        /// <summary>
        /// 列固定类
        /// </summary>
        public class ColumnFix
        {
            /// <summary>
            /// 列固定方式名称
            /// </summary>
            public string ColumnFixName { get; set; }

            /// <summary>
            /// 列固定方式代码
            /// </summary>
            public int ColumnFixCode { get; set; }
        }

        private void InitialLookUpeditControlBindDataSource()
        {
            // 列头固定方式数据源
            List<ColumnFix> columnfixed = new List<ColumnFix>();
            columnfixed.Add(new ColumnFix() { ColumnFixName = "默认", ColumnFixCode = 0 });
            columnfixed.Add(new ColumnFix() { ColumnFixName = "居左", ColumnFixCode = 1 });
            columnfixed.Add(new ColumnFix() { ColumnFixName = "居右", ColumnFixCode = 2 });
            this.rpiMediColumFixedGridLookUpEdit.DataSource = columnfixed;
            rpiMediColumFixedGridLookUpEdit.DisplayMember = "ColumnFixName";
            rpiMediColumFixedGridLookUpEdit.ValueMember = "ColumnFixCode";
            rpiMediColumFixedGridLookUpEdit.View.OptionsView.ShowIndicator = false;

            // 列头对齐方式
            List<ColumnHeaderHAlignment> columnHeaderHAlignment = new List<ColumnHeaderHAlignment>();
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = "默认", ColumnHeaderHAlignmentCode = 0 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = "居左", ColumnHeaderHAlignmentCode = 1 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = "居中", ColumnHeaderHAlignmentCode = 2 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = "居右", ColumnHeaderHAlignmentCode = 3 });
            this.rpiMediGridHeadHAlignmentLookUpEdit.DataSource = columnHeaderHAlignment;
            rpiMediGridHeadHAlignmentLookUpEdit.DisplayMember = "ColumnHeaderHAlignmentName";
            rpiMediGridHeadHAlignmentLookUpEdit.ValueMember = "ColumnHeaderHAlignmentCode";
            rpiMediGridHeadHAlignmentLookUpEdit.View.OptionsView.ShowIndicator = false;

            // 单元格对齐方式
            List<CellTextHAlignment> cellTextHAlignment = new List<CellTextHAlignment>();
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "默认", CellTextHAlignmentCode = 0 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "居左", CellTextHAlignmentCode = 1 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "居中", CellTextHAlignmentCode = 2 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "居右", CellTextHAlignmentCode = 3 });
            this.rpiMediGridCellHAlignmentLookUpEdit.DataSource = cellTextHAlignment;
            rpiMediGridCellHAlignmentLookUpEdit.DisplayMember = "CellTextHAlignmentName";
            rpiMediGridCellHAlignmentLookUpEdit.ValueMember = "CellTextHAlignmentCode";
            rpiMediGridCellHAlignmentLookUpEdit.View.OptionsView.ShowIndicator = false;

            // 显示格式类型
            List<StringFormat> stringFormat = new List<StringFormat>();
            stringFormat.Add(new StringFormat() { StringFormatName = "默认", StringFormatCode = 0 });
            stringFormat.Add(new StringFormat() { StringFormatName = "数字类型", StringFormatCode = 1 });
            stringFormat.Add(new StringFormat() { StringFormatName = "日期类型", StringFormatCode = 2 });
            this.rpiMediGridFormatTypeLookUpEdit.DataSource = stringFormat;
            rpiMediGridFormatTypeLookUpEdit.DisplayMember = "StringFormatName";
            rpiMediGridFormatTypeLookUpEdit.ValueMember = "StringFormatCode";
            rpiMediGridFormatTypeLookUpEdit.View.OptionsView.ShowIndicator = false;

            // 输入法模式
            List<ImeMode> imeMode = new List<ImeMode>();
            //imeMode.Add(new ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Inherit, ImeModeCode = -1 });
            imeMode.Add(new ImeMode() { ImeModeName = "默认", ImeModeCode = 0 });
            imeMode.Add(new ImeMode() { ImeModeName = "打开", ImeModeCode = 1 });
            imeMode.Add(new ImeMode() { ImeModeName = "关闭", ImeModeCode = 2 });
            imeMode.Add(new ImeMode() { ImeModeName = "禁用", ImeModeCode = 3 });
            this.rpiMediGridImodeLookUpEdit.DataSource = imeMode;
            rpiMediGridImodeLookUpEdit.DisplayMember = "ImeModeName";
            rpiMediGridImodeLookUpEdit.ValueMember = "ImeModeCode";
            rpiMediGridImodeLookUpEdit.View.OptionsView.ShowIndicator = false;
        }


        /// <summary>
        /// 验证用户权限
        /// </summary>
        /// <returns></returns>
        private bool ValidateUser()
        {
            if (HISClientHelper.USERID == "DBA")
            {
                return true;
            }
            else
            {
                MediMsgBox.Info(this, "非DBA（管理员）人员不能保存数据库，只允许保存本地！");
                return false;
            }
        }

        /// <summary>
        /// 跳转索引验证重复值
        /// </summary>
        private bool ValidateJumpIndex()
        {
            Dictionary<string, string> jumpIndexDic = new Dictionary<string, string>();
            for (int i = 0; i < this.mediGridView1.DataRowCount; i++)
            {
                if (jumpIndexDic.ContainsKey(this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx()) && !this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx().Equals("-1"))
                {
                    MediMsgBox.Warn("字段" + "【" + jumpIndexDic[this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx()] + "】" + "和字段" + "【" + this.mediGridView1.GetRowCellValue(i, "CAPTION").ToStringEx() + "】" + "跳转索引冲突");

                    jumpIndexDic = null;
                    return false;
                }
                else
                {
                    if (!this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx().Equals("-1"))
                    {
                        jumpIndexDic.Add(this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx(), this.mediGridView1.GetRowCellValue(i, "CAPTION").ToStringEx());
                    }
                }
            }
            if (this.mediGridView1.DataRowCount > 0)
            {
                jumpIndexDic = null;
                return true;
            }
            else
            {
                jumpIndexDic = null;
                return false;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonReset_Click(object sender, EventArgs e)
        {

            ClientGridViewPropertyGridObject e_GY_DATALAYOUT2 = mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject;

            MediTraceList<E_GY_DATALAYOUT2> dataLayout2TraceList = eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>;
            if (!string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID))
            {
                e_GY_DATALAYOUT2.SetState(DTOState.Delete);
                eGYDATALAYOUT2BindingSource.Clear();
                Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(e_GY_DATALAYOUT2.ConvertToDataLayout1(), dataLayout2TraceList.ToList<E_GY_DATALAYOUT2>());

                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    this.radioGroupLevel.EditValue = "2";
                    if (_GridView != null)
                    {
                        if (_GridView is MediGridView)
                        {
                            MediGridView observerMediGridview = (MediGridView)_GridView;

                            _EDataLayout1 = observerMediGridview.DataLayoutDefaultValue.DataLayout1;
                            _EDataLayout2 = observerMediGridview.DataLayoutDefaultValue.DataLayout2;
                        }
                    }
                    ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(_EDataLayout1, DTOState.New);
                    mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;

                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(_EDataLayout2);
                    this.Tag = "RESET";

                    if (this.radioGroupLevel.EditValue.ToString() == "0")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "1")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "2")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                    }


                    MediMsgBox.Success(this, "重置成功！");
                }
                else
                    MediMsgBox.Failure(this, "重置失败！");
            }
            else
            {
                if (this.radioGroupLevel.EditValue.ToString() == "0")
                {
                    var resertresult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                    if (resertresult.ReturnCode == ReturnCode.SUCCESS && resertresult.Return.DataLayout1 != null && resertresult.Return.DataLayout2 != null)
                    {
                        resertresult.Return.DataLayout1.SetState(DTOState.Delete);
                        Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(resertresult.Return.DataLayout1, resertresult.Return.DataLayout2);
                        if (result.ReturnCode == ReturnCode.SUCCESS)
                        {
                            MediGridView observerMediGridview = (MediGridView)_GridView;
                            if (observerMediGridview.DataLayoutDefaultValue.DataLayout1 == null || observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID))
                            {
                                this.radioGroupLevel.EditValue = "0";
                            }
                            else if (observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                            {
                                this.radioGroupLevel.EditValue = "1";
                            }
                            else
                            {
                                this.radioGroupLevel.EditValue = "2";
                            }

                            eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(observerMediGridview.DataLayoutDefaultValue.DataLayout2);
                            _EDataLayout1 = observerMediGridview.DataLayoutDefaultValue.DataLayout1;
                            this.Tag = "RESET";
                            if (this.radioGroupLevel.EditValue.ToString() == "0")
                            {
                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                            }
                            if (this.radioGroupLevel.EditValue.ToString() == "1")
                            {

                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                            }
                            if (this.radioGroupLevel.EditValue.ToString() == "2")
                            {
                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                            }

                            MediMsgBox.Success(this, "重置成功!");
                        }
                        else
                        {
                            MediMsgBox.Failure(this, "重置失败!");
                        }
                    }
                    else
                    {
                        MediMsgBox.Warn(this, "未启用自定义布局!");
                    }
                }
                if (this.radioGroupLevel.EditValue.ToString() == "1")
                {

                    var resertresult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                    if (resertresult.ReturnCode == ReturnCode.SUCCESS && resertresult.Return.DataLayout1 != null && resertresult.Return.DataLayout2 != null)
                    {
                        resertresult.Return.DataLayout1.SetState(DTOState.Delete);
                        Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(resertresult.Return.DataLayout1, resertresult.Return.DataLayout2);
                        if (result.ReturnCode == ReturnCode.SUCCESS)
                        {
                            MediGridView observerMediGridview = (MediGridView)_GridView;
                            if (observerMediGridview.DataLayoutDefaultValue.DataLayout1 == null || observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID))
                            {
                                this.radioGroupLevel.EditValue = "0";
                            }
                            else if (observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                            {
                                this.radioGroupLevel.EditValue = "1";
                            }
                            else
                            {
                                this.radioGroupLevel.EditValue = "2";
                            }

                            eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(observerMediGridview.DataLayoutDefaultValue.DataLayout2);
                            _EDataLayout1 = observerMediGridview.DataLayoutDefaultValue.DataLayout1;
                            this.Tag = "RESET";
                            if (this.radioGroupLevel.EditValue.ToString() == "0")
                            {
                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                            }
                            if (this.radioGroupLevel.EditValue.ToString() == "1")
                            {

                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                            }
                            if (this.radioGroupLevel.EditValue.ToString() == "2")
                            {
                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                            }

                            MediMsgBox.Success(this, "重置成功!");
                        }
                        else
                        {
                            MediMsgBox.Failure(this, "重置失败!");
                        }
                    }
                    else
                    {
                        MediMsgBox.Warn(this, "未启用自定义布局!");
                    }
                }
                if (this.radioGroupLevel.EditValue.ToString() == "2")
                {

                    var resertresult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                    if (resertresult.ReturnCode == ReturnCode.SUCCESS && resertresult.Return.DataLayout1 != null && resertresult.Return.DataLayout2 != null)
                    {
                        resertresult.Return.DataLayout1.SetState(DTOState.Delete);
                        Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(resertresult.Return.DataLayout1, resertresult.Return.DataLayout2);
                        if (result.ReturnCode == ReturnCode.SUCCESS)
                        {
                            MediGridView observerMediGridview = (MediGridView)_GridView;
                            if (observerMediGridview.DataLayoutDefaultValue.DataLayout1 == null || observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID))
                            {
                                this.radioGroupLevel.EditValue = "0";
                            }
                            else if (observerMediGridview.DataLayoutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                            {
                                this.radioGroupLevel.EditValue = "1";
                            }
                            else
                            {
                                this.radioGroupLevel.EditValue = "2";
                            }

                            eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(observerMediGridview.DataLayoutDefaultValue.DataLayout2);
                            _EDataLayout1 = observerMediGridview.DataLayoutDefaultValue.DataLayout1;
                            this.Tag = "RESET";
                            if (this.radioGroupLevel.EditValue.ToString() == "0")
                            {
                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                            }
                            if (this.radioGroupLevel.EditValue.ToString() == "1")
                            {

                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                            }
                            if (this.radioGroupLevel.EditValue.ToString() == "2")
                            {
                                GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                            }

                            MediMsgBox.Success(this, "重置成功!");
                        }
                        else
                        {
                            MediMsgBox.Failure(this, "重置失败!");
                        }
                    }
                    else
                    {
                        MediMsgBox.Warn(this, "未启用自定义布局!");
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateExpressStr())
                return;
            if (!ValidateJumpIndex())
                return;
            if (!ValidateUser())
                return;

            ClientGridViewPropertyGridObject e_GY_DATALAYOUT2 = mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject;
            MediTraceList<E_GY_DATALAYOUT2> dataLayout2TraceList = eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>;
            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                if ("00".Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0 && (e_GY_DATALAYOUT2.GetPropertyChangedCount() == 0 && e_GY_DATALAYOUT2.GetState() != DTOState.New))
                {
                    MediMsgBox.Info(this, "没有修改的项，无需保存！");
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {
                if (HISClientHelper.XITONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0 && (e_GY_DATALAYOUT2.GetPropertyChangedCount() == 0 && e_GY_DATALAYOUT2.GetState() != DTOState.New))
                {
                    MediMsgBox.Info(this, "没有修改的项，无需保存！");
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {
                if (HISClientHelper.YINGYONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0 && (e_GY_DATALAYOUT2.GetPropertyChangedCount() == 0 && e_GY_DATALAYOUT2.GetState() != DTOState.New))
                {
                    MediMsgBox.Info(this, "没有修改的项，无需保存！");
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                e_GY_DATALAYOUT2.ConvertToDataLayout1().YINGYONGID = "00";
                if (string.IsNullOrWhiteSpace(e_GY_DATALAYOUT2.ConvertToDataLayout1().DATALAYOUTID))
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.New);
                }
                else
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.Update);
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {
                e_GY_DATALAYOUT2.ConvertToDataLayout1().YINGYONGID = HISClientHelper.XITONGID;
                if (string.IsNullOrWhiteSpace(e_GY_DATALAYOUT2.ConvertToDataLayout1().DATALAYOUTID))
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.New);
                }
                else
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.Update);
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {
                e_GY_DATALAYOUT2.ConvertToDataLayout1().YINGYONGID = HISClientHelper.YINGYONGID;
                if (string.IsNullOrWhiteSpace(e_GY_DATALAYOUT2.ConvertToDataLayout1().DATALAYOUTID))
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.New);
                }
                else
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.Update);
                }
            }

            if (!string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID))
            {
                Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(e_GY_DATALAYOUT2.ConvertToDataLayout1(), dataLayout2TraceList.GetChanges());

                if (result.Return)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = e_GY_DATALAYOUT2.ConvertToDataLayout1();
                    E_GY_DATALAYOUTDTO.DataLayout2 = dataLayout2TraceList.ToList<E_GY_DATALAYOUT2>();
                    this.Tag = E_GY_DATALAYOUTDTO;

                    if (this.radioGroupLevel.EditValue.ToString() == "0")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "1")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "2")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    (mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject).SetState(DTOState.UnChange);
                    (eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>).ResetChangeStatus();
                    MediMsgBox.Success(this, "保存成功！");

                    // 异步更新缓存
                }
                else
                    MediMsgBox.Failure(this, "保存失败！");
            }
            else
            {
                Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(e_GY_DATALAYOUT2.ConvertToDataLayout1(), dataLayout2TraceList.GetChanges());

                if (result.Return)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = e_GY_DATALAYOUT2.ConvertToDataLayout1();
                    E_GY_DATALAYOUTDTO.DataLayout2 = dataLayout2TraceList.GetChanges();
                    this.Tag = E_GY_DATALAYOUTDTO;
                    if (this.radioGroupLevel.EditValue.ToString() == "0")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "1")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "2")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    (mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject).SetState(DTOState.UnChange);
                    (eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>).ResetChangeStatus();
                    MediMsgBox.Success(this, "保存成功！");

                    // 异步更新缓存
                }
                else
                    MediMsgBox.Failure(this, "保存失败！");
            }
        }

        private bool ValidateExpressStr()
        {
            for (int i = 0; i < mediGridView1.DataRowCount; i++)
            {
                // 列背景色表达式
                string errorMsg = string.Empty;
                if (!ExpressionCommonHelper.ExpressionValidate(mediGridView1.GetRowCellValue(i, "BACKCOLOREXPRISSION").ToStringEx(), out errorMsg))
                {
                    MediMsgBox.Warn("第" + (i + 1) + "行列背景色表达式不合法!\r\n错误原因:" + errorMsg);

                    mediGridView1.FocusedRowHandle = i;
                    mediGridView1.FocusedColumn = this.mediGridView1.Columns["BACKCOLOREXPRISSION"];
                    mediGridView1.ShowEditor();

                    return false;
                }

                if (!ExpressionCommonHelper.ExpressionValidate(mediGridView1.GetRowCellValue(i, "CELLFORECOLOREXPRISSION").ToStringEx(), out errorMsg))
                {
                    MediMsgBox.Warn("第" + (i + 1) + "行列字体颜色表达式!\r\n错误原因:" + errorMsg);

                    mediGridView1.FocusedRowHandle = i;
                    mediGridView1.FocusedColumn = this.mediGridView1.Columns["CELLFORECOLOREXPRISSION"];

                    mediGridView1.ShowEditor();

                    return false;
                }
            }

            if (this.mediPropertyGrid1.SelectedObject != null)
            {
                ClientGridViewPropertyGridObject clientDataLayout1 = mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject;
                if (clientDataLayout1 != null)
                {
                    string errorMsg = string.Empty;
                    if (!ExpressionCommonHelper.ExpressionValidate(clientDataLayout1.RowBackColor, out errorMsg))
                    {
                        this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
                        MediMsgBox.Warn("其他属性面板中行背景色表达式不合法!\r\n错误原因:" + errorMsg);

                        return false;
                    }
                }
            }

            return true;

        }

        /// <summary>
        /// 保存并关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (!ValidateExpressStr())
                return;
            if (!ValidateJumpIndex())
                return;
            if (!ValidateUser())
                return;
            ClientGridViewPropertyGridObject e_GY_DATALAYOUT2 = mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject;
            MediTraceList<E_GY_DATALAYOUT2> dataLayout2TraceList = eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>;
            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                if ("00".Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0 && (e_GY_DATALAYOUT2.GetPropertyChangedCount() == 0 && e_GY_DATALAYOUT2.GetState() != DTOState.New))
                {
                    MediMsgBox.Info(this, "没有修改的项，无需保存！");
                    this.Close();
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {
                if (HISClientHelper.XITONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0 && (e_GY_DATALAYOUT2.GetPropertyChangedCount() == 0 && e_GY_DATALAYOUT2.GetState() != DTOState.New))
                {
                    MediMsgBox.Info(this, "没有修改的项，无需保存！");
                    this.Close();
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {
                if (HISClientHelper.YINGYONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0 && (e_GY_DATALAYOUT2.GetPropertyChangedCount() == 0 && e_GY_DATALAYOUT2.GetState() != DTOState.New))
                {
                    MediMsgBox.Info(this, "没有修改的项，无需保存！");
                    this.Close();
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                e_GY_DATALAYOUT2.ConvertToDataLayout1().YINGYONGID = "00";
                if (string.IsNullOrWhiteSpace(e_GY_DATALAYOUT2.ConvertToDataLayout1().DATALAYOUTID))
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.New);
                }
                else
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.Update);
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {
                e_GY_DATALAYOUT2.ConvertToDataLayout1().YINGYONGID = HISClientHelper.XITONGID;
                if (string.IsNullOrWhiteSpace(e_GY_DATALAYOUT2.ConvertToDataLayout1().DATALAYOUTID))
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.New);
                }
                else
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.Update);
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {
                e_GY_DATALAYOUT2.ConvertToDataLayout1().YINGYONGID = HISClientHelper.YINGYONGID;
                if (string.IsNullOrWhiteSpace(e_GY_DATALAYOUT2.ConvertToDataLayout1().DATALAYOUTID))
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.New);
                }
                else
                {
                    e_GY_DATALAYOUT2.ConvertToDataLayout1().SetState(DTOState.Update);
                }
            }

            if (!string.IsNullOrEmpty(_EDataLayout1.DATALAYOUTID))
            {
                Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(e_GY_DATALAYOUT2.ConvertToDataLayout1(), dataLayout2TraceList.GetChanges());

                if (result.Return)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = e_GY_DATALAYOUT2.ConvertToDataLayout1();

                    E_GY_DATALAYOUTDTO.DataLayout2 = dataLayout2TraceList.ToList<E_GY_DATALAYOUT2>();
                    this.Tag = E_GY_DATALAYOUTDTO;
                    if (this.radioGroupLevel.EditValue.ToString() == "0")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "1")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "2")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    //if (dataLayout2TraceList.GetChanges().Count > 0|| e_GY_DATALAYOUT2.IsNewStateUpdate)
                    (mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject).SetState(DTOState.UnChange);
                    (eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>).ResetChangeStatus();
                    MediMsgBox.Success(this, "保存成功！");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MediMsgBox.Failure(this, "保存失败！");
            }
            else
            {
                Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(e_GY_DATALAYOUT2.ConvertToDataLayout1(), dataLayout2TraceList.GetChanges());

                if (result.Return)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = e_GY_DATALAYOUT2.ConvertToDataLayout1();
                    E_GY_DATALAYOUTDTO.DataLayout2 = dataLayout2TraceList.GetChanges();
                    this.Tag = E_GY_DATALAYOUTDTO;
                    if (this.radioGroupLevel.EditValue.ToString() == "0")
                    {
                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "1")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    if (this.radioGroupLevel.EditValue.ToString() == "2")
                    {

                        GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                        ClientGridViewPropertyGridObject clientGridViewPropertyGridObject = new ClientGridViewPropertyGridObject(datalayoutInfo.DataLayout1, DTOState.UnChange);
                        if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                        {
                            this.radioGroupLevel.EditValue = "0";
                        }
                        else if (_EDataLayout1.YINGYONGID.Length == 2)
                        {
                            this.radioGroupLevel.EditValue = "1";
                        }
                        else
                        {
                            this.radioGroupLevel.EditValue = "2";
                        }
                        mediPropertyGrid1.SelectedObject = clientGridViewPropertyGridObject;
                        eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                    }
                    //if (dataLayout2TraceList.GetChanges().Count > 0 || e_GY_DATALAYOUT2.IsNewStateUpdate)
                    (mediPropertyGrid1.SelectedObject as ClientGridViewPropertyGridObject).SetState(DTOState.UnChange);
                    (eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>).ResetChangeStatus();
                    MediMsgBox.Success(this, "保存成功！");
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                    MediMsgBox.Failure(this, "保存失败！");
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            //this.Dispose();
        }

        /// <summary>
        /// 关闭时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewStyleSetFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 背景色表达式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rpiColumnBackColorBtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit buttonEdit = sender as DevExpress.XtraEditors.ButtonEdit;
            if (buttonEdit.TopLevelControl != null)
            {
                string sourceValue = string.Empty;
                if (this.mediGridView1.GetFocusedValue() != null)
                    sourceValue = this.mediGridView1.GetFocusedValue().ToString();
                using (CellExpressionShowFrm cellExpressionShowFrm = new CellExpressionShowFrm(buttonEdit.TopLevelControl, sourceValue))
                {
                    cellExpressionShowFrm.ShowDialog();
                    if (cellExpressionShowFrm.DialogResult == DialogResult.OK)
                    {
                        this.mediGridView1.SetFocusedValue(cellExpressionShowFrm.Tag);
                    }
                }
            }
        }

        /// <summary>
        /// 字体颜色表达式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rpiColumnFontColorBtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit buttonEdit = sender as DevExpress.XtraEditors.ButtonEdit;
            if (buttonEdit.TopLevelControl != null)
            {
                string sourceValue = string.Empty;
                if (this.mediGridView1.GetFocusedValue() != null)
                    sourceValue = this.mediGridView1.GetFocusedValue().ToString();
                using (CellExpressionShowFrm cellExpressionShowFrm = new CellExpressionShowFrm(buttonEdit.TopLevelControl, sourceValue))
                {
                    cellExpressionShowFrm.ShowDialog();

                    if (cellExpressionShowFrm.DialogResult == DialogResult.OK)
                    {
                        this.mediGridView1.SetFocusedValue(cellExpressionShowFrm.Tag);
                    }
                }
            }
        }

        private void GridViewStyleSetFrm_Shown(object sender, EventArgs e)
        {

        }

        private void mediGridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (this.mediGridView1.FocusedColumn.FieldName.ToUpper().Equals("SORTNO"))
                {
                    //this.mediGridView1.Columns["SORTNO"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
                    Dictionary<int, object> sortdic = new Dictionary<int, object>();

                    for (int i = 0; i < this.mediGridView1.DataRowCount; i++)
                    {
                        if (this.mediGridView1.FocusedRowHandle != i)
                        {
                            object ovalue = this.mediGridView1.GetRowCellValue(i, "SORTNO");
                            if (!ovalue.ToString().Equals("-1") && Convert.ToInt32(ovalue) >= Convert.ToInt32(e.Value))
                                sortdic.Add(i, Convert.ToInt32(ovalue) + 1);
                        }
                        else
                        {
                            sortdic.Add(i, Convert.ToInt32(e.Value));
                        }

                    }

                    foreach (var item in sortdic.Keys)
                    {
                        this.mediGridView1.SetRowCellValue(item, this.mediGridView1.FocusedColumn.FieldName, sortdic[item]);
                    }
                    var tempdatalayout2 = (MediTraceList<E_GY_DATALAYOUT2>)eGYDATALAYOUT2BindingSource.DataSource;

                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(tempdatalayout2.ToList<E_GY_DATALAYOUT2>().OrderBy<E_GY_DATALAYOUT2, int?>(o => o.SORTNO).ToList());
                }

                //this.mediGridView1.Columns["SORTNO"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
        }

        private void mediGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //this.mediGridView1.Columns["SORTNO"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
        }

        private void columnAttributeGridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.Home)
            {
                this.mediGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            }
        }

        /// <summary>
        /// 本地节点是否存在
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="node"></param>
        /// <param name="lb"></param>
        /// <param name="GetValue"></param>
        /// <param name="value"></param>
        public void IsSelectNode(string xmlPath, string node, string lb, ref string GetValue, long value = 0)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                var s = node.Split('/');
                xmlDocument.Load(xmlPath);
                if (!(xmlDocument.SelectSingleNode(node) == null ? false : true))
                {
                    switch (s.Count())
                    {
                        case 2:
                            XmlNode memberlist2 = xmlDocument.SelectSingleNode("YOUXIANJIPZ");
                            XmlElement member = xmlDocument.CreateElement(_FormName);
                            memberlist2.AppendChild(member);
                            xmlDocument.Save(xmlPath);
                            break;
                        case 3:
                            XmlNode memberlist3 = xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{_FormName}");
                            XmlElement fieldname = xmlDocument.CreateElement(s[2].ToString().Trim());
                            memberlist3.AppendChild(fieldname);
                            xmlDocument.Save(xmlPath);
                            break;
                        case 4:
                            XmlNode memberlist4 = xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{_FormName}/{s[2].ToString().Trim()}");
                            XmlElement yxj = xmlDocument.CreateElement("YOUXIANJI");
                            yxj.InnerText = value.ToString().Trim();
                            memberlist4.AppendChild(yxj);
                            xmlDocument.Save(xmlPath);
                            break;
                    }
                }
                if ((xmlDocument.SelectSingleNode(node) != null ? true : false) && s.Count() == 4)
                {
                    if (lb == "Set")//设置节点值
                    {
                        XmlNode memberlist = xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{_FormName}/{s[2].ToString().Trim()}");
                        XmlNodeList nodelist = memberlist.ChildNodes;
                        foreach (XmlNode nodes in nodelist)
                        {
                            nodes.InnerText = value.ToString().Trim();
                            break;
                        }
                        xmlDocument.Save(xmlPath);
                    }
                    else if (lb == "Get")//获取节点值
                    {
                        XmlNode memberlist = null;
                        if (_FormName == null)
                            memberlist = xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{s[1].ToString().Trim()}/{s[2].ToString().Trim()}");
                        else
                            memberlist = xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{_FormName}/{s[2].ToString().Trim()}");


                        if (memberlist != null)
                        {
                            GetValue = (memberlist.SelectSingleNode("YOUXIANJI")).InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 表格布局存储本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSaveLocal_Click(object sender, EventArgs e)
        {
            try
            {
                string GetValue = "";
                XmlDocument doc = new XmlDocument();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string paths = $@"{path}{_FormName}.xml";
                string pathconfig = $@"{path}Config.xml";
                if (!File.Exists(pathconfig))
                {
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
                    doc.AppendChild(dec);
                    //  创建根结点
                    XmlElement XMLroot1 = doc.CreateElement("YOUXIANJIPZ");
                    doc.AppendChild(XMLroot1);
                    try
                    {
                        doc.Save(pathconfig);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if ((eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>).GetChanges().Count > 0)
                {
                    this.Tag = new List<E_GY_DATALAYOUT2>(eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>);
                    IsSelectNode(pathconfig, $@"YOUXIANJIPZ/{_FormName}", "Set", ref GetValue);
                    foreach (var s in (eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>).GetChanges())
                    {
                        if (!string.IsNullOrWhiteSpace(s.FIELDNAME))
                        {
                            IsSelectNode(pathconfig, $@"YOUXIANJIPZ/{_FormName}/{s.FIELDNAME}", "Set", ref GetValue);
                            IsSelectNode(pathconfig, $@"YOUXIANJIPZ/{_FormName}/{s.FIELDNAME}/YOUXIANJI", "Set", ref GetValue, Convert.ToInt64(s.YOUXIANJI));
                            _GridView.Columns.ColumnByFieldName(s.FIELDNAME).SortOrder = s.SHENGJIANGXH == 1 ? DevExpress.Data.ColumnSortOrder.Ascending : s.SHENGJIANGXH == 2 ? DevExpress.Data.ColumnSortOrder.Descending : DevExpress.Data.ColumnSortOrder.None;
                        }
                    }

                    _GridView.SaveLayoutToXml(paths);
                    if (File.Exists(paths))
                    {
                        MediMsgBox.Success(this, "保存本地成功！");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MediMsgBox.Success(this, "保存本地失败！");
                        this.DialogResult = DialogResult.No;
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 删除本地表格配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonDeleteLocal_Click(object sender, EventArgs e)
        {
            string pathInfo = $@"{path}{_FormName}.xml";
            string pathconfig = $@"{path}Config.xml";
            if (Directory.Exists(path) || Directory.Exists(pathInfo) || Directory.Exists(pathconfig))
            {
                if (File.Exists(pathInfo))
                {
                    File.Delete(pathInfo);
                }
                if (File.Exists(pathconfig))
                {
                    File.Delete(pathconfig);
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            this.DialogResult = DialogResult.OK;
            this.Tag = "RESET";
            MediMsgBox.Success(this, "删除本地成功！");
        }
    }
}