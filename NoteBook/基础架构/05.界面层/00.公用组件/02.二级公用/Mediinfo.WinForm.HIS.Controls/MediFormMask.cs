using Mediinfo.HIS.Core;

using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 遮罩层窗口
    /// </summary>
    public partial class MediFormMask : MediFormWithQX
    {
        #region constructor

        /// <summary>
        /// 遮罩层窗口
        /// </summary>
        public MediFormMask()
        {
            InitializeComponent();
        }

        #endregion

        #region fields

        private MaskForm form1;

        #endregion

        #region properties

        public IWin32Window window { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// 将表单显示为具有指定所有者的模式对话框。
        /// </summary>
        /// <param name="owner">实现IWin32Window的任何对象，该对象代表将拥有模式对话框的顶级窗口。</param>
        /// <returns></returns>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            form1 = new MaskForm
            {
                // 设置遮罩层范围
                Size = Screen.PrimaryScreen.WorkingArea.Size
            };
            form1.Show(owner);
            return base.ShowDialog(owner);
        }

        /// <summary>
        /// 将表单显示为模式对话框。
        /// </summary>
        /// <returns></returns>
        public new DialogResult ShowDialog()
        {
            form1 = new MaskForm
            {
                // 设置遮罩层范围
                Size = Screen.PrimaryScreen.WorkingArea.Size
            };
            form1.Show();

            window = this;

            // 判断父级窗口是否为空
            if (HISClientHelper.ower?.Target != null)
            {
                string tag = HISClientHelper.ower?.Target.ToString();
                string[] array = tag?.Split('|');
                if (array?.Length > 1)
                {
                    Assembly assembly = Assembly.LoadFrom(array[0]);
                    window = (IWin32Window)assembly.CreateInstance(array[1]);

                    return base.ShowDialog(window);
                }
                else
                {
                    return base.ShowDialog();
                }
            }
            else
            {
                return base.ShowDialog();
            }
        }

        #endregion

        #region events

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediFormMask_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form1 != null)
            {
                form1.Close();
                form1.Dispose();
            }
            this.Close();
        }

        #endregion
    }
}
