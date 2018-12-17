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
        private DataAccess dataAccess;
        public AdressesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/Adresses
        [HttpGet]
        public async Task<IActionResult> Get(string ville = null)
        {
            IEnumerable<CroixRouge.Model.Adresse> entities = await dataAccess.GetAdressesAsync(ville);
            
            var results = Mapper.Map<IEnumerable<AdresseModel>>(entities);

            return Ok(results);
        }

        // GET api/Adresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CroixRouge.Model.Adresse entity = await dataAccess.FindAdresseById(id);
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

            await dataAccess.AddAdresseAsync(entity);

            return Created($"api/Adresses/{entity.Id}", Mapper.Map<AdresseModel>(entity));
        }

        // PUT api/Adresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CroixRouge.DTO.AdresseModel dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Adresse entity = await dataAccess.FindAdresseById(id);
            if (entity == null)
                return NotFound();
            //fixme: améliorer cette implémentation
            await dataAccess.UpdateAdresseAsync(entity, dto);

            return Ok(Mapper.Map<CroixRouge.DTO.AdresseModel>(entity));
        }

        // DELETE api/Adresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CroixRouge.Model.Adresse adr = await dataAccess.FindAdresseById(id);
            if (adr == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
            //if (adr.Utilisateur != null)
                //return BadRequest("FK UTILISATEUR");

            await dataAccess.RemoveAdresseAsync(adr);
            return Ok();
        }
    }
}
