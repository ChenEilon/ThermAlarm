using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Services;

namespace ThermAlarm.WebApp.Controllers
{
    public class AlarmControllerBase : Controller
    {
        public IDatabaseManager dbManager;
        public Alarm alarm;

        public AlarmControllerBase(IDatabaseManager dbManager, Alarm alarm)
        {
            this.dbManager = dbManager;
            this.alarm = alarm;
            this.alarm.family = dbManager.GetFamily();
        }

        #region Family Methods

        protected void addFamilyMember(Person p)
        {
            alarm.addFamilyMember(p);
            dbManager.AddPersonToFamily(p);
        }

        protected void removeFamilyMember(Person p)
        {
            alarm.removeFamilyMember(p);
            dbManager.RemovePersonFromFamily(p);
        }

        #endregion

        #region Event handeling

        protected void triggerAction(eDeviceAction act)
        {
            alarm.triggerAction(act);
            dbManager.LogAlarmActionInDB(act);
            if (act == eDeviceAction.Disarm)
                dbManager.UpdateFamily(alarm.family);
        }

        #endregion
    }
}
