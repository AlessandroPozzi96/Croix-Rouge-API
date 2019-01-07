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
    public class RolesController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public RolesController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/Roles
        // Recuperer les roles pour ne pas les hards coder en angular
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Role> entities = await dataAccess.GetRolesAsync();
            var results = Mapper.Map<IEnumerable<RoleDTO>>(entities);
            //return Ok(entities.Select(CreateDTOFromEntity));
            return Ok(results);
        }
       /*        private static DTO.RoleDTO CreateDTOFromEntity(Model.Role entity)
        {
            //fixme: comment améliorer cette implémentation?
            return new DTO.RoleDTO()
            {
                Libelle = entity.Libelle
            };
        }*/
    }
}
