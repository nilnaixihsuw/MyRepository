using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// Supplier Quality Management
    /// </summary>
    public class SQM
    {

        /// <summary>
        /// 注册日志事件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Register(string title,object sender, EventArgs e)
        {
            #region 记录日志

            if (sender is System.Windows.Forms.Control && e is MouseEventArgs)
            {
                var me = (MouseEventArgs)e;

                //记录日志=====================================================================
                //ESLog eSLog = new ESLog();
                SysLogEntity logEntity = new SysLogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.RiZhiBt = "在[" + HISClientHelper.DANGQIANCKMC + "]界面的[" + ((System.Windows.Forms.Control)sender).Text + "("+ ((System.Windows.Forms.Control)sender).Name + ")]控件上触发了鼠标事件["+ title + "]：Button(" + me.Button+ "),Clicks(" + me.Clicks + "),Delta(" + me.Delta + ")";
                logEntity.RiZhiNr = "[" + HISClientHelper.USERNAME + "]在[" + HISClientHelper.DANGQIANCKMC + "]界面的[" + ((System.Windows.Forms.Control)sender).Text + "("+ ((System.Windows.Forms.Control)sender).Name + ")]控件上触发了鼠标事件[" + title + "]：Button(" + me.Button+ "),Clicks(" + me.Clicks + "),Delta(" + me.Delta + ")";

                logEntity.FuWuMc = "";
                logEntity.QingQiuLy = ((System.Windows.Forms.Control)sender).Text;
                //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志，7.控件操作
                logEntity.RiZhiLx = 7;
                logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
                logEntity.XITONGID = HISClientHelper.XITONGID;
                logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
                logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
                logEntity.VERSION = HISClientHelper.VERSION;
                logEntity.IP = HISClientHelper.IP;
                logEntity.MAC = HISClientHelper.MAC;
                logEntity.COMPUTERNAME = HISClientHelper.COMPUTERNAME;
                logEntity.USERNAME = HISClientHelper.USERNAME;
                logEntity.USERID = HISClientHelper.USERID;
                logEntity.KESHIID = HISClientHelper.KESHIID;
                logEntity.KESHIMC = HISClientHelper.KESHIMC;
                logEntity.BINGQUID = HISClientHelper.BINGQUID;
                logEntity.BINGQUMC = HISClientHelper.BINGQUMC;
                logEntity.JIUZHENKSID = HISClientHelper.JIUZHENKSID;
                logEntity.JIUZHENKSMC = HISClientHelper.JIUZHENKSMC;
                logEntity.YUANQUID = HISClientHelper.YUANQUID;
                logEntity.GONGZUOZID = HISClientHelper.GONGZUOZID;
                //eSLog.PutLog(logEntity);
                LogHelper.Intance.PutSysInfoLog(logEntity);
                //记录日志=====================================================================


            }

            if (sender is System.Windows.Forms.Control && e is KeyEventArgs)
            {
                var ke = (KeyEventArgs)e;

                //记录日志=====================================================================
                //ESLog eSLog = new ESLog();
                SysLogEntity logEntity = new SysLogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.RiZhiBt = "在[" + HISClientHelper.DANGQIANCKMC + "]界面的[" + ((System.Windows.Forms.Control)sender).Text + "(" + ((System.Windows.Forms.Control)sender).Name + ")]控件上触发了键盘事件[" + title + "]：Control(" + ke.Control + "),Alt(" + ke.Alt + "),Shift(" + ke.Shift + "),KeyCode(" + ke.KeyCode + "),KeyData(" + ke.KeyData + "),KeyValue(" + ke.KeyValue + ")";
                logEntity.RiZhiNr = "[" + HISClientHelper.USERNAME + "]在[" + HISClientHelper.DANGQIANCKMC + "]界面的[" + ((System.Windows.Forms.Control)sender).Text + "(" + ((System.Windows.Forms.Control)sender).Name + ")]控件上触发了键盘事件[" + title + "]：Control(" + ke.Control + "),Alt(" + ke.Alt + "),Shift(" + ke.Shift + "),KeyCode(" + ke.KeyCode + "),KeyData(" + ke.KeyData + "),KeyValue(" + ke.KeyValue + ")";

                logEntity.FuWuMc = "";
                logEntity.QingQiuLy = ((System.Windows.Forms.Control)sender).Text;
                //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志，7.控件操作
                logEntity.RiZhiLx = 7;
                logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
                logEntity.XITONGID = HISClientHelper.XITONGID;
                logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
                logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
                logEntity.VERSION = HISClientHelper.VERSION;
                logEntity.IP = HISClientHelper.IP;
                logEntity.MAC = HISClientHelper.MAC;
                logEntity.COMPUTERNAME = HISClientHelper.COMPUTERNAME;
                logEntity.USERNAME = HISClientHelper.USERNAME;
                logEntity.USERID = HISClientHelper.USERID;
                logEntity.KESHIID = HISClientHelper.KESHIID;
                logEntity.KESHIMC = HISClientHelper.KESHIMC;
                logEntity.BINGQUID = HISClientHelper.BINGQUID;
                logEntity.BINGQUMC = HISClientHelper.BINGQUMC;
                logEntity.JIUZHENKSID = HISClientHelper.JIUZHENKSID;
                logEntity.JIUZHENKSMC = HISClientHelper.JIUZHENKSMC;
                logEntity.YUANQUID = HISClientHelper.YUANQUID;
                logEntity.GONGZUOZID = HISClientHelper.GONGZUOZID;
                //eSLog.PutLog(logEntity);
                LogHelper.Intance.PutSysInfoLog(logEntity);
                //记录日志=====================================================================


            }

            if (sender is System.Windows.Forms.Control && e is KeyPressEventArgs)
            {
                var ke = (KeyPressEventArgs)e;

                //记录日志=====================================================================
                //ESLog eSLog = new ESLog();
                SysLogEntity logEntity = new SysLogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.RiZhiBt = "在[" + HISClientHelper.DANGQIANCKMC + "]界面的[" + ((System.Windows.Forms.Control)sender).Text + "(" + ((System.Windows.Forms.Control)sender).Name + ")]控件上触发了键盘事件[" + title + "]：KeyChar(" + ke.KeyChar + ")";
                logEntity.RiZhiNr = "[" + HISClientHelper.USERNAME + "]在[" + HISClientHelper.DANGQIANCKMC + "]界面的[" + ((System.Windows.Forms.Control)sender).Text + "(" + ((System.Windows.Forms.Control)sender).Name + ")]控件上触发了键盘事件[" + title + "]：KeyChar(" + ke.KeyChar + ")";

                logEntity.FuWuMc = "";
                logEntity.QingQiuLy = ((System.Windows.Forms.Control)sender).Text;
                //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志，7.控件操作
                logEntity.RiZhiLx = 7;
                logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
                logEntity.XITONGID = HISClientHelper.XITONGID;
                logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
                logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
                logEntity.VERSION = HISClientHelper.VERSION;
                logEntity.IP = HISClientHelper.IP;
                logEntity.MAC = HISClientHelper.MAC;
                logEntity.COMPUTERNAME = HISClientHelper.COMPUTERNAME;
                logEntity.USERNAME = HISClientHelper.USERNAME;
                logEntity.USERID = HISClientHelper.USERID;
                logEntity.KESHIID = HISClientHelper.KESHIID;
                logEntity.KESHIMC = HISClientHelper.KESHIMC;
                logEntity.BINGQUID = HISClientHelper.BINGQUID;
                logEntity.BINGQUMC = HISClientHelper.BINGQUMC;
                logEntity.JIUZHENKSID = HISClientHelper.JIUZHENKSID;
                logEntity.JIUZHENKSMC = HISClientHelper.JIUZHENKSMC;
                logEntity.YUANQUID = HISClientHelper.YUANQUID;
                logEntity.GONGZUOZID = HISClientHelper.GONGZUOZID;
                //eSLog.PutLog(logEntity);
                LogHelper.Intance.PutSysInfoLog(logEntity);
                //记录日志=====================================================================


            }

            #endregion 记录日志
        }


    }
}

