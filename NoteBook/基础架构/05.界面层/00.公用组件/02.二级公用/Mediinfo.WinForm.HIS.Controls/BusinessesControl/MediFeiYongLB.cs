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
    [UserRepositoryItem("RegisterMediFeiYongLB")]
    public class RepositoryItemMediFeiYongLB : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容

        static RepositoryItemMediFeiYongLB()
        {
            RegisterMediFeiYongLB();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediFeiYongLB";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediFeiYongLB()
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
        public static void RegisterMediFeiYongLB()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediFeiYongLB), typeof(RepositoryItemMediFeiYongLB), typeof(MediFeiYongLBViewInfo), new MediFeiYongLBPainter(), true, img));
        }

        #endregion

        #region 属性,变量定义      

        /// <summary>
        /// 费用结果集
        /// </summary>
        private DataTable dataTableFYLB = new DataTable();

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
                RepositoryItemMediFeiYongLB source = item as RepositoryItemMediFeiYongLB;
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
            if (dataTableFYLB != null && dataTableFYLB.Rows.Count < 1)
            {
                dataTableFYLB = GYShuJuZDHelper.GetShuJuZD("公用费用类别");

                var dataRow = dataTableFYLB.NewRow();
                dataRow["LEIBIEID"] = "";
                dataRow["LEIBIEMC"] = "全部";
                dataTableFYLB.Rows.InsertAt(dataRow, 0);

                this.SetBindConfig(dataTableFYLB, "LEIBIEID", "LEIBIEMC");
            }
        }

        #endregion
    }

    #region 界面显示控件，根据需要修改

    [ToolboxItem(true)]
    public class MediFeiYongLB : MediGridLookUpEdit
    {
        static MediFeiYongLB()
        {
            RepositoryItemMediFeiYongLB.RegisterMediFeiYongLB();
        }

        public MediFeiYongLB()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediFeiYongLB Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediFeiYongLB;
            }
        }


        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediFeiYongLB.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediFeiYongLBPopupForm(this);
        }
    }

    public class MediFeiYongLBViewInfo : MediGridLookUpEditViewInfo
    {
        public MediFeiYongLBViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediFeiYongLBPainter : ButtonEditPainter
    {
        public MediFeiYongLBPainter()
        {
        }
    }

    public class MediFeiYongLBPopupForm : MediGridLookUpEditPopupForm
    {
        public MediFeiYongLBPopupForm(MediFeiYongLB ownerEdit)
            : base(ownerEdit)
        {
        }
    }

    #endregion
}
