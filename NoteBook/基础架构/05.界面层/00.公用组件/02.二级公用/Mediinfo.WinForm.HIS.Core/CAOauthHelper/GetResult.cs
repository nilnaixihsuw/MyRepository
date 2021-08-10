using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Core
{
    public class GetResult
    {
        public Data data { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }

    public class Data
    {
        public int grantStep { get; set; }
    }
   
}
