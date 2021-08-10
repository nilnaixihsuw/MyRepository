using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.Utility.Extensions;
using Mediinfo.Utility.Util;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LogHelper = Mediinfo.Enterprise.Log.LogHelper;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class ServerConnectTestForm : MediForm
    {
        private JCJGLoginService loginService = null;
        private List<NetworkConfig> NetworkList;
        private string exceptionInfo;

        public ServerConnectTestForm()
        {
            InitializeComponent();
        }

        public Result<LoginDTO> result { get; set; }

        /// <summary>
        /// 连接重试窗口
        /// </summary>
        /// <param name="networkList"></param>
        /// <param name="exceptionstr"></param>
        public ServerConnectTestForm(List<NetworkConfig> networkList, string exceptionstr)
        {
            InitializeComponent();
            loginService = new JCJGLoginService();
            NetworkList = networkList;
            exceptionInfo = exceptionstr;
        }

        private void ServerConnectTestForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(exceptionInfo))
            {
                this.mediLayoutControl1.MaximumSize = new Size(this.mediLayoutControl1.Width, 115);
                this.MaximumSize = new Size(this.Width, 115);
            }
            else
            {
                this.exceptionMemoEdit.Text = exceptionInfo;
                this.mediLayoutControl1.MaximumSize = new Size(this.mediLayoutControl1.Width, 115);
                this.MaximumSize = new Size(this.Width, 115);
            }
        }

        /// <summary>
        /// 重试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repeatMediBtn_Click(object sender, EventArgs e)
        {
            serverConnectTestWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitMediBtn_Click(object sender, EventArgs e)
        {
            this.serverConnectTestWorker.CancelAsync();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void mediCustomProgressBar1_Load(object sender, EventArgs e)
        {

        }

        private void serverConnectTestWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void serverConnectTestWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void serverConnectTestWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.repeatMediBtn.Enabled = false;
                this.mediCustomProgressBar1.ShowCaption = true;
                this.mediCustomProgressBar1.UploadCountDes = "正在连接服务器...";
                this.mediCustomProgressBar1.Refresh();
                Thread.Sleep(300);
            }));
            Result<LoginDTO> loginResult = null;
            try
            {
                loginResult = loginService.GetYongHuXByGH(HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH, NetworkList);

                if (loginResult != null && loginResult.ReturnCode == Enterprise.ReturnCode.SUCCESS)
                {
                    result = loginResult;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }));
                }
                else
                {
                    if (loginResult != null)
                    {
                        exceptionInfo = loginResult.ExceptionContent;
                        //记录日志=====================================================================
                        var network = NetworkHeler.GetAvailableNetwork()[0];

                        SysLogEntity logEntity = new SysLogEntity();
                        logEntity.RiZhiID = Guid.NewGuid().ToString();
                        logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                        logEntity.RiZhiBt = "测试服务器连接状态时发生错误";
                        logEntity.RiZhiNr = loginResult.ExceptionContent;

                        logEntity.FuWuMc = "";
                        logEntity.QingQiuLy = HISClientHelper.DANGQIANCKMC;
                        //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
                        logEntity.RiZhiLx = 2;
                        logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
                        logEntity.XITONGID = HISClientHelper.XITONGID;
                        logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
                        logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
                        logEntity.VERSION = HISClientHelper.VERSION;
                        logEntity.IP = network.Ip;
                        logEntity.MAC = network.PhysicalAddress;
                        logEntity.COMPUTERNAME = network.ComputerName;
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
                        //eSLog.PutLog(logEntity);
                        LogHelper.Intance.PutSysErrorLog(logEntity);
                    }

                    //记录日志=====================================================================

                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.mediCustomProgressBar1.ShowCaption = false;
                        this.mediCustomProgressBar1.UploadCountDes = "服务器连接失败...";
                        this.repeatMediBtn.Enabled = true;
                        this.serverConnectTestWorker.CancelAsync();
                        this.mediCustomProgressBar1.Refresh();
                    }));

                }
            }
            catch (Exception ex)
            {
                exceptionInfo = ex.ToString();
                //记录日志=====================================================================
                var network = NetworkHeler.GetAvailableNetwork()[0];
                //ESLog eSLog = new ESLog();
                SysLogEntity logEntity = new SysLogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.RiZhiBt = "测试服务器连接状态时发生错误";
                logEntity.RiZhiNr = ex.ToString() + "\r\nJson日志：" + JsonUtil.SerializeObject(ex);

                logEntity.FuWuMc = "";
                logEntity.QingQiuLy = HISClientHelper.DANGQIANCKMC;
                //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
                logEntity.RiZhiLx = 2;
                logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
                logEntity.XITONGID = HISClientHelper.XITONGID;
                logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
                logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
                logEntity.VERSION = HISClientHelper.VERSION;
                logEntity.IP = network.Ip;
                logEntity.MAC = network.PhysicalAddress;
                logEntity.COMPUTERNAME = network.ComputerName;
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
                //eSLog.PutLog(logEntity);
                LogHelper.Intance.PutSysErrorLog(logEntity);
                //记录日志=====================================================================

                this.Invoke(new MethodInvoker(delegate
                {
                    this.mediCustomProgressBar1.ShowCaption = false;
                    this.mediCustomProgressBar1.UploadCountDes = "服务器连接失败...";
                    this.repeatMediBtn.Enabled = true;
                    this.serverConnectTestWorker.CancelAsync();
                    this.mediCustomProgressBar1.Refresh();
                }));
            }
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnDetailInfo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(exceptionInfo))
            {
                if (this.Height <= 115)
                {
                    this.mediLayoutControl1.MinimumSize = new Size(this.mediLayoutControl1.Width, 300);
                    this.MinimumSize = new Size(this.Width, 300);
                }
                else
                {
                    this.mediLayoutControl1.MaximumSize = new Size(this.mediLayoutControl1.Width, 115);
                    this.MaximumSize = new Size(this.Width, 115);
                }
            }
            else
            {
                this.mediLayoutControl1.MaximumSize = new Size(this.mediLayoutControl1.Width, 115);
                this.MaximumSize = new Size(this.Width, 115);
            }
        }
    }
}