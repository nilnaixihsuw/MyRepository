using DevExpress.XtraTreeList.Nodes;

using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class CustomDownLoadFileFrm : MediForm
    {
        public CustomDownLoadFileFrm()
        {
            InitializeComponent();
        }

        private void medisbtnOK_Click(object sender, EventArgs e)
        {
            List<TreeListNode> selectedTreeList = this.updateFiletreeList.GetAllCheckedNodes();

            List<TreeListNode> allNodeList = this.updateFiletreeList.GetNodeList();
            string unUpdateCatelog = string.Empty;
            allNodeList.ForEach(o =>
            {
                if (!o.Checked)
                {
                    unUpdateCatelog += o.GetValue("DirectoryName").ToString() + "^";
                }
            });
            if (unUpdateCatelog.Length > 0)
            {
                unUpdateCatelog = unUpdateCatelog.Remove(unUpdateCatelog.Length - 1);
            }
            OperateIniFile operateIniFile = new OperateIniFile(AppDomain.CurrentDomain.BaseDirectory + "update.ini");
            operateIniFile.WriteString("FTPINFO", "UnUpdateCatelog", unUpdateCatelog);
            this.Close();
            this.Dispose();
        }

        private void medisbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomDownLoadFileFrm_Load(object sender, EventArgs e)
        {
            if (!UpdateHelper.InitialUserCustomInfo(new UpdateConfigInfo()
            {
                FtpFirstSubDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName,
                FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key),
                FtpPwd = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key),
                // FtpRootDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName,
                FtpUser = HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser,
                UpdateExeName = HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName,
                LoginFormName = HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName

            }))
            {
                MediMsgBox.Warn("更新文件FTP信息未配置,请配置相关信息!");
                FTPConfigFrm fTPConfigFrm = new FTPConfigFrm();
                fTPConfigFrm.ShowDialog();
                fTPConfigFrm.Dispose();
            }
            if (string.IsNullOrWhiteSpace(UpdateHelper.FtpFirstSubDirectoryName))
                return;
            string[] Directories = UpdateHelper.FtpFirstSubDirectoryName.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);
            // 获取启动程序的上一级文件目录
            DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录
            string errorMsg = string.Empty;
            List<UpdateDirectories> directorylist = UpdateDirectory.GetUpdateDirectories(rootPathInfo, UpdateHelper.FtpFirstSubDirectoryName.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries), out errorMsg);
            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                MediMsgBox.Warn(errorMsg);
                return;
            }
            foreach (var item in Directories)
            {
                directorylist.Add(new UpdateDirectories() { DirectoryName = item });
            }

            OperateIniFile operateIniFile = new OperateIniFile(AppDomain.CurrentDomain.BaseDirectory + "update.ini");

            if (string.IsNullOrWhiteSpace(operateIniFile.ReadString("FTPINFO", "UnUpdateCatelog", "")))
            {
                this.updateFiletreeList.DataSource = directorylist;
            }
            else
            {
                this.updateFiletreeList.DataSource = directorylist;
                List<TreeListNode> allNodeList = this.updateFiletreeList.GetNodeList();
                string[] UnUpdateCatelogs = operateIniFile.ReadString("FTPINFO", "UnUpdateCatelog", "").Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);

                allNodeList.ForEach(o => { o.Checked = true; });
                foreach (string item in UnUpdateCatelogs)
                {
                    allNodeList.Where(o => o.GetValue("DirectoryName").ToString().Trim() == item.Trim()).ToList().ForEach(o => { o.Checked = false; });
                }
            }
        }
    }
}
