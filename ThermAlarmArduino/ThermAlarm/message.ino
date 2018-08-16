#include <ArduinoJson.h>
#include "ThermAlarm.h"

/**********************************************/
/***************** IOT functions **************/
/**********************************************/

void readMessage(int messageId, char *payload) {
  uint8_t pirValue = getPirData();
  float *thermData = getThermData();
  
  StaticJsonBuffer<MESSAGE_MAX_LEN> jsonBuffer;
  JsonObject &root = jsonBuffer.createObject();
  root["deviceId"] = DEVICE_ID;
  root["messageId"] = messageId;
  root["mType"] = 2;
  root["pirValue"] = pirValue;
  JsonArray& thermValue = root.createNestedArray("thermValue");
//  thermValue.copyFrom(thermData);
  for (int i = 0; i < THERMAL_ARRAY_SIZE; i++) {
    thermValue.add(thermData[i]);
  }
  
  root.printTo(payload, MESSAGE_MAX_LEN);
}

static void sendMessage(IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle, char *buffer) {
  IOTHUB_MESSAGE_HANDLE messageHandle = IoTHubMessage_CreateFromByteArray((const unsigned char *)buffer, strlen(buffer));
  if (messageHandle == NULL) {
    Serial.println("Unable to create a new IoTHubMessage.");
  }
  else {
    Serial.printf("Sending message: %s.\r\n", buffer);
    if (IoTHubClient_LL_SendEventAsync(iotHubClientHandle, messageHandle, sendCallback, NULL) != IOTHUB_CLIENT_OK) {
      Serial.println("Failed to hand over the message to IoTHubClient.");
    }
    else {
      messagePending = true;
      Serial.println("IoTHubClient accepted the message for delivery.");
    }

    IoTHubMessage_Destroy(messageHandle);
  }
}



static void sendCallback(IOTHUB_CLIENT_CONFIRMATION_RESULT result, void *userContextCallback) {
  if (IOTHUB_CLIENT_CONFIRMATION_OK == result) {
    Serial.println("Message sent to Azure IoT Hub");
    //blinkLED();
  }
  else {
    Serial.println("Failed to send message to Azure IoT Hub");
  }
  messagePending = false;
}




//void parseTwinMessage(char *message)
//{
//    StaticJsonBuffer<MESSAGE_MAX_LEN> jsonBuffer;
//    JsonObject &root = jsonBuffer.parseObject(message);
//    if (!root.success())
//    {
//        Serial.printf("Parse %s failed.\r\n", message);
//        return;
//    }
//
//    if (root["desired"]["interval"].success())
//    {
//        interval = root["desired"]["interval"];
//    }
//    else if (root.containsKey("interval"))
//    {
//        interval = root["interval"];
//    }
//}


//from command center example:
//void sendCallback(IOTHUB_CLIENT_CONFIRMATION_RESULT result, void* userContextCallback)
//{
//    int messageTrackingId = (intptr_t)userContextCallback;
//
//    LogInfo("Message Id: %d Received.\r\n", messageTrackingId);
//
//    LogInfo("Result Call Back Called! Result is: %s \r\n", ENUM_TO_STRING(IOTHUB_CLIENT_CONFIRMATION_RESULT, result));
//}
//
//static void sendMessage(IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle, const unsigned char* buffer, size_t size)
//{
//    static unsigned int messageTrackingId;
//    IOTHUB_MESSAGE_HANDLE messageHandle = IoTHubMessage_CreateFromByteArray(buffer, size);
//    if (messageHandle == NULL)
//    {
//        LogInfo("unable to create a new IoTHubMessage\r\n");
//    }
//    else
//    {
//        if (IoTHubClient_LL_SendEventAsync(iotHubClientHandle, messageHandle, sendCallback, (void*)(uintptr_t)messageTrackingId) != IOTHUB_CLIENT_OK)
//        {
//            LogInfo("failed to hand over the message to IoTHubClient");
//        }
//        else
//        {
//            LogInfo("IoTHubClient accepted the message for delivery\r\n");
//        }
//        IoTHubMessage_Destroy(messageHandle);
//    }
//    free((void*)buffer);
//    messageTrackingId++;
//}
