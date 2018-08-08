using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermAlarm.BackEnd.API
{
    public class Person
    {

        private char[] firstName;
        private char[] lastName;
        private char[] password;
        private char[] email;

        public Person(String firstName, String lastName, String password, String email)
        {
            if (firstName.Length > 1)
            {
                this.firstName = new char[firstName.Length];
                firstName.CopyTo(0, this.lastName, 0, firstName.Length);//TODO possibly need to add -1
            }
            if (lastName.Length > 1)
            {
                this.lastName = new char[lastName.Length];
                lastName.CopyTo(0, this.lastName, 0, lastName.Length);//TODO possibly need to add -1
            }
            if (password.Length > 4)
            {//TODO need to add more security checks
                password.CopyTo(0, this.password, 0, password.Length);//TODO possibly need to add -1
            }
            if(email.Length>2 && email.Contains('@') && email.Contains('.'))
            {
                this.email = new char[email.Length];
                email.CopyTo(0, this.email, 0, email.Length);
            }

        }
        public char[] get_first_name()
        {
            return this.firstName;
        }

        public char[] get_last_name()
        {
            return this.lastName;
        }

        public char[] get_password()
        {
            return this.password;
        }

        public char[] get_email()
        {
            return this.email;
        }

        public String set_first_name(String newFirstName)
        {
            if (newFirstName.Length > 1)
            {
                this.firstName = new char[newFirstName.Length];
                newFirstName.CopyTo(0, this.lastName, 0, newFirstName.Length);//TODO possibly need to add -1
                return "Update first name successful\n";
            }
            else
                return "Illegal first name\n";
        }

        public String set_last_name(String newLastName)
        {
            if (newLastName.Length > 1)
            {
                this.lastName = new char[newLastName.Length];
                newLastName.CopyTo(0, this.lastName, 0, newLastName.Length);//TODO possibly need to add -1
                return "Update last name successful\n";
            }
            else
                return "Illegal last name";
        }

        public String set_password(String newPassword)
        {
            if (newPassword.Length > 4)
            {//TODO need to add more security checks
                newPassword.CopyTo(0, this.password, 0, newPassword.Length);//TODO possibly need to add -1
                return "Managed to update password\n";
            }
            else
            {
                //TODO update this line, according to the security checks
                return "Please choose a password that contains at least __ charas, at least one none alphanumeric, at least one captial letter\n";

            }
        }

        public String set_email( String email)
        {
            if (email.Length > 2 && email.Contains('@') && email.Contains('.'))
            {
                this.email = new char[email.Length];
                email.CopyTo(0, this.email, 0, email.Length);
                return "Updated email";
            }
            else
                return "Email invalid";
        }


    }
}
