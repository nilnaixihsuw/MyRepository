using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    [UserRepositoryItem("RegisterMediFeiYongXZ")]
    public class RepositoryItemMediFeiYongXZ : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容

        static RepositoryItemMediFeiYongXZ()
        {
            RegisterMediFeiYongXZ();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediFeiYongXZ";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediFeiYongXZ()
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
        public static void RegisterMediFeiYongXZ()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediFeiYongXZ), typeof(RepositoryItemMediFeiYongXZ), typeof(MediFeiYongXZViewInfo), new MediFeiYongXZPainter(), true, img));
        }

        #endregion

        #region 属性,变量定义

        /// <summary>
        /// 费用类别ID
        /// </summary>
        private string feiYongLBID = "";

        /// <summary>
        /// 费用类别ID
        /// </summary>
        [Browsable(false)]
        public string FeiYongLBID
        {
            get { return feiYongLBID; }
            set
            {
                feiYongLBID = value;
                ChangeSourceByFYLB();
            }
        }

        /// <summary>
        /// 费用性质结果集
        /// </summary>
        private DataTable dataTableFYXZ = new DataTable();

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
                RepositoryItemMediFeiYongXZ source = item as RepositoryItemMediFeiYongXZ;
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
            if (dataTableFYXZ != null && dataTableFYXZ.Rows.Count < 1)
            {
                dataTableFYXZ = GYShuJuZDHelper.GetShuJuZD("公用费用性质");
                var dataRow = dataTableFYXZ.NewRow();
                dataRow["XINGZHIID"] = "";
                dataRow["XINGZHIMC"] = "";
                dataTableFYXZ.Rows.InsertAt(dataRow, 0);
                this.SetBindConfig(dataTableFYXZ, "XINGZHIID", "XINGZHIMC");
            }
        }

        /// <summary>
        /// 当费用类别改变时，改变数据源显示内容
        /// </summary>
        private void ChangeSourceByFYLB()
        {
            if (dataTableFYXZ != null)
            {
                DataTable FeiYongXZ = dataTableFYXZ.Clone();
                if (FeiYongLBID != "")
                {
                    var rows = dataTableFYXZ.Select(" FEIYONGLB='" + FeiYongLBID + "'", "SHUNXUHAO ASC");
                    if (rows.Count() > 0)
                    {
                        foreach (var row in rows)
                        {
                            FeiYongXZ.Rows.Add(row.ItemArray);
                        }
                    }
                }
                else
                {
                    FeiYongXZ = dataTableFYXZ;
                }
                if (this.EditStyle == EditStyle.DropDownList)
                {
                    if (this.AddSelectItemDic != null && this.AddSelectItemDic.Count > 0)
                    {
                        foreach (var item in this.AddSelectItemDic)
                        {
                            var dataRow = FeiYongXZ.NewRow();
                            dataRow["XINGZHIID"] = item.Key;
                            dataRow["XINGZHIMC"] = item.Value;
                            FeiYongXZ.Rows.InsertAt(dataRow, 0);
                        }
                    }
                }
                this.DataSource = FeiYongXZ;
            }
        }

        #endregion
    }

    #region 界面显示控件，根据需要修改

    [ToolboxItem(true)]
    public class MediFeiYongXZ : MediGridLookUpEdit
    {
        static MediFeiYongXZ()
        {
            RepositoryItemMediFeiYongXZ.RegisterMediFeiYongXZ();
        }

        public MediFeiYongXZ()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediFeiYongXZ Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediFeiYongXZ;
            }
        }
        
        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediFeiYongXZ.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediFeiYongXZPopupForm(this);
        }
    }

    public class MediFeiYongXZViewInfo : MediGridLookUpEditViewInfo
    {
        public MediFeiYongXZViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediFeiYongXZPainter : ButtonEditPainter
    {
        public MediFeiYongXZPainter()
        {
        }
    }

    public class MediFeiYongXZPopupForm : MediGridLookUpEditPopupForm
    {
        public MediFeiYongXZPopupForm(MediFeiYongXZ ownerEdit)
            : base(ownerEdit)
        {
        }
    }

    #endregion
}
