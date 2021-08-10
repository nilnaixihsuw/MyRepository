using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls.SecondaryFramework;
using Mediinfo.WinForm.HIS.Controls.TabForm;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LogHelper = Mediinfo.Enterprise.Log.LogHelper;

namespace Mediinfo.WinForm.HIS.Controls.FirstLevelFramework
{
    public partial class LinChuangMainFormBase : MediUniversalMFBase
    {
        #region LastInputInfo

        [StructLayout(LayoutKind.Sequential)]
        public struct LastInputInfo
        {
            /// <summary>
            /// 指示如何在托管代码和非托管代码之间封送数据
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]   // 4字节无符号整数
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTime;
        }

        #endregion

        #region fields

        /// <summary>
        /// 新增Tab页所在位置的索引(不设置则默认在最后)
        /// </summary>
        public int NewPageIndex { set; get; } = -1;

        #endregion

        protected event Action<MainFormOpenEventArgs> BeforeMainFormOpenEevent;
        protected event Action<MainFormOpenEventArgs> ClosingMainFormOpenEevent;

        /// <summary>
        /// 科室切换事件
        /// </summary>
        protected event Action<object, MainFormOpenEventArgs> ChangeKeShiEevent;
        /// <summary>
        /// 重新登录事件
        /// </summary>
        protected event Action<object, MainFormOpenEventArgs> LockScreenEevent;
        /// <summary>
        /// 操作手册
        /// </summary>
        protected event Action<object, MainFormOpenEventArgs> CaoZuoSCEevent;

        public Dictionary<string, dynamic> firstMainTab = new Dictionary<string, dynamic>();

        /// <summary>
        /// 
        /// </summary>
        public LinChuangMainFormBase()
        {
            InitializeComponent();

            // 注册事件
            RegisterEvents();

            if (!SkinCat.Instance.IsDesignMode)
                LoadDefaultMenuAndToolBar();
        }

        /// <summary>
        /// 设置状态栏的科室名称或者病区名称
        /// </summary>
        public void SetStatusBarKeShiBq()
        {
            if (HISClientHelper.XITONGID.Equals("04"))
            {
                userNamebsi.Caption = HISClientHelper.JIUZHENKSMC;
                if (HISClientHelper.JIUZHENGHLB == HISGlobalHelper.GuaHaoLBGL.GuaHaoLBGL_JiZhen)
                {
                    barStaticItemZhuanJia.Caption = @"(急诊)";
                }
                else
                {
                    if (HISClientHelper.ZUOZHENLX == HISGlobalHelper.MenZhenZZLX.ZuoZhenLX_ZhuanJia)
                    {
                        barStaticItemZhuanJia.Caption = @"(专家)";
                    }
                    else if (HISClientHelper.ZUOZHENLX == HISGlobalHelper.MenZhenZZLX.ZuoZhenLX_Putong)
                    {
                        barStaticItemZhuanJia.Caption = @"(普通)";
                    }
                    else
                    {
                        barStaticItemZhuanJia.Caption = "";
                    }
                }
            }
            else if (HISClientHelper.XITONGID.Equals("10") || HISClientHelper.XITONGID.Equals("35"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANKSMC))
                {
                    userNamebsi.Caption = HISClientHelper.DANGQIANKSMC;
                }
            }
            else if (HISClientHelper.XITONGID.Equals("12") || HISClientHelper.XITONGID.Equals("34"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANBQMC))
                {
                    userNamebsi.Caption = HISClientHelper.DANGQIANBQMC;
                }
            }
            else if (HISClientHelper.XITONGID.Equals("18") || HISClientHelper.XITONGID.Equals("26"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANBQMC))
                {
                    userNamebsi.Caption = HISClientHelper.DANGQIANBQMC;
                }
            }
            else
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.KESHIMC))
                {
                    userNamebsi.Caption = HISClientHelper.XITONGID.Equals("36") ? null : HISClientHelper.KESHIMC;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CaiDanInfomation()
        {
            // 获取当前用户权限数据
            YongHuQXList = GYQuanXianHelper.GetQuanXian();

            BaseFormCommonHelper.Assemblys = Assemblys;
            BaseFormCommonHelper.CaiDanList = CanDanList;
            BaseFormCommonHelper.YongHuQXList = YongHuQXList;
            BaseFormCommonHelper.LinChuangMainForm = this;
            OpenRootGongNeng();
        }

        /// <summary>
        /// 关闭所有打开窗体
        /// </summary>
        public void CloseAllOpenWindows()
        {
            firstMainTab.ToList().ForEach(o =>
            {
                if (o.Value is MediForm form)
                {
                    form.Close();
                }
            });
            openwindowsdic.ToList().ForEach(o =>
            {
                o.Value?.Close();
            });
            kuangjiadic.ToList().ForEach(o =>
            {
                o.Value?.Close();
            });
            firstMainTab.Clear();
            openwindowsdic.Clear();
            kuangjiadic.Clear();
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FireMainEvent(object sender, MainFormOpenEventArgs e)
        {
            if (ChangeKeShiEevent != null && e.ActionStateType == ActionType.ChangeKeShi)
                ChangeKeShiEevent(sender, e);
            if (LockScreenEevent != null && e.ActionStateType == ActionType.LockScreen)
                LockScreenEevent(sender, e);
            if (CaoZuoSCEevent != null && e.ActionStateType == ActionType.CaoZuoShouCe)
                CaoZuoSCEevent(sender, e);
        }

        /// <summary>
        /// 打开菜单窗口
        /// </summary>
        public override void OpenCaiDanForm()
        {

        }

        /// <summary>
        /// 已打开的窗口集合
        /// </summary>
        public Dictionary<OpenCKInfo, MediForm> openwindowsdic = new Dictionary<OpenCKInfo, MediForm>();
        /// <summary>
        /// 已打开的病人框架
        /// </summary>
        public Dictionary<string, W_BINGRENKJ_BASE> kuangjiadic = new Dictionary<string, W_BINGRENKJ_BASE>();

        /// <summary>
        /// 应用名称
        /// </summary>
        [DefaultValue("应用名称"), Browsable(true)]
        public string YingYongMC { get; set; }

        /// <summary>
        /// 当前选中的主菜单ID
        /// </summary>
        public string SelectMainMenuId { get; set; }

        /// <summary>
        /// 加载默认的菜单及工具栏
        /// </summary>
        public virtual void LoadDefaultMenuAndToolBar()
        {
            if (CanDanList == null || CanDanList.Count < 1)
                return;
            int caidanLength = 0;
            if (CanDanList.Count > 0)
            {
                caidanLength = CanDanList.Count - 1;
            }
            if (caidanLength < 1) return;
            for (int i = caidanLength; i >= 0; i--)
            {
                string shangjicdid = CanDanList[i].SHANGJICDID;
                if (shangjicdid == "-" || shangjicdid == null)
                {
                    shangjicdid = "";
                }
                if (shangjicdid.Length > 0)
                {
                    string gongnengid = CanDanList[i].GONGNENGID;
                    if (YongHuQXList != null)
                    {
                        //判断功能权限
                        var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                        if (yonghuqx == null)
                        {
                            CanDanList[i].Delete();
                        }
                    }
                }
            }

            //一级菜单
            CanDanList.Where(o => o.SHANGJICDID == "-").OrderBy(o => o.SHUNXUHAO).ToList().ForEach(o =>
            {
                BarButtonItem barButtonItem = new BarButtonItem();
                BarItem item;

                var count = CanDanList.Count(r => r.SHANGJICDID == o.CAIDANID);
                if (count > 0)
                {
                    item = new BarSubItem();
                }
                else
                {
                    item = new BarButtonItem();
                    item.ItemClick += new ItemClickEventHandler(caidanlan_ItemClick);
                }
                barButtonItem.Name = o.CAIDANID;
                barButtonItem.Caption = o.CAIDANMC;
                barButtonItem.ItemAppearance.Hovered.Options.UseBackColor = true;
                barButtonItem.ItemAppearance.Hovered.Options.UseForeColor = true;
                barButtonItem.ItemAppearance.Hovered.BackColor = Color.FromArgb(180, 215, 245);
                barButtonItem.ItemAppearance.Pressed.Options.UseForeColor = true;
                barButtonItem.ItemAppearance.Pressed.Options.UseBackColor = true;
                barButtonItem.ItemAppearance.Pressed.BackColor = Color.FromArgb(5, 145, 206);
                barButtonItem.ItemAppearance.Pressed.ForeColor = Color.White;
            });

        }


        /// <summary>
        /// 用于业务窗口选择科室后刷新主窗体的状态栏显示
        /// </summary>
        protected void SetKeShiMc()
        {
            if (HISClientHelper.XITONGID.Equals("04"))
            {
                //Modify by SunChao on 2019.12.03 for [HB6-5545]
                userNamebsi.Caption = HISClientHelper.JIUZHENKSMC;
                if (HISClientHelper.ZUOZHENLX == HISGlobalHelper.MenZhenZZLX.ZuoZhenLX_ZhuanJia)
                    barStaticItemZhuanJia.Caption = @"(专家)";
                else if (HISClientHelper.ZUOZHENLX == HISGlobalHelper.MenZhenZZLX.ZuoZhenLX_Putong)
                    barStaticItemZhuanJia.Caption = @"(普通)";
                else
                    barStaticItemZhuanJia.Caption = "";
            }
            else if (HISClientHelper.XITONGID.Equals("10") || HISClientHelper.XITONGID.Equals("35"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANKSMC))
                    userNamebsi.Caption = HISClientHelper.DANGQIANKSMC;
            }
            else if (HISClientHelper.XITONGID.Equals("12") || HISClientHelper.XITONGID.Equals("34"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANBQMC))
                    userNamebsi.Caption = HISClientHelper.DANGQIANBQMC;
            }
            else if (HISClientHelper.XITONGID.Equals("18"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANBQMC))
                    userNamebsi.Caption = HISClientHelper.DANGQIANBQMC;
            }
            else if (HISClientHelper.XITONGID.Equals("26"))
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.DANGQIANBQMC))
                    userNamebsi.Caption = HISClientHelper.DANGQIANBQMC;
            }
            else
            {
                if (!userNamebsi.Caption.Equals(HISClientHelper.KESHIMC))
                    userNamebsi.Caption = HISClientHelper.XITONGID.Equals("36") ? null : HISClientHelper.KESHIMC;
            }
        }

        public override void LoadStatusBar()
        {
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("微软雅黑", 11);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = new Font("微软雅黑", 11);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = new Font("微软雅黑", 11);
            if (HISClientHelper.XITONGID == "18" || HISClientHelper.XITONGID == "26")
            {
                userNamebsi.Caption = HISClientHelper.DANGQIANKSMC ;
            }
            else if (HISClientHelper.XITONGID == "04")
            {
                userNamebsi.Caption = HISClientHelper.JIUZHENKSMC + "/" + HISClientHelper.USERNAME;
            }
            
            //yingYongMCbsi.Caption = HISClientHelper.YINGYONGMC;

            ipInfobsi.Caption = "本机 " + HISClientHelper.IP;
            barStaticHosName.Caption = HISClientHelper.YUANQUMC;
            barStaticItemKongBai1.Size = new Size(50, 20);//占位符100
            barStaticItemKongBai2.Size = new Size(50, 20);
            barStaticItemKongBai3.Size = new Size(50, 20);
            barStaticItemKongBai4.Size = new Size(50, 20);
            if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode").Trim().Equals("Standalone"))
            {
                barStatic_RunningMode.Caption = "单机模式";
            }
            else if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode").Trim().Equals("Cluster"))
            {
                barStatic_RunningMode.Caption = "集群模式";
            }
            barStatic_ServerUrl.Caption = MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Substring(0, MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Length > 0 ? MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").IndexOf(':') : 0);
            // 本地客户端版本号
            barStatic_Version.Caption = "版本号:" + MediinfoConfig.Read(Application.StartupPath + "\\version.ini", "version", "version");
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenRootGongNeng()
        {
            List<E_GY_CAIDAN_NEW> canDanList = CanDanList.Where(o => o.SHANGJICDID.Equals("-") && o.ISOPEN.Equals(1)).OrderBy(o => o.SHUNXUHAO).ToList();

            canDanList.ForEach(o =>
            {
                if (string.IsNullOrEmpty(o.CAIDANID))
                {
                    MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单ID【" + o.CAIDANID + "】");
                    return;
                }
                this.SuspendLayout();
                CreateForm(o, null);
                this.ResumeLayout(false);
            });
        }

        private void ChuangJianCD(LinksInfo items, List<E_GY_CAIDAN_NEW> caidanList)
        {
            foreach (LinkPersistInfo o in items)
            {
                bool breaksplit = false;
                caidanList.Where(p => p.SHANGJICDID == o.Item.Name).OrderBy(p => int.Parse(p.SHUNXUHAO)).GroupBy(p => p.CAIDANID).ToList().ForEach(q =>
                {
                    var caidan = q.FirstOrDefault();
                    if (caidan != null && caidan.CAIDANMC == "-")
                    {
                        breaksplit = true;
                    }
                    else
                    {
                        BarItem tsmi;
                        var count = caidanList.Count(r => r.SHANGJICDID == caidan.CAIDANID);
                        if (count > 0)
                        {
                            tsmi = new BarSubItem();
                        }
                        else
                        {
                            tsmi = new BarButtonItem();
                            tsmi.ItemClick += caidanlan_ItemClick;
                        }
                        tsmi.Name = caidan.CAIDANID;
                        tsmi.Caption = caidan.CAIDANMC;
                        tsmi.Enabled = caidan.QIYONGBZ == 1 ? false : true;
                        tsmi.ItemInMenuAppearance.Hovered.Options.UseBackColor = true;
                        tsmi.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(10, 163, 230);
                        tsmi.ItemInMenuAppearance.Hovered.Options.UseForeColor = true;
                        tsmi.ItemInMenuAppearance.Hovered.ForeColor = Color.White;

                        BarItem barManag = o.Item;

                        if (breaksplit)
                        {
                            (o.Item as BarSubItem)?.LinksPersistInfo.Add(new LinkPersistInfo(tsmi, true));
                            breaksplit = false;
                        }
                        else
                        {
                            (o.Item as BarSubItem)?.LinksPersistInfo.Add(new LinkPersistInfo(tsmi, false));
                        }
                    }
                });
                if (o.Item is BarSubItem item && item.LinksPersistInfo.Count > 0)
                {
                    ChuangJianCD(item?.LinksPersistInfo, caidanList);
                }
            }
        }

        private void caidanlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var denglucd = CanDanList.Where(o => o.CAIDANID == e.Item.Name).FirstOrDefault();
            if (denglucd == null && string.IsNullOrEmpty(e.Item.Name))
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单ID【" + e.Item.Name + "】");
                return;
            }
        }

        /// <summary>
        /// 创建窗体对象
        /// </summary>
        /// <param name="denglucd"></param>
        /// <param name="gongnengcss"></param>
        /// <param name="fomkeys"></param>
        protected override void CreateForm(E_GY_CAIDAN_NEW denglucd, List<object> gongnengcss, params object[] fomkeys)
        {
            if (denglucd != null)
            {
                if (CreateFormEX(denglucd, gongnengcss, fomkeys))
                    return;
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));
                OpenCKInfo openCkInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, chuangkoumc = formname.ToUpper() };
                if (openwindowsdic.Count > 0 && openwindowsdic.ContainsKey(openCkInfo))
                {
                    openwindowsdic[openCkInfo].Show();
                    return;
                }
                XtraTabPage firstLevelTabPage = new XtraTabPage();
                MediPanelControl mediPanelControl = new MediPanelControl();
                mediPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                firstLevelTabPage.Name = String.Format("{0}-{1}", denglucd.CAIDANID, denglucd.CAIDANMC);
                firstLevelTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
                firstLevelTabPage.Text = denglucd.CAIDANMC;
                mediPanelControl.Dock = DockStyle.Fill;
                firstLevelTabPage.Controls.Add(mediPanelControl);

                if (denglucd.DIAOYONGCS != null)
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    if (!Assemblys[path].ContainsKey(formname.ToUpper()))
                    {
                        MediMsgBox.Info("Dll中未找到包含" + formname.ToUpper() + "名称的窗体!");
                        return;
                    }

                    try
                    {
                        dynamic form = Assemblys[path][formname.ToUpper()].Value
                            .CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                        //取窗体的名称，用于在showdialog时制定owner者
                        HISClientHelper.ower = new WeakReference(form);

                        if (form != null)
                        {
                            form.TopLevel = false;
                            form.Dock = DockStyle.Fill;
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.Parent = mediPanelControl;
                            form.CaiDanList = CanDanList;
                            form.YongHuQXList = YongHuQXList;
                            form.currentCaiDanId = denglucd.CAIDANID;
                            form.InitialCaiDanData();

                            if (!firstMainTab.ContainsKey(form.Name))
                            {
                                firstMainTab.Add(form.Name, form);
                            }
                            else
                            {
                                firstMainTab[form.Name] = form;
                            }

                            openwindowsdic.Add(
                                new OpenCKInfo()
                                {
                                    openWindowMode = OpenWindowMode.Menu,
                                    xitongid = HISClientHelper.XITONGID,
                                    gongnengId = denglucd.GONGNENGID,
                                    caidanId = denglucd.CAIDANID,
                                    chuangkoumc = formname.ToUpper()
                                }, form);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            throw new Exception(ex.InnerException.ToString());
                        }
                        throw new Exception(ex.ToString());
                    }

                }
                //Modify by niquan 2019/6/18 请不要调整下面代码的顺序
                mediLCMainFormTabControl.TabPages.AddRange(new[] { firstLevelTabPage });
                mediLCMainFormTabControl.SelectedTabPage = firstLevelTabPage;
            }
        }

        W_BINGRENKJ_BASE kuangJiaBase = new W_BINGRENKJ_BASE();

        /// <summary>
        /// 通过委托打开相关业务tab页
        /// </summary>
        /// <param name="kuangjiainstance">相关病人信息</param>
        public bool OpenTabForm(W_BINGRENKJ_BASE kuangjiainstance, bool isChaKanBL = false)
        {
            if (!kuangjiainstance.IntialAllBinXX(kuangjiainstance.BingRenID, kuangjiainstance.BingRenZYID, kuangjiainstance.JiuZhenID))
                return false;

            kuangjiainstance.IsChaKanMZBL = isChaKanBL;

            kuangjiainstance.FormClosed -= Kuangjiainstance_FormClosed;
            kuangjiainstance.FormClosed += Kuangjiainstance_FormClosed;

            if (kuangjiainstance.BingRenXXKJ.BingRenXX != null)
            {
                if (kuangjiadic.ContainsKey(kuangjiainstance.BingRenID))
                {
                    List<XtraTabPage> xtraTabPages = this.mediLCMainFormTabControl.TabPages.Where(o => o.Name.Equals(kuangjiainstance.BingRenID)).ToList();
                    if (xtraTabPages.Count > 0)
                    {
                        this.mediLCMainFormTabControl.SelectedTabPage = xtraTabPages[0];
                        RightClickForm(kuangjiainstance);
                    }
                    return true;

                }
                MediPanelControl mediPanelControl = new MediPanelControl
                {
                    BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                    BackColor = Color.FromArgb(243, 243, 243),
                    Dock = DockStyle.Fill
                };
                //显示床位
                string chuangWeiMC = "";
                if (!string.IsNullOrWhiteSpace(kuangjiainstance.ChuangWeiID))
                {
                    var chuangWeiXS = GYCanShuHelper.GetCanShu("住院_病人框架TAB页标签是否显示床位", "");
                    if (!string.IsNullOrWhiteSpace(chuangWeiXS))
                    {
                        foreach (var item in chuangWeiXS.Split('|'))
                        {
                            if (!string.IsNullOrWhiteSpace(item) && item.Split(':') is string[] xsArr && xsArr.Length == 2)
                            {
                                string xtID = xsArr[0];//系统ID
                                string xianShi = xsArr[1];//是否显示床位
                                if (xtID.Equals(XiTongID) && xianShi.Equals("1"))
                                {
                                    chuangWeiMC = $"[{kuangjiainstance.ChuangWeiID}床]";
                                    break;
                                }
                            }
                        }
                    }
                }

                XtraTabPage xtraTabPage = new XtraTabPage
                {
                    BackColor = Color.FromArgb(243, 243, 243),
                    ShowCloseButton = DevExpress.Utils.DefaultBoolean.True,
                    Name = kuangjiainstance.BingRenID,
                    Text = chuangWeiMC + kuangjiainstance.BingRenXXKJ.BingRenXX.XINGMING
                };
                xtraTabPage.Controls.Add(mediPanelControl);
                if (this.NewPageIndex > -1)
                {
                    //在指定位置打开患者界面
                    this.mediLCMainFormTabControl.TabPages.Insert(this.NewPageIndex, xtraTabPage);
                }
                else
                {
                    this.mediLCMainFormTabControl.TabPages.Add(xtraTabPage);
                }
                xtraTabPage.Name = kuangjiainstance.BingRenID;
                kuangjiainstance.CaiDanList = CanDanList;
                kuangjiainstance.YongHuQXList = YongHuQXList;
                kuangjiainstance.Assemblys = Assemblys;
                kuangjiainstance.openwindowsdic = openwindowsdic;
                kuangjiainstance.CurrentMainCaiDanID = SelectMainMenuId;
                kuangjiainstance.IsMouseInCloseButton = MouseInCloseButton;
                kuangjiainstance.TopLevel = false;
                kuangjiainstance.Dock = DockStyle.Fill;
                kuangjiainstance.FormBorderStyle = FormBorderStyle.None;
                kuangjiainstance.Parent = mediPanelControl;

                if (kuangjiainstance.MuBiaoCKInfo != null)
                {
                    var denglucd = CanDanList.Where(o => o.DIAOYONGCS.Contains(kuangjiainstance.MuBiaoCKInfo.GongNengCKMC?.TrimStart('I') + "|" + kuangjiainstance.MuBiaoCKInfo.GongNengID)).FirstOrDefault();
                    if (denglucd != null && string.IsNullOrEmpty(kuangjiainstance.TabPageName))
                        kuangjiainstance.TabPageName = denglucd.CAIDANMC;
                }

                kuangjiainstance.Show();
                this.mediLCMainFormTabControl.SelectedTabPage = xtraTabPage;
                kuangjiadic.Add(kuangjiainstance.BingRenID, kuangjiainstance);
            }
            else
            {
                MediMsgBox.Warn(this, "获取病人门诊信息失败或接诊失败，请联系管理员!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 右键重打开Tab页处理
        /// </summary>
        private void RightClickForm(W_BINGRENKJ_BASE kuangjiainstance)
        {
            #region 重打开定位Tab窗口

            if (kuangjiainstance.MuBiaoCKInfo == null)
                return;

            var denglucd = CanDanList.FirstOrDefault(o => o.DIAOYONGCS.Contains(kuangjiainstance.MuBiaoCKInfo.GongNengCKMC.TrimStart('I') + "|" + kuangjiainstance.MuBiaoCKInfo.GongNengID));
            if (denglucd == null && string.IsNullOrEmpty(kuangjiainstance.MuBiaoCKInfo.GongNengCKMC))
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的窗口名称【" + kuangjiainstance.MuBiaoCKInfo.GongNengCKMC + "】");
            }

            if (denglucd != null)
            {
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));
                var openCkInfo = denglucd.GONGNENGID.StartsWith("gn")
                    ? new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Button,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = denglucd.GONGNENGID,
                        caidanId = denglucd.CAIDANID,
                        binrenid = kuangjiainstance.BingRenID,
                        chuangkoumc = formname.ToUpper()
                    }
                    : new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Menu,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = denglucd.GONGNENGID,
                        caidanId = denglucd.CAIDANID,
                        binrenid = kuangjiainstance.BingRenID,
                        chuangkoumc = formname.ToUpper()
                    };
                this.SuspendLayout();
                if (!openwindowsdic.ContainsKey(openCkInfo))
                {
                    MediTabControl mediTabControl = BaseFormCommonHelper.mediBRTabControl.FirstOrDefault(p => p.Target != null)?.Target;
                    BaseFormCommonHelper.CreateForm(kuangjiainstance, mediTabControl, denglucd, ref openwindowsdic, false);

                    BaseFormCommonHelper.openwindowsdic = openwindowsdic;
                    if (openwindowsdic.ContainsKey(openCkInfo))
                    {
                        if (!kuangjiainstance.openedlcdic.ContainsKey(openCkInfo.chuangkoumc))
                        {
                            if (openwindowsdic[openCkInfo] is W_LINCHUANG_BASE lingchuangbase)
                            {
                                lingchuangbase.LinChuangFormClosed += Lingchuangbase_LinChuangFormClosed;
                                kuangjiainstance.openedlcdic.Add(openCkInfo.chuangkoumc, lingchuangbase);
                            }
                        }
                    }
                    kuangJiaBase = kuangjiainstance;
                }
                else
                {
                    if (denglucd.DIAOYONGCS.Contains("NO"))
                        denglucd.DIAOYONGCS = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.Length - 3) + "|YES";

                    if (string.IsNullOrEmpty(kuangjiainstance.TabPageName))
                        kuangjiainstance.TabPageName = denglucd.CAIDANMC;
                    //通过病人ID获取对应患者界面所在的tabcontrol控件    [HB6-14006]   add by 余佳平
                    var tabControl = BaseFormCommonHelper.mediBRTabControl.First(p => p.Target != null && p.Target.Tag.ToStringEx() == kuangjiainstance.BingRenID).Target;
                    BaseFormCommonHelper.CreateForm(kuangjiainstance, tabControl, denglucd, ref BaseFormCommonHelper.openwindowsdic, false);

                    if (kuangjiainstance.openedlcdic.ContainsKey(openCkInfo.chuangkoumc) && kuangjiainstance.openedlcdic[openCkInfo.chuangkoumc] == null)
                    {
                        if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCkInfo))
                        {
                            if (BaseFormCommonHelper.openwindowsdic[openCkInfo] is W_LINCHUANG_BASE linchuangBase)
                            {
                                kuangjiainstance.openedlcdic[openCkInfo.chuangkoumc] = linchuangBase;
                            }
                        }
                    }
                }
            }

            this.ResumeLayout(false);

            #endregion
        }

        /// <summary>
        /// 选择tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            foreach (Control control in e.Page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is MediFormLCWithQX tempForm)
                    {
                        SelectMainMenuId = tempForm.currentCaiDanId;
                    }

                    if (item is W_BINGRENKJ_BASE p)
                    {
                        SelectMainMenuId = p.CurrentMainCaiDanID;
                        p.SelectPageChanged(item);
                        DataMiddleWare.SelectPatientPageChanged(p.BingRenID);
                        //切换后记录患者界面所选中菜单的功能ID
                        if (BaseFormCommonHelper.OpenedCaiDanDic.ContainsKey(p.BingRenID))
                            HISClientHelper.SELECTEDGONGNENGID = BaseFormCommonHelper.OpenedCaiDanDic[p.BingRenID].GONGNENGID;
                        //CDSS病人初始化
                         p.InitCDSSByBingRenXX();
                    }

                    //添加切换病人时保存病人框架中的信息
                    HISClientHelper.KuangJiaBase = item as Form;
                }
            }
        }

        /// <summary>
        /// 关闭tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (e is ClosePageButtonEventArgs closePageButtonEventArgs)
            {
                XtraTabPage xtraTabPage = closePageButtonEventArgs.Page as XtraTabPage;
                CloseTabPage(xtraTabPage);
            }
        }

        /// <summary>
        /// 关闭tab页
        /// </summary>
        /// <param name="xtraTabPage"></param>
        public void CloseTabPage(XtraTabPage xtraTabPage)
        {
            BaseFormCommonHelper.CloseForm = true;
            string bingRenID = "";//关闭tab页所在病人的ID
            int closePageIndex = -1;//关闭tab页所在位置索引
            foreach (Control control in xtraTabPage.Controls)
            {
                foreach (Control form in control.Controls)
                {
                    if (form is W_BINGRENKJ_BASE p)
                    {
                        bingRenID = p.BingRenID;
                        var closePage = this.mediLCMainFormTabControl.TabPages.FirstOrDefault(o => o.Name.Equals(bingRenID));
                        closePageIndex = this.mediLCMainFormTabControl.TabPages.IndexOf(closePage);

                        p.RemovePage(form);
                        p.ValidateDataModifiedBeforeClose();//界面关闭前校验数据是否更改
                                                            // DataMiddleWare.RemovePatientPage(p.BingRenID);
                        p.Close();
                    }

                    if (form.IsDisposed)
                    {
                        //关闭
                        xtraTabPage.PageVisible = false;

                        DataMiddleWare.RemovePatientPage(xtraTabPage.Name);

                        if (this.mediLCMainFormTabControl.TabPages.Contains(xtraTabPage))
                            this.mediLCMainFormTabControl.TabPages.Remove(xtraTabPage);
                        if (kuangjiadic.ContainsKey(xtraTabPage.Name))
                            kuangjiadic.Remove(xtraTabPage.Name);

                        //关闭tab页为当前选中页时，切换tab页，否则无需切换
                        if (closePageIndex == this.mediLCMainFormTabControl.SelectedTabPageIndex)
                        {
                            if (this.NewPageIndex > -1)
                            {
                                this.mediLCMainFormTabControl.SelectedTabPageIndex = this.NewPageIndex;
                            }
                            else
                            {
                                this.mediLCMainFormTabControl.SelectedTabPageIndex = this.mediLCMainFormTabControl.SelectedTabPageIndex - 1;
                            }
                        }

                        //关闭病人后移除该病人对应的项
                        BaseFormCommonHelper.OpenedCaiDanDic.Remove(bingRenID);
                    }
                    else
                    {
                        //取消关闭
                        //设置可见并选中为当前显示页
                        xtraTabPage.PageVisible = true;
                        xtraTabPage.TabControl.SelectedTabPage = xtraTabPage;
                    }
                }
            }
            BaseFormCommonHelper.CloseForm = false;
        }

        #region fields

        /// <summary>
        /// 锁屏定时器
        /// </summary>
        private Timer lockScreenTimer;

        /// <summary>
        /// 自动检测更新定时器
        /// </summary>
        private Timer AutoUpdateTimer;

        #endregion

        #region extern

        /// <summary>
        /// 调用windows API获取鼠标键盘空闲时间
        /// </summary>
        /// <param name="lastInputInfo"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LastInputInfo lastInputInfo);

        #endregion

        #region properties

        /// <summary>
        /// 创建控件句柄时获取所需的创建参数
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x00020000;
                return cp;
            }
        }

        /// <summary>
        /// 允许打开最大住院病人数目
        /// </summary>
        public int MaxPatientCount
        {
            get
            {
                var xiTongId = HISClientHelper.YINGYONGID.SubString(0, 2);
                switch (xiTongId)
                {
                    case "04":
                        return GYCanShuHelper.GetCanShu("04", "诊间_允许打开最大接诊病人数目", "5").ToInt();
                    case "10":
                        return GYCanShuHelper.GetCanShu("10", "病区医生_允许打开最大住院病人数目", "5").ToInt();
                    case "12":
                        return GYCanShuHelper.GetCanShu("12", "病区护士_允许打开最大住院病人数目", "5").ToInt();
                    default:
                        return 5;
                }
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            // 注册锁屏事件
            this.LockScreenEevent -= MediLCMainForm_LockScreenEevent;
            this.LockScreenEevent += MediLCMainForm_LockScreenEevent;
        }

        /// <summary>
        /// 设置锁屏
        /// </summary>
        private void SetLockScreen()
        {
            // 获取自动锁屏时间，如果自动锁频时间为0或为空时不启动锁屏
            string lockScreenTime = GYCanShuHelper.GetCanShu("系统_自动锁定控制时间", "0");
            if (!String.IsNullOrWhiteSpace(lockScreenTime) && !"0".Equals(lockScreenTime))
            {
                int time = Convert.ToInt32(lockScreenTime);

                // 创建锁屏定时器，1分钟检查一次
                lockScreenTimer = new Timer
                {
                    Interval = 60000,
                    Tag = time
                };
                lockScreenTimer.Tick += new EventHandler(LockTimer_Tick);
                lockScreenTimer.Start();
            }
        }

        private void SetAutoUpdate()
        {
            //获取自动检测更新时间，如果时间为0或者为空不启动检测
            string AutoUpdateTime = GYCanShuHelper.GetCanShu("系统_自动检测更新时间", "0");//默认为0，不启用
            if (!String.IsNullOrWhiteSpace(AutoUpdateTime) && !"0".Equals(AutoUpdateTime))
            {
                int time = Convert.ToInt32(AutoUpdateTime) * 60 * 1000;//转化成毫秒
                AutoUpdateTimer = new Timer
                {
                    Interval = time
                };
                AutoUpdateTimer.Tick += new EventHandler(UpdateTimer_Tick);
                AutoUpdateTimer.Start();
            }
        }

        /// <summary>
        /// 获取鼠标键盘空闲时间
        /// </summary>
        /// <returns></returns>
        private long GetIdleTick()
        {
            LastInputInfo lastInputInfo = new LastInputInfo();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            if (!GetLastInputInfo(ref lastInputInfo))
                return 0;
            return Environment.TickCount - (long)lastInputInfo.dwTime;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            foreach (Control page in this.mediLCMainFormTabControl.TabPages)
            {
                foreach (Control ctr in page.Controls)
                {
                    foreach (Control form in ctr.Controls)
                    {
                        if (form is MediFormLCWithQX frm)
                        {
                            // 刷新已经打开的业务数据
                            frm.RefreshBusinessData();
                        }
                    }
                }
            }
        }

        #endregion

        #region events

        /// <summary>
        /// 窗口第一次加载事件，随后最小化，最大化，还原，隐藏，显示或无效化和重绘将不会引发此事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinChuangMainFormBase_Shown(object sender, EventArgs e)
        {
            // 设置锁屏
            SetLockScreen();

            //自动更新
            SetAutoUpdate();

            if (BeforeMainFormOpenEevent != null)
            {
                MainFormOpenEventArgs mainFormOpenEventArgs = new MainFormOpenEventArgs();

                BeforeMainFormOpenEevent(mainFormOpenEventArgs);
                if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Close)
                {
                    if (!this.IsDisposed)
                    {
                        this.Close();
                    }
                }
                else if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Open)
                {
                    foreach (var form in firstMainTab.Values)
                    {
                        form.Show();
                    }
                }
            }
        }

        /// <summary>
        /// 窗口数据加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinChuangMainFormBase_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            if (!SkinCat.Instance.IsDesignMode)
            {
                this.TextChanged -= this.LinChuangMainFormBase_TextChanged;
                this.TextChanged += this.LinChuangMainFormBase_TextChanged;
            }
        }

        /// <summary>
        /// 标题改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinChuangMainFormBase_TextChanged(object sender, EventArgs e)
        {
            #region 记录日志

            //记录日志=====================================================================
            //ESLog eSLog = new ESLog();
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]打开了[" + ((DevExpress.XtraEditors.XtraForm)sender).Text + "]界面。";
            logEntity.RiZhiNr = "[" + HISClientHelper.USERNAME + "]打开了[" + ((DevExpress.XtraEditors.XtraForm)sender).Text + "]界面。\r\n上一个界面是：" + HISClientHelper.DANGQIANCKMC;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = ((DevExpress.XtraEditors.XtraForm)sender).Text;
            //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
            logEntity.RiZhiLx = 1;
            logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
            logEntity.XITONGID = HISClientHelper.XITONGID;
            logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
            logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
            logEntity.VERSION = HISClientHelper.VERSION;
            logEntity.IP = HISClientHelper.IP;
            logEntity.MAC = HISClientHelper.MAC;
            logEntity.COMPUTERNAME = HISClientHelper.COMPUTERNAME;
            logEntity.USERNAME = HISClientHelper.USERNAME;
            logEntity.USERID = HISClientHelper.USERID;
            logEntity.KESHIID = HISClientHelper.KESHIID;
            logEntity.KESHIMC = HISClientHelper.KESHIMC;
            logEntity.BINGQUID = HISClientHelper.BINGQUID;
            logEntity.BINGQUMC = HISClientHelper.BINGQUMC;
            logEntity.JIUZHENKSID = HISClientHelper.JIUZHENKSID;
            logEntity.JIUZHENKSMC = HISClientHelper.JIUZHENKSMC;
            logEntity.YUANQUID = HISClientHelper.YUANQUID;
            logEntity.GONGZUOZID = HISClientHelper.GONGZUOZID;
            LogHelper.Intance.PutSysInfoLog(logEntity);

            //记录日志=====================================================================

            #endregion 记录日志

            HISClientHelper.DANGQIANCKMC = ((DevExpress.XtraEditors.XtraForm)sender).Text;
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinChuangMainFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClosingMainFormOpenEevent != null)
            {
                MainFormOpenEventArgs mainFormOpenEventArgs = new MainFormOpenEventArgs();
                ClosingMainFormOpenEevent(mainFormOpenEventArgs);
                if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Close)
                {
                    this.Close();
                }
                else if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Open)
                {
                    foreach (var form in firstMainTab.Values)
                    {
                        form.Show();
                    }
                }
            }

            if (this.IsDisposed)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 临床关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="lceventArgs"></param>
        private void Lingchuangbase_LinChuangFormClosed(object sender, LinChuangEventArgs lceventArgs)
        {
            if (sender is W_LINCHUANG_BASE linchuangbase)
            {
                if (kuangJiaBase.openedlcdic.ContainsKey(linchuangbase.Name.ToUpper()))
                {
                    kuangJiaBase.openedlcdic.Remove(linchuangbase.Name.ToUpper());
                }
            }

            // 窗口关闭时候释放锁屏定时器
            if (lockScreenTimer != null)
            {
                lockScreenTimer.Dispose();
            }
        }

        /// <summary>
        /// 病人框架关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kuangjiainstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is W_BINGRENKJ_BASE kuangjiaBase)
            {
                var binrenid = kuangjiaBase.BingRenID;
                if (kuangjiadic.ContainsKey(binrenid))
                {
                    kuangjiadic.Remove(binrenid);
                }
            }
        }

        /// <summary>
        /// 自动锁屏定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockTimer_Tick(object sender, EventArgs e)
        {
            // 获取自动锁屏时间
            if (sender is Timer tm)
            {
                object obj = tm.Tag;
                if (obj != null)
                {
                    // 将自动锁屏的时间由分钟转换成毫秒
                    long lockSeconds = Convert.ToInt32(obj) * 60 * 1000;

                    // 获取鼠标键盘空闲时间
                    long ticks = GetIdleTick();

                    // 如果鼠标键盘空闲时间大于等于锁屏时间那么启动锁屏
                    if (ticks >= lockSeconds)
                    {
                        // 锁屏时候关闭定时器
                        tm.Stop();

                        // 触发锁屏事件
                        MediLCMainForm_LockScreenEevent(tm, new MainFormOpenEventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// 锁屏事件
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void MediLCMainForm_LockScreenEevent(object arg1, MainFormOpenEventArgs arg2)
        {
            // 弹出锁屏窗口，锁屏窗口居于当前窗口上层
            using (W_GY_SUODINGPM frm = new W_GY_SUODINGPM())
            {
                IsLockScreen = true;

                frm.Unlock += new EventHandler(LinChuangMainFormBase_Unlock);
                frm.Tag = arg1;
                frm.ShowInTaskbar = false;
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 屏幕解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinChuangMainFormBase_Unlock(object sender, EventArgs e)
        {
            if (sender is W_GY_SUODINGPM frm)
            {
                if (frm.Tag is Timer tm)
                {
                    // 重新启动定时器
                    tm.Start();

                    // 解锁之后刷新数据
                    RefreshData();
                }
            }
        }


        /// <summary>
        /// 自动检测更新定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            //判断是否是http更新
            if (HISGlobalSetting.IsHttp)
            {
                HISGlobalSetting.zxt = HISClientHelper.XITONGID;
                var rootpath = AppDomain.CurrentDomain.BaseDirectory;
                bool needUpdate_zxt = CheckUpdate_ZXT(rootpath);//判断是否需要更新子系统 并询问是否更新
                if (needUpdate_zxt)
                {
                    if (MediMsgBox.YesNo("是否退出并更新程序?", MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        //ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        //processStartInfo.FileName = UpdateHelper.LoginFormName + ".exe";
                        //processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        //processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        //Process.Start(processStartInfo);
                        DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                        string rootPathInfo = startPathInfo.Parent.FullName; // 上一级的目录
                        ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        processStartInfo.FileName = "Mediinfo.WinForm.HIS.Starter.exe";
                        processStartInfo.WorkingDirectory = rootPathInfo;
                        processStartInfo.Arguments = $"AgainSystem  {HISClientHelper.USERID} {HISClientHelper.USERPWD} {HISClientHelper.YINGYONGID}";
                        processStartInfo.Verb = "runas";
                        processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        // 处理XP系统报错问题
                        if (Environment.OSVersion.Version.Major >= 6)
                        {

                            Process.Start(processStartInfo);
                        }
                        else
                            WinApiHelper.ShellExecute(0,
                                "open",
                                processStartInfo.FileName,
                                processStartInfo.Arguments,
                                processStartInfo.WorkingDirectory,
                                11);

                        //关闭现有子系统
                        CloseProcess();
                    }

                }
            }
            
        }
        /// <summary>
        /// 检测是否需要更新子系统
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CheckUpdate_ZXT(string rootpath)
        {
            //先判断目录是否存在
            if (!Directory.Exists(Path.Combine(rootpath))) return true;
            //判断文件是否存在
            string filePath = Path.Combine(rootpath, "Mediinfo.WinForm.HIS.Main.exe");
            if (File.Exists(filePath))
            {
                //判断配置文件是否存在
                if (!File.Exists(Path.Combine(rootpath, "HISGlobalSettingHttp.xml"))) return true;
                //读取本地配置文件
                List<HTTPUpdateConfig> httpconfigs = HISGlobalSetting.LoadZxtInfos(Path.Combine(rootpath, "HISGlobalSettingHttp.xml"));
                int count = 0;
                List<HTTPUpdateConfig> Needupdatedconfigs = new List<HTTPUpdateConfig>();
                if (httpconfigs != null && httpconfigs.Count != 0)
                {
                    DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    string httpAddress = startPathInfo.Parent.Parent.FullName + "\\DownLoadAddress.xml";
                    if (!File.Exists(httpAddress)) return false;
                    foreach (HTTPUpdateConfig item in httpconfigs)
                    {
                        string severUrl = MediinfoConfig.GetXmlNodeValue(httpAddress, "ipAddress");
                        string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC); //最新版本号
                        if (!string.IsNullOrEmpty(returnMessage))
                        {
                            if (returnMessage.Equals(item.BanBenHao))
                            {
                                // 版本号没有更新
                                count++;
                                continue;
                            }
                        }
                    }
                    if (count == httpconfigs.Count)
                    {
                        //不需要下载更新
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {  //不存在需要更新
                return true;
            }
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        private void CloseProcess()
        {
            HISClientHelper.BatRunCmd("appshutdown.bat", AppDomain.CurrentDomain.BaseDirectory, out var errorMsg);
            if (!string.IsNullOrWhiteSpace(errorMsg))
                throw new ApplicationException(errorMsg);
            List<string> yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();
            if (yingYongIdList != null)
            {
                if (yingYongIdList.Contains(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString()))
                {
                    MemoryMappedFileHelper.RemoveClipBoardData(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
                    yingYongIdList.Remove(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString());
                }
                if (yingYongIdList.Count < 1)
                    MemoryMappedFileHelper.ClearClipBoardData();
            }

            // 退出系统时调用
            ClosedProcess();

            this.Dispose();
            // 关闭进程
            DisposeCurrentProcess();
        }


        #endregion

        #region 更新消息数量
        /// <summary>
        /// 更新消息徽章
        /// </summary>
        /// <param name="isVisible"></param>
        /// <param name="numberStr"></param>
        public void UpdateXiaoXiBellCount(bool isVisible, string numberStr)
        {
            this.mediLCMainFormTabControl.UpdateBageControl(isVisible,numberStr);
        }
        #endregion
    }
}