using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using System.Collections;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Services;

namespace ThermAlarm.WebApp.Models
{
    public sealed class Alarm
    {
        private IDatabaseManager dbManager;

        /*singelton pattern*/
        public eDeviceAction status { get; set; }
        private Hashtable family;    //hashtable to query fast at runtime (ALARM)
        public ServiceClient serviceClient;

        // A private static instance of the same class
        private static Alarm instance = null;

        // A private constructor to restrict the object creation from outside
        private Alarm(IDatabaseManager dbManager)
        {
            this.dbManager = dbManager;
            this.status = eDeviceAction.Disarm;
            if(dbManager.IsFamily())
            {
                this.family = dbManager.GetFamily();
            }
            else
            {
                this.family = new Hashtable();
            }
            serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            //MsgReceivedEvent.MsgReceived += new msgReceivedHandler(msgReceived_handler);
        }

        public static Alarm GetInstance(IDatabaseManager dbManager)
        {
            // create the instance only if the instance is null
            if (instance == null)
            {
                instance = new Alarm(dbManager);
            }
            // Otherwise return the already existing instance
            return instance;
        }


        #region Family Methods
        
        public void addFamilyMember(Person p)
        {
            this.family.Add(p.BTid, p);
            this.dbManager.AddPersonToFamily(p);
        }

        public void removeFamilyMember(Person p)
        {
            this.family.Remove(p.BTid);
            this.dbManager.RemovePersonFromFamily(p);
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
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, act, serviceClient).Wait();
            this.dbManager.LogAlarmActionInDB(act);
            //TODO - call website action function?
        }

        public void msgReceived_handler(MsgObj msg)
        {
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
            #endregion
        }
    }
}
