using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Update
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public partial class HISGlobalHelper
    {
        #region constructor

        static HISGlobalHelper()
        {
            if (HISGlobalSetting.IsHttp)
                HttpConfigs = HISGlobalSetting.LoadHttpInfos();
            else
                GlobalSetting = HISGlobalSetting.Load();
        }

        #endregion

        #region properties

        /// <summary>
        /// FTP配置文件
        /// </summary>
        public static HISGlobalSetting GlobalSetting { get; set; }

        /// <summary>
        /// http配置文件
        /// </summary>
        public static List<HTTPUpdateConfig> HttpConfigs { get; set; }

        /// <summary>
        /// 需要进行删除操作的目录
        /// </summary>
        public static List<string> DeleteDirectories { get; set; } = new List<string>();

        #endregion

        #region methods

        public static List<HTTPUpdateConfig> CheckHttpConfig(List<HTTPUpdateConfig> hTTPUpdateConfigs)
        {
            // 检查读到得HTTP配置文件
            List<HTTPUpdateConfig> result = new List<HTTPUpdateConfig>();
            if (hTTPUpdateConfigs.Count != 0 && hTTPUpdateConfigs != null)
            {
                foreach (HTTPUpdateConfig item in hTTPUpdateConfigs)
                {
                    if (item.JIXIANMC == null || item.BanBenHao == null) continue;
                    result.Add(item);
                }
            }
            return result;
        }

        #endregion
    }
}
