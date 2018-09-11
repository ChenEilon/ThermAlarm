using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Models;

namespace ThermAlarm.WebApp.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        private ThermAlarmDbContext context;

        public DatabaseManager(ThermAlarmDbContext context)
        {
            this.context = context;
        }

        #region Family Methods

        public bool IsFamily()
        {
            return context.Person.Count() > 0;
        }

        public Dictionary<string, Person> GetFamily()
        {
            Dictionary<string, Person> family = new Dictionary<string, Person>();
            foreach (Person p in context.Person.AsNoTracking())
                family.Add(p.BTid, p);
            return family;
        }

        public void UpdateFamily(Dictionary<string, Person> family)
        {
            context.Person.UpdateRange(family.Values.ToArray());
            context.SaveChanges();
        }

        public void AddPersonToFamily(Person p)
        {
            context.Person.Add(p);
            context.SaveChanges();
        }

        public void RemovePersonFromFamily(Person p)
        {
            context.Person.Remove(p);
            context.SaveChanges();
        }

        public Person[] FindPersonByEmail(String email)
        {
            return context.Person.AsNoTracking().Where(p => p.email == email).ToArray();
        }

        #endregion

        #region Msg Methods

        public void LogInDB(MsgObj msg)
        {
            context.Msg.Add(msg);
            context.SaveChanges();
        }

        #endregion

        #region Alarm Methods

        public void LogAlarmActionInDB(eDeviceAction act)
        {
            context.AlarmAction.Add(new AlarmAction(act));
            context.SaveChanges();
        }

        #endregion
    }
}
