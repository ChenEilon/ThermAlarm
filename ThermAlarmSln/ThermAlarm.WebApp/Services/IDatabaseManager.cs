using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermAlarm.Common;

namespace ThermAlarm.WebApp.Services
{
    public interface IDatabaseManager
    {
        #region Family Methods

        bool IsFamily();
        Dictionary<string, Person> GetFamily();
        void UpdateFamily(Dictionary<string, Person> family);
        void AddPersonToFamily(Person p);
        void RemovePersonFromFamily(Person p);
        Person[] FindPersonByEmail(String email);

        #endregion

        #region Msg Methods

        void LogInDB(MsgObj msg);

        #endregion

        #region Alarm Methods
        void LogAlarmActionInDB(eDeviceAction act);
        #endregion
    }
}
