using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// datalayout类
    /// </summary>
    [ToolboxItem(true)]
    public class MediDataLayoutControl : DataLayoutControl
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        private LayoutControlGroup[] layoutControlGroupCollection;

        [Browsable(true)]
        [Category("LayoutControlGroup"), Description("当多个Datalayout布局需要发生变化时,选择其中任意一个自定义布局"), DefaultValue("")]
        public LayoutControlGroup[] LayoutControlGroupCollection
        {
            get
            {
                if (this.layoutControlGroupCollection == null)
                {
                    LayoutControlGroup layoutControlGroup1 = this.Root;
                    layoutControlGroupCollection = new LayoutControlGroup[] { layoutControlGroup1 };
                }
                return layoutControlGroupCollection;
            }
            set
            {
                layoutControlGroupCollection = value;

            }
        }

        /// <summary>
        /// 当前选择的面板组
        /// </summary>
        [Browsable(true)]
        [Category("LayoutControlGroup"), Description("当前选择的LayoutControlGroup"), DefaultValue("")]
        public LayoutControlGroup SelectedLayoutControlGroup
        {
            get
            {
                return this.Root;
            }
            set
            {
                this.Root = value;
                foreach (LayoutControlGroup item in this.LayoutControlGroupCollection)
                {
                    if (item != this.Root)
                    {
                        this.Root.GroupBordersVisible = false;
                        foreach (LayoutControlItem layoutControlItem in item.Items)
                        {
                            layoutControlItem.Control.Location = new System.Drawing.Point(-1000, -1000);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控件释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            foreach (LayoutControlGroup layoutControlGroup in this.LayoutControlGroupCollection)
            {
                layoutControlGroup.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 全局变量

        /// <summary>
        /// 存储布局信息
        /// </summary>
        private E_GY_DATALAYOUT1 _EDataLayout1 = null;

        /// <summary>
        /// 存储布局信息详情
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;

        /// <summary>
        /// 默认页面布局保存
        /// </summary>
        public E_GY_DATALAYOUTDTO DataLayOutDefaultValue { get; set; }

        /// <summary>
        /// 创建服务实例
        /// </summary>
        //private GYDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// 存储非空字段项
        /// </summary>
        private List<string> _NonEmptyFields = new List<string>();

        /// <summary>
        /// 基础数据源
        /// </summary>
        private DTOBase _DTOBase;

        #endregion 全局变量

        #region 属性

        /// <summary>
        /// 是否可以
        /// </summary>
        private bool _ReaderOnly = false;

        /// <summary>
        /// 是否可用
        /// </summary>
        [DescriptionAttribute("控件是否可读")]
        [Browsable(false)]
        public bool ReaderOnly
        {
            get
            {
                return _ReaderOnly;
            }
            set
            {
                if (_ReaderOnly != value)
                {
                    _ReaderOnly = value;
                    // FomatEditItems();
                }
            }
        }

        private bool _EnterMoveNextControl = true;

        /// <summary>
        /// 按回车焦点移动到下一个控件
        /// </summary>
        [DescriptionAttribute("按回车焦点移动到下一个控件")]
        [Browsable(true)]
        public bool EnterMoveNextControl
        {
            get { return _EnterMoveNextControl; }
            set { _EnterMoveNextControl = value; }
        }

        #endregion 属性

        #region 公共方法

        /// <summary>
        /// 添加非空字段设置
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public void AddRequiredField(string fieldName)
        {
            if (fieldName.IsNullOrEmpty()) return;

            if (_NonEmptyFields.Count == 0)
                _NonEmptyFields.Add(fieldName.ToUpper());
            else
            {
                if (!_NonEmptyFields.Contains(fieldName.ToUpper()))
                {
                    _NonEmptyFields.Add(fieldName.ToUpper());
                    FomatEditItems();
                }
            }
        }

        /// <summary>
        /// 设置文本框禁用
        /// </summary>
        /// <param name="FiledName"></param>
        public void SetTextBoxEnbaled(string FiledName)
        {
            foreach (BaseLayoutItem item in this.Items)
            {
                if (item is LayoutControlItem)
                {
                    Control c = (item as LayoutControlItem).Control;
                    if (c == null) continue;

                    if (c is BaseEdit)
                    {
                        BaseEdit baseEdit = c as BaseEdit;

                        if (c.DataBindings == null || c.DataBindings.Count == 0) continue;

                        string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                        if (!string.IsNullOrWhiteSpace(fieldName) && fieldName.Equals(FiledName))
                            c.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 设置跳转列
        /// </summary>
        public void SetJumpTextBoxByFieldName(string FiledName)
        {
            foreach (BaseLayoutItem item in this.Items)
            {
                if (item is LayoutControlItem)
                {
                    Control c = (item as LayoutControlItem).Control;
                    if (c == null) continue;

                    if (c is BaseEdit)
                    {
                        BaseEdit baseEdit = c as BaseEdit;

                        if (c.DataBindings == null || c.DataBindings.Count == 0) continue;

                        string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                        if (!string.IsNullOrWhiteSpace(fieldName) && fieldName.Equals(FiledName))
                        {
                            baseEdit.TabStop = true;
                            baseEdit.EnterMoveNextControl = false;
                            baseEdit.Focus();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清除非空项
        /// </summary>
        public void ResetRequiredFields()
        {
            if (_NonEmptyFields != null && _NonEmptyFields.Count > 0)
            {
                _NonEmptyFields.Clear();
            }
        }

        /// <summary>
        /// 保存验证
        /// </summary>
        /// <param name="trimSpace">是否移除空值(默认去除)</param>
        /// <param name="showErrorMsg">是否显示错误信息(默认显示)</param>
        /// <returns></returns>
        public bool CheckRequiredFields(bool trimSpace = true, bool showErrorMsg = true)
        {
            // 存储必填项提示信息
            List<string> requiredFieldsMsg = new List<string>();

            // 存储第一个非空项
            BaseEdit baseEditFirst = null;

            #region 有效性检查

            if (_EDataLayout2 != null && _EDataLayout2.Count > 0)
            {
                foreach (BaseLayoutItem item in this.Items)
                {
                    if (item is LayoutControlItem)
                    {
                        Control c = (item as LayoutControlItem).Control;
                        if (c == null) continue;

                        string caption = (item as LayoutControlItem).Text;
                        if (c is BaseEdit)
                        {
                            BaseEdit baseEdit = c as BaseEdit;

                            if (baseEdit.EditValue == null) continue;

                            if (c.DataBindings == null || c.DataBindings.Count == 0) continue;

                            string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                            if (_EDataLayout2 != null)
                            {
                                // 屏幕下面的foreach，通过查找方式处理
                                var ret = _EDataLayout2.Where(o => o.FIELDNAME.ToUpper() == fieldName.ToUpper()).FirstOrDefault();
                                if (ret != null)
                                {
                                    if (!ret.VALIDATEEXPRISSION.IsNullOrWhiteSpace())
                                    {
                                        // 正则表达式验证
                                        Regex reg = new Regex(ret.VALIDATEEXPRISSION);
                                        Match m = reg.Match(baseEdit.EditValue.ToString().Trim());
                                        if (!m.Success)
                                        {
                                            requiredFieldsMsg.Add(string.Format("【{0}】，有效性验证失败！", caption));
                                            if (requiredFieldsMsg.Count == 1)
                                                baseEditFirst = baseEdit;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion 有效性检查

            #region 代码设置非空字段

            if (_NonEmptyFields != null && _NonEmptyFields.Count > 0)
            {
                foreach (BaseLayoutItem item in this.Items)
                {
                    if (item is LayoutControlItem)
                    {
                        Control c = (item as LayoutControlItem).Control;
                        string caption = (item as LayoutControlItem).Text;
                        if (c == null) continue;
                        if (c is BaseEdit)
                        {
                            BaseEdit baseEdit = c as BaseEdit;
                            if (c.DataBindings == null || c.DataBindings.Count == 0) continue;
                            string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                            if (_NonEmptyFields.Contains(fieldName.ToUpper()) && (baseEdit.EditValue == null || (trimSpace ? baseEdit.EditValue.ToString().Trim().IsNullOrWhiteSpace() : baseEdit.EditValue.ToString().IsNullOrEmpty())))
                            {
                                requiredFieldsMsg.Add(string.Format("{0}不能为空！", caption.Replace(":", "")));
                                if (requiredFieldsMsg.Count == 1)
                                    baseEditFirst = baseEdit;
                            }
                        }
                    }
                }
            }

            #endregion 代码设置非空字段

            #region 返回处理消息

            if (requiredFieldsMsg.Count == 0)
                return true;
            else
            {
                if (showErrorMsg)
                {
                    string msg = "";
                    requiredFieldsMsg.ForEach(o =>
                    {
                        msg += o + "\r\n";
                    });

                    if (msg.Length > 2)
                        msg = msg.Remove(msg.Length - 2);

                    MediMsgBox.Show(msg);
                }

                if (baseEditFirst != null) baseEditFirst.Focus();
                return false;
            }

            #endregion 返回处理消息
        }

        /// <summary>
        /// 设置字段默认值 需先调用此方法再调用AddNew
        /// </summary>
        /// <typeparam name="T">DTOBase</typeparam>
        /// <param name="t">数据值</param>
        public void SetFieldDefaultValue<T>(T t) where T : DTOBase
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0)
            {
                _DTOBase = t; return;
            }

            Type type = typeof(T);
            foreach (E_GY_DATALAYOUT2 e in _EDataLayout2)
            {
                if (e.DEFAULTVALUE.IsNullOrEmpty()) continue;
                // 不在循环处理,通过查找的方式，
                var _Properties = type.GetProperties().Where(o => o.Name.ToUpper() == e.FIELDNAME.ToUpper()).FirstOrDefault();
                if (_Properties != null)
                {
                    _Properties.SetValue(t, e.DEFAULTVALUE, null);
                    continue;
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                foreach (E_GY_DATALAYOUT2 e in _EDataLayout2)
                {
                    if (e.DEFAULTVALUE.IsNullOrEmpty()) continue;

                    if (e.FIELDNAME.ToUpper() == p.Name.ToUpper())
                    {
                        p.SetValue(t, e.DEFAULTVALUE, null);
                        break;
                    }
                }
            }
            _DTOBase = t;
            RegisterItemEvent();
        }

        #endregion 公共方法

        #region 初始化构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediDataLayoutControl()
        {
            InitDataLayOutControlEx();
            if (!SkinCat.Instance.IsDesignMode)
            {
                // DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("微软雅黑", 11f);
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitDataLayOutControlEx()
        {
            this.Dock = DockStyle.Fill;

            if (SkinCat.Instance.IsDesignMode)
            {
                this.Padding = new System.Windows.Forms.Padding(0);

                this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 4, 4, 4);
            }
            this.BackColor = System.Drawing.Color.White;
            this.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.Color.Black;
            this.OptionsFocus.EnableAutoTabOrder = false;
            this.AllowCustomization = false;
        }

        #endregion 初始化构造函数

        #region 数据库操作

        /// <summary>
        /// 获取数据布局信息从数据库
        /// </summary>
        private void GetDataLayoutForDB()
        {
            try
            {
                Form form = this.FindForm();
                if (form == null) return;
                string sFormName = form.Name.ToString();
                string sNameSpace = form.GetType().Namespace;
                // 设置界面就不在取了
                if (sFormName == "New2FrmDataLayoutSet")
                {
                    return;
                }
                if (HISClientHelper.YINGYONGID == null) return;

                var ret = GYDataLayoutHelper.GetDataLayoutInfo(this.Name, sFormName, sNameSpace, HISClientHelper.YINGYONGID);

                if (null != ret)
                {
                    _EDataLayout1 = ret.DataLayout1;
                    _EDataLayout2 = ret.DataLayout2;
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        #endregion 数据库操作

        #region 格式化项

        /// <summary>
        /// 格式化非空项颜色（全部）
        /// </summary>
        public void FormatNonEmptyItmes()
        {
            // 必输项字段添加
            if (!(_EDataLayout1 == null || _EDataLayout2 == null || _EDataLayout2.Count == 0))
                foreach (E_GY_DATALAYOUT2 eDataLayout2 in _EDataLayout2)
                    if (!string.IsNullOrEmpty(eDataLayout2.FIELDNAME))
                        if (eDataLayout2.NONEMPTY == 1) // 必输项添加相关字段
                            AddRequiredField(eDataLayout2.FIELDNAME.ToUpper());

            if (_NonEmptyFields == null || _NonEmptyFields.Count == 0) return;

            //string[] nonEmptyFields = new string[100] ;
            //_NonEmptyFields.CopyTo(nonEmptyFields);

            this.BeginUpdate();

            foreach (BaseLayoutItem item in this.Items)
            {
                if (item is LayoutControlItem)
                {
                    if (this.HiddenItems.Contains(item)) continue;

                    var itemLayout = item as LayoutControlItem;
                    Control c = (itemLayout).Control;
                    if (c == null) continue;

                    string caption = (itemLayout).Text;

                    if (c is BaseEdit)
                    {
                        //  BaseEdit baseEdit = c as BaseEdit;
                        if (c.DataBindings == null || c.DataBindings.Count == 0) continue;

                        string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                        // 用户设置必输项
                        if (_NonEmptyFields.Contains(fieldName.ToUpper()))
                            item.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                        else
                            item.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }

            this.EndUpdate();
        }

        /// <summary>
        /// 格式化必输项颜色
        /// </summary>
        /// <param name="nonEmptyItemsList"></param>
        public void FormatNonEmptyItmes(List<LayoutControlItem> nonEmptyItemsList)
        {
            if (nonEmptyItemsList == null || nonEmptyItemsList.Count == 0) return;

            this.BeginUpdate();

            try
            {
                foreach (LayoutControlItem item in nonEmptyItemsList)
                {
                    if (this.HiddenItems.Contains(item)) continue;
                    Control c = (item).Control;
                    string caption = (item).Text;
                    if (c == null) continue;

                    if (c is BaseEdit)
                    {
                        //  BaseEdit baseEdit = c as BaseEdit;
                        if (c.DataBindings == null || c.DataBindings.Count == 0) continue;

                        string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;

                        AddRequiredField(fieldName);

                        item.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        /// <summary>
        /// 格式化项目
        /// </summary>
        private void FomatEditItems()
        {
            try
            {
                if (!ControlCommonHelper.IsDesignMode())
                {
                    this.BeginUpdate();
                    foreach (BaseLayoutItem item in this.Items)
                    {
                        if (item is LayoutControlItem)
                        {
                            SetControlStyle(item as LayoutControlItem);
                        }
                    }
                    FormatNonEmptyItmes();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.EndUpdate();
            }
        }

        /// <summary>
        /// 设置控件样式
        /// </summary>
        /// <param name="layItem"></param>
        private void SetControlStyle(LayoutControlItem layItem)
        {
            // 获取ControlItem包含的控件
            Control control = layItem.Control;

            if (control == null) return;

            if (control is BaseEdit)
            {
                BaseEdit baseEdit = control as BaseEdit;

                if (baseEdit == null) return;
                baseEdit.TabStop = true;
                //baseEdit.ReadOnly = _ReaderOnly;

                // 按回车光标移动到下一行
                baseEdit.EnterMoveNextControl = true;

                if (control.DataBindings == null || control.DataBindings.Count == 0) return;

                // 获取该控件绑定的数据字段
                string sFeildName = control.DataBindings[0].BindingMemberInfo.BindingField;
                if (_EDataLayout2 == null)
                    return;
                // 获取该字段的设置
                List<E_GY_DATALAYOUT2> colSet = _EDataLayout2.Where(x => x.FIELDNAME == sFeildName).ToList();

                if (colSet == null || colSet.Count == 0) return;

                E_GY_DATALAYOUT2 eDataLayout2 = colSet[0];

                // 中文名
                layItem.Text = eDataLayout2.CAPTION;

                // 字体大小
                //layItem.AppearanceItemCaption.Font = new System.Drawing.Font(layItem.AppearanceItemCaption.Font.FontFamily, eDataLayout2.HEADERFONTSIZE.ToInt(9));

                // 必输项 合并为非空表达式
                if (!eDataLayout2.NONEMPTY.ToString().IsNullOrEmpty())
                    layItem.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                else
                    layItem.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;

                // 文本对齐方式
                layItem.AppearanceItemCaption.TextOptions.HAlignment = eDataLayout2.HEADERHALIGNMENT.ToInt(0).ToEnum<HorzAlignment>();

                // 输入法模式
                baseEdit.ImeMode = eDataLayout2.IMEMODE.ToEnum<ImeMode>();

                // 跳转顺序
                baseEdit.TabIndex = eDataLayout2.TABINDEX.ToInt(0);

                // 显示标志
                layItem.ContentVisible = eDataLayout2.VISIBLE.ToInt(1) == 0 ? false : true;
                baseEdit.Visible = eDataLayout2.VISIBLE.ToInt(1) == 0 ? false : true;
                ////有效性检查
                //baseEdit.Validating -= BaseEdit_Validating;
                //baseEdit.Validating += BaseEdit_Validating;
                // 初始值

                // 保护
                baseEdit.ReadOnly = eDataLayout2.READONLY.ToInt(0) == 0 ? false : true;
                //baseEdit.Enabled = eDataLayout2.READONLY.ToInt(0) == 0 ? true : false;
            }
        }

        /// <summary>
        /// 设置DataLayout默认值
        /// </summary>
        private void GetDefaultDataLayoutValue()
        {
            DataLayOutDefaultValue = new E_GY_DATALAYOUTDTO();
            DataLayOutDefaultValue.DataLayout1 = new E_GY_DATALAYOUT1();

            List<E_GY_DATALAYOUT2> e_GY_DATALAYOUT2s = new List<E_GY_DATALAYOUT2>();
            DataLayOutDefaultValue.DataLayout1.YINGYONGID = HISClientHelper.YINGYONGID;
            DataLayOutDefaultValue.DataLayout1.FORMNAME = this.FindForm().Name;
            DataLayOutDefaultValue.DataLayout1.CONTROLNAME = this.Name;
            DataLayOutDefaultValue.DataLayout1.NAMESPACE = this.FindForm().GetType().Namespace;
            DataLayOutDefaultValue.DataLayout1.ALLOWFILTER = 0;
            DataLayOutDefaultValue.DataLayout1.ALLOWSORT = 0;
            DataLayOutDefaultValue.DataLayout1.ENABLECOLUMNMENU = 0;
            DataLayOutDefaultValue.DataLayout1.ORDERBYFIELD = "";
            DataLayOutDefaultValue.DataLayout1.SHOWGROUPPANEL = 0;
            DataLayOutDefaultValue.DataLayout1.LINENUMBER = 0;
            DataLayOutDefaultValue.DataLayout1.ROWBACKCOLOREXPRESSION = "";
            DataLayOutDefaultValue.DataLayout1.ROWBACKCOLORDESCRIBE = "";
            DataLayOutDefaultValue.DataLayout1.ROWFONTSIZE = 9;

            RecursionLayoutControl(this.Root, ref e_GY_DATALAYOUT2s);

            DataLayOutDefaultValue.DataLayout2 = e_GY_DATALAYOUT2s;
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

                        // 获取数据条
                        LayoutControlItem layItem = item as LayoutControlItem;

                        // 获取ControlItem包含的控件
                        BaseEdit control = layItem.Control as BaseEdit;
                        //if (control != null)
                        //    control.Validated += Control_Validated;

                        // 获取该控件绑定的数据字段
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

                        #endregion 格式化项
                    }
                }
            }
        }

        /// <summary>
        /// 单个控件值验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Validated(object sender, EventArgs e)
        {
            if (_EDataLayout2 != null)
            {
                if (sender is BaseEdit)
                {
                    foreach (var item in _EDataLayout2)
                    {
                        if (item.FIELDNAME.ToUpper().Equals(((BaseEdit)sender).DataBindings[0].BindingMemberInfo.BindingField.ToUpper()))
                        {
                            if (item.NONEMPTY != null && item.NONEMPTY == 1)
                            {
                                if (string.IsNullOrWhiteSpace(((BaseEdit)sender).EditValue.ToStringEx()))
                                {
                                    MediMsgBox.Show("当前文本值不允许为空!");
                                    ((BaseEdit)sender).Focus();
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 重写初始化方法

        /// <summary>
        /// 声明初始化布局委托
        /// </summary>
        public delegate void InitialDataLayoutDel();

        /// <summary>
        /// 声明委托变量
        /// </summary>
        public InitialDataLayoutDel initialDataLayoutDel;

        /// <summary>
        /// 初始化页面布局
        /// </summary>
        public void InitialDataLayout()
        {
            GetDefaultDataLayoutValue();
            GetDataLayoutForDB();
        }

        /// <summary>
        /// 数据源改变
        /// </summary>
        protected override void OnDataSourceChanged()
        {
            base.OnDataSourceChanged();
            if (this.DataSource != null)
            {
                (this.DataSource as BindingSource).AddingNew -= MediDataLayoutControl_AddingNew;
                (this.DataSource as BindingSource).AddingNew += MediDataLayoutControl_AddingNew;
            }
        }

        /// <summary>
        /// 结束初始化
        /// </summary>
        public override void EndInit()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                RegisterEvent();
                initialDataLayoutDel = InitialDataLayout;
                FomatEditItems();
                RegisterItemEvent();
            }

            base.EndInit();
        }

        #endregion

        #region 事件相关

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            this.PopupMenuShowing += DataLayOutControlEx_PopupMenuShowing;
        }

        private void RegisterItemEvent()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                // 注册非空表达式的事件
                foreach (BaseLayoutItem item in this.Items)
                {
                    if (item is LayoutControlItem)
                    {
                        Control c = (item as LayoutControlItem).Control;
                        if (c == null) continue;

                        string caption = (item as LayoutControlItem).Text;
                        if (c is BaseEdit)
                        {
                            BaseEdit baseEdit = c as BaseEdit;

                            // if (baseEdit.EditValue == null) continue;
                            if (c.DataBindings == null || c.DataBindings.Count == 0) continue;

                            string fieldName = c.DataBindings[0].BindingMemberInfo.BindingMember;
                            if (_EDataLayout2 != null)
                            {
                                // 屏幕下面的foreach，通过查找方式处理
                                var ret = _EDataLayout2.Where(o => o.FIELDNAME.ToUpper() == fieldName.ToUpper()).FirstOrDefault();
                                if (ret != null)
                                {
                                    baseEdit.Validated += DataLayOutControlEx_Validated;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DataLayOutControlEx_Validated(object sender, EventArgs e)
        {
            BaseEdit item = sender as BaseEdit;
            string fieldName = item.DataBindings[0].BindingMemberInfo.BindingMember;
            var ret = _EDataLayout2.Where(o => o.NONEMPTY != null && o.FIELDNAME.ToUpper() == fieldName.ToUpper()).FirstOrDefault();
            if (ret != null)
            {
                string expression = ret.NONEMPTY.ToString();
            }
        }

        /// <summary>
        /// 添加新行可以设置默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MediDataLayoutControl_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (_DTOBase != null)
            {
                e.NewObject = _DTOBase.Clone();
            }
        }

        /// <summary>
        /// 双击弹出设置窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void LayItem_DoubleClick(object sender, EventArgs e)
        //{
        //    if (((MouseEventArgs)e).Button == MouseButtons.Right && HISClientHelper.USERID=="DBA")
        //    {
        //        string sFormName = this.FindForm().Name.ToString();
        //        string sNameSpace = this.FindForm().GetType().Namespace;

        //        FrmDataLayoutSet frm = new FrmDataLayoutSet(sFormName, this.Name, sNameSpace, this);

        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            if (this._EDataLayout1 != null)
        //            {
        //                GYDataLayoutHelper.RefreshDataLayoutInfo(sFormName, this.Name, sNameSpace);
        //            }

        //            GetDataLayoutForDB();

        //            FomatEditItems();
        //        }
        //    }
        //}

        /// <summary>
        /// 弹出菜单设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataLayOutControlEx_PopupMenuShowing(object sender, DevExpress.XtraLayout.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.IsGroup)
            {
                e.Allow = false; return;
            }

            if (e.Menu.MenuViewType == DevExpress.Utils.Menu.MenuViewType.Menu && HISClientHelper.USERID == "DBA")
            {
                bool bExist = false;
                foreach (DXMenuItem dx in e.Menu.Items)
                {
                    if (dx.Caption == "列设置")
                        bExist = true;
                }

                if (!bExist)
                {
                    DXMenuItem item = new DXMenuItem("列设置");
                    item.Click += new EventHandler(delegate (object sd, EventArgs ce)
                    {
                        string sFormName = this.FindForm().Name.ToString();
                        string sNameSpace = this.FindForm().GetType().Namespace;
                        DataLayoutStyleSetFrm frm = new DataLayoutStyleSetFrm(sFormName, this.Name, sNameSpace, this);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            if (frm.Tag != null)
                            {
                                if (frm.Tag.ToString().Equals("RESET"))
                                {
                                    _EDataLayout1 = DataLayOutDefaultValue.DataLayout1;
                                    _EDataLayout2 = DataLayOutDefaultValue.DataLayout2;
                                }
                                else
                                {
                                    E_GY_DATALAYOUTDTO e_GY_DATALAYOUTDTO = frm.Tag as E_GY_DATALAYOUTDTO;
                                    _EDataLayout1 = e_GY_DATALAYOUTDTO.DataLayout1;
                                    _EDataLayout2 = e_GY_DATALAYOUTDTO.DataLayout2;
                                }
                            }
                        }
                        frm.Dispose();
                        //GetDataLayoutForDB();
                        FomatEditItems();
                    });

                    e.Menu.Items.Add(item);
                }
            }
        }

        #endregion

        #region 重写创建控件方法

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override BaseLayoutItem CreateLayoutItem(LayoutGroup parent)
        {
            BaseLayoutItem baseItem = base.CreateLayoutItem(parent);
            baseItem.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            // baseItem.AppearanceItemCaption.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            return baseItem;
        }

        /// <summary>
        /// 创建LayoutGroup
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override LayoutGroup CreateLayoutGroup(LayoutGroup parent)
        {
            LayoutGroup layGroup = base.CreateLayoutGroup(parent);

            layGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5);

            return layGroup;
        }

        /// <summary>
        /// 创建Layout生成器
        /// </summary>
        /// <returns></returns>
        public override LayoutCreator CreateLayoutCreator()
        {
            return new MediLayoutCreator(this);
        }

        /// <summary>
        /// 创建控件生成器
        /// </summary>
        /// <returns></returns>
        protected override ControlsManager CreateControlsManager()
        {
            return new CustomControlsManager();
        }

        /// <summary>
        ///  设置控件项的外观
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        protected override RepositoryItem GetRepositoryItem(LayoutElementBindingInfo bi)
        {
            RepositoryItem rep = base.GetRepositoryItem(bi);
            //rep.ReadOnly = _ReaderOnly;

            //if (this.DesignMode)
            //{
            //    rep.AppearanceFocused.Options.UseBorderColor = true;
            //    //外观边框颜色
            //    System.Drawing.Color AppearanceBorderColor = System.Drawing.Color.FromArgb(205, 205, 205);
            //    //选中边框颜色
            //    System.Drawing.Color FocusedBorderColor = System.Drawing.Color.FromArgb(64, 169, 216);

            //    rep.Appearance.BorderColor = AppearanceBorderColor;
            //    rep.AppearanceFocused.BorderColor = FocusedBorderColor;

            //    rep.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //}
            return rep;
        }

        #endregion

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
           
            this.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }

    /// <summary>
    /// 重写布局控件生成器
    /// </summary>
    public class MediLayoutCreator : LayoutCreator
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <param name="dataLayoutControl"></param>
        public MediLayoutCreator(DataLayoutControl dataLayoutControl) : base(dataLayoutControl)
        {

        }

        /// <summary>
        /// 设计时
        /// </summary>
        /// <param name="elementBi"></param>
        /// <returns></returns>
        protected override Control CreateBindableControlDesignTime(LayoutElementBindingInfo elementBi)
        {
            Control customControl = base.CreateBindableControlDesignTime(elementBi);
            customControl.ImeMode = ImeMode.NoControl;
            return customControl;
        }
    }

    /// <summary>
    /// 控件管理类
    /// </summary>
    public class CustomControlsManager : ControlsManager
    {
        public CustomControlsManager() : base()
        {
        }

        public override Type[] GetOtherControls(Type[] inControls)
        {
            Type[] typs = base.GetOtherControls(inControls);
            return typs;
        }

        public override string GetSuggestedBindingProperty(Type editorType)
        {
            string type = base.GetSuggestedBindingProperty(editorType);
            return type;
        }

        public override Type GetSuggestedNavigator()
        {
            Type type = base.GetSuggestedNavigator();
            return type;
        }


        public override Type[] GetSuggestedControls(Type dataType)
        {
            List<Type> suggestedTypes = new List<Type>(base.GetSuggestedControls(dataType));
            if (dataType == typeof(string))
            {
                suggestedTypes[0] = typeof(MediTextBox);
            }
            return suggestedTypes.ToArray();
        }
    }
}