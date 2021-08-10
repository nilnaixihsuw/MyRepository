using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Utility.XiaoXi
{
    /// <summary>
    /// 消息发送者
    /// </summary>
    public class SendMessageHelper
    {
        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="receiverName"></param>
        /// <param name="messageBody"></param>
        public static void SendMessage(string receiverName, string messageBody)
        {
            Task.Factory.StartNew(() =>
            {
                if (!string.IsNullOrWhiteSpace(receiverName) && !string.IsNullOrWhiteSpace(messageBody))
                {
                    try
                    {
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] bytes;
                        using (NamedPipeServerStream pipeStream = new
                                NamedPipeServerStream(receiverName, PipeDirection.InOut, 1,
                                PipeTransmissionMode.Message, PipeOptions.None))
                        {
                            pipeStream.WaitForConnection();
                            bytes = encoding.GetBytes(messageBody);
                            pipeStream.Write(bytes, 0, bytes.Length);

                        }
                    }
                    catch
                    {
                    }
                }
            });
        }
    }
}
