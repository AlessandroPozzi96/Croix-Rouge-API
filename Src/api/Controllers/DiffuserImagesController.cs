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
    public class DiffuserImagesController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public DiffuserImagesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/alertes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Diffuserimage> entities = await dataAccess.GetDiffuserimageAsync();
            
            var results = Mapper.Map<IEnumerable<DiffuserimageDTO>>(entities);

            return Ok(results);
        }

        // GET api/Alertes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Diffuserimage entity = await dataAccess.FindDiffuserimageById(id);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<DiffuserimageDTO>(entity);

            return Ok(result);
        }

        // POST api/Alertes
        [HttpPost]
        //[Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]DiffuserimageDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Utilisateur utilisateur = await dataAccess.FindUtilisateurByLogin(dto.FkUtilisateur);
            if (utilisateur == null)
                return NotFound();
            
            Imagepromotion imagepromotion = await dataAccess.FindImagepromotionById(dto.FkImage);
            if (imagepromotion == null)
                return NotFound();

            var entity = Mapper.Map<Diffuserimage>(dto);

            await dataAccess.AddDiffuserimageAsync(entity);

            return Created($"api/DiffuserImages/{entity.Id}", Mapper.Map<DiffuserimageDTO>(entity));
        }

        // PUT api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DiffuserimageDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            Diffuserimage entity = await dataAccess.FindDiffuserimageById(id);
            if (entity == null)
                return NotFound();

            Utilisateur utilisateur = await dataAccess.FindUtilisateurByLogin(dto.FkUtilisateur);
            if (utilisateur == null)
                return NotFound();
            
            Imagepromotion imagepromotion = await dataAccess.FindImagepromotionById(dto.FkImage);
            if (imagepromotion == null)
                return NotFound();

            entity = Mapper.Map<Diffuserimage>(dto);
            await dataAccess.UpdateDiffuserimageAsync(entity);

            return Ok(Mapper.Map<DiffuserimageDTO>(entity));
        }

        // DELETE api/Alertes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Diffuserimage entity = await dataAccess.FindDiffuserimageById(id);
            if (entity == null)
                return NotFound();
                
            await dataAccess.RemoveDiffuserimageAsync(entity);

            return Ok();
        }
    }
}
