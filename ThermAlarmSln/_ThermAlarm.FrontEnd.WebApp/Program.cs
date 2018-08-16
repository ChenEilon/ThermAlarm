using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ThermAlarm.FrontEnd.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ThermAlarm.FrontEnd.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host= BuildWebHost(args);
            MigrateDatabase(host);
            host.Run();
        }

        //now, instead of here, it being connected to db, it'll be connected to backend, and send all info to it.

        public static void MigrateDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider
                              .GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
