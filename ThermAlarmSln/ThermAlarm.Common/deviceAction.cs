using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public class deviceAction
    {
        public string Name;
        public String Parameters;
        public deviceAction(string Name, String parameters)
        {
            this.Name = Name;
            if (this.Parameters == null)
            {
                this.Parameters = "{}";
            }
            else
            {
                this.Parameters = parameters;
            }

        }
    }
}
