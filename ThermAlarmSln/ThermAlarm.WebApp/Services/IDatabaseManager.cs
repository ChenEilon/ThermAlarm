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
        Hashtable GetFamily();
        void AddPersonToFamily(Person p);
        void RemovePersonFromFamily(Person p);

        #endregion

        #region Msg Methods

        void LogInDB(MsgObj msg);

        #endregion
    }
}
