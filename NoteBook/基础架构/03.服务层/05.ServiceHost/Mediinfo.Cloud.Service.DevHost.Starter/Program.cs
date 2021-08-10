using Mediinfo.Infrastructure.JCJG;

using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Windows.Forms;

namespace Mediinfo.Cloud.Service.DevHost.Starter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DbInterception.Add(new EFIntercepterLogging());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
