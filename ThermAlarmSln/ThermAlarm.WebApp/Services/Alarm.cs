using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using System.Collections;
using ThermAlarm.Common;


namespace ThermAlarm.WebApp.Services
{
    public sealed class Alarm
    {
        public eDeviceAction status { get; set; }
        private Hashtable family;    //hashtable to query fast at runtime (ALARM)
        public ServiceClient serviceClient;

        // A private constructor to restrict the object creation from outside
        public Alarm()
        {
            this.status = eDeviceAction.Disarm; // TODO: read from db
            serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            //MsgReceivedEvent.MsgReceived += new msgReceivedHandler(msgReceived_handler);
        }

        #region Family Methods

        public void initFamily(Hashtable family)
        {
            this.family = family;
        }

        public void addFamilyMember(Person p)
        {
            this.family.Add(p.BTid, p);
        }

        public void removeFamilyMember(Person p)
        {
            this.family.Remove(p.BTid);
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
            //TODO - call website action function?
        }

        public void msgReceived_handler(MsgObj msg)
        {
            eMsgType type = msg.mType;
            if (type == eMsgType.MeasurementsAndBT)
            {
                if (this.status == eDeviceAction.Arm)
                {
                    if (SensorsProcessing.shouldAlarm(msg.pirValue, msg.thermValue))
                    {
                        if(msg.idsBTScan.Length > 0)
                        {
                            foreach (String BTid in msg.idsBTScan)
                            {
                                if (this.isFamilyMember(BTid))
                                {
                                    triggerAction(eDeviceAction.Disarm);
                                    return;
                                }
                            }
                        }
                        triggerAction(eDeviceAction.Alarm);
                    }

                }
            }
            
        }

        #endregion
    }
}
