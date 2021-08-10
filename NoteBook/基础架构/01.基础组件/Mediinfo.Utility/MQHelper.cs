using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Utility
{

    public interface IMQHelper
    {
        bool SendMessage(MQMessage message, MQConnParms parms);
    }
    public abstract class MQHelper : IMQHelper
    {
        public abstract bool SendMessage(MQMessage message, MQConnParms parms);

        public static IMQHelper Create(MQConnect connect)
        {
            //根据配置切换MQ
            RabbitMQHelper Helper = new RabbitMQHelper(connect);
            return Helper;
        } 
    }
}
