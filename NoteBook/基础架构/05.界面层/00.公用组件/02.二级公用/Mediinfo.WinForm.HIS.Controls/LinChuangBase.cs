using System;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class LinChuangBase
    {

    }

    /// <summary>
    /// 窗体打开方式
    /// </summary>
    public enum OpenType
    {
        /// <summary>
        /// 独占式
        /// </summary>
        Open = 0,
        /// <summary>
        /// 填充式
        /// </summary>
        OPENSHEET = 1,
        /// <summary>
        /// 外链式
        /// </summary>
        OPENBROWSER = 2,
        /// <summary>
        /// 弹窗
        /// </summary>
        SHOW = 3
    }

    /// <summary>
    /// 打开患者列表之前返回结果
    /// </summary>
    public enum MediDialogResult
    {
        /// <summary>
        /// 打开窗体
        /// </summary>
        Open = 1,
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 2,
        /// <summary>
        /// 不作处理
        /// </summary>
        Cancel = 3

    }

    public class MainFormOpenEventArgs : FormOpenEventArgs
    {

    }

    public class FormOpenEventArgs : EventArgs
    {
        /// <summary>
        /// 主窗体打开状态
        /// </summary>
        public MediDialogResult MFOpenResult;

        public ActionType ActionStateType;
    }

    public enum ActionType
    {
        ChangeKeShi = 0,
        LockScreen = 1,
        CaoZuoShouCe = 2
    }

    public enum OpenWindowMode
    {
        Menu = 0,
        Button = 1
    }

    public struct OpenCKInfo
    {
        /// <summary>
        /// 打开方式
        /// </summary>
        public OpenWindowMode openWindowMode;
        /// <summary>
        /// 病人ID
        /// </summary>
        public string binrenid;
        /// <summary>
        /// 窗口名称
        /// </summary>
        public string chuangkoumc;
        /// <summary>
        /// 系统ID
        /// </summary>
        public string xitongid;
        /// <summary>
        /// 功能ID
        /// </summary>
        public string gongnengId;
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string caidanId;
        /// <summary>
        /// 比较功能键是否相等
        /// </summary>
        /// <param name="openCKInfo"></param>
        /// <returns></returns>
        internal bool CustomCompareVaue(OpenCKInfo openCKInfo)
        {

            if (this.openWindowMode == openCKInfo.openWindowMode && this.xitongid == openCKInfo.xitongid && this.gongnengId == openCKInfo.gongnengId && this.caidanId == openCKInfo.caidanId)
                return true;
            else
                return false;

        }
    }



}
