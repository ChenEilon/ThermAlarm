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
        public Person(String firstName, String lastName, String password, String email, String BTid)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.BTid = BTid;
        }

        public Person()
        {
            this.firstName = "";
            this.lastName = "";
            this.email = "";
            this.BTid = "";
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string BTid { get; set; }
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
        public int Id { get; set; }

        public eMsgType mType { get; set; }

        public int pirValue { get; set; }

        [NotMapped]
        public float[] thermValue
        {
            get => Array.ConvertAll(thermValueInternal.Split(',', StringSplitOptions.RemoveEmptyEntries), float.Parse);
            set => thermValueInternal = string.Join(',', value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string thermValueInternal { get; set; } = "";

        [NotMapped]
        public string[] idsBTScan
        {
            get => idsBTScanInternal.Split(',', StringSplitOptions.RemoveEmptyEntries);
            set => idsBTScanInternal = string.Join(',', value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string idsBTScanInternal { get; set; } = "";
    }

    public class AlarmAction
    {
        public AlarmAction(eDeviceAction act)
        {
            this.Type = act;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public eDeviceAction Type { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }
}
