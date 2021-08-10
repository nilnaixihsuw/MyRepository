using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    public partial class MediInfoUpdate : Form
    {


        /// <summary>
        /// 所有要更新的文件
        /// </summary>
        public List<ServerAllFileDic> ServerAllFileList { get; set; }
        /// <summary>
        /// 服务端获取文件委托
        /// </summary>
        /// <param name="serverPath"></param>
        /// <returns></returns>

        public delegate List<ServerAllFileDic> GetServerFileGather(string ftpServertp, string serverPath, string ftpUserName, string ftpUserpwd);


        public GetServerFileGather GetServerFileFun;


        public MediInfoUpdate()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 更新程序加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediInfoUpdate_Load(object sender, EventArgs e)
        {
            if (HISGlobalSetting.IsHttp)
            {
                #region Http更新
                UpdateCommonHelper.InitialUserCustomInfo();
                try
                {
                    List<HTTPUpdateConfig> httpconfigs = HISGlobalHelper.HttpConfigs;//读取本地配置文件
                    List<HTTPUpdateConfig> UNupdatedconfigs = HISGlobalHelper.CheckHttpConfig(httpconfigs);//检查需要更新的代码包
                    int count = 0;
                    List<HTTPUpdateConfig> Needupdatedconfigs = new List<HTTPUpdateConfig>();//需要更新得配置
                    if (UNupdatedconfigs != null && UNupdatedconfigs.Count != 0)
                    {
                        foreach (HTTPUpdateConfig item in UNupdatedconfigs)
                        {
                            string severUrl = MediinfoConfig.GetValue("DownLoadAddress.xml", "ipAddress"); //string ip = Configuration.GetIP()              
                            string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC); //最新版本号

                            string message = string.Empty;
                            long size = HTTPHelper.GetFileSize(severUrl, returnMessage, item.JIXIANMC, ref message);

                            if (!string.IsNullOrEmpty(returnMessage))
                            {
                                if (returnMessage.Equals(item.BanBenHao))
                                {//版本号没有更新
                                    count++;
                                    continue;
                                }
                                else
                                {//版本号更新
                                    long ReturnMessage;
                                    if (long.TryParse(returnMessage, out ReturnMessage) && returnMessage.Length == 18)
                                    {
                                        HTTPUpdateConfig hTTPUpdateConfig = new HTTPUpdateConfig(returnMessage, item.JIXIANMC, item.localConfigPath,size);
                                        Needupdatedconfigs.Add(hTTPUpdateConfig);                                
                                    }
                                    else
                                    {//服务器返回错误信息写入日志
                                        HTTPHelper.WriteLog(string.Format("{0},Date:{1}", returnMessage, DateTime.Now));
                                    }
                                }
                            }
                        }
                        if (count == UNupdatedconfigs.Count)
                        { //不需要下载更新 关闭
                            this.Tag = null;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                        this.Tag = Needupdatedconfigs;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch(ApplicationException ex)
                {
                    MessageBox.Show(ex.Message + ex.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //MessageBox.Show("更新程序内部出现错误\n请联系管理员", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                              ex.Message,
                             ex.InnerException));
                    Application.Exit();
                    this.Invoke((MethodInvoker)delegate { this.DialogResult = DialogResult.Cancel; });
                }
                #endregion Http更新
            }
            else
            {
                #region FTP更新
                try
                {
                    FTPHelper ftpHelper = new FTPHelper("ftp://" + DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key) + "", HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser, DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key));
                    string testerrorMsg = string.Empty;
                    if (!ftpHelper.TestFtpConnection(ref testerrorMsg))
                    {
                        UpdateCommonHelper.FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key);
                        ftpHelper = new FTPHelper("ftp://" + DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key) + "", HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser, DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key));
                    }

                    if (!ftpHelper.TestFtpConnection(ref testerrorMsg))
                    {
                        //MessageBox.Show("FTP信息未配置!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        using (FTPConfigFrm fTPConfigFrm = new FTPConfigFrm())
                        {
                            fTPConfigFrm.ShowDialog();
                            if (fTPConfigFrm.DialogResult == DialogResult.Cancel)
                            {
                                this.Close();
                                this.Dispose();
                                return;
                            }
                        }
                    }

                    UpdateCommonHelper.InitialUserCustomInfo(new UpdateConfigInfo()
                    {
                        FtpFirstSubDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName,
                        FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key),
                        FtpPwd = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key),
                        //FtpRootDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName,
                        FtpUser = HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser,
                        UpdateExeName = HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName,
                        LoginFormName = HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName
                    });

                    string errorMsg = string.Empty;
                    long tempDownLoadFileSize = 0;
                    List<UpdateDirectories> directorylist = UpdateDirectory.GetUpdateDirectories(UpdateCommonHelper.FtpFirstSubDirectoryName.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries), out errorMsg, null, 0, ref tempDownLoadFileSize);
                    if (ServerAllFileList != null)
                        ServerAllFileList.Clear();
                    else
                        ServerAllFileList = new List<ServerAllFileDic>();

                    foreach (UpdateDirectories updateFileName in directorylist)
                    {
                        GetServerFileFun = GetServerFilesByDelagete;
                        var result = this.BeginInvoke(GetServerFileFun, new object[] { UpdateCommonHelper.FtpIp, updateFileName.DirectoryName + "", UpdateCommonHelper.FtpUser, UpdateCommonHelper.FtpPwd });

                        object ret = this.EndInvoke(result);//返回的服务端文件集合
                        if (ret == null)
                        {
                        }

                        List<ServerAllFileDic> serverAllFileDic = ret as List<ServerAllFileDic>;
                        ServerAllFileList.AddRange(serverAllFileDic);

                    }
                    //foreach (string referencedll in MainReferencedll)
                    //{

                    //    //如果服务端有对应的引用更新则下载


                    //    if (ServerAllFileList.Where(o => o.ServerFileName == referencedll).ToList().Count > 0)
                    //    {
                    //        ServerAllFileDic serverAllFileDic = ServerAllFileList.Where(o => o.ServerFileName == referencedll).FirstOrDefault();
                    //        if (serverAllFileDic.LastUpdateTime.ToString() != "")//查询客户端想对应文件日志记录
                    //        {
                    //            //更新相应的dll
                    //        }
                    //    }
                    //}

                    //将文件按最后修改时间排序，防止解压时旧文件覆盖新文件
                    ServerAllFileList = ServerAllFileList.OrderBy(s => s.LastUpdateTime, new LastUpdateTimeComparer()).ToList();

                    this.Tag = ServerAllFileList;
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message + ex.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //MessageBox.Show("更新程序内部出现错误\n请联系管理员", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                              ex.Message,
                             ex.InnerException));
                    Application.Exit();
                    this.Invoke((MethodInvoker)delegate { this.DialogResult = DialogResult.Cancel; });
                }
                #endregion FTP更新
            }

        }




        /// <summary>
        /// 委托返回服务端所有文件
        /// </summary>
        /// <param name="ftpServertp"></param>
        /// <param name="serverPath"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpUserpwd"></param>
        /// <returns></returns>
        public List<ServerAllFileDic> GetServerFilesByDelagete(string ftpServertp, string serverPath, string ftpUserName, string ftpUserpwd)
        {
            List<ServerAllFileDic> serverAllFileDicList = new List<ServerAllFileDic>();

            try
            {
                GetServerFiles(ftpServertp, serverPath, ftpUserName, ftpUserpwd, ref serverAllFileDicList);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(string.Format("原因: 获取服务端文件列表失败, 请检查\n配置文件HISGlobalSetting.xml信息!DateTime:{0},Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                         ex.Message,
                        ex.InnerException));
            }
            return serverAllFileDicList;
        }



        /// <summary>
        /// 服务端文件路径
        /// </summary>
        /// <param name="serverPath"></param>
        /// <returns></returns>
        private void GetServerFiles(string ftpServertp, string serverPath, string ftpUserName, string ftpUserpwd, ref List<ServerAllFileDic> serverAllFileDicList)
        {
            string PreFilePathName = string.Empty;
            string serverType = string.Empty;
            FTPHelper ftpHelper = new FTPHelper(ftpServertp, serverPath, ftpUserName, ftpUserpwd);
            string[] strs = ftpHelper.GetFilesDetailList(ref serverType);
            if (strs == null)
                return;
            if (!string.IsNullOrWhiteSpace(serverType))
            {
                if (serverType.Contains("Microsoft"))
                {
                    foreach (string fileinfos in strs)
                    {
                        string[] fileinfo = fileinfos.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (!fileinfo[2].Contains("DIR"))
                        {
                            string errorMsg1 = string.Empty;
                            string errorMsg2 = string.Empty;
                            ServerAllFileDic serverAllFileDic = new ServerAllFileDic
                            {
                                ServerFileName = fileinfos.Substring(fileinfos.IndexOf(fileinfo[3])),
                                ServerFilePath = serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[3])),
                                LastUpdateTime = ftpHelper.GetFileLastModifyTime("ftp://" + ftpServertp + "//" + serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[3])), ref errorMsg1),
                                ServerFileSize = ftpHelper.GetFileSize("ftp://" + ftpServertp + "//" + serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[3])), ref errorMsg2)

                            };
                            serverAllFileDicList.Add(serverAllFileDic);
                        }
                        else if (fileinfo[2].Contains("DIR"))
                        {
                            PreFilePathName = serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[3]));
                            if (HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName.Split('^').Contains(PreFilePathName.Replace("\\", "/")))
                                GetServerFiles(ftpServertp, PreFilePathName, ftpUserName, ftpUserpwd, ref serverAllFileDicList);
                        }
                    }
                }
                else if (serverType.Contains("Serv-U"))
                {
                    foreach (string fileinfos in strs)
                    {
                        string[] fileinfo = fileinfos.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (!fileinfo[0].Substring(0).Contains("d"))
                        {
                            string errorMsg1 = string.Empty;
                            string errorMsg2 = string.Empty;
                            ServerAllFileDic serverAllFileDic = new ServerAllFileDic
                            {
                                ServerFileName = fileinfos.Substring(fileinfos.IndexOf(fileinfo[8])),
                                ServerFilePath = serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[8])),
                                LastUpdateTime = ftpHelper.GetFileLastModifyTime("ftp://" + ftpServertp + "//" + serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[8])), ref errorMsg1),
                                ServerFileSize = ftpHelper.GetFileSize("ftp://" + ftpServertp + "//" + serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[8])), ref errorMsg2)

                            };
                            serverAllFileDicList.Add(serverAllFileDic);
                        }
                        else if (fileinfo[0].Substring(0).Contains("-"))
                        {
                            if (fileinfo[8] != "." && fileinfo[8] != "..")
                            {
                                PreFilePathName = serverPath + "\\" + fileinfos.Substring(fileinfos.IndexOf(fileinfo[8]));
                                if (HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName.Split('^').Contains(PreFilePathName.Replace("\\", "/")))
                                    GetServerFiles(ftpServertp, PreFilePathName, ftpUserName, ftpUserpwd, ref serverAllFileDicList);
                            }

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("ftp服务器版本信息获取失败!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

    }

    public class LastUpdateTimeComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            DateTime.TryParse(x, out DateTime xTime);
            DateTime.TryParse(y, out DateTime yTime);
            return DateTime.Compare(xTime, yTime);
        }
    }
}
