using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThermAlarm.WebApp.Controllers
{
    //[Produces("application/json")]
    [Route("api/[Controller]")]
    public class DeviceController : Controller
    {
        // GET: api/Device
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Device/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/Device/5
        [HttpGet]
        public string Hello()
        {
            return "Hiiiiii!!";
        }

        // POST: api/Device
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Device/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
