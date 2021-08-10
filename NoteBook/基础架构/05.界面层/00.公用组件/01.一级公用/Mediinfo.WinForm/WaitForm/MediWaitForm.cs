using DevExpress.XtraWaitForm;

using System;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 加载等候窗体
    /// </summary>
    public partial class MediWaitForm : WaitForm
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediWaitForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides
        /// <summary>
        /// 设置加载标题
        /// </summary>
        /// <param name="caption"></param>
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        /// <summary>
        /// 设置加载描述信息
        /// </summary>
        /// <param name="description"></param>
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion Overrides

        public enum WaitFormCommand
        {
        }
    }
}