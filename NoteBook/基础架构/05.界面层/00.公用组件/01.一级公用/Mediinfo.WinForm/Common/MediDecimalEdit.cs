using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;

namespace Mediinfo.WinForm.Common
{
    [UserRepositoryItem("RegisterMediDecimalEdit")]
    public class RepositoryItemMediDecimalEdit : RepositoryItemTextEdit
    {
        static RepositoryItemMediDecimalEdit()
        {
            RegisterMediDecimalEdit();
        }

        public const string CustomEditName = "MediDecimalEdit";

        public RepositoryItemMediDecimalEdit()
        {
            this.Mask.EditMask = "[0-9]+\\.?[0-9]{0,4}";
            this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DisplayFormat.FormatString = "###############.0000";
            
            this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
        }

        public override string EditorTypeName
        {
            get
            {
                return CustomEditName;
            }
        }

        public static void RegisterMediDecimalEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediDecimalEdit), typeof(RepositoryItemMediDecimalEdit), typeof(MediDecimalEditViewInfo), new MediDecimalEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediDecimalEdit source = item as RepositoryItemMediDecimalEdit;
                if (source == null) return;
                //
            }
            finally
            {
                EndUpdate();
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }

    [ToolboxItem(true)]
    public class MediDecimalEdit : TextEdit
    {
        static MediDecimalEdit()
        {
            RepositoryItemMediDecimalEdit.RegisterMediDecimalEdit();
        }

        public MediDecimalEdit()
        {
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediDecimalEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediDecimalEdit;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediDecimalEdit.CustomEditName;
            }
        }
    }

    public class MediDecimalEditViewInfo : TextEditViewInfo
    {
        public MediDecimalEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediDecimalEditPainter : TextEditPainter
    {
        public MediDecimalEditPainter()
        {
        }
    }
}
