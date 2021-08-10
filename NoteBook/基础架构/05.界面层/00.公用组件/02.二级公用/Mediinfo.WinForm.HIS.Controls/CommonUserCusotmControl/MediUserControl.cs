using DevExpress.XtraEditors;

using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 用户自定义控价基类
    /// </summary>
    public partial class MediUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediUserControl()
        {
            InitializeComponent();
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        /// <summary>
        /// 窗口调用参数
        /// </summary>
        [Browsable(false)]
        public string DiaoYongCS { get; set; }

        /// <summary>
        /// 自定义控件列表（按钮）
        /// </summary>
        protected List<Control> controlList = new List<Control>();

        /// <summary>
        /// 窗口自定义列表
        /// </summary>
        protected List<E_GY_CHUANGKOUZY_NEW> chuangKouZYList;

        /// <summary>
        /// 用户权限信息
        /// </summary>
        protected List<E_GY_JUESECKQX_NEW> yongHuQXList;
        private void MediUserControl_Load(object sender, EventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (HISClientHelper.USERID == "DBA")
                    RecursionControl(null);

                LoadChuangKouZY();
            }
        }

        /// <summary>
        /// 设置Panel的双击事件
        /// </summary>
        private void RecursionControl(Control parentCtrl)
        {
            if (parentCtrl == null)
                parentCtrl = this;

            if (parentCtrl.Controls.Count == 0) return;

            foreach (Control ctr in parentCtrl.Controls)
            {
                if (ctr is MediTitleBar || ctr is MediPanelControl)
                {
                    ctr.MouseDoubleClick -= Ctr_MouseDoubleClick;
                    ctr.MouseDoubleClick += Ctr_MouseDoubleClick;
                }
                if (ctr.Controls.Count <= 0)
                {
                    if (ctr is BaseButton)
                    {
                        if (controlList.Where(o => o.Name == ctr.Name).ToList().Count > 0)
                            controlList.Remove(controlList.Where(o => o.Name == ctr.Name).FirstOrDefault());
                        controlList.Add(ctr as SimpleButton);
                    }
                }
                else
                {
                    RecursionControl(ctr);
                }
            }
        }

        private void Ctr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 判断按钮个数决定是否弹出按钮权限窗体
            int medButtonCount = 0;
            if (sender is Control)
                foreach (var item in ((Control)sender).Controls)
                    if (item is MediButton)
                        medButtonCount++;
            // 如果当前窗体是跳过Gridcontrol则
            if (sender is MediGridControl || medButtonCount < 1)
                return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // 在不存在的情况下才遍历界面上的按钮
                if (controlList.Count() <= 0)
                    RecursionMediButton(this.Controls);

                // 弹出管理界面
                //MediXiTongFZForm form = new MediXiTongFZForm(this.GetType().Namespace, this.Name, controlList, GongNengId);

                using (FrmButtonSetting form = new FrmButtonSetting(this.GetType().Namespace, this.Name, controlList, ""))
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        GYChuangKouZYHelper.RefreshChuangKouInfo(this.GetType().Namespace, this.Name);
                    }
                }
            }
        }

        /// <summary>
        ///  加载自定义窗口按钮
        /// </summary>
        protected void LoadChuangKouZY()
        {
            if (SkinCat.Instance.IsDesignMode) return;

            if (this.FindForm() == null) return;
            // RecursionUnboundExpressionControls(this.FindForm());

            // 1.获取按钮控件信息
            chuangKouZYList = GYChuangKouZYHelper.GetByForm(this.GetType().Namespace, this.Name);
            if (chuangKouZYList == null || chuangKouZYList.Count()==0)
                return;
            // 需要控制的情况下才遍历查找按钮
            if (chuangKouZYList.Where(c => c.XIANSHIKZ == 1 || c.QUANXIANKZ == 1).Count() > 0)
            {
                RecursionMediButton(this.Controls);
            }

            // 获取权限
            List<string> quanXian = new List<string>();
            foreach (var item in controlList)
            {
                quanXian.Add(string.Format("{0}.{1}.{2}", this.GetType().Namespace, this.Name, item.Name));
            }
            var quanXianDict = GYQuanXianHelper.GetQuanXian(quanXian);

            // 设置按钮的属性

            // 3.获取用户权限信息
            yongHuQXList = GYQuanXianHelper.GetJueSeYHQX();
            if (yongHuQXList == null)
                return;
            foreach (var jsyhqxbutton in yongHuQXList)
            {
                Control[] jsqxctr = this.FindForm().Controls.Find(jsyhqxbutton.CONTROLNAME, true);
                foreach (var item in jsqxctr)
                {
                    if (item is MediButton)
                    {
                        // 文字
                        if (!jsyhqxbutton.WENZI.IsNullOrWhiteSpace())
                            item.Text = jsyhqxbutton.WENZI;

                        // 显示标志
                        if (jsyhqxbutton.XIANSHIBZ == 1)
                            item.Visible = true;
                        else
                            item.Visible = false;
                    }
                }
            }

            foreach (Control item in controlList)
            {
                var chuangKouZY = chuangKouZYList.Where(c => c.CONTROLNAME == item.Name).FirstOrDefault();

                if (chuangKouZY == null)
                    continue;

                if (chuangKouZY.XIANSHIKZ == 1)
                {

                    SimpleButton sbtn = (SimpleButton)item;

                    foreach (var jsyhqxbutton in yongHuQXList)
                    {
                        if (sbtn.Name == jsyhqxbutton.CONTROLNAME)
                        {
                            // 文字
                            if (!chuangKouZY.WENZI.IsNullOrWhiteSpace())
                                sbtn.Text = jsyhqxbutton.WENZI;

                            // 显示标志
                            if (jsyhqxbutton.XIANSHIBZ == 1)
                                sbtn.Visible = true;
                            else
                                sbtn.Visible = false;
                        }
                    }

                    // 颜色
                    if (!chuangKouZY.YANSE.IsNullOrWhiteSpace())
                    {
                        sbtn.Appearance.Options.UseForeColor = true;
                        Regex regex = new Regex(@"RGB\(\d*\,\d*,\d*\)");
                        if (regex.IsMatch(chuangKouZY.YANSE))
                        {
                            string[] rgbstrings = regex.Match(chuangKouZY.YANSE).Value.Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                            int r = Convert.ToInt32(rgbstrings[1]);

                            int g = Convert.ToInt32(rgbstrings[2]);

                            int b = Convert.ToInt32(rgbstrings[3]);
                            sbtn.Appearance.ForeColor = Color.FromArgb(r, g, b);
                        }

                    }

                    // 字体大小
                    if (chuangKouZY.ZITIDX.ToInt(2) != 2)
                        sbtn.Font = new Font("微软雅黑", float.Parse(chuangKouZY.ZITIDX.ToInt(9).ToString()));
                }

                if (chuangKouZY.QUANXIANKZ == 1)
                    item.Enabled = quanXianDict.ContainsKey(string.Format("{0}.{1}.{2}", this.GetType().Namespace, this.Name, item.Name));
            }
        }

        /// <summary>
        /// 递归遍历界面所有的Button按钮
        /// </summary>
        /// <param name="controls"></param>
        private void RecursionMediButton(Control.ControlCollection controls)
        {
            if (controls.Count == 0) return;

            foreach (Control ctl in controls)
            {
                if (ctl.Controls.Count > 0)
                {
                    RecursionMediButton(ctl.Controls);
                }
                else if (ctl is BaseButton)
                {
                    if (controlList.Where(o => o.Name == ctl.Name).ToList().Count > 0)
                        controlList.Remove(controlList.Where(o => o.Name == ctl.Name).FirstOrDefault());
                    controlList.Add(ctl as SimpleButton);
                }
            }
        }
    }
}