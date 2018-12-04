using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using api.model;
using model;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AlertesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Alerte>> Get()
        {
            User.Claims.ToList().ForEach(claim => Console.WriteLine($"Claim : {claim.Type}: {claim.Value}"));
            var dbCroixRougeContext = new bdCroixRougeContext();
            return dbCroixRougeContext.Alerte.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Alerte> Get(int id)
        {
            var dbCroixRougeContext = new bdCroixRougeContext();
            return (Alerte)dbCroixRougeContext.Alerte.ToArray().GetValue(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
