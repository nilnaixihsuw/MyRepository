using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Build
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                foreach (var arg in args)
                {
                    if (string.IsNullOrEmpty(arg))
                    {
                        Console.WriteLine("没有找到编译目录的参数：" + arg);
                        continue;
                    }

                    // 编译日志存放路径
                    if (arg.StartsWith("/log:"))
                    {
                        logPath = arg.Replace("/log:", "");
                        FileHelper.DeleteDirectory(@"C:\mediinfo_build");
                        FileHelper.DeleteFile(logPath);
                        continue;
                    }

                    // 开始编译目录
                    BuildDir(arg);

                }

                if(!success)
                {
                    MargeLog();
                    return 1;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                MargeLog();

                return 1;
            }

            Console.WriteLine("检测到项目总数："+ allCount + ",编译成功数：" + sCount);


            
            //var processes = Process.GetProcessesByName("MSBuild");
            //Console.WriteLine("正在关闭msbuild进程...（"+ processes.Length + "）");
            //for (int i = 0; i < processes.Length; i++)
            //{
            //    processes[i].Kill();
            //}
            //Console.WriteLine("msbuild进程关闭完成。");

            return 0;
        }

        private static string logPath = "";
        private static bool success = true;
        public static int allCount = 0;
        public static int iCount = 0;
        public static int sCount = 0;

        /// <summary>
        /// 编译目录
        /// </summary>
        /// <param name="dir"></param>
        static void BuildDir(string dir)
        {
            if (string.IsNullOrEmpty(dir))
            {
                Console.WriteLine("没有找到编译目录的参数：" + dir);
                return;
            }

            if (FileHelper.Contains(dir, "*.csproj"))
            {
                allCount++;
                Process _proc = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.FileName = "msbuild";
                processStartInfo.Arguments = dir + @" /flp:logfile=C:\mediinfo_build\" + FileHelper.GetFileName(dir) + "___" + allCount + ";errorsonly;";
                _proc.StartInfo = processStartInfo;
                _proc.EnableRaisingEvents = true;
                

                _proc.Exited += (s, e) =>
                {
                    iCount++;
                    Console.WriteLine("[" + dir + "] 编译结束");
                    Console.WriteLine("当前编译完成：" + iCount);
                };

                _proc.Start();

                //Task.Factory.StartNew(()=>{
                    string output = _proc.StandardOutput.ReadToEnd();

                    if (!string.IsNullOrEmpty(logPath))
                    {
                        //Console.WriteLine(output);
                        if (!string.IsNullOrEmpty(output) && output.IndexOf("0 个错误") < 0)
                        {
                            success = false;
                            Console.WriteLine("编译[" + dir + "]发生错误！");
                            //FileHelper.ErrorLog(output, logPath);
                        }
                        else
                        {
                            sCount++;
                        }
                    }
                    else
                    {
                        throw new ApplicationException("没有找到日志文件！");
                    }


                    //_proc.WaitForExit();
                //});

            }

            string[] childDirs = FileHelper.GetDirectories(dir);
            foreach (var childDir in childDirs)
            {
                BuildDir(childDir);
            }
        }

        /// <summary>
        /// 合并日志
        /// </summary>
        static void MargeLog()
        {
            Console.WriteLine("编译失败！正在合并错误日志....");

            string[] files = FileHelper.GetFileNames(@"C:\mediinfo_build");
            foreach (var item in files)
            {
                Console.WriteLine("正在读取：" + item);

                string txt = FileHelper.FileToString(item);

                if (string.IsNullOrWhiteSpace(txt))
                {
                    continue;
                }

                FileHelper.ErrorLog("=================================================\r\n", logPath);
                FileHelper.ErrorLog("[" + FileHelper.GetFileName(item).Split(new string[] { "___" }, StringSplitOptions.None)[0] + "]项目编译失败：\r\n", logPath);
                FileHelper.ErrorLog("================================================\r\n", logPath);
                FileHelper.ErrorLog(txt, logPath);
                FileHelper.ErrorLog("\r\n", logPath);
            }
            Console.WriteLine("合并错误日志完成！");
        }

    }
}
