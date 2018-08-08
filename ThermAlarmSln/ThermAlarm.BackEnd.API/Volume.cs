using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermAlarm.BackEnd.API
{//TODO might not be necessary, at all
    public class Volume
    {
        private bool detected_status;

        bool get_detected_status()
        {
            return this.detected_status;
        }

        void set_detected_status(bool status)
        {
            this.detected_status = status;
        }
    }
}
