#ifndef THERMALARM_H
#define THERMALARM_H

#include "config.h"


#ifdef __cplusplus
extern "C" {
#endif

uint8_t get_PIR_data(void);

IOTHUB_CLIENT_LL_HANDLE createIOThubClient(void);
IOTHUBMESSAGE_DISPOSITION_RESULT IoTHubMessage(IOTHUB_MESSAGE_HANDLE message, void* userContextCallback);

#ifdef __cplusplus
}
#endif



#endif /* THERMALARM_H */


