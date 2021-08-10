using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mediinfo.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class UserControl_ZDYXX : MediUserControl
    {
        /// <summary>
        /// 消息
        /// </summary>
        public HISMessageBody message { get; set; }

        public UserControl_ZDYXX()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取自定义消息
        /// </summary>
        /// <returns></returns>
        public virtual List<HISMessageBody> GetZDYMessage()
        {
            //重写该方法，将从自定义sql查出的数据转化为 List<HISMessageBody> 类型
            //XiaoXiID必填且保证唯一，防止多次刷新加载重复的消息
            return new List<HISMessageBody>();
        }
    }
}
