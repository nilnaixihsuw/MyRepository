using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ini;
namespace OpMonitor
{
    class IniReader
    {
            public static void LoadProfile()
            {
                string strPath = string.Format("{0}\\Config\\Cfg.ini", Environment.CurrentDirectory);
                _file = new IniFile(strPath);
                G_IP = _file.ReadString("OpcServer", "IP", "127.0.0.1");    //读数据，下同
                G_SERVERNAME = _file.ReadString("OpcServer", "ServerName", "KEPware.KEPServerEx.V4");
                G_TAGPATH = _file.ReadString("Tag", "TagPath", "\\Config\\tags.xml");
            }

            public static void SaveProfile()
            {
                string strPath = string.Format("{0}\\Config\\Cfg.ini", Environment.CurrentDirectory);
                _file = new IniFile(strPath);
                _file.WriteString("OpcServer", "IP", G_IP);            //写数据，下同
                _file.WriteString("OpcServer", "ServerName", G_SERVERNAME); 
                //_file.Writestring("CONFIG", "DataBits", G_DATABITS);
                //_file.Writestring("CONFIG", "StopBits", G_STOP);
                //_file.Writestring("CONFIG", "G_PARITY", G_PARITY);
                //_file.Writestring("CONFIG", "Comm", G_COMM);
                //_file.Writestring("CONFIG", "SheetA", G_SHEETA);
                //_file.Writestring("CONFIG", "SheetB", G_SHEETB);
                //_file.Writestring("CONFIG", "WeightNo", G_WEIGHTNO);
            }

            private static IniFile _file;//内置了一个对象
            public static string G_IP = "127.0.0.1";//给ini文件赋新值
            public static string G_SERVERNAME = "KEPware.KEPServerEx.V4";
            public static string G_TAGPATH = "\\Config\\tags.xml"; 
    }
}
