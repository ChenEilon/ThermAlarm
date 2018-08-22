using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public enum eMsgType
    {
        Meausurements = 0,
        BTscan = 1,
        MeasurementsAndBT = 2
    }

    public enum eDeviceAction
    {
        Arm = 0,
        Disarm = 1,
        Alarm = 2
    }

    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string BTid { get; set; }

        public Person(String firstName, String lastName, String password, String email, String BTid)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.BTid = BTid;
        }
    }

    public class deviceAction
    {
        public string Name;
        public String Parameters;
        public deviceAction(string Name, String parameters)
        {
            this.Name = Name;
            if (this.Parameters == null)
            {
                this.Parameters = "{}";
            }
            else
            {
                this.Parameters = parameters;
            }

        }
    }

    public class MsgObj
    {
        public eMsgType mType { get; set; }

        public int pirValue { get; set; }

        public float[] thermValue { get; set; }

        public string[] idsBTScan { get; set; }

    }
}
