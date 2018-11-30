using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using api.model;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AlertesController : ControllerBase
    {
        private Alerte[] alertes = new Alerte[] {
            new Alerte(1, "Stock AB+ faible", "La croix rouge estime que le niveau de sang AB+ ne sera pas suffisant pour satisfaire la demande, nous vous demandons votre aide !"), 
            new Alerte(2, "Stock O-", "Message a tous les donneurs universelles, suite aux récents attentats nous vous demandons de venir le plus vite possible donner votre sang pour sauver un maximum de personnes"), 
            new Alerte(3, "Stock All", "Cher donneurs et cher donneuses, la croix rouge dispose de suffisamment de sang pour le moment, ne vous sentez pas obligué de venir ces temps ci")
        };
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Alerte>> Get()
        {
            User.Claims.ToList().ForEach(claim => Console.WriteLine($"Claim : {claim.Type}: {claim.Value}"));
            return this.alertes;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Alerte> Get(int id)
        {
            return this.alertes[id];
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
