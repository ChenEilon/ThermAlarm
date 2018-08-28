using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ThermAlarm.Common;


namespace ThermAlarm.WebApp
{
    public class Program
    {
        
        public static Alarm alarm;

        public static void Main(string[] args)
        {
            
            alarm = Alarm.GetInstance();
            
            //TODO - add or remove people?


            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        
        
    }
}
