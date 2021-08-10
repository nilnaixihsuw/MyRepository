using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraSplashScreen;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.Utility.Util;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Controls.Message;
using Mediinfo.WinForm.HIS.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 添加消息内容后弹出提示框
    /// </summary>
    public delegate void XiaoXiDialogEvent();
    /// <summary>
    /// 消息数量处理
    /// </summary>
    /// <param name="xiaoXiSL"></param>
    public delegate void XiaoXiSLEvent(int xiaoXiSL);
    /// <summary>
    /// 窗口关闭处理
    /// </summary>
    /// <param name="xiaoXiSL"></param>
    public delegate void XiaoXiCloseEvent(int xiaoXiSL);
    /// <summary>
    /// 消息管理窗口
    /// </summary>
    public partial class XiaoXiGL : MediDialog
    {
        /// <summary>
        /// 消息数量动态获取委托
        /// </summary>
        public event XiaoXiSLEvent XXSLEvent;
        /// <summary>
        /// 消息关闭委托处理
        /// </summary>
        public event XiaoXiCloseEvent XXCloseEvent;


        public XiaoXiDialogEvent XiaoXiSaveDialog;
        public HISMessageBody xiaoXi = new HISMessageBody();
        private JCJGXiaoXiService xiaoXiService = new JCJGXiaoXiService();
        private List<HISMessageBody> yiDuXX = new List<HISMessageBody>();
        private List<HISMessageBody> removeMess = new List<HISMessageBody>();
        private Control currentControl;
        private DateTime faSongSJ = HISClientHelper.GetSysDate().AddDays(-30);
        private List<HISMessageBody> yeMianYDCL = new List<HISMessageBody>();

        public LayoutViewHitInfo gyLayoutViewHitInfo;
        private bool isSave;
        /// <summary>
        /// 表示在哪个窗口的标识false在未读，true在已读
        /// </summary>
        private bool readXXBZ;
        /// <summary>
        /// 是否停留在本tab页
        /// </summary>
        public bool IsStayPage;
        /// <summary>
        /// 判断是否处理了数据
        /// </summary>
        public bool IsSave
        {
            get
            {
                return isSave;
            }
            set
            {
                isSave = value;
                if (!isSave && gyLayoutViewHitInfo != null)
                {
                    if (readXXBZ)//表示点击的是已读tab页中的数据
                    {
                        SetYiDuXXCK(gyLayoutViewHitInfo);
                    }
                    else
                    {//表示点击的是未读tab页中的数据
                        SetWeiDuXXCK(gyLayoutViewHitInfo);
                    }
                    gyLayoutViewHitInfo = null;
                }
            }
        }
        //private int index = 0;
        public XiaoXiGL()
        {
            InitializeComponent();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            InitControls();
            InitData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            //重打开页面时默认选中未读tab页
            this.mediTabControl2.SelectedTabPageIndex = 1;
            this.mediTabControl2.SelectedPageChanging += MediTabControl2_SelectedPageChanging;
            mediSearchControl1.Text = null;
            //查询已读消息
            var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, faSongSJ);
            if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Count > 0)
            {
                if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
                {
                    yiDuXX = xiaoXiData.Return;
                    if (yiDuXX != null && yiDuXX.Count > 0)
                    {
                        foreach (HISMessageBody body in yiDuXX)
                        {
                            //判断是否为当前应用消息
                            if (body.XiaoXiBM.SubString(0, 2) == HISClientHelper.KUCUNYYID.SubString(0, 2))
                            {
                                body.XiaoXiBD = "●";
                                var messageBody = HISClientHelper.dictryMessBody["未处理"].Where(p => p.XiaoXiID == body.XiaoXiID);
                                if (messageBody != null && messageBody.Count() > 0)
                                {
                                    HISClientHelper.dictryMessBody["未处理"].Remove(messageBody.ToList()[0]);
                                }
                            }
                        }
                    }
                }
                //绑定未处理消息
                this.xtraTabPage1.Text = "未处理(" + HISClientHelper.dictryMessBody["未处理"].Count + ")";
                HISClientHelper.XIAOXIZS = HISClientHelper.dictryMessBody["未处理"].Count;
                this.mediGridControl1.DataSource = HISClientHelper.dictryMessBody["未处理"].OrderByDescending(p => p.FaSongSj).ToList();
            }
            else
            {
                if (HISClientHelper.XIAOXIZS == 0)
                    this.xtraTabPage1.Text = "未处理(0)";
            }

            if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
            {
                if (!HISClientHelper.dictryMessBody.ContainsKey("已处理"))
                {
                    HISClientHelper.dictryMessBody.Add("已处理", xiaoXiData.Return);
                }

                foreach (HISMessageBody mess in xiaoXiData.Return)
                {
                    //判断是否为当前应用消息
                    if (mess.XiaoXiBM.SubString(0, 2) == HISClientHelper.KUCUNYYID.SubString(0, 2))
                    {
                        mess.XiaoXiBD = "";
                        if (!HISClientHelper.dictryMessBody["已处理"].Where(p => p.XiaoXiID == mess.XiaoXiID).Any())
                        {
                            HISClientHelper.dictryMessBody["已处理"].Add(mess);
                        }
                    }
                }

                if (HISClientHelper.dictryMessBody["已处理"].Count > 0)
                {
                    IList<HISMessageBody> yiChuLi = HISClientHelper.dictryMessBody["已处理"];
                    this.mediGridControl2.DataSource = yiChuLi.Where(p => p.FaSongSj >= DateTime.Now.AddDays(-7)).OrderByDescending(p => p.FaSongSj).ToList();
                }
            }

            #region 临时注释

            //var xiaoXiAllData = xiaoXiService.GetAllXX(HISClientHelper.ZAIXIANZTID);
            ////加载全部消息
            //List<HISMessageBody> list = new List<HISMessageBody>();
            //if (HISClientHelper.dictryMessBody.ContainsKey("已处理"))
            //    list.AddRange(HISClientHelper.dictryMessBody["已处理"]);
            //if (HISClientHelper.dictryMessBody.ContainsKey("未处理"))
            //    list.AddRange(HISClientHelper.dictryMessBody["未处理"]);
            //if (xiaoXiAllData.ReturnCode == ReturnCode.SUCCESS)
            //{
            //    if (list.Count > xiaoXiAllData.Return.Count)
            //    {
            //        this.xtraTabPage4.Text = "全部消息(" + list.Count + ")";
            //        this.mediGridControl4.DataSource = list.OrderByDescending(p => p.FaSongSj).ToList();
            //    }
            //    else
            //    {
            //        this.xtraTabPage4.Text = "全部消息(" + xiaoXiAllData.Return.Count + ")";
            //        this.mediGridControl4.DataSource = xiaoXiAllData.Return.OrderByDescending(p => p.FaSongSj).ToList();
            //    }
            //}
            //else
            //{
            //    this.xtraTabPage4.Text = "全部消息(" + list.Count + ")";
            //    this.mediGridControl4.DataSource = list.OrderByDescending(p => p.FaSongSj).ToList();
            //}

            #endregion

            if (XXSLEvent != null)
            {
                //动态加载消息数量
                XXSLEvent(HISClientHelper.XIAOXIZS);
            }

            #region 消息默认加载

            //if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Count > 0)
            //{
            //    this.mediLayoutView1.ActiveBackColor = Color.FromArgb(171, 214, 255);
            //    HISMessageBody xiaoxXiMessage = this.mediLayoutView1.GetRow(0) as HISMessageBody;
            //    if (xiaoxXiMessage == null)
            //    {
            //        xiaoxXiMessage = HISClientHelper.dictryMessBody["未处理"][HISClientHelper.dictryMessBody["未处理"].Count - 1];
            //    }
            //    //消息查看处理
            //    if (xiaoxXiMessage.BaoMiXxBz == 1)
            //    {
            //        using (ChaKanXXRZ rzFrom = new ChaKanXXRZ())
            //        {
            //            rzFrom.yongHuRZ.ZhiGongGH = HISClientHelper.ZHIGONGGH;
            //            rzFrom.ShowDialog();
            //            if (!rzFrom.RenZhenJG)
            //                return;
            //        }
            //    }
            //    //加载右边消息主体
            //    ShowRightNR(xiaoxXiMessage);
            //}

            moRenJZ("未处理");

            #endregion
        }

        /// <summary>
        /// 默认打开页面
        /// </summary>
        /// <param name="type"></param>
        private void moRenJZ(string type)
        {
            #region 消息默认加载

            if (HISClientHelper.dictryMessBody.ContainsKey(type) && HISClientHelper.dictryMessBody[type].Count > 0)
            {
                this.mediLayoutView1.ActiveBackColor = Color.FromArgb(171, 214, 255);
                HISMessageBody xiaoxXiMessage = new HISMessageBody();
                if (type == "未处理")
                    xiaoxXiMessage = this.mediLayoutView1.GetRow(0) as HISMessageBody;
                else if (type == "已处理")
                    xiaoxXiMessage = this.mediLayoutView2.GetRow(0) as HISMessageBody;
                if (xiaoxXiMessage == null)
                {
                    xiaoxXiMessage = HISClientHelper.dictryMessBody[type][HISClientHelper.dictryMessBody[type].Count - 1];
                }
                //消息查看处理
                if (xiaoxXiMessage.BaoMiXxBz == 1)
                {
                    using (ChaKanXXRZ rzFrom = new ChaKanXXRZ())
                    {
                        rzFrom.yongHuRZ.ZhiGongGH = HISClientHelper.ZHIGONGGH;
                        rzFrom.ShowDialog();
                        if (!rzFrom.RenZhenJG)
                            return;
                    }
                }
                //加载右边消息主体
                ShowRightNR(xiaoxXiMessage);
            }

            #endregion
        }


        private void MediTabControl2_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //用来判断是否点击其他tab页了
            //add by meiyuan @2020.8.5 for (HB6-14388)
            switch (mediTabControl2.SelectedTabPageIndex)
            {
                case 1:
                    if (isSave)
                    {
                        if (!IsStayPage)
                        {
                            readXXBZ = true;
                            //gyLayoutViewHitInfo = layoutViewHitInfo;
                            //委托去通知右侧对话框，可以弹出提示框
                            if (XiaoXiSaveDialog != null)
                                XiaoXiSaveDialog.Invoke();
                            //这个时候gyLayoutViewHitInfo为空就去已处理中选中默认行，如果没有被点击就选中第一行
                            if (!isSave && gyLayoutViewHitInfo == null)
                            {
                                IsStayPage = false;
                                var rows = this.mediLayoutView2.GetSelectedRows();
                                if (rows != null && !rows.Any())
                                    this.mediLayoutView2.SelectRow(0);
                                else
                                {
                                    HISMessageBody xiaoxXiMessage = this.mediLayoutView2.GetFocusedRow() as HISMessageBody;
                                    ShowRightNR(xiaoxXiMessage);
                                }
                            }
                            else
                            {
                                IsStayPage = true;
                                mediTabControl2.SelectedTabPageIndex = 1;
                            }
                        }
                    }
                    break;
                case 2:
                    IsStayPage = false;
                    break;
            }
        }

        /// <summary>
        ///消息数量加载
        /// </summary>
        /// <param name="xiaoXiSL"></param>
        public void XiaoXiInit()
        {
            this.xtraTabPage1.Text = "未处理(" + HISClientHelper.dictryMessBody["未处理"].Count.ToString() + ")";
            this.mediGridControl1.DataSource = HISClientHelper.dictryMessBody["未处理"].OrderByDescending(p => p.FaSongSj).ToList();
            if (XXSLEvent != null)
            {
                //动态加载消息数量
                XXSLEvent(HISClientHelper.dictryMessBody["未处理"].Count);
            }
            //每次重新加载时验证已读消息
            //var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, faSongSJ);
            //if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
            //{
            //    yiDuXX = xiaoXiData.Return;
            //}
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        public void InitControls()
        {
            this.mediLayoutView4.CustomFieldValueStyle += MediLayoutView4_CustomFieldValueStyle;
            this.mediLayoutView4.MouseDown += MediLayoutView4_MouseDown;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediLayoutView4_MouseDown(object sender, MouseEventArgs e)
        {
            //string[] wjzBM = new string[] { "04001", "10001", "12001", "12005", "10004", "04002" };
            string[] wjzBM = new string[] { "04001", "10001", "12001", "12005", "10004", "04002", "34001", "34005", "35001", "35004" };
            Point mousePos = this.mediGridControl4.PointToClient(MousePosition);
            LayoutViewHitInfo layoutViewHitInfo = this.mediLayoutView4.CalcHitInfo(mousePos.X, mousePos.Y);
            if (e.Button == MouseButtons.Left)
            {
                if (layoutViewHitInfo != null && layoutViewHitInfo.InCard)
                {
                    this.mediLayoutView4.FocusedRowHandle = layoutViewHitInfo.RowHandle;
                    //点击查看页面
                    this.mediLayoutView4.ActiveBackColor = Color.FromArgb(171, 214, 255);
                    HISMessageBody xiaoxXiMessage = this.mediLayoutView4.GetFocusedRow() as HISMessageBody;

                    if (wjzBM.Contains(xiaoxXiMessage.XiaoXiBM) || string.IsNullOrEmpty(xiaoxXiMessage.XiaoXiBD))
                    {
                        ShowRightNR(xiaoxXiMessage);
                        return;
                    }

                    xiaoxXiMessage.XiaoXiBD = "";
                    ShowRightNR(xiaoxXiMessage);

                    var yueDuXX = xiaoXiService.YueDuXiaoXi(Convert.ToInt32(xiaoxXiMessage.XiaoXiID), HISClientHelper.USERID);

                    //清除未处理，添加已处理
                    if (HISClientHelper.dictryMessBody.ContainsKey("未处理"))
                    {
                        if (HISClientHelper.dictryMessBody["未处理"].Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).Any())
                        {
                            HISClientHelper.dictryMessBody["未处理"].Remove(xiaoxXiMessage);
                            HISClientHelper.XIAOXIZS--;
                        }

                        //绑定数据
                        this.xtraTabPage1.Text = "未处理(" + HISClientHelper.dictryMessBody["未处理"].Count + ")";
                        this.mediGridControl1.DataSource = HISClientHelper.dictryMessBody["未处理"].OrderByDescending(p => p.FaSongSj).ToList(); ;
                        this.mediGridControl1.RefreshDataSource();
                    }
                    if (!HISClientHelper.dictryMessBody.ContainsKey("已处理"))
                        HISClientHelper.dictryMessBody.Add("已处理", (new List<HISMessageBody>() { xiaoxXiMessage }));
                    else if (!HISClientHelper.dictryMessBody["已处理"].Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).Any())
                        HISClientHelper.dictryMessBody["已处理"].Add(xiaoxXiMessage);

                    this.mediGridControl2.DataSource = HISClientHelper.dictryMessBody["已处理"].OrderByDescending(p => p.FaSongSj).ToList(); ;
                    this.mediGridControl1.RefreshDataSource();

                    //加载全部消息
                    List<HISMessageBody> list = new List<HISMessageBody>();
                    if (HISClientHelper.dictryMessBody.ContainsKey("已处理"))
                        list.AddRange(HISClientHelper.dictryMessBody["已处理"]);
                    if (HISClientHelper.dictryMessBody.ContainsKey("未处理"))
                        list.AddRange(HISClientHelper.dictryMessBody["未处理"]);
                    this.xtraTabPage4.Text = "全部消息(" + list.Count + ")";
                    this.mediGridControl4.DataSource = list.OrderByDescending(p => p.FaSongSj).ToList();
                    this.mediLayoutView4.SelectRow(layoutViewHitInfo.RowHandle);
                }
            }
        }

        /// <summary>
        /// 加载窗体内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediLayoutView4_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
        {
            if (e.Column.FieldName == "XiaoXiBD")
            {
                string XiaoXiJc = this.mediLayoutView1.GetRowCellValue(e.RowHandle, "XiaoXiJc").ToStringEx();
                if (XiaoXiJc.Contains("危急"))
                    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
                else if (XiaoXiJc.Contains("通知"))
                    e.Appearance.ForeColor = Color.FromArgb(204, 204, 204);
                else
                    e.Appearance.ForeColor = Color.FromArgb(255, 153, 000);

            }
            if (e.Column.FieldName == "XiaoXiJc")
            {
                e.Appearance.BackColor = Color.FromArgb(255, 153, 000);
            }
            //if (e.Column.FieldName == "XiaoXiMc")
            //{
            //    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
            //}
        }

        /// <summary>
        /// 隐藏窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Count > 0)
            {
                foreach (HISMessageBody messBody in HISClientHelper.dictryMessBody["未处理"])
                {
                    if (!yiDuXX.Where(p => p.XiaoXiID == messBody.XiaoXiID).Any() && HISClientHelper.XiaoXiRightNR[messBody.XiaoXiBM].Split('|')[1] == "1")
                    {
                        var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, faSongSJ);
                        if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
                        {
                            yiDuXX = xiaoXiData.Return;
                        }
                        if (!yiDuXX.Where(p => p.XiaoXiID == messBody.XiaoXiID).Any() && HISClientHelper.XiaoXiRightNR[messBody.XiaoXiBM].Split('|')[1] == "1")
                        {
                            if (HISClientHelper.XiaoXiRightNR.ContainsKey(messBody.XiaoXiBM) && HISClientHelper.XiaoXiRightNR[messBody.XiaoXiBM].Split('|')[1] == "1")
                            {
                                MediMsgBox.Show("请优先处理完危急值消息再关闭窗口！");
                                e.Cancel = true;
                                return;
                            }
                        }
                    }
                }
            }

            if (HISClientHelper.dictryMessBody.ContainsKey("未处理"))
                XXCloseEvent(HISClientHelper.dictryMessBody["未处理"].Count);
            else
                XXCloseEvent(0);

            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// 消息框右边控件
        /// </summary>
        /// <param name="xiaoxXiMessage"></param>
        private void XiaoXi_retMsgEvent(HISMessageBody xiaoxXiMessage)
        {
            if (HISClientHelper.XiaoXiRightNR.ContainsKey(xiaoxXiMessage.XiaoXiBM) && !string.IsNullOrEmpty(HISClientHelper.XiaoXiRightNR[xiaoxXiMessage.XiaoXiBM]))
            {
                ShowRightNR(xiaoxXiMessage);
            }

            xiaoXi = xiaoxXiMessage;
            //颜色处理
            foreach (KeyValuePair<string, Control> kvp in HISClientHelper.xiaoXiBackColor)
            {
                MediLayoutXiaoXiZS xiaoXi = kvp.Value as MediLayoutXiaoXiZS;

                if (kvp.Key == xiaoxXiMessage.XiaoXiID)
                {
                    xiaoXi.BackColorSZ(true);
                    if (yiDuXX.Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).Any() && !HISClientHelper.XIAOXIYDNR.Contains(xiaoxXiMessage))
                    {
                        HISClientHelper.XIAOXIZS--;
                        //HISClientHelper.XIAOXINR.Remove(xiaoxXiMessage);
                        HISClientHelper.XIAOXIYDNR.Add(xiaoxXiMessage);
                        HISClientHelper.XIAOXINR_LS.Add(xiaoxXiMessage);
                    }
                }
                else
                    xiaoXi.BackColorSZ(false);
            }

            if (HISClientHelper.XIAOXINR_LS.Count == 2)
            {                //未读消息加载
                Invoke(new MethodInvoker(delegate ()
                {
                    //foreach (Control c in this.mediScrollablePanelControl1.Controls)
                    //{
                    //    if (c is MediLayoutXiaoXiZS)
                    //    {
                    //        if (c.Name != xiaoxXiMessage.XiaoXiID)
                    //        {
                    //            this.mediScrollablePanelControl1.Controls.Remove(c);
                    //            c.Dispose();
                    //            HISMessageBody body = HISClientHelper.XIAOXINR_LS.Where(p => p.XiaoXiID != xiaoxXiMessage.XiaoXiID).ToList()[0];
                    //            HISClientHelper.XIAOXINR_LS.Remove(body);
                    //            HISClientHelper.XIAOXINR.Remove(body);
                    //            this.xtraTabPage1.Text = "未读(" + HISClientHelper.XIAOXIZS.ToString() + ")";
                    //        }
                    //    }
                    //}
                }));
            }
            if (HISClientHelper.XIAOXINR_LS.Count == 1)
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    //foreach (Control c in this.mediScrollablePanelControl1.Controls)
                    //{
                    //    if (c is MediLayoutXiaoXiZS)
                    //    {
                    //        if (c.Name == xiaoxXiMessage.XiaoXiID)
                    //        {
                    //            MediLayoutXiaoXiZS ydXiaoXi = (MediLayoutXiaoXiZS)c;
                    //            ydXiaoXi.retMsgEvent += XiaoXi_retMsgEvent;
                    //            ydXiaoXi.retxiaoXiYDEvent += XiaoXi_retxiaoXiYDEvent;
                    //        }
                    //    }
                    //}
                }));
            }
            //已读消息加载
            Invoke(new MethodInvoker(delegate ()
            {
                HISClientHelper.XIAOXIYDZS++;
                MediLayoutXiaoXiZS xiaoXi = new MediLayoutXiaoXiZS(xiaoxXiMessage);
                //MediXiaoXiZS xiaoXi = new MediXiaoXiZS(message);
                xiaoXi.Dock = DockStyle.Top;
                xiaoXi.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                if (HISClientHelper.XIAOXIYDZS == 0)
                    xiaoXi.Location = new Point(1, 1);
                else
                    xiaoXi.Location = new Point(1, HISClientHelper.XIAOXIYDZS * xiaoXi.Height);
                //this.mediScrollablePanelControl2.Controls.Add(xiaoXi);
                xiaoXi.retxiaoXiYDEvent += XiaoXi_retxiaoXiYDEvent;
            }));
        }

        /// <summary>
        /// 未读消息加载
        /// </summary>
        private void WeiDuXX()
        {
            //foreach (HISMessageBody mesage in HISClientHelper.XIAOXINR_LS)
            //{                
            //    HISClientHelper.XIAOXIZS--;
            //    HISClientHelper.XIAOXINR.Remove(mesage);
            //    HISClientHelper.XIAOXIYDNR.Add(mesage);
            //}

            //已读消息加载
            Invoke(new MethodInvoker(delegate ()
            {
                //this.mediScrollablePanelControl1.Controls.Clear();
                foreach (HISMessageBody msage in HISClientHelper.XIAOXINR)
                {
                    MediLayoutXiaoXiZS xiaoXi = new MediLayoutXiaoXiZS(msage);
                    //MediXiaoXiZS xiaoXi = new MediXiaoXiZS(message);
                    xiaoXi.Dock = DockStyle.Top;
                    xiaoXi.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    if (HISClientHelper.XIAOXIZS == 0)
                        xiaoXi.Location = new Point(1, 1);
                    else
                        xiaoXi.Location = new Point(1, HISClientHelper.XIAOXIZS * xiaoXi.Height);
                    //this.mediScrollablePanelControl1.Controls.Add(xiaoXi);
                    xiaoXi.retMsgEvent -= XiaoXi_retMsgEvent;
                    xiaoXi.retxiaoXiYDEvent += XiaoXi_retxiaoXiYDEvent;
                }
            }));
        }

        /// <summary>
        /// 获取已读消息
        /// </summary>
        private void GetYiDuXiaoXi()
        {
            var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, faSongSJ);
            if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
            {
                yiDuXX = xiaoXiData.Return;
                if (yiDuXX != null && yiDuXX.Count > 0)
                {
                    foreach (HISMessageBody xx in yiDuXX)
                    {
                        HISClientHelper.XIAOXIYDNR.Add(xx);
                        HISClientHelper.XIAOXIYDZS++;
                        MediLayoutXiaoXiZS xiaoXi = new MediLayoutXiaoXiZS(xx);
                        //MediXiaoXiZS xiaoXi = new MediXiaoXiZS(message);
                        xiaoXi.Dock = DockStyle.Top;
                        xiaoXi.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        if (HISClientHelper.XIAOXIYDZS == 0)
                            xiaoXi.Location = new Point(1, 1);
                        else
                            xiaoXi.Location = new Point(1, HISClientHelper.XIAOXIYDZS * xiaoXi.Height);
                        //this.mediScrollablePanelControl2.Controls.Add(xiaoXi);
                        HISClientHelper.xiaoXiBackColor.Add(xx.XiaoXiID, xiaoXi);
                        xiaoXi.retxiaoXiYDEvent += XiaoXi_retxiaoXiYDEvent;
                    }
                }
            }
        }

        /// <summary>
        /// 消息框右边控件
        /// </summary>
        /// <param name="xiaoxXiMessage"></param>
        private void XiaoXi_retxiaoXiYDEvent(HISMessageBody xiaoxXiMessage)
        {
            xiaoXi = xiaoxXiMessage;
            //颜色处理
            foreach (KeyValuePair<string, Control> kvp in HISClientHelper.xiaoXiBackColor)
            {
                MediLayoutXiaoXiZS xiaoXi = kvp.Value as MediLayoutXiaoXiZS;
                if (kvp.Key == xiaoxXiMessage.XiaoXiID)
                    xiaoXi.BackColorSZ(true);
                else
                    xiaoXi.BackColorSZ(false);
            }

            if (HISClientHelper.XiaoXiRightNR.ContainsKey(xiaoxXiMessage.XiaoXiBM) && !string.IsNullOrEmpty(HISClientHelper.XiaoXiRightNR[xiaoxXiMessage.XiaoXiBM]))
            {
                ShowRightNR(xiaoxXiMessage);
            }
        }

        /// <summary>
        /// 未读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLayoutView1_MouseDown(object sender, MouseEventArgs e)
        {

            //Point mousePos = this.mediGridControl1.PointToClient(MousePosition);
            LayoutViewHitInfo layoutViewHitInfo = this.mediLayoutView1.CalcHitInfo(e.X, e.Y);
            //当未读消息有值后才继续走
            if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"] != null && HISClientHelper.dictryMessBody["未处理"].Any())
            {
                //右侧内容有修改需要通知页面
                if (isSave)
                {
                    readXXBZ = false;
                    gyLayoutViewHitInfo = layoutViewHitInfo;
                    //委托去通知右侧对话框，可以弹出提示框
                    if (XiaoXiSaveDialog != null)
                        XiaoXiSaveDialog.Invoke();
                    return;
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                SetWeiDuXXCK(layoutViewHitInfo);
            }
        }

        /// <summary>
        /// 未读消息处理
        /// </summary>
        /// <param name="layoutViewHitInfo"></param>
        public void SetWeiDuXXCK(LayoutViewHitInfo layoutViewHitInfo)
        {
            //添加了急诊的编码
            //add by meiyuan @2020.7.25
            string[] wjzBM = new string[] { "04001", "10001", "12001", "12005", "10004", "04002", "34001", "34005", "35001", "35004", "10906" };
            if (layoutViewHitInfo != null && layoutViewHitInfo.InCard)
            {
                this.mediLayoutView1.FocusedRowHandle = layoutViewHitInfo.RowHandle;

                this.mediLayoutView1.ActiveBackColor = Color.FromArgb(171, 214, 255);
                HISMessageBody xiaoxXiMessage = this.mediLayoutView1.GetFocusedRow() as HISMessageBody;
                //消息查看处理
                if (xiaoxXiMessage.BaoMiXxBz == 1)
                {
                    using (ChaKanXXRZ rzFrom = new ChaKanXXRZ())
                    {
                        rzFrom.yongHuRZ.ZhiGongGH = HISClientHelper.ZHIGONGGH;
                        rzFrom.ShowDialog();
                        if (!rzFrom.RenZhenJG)
                            return;
                    }
                }
                //加载右边消息主体
                ShowRightNR(xiaoxXiMessage);

                //if (e.Clicks == 2)
                {
                    //改变消息已读状态处理
                    //var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, HISClientHelper.GetSysDate());
                    var xiaoXiData = xiaoXiService.GetYiDuXX_YH(HISClientHelper.ZAIXIANZTID, removeMess, wjzBM.ToList());
                    //bool bol = false;
                    if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
                    {
                        yiDuXX = xiaoXiData.Return;
                        if (!yeMianYDCL.Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).Any())
                        {
                            if (!yiDuXX.Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID && p.XiaoXiBM.SubString(0, 2) == HISClientHelper.KUCUNYYID.SubString(0, 2)).Any())
                            {
                                if (HISClientHelper.XiaoXiRightNR.ContainsKey(xiaoxXiMessage.XiaoXiBM))
                                {
                                    //阅读类消息且没有设置霸屏
                                    if (!wjzBM.Contains(xiaoxXiMessage.XiaoXiBM) && (HISClientHelper.XiaoXiRightNR[xiaoxXiMessage.XiaoXiBM].Split('|')[1] == "0" || string.IsNullOrEmpty(HISClientHelper.XiaoXiRightNR[xiaoxXiMessage.XiaoXiBM].Split('|')[1])))
                                    {
                                        //bol = true;
                                        var yueDuXX = xiaoXiService.YueDuXiaoXi(Convert.ToInt32(xiaoxXiMessage.XiaoXiID), HISClientHelper.USERID);
                                        yeMianYDCL.Add(xiaoxXiMessage);
                                    }
                                }
                            }
                        }
                    }

                    List<HISMessageBody> messBody = yiDuXX;
                    //if (bol)
                    //{
                    //    //重新获取已读消息
                    //    xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, faSongSJ);
                    //}
                    bool bluess = false;
                    if (xiaoXiData.ReturnCode == Enterprise.ReturnCode.SUCCESS)
                    {
                        if (!string.IsNullOrEmpty(xiaoxXiMessage.XiaoXiID))
                        {
                            //messBody = xiaoXiData.Return;
                            //判断选中的消息是否为已读消息
                            //&& p.XiaoXiBM.SubString(0, 2) == HISClientHelper.KUCUNYYID.SubString(0, 2)
                            messBody = messBody.Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).ToList();
                            if (messBody.Count == 0)
                                messBody = yeMianYDCL.Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).ToList();
                            if (messBody.Any() && (!removeMess.Where(p => p.XiaoXiID == xiaoxXiMessage.XiaoXiID).Any() || removeMess.Count <= 1))
                            {
                                bluess = true;
                                removeMess.Add(xiaoxXiMessage);
                                if (removeMess.Count > 0) //(removeMess.Count == 2 || this.mediLayoutView1.RowCount == 1) && 
                                {
                                    removeMess[0].XiaoXiBD = "";
                                    if (HISClientHelper.dictryMessBody.ContainsKey("已处理") && !HISClientHelper.dictryMessBody["已处理"].Where(p => p.XiaoXiID == removeMess[0].XiaoXiID).Any())
                                        HISClientHelper.dictryMessBody["已处理"].Add(removeMess[0]);
                                    else if (!HISClientHelper.dictryMessBody.ContainsKey("已处理"))
                                        HISClientHelper.dictryMessBody.Add("已处理", (new List<HISMessageBody>() { removeMess[0] }));

                                    if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Where(p => p.XiaoXiID == removeMess[0].XiaoXiID).Any())
                                    {
                                        yeMianYDCL.Remove(removeMess[0]);
                                        HISClientHelper.dictryMessBody["未处理"].Remove(removeMess[0]);
                                        HISClientHelper.XIAOXIZS--;
                                    }
                                    else if (!HISClientHelper.dictryMessBody.ContainsKey("未处理"))
                                        HISClientHelper.dictryMessBody.Add("未处理", (new List<HISMessageBody>()));

                                    //removeMess.Clear();
                                    if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Count > 0)
                                    {
                                        this.mediGridControl1.DataSource = HISClientHelper.dictryMessBody["未处理"].OrderByDescending(p => p.FaSongSj).ToList(); ;
                                        this.mediGridControl1.RefreshDataSource();
                                    }
                                    if (layoutViewHitInfo.RowHandle > 0)
                                    {
                                        this.mediLayoutView1.FocusedRowHandle = layoutViewHitInfo.RowHandle - 1;
                                    }
                                    //动态加载消息数量
                                    XXSLEvent?.Invoke(HISClientHelper.dictryMessBody["未处理"].Count);
                                    this.xtraTabPage1.Text = "未处理(" + HISClientHelper.dictryMessBody["未处理"].Count + ")";
                                }
                            }
                        }
                    }

                    //判断移除上一条信息
                    if ((removeMess.Count == 2 || this.mediLayoutView1.RowCount == 1) && bluess)
                    {
                        removeMess[0].XiaoXiBD = "";
                        if (HISClientHelper.dictryMessBody.ContainsKey("已处理") && !HISClientHelper.dictryMessBody["已处理"].Where(p => p.XiaoXiID == removeMess[0].XiaoXiID).Any())
                            HISClientHelper.dictryMessBody["已处理"].Add(removeMess[0]);
                        else if (!HISClientHelper.dictryMessBody.ContainsKey("已处理"))
                            HISClientHelper.dictryMessBody.Add("已处理", (new List<HISMessageBody>() { removeMess[0] }));

                        if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Where(p => p.XiaoXiID == removeMess[0].XiaoXiID).Any())
                        {
                            HISClientHelper.dictryMessBody["未处理"].Remove(removeMess[0]);
                            HISClientHelper.XIAOXIZS--;
                        }
                        else if (!HISClientHelper.dictryMessBody.ContainsKey("未处理"))
                            HISClientHelper.dictryMessBody.Add("未处理", (new List<HISMessageBody>()));
                        if (removeMess.Count > 1)
                        {
                            if (removeMess[0] != removeMess[1])
                                removeMess.RemoveAt(0);
                            else
                                removeMess.Clear();
                            //HISClientHelper.XIAOXIZS--;
                        }

                        if (XXSLEvent != null)
                        {
                            //动态加载消息数量
                            XXSLEvent(HISClientHelper.dictryMessBody["未处理"].Count);
                        }
                        this.xtraTabPage1.Text = "未处理(" + HISClientHelper.dictryMessBody["未处理"].Count + ")";
                        this.mediGridControl1.DataSource = HISClientHelper.dictryMessBody["未处理"].OrderByDescending(p => p.FaSongSj).ToList(); ;
                        this.mediGridControl1.RefreshDataSource();

                        this.mediGridControl2.DataSource = HISClientHelper.dictryMessBody["已处理"].OrderByDescending(p => p.FaSongSj).ToList(); ;
                        this.mediGridControl1.RefreshDataSource();
                        if (layoutViewHitInfo.RowHandle > 0)
                            this.mediLayoutView1.FocusedRowHandle = layoutViewHitInfo.RowHandle - 1;
                    }
                }
            }
        }

        /// <summary>
        /// 已读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLayoutView2_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = this.mediGridControl2.PointToClient(MousePosition);
            LayoutViewHitInfo layoutViewHitInfo = this.mediLayoutView2.CalcHitInfo(mousePos.X, mousePos.Y);
            if (HISClientHelper.dictryMessBody.ContainsKey("已处理") && HISClientHelper.dictryMessBody["已处理"] != null && HISClientHelper.dictryMessBody["已处理"].Any())
            {
                //右侧内容有修改需要通知页面
                if (isSave)
                {
                    readXXBZ = true;
                    gyLayoutViewHitInfo = layoutViewHitInfo;
                    //委托去通知右侧对话框，可以弹出提示框
                    if (XiaoXiSaveDialog != null)
                        XiaoXiSaveDialog.Invoke();
                    return;
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                SetYiDuXXCK(layoutViewHitInfo);
            }
        }

        /// <summary>
        /// 设置已读消息窗口
        /// </summary>
        /// <param name="layoutViewHitInfo"></param>
        private void SetYiDuXXCK(LayoutViewHitInfo layoutViewHitInfo)
        {
            if (layoutViewHitInfo != null && layoutViewHitInfo.InCard)
            {
                //重置停留页面状态
                IsStayPage = false;
                this.mediLayoutView2.FocusedRowHandle = layoutViewHitInfo.RowHandle;

                this.mediLayoutView2.ActiveBackColor = Color.FromArgb(171, 214, 255);
                HISMessageBody xiaoxXiMessage = this.mediLayoutView2.GetFocusedRow() as HISMessageBody;
                ShowRightNR(xiaoxXiMessage);
            }
        }

        /// <summary>
        /// 根据消息显示右侧消息内容
        /// </summary>
        /// <param name="xiaoxXiMessage"></param>
        private void ShowRightNR(HISMessageBody xiaoxXiMessage)
        {
            try
            {
                //add by xxl @2020-09-15 for [HR6-5199]【市二住院急诊】新增处方审核提醒接口，做在消息界面中【三家评审要求功能】
                if (xiaoxXiMessage.XiaoXiBM == "10906")
                {
                    //List<E_GY_YAOPINYZSHXX> jo = JsonConvert.DeserializeObject<List<E_GY_YAOPINYZSHXX>>(xiaoxXiMessage.XiaoXiNR.ToString());
                    //xiaoxXiMessage.XiaoXiNR = jo;
                }
                else
                {
                    //判断消息内容，如果是json将转换
                    if (!string.IsNullOrEmpty(xiaoxXiMessage.XiaoXiNR.ToStringEx()) && JsonSplit.IsJson(xiaoxXiMessage.XiaoXiNR.ToString()))
                    {
                        JObject jo = (JObject)JsonConvert.DeserializeObject(xiaoxXiMessage.XiaoXiNR.ToString());
                        xiaoxXiMessage.XiaoXiNR = jo;
                    }
                }
                //加载匹配窗口
                string from = HISClientHelper.XiaoXiRightNR[xiaoxXiMessage.XiaoXiBM].Split('|')[0];
                if (string.IsNullOrEmpty(from))
                    return;
                string yingYongID = HISClientHelper.KUCUNYYID.Substring(0, 2);
                if (from.Split('.')[2] != "HIS")
                {
                    switch (yingYongID)
                    {
                        case "12":
                            from = from.Replace(from.Split('.')[2], "BQYZ");
                            break;
                        case "34":
                            from = from.Replace(from.Split('.')[2], "BQYZ");
                            break;
                        case "10":
                            from = from.Replace(from.Split('.')[2], "BQYS");
                            break;
                        case "35":
                            from = from.Replace(from.Split('.')[2], "BQYS");
                            break;
                        case "04":
                            from = from.Replace(from.Split('.')[2], "HIS");
                            break;
                    }
                }
                //加载窗体
                Control control = Assembly.Load(from.Substring(0, from.LastIndexOf('.'))).CreateInstance(from) as Control;
                Type type = control.GetType(); //获取类型
                PropertyInfo propertyInfo = type.GetProperty("message"); //获取指定名称的属性
                if (string.IsNullOrEmpty(xiaoxXiMessage.XiaoXiLY))
                {
                    var result = xiaoXiService.GetXiaoXiSJ(xiaoxXiMessage.XiaoXiID);
                    if (result.ReturnCode == ReturnCode.SUCCESS && result.Return != null)
                    {
                        xiaoxXiMessage.XiaoXiLY = result.Return.FirstOrDefault().XIAOXILY;
                    }
                }
                if (propertyInfo != null)
                    propertyInfo.SetValue(control, xiaoxXiMessage, null); //给对应属性赋值
                this.mediPanelControl2.Controls.Clear();
                control.Dock = DockStyle.Fill;
                this.mediPanelControl2.Controls.Add(control);
                currentControl = control;
                control.Tag = this;
            }
            catch (Exception ex)
            {
                MediMsgBox.Show(this, "窗体不存在！");
            }
        }

        private Control GetControl()
        {
            return currentControl;
        }

        /// <summary>
        /// 选择标签页处理数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }

        /// <summary>
        /// 处理字段颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLayoutView1_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
        {
            //设置内容底色
            if (e.Column.FieldName == "XiaoXiBD")
            {
                string XiaoXiJc = this.mediLayoutView1.GetRowCellValue(e.RowHandle, "XiaoXiJc").ToStringEx();
                if (XiaoXiJc.Contains("危急"))
                    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
                else if (XiaoXiJc.Contains("通知"))
                    e.Appearance.ForeColor = Color.FromArgb(204, 204, 204);
                else
                    e.Appearance.ForeColor = Color.FromArgb(255, 153, 000);
            }
            if (e.Column.FieldName == "XiaoXiJc")
            {
                e.Appearance.BackColor = Color.FromArgb(255, 153, 000);
            }
            if (e.Column.FieldName == "XiaoXiMc")
            {
                string XiaoXiMc = this.mediLayoutView1.GetRowCellValue(e.RowHandle, "XiaoXiMc").ToStringEx();
                if (XiaoXiMc == "危急")
                {
                    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
                }
            }
        }

        /// <summary>
        /// 计算宽度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="layoutViewField"></param>
        private void LayOutPaint(string value, DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField)
        {
            MediLabel lab = new MediLabel();
            lab.Text = value;
            Font f = lab.Font;
            Graphics g = this.CreateGraphics();
            SizeF z = g.MeasureString(lab.Text, f);
            layoutViewField.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutViewField.MinSize = new System.Drawing.Size() { Height = Convert.ToInt32(z.Height), Width = Convert.ToInt32(z.Width) };
            layoutViewField.MaxSize = new System.Drawing.Size() { Height = Convert.ToInt32(z.Height), Width = Convert.ToInt32(z.Width) };
        }

        /// <summary>
        /// 处理字段颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLayoutView2_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
        {
            //设置内容底色
            if (e.Column.FieldName == "XiaoXiBD")
            {
                string XiaoXiJc = this.mediLayoutView2.GetRowCellValue(e.RowHandle, "XiaoXiJc").ToStringEx();
                if (XiaoXiJc.Contains("危急"))
                    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
                else if (XiaoXiJc.Contains("通知"))
                    e.Appearance.ForeColor = Color.FromArgb(204, 204, 204);
                else
                    e.Appearance.ForeColor = Color.FromArgb(255, 153, 000);
            }
            if (e.Column.FieldName == "XiaoXiJc")
            {
                e.Appearance.BackColor = Color.FromArgb(255, 153, 000);
            }
            if (e.Column.FieldName == "XiaoXiMc")
            {
                string XiaoXiMc = this.mediLayoutView2.GetRowCellValue(e.RowHandle, "XiaoXiMc").ToStringEx();
                if (XiaoXiMc == "危急")
                {
                    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonFaSongXX_Click(object sender, EventArgs e)
        {
            mediTabControl2.SelectedTabPageIndex = 2;
            HISMessageBody xiaoxXiMessage = new HISMessageBody();
            //写邮件的时候消息编码固定为此，用于读取其中的xiaoxiclck信息，用于显示到右侧
            xiaoxXiMessage.XiaoXiBM = HISClientHelper.KUCUNYYID.Substring(0, 2) + "0001";
            ShowRightNR(xiaoxXiMessage);
        }

        private void mediLayoutView3_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = this.mediGridControl3.PointToClient(MousePosition);
            LayoutViewHitInfo layoutViewHitInfo = this.mediLayoutView3.CalcHitInfo(mousePos.X, mousePos.Y);
            if (e.Button == MouseButtons.Left)
            {
                if (layoutViewHitInfo != null && layoutViewHitInfo.InCard)
                {
                    this.mediLayoutView3.FocusedRowHandle = layoutViewHitInfo.RowHandle;

                    this.mediLayoutView3.ActiveBackColor = Color.FromArgb(171, 214, 255);
                    HISMessageBody xiaoxXiMessage = this.mediLayoutView3.GetFocusedRow() as HISMessageBody;
                    ShowRightNR(xiaoxXiMessage);
                }
            }
        }

        private void mediTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Text.Contains("已处理"))
            {
                if (HISClientHelper.dictryMessBody.ContainsKey("已处理") && HISClientHelper.dictryMessBody["已处理"].Count > 0)
                {
                    IList<HISMessageBody> yiChuLi = HISClientHelper.dictryMessBody["已处理"];
                    this.mediGridControl2.DataSource = yiChuLi.Where(p => p.FaSongSj >= DateTime.Now.AddDays(-7)).OrderByDescending(p => p.FaSongSj).ToList();
                    this.mediGridControl2.RefreshDataSource();
                    moRenJZ("已处理");
                }
            }
            else if (e.Page.Text.Contains("未处理"))
            {
                if (HISClientHelper.dictryMessBody.ContainsKey("未处理") && HISClientHelper.dictryMessBody["未处理"].Count > 0)
                {
                    this.mediGridControl1.DataSource = HISClientHelper.dictryMessBody["未处理"].OrderByDescending(p => p.FaSongSj).ToList();
                    this.mediGridControl1.RefreshDataSource();
                    moRenJZ("未处理");
                }
            }
            else if (e.Page.Text.Contains("全部消息"))
            {
                //加载全部消息
                List<HISMessageBody> list = new List<HISMessageBody>();
                if (HISClientHelper.dictryMessBody.ContainsKey("已处理"))
                    list.AddRange(HISClientHelper.dictryMessBody["已处理"]);
                if (HISClientHelper.dictryMessBody.ContainsKey("未处理"))
                    list.AddRange(HISClientHelper.dictryMessBody["未处理"]);
                this.xtraTabPage4.Text = "全部消息(" + list.Count + ")";
                this.mediGridControl4.DataSource = list.OrderByDescending(p => p.FaSongSj).ToList();
            }
            else
            {
                //var result = xiaoXiService.GetMyMessage(HISClientHelper.USERID);
                //if (result.ReturnCode != ReturnCode.SUCCESS)
                //{
                //    MediMsgBox.Failure(this, "获取我的消息失败，请重试！");
                //    return;
                //}
                //if (result.Return != null)
                //{
                //    var myMessages = result.Return.Where(p => p.XiaoXiLY == "0");
                //    this.mediGridControl3.DataSource = myMessages;
                //    this.mediGridControl3.RefreshDataSource();
                //}
            }
            //如果停留在本页面则索引重新赋值
            if (IsStayPage)
            {
                mediTabControl2.SelectedTabPageIndex = 1;
            }
        }

        private void mediButtonFaSongXX_MouseHover(object sender, EventArgs e)
        {
            //this.mediButtonFaSongXX.ImageOptions.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.add_message_hover;
        }

        private void mediButtonFaSongXX_MouseLeave(object sender, EventArgs e)
        {
            //this.mediButtonFaSongXX.ImageOptions.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.add_message;
        }

        private void mediLayoutView3_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
        {
            if (e.Column.FieldName == "XiaoXiBD")
            {
                string XiaoXiJc = this.mediLayoutView1.GetRowCellValue(e.RowHandle, "XiaoXiJc").ToStringEx();
                if (XiaoXiJc.Contains("危急"))
                    e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
                else if (XiaoXiJc.Contains("通知"))
                    e.Appearance.ForeColor = Color.FromArgb(204, 204, 204);
                else
                    e.Appearance.ForeColor = Color.FromArgb(255, 153, 000);

            }
            if (e.Column.FieldName == "XiaoXiJc")
            {
                e.Appearance.BackColor = Color.FromArgb(255, 153, 000);
            }
            if (e.Column.FieldName == "XiaoXiMc")
            {
                e.Appearance.ForeColor = Color.FromArgb(248, 024, 038);
            }
        }

        private void mediSearchControl1_TextChanged(object sender, EventArgs e)
        {
            var inputText = mediSearchControl1.Text;//输入文本
            if (string.IsNullOrEmpty(inputText))
                return;

            List<HISMessageBody> list = new List<HISMessageBody>();

            if (this.mediTabControl2.SelectedTabPageIndex == 0)
            {
                list = this.mediGridControl4.DataSource as List<HISMessageBody>;

                if (list == null)
                    return;

                for (int i = 0; i < this.mediLayoutView4.RowCount; i++)
                {
                    string shouJianRXM = this.mediLayoutView4.GetRowCellValue(i, "XiaoXiBT").ToStringEx();
                    if (shouJianRXM.Contains(inputText))
                    {
                        this.mediLayoutView4.FocusedRowHandle = i;
                        this.mediLayoutView4.ShowEditor();
                        HISMessageBody hISMessage = this.mediLayoutView4.GetFocusedRow() as HISMessageBody;
                        ShowRightNR(hISMessage);
                        return;
                    }
                }
            }
            else if (this.mediTabControl2.SelectedTabPageIndex == 1)
            {
                list = this.mediGridControl1.DataSource as List<HISMessageBody>;

                if (list == null)
                    return;

                for (int i = 0; i < this.mediLayoutView1.RowCount; i++)
                {
                    string shouJianRXM = this.mediLayoutView1.GetRowCellValue(i, "XiaoXiBT").ToStringEx();
                    if (shouJianRXM.Contains(inputText))
                    {
                        this.mediLayoutView1.FocusedRowHandle = i;
                        this.mediLayoutView1.ActiveBackColor = Color.FromArgb(171, 214, 255);
                        HISMessageBody hISMessage = this.mediLayoutView1.GetFocusedRow() as HISMessageBody;
                        ShowRightNR(hISMessage);
                        return;
                    }
                }
            }
            else if (this.mediTabControl2.SelectedTabPageIndex == 2)
            {
                if (HISClientHelper.dictryMessBody["未处理"] == null && !HISClientHelper.dictryMessBody["未处理"].Any())
                    return;

                for (int i = 0; i < this.mediLayoutView2.RowCount; i++)
                {
                    string shouJianRXM = this.mediLayoutView2.GetRowCellValue(i, "XiaoXiBT").ToStringEx();
                    if (shouJianRXM.Contains(inputText))
                    {
                        this.mediLayoutView2.FocusedRowHandle = i;
                        this.mediLayoutView2.ShowEditor();
                        HISMessageBody hISMessage = this.mediLayoutView2.GetFocusedRow() as HISMessageBody;
                        ShowRightNR(hISMessage);
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// 已读数据筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HISClientHelper.dictryMessBody.ContainsKey("已处理"))
            {
                IList<HISMessageBody> list = HISClientHelper.dictryMessBody["已处理"];
                if (this.mediRadioGroup1.SelectedIndex == 0)
                {
                    this.mediGridControl2.DataSource = list.Where(p => p.FaSongSj >= DateTime.Now.AddDays(-7)).OrderByDescending(m => m.FaSongSj);
                }
                else if (this.mediRadioGroup1.SelectedIndex == 1)
                {
                    DateTime fssj = HISClientHelper.GetSysDate().AddDays(-30);
                    QiTaSJCX(fssj);
                }
                else
                {
                    DateTime fssj = HISClientHelper.GetSysDate().AddDays(-90);
                    QiTaSJCX(fssj);
                }
                moRenJZ("已处理");
            }
        }

        /// <summary>
        /// 其他数据检索
        /// </summary>
        private void QiTaSJCX(DateTime fssj)
        {
            AsynWaitForm.CreateWaitForm(this, string.Empty, string.Empty);
            AsynWaitForm.SetContent("加载数据中，请稍后……");
            AsynWaitForm.SetPercentVisible(false);

            var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, fssj);
            if (xiaoXiData.ReturnCode == ReturnCode.SUCCESS)
            {
                this.mediGridControl2.DataSource = xiaoXiData.Return.OrderByDescending(m => m.FaSongSj);
            }

            AsynWaitForm.CloseWaitForm();
        }

        #region override

        /// <summary>
        /// 创建控件句柄
        /// </summary>
        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                try
                {
                    base.CreateHandle();
                }
                catch
                {

                }
                finally
                {
                    if (!IsHandleCreated)
                    {
                        // 强制重新创建控件的句柄
                        base.RecreateHandle();
                    }
                }
            }
        }

        #endregion
    }
    #region 判断JSON是否合法

    internal class JsonSplit
    {
        private static bool IsJsonStart(ref string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                json = json.Trim('\r', '\n', ' ');
                if (json.Length > 1)
                {
                    char s = json[0];
                    char e = json[json.Length - 1];
                    return (s == '{' && e == '}') || (s == '[' && e == ']');
                }
            }
            return false;
        }
        internal static bool IsJson(string json)
        {
            int errIndex;
            return IsJson(json, out errIndex);
        }
        internal static bool IsJson(string json, out int errIndex)
        {
            errIndex = 0;
            if (IsJsonStart(ref json))
            {
                CharState cs = new CharState();
                char c;
                for (int i = 0; i < json.Length; i++)
                {
                    c = json[i];
                    if (SetCharState(c, ref cs) && cs.childrenStart)//设置关键符号状态。
                    {
                        string item = json.Substring(i);
                        int err;
                        int length = GetValueLength(item, true, out err);
                        cs.childrenStart = false;
                        if (err > 0)
                        {
                            errIndex = i + err;
                            return false;
                        }
                        i = i + length - 1;
                    }
                    if (cs.isError)
                    {
                        errIndex = i;
                        return false;
                    }
                }

                return !cs.arrayStart && !cs.jsonStart;
            }
            return false;
        }

        /// <summary>
        /// 获取值的长度（当Json值嵌套以"{"或"["开头时）
        /// </summary>
        private static int GetValueLength(string json, bool breakOnErr, out int errIndex)
        {
            errIndex = 0;
            int len = 0;
            if (!string.IsNullOrEmpty(json))
            {
                CharState cs = new CharState();
                char c;
                for (int i = 0; i < json.Length; i++)
                {
                    c = json[i];
                    if (!SetCharState(c, ref cs))//设置关键符号状态。
                    {
                        if (!cs.jsonStart && !cs.arrayStart)//json结束，又不是数组，则退出。
                        {
                            break;
                        }
                    }
                    else if (cs.childrenStart)//正常字符，值状态下。
                    {
                        int length = GetValueLength(json.Substring(i), breakOnErr, out errIndex);//递归子值，返回一个长度。。。
                        cs.childrenStart = false;
                        cs.valueStart = 0;
                        //cs.state = 0;
                        i = i + length - 1;
                    }
                    if (breakOnErr && cs.isError)
                    {
                        errIndex = i;
                        return i;
                    }
                    if (!cs.jsonStart && !cs.arrayStart)//记录当前结束位置。
                    {
                        len = i + 1;//长度比索引+1
                        break;
                    }
                }
            }
            return len;
        }
        /// <summary>
        /// 字符状态
        /// </summary>
        private class CharState
        {
            internal bool jsonStart = false;//以 "{"开始了...
            internal bool setDicValue = false;// 可以设置字典值了。
            internal bool escapeChar = false;//以"\"转义符号开始了
            /// <summary>
            /// 数组开始【仅第一开头才算】，值嵌套的以【childrenStart】来标识。
            /// </summary>
            internal bool arrayStart = false;//以"[" 符号开始了
            internal bool childrenStart = false;//子级嵌套开始了。
            /// <summary>
            /// 【0 初始状态，或 遇到“,”逗号】；【1 遇到“：”冒号】
            /// </summary>
            internal int state = 0;

            /// <summary>
            /// 【-1 取值结束】【0 未开始】【1 无引号开始】【2 单引号开始】【3 双引号开始】
            /// </summary>
            internal int keyStart = 0;
            /// <summary>
            /// 【-1 取值结束】【0 未开始】【1 无引号开始】【2 单引号开始】【3 双引号开始】
            /// </summary>
            internal int valueStart = 0;
            internal bool isError = false;//是否语法错误。

            internal void CheckIsError(char c)//只当成一级处理（因为GetLength会递归到每一个子项处理）
            {
                if (keyStart > 1 || valueStart > 1)
                {
                    return;
                }
                //示例 ["aa",{"bbbb":123,"fff","ddd"}] 
                switch (c)
                {
                    case '{':
                        isError = jsonStart && state == 0;//重复开始错误 同时不是值处理。
                        break;
                    case '}':
                        isError = !jsonStart || (keyStart != 0 && state == 0);//重复结束错误 或者 提前结束{"aa"}。正常的有{}
                        break;
                    case '[':
                        isError = arrayStart && state == 0;//重复开始错误
                        break;
                    case ']':
                        isError = !arrayStart || jsonStart;//重复开始错误 或者 Json 未结束
                        break;
                    case '"':
                    case '\'':
                        isError = !(jsonStart || arrayStart); //json 或数组开始。
                        if (!isError)
                        {
                            //重复开始 [""",{"" "}]
                            isError = (state == 0 && keyStart == -1) || (state == 1 && valueStart == -1);
                        }
                        if (!isError && arrayStart && !jsonStart && c == '\'')//['aa',{}]
                        {
                            isError = true;
                        }
                        break;
                    case ':':
                        isError = !jsonStart || state == 1;//重复出现。
                        break;
                    case ',':
                        isError = !(jsonStart || arrayStart); //json 或数组开始。
                        if (!isError)
                        {
                            if (jsonStart)
                            {
                                isError = state == 0 || (state == 1 && valueStart > 1);//重复出现。
                            }
                            else if (arrayStart)
                            {
                                isError = keyStart == 0 && !setDicValue;
                            }
                        }
                        break;
                    case ' ':
                    case '\r':
                    case '\n':
                    case '\0':
                    case '\t':
                        break;
                    default: //值开头。。
                        isError = (!jsonStart && !arrayStart) || (state == 0 && keyStart == -1) || (valueStart == -1 && state == 1);//
                        break;
                }
            }
        }
        /// <summary>
        /// 设置字符状态(返回true则为关键词，返回false则当为普通字符处理）
        /// </summary>
        private static bool SetCharState(char c, ref CharState cs)
        {
            cs.CheckIsError(c);
            switch (c)
            {
                case '{':
                    #region 大括号
                    if (cs.keyStart <= 0 && cs.valueStart <= 0)
                    {
                        cs.keyStart = 0;
                        cs.valueStart = 0;
                        if (cs.jsonStart && cs.state == 1)
                        {
                            cs.childrenStart = true;
                        }
                        else
                        {
                            cs.state = 0;
                        }
                        cs.jsonStart = true;//开始。
                        return true;
                    }
                    #endregion
                    break;
                case '}':
                    #region 大括号结束
                    if (cs.keyStart <= 0 && cs.valueStart < 2 && cs.jsonStart)
                    {
                        cs.jsonStart = false;//正常结束。
                        cs.state = 0;
                        cs.keyStart = 0;
                        cs.valueStart = 0;
                        cs.setDicValue = true;
                        return true;
                    }
                    #endregion
                    break;
                case '[':
                    #region 中括号开始
                    if (!cs.jsonStart)
                    {
                        cs.arrayStart = true;
                        return true;
                    }
                    else if (cs.jsonStart && cs.state == 1)
                    {
                        cs.childrenStart = true;
                        return true;
                    }
                    #endregion
                    break;
                case ']':
                    #region 中括号结束
                    if (cs.arrayStart && !cs.jsonStart && cs.keyStart <= 2 && cs.valueStart <= 0)//[{},333]//这样结束。
                    {
                        cs.keyStart = 0;
                        cs.valueStart = 0;
                        cs.arrayStart = false;
                        return true;
                    }
                    #endregion
                    break;
                case '"':
                case '\'':
                    #region 引号
                    if (cs.jsonStart || cs.arrayStart)
                    {
                        if (cs.state == 0)//key阶段,有可能是数组["aa",{}]
                        {
                            if (cs.keyStart <= 0)
                            {
                                cs.keyStart = (c == '"' ? 3 : 2);
                                return true;
                            }
                            else if ((cs.keyStart == 2 && c == '\'') || (cs.keyStart == 3 && c == '"'))
                            {
                                if (!cs.escapeChar)
                                {
                                    cs.keyStart = -1;
                                    return true;
                                }
                                else
                                {
                                    cs.escapeChar = false;
                                }
                            }
                        }
                        else if (cs.state == 1 && cs.jsonStart)//值阶段必须是Json开始了。
                        {
                            if (cs.valueStart <= 0)
                            {
                                cs.valueStart = (c == '"' ? 3 : 2);
                                return true;
                            }
                            else if ((cs.valueStart == 2 && c == '\'') || (cs.valueStart == 3 && c == '"'))
                            {
                                if (!cs.escapeChar)
                                {
                                    cs.valueStart = -1;
                                    return true;
                                }
                                else
                                {
                                    cs.escapeChar = false;
                                }
                            }

                        }
                    }
                    #endregion
                    break;
                case ':':
                    #region 冒号
                    if (cs.jsonStart && cs.keyStart < 2 && cs.valueStart < 2 && cs.state == 0)
                    {
                        if (cs.keyStart == 1)
                        {
                            cs.keyStart = -1;
                        }
                        cs.state = 1;
                        return true;
                    }
                    #endregion
                    break;
                case ',':
                    #region 逗号 //["aa",{aa:12,}]

                    if (cs.jsonStart)
                    {
                        if (cs.keyStart < 2 && cs.valueStart < 2 && cs.state == 1)
                        {
                            cs.state = 0;
                            cs.keyStart = 0;
                            cs.valueStart = 0;
                            cs.setDicValue = true;
                            return true;
                        }
                    }
                    else if (cs.arrayStart && cs.keyStart <= 2)
                    {
                        cs.keyStart = 0;
                        return true;
                    }
                    #endregion
                    break;
                case ' ':
                case '\r':
                case '\n':
                case '\0':
                case '\t':
                    if (cs.keyStart <= 0 && cs.valueStart <= 0) //cs.jsonStart && 
                    {
                        return true;//跳过空格。
                    }
                    break;
                default: //值开头。。
                    if (c == '\\') //转义符号
                    {
                        if (cs.escapeChar)
                        {
                            cs.escapeChar = false;
                        }
                        else
                        {
                            cs.escapeChar = true;
                            return true;
                        }
                    }
                    else
                    {
                        cs.escapeChar = false;
                    }
                    if (cs.jsonStart || cs.arrayStart) // Json 或数组开始了。
                    {
                        if (cs.keyStart <= 0 && cs.state == 0)
                        {
                            cs.keyStart = 1;//无引号的
                        }
                        else if (cs.valueStart <= 0 && cs.state == 1 && cs.jsonStart)//只有Json开始才有值。
                        {
                            cs.valueStart = 1;//无引号的
                        }
                    }
                    break;
            }
            return false;
        }
    }

    #endregion
}
