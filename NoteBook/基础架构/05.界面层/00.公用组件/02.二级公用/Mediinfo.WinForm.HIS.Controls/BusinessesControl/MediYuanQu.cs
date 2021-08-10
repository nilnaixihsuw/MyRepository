using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.BusinessesControl
{
    [UserRepositoryItem("RegisterMediYuanQu")]
    public class RepositoryItemMediYuanQu : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容
        static RepositoryItemMediYuanQu()
        {
            RegisterMediYuanQu();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediYuanQu";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediYuanQu()
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
        public static void RegisterMediYuanQu()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediYuanQu), typeof(RepositoryItemMediYuanQu), typeof(MediYuanQuViewInfo), new MediYuanQuPainter(), true, img));
        }
        #endregion

        #region 属性变量
         
        private List<E_GY_YUANQU> YuanQuList = new List<E_GY_YUANQU>();

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
                RepositoryItemMediYuanQu source = item as RepositoryItemMediYuanQu;
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
            if (YuanQuList != null && YuanQuList.Count < 1)
            {
                YuanQuList = GYYuanQuHelper.GetYuanQuList();
                E_GY_YUANQU model = new E_GY_YUANQU
                {
                    YUANQUID = string.Empty,
                    YUANQUMC = string.Empty
                };
                YuanQuList.Insert(0, model);
                this.SetBindConfig(YuanQuList, "YUANQUID", "YUANQUMC");
            }
        }
        #endregion
    }

    #region 界面显示控件，根据需要修改
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(true)]
    public class MediYuanQu : MediGridLookUpEdit
    {
        static MediYuanQu()
        {
            RepositoryItemMediYuanQu.RegisterMediYuanQu();
        }

        public MediYuanQu()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediYuanQu Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediYuanQu;
            }
        }


        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediYuanQu.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediYuanQuPopupForm(this);
        }
    }

    public class MediYuanQuViewInfo : MediGridLookUpEditViewInfo
    {
        public MediYuanQuViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediYuanQuPainter : ButtonEditPainter
    {
        public MediYuanQuPainter()
        {
        }
    }

    public class MediYuanQuPopupForm : MediGridLookUpEditPopupForm
    {
        public MediYuanQuPopupForm(MediYuanQu ownerEdit)
            : base(ownerEdit)
        {
        }
    }
    #endregion
}
