using ThermAlarm.BackEnd.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ThermAlarm.BackEnd.API.Person;


namespace ThermAlarm.BackEnd.API
{

    public class Home
    {
        private int num_users=0;
        private Person[] users;

        
        bool emailExists(String email) {
            for(int i=0; i< num_users; i++)
            {
                //if (users[i].get_email())
                   // return true;
            }
            return false;
        }
        bool firstNameExists(string firstName)
        {
            for (int i = 0; i < num_users; i++)
            {
                //if (users[i].get_email())
                // return true;
            }
            return false;
        }
        bool lastNameExists(string lastName)
        {
            for (int i = 0; i < num_users; i++)
            {
                //if (users[i].get_email())
                // return true;
            }
            return false;
        }
        bool personExists(string email, string firstName, string lastName)
        {
            if (emailExists(email))
                if (lastNameExists(lastName))
                    if (firstNameExists(firstName))
                        return true;
            return false;
        }

        void createPerson(string email, string firstName, string lastName, string password)
        {
            if (!personExists(email, firstName, lastName))
            {
                users[num_users] = new Person(firstName, lastName, password, email);
                num_users++;
            }
        }

        void deletePerson()//TODO how do we want this to work?
        {
        }

    }
}
