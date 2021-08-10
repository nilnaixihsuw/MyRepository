using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using Mediinfo.WinForm;
using Mediinfo.WinForm.Common;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.Common
{
    [ToolboxItem(true)]
    public class MediButtonEdit : ButtonEdit, IInputIMEMode
    {
        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        static MediButtonEdit()
        {
            RepositoryItemMediButtonEdit.RegisterMediButtonEdit();
        }

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediButtonEdit()
        {
            this.Cursor = Cursors.Hand;
            this.GotFocus -= MediButtonEdit_GotFocus;
            this.GotFocus += MediButtonEdit_GotFocus;
            this.MouseUp -= MediButtonEdit_MouseUp;
            this.MouseUp += MediButtonEdit_MouseUp;
            this.Enter -= MediButtonEdit_Enter;
            this.Enter += MediButtonEdit_Enter;
            this.MouseDown -= MediButtonEdit_MouseDown;
            this.MouseDown += MediButtonEdit_MouseDown;

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediButtonEdit_Enter(object sender, EventArgs e)
        {
            enter = true;
            if (this.IsHandleCreated)
                BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediButtonEdit_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }
        
        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }
        
        private void MediButtonEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                (sender as TextEdit).SelectAll();
            }
        }
        
        private void MediButtonEdit_GotFocus(object sender, System.EventArgs e)
        {
            if (FocusAllText)
                this.SelectAll();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediButtonEdit Properties => base.Properties as RepositoryItemMediButtonEdit;

        public override string EditorTypeName => RepositoryItemMediButtonEdit.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }
    }

    public class MediButtonEditViewInfo : ButtonEditViewInfo
    {
        public MediButtonEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediButtonEditPainter : ButtonEditPainter
    {
        public MediButtonEditPainter()
        {
        }
    }
}

[UserRepositoryItem("RegisterMediButtonEdit")]
public class RepositoryItemMediButtonEdit : DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit, IInputIMEMode
{
    [Browsable(false)]
    public IDeveloperHelper developerHelper { get; set; }

    static RepositoryItemMediButtonEdit()
    {
        RegisterMediButtonEdit();
    }

    public const string CustomEditName = "MediButtonEdit";

    public RepositoryItemMediButtonEdit()
    {
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

    public static void RegisterMediButtonEdit()
    {
        Image img = null;
        EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediButtonEdit), typeof(RepositoryItemMediButtonEdit), typeof(MediButtonEditViewInfo), new MediButtonEditPainter(), true, img));
    }

    public override void Assign(RepositoryItem item)
    {
        BeginUpdate();
        try
        {
            base.Assign(item);
            RepositoryItemMediButtonEdit source = item as RepositoryItemMediButtonEdit;
            if (source == null) return;
        }
        finally
        {
            EndUpdate();
        }
    }
}