using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThermAlarm.WebApp.Models;
using ThermAlarm.Common;
using ThermAlarm.WebApp.Services;
using System.Net.Http;

namespace ThermAlarm.WebApp.Controllers
{
    public class HomeController : AlarmControllerBase
    {
        public HomeController(IDatabaseManager dbManager, Alarm alarm) : base(dbManager, alarm)
        {
        }

        public IActionResult Index()
        {
            TempData.Keep();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet("addPerson")]
        public IActionResult AddPerson()
        {
            ViewData["Message"] = "Add Person page.";
            TempData["MemberAdded"] = "";
            TempData.Keep();
            return View();
        }

        [HttpPost("addPerson")]
        public IActionResult addPerson(Person p)
        {
            this.addFamilyMember(p);
            TempData["MemberAdded"] = "Member added successfully!";
            TempData.Keep();
            return View();
        }

        [HttpGet("removePerson")]
        public IActionResult RemovePerson()
        {
            ViewData["Message"] = "Remove Person page.";
            TempData["MemberRemoved"] = "";
            TempData.Keep();
            return View();
        }

        [HttpPost("removePerson")]
        public IActionResult removePerson(Person p)
        {
            foreach (Person person in dbManager.FindPersonByEmail(p.email))
                this.removeFamilyMember(person);
            TempData["MemberRemoved"] = "Member removed successfully!";
            TempData.Keep();
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
            TempData["AlarmMsg"] = "";
            TempData.Keep();
            this.triggerAction(eDeviceAction.Arm);
            dbManager.LogAlarmActionInDB(eDeviceAction.Arm);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Disarm()
        {
            TempData["Color"] = "#ABEBC6";
            TempData["AlarmMsg"] = "";
            TempData.Keep();
            this.triggerAction(eDeviceAction.Disarm);
            dbManager.LogAlarmActionInDB(eDeviceAction.Disarm);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Buzz()
        {
            if(alarm.status == eDeviceAction.Alarm)
            {
                TempData["Color"] = "#FF4529";
                TempData["AlarmMsg"] = "Alarm!!!  BUZZZ";
                TempData.Keep();
                dbManager.LogAlarmActionInDB(eDeviceAction.Alarm);
            }
            else if(alarm.status == eDeviceAction.Arm)
            {
                return RedirectToAction("Arm");
            }
            else //disarm
            {
                return RedirectToAction("Disarm");
            }
            return Redirect("/");
        }

        
    }

}
