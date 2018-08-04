#include <ESP8266WiFi.h>
//#include <time.h>
#include <AzureIoTHub.h>
#include <AzureIoTProtocol_MQTT.h>

const char ssid[] = "[SSID]"; //  your WiFi SSID (name)
const char pass[] = "[PASSWORD]";    // your WiFi password (use for WPA, or use as key for WEP)
const char connectionString[] = "HostName=[HubName].azure-devices.net;DeviceId=[DeviceName];SharedAccessKey=[KEY]";

enum ThermAlarmStatus {
  INIT,
  DISARMED,
  ARMED,
  ALARM
};



void setup() {
  // put your setup code here, to run once:

}

void loop() {
  // put your main code here, to run repeatedly:

}
