using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermAlarm.Common;

namespace ThermAlarm.WebApp.Models
{
    public class ThermAlarmDbContext : DbContext
    {
        public ThermAlarmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MsgObj> Msg { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}
