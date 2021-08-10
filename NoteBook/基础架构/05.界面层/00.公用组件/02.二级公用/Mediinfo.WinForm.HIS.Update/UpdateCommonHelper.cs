using System;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    public static class UpdateCommonHelper
    {

        /// <summary>
        /// ftp用户名
        /// </summary>
        public static string FtpUser { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public static string FtpPwd { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public static string FtpIp { get; set; }

        /// <summary>
        /// 更新程序名称
        /// </summary>
        public static string UpdateExeName { get; set; }

        /// <summary>
        /// ftp根文件
        /// </summary>
       // public static string FtpRootDirectoryName { get; set; }


        /// <summary>
        /// 一级节点文件夹
        /// </summary>
        public static string FtpFirstSubDirectoryName { get; set; }
        /// <summary>
        /// 登录窗口名称
        /// </summary>
        public static string LoginFormName { get; set; }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        public static bool InitialUserCustomInfo(UpdateConfigInfo updateftpconfigInfo)
        {

            FTPHelper ftpHelper = new FTPHelper("ftp://" + DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key) + "", HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser, DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key));
            string errorMsg = string.Empty;
            if (!ftpHelper.TestFtpConnection(ref errorMsg))
                FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key);
            else
                FtpIp = updateftpconfigInfo.FtpIp;
            FtpUser = updateftpconfigInfo.FtpUser;
            FtpPwd = updateftpconfigInfo.FtpPwd;

            UpdateExeName = updateftpconfigInfo.UpdateExeName;
            LoginFormName = updateftpconfigInfo.LoginFormName;
            // FtpRootDirectoryName = updateftpconfigInfo.FtpRootDirectoryName;
            FtpFirstSubDirectoryName = updateftpconfigInfo.FtpFirstSubDirectoryName;
            //FtpUser = System.Configuration.ConfigurationManager.AppSettings["FTPUser"];
            //FtpPwd = System.Configuration.ConfigurationManager.AppSettings["FTPPwd"];
            //FtpIp = System.Configuration.ConfigurationManager.AppSettings["FTPHost"];
            //UpdateExeName = System.Configuration.ConfigurationManager.AppSettings["FTPupdateName"];
            //LoginFormName = System.Configuration.ConfigurationManager.AppSettings["FTPLoginName"];
            //FtpRootDirectoryName = System.Configuration.ConfigurationManager.AppSettings["FTPRoot"];
            //FtpFirstSubDirectoryName = System.Configuration.ConfigurationManager.AppSettings["FTPSUB"];
            if (!string.IsNullOrWhiteSpace(FtpUser) && !string.IsNullOrWhiteSpace(FtpPwd) && !string.IsNullOrWhiteSpace(FtpIp) && !string.IsNullOrWhiteSpace(UpdateExeName) && !string.IsNullOrWhiteSpace(LoginFormName) && !string.IsNullOrWhiteSpace(FtpFirstSubDirectoryName))
                return true;
            else
                return false;
        }
        /// <summary>
        /// HTTP 下载初始化用户信息
        /// </summary>
        /// <param name="updateftpconfigInfo"></param>
        /// <returns></returns>
        public static void InitialUserCustomInfo()
        {
            UpdateExeName = "Mediinfo.WinForm.HIS.Update";
            LoginFormName = "Mediinfo.WinForm.HIS.Main";
        }
        /// <summary>
        /// ftp下载文件
        /// </summary>
        /// <param name="localDir"></param>
        /// <param name="FtpFile"></param>
        public static bool GetFileNoBinary(string ftpFolder, string ftpFileName, string localDir, string localFileName, MediWaitCircleControl label, long totalFileSize, ref long downLoadFileSize)
        {

            try
            {
                long dowwnLoadFileSize = downLoadFileSize;


                if ( label != null && totalFileSize != 0)
                {
                    label.Invoke((MethodInvoker)delegate
                    {
                        //label.Description = string.Format("正在下载{0}", localFileName);
                    });
                    //mediProgressBarControl.Invoke(
                    //  (MethodInvoker)(() => mediProgressBarControl.Maximum = 100));
                }

                string URI = "ftp://" + FtpIp + "\\" + ftpFolder + "\\" + ftpFileName;//所下载的文件在服务器所在的位置
                URI = URI.Replace("\\", "//");
                string localfile = localDir + "\\" + localFileName;//本地目录
                try
                {
                    if (File.Exists(localDir + @"" + "\\" + ftpFileName))
                    {
                        File.Delete(localDir + @"" + "\\" + ftpFileName);
                    }
                    //File.Move(localfile, localDir + @"" + ftpFolder + "//" + ftpFileName);
                }
                catch (Exception ex)
                {
                    if (File.Exists(localfile))
                    {
                        File.Delete(localfile);
                    }
                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "class:UpdateCommonHelper" + "出错文件:" + localDir + @"" + "\\" + ftpFileName,
                    ex.Message,
                   ex.InnerException));
                    return false;

                }
                System.Net.FtpWebRequest ftpwr = GetRequest(URI, FtpUser, FtpPwd);

                ftpwr.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;

                ftpwr.UseBinary = true;
                ftpwr.KeepAlive = false;
                ftpwr.UsePassive = true;


                try
                {
                    using (FtpWebResponse response = (FtpWebResponse)ftpwr.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (FileStream fs = new FileStream(localfile, FileMode.CreateNew))
                            {
                                try
                                {
                                    byte[] buffer = new byte[2048];

                                    int read = 0;

                                    do
                                    {
                                        read = responseStream.Read(buffer, 0, buffer.Length);

                                        fs.Write(buffer, 0, read);
                                        if (label != null && totalFileSize != 0)
                                        {

                                            label.Invoke(
                                                (MethodInvoker)(
                                                () =>
                                                {
                                                    double progressCount = 0;
                                                    if (((dowwnLoadFileSize + fs.Position) * 1.0d / totalFileSize) * 100 > 100)
                                                    {
                                                        progressCount = 100;
                                                    }
                                                    else
                                                    {
                                                        progressCount = ((dowwnLoadFileSize + fs.Position) * 1.0d / totalFileSize) * 100;
                                                        label.Description = "正在下载文件" + Convert.ToInt32(progressCount) +"%";
                                                    }



                                                })
                                                );
                                        }

                                    } while (!(read == 0));
                                    downLoadFileSize += fs.Position;
                                    responseStream.Close();
                                    fs.Flush();
                                    fs.Close();
                                }
                                catch (Exception e)
                                {
                                    fs.Close();

                                    if (File.Exists(localfile))
                                    {
                                        File.Delete(localfile);
                                    }


                                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "class:UpdateCommonHelper" + "出错文件:" + ftpFileName,
                     e.Message,
                    e.InnerException));
                                }
                            }
                            responseStream.Close();
                        }
                        response.Close();
                    }
                }
                catch (Exception ex)
                {


                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "class:UpdateCommonHelper" + "出错文件:" + ftpFileName,
                       ex.Message,
                      ex.InnerException));
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "class:UpdateCommonHelper" + "出错文件:" + ftpFileName,
                     ex.Message,
                    ex.InnerException));
                return false;
            }
        }

        private static FtpWebRequest GetRequest(string URI, string username, string password)
        {
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
            result.Credentials = new System.Net.NetworkCredential(username, password);
            result.KeepAlive = false;
            return result;
        }




    }


    /// <summary>
    ///操作ini读取或者写数据库连接字符串
    /// </summary>
    public class OperateIniFile
    {
        private string iniFileName;

        public string FileName
        {
            get { return iniFileName; }
            set { iniFileName = value; }
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(
            string iniAppName,
            string iniKeyName,
            int iniDefault,
            string iniFileName
            );

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string iniAppName,
            string iniKeyName,
            string iniDefault,
            StringBuilder iniReturnedString,
            int nSize,
            string iniFileName
            );

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string iniAppName,
            string iniKeyName,
            string iniString,
            string iniFileName
            );

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aFileName">Ini文件路径</param>
        public OperateIniFile(string aFileName)
        {
            this.iniFileName = aFileName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public OperateIniFile()
        { }

        /// <summary>
        /// [扩展]读Int数值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public int ReadInt(string section, string name, int def)
        {
            return GetPrivateProfileInt(section, name, def, this.iniFileName);
        }

        /// <summary>
        /// [扩展]读取string字符串
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string ReadString(string section, string name, string def)
        {
            StringBuilder vRetSb = new StringBuilder(2048);
            GetPrivateProfileString(section, name, def, vRetSb, 2048, this.iniFileName);
            return vRetSb.ToString();
        }

        /// <summary>
        /// [扩展]写入Int数值，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="Ival">写入值</param>
        public void WriteInt(string section, string name, int Ival)
        {
            WritePrivateProfileString(section, name, Ival.ToString(), this.iniFileName);
        }

        /// <summary>
        /// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="strVal">写入值</param>
        public void WriteString(string section, string name, string strVal)
        {
            WritePrivateProfileString(section, name, strVal, this.iniFileName);
        }

        /// <summary>
        /// 删除指定的 节
        /// </summary>
        /// <param name="section"></param>
        public void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, this.iniFileName);
        }

        /// <summary>
        /// 删除全部 节
        /// </summary>
        public void DeleteAllSection()
        {
            WritePrivateProfileString(null, null, null, this.iniFileName);
        }

        /// <summary>
        /// 读取指定 节-键 的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string IniReadValue(string section, string name)
        {
            StringBuilder strSb = new StringBuilder(256);
            GetPrivateProfileString(section, name, "", strSb, 256, this.iniFileName);
            return strSb.ToString();
        }

        /// <summary>
        /// 写入指定值，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void IniWriteValue(string section, string name, string value)
        {
            WritePrivateProfileString(section, name, value, this.iniFileName);
        }
    }
    /// <summary>
    /// 更新文件夹
    /// </summary>
    public static class UpdateDirectory
    {





        /// <summary>
        /// 获取项目文件夹
        /// </summary>
        /// <param name="programDirectories"></param>
        /// <returns></returns>
        public static List<UpdateDirectories> GetUpdateDirectories(string[] programDirectories, out string errorMessage, MediWaitCircleControl label, long totalFileSize, ref long downLoadFileSize)
        {
            errorMessage = string.Empty;
            OperateIniFile updateOperateIniFile = new OperateIniFile(AppDomain.CurrentDomain.BaseDirectory + "update.ini");

            string unupadteDirectories = updateOperateIniFile.ReadString("FTPINFO", "UnUpdateCatelog", "");

            List<UpdateDirectories> directorylist = new List<UpdateDirectories>();

            //比较版本配置文件和temp配置文件版本号是否一致，如果一致不作处理，否则获取temp文件节点信息加载到该窗体

            foreach (var subDirectory in programDirectories)
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + subDirectory))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + subDirectory);

                    if (!string.IsNullOrWhiteSpace(unupadteDirectories))
                    {




                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp");
                        try
                        {
                            if (UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize))
                            {
                                if (!unupadteDirectories.Contains(subDirectory))
                                {
                                    directorylist.Add(new UpdateDirectories { DirectoryName = subDirectory });

                                }
                            }
                        }
                        catch (WebException exp)
                        {
                            errorMessage = "更新程序" + exp.Message;
                        }

                    }
                    else
                    {

                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp");
                        try
                        {
                            if (UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize))
                            {
                                directorylist.Add(new UpdateDirectories { DirectoryName = subDirectory });
                            }
                        }
                        catch (WebException exp)
                        {
                            errorMessage = "更新程序" + exp.Message;
                        }
                    }

                }



                else
                {
                    //获取当前文件夹下的ini文件判断版本号
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\version.ini"))
                    {


                        if (!string.IsNullOrWhiteSpace(unupadteDirectories))
                        {





                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp");
                            try
                            {
                                if (UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize))
                                {
                                    if (!unupadteDirectories.Contains(subDirectory))
                                    {
                                        //服务端获取
                                        directorylist.Add(new UpdateDirectories { DirectoryName = subDirectory });

                                    }
                                }
                            }
                            catch (WebException exp)
                            {
                                errorMessage = "更新程序" + exp.Message;
                            }


                        }
                        else
                        {

                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp");
                            try
                            {
                                if (UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize))
                                {
                                    //服务端获取
                                    directorylist.Add(new UpdateDirectories { DirectoryName = subDirectory });
                                }
                            }
                            catch (WebException exp)
                            {
                                errorMessage = "更新程序" + exp.Message;
                            }
                        }

                    }
                    else
                    {
                        //服务端获取Version.ini文件放在temp文件下
                        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp"))
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp");
                            try
                            {
                                if (UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize))
                                {

                                }
                            }
                            catch (WebException exp)
                            {
                                errorMessage = "更新程序" + exp.Message;
                            }
                        }
                        else//存在
                        {
                            //服务端获取Version.ini文件放在temp文件下
                            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Temp"))
                            {
                                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Temp");
                                try
                                {
                                    UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize);
                                }
                                catch (WebException exp)
                                {
                                    errorMessage = "更新程序" + exp.Message;
                                }
                            }
                            else//存在
                            {
                                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp\\" + "version.ini"))
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp\\" + "version.ini");
                                try
                                {
                                    if (UpdateCommonHelper.GetFileNoBinary(subDirectory, "version.ini", AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize))
                                    {

                                    }
                                }
                                catch (WebException exp)
                                {
                                    errorMessage = "更新程序" + exp.Message;
                                }
                            }
                        }

                        //比较本地和temp文件夹下版本号是否一致

                        OperateIniFile tempoperateIniFile = new OperateIniFile(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\Temp\\" + "version.ini");

                        OperateIniFile operateIniFile = new OperateIniFile(AppDomain.CurrentDomain.BaseDirectory + subDirectory + "\\" + "version.ini");
                        string tempVersionNo = tempoperateIniFile.ReadString("version", "version", string.Empty);
                        string versionNo = operateIniFile.ReadString("version", "version", string.Empty);

                        string tempUpdateOK = tempoperateIniFile.ReadString("version", "UpdateOK", string.Empty);

                        string updateOK = operateIniFile.ReadString("version", "UpdateOK", string.Empty);
                        if (!tempVersionNo.Equals(versionNo))
                        {

                            if (!string.IsNullOrWhiteSpace(unupadteDirectories))
                            {


                                if (!unupadteDirectories.Contains(subDirectory) || (unupadteDirectories.Contains(subDirectory) && (!tempVersionNo.Equals(versionNo))))
                                {
                                    directorylist.Add(new UpdateDirectories { DirectoryName = subDirectory });
                                }

                            }
                            else
                            {
                                directorylist.Add(new UpdateDirectories { DirectoryName = subDirectory });
                            }

                            //记录需要执行删除操作的目录
                            string delete = tempoperateIniFile.ReadString("version", "delete", string.Empty);
                            string dir =new DirectoryInfo(Path.Combine( AppDomain.CurrentDomain.BaseDirectory,subDirectory)).FullName;
                            if (delete.Equals("1") && !HISGlobalHelper.DeleteDirectories.Contains(dir))
                            {
                                HISGlobalHelper.DeleteDirectories.Add(dir);
                            }

                        }


                    }
                }

            }



            return directorylist;
        }





        /// <summary>
        /// 获取项目文件夹
        /// </summary>
        /// <param name="ftpRootDirectory"></param>
        /// <param name="programDirectories"></param>
        /// <returns></returns>
        public static void GetUpdateDirectory(string startRootPath, string programDirectorie, out string errorMessage, MediProgressBarControl mediProgressBarControl, MediWaitCircleControl label, long totalFileSize, ref long downLoadFileSize)
        {
            errorMessage = string.Empty;
            OperateIniFile updateOperateIniFile = new OperateIniFile(startRootPath + "\\update.ini");

            string unupadteDirectories = updateOperateIniFile.ReadString("FTPINFO", "UnUpdateCatelog", "");

            List<UpdateDirectories> directorylist = new List<UpdateDirectories>();

            //比较版本配置文件和temp配置文件版本号是否一致，如果一致不作处理，否则获取temp文件节点信息加载到该窗体

            if (!Directory.Exists(startRootPath + "\\" + programDirectorie))
            {
                Directory.CreateDirectory(startRootPath + "\\" + programDirectorie);

                if (!string.IsNullOrWhiteSpace(unupadteDirectories))
                {
                    Directory.CreateDirectory(startRootPath + "\\" + programDirectorie + "\\Temp");
                    try
                    {
                        UpdateCommonHelper.GetFileNoBinary(programDirectorie, "version.ini", startRootPath + "\\" + programDirectorie + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize);
                    }
                    catch (WebException exp)
                    {
                        errorMessage = "更新程序" + exp.Message;
                    }
                }
            }
            else
            {
                //获取当前文件夹下的ini文件判断版本号
                if (!File.Exists(startRootPath + "\\" + programDirectorie + "\\version.ini"))
                {
                    if (!string.IsNullOrWhiteSpace(unupadteDirectories))
                    {

                        Directory.CreateDirectory(startRootPath + "\\" + programDirectorie + "\\Temp");
                        try
                        {
                            UpdateCommonHelper.GetFileNoBinary(programDirectorie, "version.ini", startRootPath + "\\" + programDirectorie + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize);
                        }
                        catch (WebException exp)
                        {
                            errorMessage = "更新程序" + exp.Message;
                        }
                    }
                }
                else
                {
                    //服务端获取Version.ini文件放在temp文件下
                    if (!Directory.Exists(startRootPath + "\\" + programDirectorie + "\\Temp"))
                    {
                        Directory.CreateDirectory(startRootPath + "\\" + programDirectorie + "\\Temp");
                        try
                        {
                            UpdateCommonHelper.GetFileNoBinary(programDirectorie, "version.ini", startRootPath + "\\" + programDirectorie + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize);
                        }
                        catch (WebException exp)
                        {
                            errorMessage = "更新程序" + exp.Message;
                        }
                    }
                    else//存在
                    {
                        //服务端获取Version.ini文件放在temp文件下
                        if (!Directory.Exists(startRootPath + "\\" + "Temp"))
                        {
                            Directory.CreateDirectory(startRootPath + "\\" + "Temp");
                            try
                            {
                                UpdateCommonHelper.GetFileNoBinary(programDirectorie, "version.ini", startRootPath + "\\" + programDirectorie + "\\Temp", "version.ini", label, totalFileSize, ref downLoadFileSize);
                            }
                            catch (WebException exp)
                            {
                                errorMessage = "更新程序" + exp.Message;
                            }
                        }
                    }

                    //比较本地和temp文件夹下版本号是否一致

                    OperateIniFile tempoperateIniFile = new OperateIniFile(startRootPath + "\\" + programDirectorie + "\\Temp\\" + "version.ini");

                    OperateIniFile operateIniFile = new OperateIniFile(startRootPath + "\\" + programDirectorie + "\\" + "version.ini");
                    string tempVersionNo = tempoperateIniFile.ReadString("version", "version", string.Empty);
                    string versionNo = operateIniFile.ReadString("version", "version", string.Empty);

                    if (!tempVersionNo.Equals(versionNo))
                    {
                        if (File.Exists(startRootPath + programDirectorie + "\\Temp\\version.ini"))
                            File.Copy(startRootPath + programDirectorie + "\\Temp\\version.ini", startRootPath + programDirectorie + "\\version.ini", true);

                    }
                }
            }




        }
    }

    /// <summary>
    /// 待更新文件夹类
    /// </summary>
    public class UpdateDirectories
    {
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string DirectoryName { get; set; }
    }





    /// <summary>
    /// 更新文件类
    /// </summary>
    public struct UpdateConfigInfo
    {
        public string FtpUser;
        public string FtpPwd;
        public string FtpIp;
        public string UpdateExeName;
        public string LoginFormName;
        public string FtpFirstSubDirectoryName;
    }


    public class ServerAllFileDic
    {
        public string ServerFileName { get; set; }
        public string ServerFilePath { get; set; }

        public string LastUpdateTime { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long ServerFileSize { get; set; }
    }


}