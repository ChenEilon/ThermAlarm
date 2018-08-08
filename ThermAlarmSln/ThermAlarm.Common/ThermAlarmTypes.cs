using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public enum msgType
    {
        Meausurements = 0,
        BTscan = 1
    }

    public enum deviceAction
    {
        Arm = 0,
        Disarm = 1,
        Alarm = 2
    }
}
