using System.ComponentModel;
using System.Data;
using System.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using Mediinfo.HIS.Core;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.BusinessesControl
{
    /// <summary>
    /// 费用性质封装控件
    /// </summary>
    [UserRepositoryItem("RegisterMediKeShi")]
    public class RepositoryItemMediKeShi : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容

        static RepositoryItemMediKeShi()
        {
            RegisterMediKeShi();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediKeShi";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediKeShi()
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
        public static void RegisterMediKeShi()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediKeShi), typeof(RepositoryItemMediKeShi), typeof(MediKeShiViewInfo), new MediKeShiPainter(), true, img));
        }

        #endregion

        #region 属性,变量定义      

        /// <summary>
        /// 科室结果集
        /// </summary>
        private DataTable dataTableKeShi = new DataTable();

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
                RepositoryItemMediKeShi source = item as RepositoryItemMediKeShi;
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
            if (dataTableKeShi != null && dataTableKeShi.Rows.Count < 1)
            {
                dataTableKeShi = GYShuJuZDHelper.GetShuJuZD("公用科室");

                var dataRow = dataTableKeShi.NewRow();
                dataRow["KESHIID"] = "";
                dataRow["KESHIMC"] = "";
                dataTableKeShi.Rows.InsertAt(dataRow, 0);

                this.SetBindConfig(dataTableKeShi, "KESHIID", "KESHIMC");
                this.FilterField = new string[] { "KESHIID", HISClientHelper.SHURUMLX };

            }
        }

        #endregion
    }

    #region 界面显示控件，根据需要修改

    [ToolboxItem(true)]
    public class MediKeShi : MediGridLookUpEdit
    {
        static MediKeShi()
        {
            RepositoryItemMediKeShi.RegisterMediKeShi();
        }

        public MediKeShi()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediKeShi Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediKeShi;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediKeShi.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediKeShiPopupForm(this);
        }
    }

    public class MediKeShiViewInfo : MediGridLookUpEditViewInfo
    {
        public MediKeShiViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediKeShiPainter : ButtonEditPainter
    {
        public MediKeShiPainter()
        {
        }
    }

    public class MediKeShiPopupForm : MediGridLookUpEditPopupForm
    {
        public MediKeShiPopupForm(MediKeShi ownerEdit)
            : base(ownerEdit)
        {
        }
    }

    #endregion
}
