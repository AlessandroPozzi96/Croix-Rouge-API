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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AlertesController : Controller
    {
        private bdCroixRougeContext _context;

        public AlertesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET api/alertes
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex=0, int? pageSize = 10, string nom = null)
        {
            IEnumerable<CroixRouge.Model.Alerte> entities = await _context.Alerte
            .Where(alerte => nom == null || alerte.Nom.Contains(nom))
            .OrderBy(alerte => alerte.Id)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
            
            var results = Mapper.Map<IEnumerable<AlerteModel>>(entities);

            return Ok(results);
        }

        // GET api/Alertes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CroixRouge.Model.Alerte entity = await FindAlerteById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<AlerteModel>(entity);

            return Ok(result);
        }

        // POST api/Alertes
        [HttpPost]
        //[Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.AlerteModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<CroixRouge.Model.Alerte>(dto);
            _context.Alerte.Add(entity);
            await _context.SaveChangesAsync();
            return Created($"api/Alertes/{entity.Id}", Mapper.Map<AlerteModel>(entity));
        }

        // PUT api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CroixRouge.DTO.AlerteModel dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Alerte entity = await FindAlerteById(id);
            if (entity == null)
                return NotFound();
            //fixme: améliorer cette implémentation
            entity.Nom = dto.Nom;
            entity.Contenu = dto.Contenu;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(entity).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
            return Ok(Mapper.Map<CroixRouge.DTO.AlerteModel>(entity));
        }

        // DELETE api/Alertes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CroixRouge.Model.Alerte alerte = await FindAlerteById(id);
            if (alerte == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
            _context.Alerte.Remove(alerte);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public Task<CroixRouge.Model.Alerte> FindAlerteById(int id)
        {
            return _context.Alerte.FindAsync(id);
        }
    }
}
