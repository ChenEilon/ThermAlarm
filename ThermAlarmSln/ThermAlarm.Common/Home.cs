using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermAlarm.Common
{

    public class Home
    {
        private int num_users;
        private Person[] users; //TODO - should we use list/dict/other of persons?...

        public Home()
        {
            this.num_users = 0;
            this.users = null;
        }
        

        void addPerson(Person p)
        {
            if (p == null)
                Console.WriteLine("ERROR - Person is NULL!");
            else
            {
                num_users++;
                //TODO - should we use list/dict/other of persons?...
            }
        }

        void deletePerson()
        {
        }

    }
}
