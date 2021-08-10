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
    [UserRepositoryItem("RegisterMediYouHuiLB")]
    public class RepositoryItemMediYouHuiLB : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容
        static RepositoryItemMediYouHuiLB()
        {
            RegisterMediYouHuiLB();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediYouHuiLB";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediYouHuiLB()
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
        public static void RegisterMediYouHuiLB()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediYouHuiLB), typeof(RepositoryItemMediYouHuiLB), typeof(MediYouHuiLBViewInfo), new MediYouHuiLBPainter(), true, img));
        }
        #endregion

        #region 属性,变量定义      

        /// <summary>
        ///优惠类别结果集
        /// </summary>
        private DataTable dataTableYHLB = new DataTable();


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
                RepositoryItemMediYouHuiLB source = item as RepositoryItemMediYouHuiLB;
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
            if (dataTableYHLB != null && dataTableYHLB.Rows.Count < 1)
            {
                dataTableYHLB = GYShuJuZDHelper.GetShuJuZD("公用优惠类别");               
                this.SetBindConfig(dataTableYHLB, "YOUHUILBID", "YOUHUIMC");             
            }
        }
        #endregion
    }

    #region 界面显示控件，根据需要修改
    [ToolboxItem(true)]
       public class MediYouHuiLB : MediGridLookUpEdit
       {
           static MediYouHuiLB()
           {
               RepositoryItemMediYouHuiLB.RegisterMediYouHuiLB();
           }

           public MediYouHuiLB()
           {

           }

           protected override void OnCreateControl()
           {
               base.OnCreateControl();
           }

           [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
           public new RepositoryItemMediYouHuiLB Properties
           {
               get
               {
                   return base.Properties as RepositoryItemMediYouHuiLB;
               }
           }

        

           public override string EditorTypeName
           {
               get
               {
                   return RepositoryItemMediYouHuiLB.CustomEditName;
               }
           }

           protected override PopupBaseForm CreatePopupForm()
           {
               return new MediYouHuiLBPopupForm(this);
           }
       }

       public class MediYouHuiLBViewInfo : MediGridLookUpEditViewInfo
       {
           public MediYouHuiLBViewInfo(RepositoryItem item)
               : base(item)
           {
           }
       }

       public class MediYouHuiLBPainter : ButtonEditPainter
       {
           public MediYouHuiLBPainter()
           {
           }
       }

       public class MediYouHuiLBPopupForm : MediGridLookUpEditPopupForm
       {
           public MediYouHuiLBPopupForm(MediYouHuiLB ownerEdit)
               : base(ownerEdit)
           {
           }
       }
    #endregion

}
