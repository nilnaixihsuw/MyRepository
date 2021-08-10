using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using Mediinfo.Utility;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 文本框的数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 默认不设定格式
        /// </summary>
        [Description("不设定格式")]
        Default = 0,

        /// <summary>
        /// 整数
        /// </summary>
        [Description("整数")]
        Int = 1,

        /// <summary>
        /// 正整数
        /// </summary>
        [Description("正整数")]
        PositiveInt = 2,

        /// <summary>
        /// 浮点型
        /// </summary>
        [Description("浮点型")]
        Decimal = 3,

        /// <summary>
        /// 正浮点型
        /// </summary>
        [Description("正浮点型")]
        PositiveDecimal = 4,

        /// <summary>
        /// 身份证号
        /// </summary>
        [Description("身份证号")]
        IDCard = 5,

        /// <summary>
        /// 电话号码
        /// </summary>
        [Description("电话号码")]
        PhoneNumber = 6,

        /// <summary>
        /// 邮编
        /// </summary>
        [Description("邮编")]
        PostCode = 7,

        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        PassWord = 8,

        /// <summary>
        /// 电子邮件
        /// </summary>
        [Description("电子邮件")]
        Email = 9
    }

    /// <summary>
    /// 单行文本编辑器(网格中)
    /// </summary>
    [UserRepositoryItem("RegisterMediTextBox")]
    public class RepositoryItemMediTextBox : RepositoryItemTextEdit, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 表达式属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static RepositoryItemMediTextBox()
        {
            RegisterMediTextBox();
        }

        public const string CustomEditName = "MediTextBox";

        public RepositoryItemMediTextBox()
        {
             Init();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return CustomEditName;
            }
        }

        public static void RegisterMediTextBox()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediTextBox), typeof(RepositoryItemMediTextBox), typeof(MediTextBoxViewInfo), new MediTextBoxPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediTextBox source = item as RepositoryItemMediTextBox;
                if (source == null) return;
                //
            }
            finally
            {
                EndUpdate();
            }
        }

        private bool readOnly = false;

        #region 自定义皮肤相关

        /// <summary>
        /// 通过皮肤获取控件信息
        /// </summary>
        protected virtual void Init()
        {
            this.BeginInit();

            try
            {
                //禁止textedit中使用滚轮， maskType设置成Numeric时候 鼠标滚轮滚动数字会自动增加
                AllowMouseWheel = false;
              
                //this.SetCustomSkin();
            }
            finally
            {
                this.EndInit();
            }
        }

        #endregion 自定义皮肤相关

        #region 自定义函数

        private void ControlType(DataType dataType)
        {
            if (dataType == DataType.PositiveDecimal)
            {
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

                this.Mask.EditMask = "[0-9]+\\.?[0-9]{0,4}";
                this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.Appearance.Options.UseTextOptions = true;
                //this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                //this.DisplayFormat.FormatString = "";
            }
            else if (dataType == DataType.Decimal)
            {
                //AllowNullInput 对RegEx 无效
                //this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                // this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                //this.Mask.EditMask = "\\-?[0-9]+\\.?[0-9]{0,4}";
                this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.Appearance.Options.UseTextOptions = true;
                //this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                //this.DisplayFormat.FormatString = "";
            }
            else if (dataType == DataType.IDCard)
            {
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.Mask.EditMask = @"(\d{15}|\d{18}|\d{17}(\d|X|x))";//原验证会出现前面几位无法删除，只能从后往前删，所有改这个验证--modify by zhb 2019-06-12
                //this.Mask.EditMask = "[1-9]\\d{5}(18|19|([23]\\d))\\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\\d{3}[0-9Xx])|([1-9]\\d{5}\\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\\d{3}";
                this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                this.DisplayFormat.FormatString = "";
                this.Mask.ShowPlaceHolders = false;
            }
            else if (dataType == DataType.Int)
            {
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                this.Mask.EditMask = "f0";
                //this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                //this.DisplayFormat.FormatString = "";

                this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.Appearance.Options.UseTextOptions = true;
            }
            else if (dataType == DataType.PositiveInt)
            {
                if (ControlCommonHelper.IsDesignMode())
                {
                    this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                    this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    this.Mask.EditMask = "[0-9]+";

                    this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    this.Appearance.Options.UseTextOptions = true;
                }
                //this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                //this.DisplayFormat.FormatString = "";
            }
            else if (dataType == DataType.PassWord)
            {
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.PasswordChar = '*';
            }
            else if (dataType == DataType.PhoneNumber)
            {
                //电话号码书写比较随意，控制规则是否宽松点 13868085218/665218
                this.MaxLength = 13;
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.Mask.EditMask = "[0-9]{1,}[.]{0,1}[0-9]{0,4}";
                this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                this.DisplayFormat.FormatString = "";
                this.Mask.UseMaskAsDisplayFormat = true;
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.Mask.ShowPlaceHolders = false;
            }
            else if (dataType == DataType.PostCode)
            {
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.Mask.EditMask = "\\d{6}";
                this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                this.DisplayFormat.FormatString = "";
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.Mask.ShowPlaceHolders = false;
            }
            else if (dataType == DataType.Email)
            {
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.Mask.EditMask = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                //this.Mask.EditMask = @"\w+([-+.]\w+)*[@]{0,1}\w+([-.]\w+)*\.\w+([-.]\w+)*";

                this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                this.DisplayFormat.FormatString = "";
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.Mask.ShowPlaceHolders = false;
            }
            else
            {
                this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
                this.Mask.EditMask = "";
                this.Mask.UseMaskAsDisplayFormat = false;
                this.DisplayFormat.FormatString = "";
                this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                this.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        #endregion 自定义函数

        #region 自定义属性

        private DataType _dataType = DataType.Default;

        /// <summary>
        /// 字符类型MASK控制
        /// </summary>
        [Browsable(true),DefaultValue(0)]
        public DataType Mask_DataType
        {
            get { return _dataType; }
            set
            {
                _dataType = value;
                ControlType(_dataType);
            }
        }

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        #endregion 自定义属性
    }

    /// <summary>
    /// 单行文本编辑器
    /// 默认回车跳转到下一个控件
    /// </summary>
    [ToolboxItem(true)]
    public class MediTextBox : TextEdit, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression
        {
            get; set;
        }

        static MediTextBox()
        {
            RepositoryItemMediTextBox.RegisterMediTextBox();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
            {
                if (ControlCommonHelper.IsDesignMode())
                {
                    this.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
                    this.Properties.AppearanceDisabled.Options.UseForeColor = true;
                    this.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                    this.Properties.AppearanceDisabled.Options.UseBackColor = true;
                }
            }
            else
            {
                if (ControlCommonHelper.IsDesignMode())
                {
                    this.Properties.AppearanceDisabled.ForeColor = SystemColors.Control;

                    this.Properties.AppearanceDisabled.BackColor = SystemColors.Control;
                }
            }
        }

        public bool IsOpenEnterNext { get; set; }

        private bool focusAllText = true;
        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get { return focusAllText; } set { focusAllText = value; } }

        public MediTextBox()
        {
            this.SetEditorsCustomSkin();
            if (Properties.Mask_DataType == DataType.Email)
            {
                RightToLeft = RightToLeft.No;
            }
            this.GotFocus -= MediTextBox_GotFocus;
            this.GotFocus += MediTextBox_GotFocus;
            this.MouseUp -= MediTextBox_MouseUp;
            this.MouseUp += MediTextBox_MouseUp;
            this.Enter -= MediTextBox_Enter;
            this.Enter += MediTextBox_Enter;
            this.MouseDown -= MediTextBox_MouseDown;
            this.MouseDown += MediTextBox_MouseDown;
            this.MinimumSize = new Size(0, 26);
          
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            this.EnterMoveNextControl = true;
        }

        private void MediTextBox_Enter(object sender, EventArgs e)
        {
            if (this.FindForm()!=null)
            {
                if (this.FindForm().IsHandleCreated)
                {
                    enter = true;
                    BeginInvoke(new MethodInvoker(ResetEnterFlag));
                }
              
            }
            
        }

        private void MediTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }


        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }



        private void MediTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                (sender as TextEdit).SelectAll();
            }

        }


        private void MediTextBox_GotFocus(object sender, EventArgs e)
        {
            if (FocusAllText)
                this.SelectAll();
        }

      
        protected override void OnValidating(CancelEventArgs e)
        {
            string birth = "";
            string xingBie = "";
            string message = "";
            string editValue = Convert.ToString(EditValue);
            if (Properties.Mask_DataType == DataType.IDCard)
            {
                if (!string.IsNullOrEmpty(editValue) && !IDCardValidation.CheckIDCard(Convert.ToString(EditValue), ref birth, ref xingBie, ref message))
                {
                    this.ErrorText = message;
                    e.Cancel = true;
                    // base.OnValidating(e);
                }
                else
                {
                    base.OnValidating(e);
                }
            }
            else
            {
                base.OnValidating(e);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left && !string.IsNullOrWhiteSpace(this.Text))
                this.SelectAll();

            base.OnDoubleClick(e);
        }

        protected override void OnMaskBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            base.OnMaskBox_KeyUp(sender, e);
            if (Properties.Mask.MaskType == MaskType.RegEx)
            {
                string value = Convert.ToString(this.EditValue);
                if (value == "")
                {
                    this.EditValue = null;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediTextBox Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediTextBox;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediTextBox.CustomEditName;
            }
        }

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }
    }

    public class MediTextBoxViewInfo : TextEditViewInfo
    {
        public MediTextBoxViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediTextBoxPainter : TextEditPainter
    {
        public MediTextBoxPainter()
        {
        }
    }
}