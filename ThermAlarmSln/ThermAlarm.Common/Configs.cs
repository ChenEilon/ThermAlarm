﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public static class Configs
    {
        public static string SERVICE_CONNECTION_STRING = "HostName=ThermAlarmIOTHub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=8VSekLPIPmWdysNOxVfLJIY+MNQblEtFB8MZJuyLDpU=";
        public static string DEVICE_NAME = "thermAlarmDevice";
        
        #region Event Processor

        public static string HUB_NAME = "iothub-ehub-thermalarm-692054-4ab369c244";
        public static string IOT_HUB_ENDPOINT_CONNECTION_STRING = "Endpoint=sb://ihsuprodamres052dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=N8d5I7IiUoISpXqhUK3kHeA1aU19VHUr71MHJ6jy1+M=";
        public static string STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=thermalarmstorageaccount;AccountKey=9/SJJSBqrTkd3Pel8FgnD5PgbEKV+a5530BD66tQp4sg5yx8Ejt6hAiQOukt+8TRS9NUiQhd0bzE/U/7bR0Tvw==;EndpointSuffix=core.windows.net";
        public static string STORAGE_CONTAINER_NAME = "msg-processor-host";

        #region Web Communication

        public static string LOCAL_WEB_API = @"http://localhost:60768";
        public static string AZURE_WEB_API = @"http://thermalarmwebapp.azurewebsites.net";

        #endregion

        #endregion

        #region Alarm

        public static float TEMP_THRESH = 28;
        public static int PIXEL_THRESH = 10;
        public static int BT_FORGET_TIME = 5;    // in minutes
        public static int AUTO_REARM_TIME = 60;    // in seconds

        #endregion
    }
}
