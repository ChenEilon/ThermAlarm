/* System constants & definitions*/
#ifndef CONFIG_H
#define CONFIG_H

/**********************************************/
/****************** Includes ******************/
/**********************************************/

#include <time.h>
#include <AzureIoTHub.h>
#include <AzureIoTProtocol_MQTT.h>
#include "sdk/schemaserializer.h"
#include <stdlib.h>
#include <stdio.h>
#include <stdint.h>
#include <pgmspace.h>
#include <Arduino.h>
//#include "AzureIoTHub.h"

/**********************************************/
/*************** WiFi & IOT setup *************/
/**********************************************/
#define WIFI_SSID            "prolog ap"
#define WIFI_PASSWORD        "nufGZ8N5"
#define IOT_CONFIG_CONNECTION_STRING    "HostName=IOThubLightTry.azure-devices.net;DeviceId=LightTry1;SharedAccessKey=6dAcCiG7lDBeqsVzaaHXOT0cR0UoODRZgqQ8iDtdhjM="
/* Choose the transport protocol*/
#define IOT_CONFIG_MQTT                 // uncomment this line for MQTT
// #define IOT_CONFIG_HTTP              // uncomment this line for HTTP

#define MESSAGE_MAX_LEN 256
#define DEBUG_SEC 500
#define DEVICE_ID "LightTry1" //TODO - fill device id.

/**********************************************/
/*********** ThermAlarm Types & Setup *********/
/**********************************************/

typedef enum _eThermAlarmStatus {
  INIT,
  DISARMED,
  ARMED,
  ALARM
}eThermAlarmStatus;

#define LED_PIN 0 //TODO - change base on hardware
#define PIR_PIN 14
//#define THERM_PIN 
//#define BT_PIN
//#define BUZZER_PIN



#endif /* CONFIG_H */






