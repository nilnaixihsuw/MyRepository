using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.WinForm.HIS.Controls.SecondaryFramework;
using Mediinfo.WinForm.HIS.Controls.TabForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls.FirstLevelFramework;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Enterprise;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public static class BaseFormCommonHelper
    {
        /// <summary>
        /// 预加载程序集字典
        /// </summary>
        public static ConcurrentDictionary<string, ConcurrentDictionary<string, KeyValuePair<string, Assembly>>> Assemblys;
        /// <summary>
        /// 菜单集合
        /// </summary>
        public static List<E_GY_CAIDAN_NEW> CaiDanList { get; set; }

        private static bool showBol = false;
        private static Dictionary<string, string> caiDanMCList = new Dictionary<string, string>();

        public static Dictionary<OpenCKInfo, MediForm> openAllwindowsdic;
        /// <summary>
        /// 临床主窗体
        /// </summary>
        public static MediUniversalMFBase LinChuangMainForm { get; set; }

        /// <summary>
        /// HR6-1389 1按病人授权2按功能点授权
        /// </summary>
        public static string ShouQuanBRBZ { get; set; }
        /// <summary>
        /// 授权功能点列表
        /// </summary>
        public static List<E_GY_BINGRENSQGND> BingRenSQGNDList { get; set; }

        /// <summary>
        /// 用户权限集合
        /// </summary>
        public static List<E_GY_YONGHUQX> YongHuQXList;
        private static Dictionary<int, XtraTabPage> commTabPageList = new Dictionary<int, XtraTabPage>();
        public static List<WeakReference<MediLCTabControl>> mediBRTabControl = new List<WeakReference<MediLCTabControl>>();

        /// <summary>
        /// 已打开的窗体
        /// </summary>
        public static Dictionary<OpenCKInfo, MediForm> openwindowsdic = new Dictionary<OpenCKInfo, MediForm>();
        /// <summary>
        /// 已打开的临床窗体
        /// </summary>
        public static Dictionary<string, W_LINCHUANG_BASE> openedTabFormDic = new Dictionary<string, W_LINCHUANG_BASE>();

        /// <summary>
        /// 是否关闭界面
        /// </summary>
        public static bool CloseForm { get; set; }

        public static JCJGYingYongCDService gYYingYongCDService = new JCJGYingYongCDService();

        /// <summary>
        /// 已打开患者所显示的菜单
        /// </summary>
        public static Dictionary<string, E_GY_CAIDAN_NEW> OpenedCaiDanDic = new Dictionary<string, E_GY_CAIDAN_NEW>();

        /// <summary>
        /// 创建窗体对象
        /// </summary>
        /// <param name="kuangjiaForm"></param>
        /// <param name="mediTabControl"></param>
        /// <param name="denglucd"></param>
        /// <param name="openwindowsdic"></param>
        /// <param name="opened"></param>
        public static void CreateForm(dynamic kuangjiaForm, MediTabControl mediTabControl, E_GY_CAIDAN_NEW denglucd, ref Dictionary<OpenCKInfo, MediForm> openwindowsdic, bool opened,bool isChaKanBL=false)
        {
            if (denglucd != null)
            {
                #region 病人权限判断，当按功能点授权时，判断该病人是否有任意功能的权限，如没有就禁止打开，提示用户
                if (showBol)
                {
                    if (!kuangjiaForm.Text.Contains("GONGZUOTAI") && ShouQuanBRBZ == "2" && BingRenSQGNDList.Any(p => p.BINGRENZYID == kuangjiaForm.BingRenZYID))
                    {
                        StringBuilder quanXianCd = new StringBuilder();
                        foreach (var sqgn in BingRenSQGNDList.Where(sqgn => caiDanMCList.ContainsKey(sqgn.SHOUQUANGNDID)))
                        {
                            quanXianCd.Append(caiDanMCList[sqgn.SHOUQUANGNDID] + "；\r\n");
                        }
                        MediMsgBox.Show("您没有" + quanXianCd + "的权限！");
                        if (!BingRenSQGNDList.Any(p => p.SHOUQUANGNDID == denglucd.GONGNENGID && p.BINGRENZYID == kuangjiaForm.BingRenZYID))
                            return;
                    }
                }

                #endregion
                if (LinChuangMainForm != null && LinChuangMainForm.CreateFormEX(denglucd, null)) return;
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));
                var dakaifs = denglucd.DIAOYONGCS.Split('|');
                string chuangkoumc = GetTabPageName(dakaifs);

                OpenCKInfo openCkInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = kuangjiaForm.BingRenID };

                if (openwindowsdic.Count > 0 && openwindowsdic.ContainsKey(openCkInfo) && openwindowsdic[openCkInfo] != null)
                {
                    foreach (XtraTabPage o in mediTabControl.TabPages)
                    {
                        if (o.Name.ToUpper().Equals(chuangkoumc.ToUpper()) && o.Text == denglucd.CAIDANMC)
                        {
                            mediTabControl.SelectedTabPage = o;
                            break;
                        }
                    }
                    return;
                }
                if (denglucd.DIAOYONGCS == null)
                    return;

                var path = AppDomain.CurrentDomain.BaseDirectory;
                var dakaifs3 = denglucd.DIAOYONGCS.Split('|')[3];
                if (dakaifs3?.ToUpper() != "OPENBROWSER")
                {
                    if (!Assemblys[path].ContainsKey(formname.ToUpper()))
                    {
                        MediMsgBox.Info(formname + "窗体在DLL中没有找到");
                        return;
                    }
                }
                OpenWindowFun(kuangjiaForm, mediTabControl, path, formname, denglucd, openCkInfo, opened, isChaKanBL);
            }
        }
        /// <summary>
        /// 创建窗体对象
        /// </summary>
        /// <param name="kuangjiaForm"></param>
        /// <param name="mediTabControl"></param>
        /// <param name="denglucd"></param>
        /// <param name="openwindowsdic"></param>
        /// <param name="buttonform"></param>
        public static void CreateForm(dynamic kuangjiaForm, MediTabControl mediTabControl, E_GY_CAIDAN_NEW denglucd, ref Dictionary<OpenCKInfo, MediForm> openwindowsdic, MediFormWithQX buttonform, bool opened = false, bool isSelect = true)
        {
            if (denglucd != null)
            {
                var formname = buttonform.Name;
                OpenCKInfo openCkInfo = new OpenCKInfo
                {
                    openWindowMode = OpenWindowMode.Menu,
                    xitongid = HISClientHelper.XITONGID,
                    gongnengId = denglucd.GONGNENGID,
                    caidanId = denglucd.CAIDANID,
                    chuangkoumc = formname.ToUpper(),
                    binrenid = kuangjiaForm.BingRenID
                };

                if (openwindowsdic.Count > 0 && openwindowsdic.ContainsKey(openCkInfo)&& isSelect)
                {
                    mediTabControl.TabPages.ToList().ForEach(o =>
                    {
                        if (o.Name.ToUpper().Equals(openCkInfo.chuangkoumc))
                        {
                            mediTabControl.SelectedTabPage = o;
                        }
                    });

                    return;
                }
                if (denglucd.DIAOYONGCS == null)
                    return;

                var path = AppDomain.CurrentDomain.BaseDirectory;
                dynamic form = buttonform;
                OpenWindowFun(kuangjiaForm, mediTabControl, path, formname, denglucd, openCkInfo, buttonform, opened, isSelect);
            }
        }
        /// <summary>
        /// 采用button打开窗体方法
        /// </summary>
        /// <param name="kuangjiaForm"></param>
        /// <param name="mediTabControl"></param>
        /// <param name="path"></param>
        /// <param name="formname"></param>
        /// <param name="denglucd"></param>
        /// <param name="openCKInfo"></param>
        /// <param name="mediFormWithQX"></param>
        /// <param name="opened"></param>
        private static void OpenWindowFun(dynamic kuangjiaForm, MediTabControl mediTabControl, string path, string formname, E_GY_CAIDAN_NEW denglucd, OpenCKInfo openCKInfo, MediFormWithQX mediFormWithQX, bool opened = false, bool isSelect = true)
        {
            var dakaifs = denglucd.DIAOYONGCS.Split('|');
            dynamic form = null;
            bool diaoYongCSModified = false;  //调用参数修改标记，修改之后需再打开菜单之后再改回来
            if (dakaifs.Length >= 3 && !string.IsNullOrWhiteSpace(dakaifs[3]))
            {
                switch (dakaifs[3])
                {
                    #region OPEN
                    case "OPEN":
                        form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                        HISClientHelper.ower = new WeakReference(Assemblys[path][formname.ToUpper()].Value.Location + "|" + Assemblys[path][formname.ToUpper()].Value.FullName.Split(',')[0] + "." + formname.ToUpper());
                        form.WindowState = FormWindowState.Normal;
                        form.MinimizeBox = false;
                        form.MaximizeBox = false;
                        form.FormBorderStyle = FormBorderStyle.FixedSingle;
                        form.StartPosition = FormStartPosition.CenterParent;
                        if (form is MediFormWithQX)
                        {
                            MediFormWithQX mediWithQX = form as MediFormWithQX;
                            mediWithQX.CaiDanID = denglucd.CAIDANID;
                            mediWithQX.DiaoYongCS = denglucd.DIAOYONGCS;
                            mediWithQX.FormClosed += BaseFormCommonHelper_FormClosed;
                        }

                        if (form is W_LINCHUANG_BASE)
                        {
                            kuangjiaForm.InitialKJHelper(form);
                            form.CallBackKuangJiaFunc = kuangjiaForm.ReceiveDataFromLinChuangDelegate;
                            form.bingRenId = kuangjiaForm.BingRenID;
                            form.denglucd = denglucd;
                            form.MuBiaoCKInfo = kuangjiaForm.MuBiaoCKInfo;
                            form.ShowDialogCKBZ = kuangjiaForm.ShowDialogCKBZ;
                        }

                        if (!form.IsDisposed)
                        {
                            openwindowsdic.Add(openCKInfo, form);
                            openAllwindowsdic = openwindowsdic;
                            form.ShowDialog(LinChuangMainForm);
                            form.Dispose();
                        }
                        break;
                    #endregion

                    #region OPENSHEET
                    case "OPENSHEET":
                        XtraTabPage commTabPage = new XtraTabPage();
                        if (dakaifs[dakaifs.Length - 1] != "YES" && mediTabControl != null)
                        {
                            mediTabControl.AppearancePage.Header.Font = new Font("微软雅黑", 15, FontStyle.Regular, GraphicsUnit.Pixel);
                            mediTabControl.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            if (opened)
                                commTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
                            else
                                commTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                            commTabPage.Text = denglucd.CAIDANMC;

                            commTabPage.PageVisible = false;

                            commTabPage.Name = GetTabPageName(dakaifs);
                            if (isSelect)
                            {
                                mediTabControl.SelectedTabPage = commTabPage;
                            }
                           
                            mediTabControl.TabPages.AddRange(new XtraTabPage[] { commTabPage });

                            //add by niquan 2019/8/30 当有医嘱开立时，默认打开医嘱开立
                            //foreach (XtraTabPage tabPage in mediTabControl.TabPages)
                            //{
                            //    if (tabPage.Name.Equals("W_BQYS_YIZHUKL"))
                            //    {
                            //        mediTabControl.SelectedTabPage = tabPage;
                            //    }
                            //}
                        }
                        else
                        {
                            //处理右键打开和双击打开Tab页切换功能处理
                            bool bol = true;
                            if (denglucd.CAIDANMC == kuangjiaForm.TabPageName)
                            {
                                if (kuangjiaForm.Name.Equals("W_YZ_KUANGJIA") || kuangjiaForm.Name.Equals("W_YS_KUANGJIA"))
                                {
                                    foreach (XtraTabPage tabPage in mediTabControl.TabPages)
                                    {
                                        if (tabPage.Text == kuangjiaForm.TabPageName)
                                        {
                                            bol = false;
                                            commTabPage = tabPage;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (bol || denglucd.CAIDANMC != kuangjiaForm.TabPageName)
                                commTabPage = mediTabControl.SelectedTabPage;
                        }

                        //判断是否加载页面
                        if (dakaifs[dakaifs.Length - 1] == "NO" && !openwindowsdic.ContainsKey(openCKInfo))
                        {
                            openwindowsdic.Add(openCKInfo, form);
                            openAllwindowsdic = openwindowsdic;
                            commTabPage.PageVisible = true;
                            break;
                        }
                        else
                        {
                            form = mediFormWithQX;// Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                        }

                        if (denglucd.DIAOYONGCS.Contains("YES"))
                        {
                            denglucd.DIAOYONGCS = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.Length - 4);
                            diaoYongCSModified = true;
                        }
                        Mediinfo.HIS.Core.HISClientHelper.ower = new WeakReference(Assemblys[path][formname.ToUpper()].Value.Location + "|" + Assemblys[path][formname.ToUpper()].Value.FullName.Split(',')[0] + "." + formname.ToUpper());
                        if (mediBRTabControl != null && !mediBRTabControl.Exists(m => m.Target?.Handle == mediTabControl.Handle))
                            mediBRTabControl.Add(new WeakReference<MediLCTabControl>(mediTabControl as MediLCTabControl));
                        MediPanelControl mediPanelControl = new MediPanelControl();

                        mediPanelControl.Visible = false;
                        mediPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        mediPanelControl.Dock = DockStyle.Fill;
                        commTabPage.Controls.Add(mediPanelControl);

                        form.TopLevel = false;

                        //form.Parent = mediPanelControl;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;

                        mediPanelControl.Controls.Add(form);

                        //if (kuangjiaForm.Name.Equals("W_YZ_KUANGJIA"))
                        //{
                        //    if (!string.IsNullOrWhiteSpace(kuangjiaForm.TabPageName))
                        //    {
                        //        foreach (XtraTabPage tabPage in mediTabControl.TabPages)
                        //        {
                        //            if (tabPage.Text == kuangjiaForm.TabPageName)
                        //            {
                        //                mediTabControl.SelectedTabPage = tabPage;
                        //            }
                        //        }
                        //    }
                        //}

                        //form.WindowState = FormWindowState.Maximized;
                        mediPanelControl.BringToFront();
                        if (form is MediFormWithQX)
                        {
                            MediFormWithQX mediFormWithQX1 = form as MediFormWithQX;
                            mediFormWithQX1.CaiDanID = denglucd.CAIDANID;
                            mediFormWithQX1.DiaoYongCS = denglucd.DIAOYONGCS;
                            //mediFormWithQX.FormClosing += BaseFormCommonHelper_FormClosing;
                            mediFormWithQX1.MediLCTabControl = mediTabControl;
                        }

                        if (form is W_LINCHUANG_BASE)
                        {
                            kuangjiaForm.InitialKJHelper(form);
                            string tempFormName = form.Name.ToUpper();
                            if (!openedTabFormDic.ContainsKey(tempFormName))
                                openedTabFormDic.Add(tempFormName, form);
                            //if (!openedTabFormDic.ContainsKey(tempFormName))
                            //    openedTabFormDic.Add(tempFormName, form);
                            form.openedTabFormDic = openedTabFormDic;
                            form.CallBackKuangJiaFunc = kuangjiaForm.ReceiveDataFromLinChuangDelegate;
                            form.denglucd = denglucd;
                            form.bingRenId = kuangjiaForm.BingRenID;
                            form.MuBiaoCKInfo = kuangjiaForm.MuBiaoCKInfo;
                            form.ShowDialogCKBZ = kuangjiaForm.ShowDialogCKBZ;
                        }
                        if (form is MediFormLCWithQX)
                        {
                            if (kuangjiaForm is W_GY_GZTBSAE)
                            {
                                form.OpenTabWindowByButton = ((W_GY_GZTBSAE)kuangjiaForm).OpenlcWindow;
                            }
                            form.CaiDanList = CaiDanList;
                            form.YongHuQXList = YongHuQXList;
                            form.currentCaiDanId = denglucd.CAIDANID;

                            form.ActiveEvents();
                            if (form.IsDisposed)
                                break;
                            form.InitialCaiDanData();
                        }

                        if (form.IsDisposed)
                        {
                            commTabPage.PageVisible = false;
                            mediPanelControl.Visible = false;
                        }
                        else
                        {
                            if (!openwindowsdic.ContainsKey(openCKInfo))
                                openwindowsdic.Add(openCKInfo, form);
                            else
                                openwindowsdic[openCKInfo] = form;
                            openAllwindowsdic = openwindowsdic;
                            ////add by niquan 2019/9/5
                            ////如果是医嘱开立界面，则给Tag属性赋值
                            //if (form.Name == "W_BQYS_YIZHUKL")
                            //{
                            //    form.Tag = kuangjiaForm.Tag;
                            //}
                            form.Show();
                            commTabPage.PageVisible = true;
                            mediPanelControl.Visible = true;
                            //if (dakaifs[dakaifs.Length - 1] == "1")
                            //{
                            //    mediTabControl.SelectedTabPage = commTabPage;
                            //}
                            //else if (!opened)
                            //{
                            //    mediTabControl.SelectedTabPage = commTabPage;
                            //}
                            if (isSelect)                             
                            mediTabControl.SelectedTabPage = commTabPage;
                        }
                        if (!commTabPageList.ContainsKey(form.GetHashCode()))
                            commTabPageList.Add(form.GetHashCode(), commTabPage);
                        if (diaoYongCSModified)
                            denglucd.DIAOYONGCS = denglucd.DIAOYONGCS + "|NO";
                        break;
                    #endregion

                    default:
                        break;
                }

            }
        }

        private static void OpenWindowFun(dynamic kuangjiaForm, MediTabControl mediTabControl, string path, string formname, E_GY_CAIDAN_NEW denglucd, OpenCKInfo openCKInfo, bool opened, bool isChaKanBL = false)
        {
            var dakaifs = denglucd.DIAOYONGCS.Split('|');
            dynamic form = null;
            bool diaoYongCSModified = false;  //调用参数修改标记，修改之后需在打开菜单之后再改回来，解决HB6-8423(558813)
            if (dakaifs.Length >= 3 && !string.IsNullOrWhiteSpace(dakaifs[3]))
            {
                switch (dakaifs[3])
                {
                    #region OPEN
                    case "OPEN":
                        form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                        HISClientHelper.ower = new WeakReference(form);
                        form.MdiParent = null;
                        form.WindowState = FormWindowState.Normal;
                        form.MinimizeBox = false;
                        form.MaximizeBox = false;
                        form.FormBorderStyle = FormBorderStyle.FixedSingle;
                        form.StartPosition = FormStartPosition.CenterParent;
                        if (form is MediFormWithQX mediWithQx)
                        {
                            mediWithQx.CaiDanID = denglucd.CAIDANID;
                            mediWithQx.DiaoYongCS = denglucd.DIAOYONGCS;
                            mediWithQx.FormClosed += BaseFormCommonHelper_FormClosed;
                        }

                        if (form is W_LINCHUANG_BASE)
                        {
                            kuangjiaForm.InitialKJHelper(form);
                            form.CallBackKuangJiaFunc = kuangjiaForm.ReceiveDataFromLinChuangDelegate;
                            form.bingRenId = kuangjiaForm.BingRenID;
                            form.denglucd = denglucd;
                            form.MuBiaoCKInfo = kuangjiaForm.MuBiaoCKInfo;
                            form.ShowDialogCKBZ = kuangjiaForm.ShowDialogCKBZ;
                        }

                        if (!form.IsDisposed)
                        {
                            openwindowsdic.Add(openCKInfo, form);
                            openAllwindowsdic = openwindowsdic;
                            form.ShowDialog(LinChuangMainForm);
                            form.Dispose();
                        }
                        break;
                    #endregion

                    #region OPENSHEET
                    case "OPENSHEET":
                        XtraTabPage commTabPage = new XtraTabPage();
                        if (dakaifs[dakaifs.Length - 1] != "YES" && mediTabControl != null)
                        {
                            mediTabControl.AppearancePage.Header.Font = new Font("微软雅黑", 15, FontStyle.Regular, GraphicsUnit.Pixel);
                            mediTabControl.AppearancePage.Header.TextOptions.HAlignment = HorzAlignment.Center;
                            commTabPage.ShowCloseButton = opened ? DefaultBoolean.False : DefaultBoolean.True;
                            commTabPage.Text = denglucd.CAIDANMC;

                            commTabPage.PageVisible = false;

                            commTabPage.Name = GetTabPageName(dakaifs);

                            mediTabControl.SelectedTabPage = commTabPage;
                            mediTabControl.TabPages.AddRange(new XtraTabPage[] { commTabPage });
                        }
                        else
                        {
                            //处理右键打开和双击打开Tab页切换功能处理
                            bool bol = true;
                            if (denglucd.CAIDANMC == kuangjiaForm.TabPageName)
                            {
                                if (kuangjiaForm.Name.Equals("W_YZ_KUANGJIA") || kuangjiaForm.Name.Equals("W_YS_KUANGJIA"))
                                {
                                    foreach (XtraTabPage tabPage in mediTabControl.TabPages)
                                    {
                                        if (tabPage.Text == kuangjiaForm.TabPageName)
                                        {
                                            bol = false;
                                            commTabPage = tabPage;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (bol || denglucd.CAIDANMC != kuangjiaForm.TabPageName)
                                commTabPage = mediTabControl.SelectedTabPage;
                        }

                        //判断是否加载页面
                        if (dakaifs[dakaifs.Length - 1] == "NO" && !openwindowsdic.ContainsKey(openCKInfo))
                        {
                            openwindowsdic.Add(openCKInfo, form);
                            openAllwindowsdic = openwindowsdic;
                            commTabPage.PageVisible = true;
                            break;
                        }
                        else
                        {
                            form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                        }

                        if (denglucd.DIAOYONGCS.Contains("YES"))
                        {
                            denglucd.DIAOYONGCS = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.Length - 4);
                            diaoYongCSModified = true;
                        }
                        HISClientHelper.ower = new WeakReference(form);

                        if (mediBRTabControl != null && mediTabControl != null && !mediBRTabControl.Where(o => o.Target != null && !o.Target.IsDisposed).ToList().Exists(m => m.Target?.Handle == mediTabControl.Handle))
                            mediBRTabControl.Add(new WeakReference<MediLCTabControl>(mediTabControl as MediLCTabControl));

                        MediPanelControl mediPanelControl = new MediPanelControl();

                        mediPanelControl.Visible = false;
                        mediPanelControl.BorderStyle = BorderStyles.NoBorder;
                        mediPanelControl.Dock = DockStyle.Fill;
                        commTabPage.Controls.Add(mediPanelControl);

                        form.TopLevel = false;

                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        if (form.FormName == "W_ZJ_MenZhenBL")
                        {
                            if (isChaKanBL)
                                form.SetChaKanBL();

                            form.OpenMenuAction = kuangjiaForm.OpenMenuAction;
                        }
                        mediPanelControl.Controls.Add(form);
                        mediPanelControl.BringToFront();
                        if (form is MediFormWithQX mediFormWithQx)
                        {
                            mediFormWithQx.CaiDanID = denglucd.CAIDANID;
                            mediFormWithQx.DiaoYongCS = denglucd.DIAOYONGCS;
                            if(mediTabControl !=null)
                                mediFormWithQx.MediLCTabControl = mediTabControl;
                            //mediFormWithQx.MediLCTabControl = mediTabControl;
                            form.bingRenId = kuangjiaForm.BingRenID;
                        }

                        if (form is W_LINCHUANG_BASE)
                        {
                            kuangjiaForm.InitialKJHelper(form);
                            string tempFormName = form.Name.ToUpper();
                            if (!openedTabFormDic.ContainsKey(tempFormName))
                                openedTabFormDic.Add(tempFormName, form);
                            form.openedTabFormDic = openedTabFormDic;
                            form.CallBackKuangJiaFunc = kuangjiaForm.ReceiveDataFromLinChuangDelegate;
                            form.denglucd = denglucd;
                            form.bingRenId = kuangjiaForm.BingRenID;
                            form.MuBiaoCKInfo = kuangjiaForm.MuBiaoCKInfo;
                            form.ShowDialogCKBZ = kuangjiaForm.ShowDialogCKBZ;
                        }

                        if (form is MediFormLCWithQX)
                        {
                            if (kuangjiaForm is W_GY_GZTBSAE gztbsae)
                            {
                                form.OpenTabWindowByButton = gztbsae.OpenlcWindow;
                            }
                            form.CaiDanList = CaiDanList;
                            form.YongHuQXList = YongHuQXList;
                            form.currentCaiDanId = denglucd.CAIDANID;

                            form.ActiveEvents();
                            if (form.IsDisposed)
                                break;
                            form.InitialCaiDanData();
                        }

                        if (form.IsDisposed)
                        {
                            commTabPage.PageVisible = false;
                            mediPanelControl.Visible = false;
                        }
                        else
                        {
                            if (!openwindowsdic.ContainsKey(openCKInfo))
                                openwindowsdic.Add(openCKInfo, form);
                            else
                                openwindowsdic[openCKInfo] = form;
                            openAllwindowsdic = openwindowsdic;
                            form.Show();
                            commTabPage.PageVisible = true;
                            mediPanelControl.Visible = true;
                            mediTabControl.SelectedTabPage = commTabPage;
                        }
                        if (!commTabPageList.ContainsKey(form.GetHashCode()))
                            commTabPageList.Add(form.GetHashCode(), commTabPage);
                        if (diaoYongCSModified)
                            denglucd.DIAOYONGCS = denglucd.DIAOYONGCS + "|NO";
                        break;
                    #endregion
                    #region OPENBROWSER
                    case "OPENBROWSER":
                    {
                        var uri = dakaifs[2];
                        Process process = new Process();
                        process.StartInfo.FileName = uri;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        process.Start();
                    }
                        break;
                    #endregion
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// 获取TAB页名称
        /// </summary>
        /// <param name="dakaifs">调用参数</param>
        /// <returns></returns>
        private static string GetTabPageName(string[] dakaifs)
        {
            string pageName = "";
            //当传入参数不为空时，name要加上传入参数，防止同一个窗体的name名相同的问题，使用gnongnengid会与name不符
            if (dakaifs.Length > 2)
            {
                if (!string.IsNullOrWhiteSpace(dakaifs[2]))
                    pageName = dakaifs[0] + "|" + dakaifs[2];
                else
                    pageName = dakaifs[0];
            }
            return pageName;
        }

        /// <summary>
        /// 加载默认打开的窗体
        /// </summary>
        public static void LoadDefaultOpenForm(dynamic form, string currentMenuId, MediTabControl tabComntrol, Dictionary<string, dynamic> openedlcdic,bool isChaKanBL=false)
        {
            //bluess = true;
            if (CaiDanList.Count < 1)
                return;
            int caidanLength = 0;
            if (CaiDanList.Count > 0)
            {
                caidanLength = CaiDanList.Count - 1;
            }
            if (caidanLength < 1) return;
            if (YongHuQXList == null || YongHuQXList.Count > 0)
            {
                YongHuQXList = GYQuanXianHelper.GetQuanXian();
            }
            for (int i = caidanLength; i >= 0; i--)
            {
                string shangjicdid = CaiDanList[i].SHANGJICDID;
                if (shangjicdid == "-" || shangjicdid == null)
                {
                    shangjicdid = "";
                }
                if (shangjicdid.Length > 0)
                {
                    string gongnengid = CaiDanList[i].GONGNENGID;
                    if (YongHuQXList != null)
                    {
                        //判断功能权限
                        var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                        if (yonghuqx == null)
                        {
                            CaiDanList[i].Delete();
                        }
                    }
                }
            }

            //移除没有权限的菜单
            if (GYCanShuHelper.GetCanShu("是否启用菜单权限控制", "0").Equals("1"))
                CaiDanList.RemoveAll(c => c.GetState() == DTOState.Delete);

            E_GY_CAIDAN_NEW caiDanNew = new E_GY_CAIDAN_NEW();
            // todo 要求工作台直接打开病人病历，暂时定死这样写，后续有更好的办法再修改
            if (DataMiddleWare.IsJumpToOpenVisit && currentMenuId.Equals("1") && CaiDanList.Where(o => o.CAIDANMC.Equals("门诊病历")).ToList().FirstOrDefault().ISOPEN == 1)
            {
                currentMenuId = "2";
                form.CurrentMainCaiDanID = "2";
            }
            List<E_GY_CAIDAN_NEW> subCaiDanList = CaiDanList.Where(o => o.YINGYONGID == HISClientHelper.YINGYONGID && o.SHANGJICDID.Equals(currentMenuId) && o.ISOPEN.Equals(1)).OrderBy(o => o.SHUNXUHAO).ToList();
            foreach (E_GY_CAIDAN_NEW cd in subCaiDanList)
            {
                if (cd.DIAOYONGCS.Split('|')[cd.DIAOYONGCS.Split('|').Length - 1] == "1")
                {
                    caiDanNew = cd;
                    break;
                }
            }
            List<E_GY_CAIDAN_NEW> subCaiDanListNew = CaiDanList.Where(o => o.YINGYONGID == HISClientHelper.YINGYONGID && o.SHANGJICDID.Equals(currentMenuId) && o.ISOPEN.Equals(1)).OrderBy(o => o.SHUNXUHAO).ToList();
            if (currentMenuId == "2")//右键跳转对应菜单
            {
                string str = string.Empty;
                string[] kuangJiaName = { "W_YZ_KUANGJIA", "W_YS_KUANGJIA", "W_SM_KUANGJIA" };
                if (kuangJiaName.Contains(((Control.ControlAccessibleObject)((Control)form).AccessibilityObject)?.Name))
                {
                    if (((W_BINGRENKJ_BASE)form).MuBiaoCKInfo != null)
                        str = ((W_BINGRENKJ_BASE)form).MuBiaoCKInfo.GongNengCKMC?.TrimStart('I') + "|" + ((W_BINGRENKJ_BASE)form).MuBiaoCKInfo.GongNengID;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    if (!subCaiDanListNew.Any(p => p.DIAOYONGCS.Contains(str)))
                    {
                        subCaiDanListNew.Add(CaiDanList.Where(p => p.DIAOYONGCS != null && p.DIAOYONGCS.Contains(str)).ToList()[0]);
                        caiDanNew = CaiDanList.Where(p => p.DIAOYONGCS != null && p.DIAOYONGCS.Contains(str)).ToList()[0];
                    }
                    else
                    {
                        caiDanNew = subCaiDanListNew.Where(p => p.DIAOYONGCS != null && p.DIAOYONGCS.Contains(str)).ToList()[0];
                    }
                    caiDanNew.DIAOYONGCS += "|1";
                }
            }
            int index = 0;
            caiDanMCList = new Dictionary<string, string>();
            showBol = false;
            //一级菜单
            // 对菜单按照顺序号按数值大小进行排序
            subCaiDanListNew.Sort((x, y) => x.SHUNXUHAO.ToInt().CompareTo(y.SHUNXUHAO.ToInt()));
            subCaiDanListNew.ForEach(o =>
            {
                if (!caiDanMCList.ContainsKey(o.GONGNENGID))
                    caiDanMCList.Add(o.GONGNENGID, o.CAIDANMC);
                if (caiDanMCList.Count == subCaiDanListNew.Count)
                    showBol = true;
                var count = CaiDanList.Count(r => r.SHANGJICDID.Equals(o.CAIDANID) && r.ISOPEN.Equals(1));
                if (count > 0)
                {
                    if (subCaiDanList.Contains(o))
                    {
                        subCaiDanList.Remove(o);
                        subCaiDanList.Add(o);
                    }
                    else
                    {
                        subCaiDanList.Add(o);
                    }
                }
                else
                {
                    //create tabform
                    if (o == caiDanNew)
                    {
                        if (o.DIAOYONGCS.Contains("NO"))
                            o.DIAOYONGCS = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.Length - 3);
                        CreateForm(form, tabComntrol, caiDanNew, ref openwindowsdic, true);

                    }
                    else if (string.IsNullOrEmpty(caiDanNew.DIAOYONGCS) && index == 0)
                    {
                        if (o.DIAOYONGCS.Contains("NO"))
                            o.DIAOYONGCS = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.Length - 3);
                        CreateForm(form, tabComntrol, o, ref openwindowsdic, true,isChaKanBL);
                    }
                    else
                    {
                        if (!o.DIAOYONGCS.Contains("NO"))
                            o.DIAOYONGCS += "|NO";
                        CreateForm(form, tabComntrol, o, ref openwindowsdic, true);
                    }
                    var formname = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.IndexOf('|') == -1 ? o.DIAOYONGCS.Length : o.DIAOYONGCS.IndexOf('|'));
                    if (!string.IsNullOrWhiteSpace(formname))
                    {
                        OpenCKInfo openCkInfo = new OpenCKInfo { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = o.GONGNENGID, caidanId = o.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = form.BingRenID };
                        if (!openedlcdic.ContainsKey(openCkInfo.chuangkoumc))
                        {
                            if (openwindowsdic.ContainsKey(openCkInfo))
                            {
                                openedlcdic.Add(openCkInfo.chuangkoumc, openwindowsdic[openCkInfo] as W_LINCHUANG_BASE);
                            }
                        }
                    }
                }
                CreateDefaultOpenForm(form, tabComntrol, CaiDanList, subCaiDanList, openedlcdic, isChaKanBL);
                index++;
            });
        }

        /// <summary>
        /// 加载默认打开的窗体
        /// </summary>
        public static void LoadDefaultOpenForm(dynamic form, string currentMenuId, MediTabControl tabComntrol, Dictionary<string, dynamic> openedlcdic, string MuBiaoYY)
        {
            List<E_GY_CAIDAN_NEW> MuBiaoCaiDan = new List<E_GY_CAIDAN_NEW>();

            var caidanret = gYYingYongCDService.GetGongYongCD(MuBiaoYY);
            if (caidanret.ReturnCode != ReturnCode.SUCCESS)
            {
                MediMsgBox.Failure("获取菜单失败！", caidanret);
                return;
            }

            MuBiaoCaiDan = caidanret.Return;

            //bluess = true;
            if (MuBiaoCaiDan.Count < 1)
                return;
            int caidanLength = 0;
            if (MuBiaoCaiDan.Count > 0)
            {
                caidanLength = MuBiaoCaiDan.Count - 1;
            }
            if (caidanLength < 1) return;
            for (int i = caidanLength; i >= 0; i--)
            {
                string shangjicdid = MuBiaoCaiDan[i].SHANGJICDID;
                if (shangjicdid == "-" || shangjicdid == null)
                {
                    shangjicdid = "";
                }
                if (shangjicdid.Length > 0)
                {
                    string gongnengid = MuBiaoCaiDan[i].GONGNENGID;
                    if (YongHuQXList != null)
                    {
                        //判断功能权限
                        var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                        if (yonghuqx == null)
                        {
                            MuBiaoCaiDan[i].Delete();
                        }
                    }
                }
            }
            E_GY_CAIDAN_NEW caiDanNew = new E_GY_CAIDAN_NEW();
            List<E_GY_CAIDAN_NEW> subMuBiaoCaiDan = MuBiaoCaiDan.Where(o => o.SHANGJICDID.Equals(currentMenuId) && o.ISOPEN.Equals(1)).OrderBy(o => o.SHUNXUHAO).ToList();
            foreach (E_GY_CAIDAN_NEW cd in subMuBiaoCaiDan)
            {
                if (cd.DIAOYONGCS.Split('|')[cd.DIAOYONGCS.Split('|').Length - 1] == "1")
                {
                    caiDanNew = cd;
                    break;
                }
            }
            List<E_GY_CAIDAN_NEW> subMuBiaoCaiDanNew = MuBiaoCaiDan.Where(o => o.SHANGJICDID.Equals(currentMenuId) && o.ISOPEN.Equals(1)).OrderBy(o => o.SHUNXUHAO).ToList();
            if (currentMenuId == "2")//右键跳转对应菜单
            {
                string str = string.Empty;
                string[] kuangJiaName = { "W_YZ_KUANGJIA", "W_YS_KUANGJIA" , "W_SM_KUANGJIA" };
                if (kuangJiaName.Contains(((Control.ControlAccessibleObject)((Control)form).AccessibilityObject)?.Name))
                {
                    if (((W_BINGRENKJ_BASE)form).MuBiaoCKInfo != null)
                        str = ((W_BINGRENKJ_BASE)form).MuBiaoCKInfo.GongNengCKMC?.TrimStart('I') + "|" + ((W_BINGRENKJ_BASE)form).MuBiaoCKInfo.GongNengID;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    if (!subMuBiaoCaiDanNew.Any(p => p.DIAOYONGCS.Contains(str)))
                    {
                        subMuBiaoCaiDanNew.Add(MuBiaoCaiDan.Where(p => p.DIAOYONGCS != null && p.DIAOYONGCS.Contains(str)).ToList()[0]);
                        caiDanNew = MuBiaoCaiDan.Where(p => p.DIAOYONGCS != null && p.DIAOYONGCS.Contains(str)).ToList()[0];
                    }
                    else
                    {
                        caiDanNew = subMuBiaoCaiDanNew.Where(p => p.DIAOYONGCS != null && p.DIAOYONGCS.Contains(str)).ToList()[0];
                    }
                    caiDanNew.DIAOYONGCS += "|1";
                }
            }
            int index = 0;
            caiDanMCList = new Dictionary<string, string>();
            showBol = false;
            //一级菜单
            // 对菜单按照顺序号按数值大小进行排序
            subMuBiaoCaiDanNew.Sort((x, y) => x.SHUNXUHAO.ToInt().CompareTo(y.SHUNXUHAO.ToInt()));
            subMuBiaoCaiDanNew.ForEach(o =>
            {
                if (!caiDanMCList.ContainsKey(o.GONGNENGID))
                    caiDanMCList.Add(o.GONGNENGID, o.CAIDANMC);
                if (caiDanMCList.Count == subMuBiaoCaiDanNew.Count)
                    showBol = true;
                var count = MuBiaoCaiDan.Count(r => r.SHANGJICDID.Equals(o.CAIDANID) && r.ISOPEN.Equals(1));
                if (count > 0)
                {
                    if (subMuBiaoCaiDan.Contains(o))
                    {
                        subMuBiaoCaiDan.Remove(o);
                        subMuBiaoCaiDan.Add(o);
                    }
                    else
                    {
                        subMuBiaoCaiDan.Add(o);
                    }
                }
                else
                {
                    //create tabform
                    if (o == caiDanNew)
                    {
                        //bluess = false;
                        if (o.DIAOYONGCS.Contains("NO"))
                            o.DIAOYONGCS = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.Length - 3);
                        CreateForm(form, tabComntrol, caiDanNew, ref openwindowsdic, true);

                    }
                    else if (string.IsNullOrEmpty(caiDanNew.DIAOYONGCS) && index == 0)
                    {
                        //bluess = false;
                        if (o.DIAOYONGCS.Contains("NO"))
                            o.DIAOYONGCS = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.Length - 3);
                        CreateForm(form, tabComntrol, o, ref openwindowsdic, true);
                    }
                    else
                    {
                        if (!o.DIAOYONGCS.Contains("NO"))
                            o.DIAOYONGCS += "|NO";
                        CreateForm(form, tabComntrol, o, ref openwindowsdic, true);
                    }
                    var formname = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.IndexOf('|') == -1 ? o.DIAOYONGCS.Length : o.DIAOYONGCS.IndexOf('|'));
                    if (!string.IsNullOrWhiteSpace(formname))
                    {
                        OpenCKInfo openCKInfo = new OpenCKInfo { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = o.GONGNENGID, caidanId = o.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = form.BingRenID };
                        if (!openedlcdic.ContainsKey(openCKInfo.chuangkoumc))
                        {
                            if (openwindowsdic.ContainsKey(openCKInfo))
                            {
                                openedlcdic.Add(openCKInfo.chuangkoumc, openwindowsdic[openCKInfo] as W_LINCHUANG_BASE);
                            }
                        }
                    }
                }
                CaiDanList.Add(o);

                CreateDefaultOpenForm(form, tabComntrol, MuBiaoCaiDan, subMuBiaoCaiDan, openedlcdic);
                index++;
            });
        }

        /// <summary>
        /// 默认打开的窗体
        /// </summary>
        public static void CreateDefaultOpenForm(dynamic form, MediTabControl tabComntrol, List<E_GY_CAIDAN_NEW> allcaidanList, List<E_GY_CAIDAN_NEW> openedcaidanList, Dictionary<string, dynamic> openedlcdic, bool isChaKanBL = false)
        {
            foreach (E_GY_CAIDAN_NEW o in openedcaidanList)
            {
                List<E_GY_CAIDAN_NEW> subCaiDanList = new List<E_GY_CAIDAN_NEW>();
                allcaidanList.Where(p => p.SHANGJICDID.Equals(o.CAIDANID) && p.ISOPEN.Equals(1)).OrderBy(p => int.Parse(p.SHUNXUHAO)).GroupBy(p => p.CAIDANID).ToList().ForEach(q =>
                {
                    var caidan = q.FirstOrDefault();
                    var count = allcaidanList.Count(r => caidan != null && (r.SHANGJICDID.Equals(caidan.CAIDANID) && r.ISOPEN.Equals(1)));
                    if (count > 0)
                    {
                        if (subCaiDanList.Contains(caidan))
                        {
                            subCaiDanList.Remove(caidan);
                            subCaiDanList.Add(caidan);
                        }
                        else
                        {
                            subCaiDanList.Add(caidan);
                        }
                    }
                    else
                    {
                        //create form
                        CreateForm(form, tabComntrol, caidan, ref openwindowsdic, false);
                        var formname = o.DIAOYONGCS.Substring(0, o.DIAOYONGCS.IndexOf('|') == -1 ? o.DIAOYONGCS.Length : o.DIAOYONGCS.IndexOf('|'));
                        if (!string.IsNullOrWhiteSpace(formname))
                        {
                            OpenCKInfo openCKInfo = new OpenCKInfo { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = o.GONGNENGID, caidanId = o.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = form.BingRenID };
                            if (!openedlcdic.ContainsKey(openCKInfo.chuangkoumc))
                            {
                                openedlcdic.Add(openCKInfo.chuangkoumc, openwindowsdic[openCKInfo] as W_LINCHUANG_BASE);
                            }
                        }
                    }
                });

                if (subCaiDanList.Count > 0)
                {
                    CreateDefaultOpenForm(form, tabComntrol, allcaidanList, subCaiDanList, openedlcdic);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediFormWithQX"></param>
        public static void TabClose(MediFormWithQX mediFormWithQX)
        {
            foreach (KeyValuePair<int, XtraTabPage> kv in commTabPageList)
            {
                if (kv.Key == mediFormWithQX.GetHashCode())
                {
                    MediTabClosed(kv.Value);
                    commTabPageList.Remove(kv.Key);
                    openedTabFormDic.Remove(mediFormWithQX.Name.ToUpper());
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xtraTabPage"></param>
        public static void MediTabClosed(XtraTabPage xtraTabPage)
        {
            if (openedTabFormDic.ContainsKey(xtraTabPage.Name.ToUpper()))
            {
                openedTabFormDic.Remove(xtraTabPage.Name.ToUpper());
            }
            foreach (Control control in xtraTabPage.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE tempForm)
                    {
                        OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = tempForm.denglucd.GONGNENGID, caidanId = tempForm.denglucd.CAIDANID, chuangkoumc = tempForm.Name.ToUpper(), binrenid = tempForm.bingRenId };
                        if (openAllwindowsdic.ContainsKey(openCKInfo))
                        {
                            openAllwindowsdic.Remove(openCKInfo);
                        }

                        tempForm.Close();
                    }
                }
            }
            xtraTabPage.PageVisible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public static void BaseFormCommonHelper_FormClosing(MediFormWithQX sender)
        {
            if (sender != null)
            {
                MediFormWithQX mediFormWithQX = sender;
                TabClose(mediFormWithQX);
                if (mediFormWithQX.IsDisposed)
                {
                    foreach (XtraTabPage tabpage in mediFormWithQX.MediLCTabControl.TabPages)
                    {
                        if (mediFormWithQX.Name.ToUpper().Equals(tabpage.Name.ToUpper()))
                        {
                            mediFormWithQX.MediLCTabControl.TabPages.Remove(tabpage);
                            break;
                        }
                    }

                    OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = mediFormWithQX.GongNengId, caidanId = mediFormWithQX.CaiDanID, chuangkoumc = mediFormWithQX.Name.ToUpper(), binrenid = mediFormWithQX.bingRenId };
                    if (string.IsNullOrEmpty(openCKInfo.binrenid))
                    {
                        throw new Exception("病人ID为空");
                    }
                    if (openwindowsdic.ContainsKey(openCKInfo))
                    {
                        openwindowsdic.Remove(openCKInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 打开工作台菜单
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        public static void OpenGongZuoTaiCD(string menuID)
        {
            //add by 余佳平
            if (!string.IsNullOrWhiteSpace(menuID) && HISClientHelper.MainForm is LinChuangMainFormBase mainForm)
            {
                //切换至工作台界面
                if (mainForm.mediLCMainFormTabControl.SelectedTabPageIndex > 0)
                    mainForm.mediLCMainFormTabControl.SelectedTabPageIndex = 0;
                //防止界面停止响应，刷新UI，主要用于应对登录系统后未手动点击工作台而直接调用该方法，工作台显示首页菜单而不是指定菜单的情况
                Application.DoEvents();
                
                if (mainForm.mediLCMainFormTabControl.TabPages[0].Controls[0].Controls[0] is W_GY_GZTBSAE @base)
                {
                    @base.OpenFormFromTab(menuID);
                }

            }
        }

        private static void BaseFormCommonHelper_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is MediFormWithQX mediFormWithQx)
            {
                OpenCKInfo openCKInfo = new OpenCKInfo
                {
                    openWindowMode = OpenWindowMode.Menu,
                    xitongid = HISClientHelper.XITONGID,
                    gongnengId = mediFormWithQx.GongNengId,
                    caidanId = mediFormWithQx.CaiDanID,
                    chuangkoumc = mediFormWithQx.Name.ToUpper()
                };
                if (openAllwindowsdic.ContainsKey(openCKInfo))
                {
                    openAllwindowsdic.Remove(openCKInfo);
                }
            }
        }

        /// <summary>
        /// 打开工作台tab页菜单
        /// </summary>
        /// <param name="menuId"></param>
        public static void OpenGongZuoTaiTabMenu(string menuId)
        {
            if (!string.IsNullOrWhiteSpace(menuId) && HISClientHelper.MainForm is LinChuangMainFormBase mainForm)
            {
                //切换至工作台界面
                if (mainForm.mediLCMainFormTabControl.SelectedTabPageIndex > 0)
                    mainForm.mediLCMainFormTabControl.SelectedTabPageIndex = 0;

                //防止界面停止响应，刷新UI，主要用于应对登录系统后未手动点击工作台而直接调用该方法，工作台显示首页菜单而不是指定菜单的情况
                Application.DoEvents();

                if (mainForm.mediLCMainFormTabControl.TabPages[0].Controls[0].Controls[0] is W_GY_GZTBSAE @base)
                {
                    @base.OpenFormFromTab(menuId);
                }
            }
        }

        /// <summary>
        /// 移除按钮
        /// </summary>
        /// <param name="formhashcode"></param>
        /// <returns></returns>
        public static void RemovePage(MediFormWithQX mediFormWithQX)
        {
            foreach (KeyValuePair<int, XtraTabPage> kv in commTabPageList)
            {
                if (kv.Key == mediFormWithQX.GetHashCode())
                {
                    MediTabClosed(kv.Value);
                    commTabPageList.Remove(kv.Key);
                    return;
                }
            }
        }

    }
}
