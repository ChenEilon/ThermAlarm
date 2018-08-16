using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public static class Configs
    {
        public static string SERVICE_CONNECTION_STRING = "HostName=IOThubLightTry.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=RAu/c4xbo/h081Y3yil0/M+cK/1uJr6cfhpPwNbYJys=";
        public static string DEVICE_NAME = "LightTry1";
        //for event processor:
        public static string HUB_NAME = "iothub-ehub-iothubligh-621748-5b7250a98c";
        public static string IOT_HUB_CONNECTION_STRING = "Endpoint=sb://ihsuprodbyres067dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=6iyC4LYT9cRhU8fLbO8uCsJdemq1yCh4OoXw+zFBk1I=";
        public static string STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=lighttrystorage;AccountKey=yDgACrPnyIl4F1lCjIV/+6Sq+njDSS5mD9gaBvyc76CvpNtZq2E3MfewPKLxu8dduQGpvhcVttYOd4/4tj8VaQ==;EndpointSuffix=core.windows.net";
        public static string STORAGE_CONTAINER_NAME = "msg-processor-host";
    }
}
