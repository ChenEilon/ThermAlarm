using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    
    public static class SensorsProcessing
    {
        
        private static bool thermWarmObj(float[] thermValues)
        {
            //TODO - make logic better.. this is really naive approch - third of pixels above 30
            int countWarm = 0;
            foreach (int i in thermValues)
            {
                if (i > Configs.TEMP_THRESH)
                    countWarm++;
            }
            return (countWarm > Configs.PIXEL_THRESH);
        }

        public static bool shouldAlarm(int pirValue, float[] thermValues)
        {
            if (pirValue > 0)
            {
                if (thermWarmObj(thermValues))
                    return true;
            }
            return false;
        }
    }
}
