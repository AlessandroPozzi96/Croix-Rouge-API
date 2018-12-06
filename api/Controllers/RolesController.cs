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
    public class RolesController : Controller
    {
        private bdCroixRougeContext _context;

        public RolesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET api/Roles
        // Recuperer les roles pour ne pas les hards coder en angular
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<CroixRouge.Model.Role> entities = await _context.Role
            .ToArrayAsync();
            var results = Mapper.Map<IEnumerable<RoleModel>>(entities);
            //return Ok(entities.Select(CreateDTOFromEntity));
            return Ok(results);
        }
       /*        private static DTO.RoleModel CreateDTOFromEntity(Model.Role entity)
        {
            //fixme: comment améliorer cette implémentation?
            return new DTO.RoleModel()
            {
                Libelle = entity.Libelle
            };
        }*/
    }
}
