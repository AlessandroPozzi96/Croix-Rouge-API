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
    [Route("api/[controller]")]
    public class JoursouverturesController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public JoursouverturesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }

        // GET api/Jourouvertures
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<CroixRouge.Model.Jourouverture> entities = await dataAccess.GetJoursouverturesAsync();

            var results = Mapper.Map<IEnumerable<JourouvertureModel>>(entities);

            return Ok(results);
        }

        // GET api/Jourouvertures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CroixRouge.Model.Jourouverture entity = await dataAccess.FindJourouvertureById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<JourouvertureModel>(entity);

            return Ok(result);
        }

        // POST api/Jourouvertures
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.JourouvertureModel dto)
        {
            var entity = Mapper.Map<CroixRouge.Model.Jourouverture>(dto);

            Collecte collecte = await dataAccess.FindCollecteById(dto.FkCollecte);
            if (collecte == null)
                return NotFound();

            entity.FkCollecteNavigation = collecte;

            await dataAccess.AddJourouvertureAsync(entity);
            
            return Created($"api/Joursouvertures/{entity.Id}", Mapper.Map<JourouvertureModel>(entity));
        }

        // PUT api/Jourouvertures/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Put(int id, [FromBody]CroixRouge.DTO.JourouvertureModel dto)
        {
            CroixRouge.Model.Jourouverture entity = await dataAccess.FindJourouvertureById(id);
            if (entity == null)
                return NotFound();

            Collecte collecte = await dataAccess.FindCollecteById(dto.FkCollecte);
            if (collecte == null)
                return NotFound();
            
            entity = Mapper.Map<Jourouverture>(dto);
            entity.FkCollecteNavigation = collecte;

            await dataAccess.UpdateJourouvertureAsync(entity, dto);

            return Ok(Mapper.Map<CroixRouge.DTO.JourouvertureModel>(entity));
        }

        // DELETE api/Jourouvertures/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            CroixRouge.Model.Jourouverture entity = await dataAccess.FindJourouvertureById(id);
            if (entity == null)
                return NotFound();

            await dataAccess.RemoveJourouvertureAsync(entity);

            return Ok();
        }
    }
}
