using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CroixRouge.DTO;
using CroixRouge.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CroixRouge.Dal;
using AutoMapper;

namespace CroixRouge.api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
    [Route("api/[controller]")]
    public class LancerAlertesController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public LancerAlertesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/LancerAlertes
        [HttpGet]
        public async Task<IActionResult> Get(string login = null)
        {
            IEnumerable<Lanceralerte> entities = await dataAccess.GetLanceralertesAsync(login);
            
            var results = Mapper.Map<IEnumerable<LanceralerteDTO>>(entities);

            return Ok(results);
        }

        // GET api/Lanceralertes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Lanceralerte entity = await dataAccess.FindLanceralerteById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<LanceralerteDTO>(entity);

            return Ok(result);
        }

        // POST api/LancerAlertes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LanceralerteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<Lanceralerte>(dto);

            await dataAccess.AddLanceralerteAsync(entity);

            return Created($"api/Lanceralertes/{entity.Id}", Mapper.Map<LanceralerteDTO>(entity));
        }

        // PUT api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AlerteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Lanceralerte entity = await dataAccess.FindLanceralerteById(id);
            if (entity == null)
                return NotFound();

            entity = Mapper.Map<Lanceralerte>(dto);
            await dataAccess.UpdateLanceralerteAsync(entity);

            return Ok(Mapper.Map<LanceralerteDTO>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Lanceralerte entity = await dataAccess.FindLanceralerteById(id);
            if (entity == null)
                return NotFound();
                
            await dataAccess.RemoveLanceralerteAsync(entity);

            return Ok();
        }


    }
}
