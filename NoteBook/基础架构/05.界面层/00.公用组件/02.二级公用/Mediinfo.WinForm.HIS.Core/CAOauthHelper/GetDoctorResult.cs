using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Core
{
    public class GetDoctorResult
    {
        public Datas data { get; set; }

        public string message { get; set; }

        public string status { get; set; }
    }

    public class Datas
    {
        public string openId { get; set; }
        public string employeeNumber { get; set; }
        public string process { get; set; }
        public string phoneNum { get; set; }
        public string note { get; set; }
        public string time { get; set; }
        public string stamp { get; set; }
        public string status { get; set; }
        public string stampStatus { get; set; }
        public string userIdcardNum { get; set; }
        public string title { get; set; }
        public string userAge { get; set; }
        public string department { get; set; }
        public string userName { get; set; }
        public string failReason { get; set; }
    }
}
