#include "ThermAlarm.h"

// Define the Model
BEGIN_NAMESPACE(ThermAlarmNS); 

DECLARE_MODEL(mThermAlarm,
  WITH_DATA(ascii_char_ptr, DeviceId),
  WITH_DATA(int, EventTime),
  WITH_DATA(eThermAlarmStatus, AlarmStatus),
  WITH_DATA(uint8_t, PIRvalue),//TODO - add therm sensor data
  WITH_ACTION(Arm),
  WITH_ACTION(Disarm), //TODO - Do we seprate disarm and turn off alarm?
  WITH_ACTION(TurnOnAlarm)
);

END_NAMESPACE(ThermAlarmNS);

/**********************************************/
/****************** Signal Methods ************/
/**********************************************/

EXECUTE_COMMAND_RESULT Arm(mThermAlarm* device){
  //(void)device;
  device->AlarmStatus =  ARMED;
  printf("Arm"); //DEBUG (serial.println is cpp obj)
  return EXECUTE_COMMAND_SUCCESS;
}

EXECUTE_COMMAND_RESULT Disarm(mThermAlarm* device){
  //(void)device;
  device->AlarmStatus =  DISARMED;
  printf("Disarm"); //DEBUG
  return EXECUTE_COMMAND_SUCCESS;
}

EXECUTE_COMMAND_RESULT TurnOnAlarm(mThermAlarm* device){
  //(void)device;
  device->AlarmStatus =  ALARM;
  printf("BUZZZ!!!!!!!"); //DEBUG
  return EXECUTE_COMMAND_SUCCESS;
}


/**********************************************/
/****************** Sensors *******************/
/**********************************************/

uint8_t get_PIR_data() { //EXAMPLE INCLUDES MORE COMPLEX PIR VALUE DEFINITION
   uint8_t pir_status = digitalRead(PIR_PIN);
   if(pir_status == HIGH) {
      printf("PIR - Motion detected.");//DEBUG
   }
   if(pir_status == LOW) {
      printf("PIR - Motion ended."); //DEBUG
   }
   return pir_status;
}

/**********************************************/
/***************** IOT functions **************/
/**********************************************/

//from command center example:
void sendCallback(IOTHUB_CLIENT_CONFIRMATION_RESULT result, void* userContextCallback)
{
    int messageTrackingId = (intptr_t)userContextCallback;

    LogInfo("Message Id: %d Received.\r\n", messageTrackingId);

    LogInfo("Result Call Back Called! Result is: %s \r\n", ENUM_TO_STRING(IOTHUB_CLIENT_CONFIRMATION_RESULT, result));
}

static void sendMessage(IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle, const unsigned char* buffer, size_t size)
{
    static unsigned int messageTrackingId;
    IOTHUB_MESSAGE_HANDLE messageHandle = IoTHubMessage_CreateFromByteArray(buffer, size);
    if (messageHandle == NULL)
    {
        LogInfo("unable to create a new IoTHubMessage\r\n");
    }
    else
    {
        if (IoTHubClient_LL_SendEventAsync(iotHubClientHandle, messageHandle, sendCallback, (void*)(uintptr_t)messageTrackingId) != IOTHUB_CLIENT_OK)
        {
            LogInfo("failed to hand over the message to IoTHubClient");
        }
        else
        {
            LogInfo("IoTHubClient accepted the message for delivery\r\n");
        }
        IoTHubMessage_Destroy(messageHandle);
    }
    free((void*)buffer);
    messageTrackingId++;
}

/*this function "links" IoTHub to the serialization library*/
/*This implementation of IoTHubMessage calls the specific function for each action in your model*/
static IOTHUBMESSAGE_DISPOSITION_RESULT IoTHubMessage(IOTHUB_MESSAGE_HANDLE message, void* userContextCallback)
{
    LogInfo("Command Received\r\n");
    IOTHUBMESSAGE_DISPOSITION_RESULT result;
    const unsigned char* buffer;
    size_t size;
    if (IoTHubMessage_GetByteArray(message, &buffer, &size) != IOTHUB_MESSAGE_OK)
    {
        LogInfo("unable to IoTHubMessage_GetByteArray\r\n");
        result = EXECUTE_COMMAND_ERROR;
    }
    else
    {
        /*buffer is not zero terminated*/
        char* temp = malloc(size + 1);
        if (temp == NULL)
        {
            LogInfo("failed to malloc\r\n");
            result = EXECUTE_COMMAND_ERROR;
        }
        else
        {
            memcpy(temp, buffer, size);
            temp[size] = '\0';
            EXECUTE_COMMAND_RESULT executeCommandResult = EXECUTE_COMMAND(userContextCallback, temp);
            result =
                (executeCommandResult == EXECUTE_COMMAND_ERROR) ? IOTHUBMESSAGE_ABANDONED :
                (executeCommandResult == EXECUTE_COMMAND_SUCCESS) ? IOTHUBMESSAGE_ACCEPTED :
                IOTHUBMESSAGE_REJECTED;
            free(temp);
        }
    }
    return result;
}


/**********************************************/
/****************** Main **********************/
/**********************************************/

//Based on command_center example:
void command_center_run(void)
{
    if (serializer_init(NULL) != SERIALIZER_OK)
    {
        LogInfo("Failed on serializer_init\r\n");
    }
    else
    {
        IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle = IoTHubClient_LL_CreateFromConnectionString(connectionString, MQTT_Protocol);
        srand((unsigned int)time(NULL));
        int avgWindSpeed = 10.0;

        if (iotHubClientHandle == NULL)
        {
            LogInfo("Failed on IoTHubClient_LL_Create\r\n");
        }
        else
        {
            unsigned int minimumPollingTime = 9; /*because it can poll "after 9 seconds" polls will happen effectively at ~10 seconds*/
            if (IoTHubClient_LL_SetOption(iotHubClientHandle, "MinimumPollingTime", &minimumPollingTime) != IOTHUB_CLIENT_OK)
            {
                LogInfo("failure to set option \"MinimumPollingTime\"\r\n");
            }

           #ifdef MBED_BUILD_TIMESTAMP
            // For mbed add the certificate information
            if (IoTHubClient_LL_SetOption(iotHubClientHandle, "TrustedCerts", certificates) != IOTHUB_CLIENT_OK)
            {
                LogInfo("failure to set option \"TrustedCerts\"\r\n");
            }
           #endif // MBED_BUILD_TIMESTAMP

            mThermAlarm* myThermAlarm = CREATE_MODEL_INSTANCE(ThermAlarmNS, mThermAlarm);
            if (myThermAlarm == NULL)
            {
                LogInfo("Failed on CREATE_MODEL_INSTANCE\r\n");
            }
            else
            {
                if (IoTHubClient_LL_SetMessageCallback(iotHubClientHandle, IoTHubMessage, myThermAlarm) != IOTHUB_CLIENT_OK)
                {
                    LogInfo("unable to IoTHubClient_SetMessageCallback\r\n");
                }
                else
                {
//                    /* wait for commands */
//                    long Prev_time_ms = millis();
//                    char buff[11];
//                    int timeNow = 0;
//
//                    while (1)
//                    {
//                        long Curr_time_ms = millis();
//                        if (Curr_time_ms >= Prev_time_ms + 5000)
//                        {
//                            Prev_time_ms = Curr_time_ms;
//                            
//                            timeNow = (int)time(NULL);
//                            //sprintf(buff, "%d", timeNow);
//
//                            float Temp_c__f, Humi_pct__f;
//                            getNextSample(&Temp_c__f, &Humi_pct__f);
//                            
//                            myWeather->DeviceId = DeviceId;
//                            myWeather->EventTime = timeNow;
//                            myWeather->MTemperature = (int)Temp_c__f;
//                            myWeather->Humidity = (int)Humi_pct__f;
//
//                            LogInfo("Result: %s | %d | %d | %d \r\n", myWeather->DeviceId, myWeather->EventTime, myWeather->MTemperature, myWeather->Humidity);
//                        
//                            unsigned char* destination;
//                            size_t destinationSize;
//                            
//                            if (SERIALIZE(&destination, &destinationSize, myWeather->DeviceId, myWeather->EventTime, myWeather->MTemperature, myWeather->Humidity) != IOT_AGENT_OK)
//                            {
//                                LogInfo("Failed to serialize\r\n");
//                            }
//                            else
//                            {
//                                IOTHUB_MESSAGE_HANDLE messageHandle = IoTHubMessage_CreateFromByteArray(destination, destinationSize);
//                                if (messageHandle == NULL)
//                                {
//                                    LogInfo("unable to create a new IoTHubMessage\r\n");
//                                }
//                                else
//                                {
//                                    if (IoTHubClient_LL_SendEventAsync(iotHubClientHandle, messageHandle, sendCallback, (void*)1) != IOTHUB_CLIENT_OK)
//                                    {
//                                        LogInfo("failed to hand over the message to IoTHubClient\r\n");
//                                    }
//                                    else
//                                    {
//                                        LogInfo("IoTHubClient accepted the message for delivery\r\n");
//                                    }
//    
//                                    IoTHubMessage_Destroy(messageHandle);
//                                }
//                                free(destination);
//                            }
//                            
//                        }
//                        
//                        IoTHubClient_LL_DoWork(iotHubClientHandle);
//                        ThreadAPI_Sleep(100);
//                    }
//                }

                DESTROY_MODEL_INSTANCE(myWeather);
            }
            IoTHubClient_LL_Destroy(iotHubClientHandle);
        }
        serializer_deinit();
    }
}

