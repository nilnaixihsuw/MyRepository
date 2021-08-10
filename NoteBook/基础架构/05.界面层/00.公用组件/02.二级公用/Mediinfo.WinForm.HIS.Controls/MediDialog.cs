using Mediinfo.HIS.Core;

using System;
using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 通用对话框（业务开发时使用）
    /// 带权限，默认根据父窗口居中，不支持最大化，不能缩放
    /// </summary>
    public partial class MediDialog : MediFormWithQX
    {
        #region constructor

        public MediDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region properties

        public IWin32Window Window { get; set; }

        #endregion

        #region events

        private void MediDialog_Shown(object sender, EventArgs e)
        {
            Window = this;

            // 判断父级窗口是否为空
            if (HISClientHelper.ower?.Target != null)
            {
                string tag = HISClientHelper.ower?.Target.ToString();
                string[] array = tag?.Split('|');
                if (array?.Length > 1)
                {
                    Assembly assembly = Assembly.LoadFrom(array[0]);
                    Window = (IWin32Window)assembly.CreateInstance(array[1]);
                }
            }
        }

        private void MediDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            HISClientHelper.ower = new WeakReference(Window);
        }

        #endregion
    }
}