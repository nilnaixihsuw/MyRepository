using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using Mediinfo.HIS.Core;
using Mediinfo.WinForm.HIS.Controls.SecondaryFramework;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.TabForm
{
    public partial class LinChuangTabForm : MediForm
    {
        /// <summary>
        /// 创建窗体对象（挂接窗体）
        /// </summary>
        public LinChuangTabForm linChuangTabForm { get; set; }
        ///// <summary>
        ///// 病人框架已打开的所有tab页(此窗体基类为W_LINCHUANG_BASE)
        ///// </summary>
        public Dictionary<string, dynamic> openedlcdic = new Dictionary<string, dynamic>();

        public W_BINGRENKJ_BASE kuangJiaBaseForm;

        /// <summary>
        /// 相似病历功能开启参数
        /// </summary>
        private bool EnableEMRInspection
        {
            get
            {
                return GYCanShuHelper.GetCanShu("公用_是否启用阿里相似病历比对", "1") == "1";

            }
        }

        public LinChuangTabForm()
        {
            InitializeComponent();
            this.Load -= LinChuangTabForm_Load;
            this.Load += LinChuangTabForm_Load;
        }

        private void LinChuangTabForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && (HISClientHelper.XITONGID.Equals("04")))
            {
                this.mediBRTabControl.TabHeaderHeight = 8;
                this.mediBRTabControl.Appearance.BackColor = Color.FromArgb(1, 255, 255, 255);
                this.mediBRTabControl.Appearance.Options.UseBackColor = true;

                #region 授权病人信息
                if (string.IsNullOrEmpty(BaseFormCommonHelper.ShouQuanBRBZ) && BaseFormCommonHelper.BingRenSQGNDList != null)
                {
                    // TODO 暂时
                    /*var shouQuanBR = new BQYZGYBingRenSQJLService().GetBingRenSQJLByZYID(this.kuangJiaBaseForm.BingRenZYID, HISClientHelper.DANGQIANKS, HISClientHelper.USERID);
                    if (shouQuanBR.ReturnCode == Enterprise.ReturnCode.SUCCESS && shouQuanBR.Return != null)
                    {
                        BaseFormCommonHelper.ShouQuanBRBZ = shouQuanBR.Return.SHOUQUANLX.ToString();
                        if (BaseFormCommonHelper.ShouQuanBRBZ == "2")
                        {
                            var bingRenSQGNDList = new BQYZGYBingRenSQJLService().GetBingRenSQGNDByZYID(shouQuanBR.Return.BINGRENSQJLID, this.kuangJiaBaseForm.BingRenZYID).Return;
                            BaseFormCommonHelper.BingRenSQGNDList = bingRenSQGNDList;
                        }
                    }*/
                }
                #endregion
            }

        }

        /// <summary>
        /// 创建业务对象tabpage
        /// </summary>
        public void CreateTabPages(W_LINCHUANG_BASE linchuanginstance)
        {
            DevExpress.XtraTab.XtraTabPage xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            xtraTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;

            xtraTabPage.Name = linchuanginstance.Name;
            xtraTabPage.Text = linchuanginstance.Text;

            MediPanelControl mediPanelControl = new MediPanelControl();
            mediPanelControl.Dock = DockStyle.Fill;
            xtraTabPage.Controls.Add(mediPanelControl);

            this.mediBRTabControl.TabPages.Add(xtraTabPage);
            linchuanginstance.TopLevel = false;
            linchuanginstance.Dock = DockStyle.Fill;
            linchuanginstance.FormBorderStyle = FormBorderStyle.None;
            linchuanginstance.Parent = mediPanelControl;
            linchuanginstance.Show();
            this.mediBRTabControl.SelectedTabPage = xtraTabPage;
        }

        public void mediTabClosed(DevExpress.XtraTab.XtraTabPage xtraTabPage)
        {
            if (BaseFormCommonHelper.openedTabFormDic.ContainsKey(xtraTabPage.Name.ToUpper()))
                BaseFormCommonHelper.openedTabFormDic.Remove(xtraTabPage.Name.ToUpper());
            foreach (Control control in xtraTabPage.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE)
                    {
                        W_LINCHUANG_BASE tempForm = item as W_LINCHUANG_BASE;
                        OpenCKInfo openCKInfo = new OpenCKInfo()
                        {
                            openWindowMode = OpenWindowMode.Menu,
                            xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID,
                            gongnengId = tempForm.denglucd.GONGNENGID,
                            caidanId = tempForm.denglucd.CAIDANID,
                            chuangkoumc = tempForm.Name.ToUpper(),
                            binrenid = tempForm.bingRenId
                        };
                        if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCKInfo))
                        {
                            BaseFormCommonHelper.openAllwindowsdic.Remove(openCKInfo);
                        }

                        //tempForm.Close();
                    }
                }
            }

            xtraTabPage.PageVisible = false;
            try
            {
                xtraTabPage.Dispose();
            }
            catch (Exception ex)
            {
                xtraTabPage.PageVisible = false;
                xtraTabPage.Dispose();
            }
        }

        private void mediTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (e is DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)
            {
                DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs closePageButtonEventArgs = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                DevExpress.XtraTab.XtraTabPage xtraTabPage = closePageButtonEventArgs.Page as DevExpress.XtraTab.XtraTabPage;

                foreach (Control control in xtraTabPage.Controls)
                {
                    foreach (var item in control.Controls)
                    {
                        if (item is W_LINCHUANG_BASE)
                        {
                            W_LINCHUANG_BASE tempForm = item as W_LINCHUANG_BASE;
                            tempForm.ValidateDataModifiedBeforeClose();//界面关闭前校验数据是否更改
                            tempForm.Close();
                            if (!tempForm.IsDisposed)
                                return;
                            else
                            {
                                OpenCKInfo openCKInfo = new OpenCKInfo()
                                {
                                    openWindowMode = OpenWindowMode.Menu,
                                    xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID,
                                    gongnengId = tempForm.denglucd.GONGNENGID,
                                    caidanId = tempForm.denglucd.CAIDANID,
                                    chuangkoumc = tempForm.Name.ToUpper(),
                                    binrenid = tempForm.bingRenId
                                };
                                if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCKInfo))
                                {
                                    BaseFormCommonHelper.openAllwindowsdic.Remove(openCKInfo);
                                }
                            }
                        }
                    }
                }
                if (BaseFormCommonHelper.openedTabFormDic.ContainsKey(xtraTabPage.Name.ToUpper()))
                    BaseFormCommonHelper.openedTabFormDic.Remove(xtraTabPage.Name.ToUpper());

                xtraTabPage.PageVisible = false;
                xtraTabPage.Dispose();
            }

        }

        private void LinChuangTabForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.mediBRTabControl.TabPages.Count > 0)
            {
                foreach (XtraTabPage tabpage in this.mediBRTabControl.TabPages)
                {
                    if (this.Name.Equals(tabpage.Name))
                    {
                        this.mediBRTabControl.TabPages.Remove(tabpage);
                        break;
                    }
                }

            }
        }

        private void mediBRTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //MediLCTabControl mediLCTabControl = sender as MediLCTabControl;
                //Point pt = MousePosition;
                //XtraTabHitInfo xtraTabHitInfo = mediLCTabControl.CalcHitInfo(mediLCTabControl.PointToClient(pt));
                //if (xtraTabHitInfo.HitTest== XtraTabHitTest.PageHeader)
                //{
                //    lcTabControlRightMenu.ShowPopup(pt);
                //}
            }
        }

        private void mediBRTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {

            if (e.Page == null) return;

            //关闭病人界面时，取消数据处理操作
            if (BaseFormCommonHelper.CloseForm)
                return;

            //同一个窗体传入参数不同时，tab页name为窗体名+“|”+调用参数，再进行调用参数匹配时要将name进行拆分后匹配。
            int startIndex = e.Page.Name.IndexOf('|') == -1 ? 0 : e.Page.Name.IndexOf('|') + 1;
            var denglucd = BaseFormCommonHelper.CaiDanList.Where(p => !string.IsNullOrEmpty(p.DIAOYONGCS)).FirstOrDefault(o => o.DIAOYONGCS.Contains(e.Page.Name.Substring(startIndex, e.Page.Name.Length - startIndex)) && o.CAIDANMC == e.Page.Text && o.ISOPEN.Equals(1));

            if (denglucd != null)
            {
                var openCKInfo = denglucd.GONGNENGID.StartsWith("gn")
                    ? new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Button,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = denglucd.GONGNENGID,
                        caidanId = denglucd.CAIDANID,
                        binrenid = this.kuangJiaBaseForm.BingRenID,
                        chuangkoumc = e.Page.Name.ToUpper()
                    }
                    : new OpenCKInfo
                    {
                        openWindowMode = OpenWindowMode.Menu,
                        xitongid = HISClientHelper.XITONGID,
                        gongnengId = denglucd.GONGNENGID,
                        caidanId = denglucd.CAIDANID,
                        binrenid = this.kuangJiaBaseForm.BingRenID,
                        chuangkoumc = e.Page.Name.ToUpper()
                    };
                //if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCKInfo) && BaseFormCommonHelper.openwindowsdic[openCKInfo] != null 
                //    || string.IsNullOrEmpty(e.Page.Name) || BaseFormCommonHelper.openCKInfos.Contains(openCKInfo))
                //{
                //    BaseFormCommonHelper.openCKInfos.Remove(openCKInfo);
                //    return;
                //}
                this.SuspendLayout();

                if (denglucd.DIAOYONGCS.Contains("NO"))
                    denglucd.DIAOYONGCS = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.Length - 3) + "|YES";

                BaseFormCommonHelper.CreateForm(this.kuangJiaBaseForm, mediBRTabControl, denglucd, ref BaseFormCommonHelper.openwindowsdic, false);

                if (this.kuangJiaBaseForm.openedlcdic.ContainsKey(openCKInfo.chuangkoumc) && this.kuangJiaBaseForm.openedlcdic[openCKInfo.chuangkoumc] == null)
                {
                    if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCKInfo))
                    {
                        if (BaseFormCommonHelper.openwindowsdic[openCKInfo] is W_LINCHUANG_BASE)
                        {
                            W_LINCHUANG_BASE lingchuangbase = BaseFormCommonHelper.openwindowsdic[openCKInfo] as W_LINCHUANG_BASE;

                        }
                        this.kuangJiaBaseForm.openedlcdic[openCKInfo.chuangkoumc] = (BaseFormCommonHelper.openwindowsdic[openCKInfo] as W_LINCHUANG_BASE);
                    }
                }
                this.ResumeLayout(false);

                //记录患者界面所选中菜单的功能ID add by 余佳平
                HISClientHelper.SELECTEDGONGNENGID = denglucd.GONGNENGID;
                if(!string.IsNullOrEmpty(this.kuangJiaBaseForm?.BingRenID))
                {
                    if(BaseFormCommonHelper.OpenedCaiDanDic.ContainsKey(this.kuangJiaBaseForm.BingRenID))
                    {
                        BaseFormCommonHelper.OpenedCaiDanDic[this.kuangJiaBaseForm.BingRenID] = denglucd;
                    }
                    else
                    {
                        BaseFormCommonHelper.OpenedCaiDanDic.Add(this.kuangJiaBaseForm.BingRenID,denglucd);
                    }
                }
            }
        }

        private void Lingchuangbase_LinChuangFormClosed(object sender, LinChuangEventArgs lceventArgs)
        {
            if (sender is W_LINCHUANG_BASE)
            {
                W_LINCHUANG_BASE linchuangbase = sender as W_LINCHUANG_BASE;
                if (openedlcdic.ContainsKey(linchuangbase.Name.ToUpper()))
                {
                    openedlcdic.Remove(linchuangbase.Name.ToUpper());
                }
            }
        }

        private void mediBRTabControl_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            foreach (Control control in e.PrevPage.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE)
                    {
                        W_LINCHUANG_BASE tempForm = item as W_LINCHUANG_BASE;
                        if (tempForm.IsModifyData())
                        {
                            MediMsgBox.FloatMsg(tempForm, "请先保存已修改的数据!");

                            e.Cancel = true;
                            return;

                        }

                    }
                }
            }
        }
    }
}