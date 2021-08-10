namespace Mediinfo.WinForm.HIS.Monitor
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.haloserviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.haloserviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // haloserviceProcessInstaller
            // 
            this.haloserviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.haloserviceProcessInstaller.Password = null;
            this.haloserviceProcessInstaller.Username = null;
            // 
            // haloserviceInstaller
            // 
            this.haloserviceInstaller.Description = "HALO性能检测服务";
            this.haloserviceInstaller.ServiceName = "Mediinfo.WinForm.HIS.Monitor";
            this.haloserviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.haloserviceProcessInstaller,
            this.haloserviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller haloserviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller haloserviceInstaller;
    }
}