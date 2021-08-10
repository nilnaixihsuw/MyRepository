using DevExpress.XtraEditors;

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 图片相对路径截取窗口类
    /// </summary>
    public partial class FilePathOptions : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FilePathOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 图片绝对路径
        /// </summary>
        public string SbsolutePath { get; set; }

        /// <summary>
        /// 相对文件路径
        /// </summary>
        public string RelativeFilePath { get; set; }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btePicPath.Text) || string.IsNullOrWhiteSpace(bteStartPath.Text))
            {
                XtraMessageBox.Show("文件路径不允许为空!");
            }
            RelativeFilePath = RelativePath(btePicPath.Text, bteStartPath.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 相对路径方法
        /// </summary>
        /// <param name="absolutePath">绝对路径</param>
        /// <param name="relativeTo">参照路径</param>
        /// <summary>
        private string RelativePath(string absolutePath, string relativeTo)
        {
            string[] absoluteDirectories = absolutePath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string[] relativeDirectories = relativeTo.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;
            int lastCommonRoot = -1;
            int index;
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;
            if (lastCommonRoot == -1)
                throw new ArgumentException("文件路径和启动程序文件路径不匹配，无法生成虚拟路径!");
            StringBuilder relativePath = new StringBuilder();
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length; index++)
                if (relativeDirectories[index].Length > 0)
                    relativePath.Append("..\\");
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length - 1; index++)
                relativePath.Append(absoluteDirectories[index] + "\\");
            relativePath.Append(absoluteDirectories[absoluteDirectories.Length - 1]);

            return relativePath.ToString();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 图片文件路径
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void btePicPath_Properties_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                ofd.Filter = "图片文件|*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SbsolutePath = ofd.FileName;
                    btePicPath.Text = ofd.FileName;
                }
            }
        }

        /// <summary>
        /// 启动程序路径
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void bteStartPath_Properties_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Directory.GetCurrentDirectory();
                fbd.Description = "请选择文件路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    bteStartPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}