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
    public class StatistiquesController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;
        private string ANONYMOUS = "Anonyme";

        public StatistiquesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/Statistiques/Gwynbleidd
        [HttpGet("{login}")]
        public async Task<IActionResult> Get(string login)
        {
            Utilisateur utilisateur = await dataAccess.FindUtilisateurByLogin(login);
            if (utilisateur == null)
                return NotFound();

            string loginToken = User.Claims.ElementAt(0).Value;
            string role = ANONYMOUS;

            if (User.Identity.IsAuthenticated)
                role = User.Claims.ElementAt(3).Value;

            if (role == CroixRouge.Model.Constants.Roles.User && login != loginToken)
            {
                return Forbid();
            }


            IEnumerable<Don> dons = await dataAccess.GetDonsAsync();
            IEnumerable<Collecte> collectes = await dataAccess.GetAllCollectesAsync();
            IEnumerable<Utilisateur> utilisateurs = await dataAccess.GetAllUtilisateursAsync();

            StatistiqueDTO statistique = new StatistiqueDTO();
            statistique.NbDons = utilisateur.Don.Count();
            statistique.NbDonsTot = dons.Count();
            statistique.NbCollecteTot = collectes.Count();
            statistique.NbUtilisateursInscrit = utilisateurs.Count();

            return Ok(statistique);
        }
    }
}
