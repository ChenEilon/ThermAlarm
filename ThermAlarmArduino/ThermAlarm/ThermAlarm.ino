#include <ESP8266WiFi.h>
extern "C"{
#include "ThermAlarm.h"
};





int status = WL_IDLE_STATUS;
static int messageCount = 1;
static bool messagePending = false;
static bool messageSending = true;
static IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle;

void setup() {
  initSerial();
  initWifi();
  initTime();
  initHW();
  
  iotHubClientHandle = createIOThubClient();

}

void loop() {
  /*
   * 1. Get mesurments and send to IOT HUB
   * 2. SCAN BT and send to IOT HUB
  */

  if (!messagePending && messageSending)
    {
        char messagePayload[MESSAGE_MAX_LEN];
        readMessage(messageCount, messagePayload);
        sendMessage(iotHubClientHandle, messagePayload);
        messageCount++;
        delay(DEBUG_SEC);
    }
    IoTHubClient_LL_DoWork(iotHubClientHandle);
    delay(10);
 

}


/**********************************************/
/****************** Inits *********************/
/**********************************************/

//From command_center example:

void initSerial() {
    // Start serial and initialize stdout
    Serial.begin(115200);
    Serial.setDebugOutput(true);
}

void initWifi() {
    // Attempt to connect to Wifi network:
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(WIFI_SSID);

    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:
    status = WiFi.begin(WIFI_SSID, WIFI_PASSWORD);

    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }

    Serial.println("Connected to wifi");

  
}

void initTime()
{
    time_t epochTime;
    configTime(0, 0, "pool.ntp.org", "time.nist.gov");

    while (true)
    {
        epochTime = time(NULL);

        if (epochTime == 0)
        {
            Serial.println("Fetching NTP epoch time failed! Waiting 2 seconds to retry.");
            delay(2000);
        }
        else
        {
            Serial.printf("Fetched NTP epoch time is: %lu.\r\n", epochTime);
            break;
        }
    }
}

void initHW(){
  pinMode(PIR_PIN, INPUT);
  pinMode(LED_PIN, OUTPUT);
}

//void destroy(){
//  DESTROY_MODEL_INSTANCE(myWeather);
//  IoTHubClient_LL_Destroy(iotHubClientHandle);
//  serializer_deinit();
//}

