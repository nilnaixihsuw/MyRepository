using Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpcClient
{
    public class IniReader
    {
        #region 变量
        private static IniFile _file;//内置了一个对象
        private static string defaultIP = "127.0.0.1";//（默认）
        private static string defaultSERVERNAME = "KEPware.KEPServerEx.V4";//（默认）
        private static string defaultTAGPATH = "\\Config\\tags.xml";//（默认）
        private static string defaultTRIGGERUPDATERATE = "250";//组刷新率（默认）
        private static string defaultDATAUPDATERATE = "1000";//组刷新率（默认）
        private static string defaultRECONNECTENABLE = "false";//是否启用断线重连（默认）
        private static string defaultRECONNECTINTERVAL = "10000";//重连时间间隔（默认10秒）

        /// <summary>
        /// opc ip地址
        /// </summary>
        public string G_IP { get; set; }
        public string G_SERVERNAME { get; set; }
        /// <summary>
        /// tag文件路径
        /// </summary>
        public string G_TAGPATH { get; set; }

        /// <summary>
        /// 组刷新率
        /// </summary>
        public int G_TriggerUpdateRate { get; set; }
        /// <summary>
        /// 组刷新率
        /// </summary>
        public int G_DataUpdateRate { get; set; }
        /// <summary>
        /// 是否启用断线重连
        /// </summary>
        public bool G_ReconnectEnable { get; set; }
        /// <summary>
        /// 重连时间间隔
        /// </summary>
        public int G_ReconnectInterval { get; set; }
        #endregion  
        public static IniReader Instance = new IniReader();
        public IniReader()
        {
            Load();
        }      
        public void Load()
        {
            string strPath = string.Format("{0}\\Config\\Cfg.ini", Environment.CurrentDirectory);
            _file = new IniFile(strPath);
            G_IP = _file.ReadString("OpcServer", "IP", defaultIP);    //读数据，下同
            G_SERVERNAME = _file.ReadString("OpcServer", "ServerName", defaultSERVERNAME);
            G_ReconnectEnable = Convert.ToBoolean(_file.ReadString("OpcServer", "ReconnectEnable", defaultRECONNECTENABLE));
            G_ReconnectInterval = Convert.ToInt32(_file.ReadString("OpcServer", "ReConnectInterval", defaultRECONNECTINTERVAL));
            G_DataUpdateRate =  Convert.ToInt32(_file.ReadString("Group", "DataUpdateRate", defaultDATAUPDATERATE));
            G_TriggerUpdateRate = Convert.ToInt32(_file.ReadString("Group", "TriggerUpdateRate", defaultTRIGGERUPDATERATE));
            G_TAGPATH = _file.ReadString("Tag", "TagPath", defaultTAGPATH);
        }

        public void Save()
        {
            string strPath = string.Format("{0}\\Config\\Cfg.ini", Environment.CurrentDirectory);
            _file = new IniFile(strPath);
            _file.WriteString("OpcServer", "IP", G_IP.Trim());        //写数据，下同
            _file.WriteString("OpcServer", "ServerName", G_SERVERNAME.Trim());
            _file.WriteString("OpcServer", "ReconnectEnable", G_ReconnectEnable.ToString().Trim());
            _file.WriteString("OpcServer", "ReConnectInterval", G_ReconnectInterval.ToString().Trim());
            _file.WriteString("Group", "TriggerUpdateRate", G_TriggerUpdateRate.ToString().Trim());
            _file.WriteString("Group", "DataUpdateRate", G_DataUpdateRate.ToString().Trim());
            _file.WriteString("Tag", "TagPath", G_TAGPATH.Trim());


            //_file.Writestring("CONFIG", "DataBits", G_DATABITS);
            //_file.Writestring("CONFIG", "StopBits", G_STOP);
            //_file.Writestring("CONFIG", "G_PARITY", G_PARITY);
            //_file.Writestring("CONFIG", "Comm", G_COMM);
            //_file.Writestring("CONFIG", "SheetA", G_SHEETA);
            //_file.Writestring("CONFIG", "SheetB", G_SHEETB);
            //_file.Writestring("CONFIG", "WeightNo", G_WEIGHTNO);
        }
    }
}
