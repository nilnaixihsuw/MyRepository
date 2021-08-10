using DevExpress.LookAndFeel;
using DevExpress.Utils;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class W_GY_YONGHUGRXX : MediDialog
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public W_GY_YONGHUGRXX()
        {
            InitializeComponent();

            //UserLookAndFeel.Default.SetSkinStyle("MediSkinDevExpressStyle");


            _GYYongHuGRXXService = new JCJGYongHuGRXXService();
        }
        #endregion

        #region 全局变量
        /// <summary>
        /// 字体样式名称
        /// </summary>
        private string _FontStyle = string.Empty;

        /// <summary>
        /// 字体大小
        /// </summary>
        private decimal _FontSize = FONTSIZE_9_DEFAULT;

        /// <summary>
        /// 皮肤名称
        /// </summary>
        private string _SkinName = string.Empty;

        /// <summary>
        /// 输入码
        /// </summary>
        private string _InputCode = string.Empty;

        /// <summary>
        /// 创建服务实例
        /// </summary>
        private JCJGYongHuGRXXService _GYYongHuGRXXService = null;

        /// <summary>
        /// 用户信息
        /// </summary>
        private E_GY_YONGHUXX _EYongHuXX = null;

        /// <summary>
        /// 用户皮肤信息
        /// </summary>
        private E_GY_YONGHUPFXX _EYongHuPFXX = null;
        #endregion

        #region 全局常量
        #region 皮肤常量
        /// <summary>
        /// 默认皮肤 浅蓝
        /// </summary>
        private const string SKIN_DEVEXPRESSIONSTYLE_DEFAULT = "MediSkinDevLCStyle";

        /// <summary>
        /// 皮肤 浅灰
        /// </summary>
        private const string SKIN_GRAYSTYLE_DEFAULT = "MediSkinLightGray";

        /// <summary>
        /// 皮肤 浅绿
        /// </summary>
        private const string SKIN_LIGHTGREEN_DEFAULT = "MediSkinLightGreen";
        #endregion

        #region 字体常量
        /// <summary>
        /// 微软雅黑
        /// </summary>
        private const string FONT_微软雅黑_DEFAULT = "微软雅黑";
        /// <summary>
        /// 宋体
        /// </summary>
        private const string FONT_宋体_DEFAULT = "宋体";
        /// <summary>
        /// 新微软雅黑
        /// </summary>
       // private const string FONT_新微软雅黑_DEFAULT = "新微软雅黑";
        #endregion

        #region 字体大小
        /// <summary>
        /// 9号字
        /// </summary>
        private const decimal FONTSIZE_9_DEFAULT = 9;
        /// <summary>
        /// 10号字
        /// </summary>
        private const decimal FONTSIZE_10_DEFAULT = 10;
        /// <summary>
        /// 11号字
        /// </summary>
        private const decimal FONTSIZE_11_DEFAULT = 11;
        #endregion

        #region 输入码
        /// <summary>
        /// 输入码1 拼音码
        /// </summary>
        private const string INPUTCODE_SHURUMA1_DEFAULT = "SHURUMA1";
        /// <summary>
        /// 输入码2 五笔码
        /// </summary>
        private const string INPUTCODE_SHURUMA2_DEFAULT = "SHURUMA2";
        /// <summary>
        /// 输入码3 自定义码
        /// </summary>
        private const string INPUTCODE_SHURUMA3_DEFAULT = "SHURUMA3";
        #endregion
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void W_GY_YONGHUXX_Load(object sender, EventArgs e)
        {


            string sImagePath = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(sImagePath + @"AssemblyClient\image\iconpinyin.png"))
                mediLabelPinYinM.Appearance.Image = Image.FromFile(sImagePath + @"AssemblyClient\image\iconpinyin.png");

            if (File.Exists(sImagePath + @"AssemblyClient\image\iconwubi.png"))
                mediLabelWuBiM.Appearance.Image = Image.FromFile(sImagePath + @"AssemblyClient\image\iconwubi.png");

            if (File.Exists(sImagePath + @"AssemblyClient\image\iconziding.png"))
                mediLabelZiDingYM.Appearance.Image = Image.FromFile(sImagePath + @"AssemblyClient\image\iconziding.png");

            BindSkinInfo();
            BindInputCode();
        }
        #endregion

        #region 绑定
        /// <summary>
        /// 设置默认输入码
        /// </summary>
        private void BindInputCode()
        {
            List<E_GY_YONGHUXX> eYongHuXX = _GYYongHuGRXXService.GetYongHuXXByID(HISClientHelper.USERID).Return;
            if (eYongHuXX == null || eYongHuXX.Count == 0)
            {
                _InputCode = INPUTCODE_SHURUMA1_DEFAULT;
                mediLabelPinYinM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
                mediLabelWuBiM.Appearance.BackColor = Color.Empty;
                mediLabelZiDingYM.Appearance.BackColor = Color.Empty;
            }
            else
            {
                _EYongHuXX = eYongHuXX[0];
                if (_EYongHuXX != null && !_EYongHuXX.SHURUMA.IsNullOrWhiteSpace())
                {
                    _InputCode = _EYongHuXX.SHURUMA;
                    switch (_EYongHuXX.SHURUMA)
                    {
                        case INPUTCODE_SHURUMA1_DEFAULT:
                            mediLabelPinYinM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
                            mediLabelWuBiM.Appearance.BackColor = Color.Empty;
                            mediLabelZiDingYM.Appearance.BackColor = Color.Empty;
                            break;
                        case INPUTCODE_SHURUMA2_DEFAULT:
                            mediLabelPinYinM.Appearance.BackColor = Color.Empty;
                            mediLabelWuBiM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
                            mediLabelZiDingYM.Appearance.BackColor = Color.Empty;
                            break;
                        case INPUTCODE_SHURUMA3_DEFAULT:
                            mediLabelPinYinM.Appearance.BackColor = Color.Empty;
                            mediLabelWuBiM.Appearance.BackColor = Color.Empty;
                            mediLabelZiDingYM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
                            break;
                    }
                }
                else
                {
                    _InputCode = INPUTCODE_SHURUMA1_DEFAULT;
                    mediLabelPinYinM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
                    mediLabelWuBiM.Appearance.BackColor = Color.Empty;
                    mediLabelZiDingYM.Appearance.BackColor = Color.Empty;
                }
            }
        }

        /// <summary>
        /// 绑定皮肤信息
        /// </summary>
        private void BindSkinInfo()
        {
            List<E_GY_YONGHUPFXX> eYongHuPFXX = _GYYongHuGRXXService.GetYongHuPFXXByID(HISClientHelper.USERID).Return;

            if (eYongHuPFXX == null || eYongHuPFXX.Count == 0)
            {
                BindSkin();
            }
            else
            {
                _EYongHuPFXX = eYongHuPFXX[0];
                if (_EYongHuPFXX != null)
                {
                  
                    BindSkin(_EYongHuPFXX.PIFUMC);
                }
                else
                {
                    BindSkin();
                }
            }
        }

        /// <summary>
        /// 默认字体
        /// </summary>
       
        /// <summary>
        /// 绑定皮肤
        /// </summary>
        private void BindSkin(string skinName = SKIN_DEVEXPRESSIONSTYLE_DEFAULT)
        {
            //switch(skinName)
            //{
            //    case SKIN_DEVEXPRESSIONSTYLE_DEFAULT:
            //        mediLabelDefaultStyle.Appearance.BorderColor = Color.FromArgb(1, 194, 222);
            //        mediLabelGray.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        mediLabelDark.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        _SkinName = SKIN_DEVEXPRESSIONSTYLE_DEFAULT;
            //        break;
            //    case SKIN_GRAYSTYLE_DEFAULT:
            //        mediLabelDefaultStyle.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        mediLabelGray.Appearance.BorderColor = Color.FromArgb(1, 194, 222); 
            //        mediLabelDark.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        _SkinName = SKIN_GRAYSTYLE_DEFAULT;
            //        break;
            //    case SKIN_LIGHTGREEN_DEFAULT:
            //        mediLabelDefaultStyle.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        mediLabelGray.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        mediLabelDark.Appearance.BorderColor = Color.FromArgb(1, 194, 222); 
            //        _SkinName = SKIN_LIGHTGREEN_DEFAULT;
            //        break;
            //    default:                    
            //        mediLabelDefaultStyle.Appearance.BorderColor = Color.FromArgb(1, 194, 222);
            //        mediLabelGray.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        mediLabelDark.Appearance.BorderColor = Color.FromArgb(205, 205, 205);
            //        _SkinName = SKIN_DEVEXPRESSIONSTYLE_DEFAULT;
            //        break;

            //}

            //UserLookAndFeel.Default.SetSkinStyle(_SkinName);

        }
        #endregion

        #region 输入码

        /// <summary>
        /// 拼音码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLabelPinYinM_Click(object sender, EventArgs e)
        {
            mediLabelPinYinM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
            mediLabelWuBiM.Appearance.BackColor = Color.Empty;
            mediLabelZiDingYM.Appearance.BackColor = Color.Empty;
            _InputCode = INPUTCODE_SHURUMA1_DEFAULT;
        }

        /// <summary>
        /// 五笔码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLabelWuBiM_Click(object sender, EventArgs e)
        {
            mediLabelWuBiM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
            mediLabelZiDingYM.Appearance.BackColor = Color.Empty;
            mediLabelPinYinM.Appearance.BackColor = Color.Empty;
            _InputCode = INPUTCODE_SHURUMA2_DEFAULT;
        }

        /// <summary>
        /// 自定义码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLabelZiDingYM_Click(object sender, EventArgs e)
        {
            mediLabelZiDingYM.Appearance.BackColor = Color.FromArgb(250, 255, 189);
            mediLabelWuBiM.Appearance.BackColor = Color.Empty;
            mediLabelPinYinM.Appearance.BackColor = Color.Empty;
            _InputCode = INPUTCODE_SHURUMA3_DEFAULT;
        }
        #endregion
        #region 保存与关闭
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonAdd_Click(object sender, EventArgs e)
        {
            if (_EYongHuPFXX == null)
            {
                _EYongHuPFXX = new E_GY_YONGHUPFXX();
                _EYongHuPFXX.SetTraceChange(true);
                _EYongHuPFXX.SetState(DTOState.New);
                _EYongHuPFXX.YONGHUID = HISClientHelper.USERID;
                _EYongHuPFXX.YONGHUXM = HISClientHelper.USERNAME;
            }
            else
            {
                _EYongHuPFXX.SetTraceChange(true);
                _EYongHuPFXX.SetState(DTOState.Update);
            }
            _EYongHuPFXX.TINGYONGBZ = 0;
            _EYongHuPFXX.PIFUMC = _SkinName;
            _EYongHuPFXX.ZITIMC = _FontStyle;
            _EYongHuPFXX.ZITIDX = _FontSize;

            if (_EYongHuXX != null)
            {
                _EYongHuXX.SetTraceChange(true);
                _EYongHuXX.SetState(DTOState.Update);
                _EYongHuXX.SHURUMA = _InputCode;
            }
            if (_GYYongHuGRXXService.SaveYongHuGRXX(_EYongHuPFXX, _EYongHuXX).Return)
            {
                MediMsgBox.Show("保存成功!");
                HISClientHelper.SHURUMLX = _InputCode;
                this.Close();
            }
        }
        #endregion

       
        /// <summary>
        /// 刷新第三方接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shuaxindsfjkcslb_Click(object sender, EventArgs e)
        {
            shuaxindsfjkcslb.Appearance.BackColor = Color.FromArgb(250, 255, 189);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            BindSkinInfo();
            this.Close();
        }
    }
}
