using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThermAlarm.WebApp.Models;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Services;

namespace ThermAlarm.WebApp.Controllers
{

    public class HomeController : Controller
    {
       // private IDatabaseManager databaseManager;

        //public HomeController(IDatabaseManager databaseManager)
        //{
          //  this.databaseManager = databaseManager;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(Person p)
        {
            //databaseManager.AddPersonToFamily(p);
            //TODO enable...
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Arm()
        {
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Disarm()
        {
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Alarm()
        {
            return Redirect("/");
        }

        
    }

}
