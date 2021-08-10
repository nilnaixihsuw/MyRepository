using Mediinfo.DTO.HIS.GY;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediFormLCWithQX : MediFormWithQX
    {
        /// <summary>
        /// 刷新窗口数据
        /// </summary>
        public Action<object, RefreshUIEventArgs> ShuaXinJieMianDelegate;

        public event Action<object, RefreshUIEventArgs> ShuaXinJieMianEvent;

        public Action<MediFormLCWithQX, OpenType> OpenTabWindowByButton;

        /// <summary>
        /// 窗体打开之前事件
        /// </summary>
        protected event Action<FormOpenEventArgs> BeforeFormOpenEevent;

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        protected event Action<FormOpenEventArgs> ClosingFormOpenEevent;

        /// <summary>
        /// 
        /// </summary>
        public MediFormLCWithQX()
        {
            InitializeComponent();
            ShuaXinJieMianDelegate = FireRefreshEvent;
            RegisterEvents();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            this.FormClosing += MediFormLCWithQX_FormClosing;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediFormWithQX"></param>
        /// <param name="openType"></param>
        public void OpenlcWindowByButton(MediFormLCWithQX mediFormWithQX, OpenType openType)
        {
            OpenTabWindowByButton?.Invoke(mediFormWithQX, openType);
        }
        
        private void MediFormLCWithQX_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClosingFormOpenEevent != null)
            {
                FormOpenEventArgs formOpenEventArgs = new FormOpenEventArgs();
                ClosingFormOpenEevent(formOpenEventArgs);
                if (formOpenEventArgs.MFOpenResult == MediDialogResult.Close)
                {

                }
            }
        }
        
        /// <summary>
        /// 激活事件
        /// </summary>
        public void ActiveEvents()
        {
            if (BeforeFormOpenEevent != null)
            {
                FormOpenEventArgs formOpenEventArgs = new FormOpenEventArgs();
                BeforeFormOpenEevent(formOpenEventArgs);
                if (formOpenEventArgs.MFOpenResult == MediDialogResult.Close)
                {

                }
            }
        }
        /// <summary>
        /// 当前菜单ID
        /// </summary>
        public string currentCaiDanId = string.Empty;

        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<E_GY_CAIDAN_NEW> CaiDanList;

        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<E_GY_YONGHUQX> YongHuQXList;

        /// <summary>
        /// 初始化菜单数据
        /// </summary>
        public virtual void InitialCaiDanData()
        {

        }





        /// <summary>
        /// 刷新业务数据
        /// </summary>
        public virtual void RefreshBusinessData()
        {

        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="refreshUIEventArgs"></param>
        public void FireRefreshEvent(object sender, RefreshUIEventArgs refreshUIEventArgs)
        {
            ShuaXinJieMianEvent?.Invoke(sender, refreshUIEventArgs);
        }

        private void MediFormLCWithQX_Load(object sender, EventArgs e)
        {
            // TabFormOpenHelper.OpenTabForm("13059961", "zy3232", "jz5476", new W_BINGRENKJ_BASE());
        }
    }
}