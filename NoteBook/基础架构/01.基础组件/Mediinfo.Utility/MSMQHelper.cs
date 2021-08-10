using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Utility
{
    public class MSMQHelper : MQHelper, IMQHelper
    {
        public MSMQHelper(MQConnect connect)
        {

        }

        public override bool SendMessage(MQMessage message, MQConnParms parms)
        {
            throw new NotImplementedException();
        }
    }
}