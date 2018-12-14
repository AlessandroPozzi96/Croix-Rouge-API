using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CroixRouge.DTO;
using CroixRouge.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using CroixRouge.Dal;
using AutoMapper;

namespace CroixRouge.api.Controllers
{
    [Route("api/[controller]")]
    public class UtilisateursController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public UtilisateursController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }

        // GET api/Utilisateurs
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Get(int? pageIndex= Constants.Paging.PAGE_INDEX, int? pageSize = Constants.Paging.PAGE_SIZE, string login = null)
        {
            IEnumerable<CroixRouge.Model.Utilisateur> entities = await dataAccess.GetUtilisateursAsync(pageIndex, pageSize, login);
            
            var results = Mapper.Map<IEnumerable<UtilisateurModel>>(entities);

            return Ok(results);
        }

        // GET api/Utilisateurs/Gwynbleidd
        //Vérifier si le login passé en paramètre est le même que celui du token
        [HttpGet("{login}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(string login)
        {
            CroixRouge.Model.Utilisateur entity = await dataAccess.FindUtilisateurByLogin(login);
            if (entity == null)
                return NotFound();
            
            var loginToken = GetLoginToken();

            if (login != loginToken)
                return NotFound("Login passé en paramètre différends de celui du token ");

            var result = Mapper.Map<UtilisateurModel>(entity);

            return Ok(result);
        }

        // POST api/Utilisateurs
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.UtilisateurModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(dto.FkRole == null)
            {
                dto.FkRole = "USER";
            }
            dto.Score = 0;

            var entity = Mapper.Map<CroixRouge.Model.Utilisateur>(dto);

            await dataAccess.AddUtilisateurAsync(entity);

            return Created($"api/Utilisateurs/{entity.Login}", Mapper.Map<UtilisateurModel>(entity));
        }

        // PUT api/Utilisateurs/Gwynbleidd
        //Vérifier si le login passé en paramètre est le même que celui du token
        [HttpPut("{login}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put(string login, [FromBody]CroixRouge.DTO.UtilisateurModel dto)
        {
            var loginToken = GetLoginToken();

            if(login != dto.Login)
             {
                 return BadRequest();
             }
             if (login != loginToken) // --> impossible de changer le login de qqn pour un admin
             {
                return NotFound("Login passé en paramètre différends de celui du token ");
             }

            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Utilisateur entity = await dataAccess.FindUtilisateurByLogin(login);
            if (entity == null)
                return NotFound();

            await dataAccess.UpdateUtilisateurAsync(entity, dto);

            return Ok(Mapper.Map<CroixRouge.DTO.UtilisateurModel>(entity));
        }

        // DELETE api/Utilisateurs/Gwynbleidd
        [HttpDelete("{login}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //Vérifier si le login passé en paramètre est le même que celui du token
        public async Task<IActionResult> Delete(string login)
        {
            CroixRouge.Model.Utilisateur utilisateur = await dataAccess.FindUtilisateurByLogin(login);
            if (utilisateur == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();

            var loginToken = GetLoginToken();

            if (login != loginToken)
                return NotFound("Login passé en paramètre différends de celui du token ");

            await dataAccess.RemoveUtilisateurAsync(utilisateur);

            return Ok();
        }

        public string GetLoginToken()
        {
            return User.Claims.ElementAt(0).Value;
        }
    }
}
