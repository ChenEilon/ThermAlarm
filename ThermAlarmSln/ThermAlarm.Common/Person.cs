using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermAlarm.Common
{
    public class Person
    {
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string password { get; set; } //Safety?..
        private string email { get; set; }

        public Person(String firstName, String lastName, String password, String email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.email = email;
        }

        

    }
}
