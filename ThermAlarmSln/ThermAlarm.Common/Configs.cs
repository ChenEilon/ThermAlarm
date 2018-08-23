using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public static class Configs
    {
        public static string SERVICE_CONNECTION_STRING = "HostName=ThermAlarmIOTHub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=8VSekLPIPmWdysNOxVfLJIY+MNQblEtFB8MZJuyLDpU=";
        public static string DEVICE_NAME = "thermAlarmDevice";
        //for event processor:
        public static string HUB_NAME = "iothub-ehub-thermalarm-691910-1300ebab3e";
        public static string IOT_HUB_ENDPOINT_CONNECTION_STRING = "Endpoint=sb://ihsuprodamres052dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=N8d5I7IiUoISpXqhUK3kHeA1aU19VHUr71MHJ6jy1+M=";
        public static string STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=thermalarmstorageaccount;AccountKey=9/SJJSBqrTkd3Pel8FgnD5PgbEKV+a5530BD66tQp4sg5yx8Ejt6hAiQOukt+8TRS9NUiQhd0bzE/U/7bR0Tvw==;EndpointSuffix=core.windows.net";
        public static string STORAGE_CONTAINER_NAME = "msg-processor-host";
    }
}
