using System.Configuration;
using System.IO;

namespace Mediinfo.Utility
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        static Configuration config;
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ConfigHelper()
        {
            var gconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = gconfig.AppSettings;
            if (settings.Settings["PersonalSettings"] == null)
            {
                settings.Settings.Add("PersonalSettings", "PersonalSettings.config");
                gconfig.Save(ConfigurationSaveMode.Full);
            }
            if (!File.Exists(settings.Settings["PersonalSettings"].Value))
            {
                File.Create(settings.Settings["PersonalSettings"].Value);
            }
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = settings.Settings["PersonalSettings"].Value; 
            config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            //config.AppSettings.SectionInformation.ProtectSection()
            ////即使没有修改也保存设置  
            //config.AppSettings.SectionInformation.ForceSave = true;
            //配置文件内容保存到xml  
            //config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 获取配置节
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Get(string Key)
        {
            if (config.AppSettings.Settings[Key] != null)
            {
                return config.AppSettings.Settings[Key].Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加一个配置
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void Add(string Key, string Value)
        {
            if (config.AppSettings.Settings[Key] != null)
            {
                config.AppSettings.Settings.Remove(Key);
            }
            config.AppSettings.Settings.Add(Key, Value);
            config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 移除一个配置
        /// </summary>
        /// <param name="Key"></param>
        public static void Remove(string Key)
        {
            if (config.AppSettings.Settings[Key] != null)
            {
                config.AppSettings.Settings.Remove(Key);
            }
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
