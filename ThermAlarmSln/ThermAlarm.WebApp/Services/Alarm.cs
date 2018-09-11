using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using System.Collections;
using ThermAlarm.Common;
using System.Threading;

namespace ThermAlarm.WebApp.Services
{
    public sealed class Alarm
    {
        public ServiceClient serviceClient;
        private Dictionary<string, Person> _family;    // dictionary to query fast at runtime (ALARM)
        private TimeSpan btForgetTime;
        private TimeSpan autoRearmTime;
        private Task autoRearmTask;
        private CancellationTokenSource autoRearmTokenSource;
        private CancellationToken autoRearmToken;

        // A private constructor to restrict the object creation from outside
        public Alarm()
        {
            this.serviceClient = ServiceClient.CreateFromConnectionString(Configs.SERVICE_CONNECTION_STRING);
            this.btForgetTime = new TimeSpan(0, Configs.BT_FORGET_TIME, 0);
            this.autoRearmTime = new TimeSpan(0, 0, Configs.AUTO_REARM_TIME);
            this.status = eDeviceAction.Disarm;    // TODO: read from db
        }

        public eDeviceAction status { get; set; }

        public Dictionary<string, Person> family
        {
            get => _family;
            set
            {
                if (_family == null)
                    _family = value;
            }
        }

        #region Family Methods

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

        public void triggerAction(eDeviceAction act, bool shouldCancelAutoRearm = true)
        {
            if (shouldCancelAutoRearm)
                cancelAutoRearm();
            this.status = act;
            DeviceMgr.CallDeviceAction(Configs.DEVICE_NAME, act, serviceClient).Wait();
            //TODO - call website action function?
        }

        public void msgReceived_handler(MsgObj msg)
        {
            if (msg.mType == eMsgType.MeasurementsAndBT)
            {
                DateTime now = DateTime.Now;
                foreach (string BTid in msg.idsBTScan)
                    if (isFamilyMember(BTid))
                    {
                        if (status == eDeviceAction.Arm && now.Subtract(family[BTid].LastSeen) > btForgetTime)
                        {
                            triggerAction(eDeviceAction.Disarm);
                            autoRearmTokenSource = new CancellationTokenSource();
                            autoRearmToken = autoRearmTokenSource.Token;
                            autoRearmTask = Task.Delay(autoRearmTime, autoRearmToken)
                                .ContinueWith(t => triggerAction(eDeviceAction.Arm, false), autoRearmToken);
                        }
                        family[BTid].LastSeen = now;
                    }
                if (status == eDeviceAction.Arm && SensorsProcessing.shouldAlarm(msg.pirValue, msg.thermValue))
                    triggerAction(eDeviceAction.Alarm);
            }
        }

        private void cancelAutoRearm()
        {
            if (autoRearmTask != null && !autoRearmTask.IsCompleted)
                try
                {
                    autoRearmTokenSource.Cancel();
                }
                catch
                {
                }
        }

        #endregion
    }
}
