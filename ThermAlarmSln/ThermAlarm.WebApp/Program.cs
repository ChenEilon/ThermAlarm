﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Models;

namespace ThermAlarm.WebApp
{
    public class Program
    {
        
        //public static Alarm alarm;

        public static void Main(string[] args)
        {
            
            //alarm = Alarm.GetInstance();
            
            //TODO - add or remove people?

            var host = BuildWebHost(args);
            MigrateDatabase(host);
            host.Run();
        }

        public static void MigrateDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ThermAlarmDbContext>();
                context.Database.Migrate();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
