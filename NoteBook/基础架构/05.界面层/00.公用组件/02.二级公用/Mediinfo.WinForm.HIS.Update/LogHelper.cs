using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Update
{
    public static class LogHelper
    {
        /// <summary>
        /// 检查是否存在文件夹
        /// </summary>
        public static void IsExistDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\errorLog.txt";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\errorLog.txt");
                fs.Close();
            }
        }
        /// <summary>
        /// 写入文本文件
        /// </summary>
        /// <param name="value"></param>
        public static void WriteLog(string value)
        {
            IsExistDirectory();
            string path = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\logs\\updateerror\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\errorLog.txt";
            FileStream f = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine(value);
            sw.Flush();
            sw.Close();
            f.Close();
        }
    }
}
