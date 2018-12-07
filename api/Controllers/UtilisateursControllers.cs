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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
    [Route("api/[controller]")]
    public class UtilisateursController : Controller
    {
        private bdCroixRougeContext _context;

        public UtilisateursController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        // GET api/Utilisateurs
        [HttpGet]
        //[Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Get(int? pageIndex=0, int? pageSize = 3, string login = null)
        {
            IEnumerable<CroixRouge.Model.Utilisateur> entities = await _context.Utilisateur
            .Where(u => login == null || u.Login.Contains(login))
            .OrderBy(u => u.Login)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
            
            var results = Mapper.Map<IEnumerable<UtilisateurModel>>(entities);

            return Ok(results);
        }

        // GET api/Utilisateurs/Gwynbleidd
        [HttpGet("{login}")]
        public async Task<IActionResult> GetById(string login)
        {
            CroixRouge.Model.Utilisateur entity = await FindUtilisateurByLogin(login);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<UtilisateurModel>(entity);

            return Ok(result);
        }

        // POST api/Utilisateurs
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.UtilisateurModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<CroixRouge.Model.Utilisateur>(dto);
            _context.Utilisateur.Add(entity);
            await _context.SaveChangesAsync();
            return Created($"api/Utilisateurs/{entity.Login}", Mapper.Map<UtilisateurModel>(entity));
        }

        // PUT api/Utilisateurs/Gwynbleidd
        [HttpPut("{login}")]
        public async Task<IActionResult> Put(string login, [FromBody]CroixRouge.DTO.UtilisateurModel dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Utilisateur entity = await FindUtilisateurByLogin(login);
            if (entity == null)
                return NotFound();
            //fixme: améliorer cette implémentation
            entity.Nom = dto.Nom;
            entity.Mail = dto.Mail;
            entity.Prenom = dto.Prenom;
            entity.NumGsm = dto.NumGsm;
            entity.DateNaissance = dto.DateNaissance;
            entity.IsMale = dto.IsMale;
            entity.Score = dto.Score;
            entity.Password = dto.Password;
            
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(entity).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
            return Ok(Mapper.Map<CroixRouge.DTO.UtilisateurModel>(entity));
        }

        // DELETE api/Utilisateurs/Gwynbleidd
        [HttpDelete("{login}")]
        public async Task<IActionResult> Delete(string login)
        {
            CroixRouge.Model.Utilisateur utilisateur = await FindUtilisateurByLogin(login);
            if (utilisateur == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public Task<CroixRouge.Model.Utilisateur> FindUtilisateurByLogin(string login)
        {
            return _context.Utilisateur.FindAsync(login);
        }
    }
}
