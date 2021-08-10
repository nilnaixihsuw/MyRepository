using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.BusinessesControl
{
    [UserRepositoryItem("RegisterMediDaiMa")]
    public class RepositoryItemRegisterMediDaiMa : RepositoryItemMediGridLookUpEdit
    {
        #region 原控件自带内容

        static RepositoryItemRegisterMediDaiMa()
        {
            RegisterMediDaiMa();
        }

        /// <summary>
        /// 控件名
        /// </summary>
        public const string CustomEditName = "MediDaiMa";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemRegisterMediDaiMa()
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
        public static void RegisterMediDaiMa()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediDaiMa), typeof(RepositoryItemRegisterMediDaiMa), typeof(MediDaiMaViewInfo), new MediDaiMaPainter(), true, img));
        }

        #endregion

        #region 属性变量

        private List<E_GY_DAIMA> daiMaList = new List<E_GY_DAIMA>();

        #endregion

        #region 重写原方法

        /// <summary>
        /// 结束初始化
        /// </summary>
        public override void EndInit()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                //LoadData();
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
                RepositoryItemRegisterMediDaiMa source = item as RepositoryItemRegisterMediDaiMa;
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
        public void LoadData(string daimalb, int? menzhensy, int? zhuyuansy, int zuofeibz)
        {
            if (daiMaList != null && daiMaList.Count < 1)
            {
                daiMaList = GYDaiMaHelper.GteGYList(daimalb, menzhensy, zhuyuansy, zuofeibz).OrderBy(p => p.SHUNXUHAO).ToList();
                E_GY_DAIMA model = new E_GY_DAIMA
                {
                    DAIMAID = string.Empty,
                    DAIMAMC = string.Empty
                };
                daiMaList.Insert(0, model);
                this.SetBindConfig(daiMaList, "DAIMAID", "DAIMAMC");
            }
        }

        #endregion
    }

    #region 界面显示控件，根据需要修改

    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(true)]
    public class MediDaiMa : MediGridLookUpEdit
    {
        static MediDaiMa()
        {
            RepositoryItemRegisterMediDaiMa.RegisterMediDaiMa();
        }

        public MediDaiMa()
        {
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemRegisterMediDaiMa Properties
        {
            get
            {
                return base.Properties as RepositoryItemRegisterMediDaiMa;
            }
        }


        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemRegisterMediDaiMa.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediDaiMaPopupForm(this);
        }
    }

    public class MediDaiMaViewInfo : MediGridLookUpEditViewInfo
    {
        public MediDaiMaViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediDaiMaPainter : ButtonEditPainter
    {
        public MediDaiMaPainter()
        {
        }
    }

    public class MediDaiMaPopupForm : MediGridLookUpEditPopupForm
    {
        public MediDaiMaPopupForm(MediDaiMa ownerEdit)
            : base(ownerEdit)
        {
        }
    }

    #endregion
}
