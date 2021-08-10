using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mediinfo.WinForm.HIS.Update
{
    /// <summary>
    /// 客户端配置文件辅助类（只供四层，五层使用，三层的Service不要调用）
    /// 特别注意：请不要随便更改TypeName,ElementName等属性，包括大小写！！！！
    /// 变种单例模式，因为xml序列化必须要求public的序列化方法，暂时不考虑线程安全
    /// </summary>

    [XmlType(TypeName = "HISClientSetting")]
    public class HISClientSetting : HISSetting
    {
        private static HISClientSetting _instance = null;

        /// <summary>
        /// 请不要直接从这种方式初始化
        /// </summary>
        public HISClientSetting() : base()
        {
 
        }

        /// <summary>
        /// 加载客户端配置文件（外部调用的唯一方式）
        /// </summary>
        /// <returns></returns>
        public static HISClientSetting Load()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient");
            if (_instance == null)
                _instance = Load<HISClientSetting>(AppDomain.CurrentDomain.BaseDirectory+ "AssemblyClient\\HISClientSetting.xml");

            return _instance;
        }


       


    }



 


}
