namespace Mediinfo.WinForm.HIS.Core.Enum
{
    public class GYEnum
    {
        /// <summary>
        ///读卡方式
        /// </summary>
        public enum DuKaFS
        {
            /// <summary>
            ///实体卡读卡
            /// </summary>
            ShiTi = 10,
            /// <summary>
            ///实体读卡不交易
            /// </summary>
            ShiTiBJY = 11,
            /// <summary>
            /// 做人员交易不读卡
            /// </summary>
            JiaoYiBDK = 12,
            /// <summary>
            /// 扫码读卡
            /// </summary>
            SaoMa = 20, 
            /// <summary>
            /// 扫码读卡不做人员交易
            /// </summary>
            SaoMaBJY = 21,
            /// <summary>
            /// 做人员交易不扫码读卡
            /// </summary>
            JiaoYiBSM = 22,
            /// <summary>
            /// 刷脸读卡
            /// </summary>
            ShuaLian = 30, 
            /// <summary>
            /// 刷脸读卡不做人员交易
            /// </summary>
            ShuaLianBJY = 31,
            /// <summary>
            /// 做人员交易不刷脸读卡
            /// </summary>
            JiaoYiBSL = 32,

            /// <summary>
            /// 医保凭证读卡
            /// </summary>
            PingZheng = 40,
            /// <summary>
            /// 医保凭证读卡不做人员交易
            /// </summary>
            PingZhengBJY = 41,
            /// <summary>
            /// 做人员交易不医保凭证读卡
            /// </summary>
            JiaoYiBPZ = 42,
        }
    }
}
