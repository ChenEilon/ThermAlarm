using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermAlarm.BackEnd.API
{
    public class Alarm
    {
        static private bool on;

        void turn_on()
        {
            on = true;
        }

        void turn_off()
        {
            on = false;
        }

        string get_status()
        {
            if (on)
                return "on";
            return "off";
        }


        
        
    }
}
