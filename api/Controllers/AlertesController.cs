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
    public class AlertesController : Controller
    {
        private bdCroixRougeContext _context;

        public AlertesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET api/alertes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<CroixRouge.Model.Alerte> entities = await _context.Alerte
            .OrderBy(alerte => alerte.Id)
            .ToArrayAsync();
            
            var results = Mapper.Map<IEnumerable<AlerteModel>>(entities);

            //return Ok(entities.Select(CreateDTOFromEntity));
            return Ok(results);
        }
      /*        private static DTO.AlerteModel CreateDTOFromEntity(Model.Alerte entity)

        {
            //fixme: comment améliorer cette implémentation?
            return new DTO.AlerteModel()
            {
                Id = entity.Id,
                Nom = entity.Nom, 
                Contenu = entity.Contenu
            };
        }*/
    }
}
