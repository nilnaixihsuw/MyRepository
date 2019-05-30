using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;

namespace OpLink
{
    class DaTag:BaseTag
    {
        //OPC点
        //OPCItem daItem;
        //质量
        string qualities="";
        //时间戳
        string timeStamps = "";
        //Message备注
        string message = "";

        public string Qualities
        {
            get { return qualities; }
            set { qualities = value; }
        }
        public string TimeStamps
        {
            get { return timeStamps; }
            set { timeStamps = value; }
        }
        //public OPCItem DaItem
        //{
        //    get { return daItem; }
        //    set { daItem = value; }
        //}

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
