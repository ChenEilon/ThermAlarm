using System;
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

namespace ThermAlarm.WebApp.Controllers
{
    //[Produces("application/json")]
    [Route("[Controller]/[action]")]
    public class DeviceController : Controller
    {
        ServiceClient serviceClient;
        RegistryManager registryManager;

        public DeviceController()
        {
            //init DeviceMgr
            serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            registryManager = RegistryManager.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            //var feedbackTask = DeviceMgr.ReceiveFeedback(serviceClient);
            //DeviceMgr.ReceiveFeedback(serviceClient);
        }

        [HttpGet]
        public IActionResult Arm()
        {
            /*This function calls the action Arm on the device.
             The function does not get a feedback*/
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Arm, serviceClient).Wait();
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
        public IActionResult Alarm()
        {
            /*This function calls the action ALARM on the device.
             The function does not get a feedback*/
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Alarm, serviceClient).Wait();
            return Ok("ALARM!! Command was sent to Device Device! BUZZZZZZ"); // TODO - change to logger
        }
    }
}