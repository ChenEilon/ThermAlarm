using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ThermAlarm.Common;

namespace ThermAlarm.WebApp
{
    public class Program
    {
        public static ServiceClient serviceClient;
        public static Alarm alarm;

        public static void Main(string[] args)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            alarm = new Alarm();
            MsgReceivedEvent.MsgReceived += new msgReceivedHandler(msgReceived_handler) ;
            //TODO - add or remove people

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void triggerAlarm()
        {
            alarm.status = eDeviceAction.Alarm;
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Alarm, serviceClient).Wait();
            //TODO - call website ALARM function
        }

        public static void triggerDisarm()
        {
            alarm.status = eDeviceAction.Disarm;
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Disarm, serviceClient).Wait();
            //TODO - should system be Armed again after a few second?
            //TODO - call website DISARM function
        }

        public static void triggerArm()
        {
            alarm.status = eDeviceAction.Arm;
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, eDeviceAction.Arm, serviceClient).Wait();
            //TODO - call website ARM function
        }

        public static void msgReceived_handler(MsgObj msg)
        {
            //1. check msg type
            //TODO - figure out what if measurments msg before known bt - should alarm? activly scan? 
            eMsgType type = msg.mType;
            bool member = false;
            if (type == eMsgType.Meausurements || type == eMsgType.MeasurementsAndBT)
            {
                if (alarm.status == eDeviceAction.Arm)
                {
                    if (SensorsProcessing.shouldAlarm(msg.pirValue, msg.thermValue))
                    {
                        triggerAlarm();
                    }

                }
            }
            if(type == eMsgType.BTscan || type == eMsgType.MeasurementsAndBT)
            {
                if (alarm.status == eDeviceAction.Arm)
                {
                    foreach (String BTid in msg.idsBTScan)
                    {
                        if (alarm.isFamilyMember(BTid))
                        {
                            member = true;
                            break;
                        }
                    }
                    if(!member)
                    {
                        triggerAlarm();
                    }
                    else
                    {
                        triggerDisarm();
                    }
                }
            }
            //TODO - should log be here and nor in event processor?
        }
    }
}
