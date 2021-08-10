using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediPopupContainerEdit: DevExpress.XtraEditors.PopupContainerEdit
    {

        static MediPopupContainerEdit()
        {
            RepositoryItemMediPopupContainerEdit.RegisterMediPopupContainerEdit();
        }
        public MediPopupContainerEdit():base()
        {

        }

        protected override void OnEditorKeyPress(KeyPressEventArgs e)
        {
            base.OnEditorKeyPress(e);
            if (!this.IsPopupOpen) this.ShowPopup();
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemMediPopupContainerEdit.MediPopupContainerEditName; }
        }

        new public RepositoryItemMediPopupContainerEdit Properties
        {
            get { return base.Properties as RepositoryItemMediPopupContainerEdit; }
        }


      
    }

    [UserRepositoryItem("RegisterMediPopupContainerEdit")]
    public class RepositoryItemMediPopupContainerEdit : DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    {

        static RepositoryItemMediPopupContainerEdit()
        {
            RegisterMediPopupContainerEdit();
        }
        public RepositoryItemMediPopupContainerEdit():base()
        {

        }

        public static void RegisterMediPopupContainerEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(
                MediPopupContainerEditName,
                typeof(MediPopupContainerEdit),
                typeof(RepositoryItemMediPopupContainerEdit),
                typeof(MediPopupContainerEditViewInfo),
                new MediButtonEditPainter(),
                true));
        }
        public const string MediPopupContainerEditName = "MediPopupContainerEdit";

        public override string EditorTypeName
        {
            get { return MediPopupContainerEditName; }
        }


        
    }

    public class MediPopupContainerEditViewInfo: DevExpress.XtraEditors.ViewInfo.PopupContainerEditViewInfo
    {
        public MediPopupContainerEditViewInfo(RepositoryItem item):base(item)
        {

        }
    }

    public class MediButtonEditPainter: DevExpress.XtraEditors.Drawing.ButtonEditPainter
    {

    }

}
