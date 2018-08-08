using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ThermAlarm.BackEnd.API;
using ThermAlarm.BackEnd.API.Controllers;

namespace ThermAlarm.BackEnd.API.Controllers
{
    [Produces("application/json")]
    [Route("api/WebApp")]
    public class WebAppController : Controller
    {

        [HttpPost]
        public OkResult Post(IdentityUser user)//TODO changed from IHttpActionResult, since there's nothing there yet
        {
            SQLConnection db = new SQLConnection();
            /* TODO need to add all of these funcs to the class
            db.user.Add(user);
            db.SaveChanges();
            */
            return Ok();
           
        }
    }
}
