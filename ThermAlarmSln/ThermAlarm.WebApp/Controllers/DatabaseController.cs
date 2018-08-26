using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Models;

namespace ThermAlarm.WebApp.Controllers
{
    public class DatabaseController
    {
        private ThermAlarmDbContext context;

        public DatabaseController(ThermAlarmDbContext context)
        {
            this.context = context;
        }

        #region Family Methods

        public bool IsFamily()
        {
            return context.Person.Count() > 0;
        }

        public Hashtable GetFamily()
        {
            Hashtable family = new Hashtable();
            foreach (Person p in context.Person)
                family.Add(p.BTid, p);
            return family;
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

        #endregion
    }
}
