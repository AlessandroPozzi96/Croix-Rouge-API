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
    public class AdressesController : Controller
    {
        private bdCroixRougeContext _context;

        public AdressesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET api/Adresses
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex=0, int? pageSize = 3, string ville = null)
        {
            IEnumerable<CroixRouge.Model.Adresse> entities = await _context.Adresse
            .Where(adr => ville == null || adr.Ville.Contains(ville))
            .OrderBy(adr => adr.Id)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
            
            var results = Mapper.Map<IEnumerable<AdresseModel>>(entities);

            return Ok(results);
        }

        // GET api/Adresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CroixRouge.Model.Adresse entity = await FindAdresseById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<AdresseModel>(entity);

            return Ok(result);
        }

        // POST api/Adresses
        [HttpPost]
        //[Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.AdresseModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<CroixRouge.Model.Adresse>(dto);
            _context.Adresse.Add(entity);
            await _context.SaveChangesAsync();
            return Created($"api/Adresses/{entity.Id}", Mapper.Map<AdresseModel>(entity));
        }

        // PUT api/Adresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CroixRouge.DTO.AdresseModel dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Adresse entity = await FindAdresseById(id);
            if (entity == null)
                return NotFound();
            //fixme: améliorer cette implémentation
            entity.Ville = dto.Ville;
            entity.Rue = dto.Rue; 
            entity.Numero = dto.Numero;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(entity).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
            return Ok(Mapper.Map<CroixRouge.DTO.AdresseModel>(entity));
        }

        // DELETE api/Adresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CroixRouge.Model.Adresse adr = await FindAdresseById(id);
            if (adr == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
            _context.Adresse.Remove(adr);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public Task<CroixRouge.Model.Adresse> FindAdresseById(int id)
        {
            return _context.Adresse.FindAsync(id);
        }
    }
}
