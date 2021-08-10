using DevExpress.XtraBars;
using DevExpress.XtraTab;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.SM;
using Mediinfo.DTO.HIS.ZY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.Core;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls.FirstLevelFramework;
using Mediinfo.WinForm.HIS.Controls.TabForm;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls.SecondaryFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="formName"></param>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public delegate object RefreshUIData(string formName, object eventArgs);

    /// <summary>
    /// 
    /// </summary>
    public partial class W_BINGRENKJ_BASE : MediFormWithQX
    {
        /// <summary>
        /// 查询门诊病历
        /// </summary>
        public bool IsChaKanMZBL = false;
        /// <summary>
        /// 目标窗口信息
        /// </summary>
        internal MuBiaoFormInformation MuBiaoCKInfo { get; set; }
        /// <summary>
        /// 病人框架已打开的所有tab页(此窗体基类为W_LINCHUANG_BASE)
        /// </summary>
        public Dictionary<string, dynamic> openedlcdic = new Dictionary<string, dynamic>();
        /// <summary>
        /// 
        /// </summary>
        public LinChuangMainFormBase linChuangMainForm;
        /// <summary>
        /// 
        /// </summary>
        public Action<object, LinChuangEventArgs> ReceiveDataFromLinChuangDelegate;
        /// <summary>
        /// 
        /// </summary>
        public event Action<object, LinChuangEventArgs> ShuaXinShuJuFromLinChuangEvent;

        /// <summary>
        /// 已打开的窗口集合
        /// </summary>
        public Dictionary<OpenCKInfo, MediForm> openwindowsdic;

        /// <summary>
        /// 创建窗体对象（挂接窗体）
        /// </summary>
        public LinChuangTabForm linChuangTabForm { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public W_BINGRENKJ_BASE()
        {
            InitializeComponent();
            ReceiveDataFromLinChuangDelegate = ReceiveDataFromLinChuang;
            InitialControls();
            RegesterEvents();
            IntialBingRenGYCache();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegesterEvents()
        {
            this.Load -= W_BINGRENKJ_BASE_Load;
            this.Load += W_BINGRENKJ_BASE_Load;
            this.FormClosing -= W_BINGRENKJ_BASE_FormClosing;
            this.FormClosing += W_BINGRENKJ_BASE_FormClosing;
            this.FormClosed -= W_BINGRENKJ_BASE_FormClosed;
            this.FormClosed += W_BINGRENKJ_BASE_FormClosed;
        }

        private void W_BINGRENKJ_BASE_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel)
                return;

        }

        private void W_BINGRENKJ_BASE_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataMiddleWare.RemovePatientPage(this.BingRenID);
            if (this.linChuangMainForm.NewPageIndex < 0)
            {
                //关闭tab页为当前选中页时，切换tab页，否则无需切换
                var closePage = this.linChuangMainForm.mediLCMainFormTabControl.TabPages.FirstOrDefault(o => o.Name.Equals(this.BingRenID));
                var closePageIndex = this.linChuangMainForm.mediLCMainFormTabControl.TabPages.IndexOf(closePage);
                if (closePageIndex == this.linChuangMainForm.mediLCMainFormTabControl.SelectedTabPageIndex)
                {
                    this.linChuangMainForm.mediLCMainFormTabControl.SelectedTabPageIndex = this.linChuangMainForm.mediLCMainFormTabControl.SelectedTabPageIndex - 1;
                }
            }
            HashSet<MediForm> formDisposeList = new HashSet<MediForm>();  // 用于存放所有的待销毁窗口 

            if (linChuangTabForm.mediBRTabControl.TabPages.Count < 1)
            {
                this.linChuangMainForm.mediLCMainFormTabControl.TabPages.Where(o => o.Name.Equals(this.BingRenID)).ToList().ForEach(o =>
                {
                    this.linChuangMainForm.mediLCMainFormTabControl.TabPages.Remove(o, true);
                });
                return;
            }

            OpenCKInfo openCkInfo = new OpenCKInfo();
            linChuangTabForm.mediBRTabControl.TabPages.ToList<XtraTabPage>().ForEach(o =>
            {
                if (o.Controls.Count > 0)
                {
                    foreach (Control ctr in o.Controls)
                    {
                        foreach (Control ctr1 in ctr.Controls)
                        {
                            if (ctr1 is W_LINCHUANG_BASE linchuangbase)
                            {
                                // 这个name可能是带有参数，所以改成Contains
                                if (linchuangbase.Name.ToUpper().Contains(o.Name.ToUpper()))
                                {
                                    var formname = linchuangbase.denglucd.DIAOYONGCS.Substring(0, linchuangbase.denglucd.DIAOYONGCS.IndexOf('|') == -1 ? linchuangbase.denglucd.DIAOYONGCS.Length : linchuangbase.denglucd.DIAOYONGCS.IndexOf('|'));

                                    // 添加判断，分为button和menu两种打开方式，来进行删除openwindowsdic打开的字典
                                    openCkInfo = linchuangbase.denglucd.GONGNENGID.StartsWith("gn") ? new OpenCKInfo() { openWindowMode = OpenWindowMode.Button, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = linchuangbase.denglucd.GONGNENGID, caidanId = linchuangbase.denglucd.CAIDANID, binrenid = BingRenID, chuangkoumc = formname.ToUpper() } : new OpenCKInfo { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = linchuangbase.denglucd.GONGNENGID, caidanId = linchuangbase.denglucd.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = BingRenID };

                                    if (openwindowsdic.ContainsKey(openCkInfo))     // 所有的窗口均需要关闭,并回收资源
                                    {
                                        formDisposeList.Add(openwindowsdic[openCkInfo]);    // 放入待销毁列表
                                        openwindowsdic.Remove(openCkInfo);
                                    }

                                    if (openedlcdic.ContainsKey(o.Name.ToUpper()))
                                    {
                                        formDisposeList.Add(openedlcdic[o.Name.ToUpper()]);
                                        openedlcdic.Remove(o.Name.ToUpper());
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (openwindowsdic.ContainsKey(openCkInfo))
                    {
                        formDisposeList.Add(openwindowsdic[openCkInfo]);    // 放入待销毁列表
                        openwindowsdic.Remove(openCkInfo);
                    }
                    if (openedlcdic.ContainsKey(o.Name.ToUpper()))
                    {
                        formDisposeList.Add(openedlcdic[o.Name.ToUpper()]);
                        openedlcdic.Remove(o.Name.ToUpper());
                    }
                }
            });

            this.linChuangMainForm.mediLCMainFormTabControl.TabPages.Where(o => o.Name.Equals(this.BingRenID)).ToList().ForEach(o =>
            {
                this.linChuangMainForm.mediLCMainFormTabControl.TabPages.Remove(o, true);
            });
            BaseFormCommonHelper.openedTabFormDic.Clear();

            // 轮询后销毁所有窗口
            foreach (MediForm a in formDisposeList)
            {
                if (a == null) continue;

                a.Close();
                a.Dispose();
            }
        }

        /// <summary>
        /// 移除事件，供子类调用清楚本窗口事件
        /// </summary>
        protected virtual void RemoveEvent(W_LINCHUANG_BASE lingchuangbase)
        {
            lingchuangbase.LinChuangFormClosed -= Lingchuangbase_LinChuangFormClosed;
        }

        /// <summary>
        /// 移除事件，供子类调用清楚本窗口事件
        /// 强句柄会使对象处于活动状态，而无法被GC清理
        /// </summary>
        protected virtual void RemoveStrongEvent(W_LINCHUANG_BASE lingchuangbase)
        {
            lingchuangbase.LinChuangFormClosed -= Lingchuangbase_LinChuangFormClosed;
        }

        private void W_BINGRENKJ_BASE_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BaseFormCommonHelper.openwindowsdic = openwindowsdic;
                linChuangTabForm.kuangJiaBaseForm = this;
                //添加病人ID标识，用于右键打开指定窗口时查找对应患者界面所在的tabcontrol控件   [HB6-14006]   add by 余佳平
                linChuangTabForm.mediBRTabControl.Tag = this.BingRenID;
                //门诊医生站打开病区医生
                if (HISClientHelper.YINGYONGID == "0401" && this.Text == "W_YS_KUANGJIA")
                {
                    BaseFormCommonHelper.LoadDefaultOpenForm(this, CurrentMainCaiDanID, linChuangTabForm.mediBRTabControl, openedlcdic, "1001");
                }
                else if (IsChaKanMZBL)
                {
                    BaseFormCommonHelper.LoadDefaultOpenForm(this, CurrentMainCaiDanID, linChuangTabForm.mediBRTabControl, openedlcdic, true);
                }
                else
                {
                    BaseFormCommonHelper.LoadDefaultOpenForm(this, CurrentMainCaiDanID, linChuangTabForm.mediBRTabControl, openedlcdic);
                }
                LoadDefaultMenuAndToolBar();
            }
        }

        /// <summary>
        /// 接受临床返回的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="linChuangEventArgs"></param>
        public virtual void ReceiveDataFromLinChuang(object sender, LinChuangEventArgs linChuangEventArgs)
        {
            ShuaXinShuJuFromLinChuangEvent?.Invoke(sender, linChuangEventArgs);
        }

        /// <summary>
        /// 关闭所有选项卡
        /// </summary>
        public void CloseAllTabPages()
        {
            for (int i = linChuangTabForm.mediBRTabControl.TabPages.Count - 1; i >= 0; i--)
            {
                XtraTabPage page = linChuangTabForm.mediBRTabControl.TabPages[i];
                CloseTabPage(page);
            }
        }

        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="page">选项卡</param>
        public void CloseTabPage(XtraTabPage page)
        {
            foreach (Control control in page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE tempForm1)
                    {
                        OpenCKInfo openCkInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = tempForm1.denglucd.GONGNENGID, caidanId = tempForm1.denglucd.CAIDANID, chuangkoumc = tempForm1.Name.ToUpper(), binrenid = tempForm1.bingRenId };
                        if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCkInfo))
                        {
                            BaseFormCommonHelper.openAllwindowsdic.Remove(openCkInfo);
                        }
                        if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCkInfo))
                        {
                            BaseFormCommonHelper.openwindowsdic.Remove(openCkInfo);
                        }
                    }
                    if (item is MediFormWithQX tempForm)
                    {
                        tempForm.ValidateDataModifiedBeforeClose();//界面关闭前校验数据是否更改
                        tempForm.Close();

                        if (tempForm.IsDisposed)
                        {
                            OpenCKInfo openCkInfo = new OpenCKInfo { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = tempForm.GongNengId, caidanId = tempForm.CaiDanID, chuangkoumc = tempForm.Name.ToUpper() };

                            if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCkInfo))
                            {
                                BaseFormCommonHelper.openAllwindowsdic.Remove(openCkInfo);
                            }
                            if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCkInfo))
                            {
                                BaseFormCommonHelper.openwindowsdic.Remove(openCkInfo);
                            }

                            if (openwindowsdic.ContainsKey(openCkInfo))
                                openwindowsdic.Remove(openCkInfo);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            if (BaseFormCommonHelper.openedTabFormDic.ContainsKey(page.Name.ToUpper()))
            {
                BaseFormCommonHelper.openedTabFormDic.Remove(page.Name.ToUpper());
            }

            page.PageVisible = false;
            page.Dispose();
        }

        /// <summary>
        /// 加载默认的菜单及工具栏
        /// </summary>
        public void LoadDefaultMenuAndToolBar()
        {
            if (CaiDanList.Count < 1)
                return;

            int caidanLength = 0;
            if (CaiDanList.Count > 0)
            {
                caidanLength = CaiDanList.Count - 1;
            }
            if (caidanLength < 1) return;
            for (int i = caidanLength; i >= 0; i--)
            {
                string shangjicdid = CaiDanList[i].SHANGJICDID;
                if (shangjicdid == "-" || shangjicdid == null) shangjicdid = "";
                if (shangjicdid.Length <= 0) continue;
                string gongnengid = CaiDanList[i].GONGNENGID;
                if (YongHuQXList == null) continue;
                //判断功能权限
                var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                if (yonghuqx == null) CaiDanList[i].Delete();
            }

            //移除没有权限的菜单
            if (GYCanShuHelper.GetCanShu("是否启用菜单权限控制", "0").Equals("1"))
                CaiDanList.RemoveAll(c => c.GetState() == DTOState.Delete);

            //一级菜单
            CaiDanList.Where(o => o.SHANGJICDID.Equals(CurrentMainCaiDanID) && o.ISOPEN.Equals(0)).OrderBy(o => (Convert.ToInt32(o.SHUNXUHAO))).ToList().ForEach(o =>
            {
                BarItem item;
                var count = CaiDanList.Count(r => r.SHANGJICDID.Equals(o.CAIDANID) && r.ISOPEN.Equals(0));
                if (count > 0)
                {
                    item = new BarSubItem();
                }
                else
                {
                    item = new BarButtonItem();
                    item.ItemClick += caidanlan_ItemClick;
                }
                item.Name = o.CAIDANID;
                item.Caption = o.CAIDANMC;
                item.Enabled = o.QIYONGBZ == 1 ? false : true;
                item.ItemAppearance.Hovered.Options.UseBackColor = true;
                item.ItemAppearance.Hovered.Options.UseForeColor = true;
                item.ItemAppearance.Hovered.BackColor = Color.FromArgb(180, 215, 245);

                item.ItemAppearance.Pressed.Options.UseForeColor = true;
                item.ItemAppearance.Pressed.Options.UseBackColor = true;
                item.ItemAppearance.Pressed.BackColor = Color.FromArgb(5, 145, 206);
                item.ItemAppearance.Pressed.ForeColor = Color.White;
                otherMenuPM.AddItem(item);

            });
            ChuangJianCD(otherMenuPM.LinksPersistInfo, CaiDanList);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="items"></param>
        /// <param name="caidanList"></param>
        private void ChuangJianCD(LinksInfo items, List<E_GY_CAIDAN_NEW> caidanList)
        {
            foreach (LinkPersistInfo o in items)
            {
                bool breaksplit = false;
                caidanList.Where(p => p.SHANGJICDID.Equals(o.Item.Name) && p.ISOPEN.Equals(0))
                          .OrderBy(p => int.Parse(p.SHUNXUHAO))
                          .GroupBy(p => p.CAIDANID).ToList()
                          .ForEach(q =>
                {
                    var caidan = q.FirstOrDefault();
                    if (caidan == null) return;
                    if (caidan.CAIDANMC == "-")
                    {
                        breaksplit = true;
                    }
                    else
                    {
                        BarItem tsmi;
                        var count = caidanList.Count(r => r.SHANGJICDID.Equals(caidan.CAIDANID) && r.ISOPEN.Equals(0));

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

                        //必须要 要不然子菜单无法显示
                        this.otherMenuPM.Manager = this.otherMenuBM;
                        if (breaksplit)
                        {
                            (o.Item as BarSubItem)?.ItemLinks.Add(tsmi, false);
                            breaksplit = false;
                        }
                        else
                        {
                            (o.Item as BarSubItem)?.ItemLinks.Add(tsmi, false);
                        }
                    }
                });
                if (o.Item is BarSubItem item && item.LinksPersistInfo.Count > 0)
                {
                    ChuangJianCD(item.LinksPersistInfo, caidanList);
                }
            }
        }

        private void caidanlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menu = CaiDanList.FirstOrDefault(o => o.CAIDANID == e.Item.Name && o.ISOPEN.Equals(0));
            if (menu == null && string.IsNullOrEmpty(e.Item.Name))
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单ID【" + e.Item.Name + "】");
                return;
            }
            this.SuspendLayout();
            if (menu != null)
            {
                // 打开系统 menu.DAKAIKJ
                string openBySystem = menu.DAKAIKJ;
                if (!openBySystem.IsNullOrWhiteSpace() && openBySystem != "0")
                {
                    ChuangKouXX chuanKou = new ChuangKouXX();
                    switch (openBySystem)
                    {
                        case "12":
                            {
                                // 如果需要使用12系统打开，那么一定存在12系统的服务，所以这里应该调用12系统的服务根据病人id取病人住院id
                                var serviceClient = new ServiceClient("MZZJ-GongYong", "V1");
                                var rReturn = serviceClient.Invoke<E_ZY_BINGRENXX>("MZZJGYBingRenXX", "GetZhuYuanBRByBingRenID", new ServiceParm("bingRenID", BingRenID)).Return;
                                // 用医嘱框架打开病人页
                                DataMiddleWare.zhenJianInformation.BingRenZYID = rReturn.BINGRENZYID;
                                chuanKou.XiTongLX = ChuangKouXX.EXiTongLX.YiZhu;
                                chuanKou.BingRenZYID = rReturn.BINGRENZYID;
                                chuanKou.BingRenID = rReturn.BINGRENID;
                                break;
                            }
                        case "10":
                            {
                                // 如果需要使用12系统打开，那么一定存在12系统的服务，所以这里应该调用12系统的服务根据病人id取病人住院id
                                var serviceClient = new ServiceClient("MZZJ-GongYong", "V1");
                                var rReturn = serviceClient.Invoke<E_ZY_BINGRENXX>("MZZJGYBingRenXX", "GetZhuYuanBRByBingRenID", new ServiceParm("bingRenID", BingRenID)).Return;
                                // 用医生框架打开病人页
                                chuanKou.XiTongLX = ChuangKouXX.EXiTongLX.YiSheng;
                                chuanKou.BingRenZYID = rReturn.BINGRENZYID;
                                chuanKou.BingRenID = rReturn.BINGRENID;
                                break;
                            }
                        case "04":
                            {
                                // 如果需要使用04系统打开，那么一定存在04系统的服务，所以这里应该调用04系统的服务根据病人id取病人住院id
                                var serviceClient = new ServiceClient("MZZJ-GongYong", "V1");
                                var rReturn = serviceClient.Invoke<E_ZY_BINGRENXX>("MZZJGYBingRenXX", "GetZhuYuanBRByBingRenID", new ServiceParm("bingRenID", BingRenID)).Return;
                                // 用门诊框架打开病人页
                                chuanKou.XiTongLX = ChuangKouXX.EXiTongLX.MenZhen;
                                chuanKou.BingRenZYID = rReturn.BINGRENZYID;
                                chuanKou.BingRenID = rReturn.BINGRENID;
                                break;
                            }
                    }
                    TabFormOpenHelper.OpenTabForm(chuanKou);
                }
                else
                {
                    var parameters = menu.DIAOYONGCS.Split('|');
                    //浏览器打开
                    if (menu.DIAOYONGCS.Contains("OPENBROWSER"))
                    {
                        if (parameters.Length < 4 || string.IsNullOrEmpty(parameters[2]))
                        {
                            MediMsgBox.Warn($"菜单参数未配置正确");
                            return;
                        }
                        OpenBrowser(parameters[2]);
                        return;
                    }

                    // 用当前框架打开tab页
                    BaseFormCommonHelper.CreateForm(this, linChuangTabForm.mediBRTabControl, menu, ref openwindowsdic, false);
                    var formname = menu.DIAOYONGCS.Substring(0, menu.DIAOYONGCS.IndexOf('|') == -1 ? menu.DIAOYONGCS.Length : menu.DIAOYONGCS.IndexOf('|'));

                    var openCkInfo = new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Menu,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = menu.GONGNENGID,
                        caidanId = menu.CAIDANID,
                        chuangkoumc = formname.ToUpper(),
                        binrenid = BingRenID
                    };
                    //不加载360空白页
                    this.openwindowsdic.Where(o => o.Key.chuangkoumc == "W_ZJ_360VIEW").ToList().ForEach(o =>
                    {
                        if (o.Value != null)
                        {
                            openwindowsdic.Remove(o.Key);
                            BaseFormCommonHelper.TabClose((MediFormWithQX)o.Value);
                        }
                    });
                    BaseFormCommonHelper.openwindowsdic = openwindowsdic;
                    if (openwindowsdic.ContainsKey(openCkInfo))
                    {
                        if (!openedlcdic.ContainsKey(openCkInfo.chuangkoumc))
                        {
                            if (openwindowsdic[openCkInfo] is W_LINCHUANG_BASE lingchuangbase)
                            {
                                lingchuangbase.LinChuangFormClosed += Lingchuangbase_LinChuangFormClosed;
                                openedlcdic.Add(openCkInfo.chuangkoumc, lingchuangbase);
                            }
                        }
                    }
                    this.ResumeLayout(false);
                }
            }
        }
        /// <summary>
        /// 诊间增加直接打开外部网页的方法[HR6-5911] 
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenBrowser(string parameter)
        {
            Process process = new Process();
            try
            {
                var uri = parameter;
                switch (HISClientHelper.XITONGID)
                {
                    case "04":
                    case "10":
                    case "12":
                    case "36":
                    case "77":
                        {
                            uri = uri.Replace("[JIUZHENKH]", BingRenXXKJ.BingRenXX.JIUZHENKH);
                            uri = uri.Replace("[SHENFENZH]", BingRenXXKJ.BingRenXX.SHENFENZH);
                            uri = uri.Replace("[BINGRENID]", BingRenXXKJ.BingRenXX.BINGRENID);
                            break;
                        }
                }
                process.StartInfo.FileName = uri;
                process.Start();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                process.Dispose();
            }
        }

        private void Lingchuangbase_LinChuangFormClosed(object sender, LinChuangEventArgs lceventArgs)
        {
            if (sender is W_LINCHUANG_BASE linchuangbase)
            {
                if (openedlcdic.ContainsKey(linchuangbase.Name.ToUpper()))
                {
                    openedlcdic.Remove(linchuangbase.Name.ToUpper());
                }
            }
        }

        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<E_GY_CAIDAN_NEW> CaiDanList { get; set; }

        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<E_GY_YONGHUQX> YongHuQXList;

        /// <summary>
        /// 程序集集合
        /// </summary>
        public ConcurrentDictionary<string, ConcurrentDictionary<string, KeyValuePair<string, System.Reflection.Assembly>>> Assemblys { get; set; }

        /// <summary>
        /// 当前主菜单ID
        /// </summary>
        public string CurrentMainCaiDanID { get; set; }


        /// <summary>
        /// 病人框架公用信息
        /// </summary>
        public BingRenInformation BingRenXXKJ = new BingRenInformation();
        /// <summary>
        ///  病人ID
        /// </summary>
        public virtual string BingRenID { get; set; }
        /// <summary>
        /// 住院ID
        /// </summary>
        public virtual string BingRenZYID { get; set; }
        /// <summary>
        /// 就诊ID
        /// </summary>
        public virtual string JiuZhenID { get; set; }
        /// <summary>
        /// 右键父窗口同时打开子窗口标签
        /// </summary>
        public virtual string ShowDialogCKBZ { get; set; }
        // add by niquan 2019/9/19
        /// <summary>
        /// 标签页名称
        /// </summary>
        public virtual string TabPageName { get; set; }

        /// <summary>
        /// 床位ID
        /// </summary>

        public string ChuangWeiID { get; set; }
        /// <summary>
        /// 手术单id
        /// </summary>
        public virtual string ShouShuID { get; set; }

        /// <summary>
        /// 手术信息
        /// </summary>
        public virtual E_SM_SHOUSHUXXSM ShouShuXX { get; set; }

        /// <summary>
        /// 打开病人框架时候，是否调用预结算HR6-2337(547257)
        /// </summary>
        public virtual bool DiaoYongYJS { set; get; } = false;

        /// <summary>
        /// 初始化病人缓存
        /// </summary>
        private void IntialBingRenGYCache()
        {
            InitialSubCache(BingRenID, BingRenZYID);
        }

        /// <summary>
        /// （根据相关业务自定义初始化数据）由子类实现
        /// </summary>
        /// <param name="bingrenid">病人id</param>
        /// <param name="bingrenzyid">住院id</param>
        public virtual void InitialSubCache(string bingrenid, string bingrenzyid)
        {

        }

        /// <summary>
        /// 打开病人框架内的窗体
        /// </summary>
        /// <param name="lingChuangForm">窗体实例</param>
        /// <param name="openType">窗体类型</param>
        public void OpenLinChungWindow(W_LINCHUANG_BASE lingChuangForm, OpenType openType, bool opened = false, bool isSelect = true)
        {
            string openFangShi = string.Empty;
            switch (openType)
            {
                case OpenType.Open:
                    openFangShi = "OPEN";
                    break;
                case OpenType.OPENSHEET:
                    openFangShi = "OPENSHEET";
                    break;
                case OpenType.SHOW:
                    openFangShi = "SHOW";
                    break;
                default:
                    break;
            }
            string buttonid = string.Format("{0}\\{1}", HISClientHelper.YINGYONGID, lingChuangForm.Name);
            string caidanid = ((int)Convert.ToInt64(MediUniversalMFBase.Hash2MD516(buttonid), 16)).ToString();

            var denglucd = new E_GY_CAIDAN_NEW { CAIDANID = caidanid, GONGNENGID = "gn+" + caidanid + "", CAIDANMC = lingChuangForm.Text, DIAOYONGCS = "" + lingChuangForm.Name.ToUpper() + "|" + caidanid + "||" + openFangShi + "" };
            this.SuspendLayout();
            BaseFormCommonHelper.CreateForm(this, linChuangTabForm.mediBRTabControl, denglucd, ref openwindowsdic, lingChuangForm, opened, isSelect);
            var formname = lingChuangForm.Name;

            OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Button, xitongid = HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = BingRenID };
            BaseFormCommonHelper.openwindowsdic = openwindowsdic;
            if (openwindowsdic.ContainsKey(openCKInfo))
                if (!openedlcdic.ContainsKey(openCKInfo.chuangkoumc))
                {
                    if (openwindowsdic[openCKInfo] is W_LINCHUANG_BASE lingchuangbase)
                    {
                        lingchuangbase.LinChuangFormClosed += Lingchuangbase_LinChuangFormClosed;
                        openedlcdic.Add(openCKInfo.chuangkoumc, lingchuangbase);
                    }
                }
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialControls()
        {
            linChuangTabForm = new LinChuangTabForm();
            linChuangTabForm.TopLevel = false;
            linChuangTabForm.Dock = DockStyle.Fill;
            linChuangTabForm.FormBorderStyle = FormBorderStyle.None;
            mediPanelControl2.Controls.Add(linChuangTabForm);
            linChuangTabForm.Show();
        }

        private void W_BINGREN_BASE_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 初始化病人框架及其子类相关数据
        /// </summary>
        /// <param name="bingrenid"></param>
        /// <param name="zhuyuanid"></param>
        /// <param name="jiuzhenid"></param>
        public bool IntialAllBinXX(string bingrenid, string zhuyuanid, string jiuzhenid)
        {
            if (!InitialBingRenXX(bingrenid, zhuyuanid, jiuzhenid))
                return false;
            return true;
        }
        /// <summary>
        /// 初始化病人信息
        /// </summary>
        /// <param name="bingrenid">病人id</param>
        /// <param name="zhuyuanid">住院id</param>
        /// <param name="jiuzhenid">就诊id</param>
        /// <returns></returns>
        protected virtual bool InitialBingRenXX(string bingrenid, string zhuyuanid, string jiuzhenid)
        {
            return true;
        }

        /// <summary>
        /// 初始化框架信息
        /// </summary>
        /// <param name="kjform"></param>
        public void InitialKJHelper(dynamic kjform)
        {
            kjform.BingRenXXKJ = Utility.Util.JsonUtil.DeserializeToObject<BingRenInformation>(Utility.Util.JsonUtil.SerializeObject(BingRenXXKJ));
            InitialCustomKJHelper(kjform);
        }

        /// <summary>
        /// 初始化子类自定义相关业务数据(基类内部调用)
        /// </summary>
        /// <param name="kjform"></param>
        public virtual bool InitialCustomKJHelper(dynamic kjform)
        {
            return true;
        }

        /// <summary>
        /// 刷新打开的窗体
        /// </summary>
        /// <param name="chuagkoumc">窗口名称（不区分大小写）</param>
        /// <returns></returns>
        public object RefreshData(string chuagkoumc)
        {
            if (openwindowsdic.Count < 1)
                return null;
            RefreshUIEventArgs refreshUIEventArgs = new RefreshUIEventArgs();
            foreach (var openwindowpair in openwindowsdic)
            {
                if (openwindowpair.Key.chuangkoumc.ToUpper().Equals(chuagkoumc.ToUpper()))
                {
                    if (openwindowpair.Value is MediFormLCWithQX mediFormLCWithQX)
                    {
                        if (mediFormLCWithQX.ShuaXinJieMianDelegate == null) continue;
                        mediFormLCWithQX.ShuaXinJieMianDelegate(this, refreshUIEventArgs);
                        return refreshUIEventArgs.ResultState;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 动态调整Label位置，避免被遮挡
        /// </summary>
        /// <param name="mediLabelArray">需要调整的Label控件</param>
        protected void DynamicAdjustLabelLocation(params MediLabel[] mediLabelArray)
        {
            for (int i = 0; i < mediLabelArray.Length; i++)
            {
                if (i + 1 < mediLabelArray.Length)
                {
                    var currentLabel = mediLabelArray[i];
                    var nextLabel = mediLabelArray[i + 1];
                    if (currentLabel.Bounds.IntersectsWith(nextLabel.Bounds))
                    {
                        nextLabel.Location = new Point(currentLabel.Location.X + currentLabel.Width + 5, nextLabel.Location.Y);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ValidateDataModifiedBeforeClose()
        {
            //检查所有tabpage上的界面数据是否修改
            if (linChuangMainForm == null)
                return;
            foreach (XtraTabPage tabPage in linChuangTabForm.mediBRTabControl.TabPages)
            {
                foreach (Control ctr in tabPage.Controls)
                {
                    foreach (Control ctr1 in ctr.Controls)
                    {
                        if (ctr1 is W_LINCHUANG_BASE lingChuangForm)
                        {
                            lingChuangForm.ValidateDataModifiedBeforeClose();
                            if (lingChuangForm.DataModified)
                            {
                                DataModified = true;
                                return;
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 超过10个字时显示省略号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string GetStringWithEllipsis(string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 10)
                return str.Substring(0, 9).PadRight(12, '.');
            return str;
        }

        /// <summary>
        /// mainform中tab页切换触发的方法，由业务系统实现
        /// </summary>
        /// <param name="page"></param>
        public virtual void SelectPageChanged(object page)
        {

        }

        /// <summary>
        /// CDSS病人信息初始化
        /// </summary>
        public virtual void InitCDSSByBingRenXX()
        {

        }

        /// <summary>
        /// mainform中tab页切换触发的方法，由业务系统实现
        /// </summary>
        /// <param name="page"></param>
        public virtual void RemovePage(object page)
        {

        }

        /// <summary>
        ///  切换当前病人
        /// </summary>
        /// <param name="upordown">0：上一个;1：下一个</param>
        public virtual void QieHuanBRxx(int upordown)
        {
            // 当前病人ID
            string bingrenid = this.BingRenID;
            Dictionary<string, W_BINGRENKJ_BASE> openedkj = this.linChuangMainForm.kuangjiadic;
            List<string> bingrenlist = openedkj.Keys.ToList();
            if (!bingrenlist.Contains(bingrenid)) return;

            int index = bingrenlist.IndexOf(bingrenid);
            if (upordown == 0)
            {
                // 打开上一个病人
                int i = index - 1;
                if (i == -1)
                {
                    i = bingrenlist.Count - 1;
                }
                string id = bingrenlist[i];

                W_BINGRENKJ_BASE bingrenkj = openedkj[id];
                ChuangKouXX chuanKou = new ChuangKouXX();
                chuanKou.XiTongLX = ChuangKouXX.EXiTongLX.YiSheng;
                chuanKou.BingRenZYID = bingrenkj.BingRenXXKJ.BingRenXX.BINGRENZYID;
                chuanKou.BingRenID = bingrenkj.BingRenXXKJ.BingRenXX.BINGRENID;
                TabFormOpenHelper.OpenTabForm(chuanKou);
            }
            else if (upordown == 1)
            {
                int i = index + 1;
                // 打开下一个病人
                if (i == bingrenlist.Count)
                {
                    i = 0;
                }
                string id = bingrenlist[i];

                W_BINGRENKJ_BASE bingrenkj = openedkj[id];
                ChuangKouXX chuanKou = new ChuangKouXX();
                chuanKou.XiTongLX = ChuangKouXX.EXiTongLX.YiSheng;
                chuanKou.BingRenZYID = bingrenkj.BingRenXXKJ.BingRenXX.BINGRENZYID;
                chuanKou.BingRenID = bingrenkj.BingRenXXKJ.BingRenXX.BINGRENID;
                TabFormOpenHelper.OpenTabForm(chuanKou);
            }

        }
    }
}