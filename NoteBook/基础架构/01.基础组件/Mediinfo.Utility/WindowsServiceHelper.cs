using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Mediinfo.Utility
{
    /// <summary>
    /// windows服务帮助类
    /// </summary>
    public class WindowsServiceHelper
    {
        #region 安装服务
        /// <summary>
        /// 安装服务
        /// </summary>
        public static bool InstallService(string NameService)
        {
            bool flag = true;
            if (!IsServiceIsExisted(NameService))
            {
                try
                {
                    string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string serviceFileName = location.Substring(0, location.LastIndexOf('\\') + 1) + "Monitor\\" + NameService + ".exe";
                    if (File.Exists(serviceFileName))
                    {
                        InstallHaloService(null, serviceFileName);
                    }
                    
                }
                catch
                {
                    flag = false;
                }

            }
            return flag;
        }
        #endregion

        #region 卸载服务
        /// <summary>
        /// 卸载服务
        /// </summary>
        public static bool UninstallService(string NameService)
        {
            bool flag = true;
            if (IsServiceIsExisted(NameService))
            {
                try
                {
                    string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string serviceFileName = location.Substring(0, location.LastIndexOf('\\') + 1) +"Monitor\\" + NameService + ".exe";
                    if (File.Exists(serviceFileName))
                    {
                        UnInstallHaloService(serviceFileName);
                    }
                    
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }
        #endregion

        #region 检查服务存在的存在性
        /// <summary>
        /// 检查服务存在的存在性
        /// </summary>
        /// <param name=" NameService ">服务名</param>
        /// <returns>存在返回 true,否则返回 false;</returns>
        public static bool IsServiceIsExisted(string NameService)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.ToLower() == NameService.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 安装Windows服务
        /// <summary>
        /// 安装Windows服务
        /// </summary>
        /// <param name="stateSaver">集合</param>
        /// <param name="filepath">程序文件路径</param>
        private static void InstallHaloService(IDictionary stateSaver, string filepath)
        {
            AssemblyInstaller assemblyInstaller = new AssemblyInstaller();
            assemblyInstaller.UseNewContext = true;
            assemblyInstaller.Path = filepath;
            assemblyInstaller.Install(stateSaver);
            assemblyInstaller.Commit(stateSaver);
            assemblyInstaller.Dispose();
        }
        #endregion

        #region 卸载Windows服务
        /// <summary>
        /// 卸载Windows服务
        /// </summary>
        /// <param name="filepath">程序文件路径</param>
        private static void UnInstallHaloService(string filepath)
        {
            AssemblyInstaller assemblyInstaller = new AssemblyInstaller();
            assemblyInstaller.UseNewContext = true;
            assemblyInstaller.Path = filepath;
            assemblyInstaller.Uninstall(null);
            assemblyInstaller.Dispose();
        }
        #endregion

        #region 判断window服务是否启动
        /// <summary>
        /// 判断某个Windows服务是否启动
        /// </summary>
        /// <returns></returns>
        public static bool IsServiceStart(string serviceName)
        {
            ServiceController psc = new ServiceController(serviceName);
            bool bStartStatus = false;
            try
            {
                if (!psc.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    bStartStatus = true;
                }

                return bStartStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  修改服务的启动项
        /// <summary>  
        /// 修改服务的启动项 2为自动,3为手动  
        /// </summary>  
        /// <param name="startType"></param>  
        /// <param name="serviceName"></param>  
        /// <returns></returns>  
        public static bool ChangeServiceStartType(int startType, string serviceName)
        {
            try
            {
                RegistryKey regist = Registry.LocalMachine;
                RegistryKey sysReg = regist.OpenSubKey("SYSTEM");
                RegistryKey currentControlSet = sysReg.OpenSubKey("CurrentControlSet");
                RegistryKey services = currentControlSet.OpenSubKey("Services");
                RegistryKey servicesName = services.OpenSubKey(serviceName, true);
                servicesName.SetValue("Start", startType);
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;


        }
        #endregion

        #region 启动服务
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool StartService(string serviceName)
        {
            bool flag = true;
            if (IsServiceIsExisted(serviceName))
            {
                if (!IsServiceStart(serviceName))
                {
                    System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
                    if (service.Status != System.ServiceProcess.ServiceControllerStatus.Running && service.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
                    {
                        service.Start();
                        for (int i = 0; i < 60; i++)
                        {
                            service.Refresh();
                            System.Threading.Thread.Sleep(1000);
                            if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                            {
                                break;
                            }
                            if (i == 59)
                            {
                                flag = false;
                            }
                        }
                    }
                }
            }
            return flag;
        }
        #endregion

        #region 停止服务
        private bool StopService(string serviceName)
        {
            bool flag = true;
            if (IsServiceIsExisted(serviceName))
            {
                System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
                if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    service.Stop();
                    for (int i = 0; i < 60; i++)
                    {
                        service.Refresh();
                        System.Threading.Thread.Sleep(1000);
                        if (service.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                        {
                            break;
                        }
                        if (i == 59)
                        {
                            flag = false;
                        }
                    }
                }
            }
            return flag;
        }
        #endregion

    }
}
