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
        private DataAccess dataAccess;

        public CollectesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/Collectes
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex= Constants.Paging.PAGE_INDEX, int? pageSize = Constants.Paging.PAGE_SIZE, bool horairesAJour = true)
        {
            IEnumerable<Collecte> entities = await dataAccess.GetCollectesAsync(pageIndex, pageSize, horairesAJour);

            var results = Mapper.Map<IEnumerable<CollecteDTO>>(entities);

            return Ok(results);
        }

        // GET api/Collectes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Collecte entity = await dataAccess.FindCollecteById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<CollecteDTO>(entity);

            return Ok(result);
        }

        // POST api/Collectes
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]CollecteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<Collecte>(dto);

            await dataAccess.AddCollecteAsync(entity);
            
            return Created($"api/Collectes/{entity.Id}", Mapper.Map<CollecteDTO>(entity));
        }

        // PUT api/Collectes/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Put(int id, [FromBody]CollecteDTO dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            Collecte entity = await dataAccess.FindCollecteById(id);
            if (entity == null)
                return NotFound();

            await dataAccess.UpdateCollecteAsync(entity, dto);

            return Ok(Mapper.Map<CollecteDTO>(entity));
        }

        // DELETE api/Collectes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Delete(int id,bool suppressionHorraire = false)
        {
            Collecte entity = await dataAccess.FindCollecteById(id);
            if (entity == null)
                return NotFound();

            await dataAccess.RemoveCollecteAsync(entity,suppressionHorraire);

            return Ok();
        }




    }
}
