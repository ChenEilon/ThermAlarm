#include <ESP8266WiFi.h>
#include "ThermAlarm.h"


int status = WL_IDLE_STATUS;

void setup() {
  initSerial();
  initWifi();
  initHW();

}

void loop() {
  /*
   * 1. Get mesurments and send to IOT HUB
   * 2. SCAN BT and send to IOT HUB
  */

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

void initHW(){
  pinMode(PIR_PIN, INPUT);
  pinMode(LED_PIN, OUTPUT);
}

