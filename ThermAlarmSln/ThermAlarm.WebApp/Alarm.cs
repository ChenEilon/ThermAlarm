using Microsoft.Azure.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermAlarm.Common;

namespace ThermAlarm.WebApp
{
    public sealed class Alarm
    {
        /*singelton pattern*/
        public eDeviceAction status { get; set; }
        private Hashtable family;
        public ServiceClient serviceClient;

        // A private static instance of the same class
        private static Alarm instance = null;

        // A private constructor to restrict the object creation from outside
        private Alarm()
        {
            this.status = eDeviceAction.Disarm;
            this.family = new Hashtable(); //TODO - add database read from DB, if family exist, return hashtable of it
            serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            MsgReceivedEvent.MsgReceived += new msgReceivedHandler(msgReceived_handler);
        }

        public static Alarm GetInstance()
        {
            // create the instance only if the instance is null
            if (instance == null)
            {
                instance = new Alarm();
            }
            // Otherwise return the already existing instance
            return instance;
        }


        #region Family Methods
        //TODO - add database calls.. family should be held as hashtable in runtime for making BT id query fast (ALARM is at runtime)
        public void addFamilyMember(Person p)
        {
            this.family.Add(p.BTid, p);
            //DatabaseMgr.AddPersonToFamily(p);
        }

        public void removeFamilyMember(Person p)
        {
            this.family.Remove(p.BTid);
            //DatabaseMgr.RemovePersonFromFamily(p);
        }

        public bool isFamilyMember(String BTid)
        {
            return family.ContainsKey(BTid);
        }

        #endregion

        #region Event handeling
        public void triggerAction(eDeviceAction act)
        {
            this.status = act;
            //DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, act, serviceClient).Wait();
            //TODO - call DB here
            //TODO - call website action function?
        }

        public void msgReceived_handler(MsgObj msg)
        {
            //1. check msg type
            //TODO - figure out what if measurments msg before known bt - should alarm? activly scan? 
            eMsgType type = msg.mType;
            bool member = false;
            if (type == eMsgType.Meausurements || type == eMsgType.MeasurementsAndBT)
            {
                if (this.status == eDeviceAction.Arm)
                {
                    if (SensorsProcessing.shouldAlarm(msg.pirValue, msg.thermValue))
                    {
                        triggerAction(eDeviceAction.Alarm);
                    }

                }
            }
            if (type == eMsgType.BTscan || type == eMsgType.MeasurementsAndBT)
            {
                if (this.status == eDeviceAction.Arm)
                {
                    foreach (String BTid in msg.idsBTScan)
                    {
                        if (this.isFamilyMember(BTid))
                        {
                            member = true;
                            break;
                        }
                    }
                    if (!member)
                    {
                        triggerAction(eDeviceAction.Alarm);
                    }
                    else
                    {
                        triggerAction(eDeviceAction.Disarm);
                    }
                }
            }
            //TODO - should log be here and nor in event processor?
            #endregion
        }
    }
}


