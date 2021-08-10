using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Utility.XiaoXi
{
    /// <summary>
    /// 消息委托声明
    /// </summary>
    /// <param name="messageBody"></param>
    public delegate void XiaoXiReceiverDelegate(string messageBody);
    /// <summary>
    /// 消息接收帮助类
    /// </summary>
    public class ReceiveMessageHelper
    {
        public event XiaoXiReceiverDelegate xiaoXiReceiverEvent;
        public ReceiveMessageHelper(string receiverName)
        {
            InitXiaoXiReceiver(receiverName);
        }
        /// <summary>
        /// 创建消息接收对象
        /// </summary>
        /// <param name="receiverName">消息接收者</param>
        private void InitXiaoXiReceiver(string receiverName)
        {
            Task.Factory.StartNew(() =>
            {
                bool isCompleted = false;
                while (true)
                {
                    if (!isCompleted)
                    {

                        CreateConnection(receiverName, ref isCompleted);
                    }
                    else
                    {
                        CreateConnection(receiverName, ref isCompleted);
                    }
                }
            });
        }
        /// <summary>
        /// 消息创建器
        /// </summary>
        /// <param name="isCompleted"></param>
        private void CreateConnection(string receiverName, ref bool isCompleted)
        {
            Decoder decoder = Encoding.UTF8.GetDecoder();
            byte[] bytes = new byte[10];
            char[] chars = new char[10];
            using (NamedPipeClientStream pipeStream =
                    new NamedPipeClientStream(".",receiverName,PipeDirection.InOut))
            {
                try
                {
                    pipeStream.Connect(500);
                    pipeStream.ReadMode = PipeTransmissionMode.Message;
                    int numBytes;
                    do
                    {
                        string message = string.Empty;
                        do
                        {
                            numBytes = pipeStream.Read(bytes, 0, bytes.Length);
                            int numChars = decoder.GetChars(bytes, 0, numBytes, chars, 0);
                            message += new string(chars, 0, numChars);
                        } while (!pipeStream.IsMessageComplete);

                        decoder.Reset();
                        xiaoXiReceiverEvent?.Invoke(message);
                    } while (numBytes != 0);
                }
                catch
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
            isCompleted = true;  
        }
    }
}
