﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThermAlarm.Common;
using System.Text;
using System.Threading;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using ThermAlarm.WebApp.Services;

namespace ThermAlarm.WebApp.Controllers
{
    //[Produces("application/json")]
    [Route("[Controller]/[action]")]
    public class DeviceController : AlarmControllerBase
    {
        ServiceClient serviceClient;
        RegistryManager registryManager;

        public DeviceController(IDatabaseManager dbManager, Alarm alarm) : base(dbManager, alarm)
        {
            //init DeviceMgr
            serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            registryManager = RegistryManager.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            //var feedbackTask = DeviceMgr.ReceiveFeedback(serviceClient);
            //DeviceMgr.ReceiveFeedback(serviceClient);
        }

        [HttpPost]
        public IActionResult msg_handler(string payload)
        {
            if(payload!=null)
            {
                MsgObj msg = JsonConvert.DeserializeObject<MsgObj>(payload);
                dbManager.LogInDB(msg);
                alarm.msgReceived_handler(msg);
                return Ok("Msg transfered..."); // TODO - change to logger
            }
            return Ok("Payload null.");
        }

        #region DEBUG_FUNCTIONS

        [HttpGet]
        public IActionResult Arm()
        {
            /*This function calls the action Arm on the device.
             The function does not get a feedback*/
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Arm, serviceClient).Wait();
            //Console.WriteLine("THIS IS A TEST!");
            return Ok("Arm Command was sent to Device Device!"); // TODO - change to logger
        }

        [HttpGet]
        public IActionResult Disarm()
        {
            /*This function calls the action Disarm on the device.
             The function does not get a feedback*/
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Disarm, serviceClient).Wait();
            return Ok("Disarm Command was sent to Device Device!"); // TODO - change to logger
        }

        [HttpGet]
        public IActionResult AlarmOn()
        {
            /*This function calls the action ALARM on the device.
             The function does not get a feedback*/
            this.alarm.status = eDeviceAction.Alarm;
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Alarm, serviceClient).Wait();
            return Ok("ALARM!! Command was sent to Device Device! BUZZZZZZ"); // TODO - change to logger
        }

        #endregion
    }
}