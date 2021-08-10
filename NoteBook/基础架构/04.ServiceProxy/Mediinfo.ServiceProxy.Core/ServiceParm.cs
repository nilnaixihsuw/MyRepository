namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 服务参数
    /// </summary>
    public class ServiceParm
    {
        /// <summary>
        /// 参数名
        /// </summary>
        private string _parmName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        private object _parmValue { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parmName"></param>
        /// <param name="parmValue"></param>
        public ServiceParm(string parmName, object parmValue)
        {
            _parmName = parmName;
            _parmValue = parmValue;
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string ParmName { get { return _parmName; } }

        /// <summary>
        /// 参数值
        /// </summary>
        public object ParmValue { get { return _parmValue; } }
    }
}
