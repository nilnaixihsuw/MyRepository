using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls.SecondaryFramework;

namespace Mediinfo.WinForm.HIS.Controls.TabForm
{
    public partial class W_LINCHUANG_BASE : MediFormWithQX
    {
        /// <summary>
        /// 已打开的tab窗口
        /// </summary>
        public Dictionary<string, W_LINCHUANG_BASE> openedTabFormDic;
        /// <summary>
        /// 当前菜单信息
        /// </summary>
        public E_GY_CAIDAN_NEW denglucd;
        /// <summary>
        /// 窗口所属系统
        /// </summary>
        public string SUOSHUXT;

        /// <summary>
        /// 回调框架数据
        /// </summary>
        public Action<object, LinChuangEventArgs> CallBackKuangJiaFunc;

        /// <summary>
        /// 通知打开菜单
        /// </summary>
        public Action<string> OpenMenuAction;
        /// <summary>
        /// 目标窗口信息
        /// </summary>
        public MuBiaoFormInformation MuBiaoCKInfo { get; set; }
        /// <summary>
        /// 右键父窗口同时打开子窗口标签
        /// </summary>
        public virtual string ShowDialogCKBZ { get; set; }

        public event Action<object, LinChuangEventArgs> LinChuangFormClosed;
        /// <summary>
        /// 刷新界面数据事件
        /// </summary>
        public event RefreshUIData ShuaXinJieMianEvent;
        /// <summary>
        /// 
        /// </summary>
        public W_LINCHUANG_BASE()
        {
            InitializeComponent();
            RegisterEvent();
            InitialLinChuangCache();
        }
        /// <summary>
        /// 病人框架公用信息
        /// </summary>
        public BingRenInformation BingRenXXKJ { get; set; }
        /// <summary>
        /// 新注册事件
        /// </summary>
        private void RegisterEvent()
        {
            this.Load -= W_LINCHUANG_BASE_Load;
            this.Load += W_LINCHUANG_BASE_Load;
            this.FormClosing -= W_LINCHUANG_BASE_FormClosing;
            this.FormClosing += W_LINCHUANG_BASE_FormClosing;
            this.FormClosed -= W_LINCHUANG_BASE_FormClosed;
            this.FormClosed += W_LINCHUANG_BASE_FormClosed;
        }

        private void W_LINCHUANG_BASE_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (denglucd != null)
            {
                LinChuangFormClosed?.Invoke(this, new LinChuangEventArgs());
                var openCkInfo = denglucd.GONGNENGID.StartsWith("gn")
                    ? new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Button,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = denglucd.GONGNENGID,
                        caidanId = denglucd.CAIDANID,
                        binrenid = bingRenId,
                        chuangkoumc = this.Name.ToUpper()
                    }
                    : new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Menu,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = denglucd.GONGNENGID,
                        caidanId = denglucd.CAIDANID,
                        binrenid = bingRenId,
                        chuangkoumc = this.Name.ToUpper()
                    };

                if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCkInfo))
                {
                    BaseFormCommonHelper.openAllwindowsdic.Remove(openCkInfo);
                }
            }

        }

        private void W_LINCHUANG_BASE_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新框架界面数据
        /// </summary>
        public virtual void CallBackKuangJiaEvent(object sender, LinChuangEventArgs linChuangEventArgs)
        {
            CallBackKuangJiaFunc?.Invoke(sender, linChuangEventArgs);
        }

        /// <summary>
        /// 验证业务窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void W_LINCHUANG_BASE_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public virtual bool ValidateData()
        {
            return false;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveData()
        {
            return false;
        }

        /// <summary>
        /// 是否修改数据
        /// </summary>
        /// <returns></returns>
        public virtual bool IsModifyData()
        {
            return false;
        }
        /// <summary>
        /// 初始化临床缓存
        /// </summary>
        public void InitialLinChuangCache()
        {

            //初始化业务对象
            IntialYeWuDX();
            //初始化业务数据
            IntialYeWuSJ();
            //初始化基础数据
            IntialJiChuData();
        }


        /// <summary>
        /// 初始化业务对象
        /// </summary>
        public virtual void IntialYeWuDX()
        {

        }
        /// <summary>
        /// 初始化业务数据
        /// </summary>
        public virtual void IntialYeWuSJ()
        {

        }

        //初始化基础数据
        public virtual void IntialJiChuData()
        {

        }
        /// <summary>
        /// 刷新业务数据
        /// </summary>
        public virtual void RefreshBusinessData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public object RunRefreshUI(string formName, object eventArgs)
        {
            string tempFormName = formName.ToUpper();
            if (openedTabFormDic.ContainsKey(tempFormName))
            {
                W_LINCHUANG_BASE callBackForm = openedTabFormDic[tempFormName];
                if (callBackForm != null && !callBackForm.IsDisposed)
                {
                    if (callBackForm.ShuaXinJieMianEvent != null)
                    {
                        return callBackForm.ShuaXinJieMianEvent(formName, eventArgs);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void RefreshUI()
        {

        }

    }
}