using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class SwitchSystemForm : MediForm
    {
        private JCJGLoginService loginService;
        
        private List<E_GY_YONGHUYY_EX> yingYongList;

        public SwitchSystemForm()
        {
            InitializeComponent();
            loginService = new JCJGLoginService();
        }

        private void SwitchSystemForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(470,250);
            //获取可用网卡信息
            var networkList = NetworkHeler.GetAvailableNetwork();
            //查询登录员工可用应用
            var ret = loginService.GetYongHuXByGH(HISClientHelper.USERID, networkList);
            if (ret.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                MediMsgBox.Failure(this, ret);
                DialogResult = DialogResult.Cancel;
                return;
            }
            yingYongList = ret.Return.YingYongList.OrderBy(y => y.YINGYONGID).ToList();
            //剔除当前登录应用
            yingYongList.RemoveAll(y => y.YINGYONGID == HISClientHelper.YINGYONGID);

            eGYYONGHUYYEXBindingSource.DataSource = yingYongList;
        }

        /// <summary>
        /// 鼠标移动显示应用名称
        /// </summary>
        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != mediGridControl1) return;

            ToolTipControlInfo info = null;
            MediLayoutView view = mediGridControl1.GetViewAt(e.ControlMousePosition) as MediLayoutView;
            if (view == null) return;

            LayoutViewHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            if (hi.RowHandle > -1)
            {
                info = new ToolTipControlInfo()
                {
                    Object = hi.RowHandle,
                    Text = yingYongList[hi.RowHandle].YINGYONGMC
                };
            }
            if (info != null)
                e.Info = info;
        }

        /// <summary>
        /// 绑定显示应用图片
        /// </summary>
        private void mediLayoutView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image")
            {
                var item = yingYongList[e.ListSourceRowIndex];
                if (!string.IsNullOrWhiteSpace(item.TUPIANLJ) && File.Exists(item.TUPIANLJ))
                {
                    e.Value = Image.FromFile(item.TUPIANLJ);
                }
            }
        }


        private void mediLayoutView1_CardClick(object sender, DevExpress.XtraGrid.Views.Layout.Events.CardClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                var yingYong = yingYongList[e.RowHandle];
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "Mediinfo.WinForm.HIS.Starter.exe"),
                    Arguments = $"SwitchSystem {HISClientHelper.USERID} {HISClientHelper.USERPWD} {yingYong.YINGYONGID}",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized,
                    WorkingDirectory = Directory.GetParent(Application.StartupPath).FullName,
                    UseShellExecute = false
                };

                if (Environment.OSVersion.Version.Major >= 6)
                {
                    Process.Start(processStartInfo);
                }
                else
                {
                    //兼容xp
                    WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, processStartInfo.Arguments, processStartInfo.WorkingDirectory, 11);
                }

                //关闭当前进程
                Process.GetCurrentProcess().Kill();
            }

        }

    }
}
