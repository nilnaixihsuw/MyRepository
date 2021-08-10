using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Mediinfo.DTO.HIS.BL;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.MZ;
using Mediinfo.DTO.HIS.SM;
using Mediinfo.DTO.HIS.YJ;
using Mediinfo.DTO.HIS.ZJ;
using Mediinfo.DTO.HIS.ZY;

namespace Mediinfo.HIS.Core
{
    /// <summary>
    /// 数据中间件
    /// </summary>
    public static class DataMiddleWare
    {
        /// <summary>
        /// 病人id
        /// </summary>
        public static string BingRenID { get; set; }

        /// <summary>
        /// 住院病人id
        /// </summary>
        public static string BingRenZYID { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public static string BingRenXM { get; set; }

        /// <summary>
        /// 从工作台跳转直接打开接诊界面
        /// </summary>
        public static bool IsJumpToOpenVisit = false;

        private static BingRenInformation _bingRenInformation;

        private static ZhenJianInformation _zhenJianInformation;

        private static ZhuYuanInformation _yiZhuInformation;

        private static ShouShuInformation _shoushuInformation;

        /// <summary>
        /// 病人信息
        /// </summary>
        public static BingRenInformation bingRenInformation
        {
            get { return _bingRenInformation; }
            set { _bingRenInformation = value; }
        }

        /// <summary>
        /// 目标窗口
        /// </summary>
        public static MuBiaoFormInformation targetFormInformation { get; set; }

        /// <summary>
        /// 门诊医生站业务数据
        /// </summary>
        public static ZhenJianInformation zhenJianInformation
        {
            get { return _zhenJianInformation; }
            set { _zhenJianInformation = value; }
        }


        /// <summary>
        /// 手麻业务数据
        /// </summary>
        public static ShouShuInformation ShouShuformation
        {
            get
            {
                if (_shoushuInformation == null)
                    _shoushuInformation = new ShouShuInformation();
                return _shoushuInformation;
            }
            set
            {
                _shoushuInformation = value;
            }
        }
        /// <summary>
        /// 病区护士站业务数据
        /// </summary>
        public static ZhuYuanInformation yiZhuInformation
        {
            get
            {
                if (_yiZhuInformation == null)
                    _yiZhuInformation = new ZhuYuanInformation();
                return _yiZhuInformation;
            }
            set { _yiZhuInformation = value; }
        }

        /// <summary>
        /// 病区医生站业务数据
        /// </summary>
        public static ZhuYuanInformation yiShengInformation { get; set; }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        static DataMiddleWare()
        {
            BingRenID = null;
            BingRenZYID = null;
            BingRenXM = null;
            bingRenInformation = new BingRenInformation();
            targetFormInformation = new MuBiaoFormInformation();
            zhenJianInformation = new ZhenJianInformation();
            yiZhuInformation = new ZhuYuanInformation();
            ShouShuformation = new ShouShuInformation();
            yiShengInformation = new ZhuYuanInformation();
            HistoryModels = new List<ZhenJianGYModel>();

            PatientInformationHistory = new Dictionary<string, BingRenInformation>();
            ZhenJianInformationHistory = new Dictionary<string, ZhenJianInformation>();
            ZhuYuanInformationHistory = new Dictionary<string, ZhuYuanInformation>();
            ShouShuInformationHistory = new Dictionary<string, ShouShuInformation>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void DataMiddleWareClear()
        {
            BingRenID = null;
            BingRenZYID = null;
            BingRenXM = null;
            bingRenInformation = new BingRenInformation();
            targetFormInformation = new MuBiaoFormInformation();
            zhenJianInformation = new ZhenJianInformation();
            yiZhuInformation = new ZhuYuanInformation();
            ShouShuformation = new ShouShuInformation();
            yiShengInformation = new ZhuYuanInformation();

            PatientInformationHistory = new Dictionary<string, BingRenInformation>();
            ZhenJianInformationHistory = new Dictionary<string, ZhenJianInformation>();
            ZhuYuanInformationHistory = new Dictionary<string, ZhuYuanInformation>();
            ShouShuInformationHistory = new Dictionary<string, ShouShuInformation>();
        }

        #region 诊间
        /// <summary>
        /// 诊间使用的通用Model,当前model
        /// </summary>
        public static ZhenJianGYModel CurrentModel { get; set; }

        /// <summary>
        /// 历史诊间通用model信息
        /// </summary>
        public static List<ZhenJianGYModel> HistoryModels { get; set; }

        /// <summary>
        /// 诊间保存使用的model
        /// </summary>
        public static ZhenJianSaveModel SaveModel { get; set; }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="tuple"></param>
        public static void Init(dynamic tuple)
        {
            if (tuple != null)
            {
                // 保存上一个病人的就诊信息
                if (CurrentModel != null &&
                    HistoryModels.Count(o => o.BingRenXX.BINGRENID == CurrentModel.BingRenXX.BINGRENID) <= 0)
                {
                    HistoryModels.Add(CurrentModel);
                }

                CurrentModel = new ZhenJianGYModel
                {
                    BingRenXX = tuple.Item1,
                    BingRenGMSList = tuple.Item2,
                    ZhenDuanList = tuple.Item3,
                    ShenQingDanList = tuple.Item4,
                    ChuFang1List = tuple.Item5,
                    ChuFang2List = tuple.Item6,
                    YiJi1List = tuple.Item7,
                    YiJi2List = tuple.Rest.Item1
                };

                if (tuple.Rest.Item2 != null && tuple.Rest.Item2.Count > 0)
                {
                    CurrentModel.HistoricalPrescription = tuple.Rest.Item2;
                }
                if (tuple.Rest.Item3 != null && tuple.Rest.Item3.Count > 0)
                {
                    CurrentModel.JiuZhenXXLS = tuple.Rest.Item3;
                }
                // 保存新病人的就诊信息
                var n = HistoryModels.Count(o => o.BingRenXX.BINGRENID == CurrentModel.BingRenXX.BINGRENID);
                if (CurrentModel != null)
                {
                    if (n <= 0)
                    {
                        HistoryModels.Add(CurrentModel);
                    }
                    else if (n == 1)
                    {
                        HistoryModels.RemoveAll(o => o.BingRenXX.BINGRENID == CurrentModel.BingRenXX.BINGRENID);
                        HistoryModels.Add(CurrentModel);
                    }
                }
            }
            else
            {
                //LogHelper.Default.Error("获取门诊医生站病历/检查/检验/处方单返回异常，请检查!");
            }

            // 打开病人的时候创建保存实例
            SaveModel = new ZhenJianSaveModel();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="tuple"></param>
        public static void Init(Tuple<E_GY_BINGRENXX, List<E_GY_BINGRENGMS>, List<E_ZJ_ZHENDUAN_ICD>, List<E_GY_SHENQINGDLB>, List<E_MZ_CHUFANG1>, List<E_MZ_CHUFANG2>, List<E_MZ_YIJI1>, Tuple<List<E_MZ_YIJI2>, List<E_MZ_V_CFK2_YYTS>, List<E_ZJ_JIUZHENXX>, E_MZ_GUAHAO1, E_ZJ_JIUZHENXX>> tuple)
        {
            if (tuple != null)
            {
                // 保存上一个病人的就诊信息
                if (CurrentModel != null &&
                    HistoryModels.Count(o => o.BingRenXX.BINGRENID == CurrentModel.BingRenXX.BINGRENID) <= 0)
                {
                    HistoryModels.Add(CurrentModel);
                }

                CurrentModel = new ZhenJianGYModel
                {
                    BingRenXX = tuple.Item1,
                    BingRenGMSList = tuple.Item2,
                    ZhenDuanList = tuple.Item3,
                    ShenQingDanList = tuple.Item4,
                    ChuFang1List = tuple.Item5,
                    ChuFang2List = tuple.Item6,
                    YiJi1List = tuple.Item7,
                    YiJi2List = tuple.Rest.Item1
                };

                if (tuple.Rest.Item2 != null && tuple.Rest.Item2.Count > 0)
                {
                    CurrentModel.HistoricalPrescription = tuple.Rest.Item2;
                }
                if (tuple.Rest.Item3 != null && tuple.Rest.Item3.Count > 0)
                {
                    CurrentModel.JiuZhenXXLS = tuple.Rest.Item3;
                }


                // 保存新病人的就诊信息
                var n = HistoryModels.Count(o => o.BingRenXX.BINGRENID == CurrentModel.BingRenXX.BINGRENID);
                if (CurrentModel != null)
                {
                    if (n <= 0)
                    {
                        HistoryModels.Add(CurrentModel);
                    }
                    else if (n == 1)
                    {
                        HistoryModels.RemoveAll(o => o.BingRenXX.BINGRENID == CurrentModel.BingRenXX.BINGRENID);
                        HistoryModels.Add(CurrentModel);
                    }
                }
            }
            else
            {
                //LogHelper.Default.Error("获取门诊医生站病历/检查/检验/处方单返回异常，请检查!");
            }

            // 打开病人的时候创建保存实例
            SaveModel = new ZhenJianSaveModel();
        }
        /// <summary>
        /// 清空历史信息
        /// </summary>
        public static void ClearHistory()
        {
            HistoryModels.Clear();
        }

        /// <summary>
        /// 切换病人后初始化切换就诊信息
        /// </summary>
        public static void SelectedTabInit(string patientId)
        {
            if (HistoryModels.Count(o => o.BingRenXX.BINGRENID == patientId) <= 0 && CurrentModel.BingRenXX.BINGRENID == patientId)
            {
                HistoryModels.Add(CurrentModel);
            }

            if (HistoryModels != null && HistoryModels.Count > 0)
            {
                var m = HistoryModels.FirstOrDefault(o => o.BingRenXX.BINGRENID == patientId);
                if (m != null)
                {
                    CurrentModel = m;
                }
            }
        }

        /// <summary>
        /// 移除历史model
        /// </summary>
        /// <param name="patientId"></param>
        public static void RemoveHistory(string patientId)
        {
            if (HistoryModels.Count(o => o.BingRenXX.BINGRENID == patientId) > 0)
            {
                HistoryModels.RemoveAll(o => o.BingRenXX.BINGRENID == patientId);
            }
        }

        #endregion

        /// <summary>
        /// 多个病人Tab页切换需要保存病人基础数据
        /// </summary>
        private static Dictionary<string, BingRenInformation> PatientInformationHistory { get; set; }
        /// <summary>
        /// 多个病人Tab页切换需要保存业务基础数据(门诊数据)
        /// </summary>
        private static Dictionary<string, ZhenJianInformation> ZhenJianInformationHistory { get; set; }
        /// <summary>
        /// 多个病人Tab页切换需要保存业务基础数据(住院数据)
        /// </summary>
        private static Dictionary<string, ZhuYuanInformation> ZhuYuanInformationHistory { get; set; }

        private static Dictionary<string, ShouShuInformation> ShouShuInformationHistory { get; set; }
        /// <summary>
        /// 初始化门诊中病人和业务的框架数据(嘉兴妇保)
        /// </summary>
        /// <param name="initType">初始化类型，0正常初始化，1非本系统初始化</param>
        /// <param name="item1">病人信息(只会有一个病人)</param>
        /// <param name="item2">病人过敏史信息</param>
        /// <param name="item3">病人ID</param>
        /// <param name="item4">就诊ID</param>
        /// <param name="item5">套用标志</param>
        /// <param name="item6">就诊信息</param>
        /// <param name="item7">价格体系</param>
        public static void InitMZFrameworkData(int initType, E_GY_BINGRENXX item1, List<E_GY_BINGRENGMS> item2, string item3,
            string item4, bool item5, E_ZJ_JIUZHENXX item6, string item7)
        {
            if (_bingRenInformation != null)
            {
                // 保存新病人的
                if (initType == 0)
                {
                    _bingRenInformation = new BingRenInformation { BingRenXX = item1, GuoMinShi = item2 };
                }

                _zhenJianInformation = new ZhenJianInformation
                {
                    BingRenID = item3,
                    JIUZHENID = item4,
                    MZ_ISTAOYONG = item5,
                    MZ_JIUZHENXXENTITY = item6.MapTo<E_ZJ_JIUZHENXX>(),
                    MZ_JIAGETX = item7,
                    MZ_GUAHAOXXENTITY = null
                };

                if (initType == 0 && _bingRenInformation.BingRenXX?.BINGRENID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (PatientInformationHistory.ContainsKey(_bingRenInformation.BingRenXX.BINGRENID))
                        // 如果已经存在就先移除
                        PatientInformationHistory.Remove(_bingRenInformation.BingRenXX.BINGRENID);
                    PatientInformationHistory.Add(_bingRenInformation.BingRenXX.BINGRENID, _bingRenInformation);
                }

                if (_zhenJianInformation?.BingRenID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (ZhenJianInformationHistory.ContainsKey(_zhenJianInformation.BingRenID))
                        // 如果已经存在就先移除
                        ZhenJianInformationHistory.Remove(_zhenJianInformation.BingRenID);
                    ZhenJianInformationHistory.Add(_zhenJianInformation.BingRenID, _zhenJianInformation);
                }

                if (initType == 0 && _yiZhuInformation?.BingRenID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (ZhuYuanInformationHistory.ContainsKey(_yiZhuInformation.BingRenID))
                        // 如果已经存在就先移除
                        ZhuYuanInformationHistory.Remove(_yiZhuInformation.BingRenID);
                    ZhuYuanInformationHistory.Add(_yiZhuInformation.BingRenID, _yiZhuInformation);
                }

            }
        }

        /// <summary>
        /// 初始化门诊中病人和业务的框架数据（浙江省肿瘤）
        /// </summary>
        /// <param name="initType">初始化类型，0正常初始化，1非本系统初始化</param>
        /// <param name="item1">病人信息(只会有一个病人)</param>
        /// <param name="item2">病人过敏史信息</param>
        /// <param name="item3">病人ID</param>
        /// <param name="item4">就诊ID</param>
        /// <param name="item5">套用标志</param>
        /// <param name="item6">就诊信息</param>
        /// <param name="item7">价格体系</param>
        /// <param name="shengMingTZList">生命体征</param>
        public static void InitMZFrameworkData(int initType, E_GY_BINGRENXX item1, List<E_GY_BINGRENGMS> item2, string item3,
           string item4, bool item5, E_ZJ_JIUZHENXX item6, string item7, List<int> shengMingTZList)
        {
            if (_bingRenInformation != null)
            {
                // 保存新病人的
                if (initType == 0)
                {
                    _bingRenInformation = new BingRenInformation { BingRenXX = item1, GuoMinShi = item2 };
                }

                _zhenJianInformation = new ZhenJianInformation
                {
                    BingRenID = item3,
                    JIUZHENID = item4,
                    MZ_ISTAOYONG = item5,
                    MZ_JIUZHENXXENTITY = item6.MapTo<E_ZJ_JIUZHENXX>(),
                    MZ_JIAGETX = item7
                };
                // 生命体征
                if (shengMingTZList != null && shengMingTZList.Count > 2)
                {
                    _zhenJianInformation.MZ_JIUZHENXXENTITY.SHOUSUOYA = shengMingTZList[0];
                    _zhenJianInformation.MZ_JIUZHENXXENTITY.SHUZHANGYA = shengMingTZList[1];
                    _zhenJianInformation.MZ_JIUZHENXXENTITY.MAIBO = shengMingTZList[2];
                }

                if (initType == 0 && _bingRenInformation.BingRenXX?.BINGRENID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (PatientInformationHistory.ContainsKey(_bingRenInformation.BingRenXX.BINGRENID))
                        // 如果已经存在就先移除
                        PatientInformationHistory.Remove(_bingRenInformation.BingRenXX.BINGRENID);
                    PatientInformationHistory.Add(_bingRenInformation.BingRenXX.BINGRENID, _bingRenInformation);
                }

                if (_zhenJianInformation?.BingRenID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (ZhenJianInformationHistory.ContainsKey(_zhenJianInformation.BingRenID))
                        // 如果已经存在就先移除
                        ZhenJianInformationHistory.Remove(_zhenJianInformation.BingRenID);
                    ZhenJianInformationHistory.Add(_zhenJianInformation.BingRenID, _zhenJianInformation);
                }

                if (initType == 0 && _yiZhuInformation?.BingRenID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (ZhuYuanInformationHistory.ContainsKey(_yiZhuInformation.BingRenID))
                        // 如果已经存在就先移除
                        ZhuYuanInformationHistory.Remove(_yiZhuInformation.BingRenID);
                    ZhuYuanInformationHistory.Add(_yiZhuInformation.BingRenID, _yiZhuInformation);
                }

            }
        }



        /// <summary>
        /// 初始化门诊中病人和业务的框架数据（宁波、嘉兴中医院）
        /// </summary>
        /// <param name="initType">初始化类型，0正常初始化，1非本系统初始化</param>
        /// <param name="item1">病人信息(只会有一个病人)</param>
        /// <param name="item2">病人过敏史信息</param>
        /// <param name="item3">病人ID</param>
        /// <param name="item4">就诊ID</param>
        /// <param name="item5">套用标志</param>
        /// <param name="item6">就诊信息</param>
        /// <param name="item7">价格体系</param>
        public static void InitMZFrameworkData(int initType, E_GY_BINGRENXX item1, List<E_GY_BINGRENGMS> item2, string item3,
            string item4, bool item5, E_ZJ_JIUZHENXX item6, string item7, E_MZ_GUAHAO1 item8)
        {
            if (_bingRenInformation != null)
            {
                // 保存新病人的
                if (initType == 0)
                {
                    _bingRenInformation = new BingRenInformation { BingRenXX = item1, GuoMinShi = item2 };
                }

                _zhenJianInformation = new ZhenJianInformation
                {
                    BingRenID = item3,
                    JIUZHENID = item4,
                    MZ_ISTAOYONG = item5,
                    MZ_JIUZHENXXENTITY = item6.MapTo<E_ZJ_JIUZHENXX>(),
                    MZ_JIAGETX = item7,
                    MZ_GUAHAOXXENTITY = item8 != null ? item8.MapTo<E_MZ_GUAHAO1>() : null
                };

                if (initType == 0 && _bingRenInformation.BingRenXX?.BINGRENID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (PatientInformationHistory.ContainsKey(_bingRenInformation.BingRenXX.BINGRENID))
                        // 如果已经存在就先移除
                        PatientInformationHistory.Remove(_bingRenInformation.BingRenXX.BINGRENID);
                    PatientInformationHistory.Add(_bingRenInformation.BingRenXX.BINGRENID, _bingRenInformation);
                }

                if (_zhenJianInformation?.BingRenID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (ZhenJianInformationHistory.ContainsKey(_zhenJianInformation.BingRenID))
                        // 如果已经存在就先移除
                        ZhenJianInformationHistory.Remove(_zhenJianInformation.BingRenID);
                    ZhenJianInformationHistory.Add(_zhenJianInformation.BingRenID, _zhenJianInformation);
                }

                if (initType == 0 && _yiZhuInformation?.BingRenID != null)
                {
                    // 判断数据集中是否存在该新病人的数据
                    if (ZhuYuanInformationHistory.ContainsKey(_yiZhuInformation.BingRenID))
                        // 如果已经存在就先移除
                        ZhuYuanInformationHistory.Remove(_yiZhuInformation.BingRenID);
                    ZhuYuanInformationHistory.Add(_yiZhuInformation.BingRenID, _yiZhuInformation);
                }

            }
        }


        /// <summary>
        /// 初始化门诊中病人和业务的框架数据
        /// </summary>
        /// <param name="initType">初始化类型，0正常初始化，1非本系统初始化</param>
        /// <param name="gyBingRenXx">病人信息(只会有一个病人)</param>
        /// <param name="bingRenGms">病人过敏史信息</param>
        /// <param name="bingRenId"></param>
        /// <param name="bingRenZyId"></param>
        /// <param name="bingRenXm"></param>
        /// <param name="zyBingRenXx"></param>
        /// <param name="zaiYuanZt"></param>
        /// <param name="dangQianKs"></param>
        /// <param name="dangQianBq"></param>
        /// <param name="yiLiaoZID"></param>
        /// <param name="benYiLz"></param>
        /// <param name="benKeShi"></param>
        /// <param name="BenBingQu"></param>
        /// <param name="yinErBz"></param>
        /// <param name="yingErXx"></param>
        /// <param name="buXuJcysczqx"></param>
        /// <param name="huiZhenBz"></param>
        /// <param name="guiDangBz"></param>
        /// <param name="suoDingBz"></param>
        /// <param name="xianShiBrxx"></param>
        public static void InitZYFrameworkData(int initType, E_GY_BINGRENXX gyBingRenXx, List<E_GY_BINGRENGMS> bingRenGms,
                    string bingRenId, string bingRenZyId, string bingRenXm, E_ZY_BINGRENXX_EX zyBingRenXx,
                string zaiYuanZt, string dangQianKs, string dangQianBq, string yiLiaoZID, bool benYiLz, bool benKeShi, bool BenBingQu, int yinErBz, E_ZY_YINGERXX yingErXx,
                    bool buXuJcysczqx, bool huiZhenBz, int guiDangBz, int suoDingBz, E_YS_ZHUYUANBRXX xianShiBrxx)
        {
            if (initType == 0)
            {
                _bingRenInformation = new BingRenInformation { BingRenXX = gyBingRenXx, GuoMinShi = bingRenGms };
            }
            if (initType == 0 && _bingRenInformation.BingRenXX?.BINGRENID != null)
            {
                // 判断数据集中是否存在该新病人的数据
                if (PatientInformationHistory.ContainsKey(_bingRenInformation.BingRenXX.BINGRENID))
                    // 如果已经存在就先移除
                    PatientInformationHistory.Remove(_bingRenInformation.BingRenXX.BINGRENID);
                PatientInformationHistory.Add(_bingRenInformation.BingRenXX.BINGRENID, _bingRenInformation);
            }
            if (initType == 0 && _zhenJianInformation?.BingRenID != null)
            {
                // 判断数据集中是否存在该新病人的数据
                if (ZhenJianInformationHistory.ContainsKey(_zhenJianInformation.BingRenID))
                    // 如果已经存在就先移除
                    ZhenJianInformationHistory.Remove(_zhenJianInformation.BingRenID);
                ZhenJianInformationHistory.Add(_zhenJianInformation.BingRenID, _zhenJianInformation);
            }

            _yiZhuInformation = new ZhuYuanInformation
            {
                BingRenID = bingRenId,
                BingRenZYID = bingRenZyId,
                BingRenXM = bingRenXm,
                ZhuYuanBR = zyBingRenXx,
                ZaiYuanZT = zaiYuanZt,
                DangQianKS = dangQianKs,
                DangQianBQ = dangQianBq,
                YiLiaoZID = yiLiaoZID,
                BenYiLZ = benYiLz,
                BenKeShi = benKeShi,
                BenBingQu = BenBingQu,
                YingErBZ = yinErBz,
                YingErXX = yingErXx,
                BuXuJCYSCZQX = buXuJcysczqx,
                HuiZhenBZ = huiZhenBz,
                GuiDangBZ = guiDangBz,
                SuoDingBZ = suoDingBz,
                XianShiBRXX = xianShiBrxx
            };
            if (_yiZhuInformation?.BingRenID != null)
            {
                // 判断数据集中是否存在该新病人的数据
                if (ZhuYuanInformationHistory.ContainsKey(_yiZhuInformation.BingRenID))
                    // 如果已经存在就先移除
                    ZhuYuanInformationHistory.Remove(_yiZhuInformation.BingRenID);
                ZhuYuanInformationHistory.Add(_yiZhuInformation.BingRenID, _yiZhuInformation);
            }
        }


        public static void InitSMFrameworkData(int initType, E_GY_BINGRENXX gyBingRenXx, List<E_GY_BINGRENGMS> bingRenGms,
              string bingRenId, string bingRenZyId, string shouShuID, string bingRenXm, E_ZY_BINGRENXX_EX zyBingRenXx,
          string zaiYuanZt, string dangQianKs, string dangQianBq, string yiLiaoZID, bool benYiLz, bool benKeShi, bool BenBingQu, int yinErBz, E_ZY_YINGERXX yingErXx,
              bool buXuJcysczqx, bool huiZhenBz, int guiDangBz, int suoDingBz, E_YS_ZHUYUANBRXX xianShiBrxx, E_SM_SHOUSHUXXSM shouShuXX)
        {
            if (initType == 0)
            {
                _bingRenInformation = new BingRenInformation { BingRenXX = gyBingRenXx, GuoMinShi = bingRenGms };
            }
            if (initType == 0 && _bingRenInformation.BingRenXX?.BINGRENID != null)
            {
                // 判断数据集中是否存在该新病人的数据
                if (PatientInformationHistory.ContainsKey(_bingRenInformation.BingRenXX.BINGRENID))
                    // 如果已经存在就先移除
                    PatientInformationHistory.Remove(_bingRenInformation.BingRenXX.BINGRENID);
                PatientInformationHistory.Add(_bingRenInformation.BingRenXX.BINGRENID, _bingRenInformation);
            }
            if (initType == 0 && _zhenJianInformation?.BingRenID != null)
            {
                // 判断数据集中是否存在该新病人的数据
                if (ZhenJianInformationHistory.ContainsKey(_zhenJianInformation.BingRenID))
                    // 如果已经存在就先移除
                    ZhenJianInformationHistory.Remove(_zhenJianInformation.BingRenID);
                ZhenJianInformationHistory.Add(_zhenJianInformation.BingRenID, _zhenJianInformation);
            }

            _shoushuInformation = new ShouShuInformation
            {
                BingRenID = bingRenId,
                BingRenZYID = bingRenZyId,
                ShouShuID = shouShuID,
                BingRenXM = bingRenXm,
                ZhuYuanBR = zyBingRenXx,
                ZaiYuanZT = zaiYuanZt,
                DangQianKS = dangQianKs,
                DangQianBQ = dangQianBq,
                YiLiaoZID = yiLiaoZID,
                BenYiLZ = benYiLz,
                BenKeShi = benKeShi,
                BenBingQu = BenBingQu,
                YingErBZ = yinErBz,
                YingErXX = yingErXx,
                BuXuJCYSCZQX = buXuJcysczqx,
                HuiZhenBZ = huiZhenBz,
                GuiDangBZ = guiDangBz,
                SuoDingBZ = suoDingBz,
                XianShiBRXX = xianShiBrxx,
                ShouShuXX = shouShuXX
            };
            if (_shoushuInformation?.BingRenID != null)
            {
                // 判断数据集中是否存在该新病人的数据
                if (ShouShuInformationHistory.ContainsKey(_shoushuInformation.BingRenID))
                    // 如果已经存在就先移除
                    ShouShuInformationHistory.Remove(_shoushuInformation.BingRenID);
                ShouShuInformationHistory.Add(_shoushuInformation.BingRenID, _shoushuInformation);
            }
        }

        /// <summary>
        /// 创建病人tab页时需要调用
        /// </summary>
        public static void CreatePatientPage(string patientId)
        {

        }

        /// <summary>
        /// 切换病人tab页时需要调用
        /// </summary>
        public static void SelectPatientPageChanged(string patientId)
        {
            bingRenInformation = PatientInformationHistory.TryGetValue(patientId, out BingRenInformation oValue) ? oValue : null;
            zhenJianInformation = ZhenJianInformationHistory.TryGetValue(patientId, out ZhenJianInformation pValue) ? pValue : null;
            yiZhuInformation = ZhuYuanInformationHistory.TryGetValue(patientId, out ZhuYuanInformation qValue) ? qValue : null;
            ShouShuformation = ShouShuInformationHistory.TryGetValue(patientId, out ShouShuInformation sValue) ? sValue : null;
        }
        /// <summary>
        /// 关闭病人Tab页时需要调用
        /// </summary>
        public static void RemovePatientPage(string patientId)
        {
            // 判断数据集中是否存在该新病人的数据
            if (PatientInformationHistory.ContainsKey(patientId))
                PatientInformationHistory.Remove(patientId);

            if (ZhenJianInformationHistory.ContainsKey(patientId))
                ZhenJianInformationHistory.Remove(patientId);

            if (ZhuYuanInformationHistory.ContainsKey(patientId))
                ZhuYuanInformationHistory.Remove(patientId);
            if (ShouShuInformationHistory.ContainsKey(patientId))
                ShouShuInformationHistory.Remove(patientId);

        }
    }



    /// <summary>
    /// 病人信息
    /// </summary>
    [Serializable]
    public class BingRenInformation
    {
        /// <summary>
        /// 病人信息
        /// </summary>
        public E_GY_BINGRENXX BingRenXX { get; set; }

        /// <summary>
        /// 病人过敏史
        /// </summary>
        public List<E_GY_BINGRENGMS> GuoMinShi { get; set; }
        /// <summary>
        /// 手术信息
        /// </summary>
        public E_SM_SHOUSHUXXSM ShouShuXX { get; set; }
    }

    /// <summary>
    /// 目标窗口
    /// </summary>

    [Serializable]
    public class MuBiaoFormInformation
    {  /// <summary>
       /// 功能对象名称
       /// </summary>
        public string GongNengDXMC { set; get; }
        /// <summary>
        /// 功能中文名
        /// </summary>
        public string GongNengZWMC { set; get; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public string GongNengID { set; get; }
        /// <summary>
        /// 功能窗口名称
        /// </summary>
        public string GongNengCKMC { set; get; }
    }

    /// <summary>
    /// 诊间框架
    /// </summary>

    [Serializable]
    public class ZhenJianInformation
    {
        /// <summary>
        /// 病人id
        /// </summary>
        public string BingRenID { get; set; }

        /// <summary>
        /// 住院病人id
        /// </summary>
        public string BingRenZYID { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string BingRenXM { get; set; }
        /// <summary>
        /// 就诊id
        /// </summary>
        public string JIUZHENID { get; set; }
        /// <summary>
        /// 就诊信息
        /// </summary>
        public E_ZJ_JIUZHENXX MZ_JIUZHENXXENTITY { get; set; }

        /// <summary>
        /// 就诊信息
        /// </summary>
        public E_MZ_GUAHAO1 MZ_GUAHAOXXENTITY { get; set; }
        /// <summary>
        /// 价格体系
        /// </summary>
        public string MZ_JIAGETX { get; set; }
        /// <summary>
        /// 是否套用
        /// </summary>
        public bool MZ_ISTAOYONG { get; set; }
        /// <summary>
        /// 医保特殊病种信息
        /// </summary>
        public string MZ_YIBAOTESHUBZXX { get; set; }
        /// <summary>
        /// 医保特殊病种属性
        /// </summary>
        public string MZ_YIBAOTESHUBZSX { get; set; }
    }

    /// <summary>
    /// 护士框架
    /// </summary>

    [Serializable]
    public class ZhuYuanInformation
    {
        /// <summary>
        /// 病人id
        /// </summary>
        public string BingRenID { get; set; }

        /// <summary>
        /// 住院病人id
        /// </summary>
        public string BingRenZYID { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string BingRenXM { get; set; }
        /// <summary>
        /// 婴儿标志
        /// </summary>
        public int YingErBZ { get; set; }

        /// <summary>
        /// 归档标志
        /// </summary>
        public int GuiDangBZ { get; set; }

        /// <summary>
        /// 锁定标志 锁定类型分为：病人锁定、病历锁定，病历锁定时需指定病历类型
        /// 病人锁定时，需指定锁定级别：可浏览病历、不可浏览病历
        /// </summary>
        public int SuoDingBZ { get; set; }

        /// <summary>
        /// 当前科室（病人所在的科室）
        /// </summary>
        public string ZaiYuanZT { get; set; }

        /// <summary>
        /// 当前科室（病人当前所在的科室）
        /// </summary>
        public string DangQianKS { get; set; }

        /// <summary>
        /// 当前病区（病人当前所在的病区）
        /// </summary>
        public string DangQianBQ { get; set; }

        /// <summary>
        /// 医疗组ID(病人当前所在的医疗组)
        /// </summary>
        public string YiLiaoZID { get; set; }

        /// <summary>
        /// 本医疗组
        /// </summary>
        public bool BenYiLZ { get; set; }

        /// <summary>
        /// 本科室
        /// </summary>
        public bool BenKeShi { get; set; }

        /// <summary>
        /// 本病区
        /// </summary>
        public bool BenBingQu { get; set; }

        /// <summary>
        /// 不需检查医生操作权限
        /// 会诊病人不需要检查;普通病人均需要检查医生操作权限
        /// </summary>
        public bool BuXuJCYSCZQX { get; set; }

        /// <summary>
        /// 会诊标志
        ///我甲科室 发起会诊到乙科室 会诊列表里面进去的 这个乙科室医生对甲科室那个病人下了医嘱以后必须要甲科室的主治医生同意后护士那边才可以核对执行
        /// </summary>
        public bool HuiZhenBZ { get; set; }

        /// <summary>
        /// 住院病人信息
        /// </summary>
        public E_ZY_BINGRENXX_EX ZhuYuanBR { get; set; }

        /// <summary>
        /// 住院婴儿信息
        /// </summary>
        public E_ZY_YINGERXX YingErXX { get; set; }

        /// <summary>
        /// 归档信息
        /// </summary>
        public E_BL_BINGLIGDBZ GuiDangXX { get; set; }

        /// <summary>
        /// 显示信息
        /// </summary>
        public E_YS_ZHUYUANBRXX XianShiBRXX { get; set; }

        /// <summary>
        /// 多重耐药
        /// </summary>
        public int duoChongNYBZ { get; set; }


        public string ZyZhenDuanMC { get; set; }



    }


    /// <summary>
    /// 手术框架
    /// </summary>

    [Serializable]
    public class ShouShuInformation
    {
        /// <summary>
        /// 病人id
        /// </summary>
        public string BingRenID { get; set; }

        /// <summary>
        /// 住院病人id
        /// </summary>
        public string BingRenZYID { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string BingRenXM { get; set; }
        /// <summary>
        /// 婴儿标志
        /// </summary>
        public int YingErBZ { get; set; }

        /// <summary>
        /// 归档标志
        /// </summary>
        public int GuiDangBZ { get; set; }

        /// <summary>
        /// 锁定标志 锁定类型分为：病人锁定、病历锁定，病历锁定时需指定病历类型
        /// 病人锁定时，需指定锁定级别：可浏览病历、不可浏览病历
        /// </summary>
        public int SuoDingBZ { get; set; }

        /// <summary>
        /// 当前科室（病人所在的科室）
        /// </summary>
        public string ZaiYuanZT { get; set; }

        /// <summary>
        /// 当前科室（病人当前所在的科室）
        /// </summary>
        public string DangQianKS { get; set; }

        /// <summary>
        /// 当前病区（病人当前所在的病区）
        /// </summary>
        public string DangQianBQ { get; set; }

        /// <summary>
        /// 医疗组ID(病人当前所在的医疗组)
        /// </summary>
        public string YiLiaoZID { get; set; }

        /// <summary>
        /// 本医疗组
        /// </summary>
        public bool BenYiLZ { get; set; }

        /// <summary>
        /// 本科室
        /// </summary>
        public bool BenKeShi { get; set; }

        /// <summary>
        /// 本病区
        /// </summary>
        public bool BenBingQu { get; set; }

        /// <summary>
        /// 不需检查医生操作权限
        /// 会诊病人不需要检查;普通病人均需要检查医生操作权限
        /// </summary>
        public bool BuXuJCYSCZQX { get; set; }

        /// <summary>
        /// 会诊标志
        ///我甲科室 发起会诊到乙科室 会诊列表里面进去的 这个乙科室医生对甲科室那个病人下了医嘱以后必须要甲科室的主治医生同意后护士那边才可以核对执行
        /// </summary>
        public bool HuiZhenBZ { get; set; }

        /// <summary>
        /// 住院病人信息
        /// </summary>
        public E_ZY_BINGRENXX_EX ZhuYuanBR { get; set; }

        /// <summary>
        /// 住院婴儿信息
        /// </summary>
        public E_ZY_YINGERXX YingErXX { get; set; }

        /// <summary>
        /// 归档信息
        /// </summary>
        public E_BL_BINGLIGDBZ GuiDangXX { get; set; }

        /// <summary>
        /// 显示信息
        /// </summary>
        public E_YS_ZHUYUANBRXX XianShiBRXX { get; set; }

        /// <summary>
        /// 多重耐药
        /// </summary>
        public int duoChongNYBZ { get; set; }


        public string ZyZhenDuanMC { get; set; }

        /// <summary>
        /// 手术id
        /// </summary>
        public string ShouShuID { get; set; }
        /// <summary>
        /// 手术信息
        /// </summary>
        public E_SM_SHOUSHUXXSM ShouShuXX { get; set; }
    }

    public class ZhenJianGYModel
    {
        /// <summary>
        /// 
        /// </summary>
        public E_GY_BINGRENXX BingRenXX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_GY_BINGRENGMS> BingRenGMSList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_ZJ_ZHENDUAN_ICD> ZhenDuanList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_GY_SHENQINGDLB> ShenQingDanList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_MZ_CHUFANG1> ChuFang1List { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_MZ_CHUFANG2> ChuFang2List { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_MZ_YIJI1> YiJi1List { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<E_MZ_YIJI2> YiJi2List { get; set; }

        /// <summary>
        /// 历史处方信息
        /// </summary>
        public List<E_MZ_V_CFK2_YYTS> HistoricalPrescription { get; set; }

        /// <summary>
        /// 历史就诊信息
        /// </summary>
        public List<E_ZJ_JIUZHENXX> JiuZhenXXLS { get; set; }

    }

    /// <summary>
    /// 用于诊间保存数据
    /// </summary>
    public class ZhenJianSaveModel
    {
        /// <summary>
        /// 病人信息
        /// </summary>
        public E_GY_BINGRENXX BingRenXX { get; set; }
        /// <summary>
        /// 就诊信息
        /// </summary>
        public E_ZJ_JIUZHENXX JiuZhenXX { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
        public List<E_ZJ_ZHENDUAN_ICD> ZhenDuanList { get; set; }
        /// <summary>
        /// 处方
        /// </summary>
        public List<E_MZ_CHUFANG1> ChuFang1List { get; set; }
        /// <summary>
        /// 处方明细
        /// </summary>
        public List<E_MZ_CHUFANG2> ChuFang2List { get; set; }
        /// <summary>
        /// 处方附加费
        /// </summary>
        public List<E_MZ_YIJI1> YiJi1List { get; set; }
        /// <summary>
        /// 处方附加费明细
        /// </summary>
        public List<E_MZ_YIJI2> YiJi2List { get; set; }
        /// <summary>
        /// 申请单附加费
        /// </summary>
        public List<E_MZ_YIJI1> ShenQingDYJ1List { get; set; }
        /// <summary>
        /// 申请单附加费明细
        /// </summary>
        public List<E_MZ_YIJI2> ShenQingDYJ2List { get; set; }
        /// <summary>
        /// 申请单处方
        /// </summary>
        public List<E_MZ_CHUFANG1> ShenQingDCF1List { get; set; }
        /// <summary>
        /// 申请单处方明细
        /// </summary>
        public List<E_MZ_CHUFANG2> ShenQingDCF2List { get; set; }
        /// <summary>
        /// 医技申请单
        /// </summary>
        public List<E_YJ_SHENQINGDAN> YiJiSQDList { get; set; }
        /// <summary>
        /// 医技申请单部位
        /// </summary>
        public List<E_YJ_SHENQINGDBW> ShenQingDBWList { get; set; }
        /// <summary>
        /// 检验申请单
        /// </summary>
        public List<E_YJ_JIANYANSQD> JianYanSQDList { get; set; }
        /// <summary>
        /// 病人医嘱
        /// </summary>
        public List<E_ZJ_BINGRENYZ> BingRenYZList { get; set; }
        /// <summary>
        /// 检验项目
        /// </summary>
        public List<E_YZ_JIANYANXM> JianYanXMList { get; set; }
        /// <summary>
        /// 申请单材料费用对应
        /// </summary>
        public List<E_JY_SHENQINGDCLFYDY> ShenQingDCLFYDYList { get; set; }
        /// <summary>
        /// 病历记录内容
        /// </summary>
        public List<E_BL_BINGLIJLNR_HTML> BingLiNRList { get; set; }
        /// <summary>
        /// 申请单内容
        /// </summary>
        public List<E_YJ_SHENQINGDNR> ShenQingDNRList { get; set; }
        /// <summary>
        /// 申请单样本
        /// </summary>
        public List<E_YJ_SHENQINGDYB> ShenQingDYBList { get; set; }

        #region 结构病历
        /// <summary>
        /// 病历记录
        /// </summary>
        public E_BL_BINGLIJL BingLiJL { get; set; }

        /// <summary>
        /// 病历记录内容
        /// </summary>
        public E_BL_BINGLIJLNR BingLiJLNR { get; set; }

        /// <summary>
        /// 病历记录明细
        /// </summary>
        public List<E_BL_BINGLIJLMX> BingLiJLMXList { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            BingRenXX = null;
            JiuZhenXX = null;
            ZhenDuanList = null;
            ChuFang1List = null;
            ChuFang2List = null;
            YiJi1List = null;
            YiJi2List = null;
            ShenQingDYJ1List = null;
            ShenQingDYJ2List = null;
            ShenQingDCF1List = null;
            ShenQingDCF2List = null;
            YiJiSQDList = null;
            ShenQingDBWList = null;
            JianYanSQDList = null;
            BingRenYZList = null;
            JianYanXMList = null;
            ShenQingDCLFYDYList = null;
            BingLiNRList = null;
            ShenQingDNRList = null;
            BingLiJL = null;
            BingLiJLNR = null;
            BingLiJLMXList = null;
        }

    }


}
