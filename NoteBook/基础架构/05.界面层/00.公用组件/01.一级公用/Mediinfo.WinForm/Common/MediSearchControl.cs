using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using Mediinfo.HIS.Core;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 为附加对象提供搜索和过滤功能的控件。(网格中)
    /// </summary>
    [UserRepositoryItem("RegisterMediSearchControl")]
    public class RepositoryItemMediSearchControl : RepositoryItemSearchControl, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; } = string.Empty;

        static RepositoryItemMediSearchControl()
        {
            RegisterMediSearchControl();
        }

        public const string CustomEditName = "MediSearchControl";

        public RepositoryItemMediSearchControl()
        {
            this.NullValuePrompt = "请输入搜索关键字";
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediSearchControl()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediSearchControl), typeof(RepositoryItemMediSearchControl), typeof(MediSearchControlViewInfo), new MediSearchControlPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);

                RepositoryItemMediSearchControl source = item as RepositoryItemMediSearchControl;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <summary>
    /// 为附加对象提供搜索和过滤功能的控件。
    /// </summary>
    [ToolboxItem(true)]
    public class MediSearchControl : SearchControl, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediSearchControl()
        {
            RepositoryItemMediSearchControl.RegisterMediSearchControl();
        }

        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        public MediSearchControl()
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                this.GotFocus -= MediSearchControl_GotFocus;
                this.GotFocus += MediSearchControl_GotFocus;
                this.MouseUp -= MediSearchControl_MouseUp;
                this.MouseUp += MediSearchControl_MouseUp;
                this.Enter -= MediSearchControl_Enter;
                this.Enter += MediSearchControl_Enter;
                this.MouseDown -= MediSearchControl_MouseDown;
                this.MouseDown += MediSearchControl_MouseDown;

                this.SetEditorsCustomSkin();

                this.MinimumSize = new Size(0, 26);
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediSearchControl_Enter(object sender, EventArgs e)
        {
            enter = true;
            if (this.IsHandleCreated)   //gxl  防止出现“在创建窗口句柄之前,不能在控件上调用 Invoke 或 BeginInvoke”
                BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediSearchControl_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }
        
        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }
        
        private void MediSearchControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                (sender as TextEdit).SelectAll();
            }
        }

        private void MediSearchControl_GotFocus(object sender, System.EventArgs e)
        {
            if (FocusAllText)
                this.SelectAll();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediSearchControl Properties => base.Properties as RepositoryItemMediSearchControl;

        public override string EditorTypeName => RepositoryItemMediSearchControl.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediSearchControlPopupForm(this);
        }
    }

    public class MediSearchControlViewInfo : SearchControlViewInfo
    {
        public MediSearchControlViewInfo(RepositoryItem item) : base(item)
        {
        }
        
        protected override EditorButtonObjectInfoArgs CreateButtonInfoInstance(EditorButton button)
        {
            if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) &&! HISClientHelper.LINCHUANGBZ.Equals(0))
                button.Width = 25;
            return base.CreateButtonInfoInstance(button);
        }
    }

    public class MediSearchControlPainter : ButtonEditPainter
    {
        public MediSearchControlPainter()
        {
        }
    }

    public class MediSearchControlPopupForm : PopupMRUForm
    {
        public MediSearchControlPopupForm(MediSearchControl ownerEdit) : base(ownerEdit)
        {
        }
    }
}