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
    [UserRepositoryItem("RegisterMediZhiGong")]
    public class RepositoryItemMediZhiGong : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容
        static RepositoryItemMediZhiGong()
        {
            RegisterMediZhiGong();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediZhiGong";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediZhiGong()
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
        public static void RegisterMediZhiGong()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediZhiGong), typeof(RepositoryItemMediZhiGong), typeof(MediZhiGongViewInfo), new MediZhiGongPainter(), true, img));
        }
        #endregion

        #region 属性,变量定义      

        /// <summary>
        /// 职工结果集
        /// </summary>
        private DataTable dataTableZhiGong = new DataTable();


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
                RepositoryItemMediZhiGong source = item as RepositoryItemMediZhiGong;
                if (source == null) return;
                //
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
            if (dataTableZhiGong != null && dataTableZhiGong.Rows.Count < 1)
            {
                dataTableZhiGong = GYShuJuZDHelper.GetShuJuZD("公用职工");

                var dataRow = dataTableZhiGong.NewRow();
                dataRow["ZHIGONGID"] = "";
                dataRow["ZHIGONGXM"] = "";
                dataTableZhiGong.Rows.InsertAt(dataRow, 0);

                this.SetBindConfig(dataTableZhiGong, "ZHIGONGID", "ZHIGONGXM");
            }
        }
        #endregion
    }
    #region 界面显示控件，根据需要修改
    [ToolboxItem(true)]
       public class MediZhiGong : MediGridLookUpEdit
       {
           static MediZhiGong()
           {
               RepositoryItemMediZhiGong.RegisterMediZhiGong();
           }

           public MediZhiGong()
           {

           }

           protected override void OnCreateControl()
           {
               base.OnCreateControl();
           }

           [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
           public new RepositoryItemMediZhiGong Properties
           {
               get
               {
                   return base.Properties as RepositoryItemMediZhiGong;
               }
           }        


        public override string EditorTypeName
           {
               get
               {
                   return RepositoryItemMediZhiGong.CustomEditName;
               }
           }

           protected override PopupBaseForm CreatePopupForm()
           {
               return new MediZhiGongPopupForm(this);
           }
       }

       public class MediZhiGongViewInfo : MediGridLookUpEditViewInfo
       {
           public MediZhiGongViewInfo(RepositoryItem item)
               : base(item)
           {
           }
       }

       public class MediZhiGongPainter : ButtonEditPainter
       {
           public MediZhiGongPainter()
           {
           }
       }

       public class MediZhiGongPopupForm : MediGridLookUpEditPopupForm
       {
           public MediZhiGongPopupForm(MediZhiGong ownerEdit)
               : base(ownerEdit)
           {
           }
       }
    #endregion

}
