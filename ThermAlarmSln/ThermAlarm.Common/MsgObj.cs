using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public class MsgObj
    {
        public msgType mType { get; set; }

        public int pirValue { get; set; }

        public float[] thermValue { get; set; }

        public string[] idsBTScan { get; set; }

    }
}
