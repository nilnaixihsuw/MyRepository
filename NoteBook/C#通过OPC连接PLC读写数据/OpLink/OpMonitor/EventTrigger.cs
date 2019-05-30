using OpLink.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpLink
{
    class EventTrigger
    {
        public delegate void tempChange(object sender, EventArgs e);
        public event tempChange OnTriggerChange;
        Tag temp;
        public Tag Temp
        {
            get
            {
                return temp;
            }
            set
            {
                if (temp.Value != value.Value)
                {
                    OnTriggerChange(this, new EventArgs());
                }
                temp = value;
            }
        }
    }
}
