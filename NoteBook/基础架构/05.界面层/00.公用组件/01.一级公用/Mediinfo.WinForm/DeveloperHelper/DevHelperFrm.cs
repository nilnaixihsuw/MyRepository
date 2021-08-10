using System;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 开发者帮助窗体
    /// </summary>
    public partial class DevHelperFrm : MediForm
    {
        /// <summary>
        /// 开发者帮助信息
        /// </summary>
        public string DeveloperInfo { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        private string ControlName { get; set; }

        /// <summary>
        /// 类名称
        /// </summary>
        private string ClassName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="developerInfo">控件相关信息</param>
        /// <param name="controlName">控件名称</param>
        /// <param name="className">类名称</param>
        public DevHelperFrm(string developerInfo,string controlName,string className)
        {
            InitializeComponent();
            this.DeveloperInfo = developerInfo;
            ControlName = controlName;
            ClassName = className;
        }

        /// <summary>
        /// load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevHelperFrm_Load(object sender, EventArgs e)
        {
            this.medimemoDeveloperInfo.EditValue = DeveloperInfo;
        }

        /// <summary>
        /// 控件名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlName_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(ControlName);
            //XtraMessageBox.Show("复制成功!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 窗体名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void className_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(ClassName);
            //XtraMessageBox.Show("复制成功!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}