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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AlertesController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public AlertesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/alertes
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex=Constants.Paging.PAGE_INDEX, int? pageSize = Constants.Paging.PAGE_SIZE, string nom = null)
        {
            IEnumerable<Alerte> entities = await dataAccess.GetAlertesAsync(pageIndex, pageSize, nom);
            
            var results = Mapper.Map<IEnumerable<AlerteDTO>>(entities);

            return Ok(results);
        }

        // GET api/Alertes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Alerte entity = await dataAccess.FindAlerteById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<AlerteDTO>(entity);

            return Ok(result);
        }

        // POST api/Alertes
        [HttpPost]
        //[Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]AlerteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<Alerte>(dto);

            await dataAccess.AddAlerteAsync(entity);

            return Created($"api/Alertes/{entity.Id}", Mapper.Map<AlerteDTO>(entity));
        }

        // PUT api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AlerteDTO dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            Alerte entity = await dataAccess.FindAlerteById(id);
            if (entity == null)
                return NotFound();
            await dataAccess.UpdateAlerteAsync(entity, dto);

            return Ok(Mapper.Map<AlerteDTO>(entity));
        }

        // DELETE api/Alertes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Alerte alerte = await dataAccess.FindAlerteById(id);
            if (alerte == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
                
            await dataAccess.RemoveAlerteAsync(alerte);

            return Ok();
        }


    }
}
