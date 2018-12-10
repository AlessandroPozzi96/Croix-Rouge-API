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
    public class HorairesController : Controller
    {
        private bdCroixRougeContext _context;

        public HorairesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET api/Horaires/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            IEnumerable<CroixRouge.Model.Collecte> collectes = await _context.Collecte
            .Where(c => c.Id == id)
            .Include(c => c.Jourouverture)
            .ToArrayAsync();

            CroixRouge.Model.Collecte collecte = collectes.First();

            if (collecte == null)
                return NotFound();
            
            var horaires = CreateDTOsFromEntities(id, collecte.Jourouverture);

            return Ok(horaires);
        }

        public Task<CroixRouge.Model.TrancheHoraire> FindTrancheHoraireById(int id)
        {
            return _context.TrancheHoraire.FindAsync(id);
        }

        public async Task<IEnumerable<HoraireModel>> CreateDTOsFromEntities(int idCollecte, IEnumerable<Jourouverture> jourouvertures)
        {
            List<HoraireModel> horaires = new List<HoraireModel>();
            CroixRouge.Model.TrancheHoraire trancheHoraire;
            HoraireModel horaire;

            foreach(Jourouverture jourouverture in jourouvertures)
            {
                trancheHoraire  = await FindTrancheHoraireById(jourouverture.FkTrancheHoraire);
                horaire = new HoraireModel();
                horaire.IdCollecte = idCollecte;
                horaire.LibelleJour = jourouverture.LibelleJour;
                horaire.Date = jourouverture.Date;
                horaire.HeureDebut = trancheHoraire.HeureDebut.ToString();
                horaire.HeureFin = trancheHoraire.HeureFin.ToString();

                horaires.Add(horaire);
            }
            return horaires;
        }
    }
}
