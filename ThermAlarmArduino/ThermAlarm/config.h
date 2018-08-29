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
#define IOT_CONFIG_CONNECTION_STRING    "HostName=ThermAlarmIOTHub.azure-devices.net;DeviceId=thermAlarmDevice;SharedAccessKey=Iv5yW1T+t2GaptF/Cba2QT8us86N0oYK3D7e/r1e6sU="
/* Choose the transport protocol*/
#define IOT_CONFIG_MQTT                 // uncomment this line for MQTT
// #define IOT_CONFIG_HTTP              // uncomment this line for HTTP

#define MESSAGE_MAX_LEN 1024
#define DEBUG_SEC 2000
#define DEVICE_ID "thermAlarmDevice" //TODO - fill device id.

/**********************************************/
/*********** ThermAlarm Types & Setup *********/
/**********************************************/

typedef enum _eThermAlarmStatus {
  INIT,
  DISARMED,
  ARMED,
  ALARM
} eThermAlarmStatus;

#define LED_PIN 0 //TODO - change base on hardware
#define PIR_PIN 14
#define RX_PIN 12
#define TX_PIN 13
#define BUZZER_PIN 16

#define THERMAL_ARRAY_SIZE 64
#define BT_DATA_BAUD_RATE 9600
#define BT_BUFFER_LEN 64

#endif /* CONFIG_H */






