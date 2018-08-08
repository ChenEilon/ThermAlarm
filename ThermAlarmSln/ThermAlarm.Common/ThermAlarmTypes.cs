using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public enum msgType
    {
        Meausurements = 0,
        BTscan = 1,
        MeasurementsAndBT = 2
    }

    public enum eDeviceAction
    {
        Arm = 0,
        Disarm = 1,
        Alarm = 2
    }
}
