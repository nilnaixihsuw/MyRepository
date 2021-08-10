namespace Mediinfo.WinForm
{
    /// <summary>
    /// 表达式接口
    /// </summary>
    public interface IExpressionInterface
    {
        /// <summary>
        /// 控件表达式属性接口
        /// </summary>
        string UnboundExpression { get; set; }
    }

    /// <summary>
    /// 输入法接口
    /// </summary>
    public interface IInputIMEMode
    {
        /// <summary>
        ///输入法模式
        /// </summary>
        MediInfoImeMode MediinfoIMEMode { get; set; }
    }
}