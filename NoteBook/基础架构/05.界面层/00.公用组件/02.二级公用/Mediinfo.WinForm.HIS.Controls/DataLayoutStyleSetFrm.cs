using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 个性化datalayout类
    /// </summary>
    public partial class DataLayoutStyleSetFrm : MediForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataLayoutStyleSetFrm()
        {
            InitializeComponent();
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
        /// dataLayout信息
        /// </summary>
        private E_GY_DATALAYOUTDTO E_GY_DATALAYOUTDTO { get; set; }

        /// <summary>
        /// 数据布局
        /// </summary>
        private MediDataLayoutControl _MediDataLayoutControl = null;

        /// <summary>
        /// 属性集合
        /// </summary>
        public List<NewItemLayoutAttribute> ItemLayoutAttributeList { get; set; }

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
        private JCJGDataLayoutService _GYDataLayoutService = null;

        #endregion 属性

        /// <summary>
        /// 根据DataLayOutControl获取字段
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sControlName"></param>
        /// <param name="sNameSpace"></param>
        /// <param name="control"></param>
        public DataLayoutStyleSetFrm(string sFormName, string sControlName, string sNameSpace, MediDataLayoutControl control)
        {
            InitializeComponent();
            _FormName = sFormName;
            _ControlName = sControlName;
            _MediDataLayoutControl = control;
            _NameSpace = sNameSpace;
            this.mediDataLayOutTitleBar.LabelText = "名称:" + string.Format("{0}.{1}", _FormName, _ControlName);
            this.Text = "项属性";
            _GYDataLayoutService = new JCJGDataLayoutService();
            E_GY_DATALAYOUTDTO = new E_GY_DATALAYOUTDTO();
        }

        private void InitialLookUpeditControlBindDataSource()
        {
            // 列头对齐方式
            List<CellTextHAlignment> cellTextHAlignment = new List<CellTextHAlignment>();
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "默认", CellTextHAlignmentCode = 0 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "居左", CellTextHAlignmentCode = 1 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "居中", CellTextHAlignmentCode = 2 });
            cellTextHAlignment.Add(new CellTextHAlignment() { CellTextHAlignmentName = "居右", CellTextHAlignmentCode = 3 });
            this.rpiMediGridHAligentLookUpEdit.DataSource = cellTextHAlignment;
            rpiMediGridHAligentLookUpEdit.DisplayMember = "ColumnHeaderHAlignmentName";
            rpiMediGridHAligentLookUpEdit.ValueMember = "ColumnHeaderHAlignmentCode";
            rpiMediGridHAligentLookUpEdit.View.OptionsView.ShowIndicator = false;

            List<StringFormat> stringFormat = new List<StringFormat>();
            stringFormat.Add(new StringFormat() { StringFormatName = "默认", StringFormatCode = 0 });
            stringFormat.Add(new StringFormat() { StringFormatName = "数字类型", StringFormatCode = 1 });
            stringFormat.Add(new StringFormat() { StringFormatName = "日期类型", StringFormatCode = 2 });
            this.rpiFormatTypeMediGridLookUpEdit.DataSource = stringFormat;
            rpiFormatTypeMediGridLookUpEdit.DisplayMember = "StringFormatName";
            rpiFormatTypeMediGridLookUpEdit.ValueMember = "StringFormatCode";
            rpiFormatTypeMediGridLookUpEdit.View.OptionsView.ShowIndicator = false;
            // 输入法模式
            List<ImeMode> imeMode = new List<ImeMode>();
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
        /// 从数据库加载dalayout页面布局属性
        /// </summary>
        /// <returns></returns>
        private bool LoadDBDataLayoutAttribute()
        {
            if (_EDataLayout1 == null) return false;

            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) return false;

            int index = 1;
            if (ItemLayoutAttributeList == null)
                ItemLayoutAttributeList = new List<NewItemLayoutAttribute>();
            else
                ItemLayoutAttributeList.Clear();

            _EDataLayout2.ToList().ForEach(o =>
            {
                string feildName = o.FIELDNAME;
                if (!(ItemLayoutAttributeList.Where(c => c.FIELDNAME == feildName).ToList().Count > 0))
                {
                    NewItemLayoutAttribute itemLayoutAttribute = new NewItemLayoutAttribute();
                    itemLayoutAttribute.CAPTION = o.CAPTION;
                    itemLayoutAttribute.FIELDNAME = feildName;
                    itemLayoutAttribute.READONLY = o.READONLY.ToInt(0) == 0 ? false : true;
                    itemLayoutAttribute.DEFAULTVALUE = o.DEFAULTVALUE;
                    itemLayoutAttribute.HEADERFONTSIZE = o.HEADERFONTSIZE.ToInt(9);
                    itemLayoutAttribute.VISIBLE = o.VISIBLE.ToInt(1) == 0 ? false : true;
                    itemLayoutAttribute.VALIDATEEXPRISSION = o.VALIDATEEXPRISSION;
                    itemLayoutAttribute.VALIDATEDESCRIBE = o.VALIDATEDESCRIBE;
                    itemLayoutAttribute.HEADERHALIGNMENT = o.HEADERHALIGNMENT.ToInt(0);
                    itemLayoutAttribute.TABINDEX = o.TABINDEX.ToInt(index);
                    itemLayoutAttribute.IMEMODE = o.IMEMODE;
                    itemLayoutAttribute.DatalayoutID = o.DATALAYOUTID;
                    itemLayoutAttribute.DatalayoutmxID = o.DATALAYOUTMXID;
                    ItemLayoutAttributeList.Add(itemLayoutAttribute);
                }
            });

            return true;
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
            public DevExpress.Utils.HorzAlignment ColumnHeaderHAlignmentName { get; set; }

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
            public DevExpress.XtraGrid.Columns.FixedStyle ColumnFixName { get; set; }

            /// <summary>
            /// 列固定方式代码
            /// </summary>
            public int ColumnFixCode { get; set; }
        }

        /// <summary>
        /// 遍历DataLayoutControl控件中的输入框
        /// </summary>
        /// <param name="layGroup"></param>
        /// <param name="e_GY_DATALAYOUT2List"></param>
        private void RecursionLayoutControl(LayoutControlGroup layGroup, ref List<E_GY_DATALAYOUT2> e_GY_DATALAYOUT2List)
        {
            if (layGroup == null) return;

            if (layGroup.Items == null || layGroup.Items.Count == 0) return;

            LayoutGroupItemCollection layoutGroupColl = layGroup.Items;
            int i = 0;
            foreach (BaseLayoutItem item in layoutGroupColl)
            {
                if (item is LayoutControlGroup)
                {
                    RecursionLayoutControl(item as LayoutControlGroup, ref e_GY_DATALAYOUT2List);
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
                        E_GY_DATALAYOUT2 tempDataLayout2 = new E_GY_DATALAYOUT2();
                        tempDataLayout2.DATALAYOUTID = "";
                        tempDataLayout2.DATALAYOUTMXID = "";

                        tempDataLayout2.CAPTION = layItem.Text;
                        tempDataLayout2.FIELDNAME = sFeildName;
                        tempDataLayout2.READONLY = control.ReadOnly == true ? 1 : 0;
                        tempDataLayout2.DEFAULTVALUE = "";
                        tempDataLayout2.CELLFONTSIZE = Convert.ToInt32(control.Font.Size);
                        tempDataLayout2.VISIBLE = control.Visible == true ? 1 : 0;
                        tempDataLayout2.VALIDATEEXPRISSION = "";
                        tempDataLayout2.VALIDATEDESCRIBE = "";
                        tempDataLayout2.HEADERHALIGNMENT = Convert.ToInt32(layItem.AppearanceItemCaption.TextOptions.HAlignment);
                        tempDataLayout2.TABINDEX = control.TabIndex;
                        tempDataLayout2.IMEMODE = Convert.ToInt32(control.ImeMode).ToString();
                        e_GY_DATALAYOUT2List.Add(tempDataLayout2);
                        i++;
                        #endregion
                    }
                }
            }
        }

        private void New2FrmDataLayoutSet_Load(object sender, EventArgs e)
        {
            InitialLookUpeditControlBindDataSource();
            // 初始化数据集
            GetDataLayoutForDB();

            // 绑定属性值
            if (_MediDataLayoutControl != null)
            {
                if (_EDataLayout1 != null && !string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID) && _EDataLayout2.Count > 0 && _EDataLayout2 != null)
                {
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
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(_EDataLayout2);
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

                    this.radioGroupLevel.EditValue = "2";

                    GetDefaultDataLayoutAttribute(ref _EDataLayout1, ref _EDataLayout2);

                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(_EDataLayout2);
                }
            }

            this.mediGridView1.BestFitColumns();
        }

        /// <summary>
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
        /// 设置DataLayout属性
        /// </summary>
        private void GetDefaultDataLayoutAttribute(ref E_GY_DATALAYOUT1 eDataLayout1, ref List<E_GY_DATALAYOUT2> eDataLayout2List)
        {
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
            eDataLayout1.YINGYONGID = HISClientHelper.YINGYONGID;
            eDataLayout1.FORMNAME = _FormName;
            eDataLayout1.CONTROLNAME = _ControlName;
            eDataLayout1.NAMESPACE = _NameSpace;
            eDataLayout1.ALLOWFILTER = 0;
            eDataLayout1.ALLOWSORT = 0;
            eDataLayout1.ENABLECOLUMNMENU = 0;
            eDataLayout1.ORDERBYFIELD = "";
            eDataLayout1.SHOWGROUPPANEL = 0;
            eDataLayout1.LINENUMBER = 0;
            eDataLayout1.ROWBACKCOLOREXPRESSION = "";
            eDataLayout1.ROWBACKCOLORDESCRIBE = "";
            eDataLayout1.ROWFONTSIZE = 9;

            RecursionLayoutControl(this._MediDataLayoutControl.Root, ref eDataLayout2List);
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID))
            {
                _EDataLayout1.SetState(DTOState.Delete);
                MediTraceList<E_GY_DATALAYOUT2> dataLayout2TraceList = eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>;

                Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, dataLayout2TraceList.GetChanges());
                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1 == null || this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID))
                    {
                        this.radioGroupLevel.EditValue = "0";
                    }
                    else if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                    {
                        this.radioGroupLevel.EditValue = "1";
                    }
                    else
                    {
                        this.radioGroupLevel.EditValue = "2";
                    }

                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout2);
                    _EDataLayout1 = this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1;
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
                if (this.radioGroupLevel.EditValue.ToString() == "0")
                {
                    var resertresult = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                    if (resertresult.ReturnCode == ReturnCode.SUCCESS && resertresult.Return.DataLayout1 != null && resertresult.Return.DataLayout2 != null)
                    {
                        resertresult.Return.DataLayout1.SetState(DTOState.Delete);
                        Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(resertresult.Return.DataLayout1, resertresult.Return.DataLayout2);
                        if (result.ReturnCode == ReturnCode.SUCCESS)
                        {
                            if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1 == null || this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID))
                            {
                                this.radioGroupLevel.EditValue = "0";
                            }
                            else if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                            {
                                this.radioGroupLevel.EditValue = "1";
                            }
                            else
                            {
                                this.radioGroupLevel.EditValue = "2";
                            }

                            eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout2);
                            _EDataLayout1 = this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1;
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
                            if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1 == null || this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID))
                            {
                                this.radioGroupLevel.EditValue = "0";
                            }
                            else if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                            {
                                this.radioGroupLevel.EditValue = "1";
                            }
                            else
                            {
                                this.radioGroupLevel.EditValue = "2";
                            }

                            eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout2);
                            _EDataLayout1 = this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1;
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
                            if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1 == null || this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID))
                            {
                                this.radioGroupLevel.EditValue = "0";
                            }
                            else if (this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1.YINGYONGID.Length == 2)
                            {
                                this.radioGroupLevel.EditValue = "1";
                            }
                            else
                            {
                                this.radioGroupLevel.EditValue = "2";
                            }

                            eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout2);
                            _EDataLayout1 = this._MediDataLayoutControl.DataLayOutDefaultValue.DataLayout1;
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
        /// 获取DataLayou数据源
        /// </summary>
        /// <param name="eDataLayout1"></param>
        /// <param name="eDatalayout2List"></param>
        private void GetDataLayoutAttribute(ref E_GY_DATALAYOUT1 eDataLayout1, ref List<E_GY_DATALAYOUT2> eDatalayout2List)
        {
            if (ItemLayoutAttributeList == null || ItemLayoutAttributeList.Count == 0) return;

            #region 处理DataLayout1数据

            if (_EDataLayout1 == null)
            {
                // 等于空
                eDataLayout1.SetState(DTOState.New);
            }
            else
            {
                // 不等空
                eDataLayout1.DATALAYOUTID = _EDataLayout1.DATALAYOUTID;
                eDataLayout1.SetState(DTOState.Update);
            }

            eDataLayout1.YINGYONGID = (radioGroupLevel.SelectedIndex == 0 ? HISClientHelper.YINGYONGID : (radioGroupLevel.SelectedIndex == 1 ? HISClientHelper.XITONGID : "00"));

            eDataLayout1.FORMNAME = _FormName;
            eDataLayout1.CONTROLNAME = _ControlName;
            eDataLayout1.NAMESPACE = _NameSpace;
            eDataLayout1.ALLOWFILTER = 0;
            eDataLayout1.ALLOWSORT = 0;
            eDataLayout1.ENABLECOLUMNMENU = 0;
            eDataLayout1.ORDERBYFIELD = "";
            eDataLayout1.SHOWGROUPPANEL = 0;
            eDataLayout1.LINENUMBER = 0;
            eDataLayout1.ROWBACKCOLOREXPRESSION = "";
            eDataLayout1.ROWBACKCOLORDESCRIBE = "";
            eDataLayout1.ROWFONTSIZE = 9;

            #endregion

            #region 处理DataLayout2数据

            bool update = !(_EDataLayout2 == null || _EDataLayout2.Count == 0);

            foreach (NewItemLayoutAttribute ItemLayoutAttribute in ItemLayoutAttributeList)
            {
                E_GY_DATALAYOUT2 eDataLayout2 = null;
                if (_EDataLayout2 == null)
                {
                    eDataLayout2 = DataLayoutAttributeToE(ItemLayoutAttribute, false);
                }
                else
                {
                    var count = _EDataLayout2.Count(o => String.Equals(o.FIELDNAME, ItemLayoutAttribute.FIELDNAME, StringComparison.CurrentCultureIgnoreCase));

                    eDataLayout2 = DataLayoutAttributeToE(ItemLayoutAttribute, count > 0 && update);
                }
                eDatalayout2List.Add(eDataLayout2);
            }

            #endregion
        }

        /// <summary>
        /// 属性信息转化为实体
        /// </summary>
        /// <param name="itemLayoutAttribute"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        private E_GY_DATALAYOUT2 DataLayoutAttributeToE(NewItemLayoutAttribute itemLayoutAttribute, bool update)
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
            eDataLayout2.CAPTION = itemLayoutAttribute.CAPTION;
            eDataLayout2.CELLFONTSIZE = 9;
            eDataLayout2.CELLFORECOLORDESCRIBE = "";
            eDataLayout2.CELLFORECOLOREXPRISSION = "";
            eDataLayout2.CELLHALIGNMENT = 0;
            eDataLayout2.DATALAYOUTID = itemLayoutAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = itemLayoutAttribute.DatalayoutmxID;
            eDataLayout2.DEFAULTVALUE = itemLayoutAttribute.DEFAULTVALUE;
            eDataLayout2.FIELDNAME = itemLayoutAttribute.FIELDNAME;
            eDataLayout2.FIXED = 0;
            eDataLayout2.FORMATSTRING = "";
            eDataLayout2.FORMATTYPE = 0;
            eDataLayout2.HEADERFONTSIZE = Convert.ToInt32(itemLayoutAttribute.HEADERFONTSIZE);
            eDataLayout2.HEADERHALIGNMENT = Convert.ToInt32(itemLayoutAttribute.HEADERHALIGNMENT);
            eDataLayout2.IMEMODE = Convert.ToInt32(itemLayoutAttribute.IMEMODE).ToString();
            eDataLayout2.READONLY = itemLayoutAttribute.READONLY ? 1 : 0;

            eDataLayout2.SORTNO = 0;
            eDataLayout2.TABINDEX = itemLayoutAttribute.TABINDEX;
            eDataLayout2.VALIDATEDESCRIBE = itemLayoutAttribute.VALIDATEDESCRIBE;
            eDataLayout2.VALIDATEEXPRISSION = itemLayoutAttribute.VALIDATEEXPRISSION;
            eDataLayout2.VISIBLE = itemLayoutAttribute.VISIBLE ? 1 : 0;
            eDataLayout2.WIDTH = 99;

            return eDataLayout2;
        }

        /// <summary>
        /// 跳转索引验证重复值
        /// </summary>
        private bool ValidateJumpIndex()
        {
            Dictionary<string, string> jumpIndexDic = new Dictionary<string, string>();
            for (int i = 0; i < this.mediGridView1.DataRowCount; i++)
            {
                if (jumpIndexDic.ContainsKey(this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx()))
                {
                    MediMsgBox.Warn("字段" + "【" + jumpIndexDic[this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx()] + "】" + "和字段" + "【" + this.mediGridView1.GetRowCellValue(i, "CAPTION").ToStringEx() + "】" + "跳转索引冲突");

                    return false;
                }
                else
                {
                    jumpIndexDic.Add(this.mediGridView1.GetRowCellValue(i, "TABINDEX").ToStringEx(), this.mediGridView1.GetRowCellValue(i, "CAPTION").ToStringEx());
                }
            }
            if (this.mediGridView1.DataRowCount > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateJumpIndex())
                return;
            MediTraceList<E_GY_DATALAYOUT2> dataLayout2TraceList = eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>;

            if (this.radioGroupLevel.EditValue.ToString() == "0" && dataLayout2TraceList != null && ("00".Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0)) return;
            if (this.radioGroupLevel.EditValue.ToString() == "1" && dataLayout2TraceList != null && (HISClientHelper.XITONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0)) return;
            if (this.radioGroupLevel.EditValue.ToString() == "2" && dataLayout2TraceList != null && (HISClientHelper.YINGYONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0)) return;

            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                _EDataLayout1.YINGYONGID = "00";
                _EDataLayout1.SetState(string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID)
                    ? DTOState.New
                    : DTOState.Update);
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {
                _EDataLayout1.YINGYONGID = HISClientHelper.XITONGID;
                _EDataLayout1.SetState(string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID)
                    ? DTOState.New
                    : DTOState.Update);
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {
                _EDataLayout1.YINGYONGID = HISClientHelper.YINGYONGID;
                _EDataLayout1.SetState(string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID)
                    ? DTOState.New
                    : DTOState.Update);
            }
            Result<bool> result;
            if (!string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID) && dataLayout2TraceList != null)
            {
                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, dataLayout2TraceList.GetChanges());
                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = _EDataLayout1;
                    E_GY_DATALAYOUTDTO.DataLayout2 = dataLayout2TraceList.GetChanges();
                }
            }
            else
            {
                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);
                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = _EDataLayout1;
                    E_GY_DATALAYOUTDTO.DataLayout2 = _EDataLayout2;
                }
            }

            if (result.ReturnCode == ReturnCode.SUCCESS)
            {
                this.Tag = E_GY_DATALAYOUTDTO;
                if (this.radioGroupLevel.EditValue.ToString() == "0")
                {
                    GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                    var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");

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
                    _EDataLayout1 = datalayoutInfo.DataLayout1;
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                }
                if (this.radioGroupLevel.EditValue.ToString() == "1")
                {
                    GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                    var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
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
                    _EDataLayout1 = datalayoutInfo.DataLayout1;
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                }
                if (this.radioGroupLevel.EditValue.ToString() == "2")
                {
                    GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                    var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
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
                    _EDataLayout1 = datalayoutInfo.DataLayout1;
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                }
                (eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>)?.ResetChangeStatus();
                MediMsgBox.Success(this, "保存成功！");
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", result);
            }
        }

        /// <summary>
        /// 保存关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton1_Click(object sender, EventArgs e)
        {
            if (!ValidateJumpIndex())
                return;
            MediTraceList<E_GY_DATALAYOUT2> dataLayout2TraceList = eGYDATALAYOUT2BindingSource.DataSource as MediTraceList<E_GY_DATALAYOUT2>;
            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                if (dataLayout2TraceList != null && ("00".Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0))
                {
                    this.Close();
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {

                if (HISClientHelper.XITONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0)
                {
                    this.Close();
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {

                if (HISClientHelper.YINGYONGID.Equals(_EDataLayout1.YINGYONGID) && dataLayout2TraceList.GetChanges().Count == 0)
                {
                    this.Close();
                    return;
                }
            }
            if (this.radioGroupLevel.EditValue.ToString() == "0")
            {
                _EDataLayout1.YINGYONGID = "00";
                _EDataLayout1.SetState(string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID)
                    ? DTOState.New
                    : DTOState.Update);
            }
            if (this.radioGroupLevel.EditValue.ToString() == "1")
            {
                _EDataLayout1.YINGYONGID = HISClientHelper.XITONGID;
                _EDataLayout1.SetState(string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID)
                    ? DTOState.New
                    : DTOState.Update);
            }
            if (this.radioGroupLevel.EditValue.ToString() == "2")
            {
                _EDataLayout1.YINGYONGID = HISClientHelper.YINGYONGID;
                _EDataLayout1.SetState(string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID)
                    ? DTOState.New
                    : DTOState.Update);
            }
            Result<bool> result;
            if (!string.IsNullOrWhiteSpace(_EDataLayout1.DATALAYOUTID))
            {
                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, dataLayout2TraceList?.GetChanges());
                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = _EDataLayout1;
                    E_GY_DATALAYOUTDTO.DataLayout2 = dataLayout2TraceList?.GetChanges();
                }
            }
            else
            {
                result = _GYDataLayoutService.SaveDataLayoutInfo(_EDataLayout1, _EDataLayout2);
                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    E_GY_DATALAYOUTDTO.DataLayout1 = _EDataLayout1;
                    E_GY_DATALAYOUTDTO.DataLayout2 = _EDataLayout2;
                }
            }

            if (result.ReturnCode == ReturnCode.SUCCESS)
            {
                this.Tag = E_GY_DATALAYOUTDTO;
                if (this.radioGroupLevel.EditValue.ToString() == "0")
                {
                    GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
                    var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, "00");
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
                    _EDataLayout1 = datalayoutInfo.DataLayout1;
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                }
                if (this.radioGroupLevel.EditValue.ToString() == "1")
                {

                    GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
                    var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.XITONGID);
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
                    _EDataLayout1 = datalayoutInfo.DataLayout1;
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                }
                if (this.radioGroupLevel.EditValue.ToString() == "2")
                {
                    GYDataLayoutHelper.RefreshDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
                    var datalayoutInfo = GYDataLayoutHelper.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);
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
                    _EDataLayout1 = datalayoutInfo.DataLayout1;
                    eGYDATALAYOUT2BindingSource.DataSource = new MediTraceList<E_GY_DATALAYOUT2>(datalayoutInfo.DataLayout2);
                }
                MediMsgBox.Success(this, "保存成功！");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", result);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 快捷键功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New2FrmDataLayoutSet_KeyDown(object sender, KeyEventArgs e)
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

        private void DataLayoutStyleSetFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void mediGridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (this.mediGridView1.FocusedColumn.FieldName.ToUpper().Equals("SORTNO"))
                {
                    if (this.mediGridView1.Columns["SORTNO"] != null)
                        this.mediGridView1.Columns["SORTNO"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
                    Dictionary<int, object> sortdic = new Dictionary<int, object>();

                    for (int i = 0; i < this.mediGridView1.DataRowCount; i++)
                    {
                        if (this.mediGridView1.FocusedRowHandle != i)
                        {
                            object ovalue = this.mediGridView1.GetRowCellValue(i, "SORTNO");
                            if (!ovalue.ToString().Equals("-1") && Convert.ToInt32(ovalue) >= Convert.ToInt32(e.Value))
                                sortdic.Add(i, Convert.ToInt32(ovalue) + 1);
                        }
                    }

                    foreach (var item in sortdic.Keys)
                    {
                        this.mediGridView1.SetRowCellValue(item, this.mediGridView1.FocusedColumn.FieldName, sortdic[item]);
                    }
                }
                if (this.mediGridView1.Columns["SORTNO"] != null)
                {
                    this.mediGridView1.Columns["SORTNO"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                }
            }
        }

        private void DataLayoutStyleSetFrm_Shown(object sender, EventArgs e)
        {
            mediDataLayOutTitleBar.Focus();
        }

        private void mediGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.Home)
            {
                this.mediGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            }
        }
    }

    /// <summary>
    /// DataLayoutControl项属性
    /// </summary>
    public class NewItemLayoutAttribute
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string DatalayoutmxID { get; set; }

        /// <summary>
        /// 外键ID
        /// </summary>
        public string DatalayoutID { get; set; }

        /// <summary>
        /// 字段名字
        /// </summary>
        public string FIELDNAME { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CAPTION { get; set; }

        /// <summary>
        /// 显示标志
        /// </summary>
        public bool VISIBLE
        { get; set; }

        /// <summary>
        ///字体大小
        /// </summary>
        public float HEADERFONTSIZE
        { get; set; }

        /// <summary>
        /// 标题对齐方式
        /// </summary>
        public int HEADERHALIGNMENT
        { get; set; }

        /// <summary>
        /// 跳转顺序
        /// </summary>
        public int TABINDEX
        { get; set; }

        /// <summary>
        /// 保护标志
        /// </summary>
        public bool READONLY
        { get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        public string DEFAULTVALUE
        { get; set; }

        /// <summary>
        /// 输入法模式
        /// </summary>
        public string IMEMODE
        { get; set; }

        /// <summary>
        /// 有效性检查表达式
        /// </summary>
        public string VALIDATEEXPRISSION
        { get; set; }

        /// <summary>
        /// 有效性检查描述
        /// </summary>
        public string VALIDATEDESCRIBE
        { get; set; }
    }
}