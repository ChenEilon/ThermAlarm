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
            this.alarm.initFamily(dbManager.GetFamily());
        }

        #region Family Methods

        protected void addFamilyMember(Person p)
        {
            this.alarm.addFamilyMember(p);
            this.dbManager.AddPersonToFamily(p);
        }

        protected void removeFamilyMember(Person p)
        {
            this.alarm.removeFamilyMember(p);
            this.dbManager.RemovePersonFromFamily(p);
        }

        #endregion

        #region Event handeling

        protected void triggerAction(eDeviceAction act)
        {
            this.alarm.triggerAction(act);
            this.dbManager.LogAlarmActionInDB(act);
        }

        #endregion
    }
}
