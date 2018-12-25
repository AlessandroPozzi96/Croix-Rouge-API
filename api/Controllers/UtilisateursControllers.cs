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
using CroixRouge.Dal;
using AutoMapper;
using CroixRouge.api.Infrastructure;

namespace CroixRouge.api.Controllers
{
    [Route("api/[controller]")]
    public class UtilisateursController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;
        private string ANONYMOUS = "anonymous";

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
            string loginToken = GetLoginToken();
            string role = GetRoleToken();

            if (role == CroixRouge.Model.Constants.Roles.User && login != loginToken)
            {
                return BadRequest(CroixRouge.Model.Constants.MsgErrors.ROLE_INCORRECT);
            }

            CroixRouge.Model.Utilisateur entity = await dataAccess.FindUtilisateurByLogin(login);
            if (entity == null)
                return NotFound();

            var result = Mapper.Map<UtilisateurModel>(entity);

            return Ok(result);
        }

        // POST api/Utilisateurs
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]CroixRouge.DTO.UtilisateurModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            string roleToken = GetRoleToken();

            if ((roleToken == ANONYMOUS || roleToken == CroixRouge.Model.Constants.Roles.User) && dto.FkRole != CroixRouge.Model.Constants.Roles.User)
            {
                return BadRequest(CroixRouge.Model.Constants.MsgErrors.ROLE_INCORRECT);
            }

            dto.Score = 0;
            dto.Password = Hashing.HashPassword(dto.Password);

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
            var roleToken = GetRoleToken();

            if (roleToken == CroixRouge.Model.Constants.Roles.User && (login != loginToken || loginToken != dto.Login || dto.FkRole != CroixRouge.Model.Constants.Roles.User))
            {
                return BadRequest(CroixRouge.Model.Constants.MsgErrors.ROLE_INCORRECT);
            }

            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            CroixRouge.Model.Utilisateur entity = await dataAccess.FindUtilisateurByLogin(login);
            if (entity == null)
                return NotFound();

            if(dto.FkAdresseNavigation != null){
                var entityAdresse = Mapper.Map<CroixRouge.Model.Adresse>(dto.FkAdresseNavigation);
                if(entity.FkAdresse==null){
                    entity.FkAdresse = await dataAccess.AddAdresseAsync(entityAdresse);
                    entity.FkAdresseNavigation = new Adresse();
                }
            }

            entity.Password = Hashing.HashPassword(dto.Password);
            
            await dataAccess.UpdateUtilisateurAsync(entity, dto);

            return Ok(Mapper.Map<CroixRouge.DTO.UtilisateurModel>(entity));
        }

        // DELETE api/Utilisateurs/Gwynbleidd
        [HttpDelete("{login}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //Vérifier si le login passé en paramètre est le même que celui du token
        //Vérifier les clés étrangères
        public async Task<IActionResult> Delete(string login)
        {
            var loginToken = GetLoginToken();
            string roleToken = GetRoleToken();

            if (roleToken == CroixRouge.Model.Constants.Roles.User && login != loginToken)
            {
                return BadRequest(CroixRouge.Model.Constants.MsgErrors.ROLE_INCORRECT);
            }

            CroixRouge.Model.Utilisateur utilisateur = await dataAccess.FindUtilisateurByLogin(login);
            if (utilisateur == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();

            await dataAccess.RemoveUtilisateurAsync(utilisateur);

            return Ok();
        }

        public string GetLoginToken()
        {
            return User.Claims.ElementAt(0).Value;
        }

        public string GetRoleToken()
        {
            if (User.Identity.IsAuthenticated)
                return User.Claims.ElementAt(3).Value;
            else
                return ANONYMOUS;
        }
    }
}
