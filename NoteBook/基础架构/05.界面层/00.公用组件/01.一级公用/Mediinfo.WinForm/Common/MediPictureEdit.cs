using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 显示以位图，图元文件，图标，JPEG，GIF，PNG或SVG格式存储的图像的编辑器。(网格中)
    /// </summary>
    [UserRepositoryItem("RegisterMediPictureEdit")]
    public class RepositoryItemMediPictureEdit : RepositoryItemPictureEdit, IExpressionInterface
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        static RepositoryItemMediPictureEdit()
        {
            RegisterMediPictureEdit();
        }

        public const string CustomEditName = "MediPictureEdit";

        private string relativeImagePath = string.Empty;

        /// <summary>
        /// 图片相对路径设置属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(RelativePathEditor), typeof(UITypeEditor))]
        public string RelativeImagePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(relativeImagePath))
                {
                    return relativeImagePath;
                }
                else
                {
                    if (relativeImagePath.Contains(":"))
                    {
                        relativeImagePath = string.Empty;
                        XtraMessageBox.Show("路径不合法!");
                        return string.Empty;
                    }
                    return relativeImagePath;
                }
            }
            set
            {
                relativeImagePath = value;
            }
        }

        public RepositoryItemMediPictureEdit()
        {
            this.NullText = " ";
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

        public static void RegisterMediPictureEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediPictureEdit), typeof(RepositoryItemMediPictureEdit), typeof(MediPictureEditViewInfo), new MediPictureEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediPictureEdit source = item as RepositoryItemMediPictureEdit;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <summary>
    /// 显示以位图，图元文件，图标，JPEG，GIF，PNG或SVG格式存储的图像的编辑器。
    /// </summary>
    [ToolboxItem(true)]
    public class MediPictureEdit : PictureEdit
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediPictureEdit()
        {
            RepositoryItemMediPictureEdit.RegisterMediPictureEdit();
        }

        private string relativeImagePath = string.Empty;

        /// <summary>
        /// 图片相对路径设置属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(RelativePathEditor), typeof(UITypeEditor))]
        public string RelativeImagePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(relativeImagePath))
                {
                    return relativeImagePath;
                }
                else
                {
                    if (relativeImagePath.Contains(":"))
                    {
                        relativeImagePath = string.Empty;
                        XtraMessageBox.Show("路径不合法!");
                        return string.Empty;
                    }
                    return relativeImagePath;
                }
            }
            set
            {
                relativeImagePath = value;
                if (!string.IsNullOrWhiteSpace(relativeImagePath) && File.Exists(Path.GetFullPath(relativeImagePath)))
                    this.Image = Image.FromFile(Path.GetFullPath(relativeImagePath));
            }
        }

        public override Image Image
        {
            get
            {
                object ev = EditValue;
                if (ev == null) return null;
                Image img = ev as Image;
                if (img != null) return img;
                if (ViewInfo != null) return ViewInfo.Image;
                return ByteImageConverter.FromByteArray(ByteImageConverter.ToByteArray(ev));
            }
            set { EditValue = value; }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (!this.IsDesignMode)
            {
                if (!string.IsNullOrWhiteSpace(RelativeImagePath) && File.Exists(Path.GetFullPath(RelativeImagePath)))
                    this.Image = Image.FromFile(Path.GetFullPath(RelativeImagePath));
            }
        }

        public MediPictureEdit()
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediPictureEdit Properties => base.Properties as RepositoryItemMediPictureEdit;

        public override string EditorTypeName => RepositoryItemMediPictureEdit.CustomEditName;
    }

    public class MediPictureEditViewInfo : PictureEditViewInfo
    {
        public MediPictureEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediPictureEditPainter : PictureEditPainter
    {
        public MediPictureEditPainter()
        {
        }
    }

    public class RelativePathEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }

            return base.GetEditStyle(context);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;

            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    object o = context.Instance;

                    using (FilePathOptions fpp = new FilePathOptions())
                    {
                        if (fpp.ShowDialog() == DialogResult.OK)
                        {
                            value = fpp.RelativeFilePath;
                            if (o is MediPictureEdit)
                            {
                                ((MediPictureEdit)o).Image = Image.FromFile(fpp.SbsolutePath);
                            }
                            else if (o is RepositoryItemMediPictureEdit)
                            {
                                ((RepositoryItemMediPictureEdit)o).InitialImage = Image.FromFile(fpp.SbsolutePath);
                            }

                            return value;
                        }
                    }
                }
            }

            return value;
        }
    }
}