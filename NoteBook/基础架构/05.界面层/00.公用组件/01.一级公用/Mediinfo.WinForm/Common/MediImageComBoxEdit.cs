using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    [UserRepositoryItem("RegisterMediImageComBox")]
    public class RepositoryItemMediImageComboBox : RepositoryItemImageComboBox
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        
        static RepositoryItemMediImageComboBox()
        {
            RegisterMediImageComboBox();
        }

        public const string CustomEditName = "MediImageComBox";

        /// <summary>
        /// 通过皮肤获取控件信息
        /// </summary>
        protected virtual void Init()
        {
            this.BeginInit();

            try
            {
                // this.SetCustomSkin();
            }
            finally
            {
                this.EndInit();
            }
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public RepositoryItemMediImageComboBox()
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

        private bool readOnly = false;

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediImageComboBox()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediImageComboBoxEdit), typeof(RepositoryItemMediImageComboBox), typeof(MedimageComboBoxEditViewInfo), new ImageComboBoxEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediImageComboBox source = item as RepositoryItemMediImageComboBox;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <summary>
    /// 下拉框控件
    /// 主要用于绑定集合对象
    /// </summary>
    [ToolboxItem(true)]
    public class MediImageComboBoxEdit : ImageComboBoxEdit
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        
        static MediImageComboBoxEdit()
        {
            RepositoryItemMediImageComboBox.RegisterMediImageComboBox();
        }
       
        public MediImageComboBoxEdit()
        {
            this.Cursor = Cursors.Hand;
            this.SetEditorsCustomSkin();
            this.EnterMoveNextControl = true;
            this.MinimumSize = new Size(0, 26);
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediComboBox_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }
        
        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }
        
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
            {
                this.ForeColor = Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));

                this.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                this.Enabled = false;
            }
            else
            {
                this.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                this.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.Enabled = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediImageComboBox Properties => base.Properties as RepositoryItemMediImageComboBox;

        public override string EditorTypeName => RepositoryItemMediImageComboBox.CustomEditName;
    }

    public class MedimageComboBoxEditViewInfo : ImageComboBoxEditViewInfo
    {
        public MedimageComboBoxEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediImageComboBoxEditPainter : ImageComboBoxEditPainter
    {
        public MediImageComboBoxEditPainter()
        {
        }
    }
}
