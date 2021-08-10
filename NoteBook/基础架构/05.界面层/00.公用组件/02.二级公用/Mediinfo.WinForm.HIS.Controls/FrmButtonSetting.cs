using DevExpress.XtraEditors;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class FrmButtonSetting : MediForm
    {
        #region 构造函数

        /// <summary>
        /// 窗口按钮设置
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="fromName">窗口名称</param>
        /// <param name="controlList">按钮集合</param>
        /// <param name="gongNengId">功能ID</param>
        public FrmButtonSetting(string nameSpace, string fromName, List<Control> controlList, string gongNengId)
        {
            InitializeComponent();

            this.formNameSpace = nameSpace;
            this.formName = fromName;
            this.ControlList = controlList;
            GongNengId = gongNengId;
        }

        #endregion

        #region 变量

        private List<ControlItemProperty> controlItemList = null;

        private string formNameSpace;
        private string formName;
        private string formText;
        private string GongNengId;

        private List<Control> ControlList = null;
        private List<E_GY_CHUANGKOUZY_NEW> chuangKouZYList = null;
        private JCJGChuangKouZYService chuangKouZYService = new JCJGChuangKouZYService();
        private JCJGZhiGongService gyZhiGongService = new JCJGZhiGongService();

        #endregion

        #region 事件

        /// <summary>
        /// 首次加载窗口时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmButtonSetting_Shown(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.formText = this.Owner.Text;
            }

            this.mediTitleBarControl.LabelText = string.Format("{0}【{1}.{2}】", this.formText, this.formNameSpace, this.formName);

            controlItemList = new List<ControlItemProperty>();

            chuangKouZYService = new JCJGChuangKouZYService();

            var ret = chuangKouZYService.GetByFromName(this.formNameSpace, this.formName);
            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                chuangKouZYList = ret.Return;
            }
            else
            {
                chuangKouZYList = new List<E_GY_CHUANGKOUZY_NEW>();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ControlName", typeof(string)));
            dt.Columns.Add(new DataColumn("ControlText", typeof(string)));

            foreach (var item in ControlList)
            {
                if (!(item is SimpleButton))
                    continue;

                DataRow dr = dt.NewRow();

                dr["ControlName"] = item.Name;
                dr["ControlText"] = item.Text;
                dt.Rows.Add(dr);

                var controlItem = new ControlItemProperty();
                controlItem.FormName = this.formName;
                controlItem.NameSpace = this.formNameSpace;
                controlItem.名称 = item.Name;

                var chuangKouZY = chuangKouZYList.Where(c => c.CONTROLNAME == item.Name).FirstOrDefault();

                if (null == chuangKouZY)
                {
                    controlItem.启用权限 = false;
                    controlItem.显示 = item.Visible;
                    controlItem.文字 = item.Text;
                    controlItem.启用显示设置 = false;
                }
                else
                {
                    controlItem.启用权限 = ((chuangKouZY.QUANXIANKZ.HasValue && chuangKouZY.QUANXIANKZ.Value != 0) ? true : false);

                    controlItem.启用显示设置 = ((chuangKouZY.XIANSHIKZ.HasValue && chuangKouZY.XIANSHIKZ.Value != 0) ? true : false);
                    controlItem.显示 = ((controlItem.启用显示设置 && chuangKouZY.XIANSHIBZ.HasValue) ? (chuangKouZY.XIANSHIBZ.Value == 0 ? false : true) : item.Visible);
                    controlItem.文字 = ((controlItem.启用显示设置 && !string.IsNullOrWhiteSpace(chuangKouZY.WENZI)) ? chuangKouZY.WENZI : item.Text);
                }

                controlItem.权限ID = string.Format("{0}.{1}.{2}", this.formNameSpace, this.formName, item.Name);
                //var juseQuanXian = gyZhiGongService.GetJueSeCKQXByID(HISClientHelper.YINGYONGID + "." + GongNengId + "." + controlItem.权限ID);
                var juseQuanXian = gyZhiGongService.GetYongHuCKQXByIDNEW(HISClientHelper.YINGYONGID + "." + GongNengId + "." + controlItem.权限ID);
                if (juseQuanXian.ReturnCode == ReturnCode.SUCCESS)
                    controlItem.权限 = juseQuanXian.Return;
                else
                    controlItem.权限 = null;
                controlItem.权限名称 = string.Format("{0}.{1}", this.formText, item.Text);

                controlItemList.Add(controlItem);
            }

            this.mediListBoxBtns.DataSource = dt;
            this.mediListBoxBtns.DisplayMember = "ControlText";

            this.mediButtonSave.Enabled = false;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            List<E_GY_CHUANGKOUZY_NEW> list = new List<E_GY_CHUANGKOUZY_NEW>();
            List<E_GY_JUESECKQX_NEW> jueSeCKQX = new List<E_GY_JUESECKQX_NEW>();

            foreach (var item in controlItemList)
            {
                E_GY_CHUANGKOUZY_NEW chuangkouzy_new = new E_GY_CHUANGKOUZY_NEW();

                chuangkouzy_new.CHUANGKOUZYID = item.CHUANGKOUZYID;
                chuangkouzy_new.FORMNAME = this.formName;
                chuangkouzy_new.NAMESPACE = this.formNameSpace;
                chuangkouzy_new.CONTROLNAME = item.名称;
                chuangkouzy_new.QUANXIANKZ = (item.启用权限 ? 1 : 0);
                chuangkouzy_new.XIANSHIKZ = (item.启用显示设置 ? 1 : 0);//
                chuangkouzy_new.XIANSHIBZ = (item.显示 ? 1 : 0);
                chuangkouzy_new.WENZI = item.文字;
                chuangkouzy_new.FULLTEXT = string.Format("{0}.{1}", this.formText, item.文字);
                chuangkouzy_new.SetTraceChange(true);
                chuangkouzy_new.SetState(DTOState.New);
                list.Add(chuangkouzy_new);
                if (item.权限 != null && item.权限.Count > 0)
                    //jueSeCKQX = item.权限;
                    jueSeCKQX.AddRange(item.权限.GetChanges());
            }

            // 重置窗口个性化设置
            chuangKouZYService.Reset(this.formNameSpace, this.formName);
            // 保存窗口个性化设置
            var ret = chuangKouZYService.Save(list, jueSeCKQX);
            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                MediMsgBox.Success(this, "保存成功！");
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", ret);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonResetAll_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MediMsgBox.YesNo(this, "您将重置所有控件的自定义信息，是否继续？", MessageBoxDefaultButton.Button2))
                return;

            var ret = this.chuangKouZYService.Reset(this.formNameSpace, this.formName);

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                MediMsgBox.Success(this, "重置成功！");
                foreach (var item in controlItemList)
                {
                    item.启用显示设置 = false;
                    item.启用权限 = false;
                    item.文字 = item.名称;
                }
            }
            else
            {
                MediMsgBox.Failure(this, "重置失败！", ret);
            }
        }

        private void mediListBoxBtns_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dr = this.mediListBoxBtns.SelectedItem as DataRowView;

            if (null == dr) return;

            this.mediPropertyGrid1.SelectedObject = this.controlItemList.Where(c => c.名称 == dr["ControlName"].ToString()).FirstOrDefault();
        }

        private void mediButtonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void mediPropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.mediButtonSave.Enabled = true;
            this.mediListBoxBtns.Invalidate();
        }

        private void mediListBoxBtns_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            DataRowView dataRow = (e.Item as DataRowView);
            object o = this.mediListBoxBtns.SelectedItem;

            if (o != null)
            {
                var item = this.controlItemList.Where(c => c.名称 == ((DataRowView)o).Row[0].ToString()).FirstOrDefault();
                if (item.启用显示设置 || item.启用权限)
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                //else
                //    e.Appearance.FontStyleDelta = FontStyle.Italic;
                //&& ((DataRowView)o).Row[0].ToString().Equals(dataRow.Row[0].ToString()
            }
        }

        private void FrmButtonSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (Belongform != null && Belongform is MediFormWithQX)
            //{
            //    MediFormWithQX mediFormWithQX = (MediFormWithQX)Belongform;
            //    if (mediFormWithQX.RefreshCurrentResetFormFun != null)
            //    {
            //        //mediFormWithQX.RefreshCurrentResetFormFun();
            //    }
            //}
        }

        #endregion

        public class PropertySorter : ExpandableObjectConverter
        {
            #region Methods

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                //
                // This override returns a list of properties in order
                //
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);
                ArrayList orderedProperties = new ArrayList();
                foreach (PropertyDescriptor pd in pdc)
                {
                    Attribute attribute = pd.Attributes[typeof(PropertyOrderAttribute)];
                    if (attribute != null)
                    {
                        //
                        // If the attribute is found, then create an pair object to hold it
                        //
                        PropertyOrderAttribute poa = (PropertyOrderAttribute)attribute;
                        orderedProperties.Add(new PropertyOrderPair(pd.Name, poa.Order));
                    }
                    else
                    {
                        //
                        // If no order attribute is specifed then given it an order of 0
                        //
                        orderedProperties.Add(new PropertyOrderPair(pd.Name, 0));
                    }
                }
                //
                // Perform the actual order using the value PropertyOrderPair classes
                // implementation of IComparable to sort
                //
                orderedProperties.Sort();
                //
                // Build a string list of the ordered names
                //
                ArrayList propertyNames = new ArrayList();
                foreach (PropertyOrderPair pop in orderedProperties)
                {
                    propertyNames.Add(pop.Name);
                }
                //
                // Pass in the ordered list for the PropertyDescriptorCollection to sort by
                //
                return pdc.Sort((string[])propertyNames.ToArray(typeof(string)));
            }

            #endregion Methods
        }

        #region Helper Class - PropertyOrderAttribute

        [AttributeUsage(AttributeTargets.Property)]
        public class PropertyOrderAttribute : Attribute
        {
            //
            // Simple attribute to allow the order of a property to be specified
            //
            private int _order;

            public PropertyOrderAttribute(int order)
            {
                _order = order;
            }

            public int Order
            {
                get
                {
                    return _order;
                }
            }
        }

        #endregion Helper Class - PropertyOrderAttribute

        #region Helper Class - PropertyOrderPair

        public class PropertyOrderPair : IComparable
        {
            private int _order;
            private string _name;

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public PropertyOrderPair(string name, int order)
            {
                _order = order;
                _name = name;
            }

            public int CompareTo(object obj)
            {
                //
                // Sort the pair objects by ordering by order value
                // Equal values get the same rank
                //
                int otherOrder = ((PropertyOrderPair)obj)._order;
                if (otherOrder == _order)
                {
                    //
                    // If order not specified, sort by name
                    //
                    string otherName = ((PropertyOrderPair)obj)._name;
                    return string.Compare(_name, otherName);
                }
                else if (otherOrder > _order)
                {
                    return -1;
                }
                return 1;
            }
        }

        #endregion Helper Class - PropertyOrderPair

        [TypeConverter(typeof(PropertySorter))]
        public class ControlItemProperty
        {
            [Browsable(false)]
            public string CHUANGKOUZYID { get; set; }

            [Browsable(false)]
            public string NameSpace { get; set; }

            [Browsable(false)]
            public string FormName { get; set; }

            [ReadOnly(true), Category("基本信息"), PropertyOrder(10)]
            public string 名称 { get; set; }


            private bool _启用显示设置 = false;

            [Category("显示设置"), ReadOnly(false), DefaultValue(false), PropertyOrder(20)]
            [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
            [TypeConverter(typeof(BoolValueConverter))]
            public bool 启用显示设置
            {
                get
                {
                    return _启用显示设置;
                }
                set
                {
                    _启用显示设置 = value;

                    SetPropertyReadOnly("文字", !_启用显示设置);
                    SetPropertyReadOnly("显示", !_启用显示设置);
                }
            }

            [Category("显示设置"), ReadOnly(true), Description("设置按钮是否可见"), PropertyOrder(21)]
            [TypeConverter(typeof(BoolValueConverter))]
            public bool 显示 { get; set; }

            [Category("显示设置"), ReadOnly(true), Description("设置按钮的显示文字"), PropertyOrder(22)]
            public string 文字 { get; set; }

            [Category("权限控制"), ReadOnly(true), Description("控件所对应的权限ID"), PropertyOrder(30)]
            public string 权限ID { get; set; }

            [Category("权限控制"), ReadOnly(true), Description("控件所对应的权限名称"), PropertyOrder(31)]
            public string 权限名称 { get; set; }

            private bool _启用权限 = false;

            [Category("权限控制"), DefaultValue(false), ReadOnly(false), Description("设置是否启用权限控制"), PropertyOrder(32)]
            [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
            [TypeConverter(typeof(BoolValueConverter))]
            public bool 启用权限
            {
                get
                {
                    return _启用权限;
                }
                set
                {
                    _启用权限 = value;
                }
            }

            /// <summary>
            /// 权限集合
            /// </summary>
            [Browsable(true)]
            [Category("权限控制"), ReadOnly(false), Description("设置哪些角色拥有权限"), PropertyOrder(33)]
            [Editor(typeof(QuanXianEditor), typeof(System.Drawing.Design.UITypeEditor))]
            [TypeConverter(typeof(QuanXianTypeConverter))]
            public List<E_GY_JUESECKQX_NEW> 权限 { get; set; }

            private void SetPropertyReadOnly(string propertyName, bool readOnly)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);

                if (props[propertyName] == null) return;

                AttributeCollection attrs = props[propertyName].Attributes;

                Type type = typeof(System.ComponentModel.ReadOnlyAttribute);
                FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);

                fld.SetValue(attrs[type], readOnly);
            }
        }

       public class BoolValueConverter : TypeConverter
        {
            private bool[] values;
            private string[] names;
            public BoolValueConverter()
            {
                values = new bool[2] { true, false };
            }
            public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
            {
                return true;
            }
            public override StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(values);
            }
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    if ((bool)value == true)
                        return "是";
                    else
                        return "否";
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value.GetType() == typeof(string))
                {
                    if ((string)value == "是")
                        return true;
                    else
                        return false;
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        private class QuanXianTypeConverter : TypeConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) && value is List<E_GY_JUESECKQX_NEW>)
                {
                    return "(集合)";
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
        protected class QuanXianEditor : UITypeEditor
        {
            private List<string> list = new List<string>();

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance != null)
                {
                    return UITypeEditorEditStyle.Modal;
                }

                return base.GetEditStyle(context);
            }

            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    //myform f = new myform();
                    // your setting here
                    //edSvc.ShowDialog(f);
                    //MediMsgBox.Info("sdfad");
                    Control cts = ((System.Windows.Forms.Control)edSvc).TopLevelControl;

                    if (cts is FrmButtonSetting)
                    {
                        FrmButtonSetting frms = (FrmButtonSetting)cts;
                        Mediinfo.WinForm.HIS.Controls.FrmButtonSetting.ControlItemProperty controlItemProperty = (ControlItemProperty)context.Instance;

                        if (!controlItemProperty.启用权限)
                        {
                            MediMsgBox.Warn("请启用权限!");
                            return new List<E_GY_JUESECKQX_NEW>();
                        }
                        using (MediPanelButtonQX mediErJiQXGJForm = new MediPanelButtonQX(frms.formNameSpace, frms.formName, frms.ControlList, frms.GongNengId, controlItemProperty.权限ID, controlItemProperty.权限名称, controlItemProperty.名称))
                        {
                            if (edSvc.ShowDialog(mediErJiQXGJForm) == DialogResult.OK)
                            {
                                value = mediErJiQXGJForm.Tag;
                                return value;
                            }
                            value = mediErJiQXGJForm.Tag;
                        }
                    }
                }
                return value as List<E_GY_JUESECKQX_NEW>;
            }

            public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
            {
                return false;
            }
        }

        private void FrmButtonSetting_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 保存并关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton1_Click(object sender, EventArgs e)
        {
            List<E_GY_CHUANGKOUZY_NEW> list = new List<E_GY_CHUANGKOUZY_NEW>();
            List<E_GY_JUESECKQX_NEW> jueSeCKQX = new List<E_GY_JUESECKQX_NEW>();

            foreach (var item in controlItemList)
            {
                E_GY_CHUANGKOUZY_NEW chuangkouzy_new = new E_GY_CHUANGKOUZY_NEW();

                chuangkouzy_new.CHUANGKOUZYID = item.CHUANGKOUZYID;
                chuangkouzy_new.FORMNAME = this.formName;
                chuangkouzy_new.NAMESPACE = this.formNameSpace;
                chuangkouzy_new.CONTROLNAME = item.名称;
                chuangkouzy_new.QUANXIANKZ = (item.启用权限 ? 1 : 0);
                chuangkouzy_new.XIANSHIKZ = (item.启用显示设置 ? 1 : 0);//
                chuangkouzy_new.XIANSHIBZ = (item.显示 ? 1 : 0);
                chuangkouzy_new.WENZI = item.文字;
                chuangkouzy_new.FULLTEXT = string.Format("{0}.{1}", this.formText, item.文字);
                chuangkouzy_new.SetTraceChange(true);
                chuangkouzy_new.SetState(DTOState.New);
                list.Add(chuangkouzy_new);
                if (item.权限 != null)
                    //jueSeCKQX = item.权限;
                    jueSeCKQX.AddRange(item.权限.GetChanges());
            }

            //E_GY_JUESECKQX
            this.chuangKouZYService.Reset(this.formNameSpace, this.formName);
            var ret = chuangKouZYService.Save(list, jueSeCKQX);
            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                MediMsgBox.Success(this, "保存成功！");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", ret);
            }
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmButtonSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                mediButtonSave_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                mediButtonResetAll_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                mediButtonExit_Click(null, null);
            }
        }
    }
}