using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.Message
{
    public partial class ChaKanXXRZ : MediDialog
    {
        #region fields

        public XiaoXiRZ yongHuRZ = new XiaoXiRZ();
        public bool RenZhenJG = false;
        JCJGZhiGongService GYZhiGongService = new JCJGZhiGongService();
        List<E_GY_ZHIGONGXX> listZhiGong = new List<E_GY_ZHIGONGXX>();

        #endregion

        #region constructor

        public ChaKanXXRZ()
        {
            InitializeComponent();
        }

        #endregion

        #region methods

        /// <summary>
        /// 住院用户校验
        /// </summary>
        /// <param name="yongHuRZ">用户认证类实体</param>
        /// <returns>0 成功  -1 用户名 -2 密码问题</returns>
        private int ZhuYuanYHJY(XiaoXiRZ yongHuRZ)
        {
            var result = GYZhiGongService.GetZhiGongKSByZhiGongID(yongHuRZ.ZhiGongID);
            if (result.ReturnCode != ReturnCode.SUCCESS)
            {
                yongHuRZ.Message = "住院用户校验时，取职工科室失败" + result.ReturnMessage;
                return -1;
            }
            var zhiGongKS = result.Return;

            switch (yongHuRZ.ZhiGongLB)
            {
                case "3":
                case "4":
                    // 取收费费用审核权限人
                    var quanXian = GYZhiGongService.GetYongHuCKQXByID(yongHuRZ.ZhiGongLB == "3" ? "18020408.uo_1.cb_shenhe" : "18020409.uo_1.cb_shenhe");
                    if (quanXian.ReturnCode != ReturnCode.SUCCESS)
                    {
                        yongHuRZ.Message = "职工帮助类里取手术费用审核权限人失败" + quanXian.ReturnMessage;
                    }
                    if (quanXian.Return.FirstOrDefault() == null)
                    {
                        yongHuRZ.Message = "用户没有审核权限";
                    }
                    break;
                case "2":
                    // 病区取护士
                    if (zhiGongKS.Where(o => o.KESHIBQID == yongHuRZ.BingQuID && o.KESHIBQBZ == 2).Count() < 1)
                    {
                        yongHuRZ.Message = "用户不是本病区护士";
                    }
                    break;
                case "1":
                    // 医生
                    if (zhiGongKS.Where(o => o.KESHIBQBZ == 1).Count() < 1)
                    {
                        yongHuRZ.Message = "用户是职工类别不属于医生!";
                    }
                    break;
                case "5":
                    // 所有                   
                    break;
            }
            if (!string.IsNullOrEmpty(yongHuRZ.Message))
            {
                return -1;
            }

            string miMa = yongHuRZ.YongHuMM;
            if (GYCanShuHelper.GetCanShu("公用_用户密码是否加密", "0") != "0")
                miMa = SHA256.Encrypt(miMa);

            var yanZheng = GYZhiGongService.GetYongHuXXByYongHuID(yongHuRZ.ZhiGongID);
            if (yanZheng.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                yongHuRZ.Message = "取用户信息失败" + yanZheng.ReturnMessage;
                return -1;
            }
            else
            {
                if (yanZheng.Return.Where(o => o.MIMA == miMa && o.TINGYONGBZ != 1).Count() < 1)
                {
                    yongHuRZ.Message = "用户认证失败，请输入正确的密码!";
                    return -2;
                }
            }
            return 0;
        }

        #endregion

        #region events

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChaKanXXRZ_Shown(object sender, EventArgs e)
        {
            mediTextBox_YongHuMing.Text = yongHuRZ.ZhiGongGH;

            // 如果用户区分标志是2表示当前认证用户只能和登录用户一样。
            if (yongHuRZ.YongHuQFBZ == "2")
            {
                mediTextBox_YongHuMing.ReadOnly = true;
            }

            mediTextBox_MiMa.Focus();
            var result = GYZhiGongService.GetZhiGongXX();

            if (result.ReturnCode != ReturnCode.SUCCESS)
            {
                MediMsgBox.Failure("用户认证时，取职工信息失败，" + result.ReturnMessage);
            }
            else
            {
                listZhiGong = result.Return;
            }
        }

        /// <summary>
        /// 取消验证，关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton_QueDing_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mediTextBox_YongHuMing.Text))
            {
                MediMsgBox.Show("本应用不支持匿名登录，请输入用户名!");
                mediTextBox_YongHuMing.Focus();
                return;
            }
            if (string.IsNullOrEmpty(mediTextBox_MiMa.Text))
            {
                MediMsgBox.Show("本应用不支持空密码，请输入用户密码！");
                mediTextBox_MiMa.Focus();
                return;
            }
            var zhiGong = listZhiGong.Where(o => o.ZHIGONGGH == mediTextBox_YongHuMing.Text).FirstOrDefault();
            if (zhiGong == null)
            {
                MediMsgBox.Show("用户认证失败，请输入正确的用户名！");
                mediTextBox_YongHuMing.Text = "";
                return;
            }
            yongHuRZ.ZhiGongID = zhiGong.ZHIGONGID;
            yongHuRZ.YongHuMM = mediTextBox_MiMa.Text;
            // HR3-10477(146837):判断用户是否和登录用户一样。
            // 如果用户区分标志是1表示当前认证用户不允许和登录用户一样。
            if (yongHuRZ.YongHuQFBZ == "1")
            {
                if (yongHuRZ.ZhiGongID == HISClientHelper.USERID)
                {
                    MediMsgBox.Failure("用户认证失败：要求输入非当前登录用户的职工信息!");
                    return;
                }
            }

            var yanZhenJG = ZhuYuanYHJY(yongHuRZ);
            if (yanZhenJG != 0)
            {
                MediMsgBox.Failure(yongHuRZ.Message);
                yongHuRZ.Message = string.Empty;
                if (yanZhenJG == 1)
                {
                    mediTextBox_YongHuMing.Focus();
                }
                else
                {
                    mediTextBox_MiMa.Focus();
                }
            }
            else
            {
                RenZhenJG = true;
                this.Close();
            }
        }

        #endregion
    }

    /// <summary>
    /// 用户认证
    /// </summary>
    public class XiaoXiRZ
    {
        /// <summary>
        /// 病人住院ID
        /// </summary>
        public string BingRenZYID { get; set; }

        /// <summary>
        /// 婴儿ID
        /// </summary>
        public string YingErID { get; set; }

        /// <summary>
        /// 职工类别
        /// </summary>
        public string ZhiGongLB { get; set; }

        /// <summary>
        /// 病区id
        /// </summary>
        public string BingQuID { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string YongHuMM { get; set; }

        /// <summary>
        /// 职工工号
        /// </summary>
        public string ZhiGongGH { get; set; }

        /// <summary>
        /// 职工ID
        /// </summary>
        public string ZhiGongID { get; set; }

        /// <summary>
        ///消息id
        /// </summary>
        public string XiaoXiID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string YongHuXM { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string MenZhenZYBZ { get; set; }

        /// <summary>
        /// 用户取法标志
        /// </summary>
        public string YongHuQFBZ { get; set; }

        /// <summary>
        /// 其他信息
        /// </summary>
        public string Message { get; set; }
    }
}
