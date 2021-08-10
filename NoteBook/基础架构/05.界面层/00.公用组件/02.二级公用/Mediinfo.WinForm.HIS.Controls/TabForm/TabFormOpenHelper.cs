using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.SM;
using Mediinfo.DTO.HIS.ZJ;
using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls.FirstLevelFramework;
using Mediinfo.WinForm.HIS.Controls.SecondaryFramework;

namespace Mediinfo.WinForm.HIS.Controls.TabForm
{
    /// <summary>
    /// Tab打开方式
    /// </summary>
    public class TabFormOpenHelper : TabFormHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bingRenId"></param>
        /// <param name="bingrenzyid"></param>
        /// <param name="jiuZhenId"></param>
        /// <param name="w_BINGREN_BASE"></param>
        public static void OpenTabForm(string bingRenId, string bingrenzyid, string jiuZhenId, W_BINGRENKJ_BASE w_BINGREN_BASE)
        {
            if (LingChuangMainFormBase is LinChuangMainFormBase linChuangMainFormBase)
            {
                try
                {
                    if (w_BINGREN_BASE != null)
                    {
                        w_BINGREN_BASE.BingRenID = bingRenId;
                        w_BINGREN_BASE.BingRenZYID = bingrenzyid;
                        w_BINGREN_BASE.JiuZhenID = jiuZhenId;
                        linChuangMainFormBase.OpenTabForm(w_BINGREN_BASE);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="openChuangKou"></param>
        /// <param name="xiaoXiForm"></param>
        /// <returns></returns>
        public static bool OpenTabForm(ChuangKouXX openChuangKou, dynamic xiaoXiForm = null)
        {
            if (LingChuangMainFormBase is LinChuangMainFormBase linChuangMainFormBase)
            {
                try
                {
                    const string zhenjiankuangjiamc = "W_ZJ_KUANGJIA";
                    const string yishengkuangjiamc = "W_YS_KUANGJIA";
                    const string yizhukuangjiamc = "W_YZ_KUANGJIA";
                    const string shoumakuangjiamc = "W_SM_KUANGJIA";
                    var path = AppDomain.CurrentDomain.BaseDirectory;

                    switch (openChuangKou.XiTongLX)
                    {
                        case ChuangKouXX.EXiTongLX.MenZhen:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(zhenjiankuangjiamc))
                            {
                                if (linChuangMainFormBase.NewPageIndex < 0 && linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("诊间就诊允许同时最多接诊【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][zhenjiankuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][zhenjiankuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.JiuZhenID = openChuangKou.eJiuZhenXX.JIUZHENID;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.isTaoYong = (string)openChuangKou.FuJiaXX == "DITTO";

                                    HISClientHelper.KuangJiaBase = form;
                                    if (!linChuangMainFormBase.OpenTabForm(form))
                                        return false;
                                }
                            }
                            return true;
                        case ChuangKouXX.EXiTongLX.YiSheng:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(yishengkuangjiamc))
                            {
                                if (linChuangMainFormBase.NewPageIndex < 0 && !BaseFormCommonHelper.OpenedCaiDanDic.ContainsKey(openChuangKou.BingRenID) && linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("病区医生允许同时最多接诊【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][yishengkuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][yishengkuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.BingRenZYID = openChuangKou.BingRenZYID;
                                    form.JiuZhenID = openChuangKou.JiuZhenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.TabPageName = openChuangKou.TabPageName;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.DiaoYongYJS = openChuangKou.DiaoYongYJS;
                                    form.ChuangWeiID = openChuangKou.ChuangWeiID;
                                    form.ShouShuID = openChuangKou.ShouShuID;
                                    if (xiaoXiForm != null)
                                    {
                                        form.Tag = xiaoXiForm;
                                    }

                                    HISClientHelper.KuangJiaBase = form;
                                    linChuangMainFormBase.OpenTabForm(form);
                                }
                            }
                            return true;
                        case ChuangKouXX.EXiTongLX.YiZhu:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(yizhukuangjiamc))
                            {
                                if (linChuangMainFormBase.NewPageIndex < 0 && !BaseFormCommonHelper.OpenedCaiDanDic.ContainsKey(openChuangKou.BingRenID) && linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("病区护士允许同时最多服务【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][yizhukuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][yizhukuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.BingRenZYID = openChuangKou.BingRenZYID;
                                    form.JiuZhenID = openChuangKou.JiuZhenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.TabPageName = openChuangKou.TabPageName;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.DiaoYongYJS = openChuangKou.DiaoYongYJS;
                                    form.ChuangWeiID = openChuangKou.ChuangWeiID;
                                    HISClientHelper.KuangJiaBase = form;
                                    if (!linChuangMainFormBase.OpenTabForm(form))
                                        return false;
                                }
                            }
                            return true;


                        case ChuangKouXX.EXiTongLX.ShouMa:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(shoumakuangjiamc))
                            {
                                if (linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("手麻允许同时最多服务【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][shoumakuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][shoumakuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.BingRenZYID = openChuangKou.BingRenZYID;
                                    form.JiuZhenID = openChuangKou.JiuZhenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.TabPageName = openChuangKou.TabPageName;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.DiaoYongYJS = openChuangKou.DiaoYongYJS;
                                    form.ShouShuID = openChuangKou.ShouShuID;
                                    form.ShouShuXX = openChuangKou.ShouShuXX;
                                    HISClientHelper.KuangJiaBase = form;
                                    linChuangMainFormBase.OpenTabForm(form);
                                }
                            }
                            return true;
                    }
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openChuangKou"></param>
        /// <param name="xiaoXiForm"></param>
        /// <returns></returns>
        public static bool OpenTabForm(ChuangKouXX openChuangKou, bool isChaKanBL = false, dynamic xiaoXiForm = null)
        {
            if (LingChuangMainFormBase is LinChuangMainFormBase linChuangMainFormBase)
            {
                try
                {
                    const string zhenjiankuangjiamc = "W_ZJ_KUANGJIA";
                    const string yishengkuangjiamc = "W_YS_KUANGJIA";
                    const string yizhukuangjiamc = "W_YZ_KUANGJIA";
                    const string shoumakuangjiamc = "W_SM_KUANGJIA";
                    var path = AppDomain.CurrentDomain.BaseDirectory;

                    switch (openChuangKou.XiTongLX)
                    {
                        case ChuangKouXX.EXiTongLX.MenZhen:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(zhenjiankuangjiamc))
                            {
                                if (linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("诊间就诊允许同时最多接诊【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][zhenjiankuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][zhenjiankuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.JiuZhenID = openChuangKou.eJiuZhenXX.JIUZHENID;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.isTaoYong = (string)openChuangKou.FuJiaXX == "DITTO";

                                    HISClientHelper.KuangJiaBase = form;
                                    if (!linChuangMainFormBase.OpenTabForm(form, isChaKanBL))
                                        return false;
                                }
                            }
                            return true;
                        case ChuangKouXX.EXiTongLX.YiSheng:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(yishengkuangjiamc))
                            {
                                if (linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("病区医生允许同时最多接诊【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][yishengkuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][yishengkuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.BingRenZYID = openChuangKou.BingRenZYID;
                                    form.JiuZhenID = openChuangKou.JiuZhenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.TabPageName = openChuangKou.TabPageName;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.DiaoYongYJS = openChuangKou.DiaoYongYJS;

                                    if (xiaoXiForm != null)
                                    {
                                        form.Tag = xiaoXiForm;
                                    }

                                    HISClientHelper.KuangJiaBase = form;
                                    linChuangMainFormBase.OpenTabForm(form);
                                }
                            }
                            return true;
                        case ChuangKouXX.EXiTongLX.YiZhu:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(yizhukuangjiamc))
                            {
                                if (linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("病区护士允许同时最多服务【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][yizhukuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][yizhukuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.BingRenZYID = openChuangKou.BingRenZYID;
                                    form.JiuZhenID = openChuangKou.JiuZhenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.TabPageName = openChuangKou.TabPageName;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.DiaoYongYJS = openChuangKou.DiaoYongYJS;

                                    HISClientHelper.KuangJiaBase = form;
                                    linChuangMainFormBase.OpenTabForm(form);
                                }
                            }
                            return true;
                        case ChuangKouXX.EXiTongLX.ShouMa:
                            if (linChuangMainFormBase.Assemblys[path].ContainsKey(shoumakuangjiamc))
                            {
                                if (linChuangMainFormBase.kuangjiadic.Count >= linChuangMainFormBase.MaxPatientCount)
                                {
                                    MediMsgBox.Warn("手麻允许同时最多服务【" + linChuangMainFormBase.MaxPatientCount + "】个病人！");
                                    return false;
                                }

                                dynamic form = linChuangMainFormBase.Assemblys[path][shoumakuangjiamc].Value.CreateInstance(linChuangMainFormBase.Assemblys[path][shoumakuangjiamc].Key);
                                if (form != null)
                                {
                                    form.linChuangMainForm = linChuangMainFormBase;
                                    form.BingRenID = openChuangKou.BingRenID;
                                    form.BingRenZYID = openChuangKou.BingRenZYID;
                                    form.JiuZhenID = openChuangKou.JiuZhenID;
                                    form.MuBiaoCKInfo = openChuangKou.MuBiaoCKInfo;
                                    form.TabPageName = openChuangKou.TabPageName;
                                    form.ShowDialogCKBZ = openChuangKou.ShowDialogCKBZ;
                                    form.DiaoYongYJS = openChuangKou.DiaoYongYJS;
                                    form.ShouShuID = openChuangKou.ShouShuID;
                                    form.ShouShuXX = openChuangKou.ShouShuXX;
                                    HISClientHelper.KuangJiaBase = form;
                                    linChuangMainFormBase.OpenTabForm(form);
                                }
                            }
                            return true;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return true;
        }

        //modify by niquan 2019/9/4
        public static bool OpenTabForm(ChuangKouXX openChuangKou)
        {
            if (!OpenTabForm(openChuangKou, null))
                return false;

            return true;
        }
    }


    /// <summary>
    /// 窗口信息类
    /// </summary>
    public class ChuangKouXX
    {
        public ChuangKouXX() { }
        /// <summary>
        /// 病人窗口参数信息
        /// </summary>
        /// <param name="xitonglc">系统类型</param>
        /// <param name="id">标志ID</param>
        public ChuangKouXX(EXiTongLX xitonglc, string id)
        {
            XiTongLX = xitonglc;
            ID = id;
        }

        /// <summary>
        /// 系统类型
        /// </summary>
        public EXiTongLX XiTongLX { get; set; }
        /// <summary>
        /// 病人ID
        /// </summary>
        public string BingRenID { get; set; }
        /// <summary>
        /// 病人住院ID
        /// </summary>
        public string BingRenZYID { get; set; }
        /// <summary>
        /// 就诊ID
        /// </summary>

        public string JiuZhenID { get; set; }
        /// <summary>
        /// 右键父窗口同时打开子窗口标签
        /// </summary>
        public virtual string ShowDialogCKBZ { get; set; }

        /// <summary>
        /// 就诊信息
        /// </summary>
        public E_ZJ_JIUZHENXX eJiuZhenXX { get; set; }
        /// <summary>
        /// 病人信息
        /// </summary>
        public E_GY_BINGRENXX eBingRenXX { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 目标类型
        /// </summary>
        public EMuBiaoCK MuBiaoCK { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public object FuJiaXX { get; set; }
        /// <summary>
        /// 打开病人框架时候，是否调用预结算HR6-2337(547257)
        /// </summary>
        public bool DiaoYongYJS { set; get; } = false;
        /// <summary>
        /// 目标窗口信息
        /// </summary>
        public MuBiaoFormInformation MuBiaoCKInfo { get; set; }

        /// <summary>
        /// 床位ID
        /// </summary>
        public string ChuangWeiID { get; set; }

        /// <summary>
        /// 手术申请id
        /// </summary>
        public string ShouShuID { get; set; }

        /// <summary>
        /// 手术信息
        /// </summary>
        public E_SM_SHOUSHUXXSM ShouShuXX { get; set; }

        /// <summary>
        /// 目标窗口
        /// </summary>
        public enum EMuBiaoCK
        {
            /// <summary>
            /// 
            /// </summary>
            MenZhenZJ = 1,
            /// <summary>
            /// 
            /// </summary>
            YiZhu = 2,

        }
        /// <summary>
        /// 系统类型
        /// </summary>
        public enum EXiTongLX
        {
            /// <summary>
            /// 门诊系统
            /// </summary>
            MenZhen = 1,
            /// <summary>
            /// 医生系统
            /// </summary>
            YiSheng = 2,
            /// <summary>
            /// 医嘱系统
            /// </summary>
            YiZhu = 3,
            /// <summary>
            /// 手麻系统
            /// </summary>
            ShouMa = 4,
        }

        // add by niquan 2019/9/19
        /// <summary>
        /// 标签页名称
        /// </summary>
        public string TabPageName { get; set; }
    }

}
