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
    [UserRepositoryItem("RegisterMediYingYong")]
    public class RepositoryItemMediYingYong : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容
        static RepositoryItemMediYingYong()
        {
            RegisterMediYingYong();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediYingYong";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediYingYong()
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
        public static void RegisterMediYingYong()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediYingYong), typeof(RepositoryItemMediYingYong), typeof(MediYingYongViewInfo), new MediYingYongPainter(), true, img));
        }
        #endregion

        #region 属性变量

        private List<E_GY_YINGYONG> yingYongList = new List<E_GY_YINGYONG>();

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
                RepositoryItemMediYingYong source = item as RepositoryItemMediYingYong;
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
            if (yingYongList != null && yingYongList.Count < 1)
            {
                yingYongList = GYYingYongHelper.GetYingYong();
                E_GY_YINGYONG model = new E_GY_YINGYONG
                {
                    YINGYONGID = string.Empty,
                    YINGYONGMC = string.Empty
                };
                yingYongList.Insert(0, model);
                this.SetBindConfig(yingYongList, "YINGYONGID", "YINGYONGMC");
            }
        }
        #endregion
    }

    #region 界面显示控件，根据需要修改
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(true)]
    public class MediYingYong: MediGridLookUpEdit
    {
        static MediYingYong()
        {
            RepositoryItemMediYingYong.RegisterMediYingYong();
        }

        public MediYingYong()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediYingYong Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediYingYong;
            }
        }


        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediYingYong.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediYingYongPopupForm(this);
        }
    }

    public class MediYingYongViewInfo : MediGridLookUpEditViewInfo
    {
        public MediYingYongViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediYingYongPainter : ButtonEditPainter
    {
        public MediYingYongPainter()
        {
        }
    }

    public class MediYingYongPopupForm : MediGridLookUpEditPopupForm
    {
        public MediYingYongPopupForm(MediYingYong ownerEdit)
            : base(ownerEdit)
        {
        }
    }
    #endregion
}
