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
        public Alarm alarm;
        bool isFirst = true;
        

        public HomeController()//IDatabaseManager databaseManager)
        {
          //  this.databaseManager = databaseManager;
          this.alarm = Alarm.GetInstance();
        
        }

        public IActionResult Index()
        {
            TempData.Keep();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet("addPerson")]
        public IActionResult AddPerson()
        {
            ViewData["Message"] = "Add Person page.";

            return View();
        }
        [HttpPost("addPerson")]
        public IActionResult AddPerson(Person p)
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
            TempData["Color"] = "#F9E79F";
            TempData.Keep();
            this.alarm.triggerAction(eDeviceAction.Arm);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Disarm()
        {
            TempData["Color"] = "#ABEBC6";
            TempData.Keep();
            ViewData["visibility"] = "hidden";
            this.alarm.triggerAction(eDeviceAction.Disarm);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Buzz()
        {
            TempData["Color"] = "red";
            TempData.Keep();
            ViewData["visibility"] = "";
            this.alarm.triggerAction(eDeviceAction.Alarm);
            return Redirect("/");
        }

        
    }

}
