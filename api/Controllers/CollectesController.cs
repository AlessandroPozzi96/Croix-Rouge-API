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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using CroixRouge.Dal;
using AutoMapper;

namespace CroixRouge.api.Controllers
{
    [Route("api/[controller]")]
    public class CollectesController : Controller
    {
        private bdCroixRougeContext _context;

        public CollectesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET api/Collectes
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex=0, int? pageSize = 10)
        {
            IEnumerable<CroixRouge.Model.Collecte> entities = await _context.Collecte
            .OrderBy(collecte => collecte.Id)
            .Include(c => c.Jourouverture)
                .ThenInclude(j => j.FkTrancheHoraireNavigation)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
            
            var results = Mapper.Map<IEnumerable<CollecteModel>>(entities);

            return Ok(results);
        }

        // GET api/Collectes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CroixRouge.Model.Collecte entity = await FindCollecteById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<CollecteModel>(entity);

            return Ok(result);
        }

        // POST api/Collectes
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.CollecteModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<CroixRouge.Model.Collecte>(dto);
            _context.Collecte.Add(entity);
            await _context.SaveChangesAsync();
            return Created($"api/Collectes/{entity.Id}", Mapper.Map<CollecteModel>(entity));
        }

        // PUT api/Collectes/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Put(int id, [FromBody]CroixRouge.DTO.CollecteModel dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Collecte entity = await FindCollecteById(id);
            if (entity == null)
                return NotFound();
            //fixme: améliorer cette implémentation
            entity.Nom = dto.Nom;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;
            entity.Telephone = dto.Telephone;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(entity).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
            return Ok(Mapper.Map<CroixRouge.DTO.CollecteModel>(entity));
        }

        // DELETE api/Collectes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            CroixRouge.Model.Collecte entity = await FindCollecteById(id);
            if (entity == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();

            _context.Collecte.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public Task<CroixRouge.Model.Collecte> FindCollecteById(int id)
        {
            return _context.Collecte
            .Include(c => c.Jourouverture)
                .ThenInclude(j => j.FkTrancheHoraireNavigation)
            .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
