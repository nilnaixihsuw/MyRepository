using Newtonsoft.Json;

using System;

namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 系统异常基类
    /// </summary>
    public class BaseException : Exception
    {
        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="parms">自定义参数</param>
        public BaseException(ReturnCode errorCode, string errorMessage, object[] parms = null)
            : base(errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Parms = parms;
        }

        #endregion

        #region properties

        /// <summary>
        /// 自定义参数
        /// </summary>
        public object[] Parms { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public ReturnCode ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty]
        public string ErrorMessage { get; set; }

        #endregion

        #region virtual properties

        /// <summary>
        /// 异常级别(默认: 2(重量级))
        /// <para>1: 轻量级(客户端提示时只显示自定义错误信息，不显示详细信息)</para>
        /// <para>2: 重量级(客户端提示时显示自定义错误信息，同时显示错误信息信息)</para>
        /// </summary>
        public virtual int Level { get; set; } = 2;

        #endregion
    }
}
