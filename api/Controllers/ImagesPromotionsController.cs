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
    public class ImagesPromotionsController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public ImagesPromotionsController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/Collectes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Imagepromotion> entities = await dataAccess.GetImagepromotionAsync();

            var results = Mapper.Map<IEnumerable<ImagepromotionDTO>>(entities);

            return Ok(results);
        }

        // GET api/Collectes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Imagepromotion entity = await dataAccess.FindImagepromotionById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<ImagepromotionDTO>(entity);

            return Ok(result);
        }

        // POST api/Collectes
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]ImagepromotionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<Imagepromotion>(dto);

            await dataAccess.AddImagepromotionAsync(entity);
            
            return Created($"api/Imagepromotion/{entity.Id}", Mapper.Map<ImagepromotionDTO>(entity));
        }

        // PUT api/Collectes/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Put(int id, [FromBody]ImagepromotionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            Imagepromotion entity = await dataAccess.FindImagepromotionById(id);
            if (entity == null)
                return NotFound();

            await dataAccess.UpdateImagePromotionAsync(entity, dto);

            return Ok(Mapper.Map<ImagepromotionDTO>(entity));
        }

        // DELETE api/Collectes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            Imagepromotion entity = await dataAccess.FindImagepromotionById(id);
            if (entity == null)
                return NotFound();

            await dataAccess.RemoveImagepromotionAsync(entity);

            return Ok();
        }
    }
}
