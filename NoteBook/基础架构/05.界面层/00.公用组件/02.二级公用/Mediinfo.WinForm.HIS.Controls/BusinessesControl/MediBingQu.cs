using System.ComponentModel;
using System.Data;
using System.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.BusinessesControl
{
    /// <summary>
    /// 费用性质封装控件
    /// </summary>
    [UserRepositoryItem("RegisterMediBingQu")]
    public class RepositoryItemMediBingQu : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容

        static RepositoryItemMediBingQu()
        {
            RegisterMediBingQu();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediBingQu";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediBingQu()
        {

        }

        /// <summary>
        /// 控件类型
        /// </summary>
        public override string EditorTypeName
        {
            get
            {
                return CustomEditName;
            }
        }

        /// <summary>
        /// 注册控件
        /// </summary>
        public static void RegisterMediBingQu()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediBingQu), typeof(RepositoryItemMediBingQu), typeof(MediBingQuViewInfo), new MediBingQuPainter(), true, img));
        }

        #endregion

        #region 属性,变量定义      

        /// <summary>
        /// 病区结果集
        /// </summary>
        private DataTable dataTableBingQu = new DataTable();

        #endregion

        #region 重写原方法

        /// <summary>
        /// 结束初始化
        /// </summary>
        public override void EndInit()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                LoadData();
            }

            base.EndInit();
        }

        /// <summary>
        /// 初始赋值
        /// </summary>
        /// <param name="item"></param>
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                this.SetCustomSkin();
                base.Assign(item);
                RepositoryItemMediBingQu source = item as RepositoryItemMediBingQu;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion

        #region 本地数据处理

        /// <summary>
        /// 加载数据源
        /// </summary>
        public void LoadData()
        {
            if (dataTableBingQu != null && dataTableBingQu.Rows.Count < 1)
            {
                dataTableBingQu = GYShuJuZDHelper.GetShuJuZD("公用病区");

                var dataRow = dataTableBingQu.NewRow();
                dataRow["BINGQUID"] = "";
                dataRow["BINGQUMC"] = "全院";
                dataTableBingQu.Rows.InsertAt(dataRow, 0);

                this.SetBindConfig(dataTableBingQu, "BINGQUID", "BINGQUMC");
            }
        }

        #endregion
    }

    #region 界面显示控件，根据需要修改

    [ToolboxItem(true)]
    public class MediBingQu : MediGridLookUpEdit
    {
        static MediBingQu()
        {
            RepositoryItemMediBingQu.RegisterMediBingQu();
        }

        public MediBingQu()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediBingQu Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediBingQu;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediBingQu.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediBingQuPopupForm(this);
        }
    }

    public class MediBingQuViewInfo : MediGridLookUpEditViewInfo
    {
        public MediBingQuViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediBingQuPainter : ButtonEditPainter
    {
        public MediBingQuPainter()
        {
        }
    }

    public class MediBingQuPopupForm : MediGridLookUpEditPopupForm
    {
        public MediBingQuPopupForm(MediBingQu ownerEdit)
            : base(ownerEdit)
        {
        }
    }

    #endregion
}
