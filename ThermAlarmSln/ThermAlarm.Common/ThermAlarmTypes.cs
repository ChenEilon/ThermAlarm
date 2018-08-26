using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string BTid { get; set; }

        public Person()
        {
            this.firstName = "";
            this.lastName = "";
            this.email = "";
            this.BTid = "";
        }

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
        [Key]
        public int ID { get; set; }

        public eMsgType mType { get; set; }

        public int pirValue { get; set; }

        [NotMapped]
        public float[] thermValue
        {
            get => Array.ConvertAll(thermValueInternal.Split(','), float.Parse);
            set => thermValueInternal = string.Join(',', value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string thermValueInternal { get; set; }

        [NotMapped]
        public string[] idsBTScan
        {
            get => idsBTScanInternal.Split(',');
            set => idsBTScanInternal = string.Join(',', value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string idsBTScanInternal { get; set; }
    }
}
