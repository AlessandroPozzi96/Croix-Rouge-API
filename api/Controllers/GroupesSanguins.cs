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
    public class GroupesSanguinsController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public GroupesSanguinsController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }
        // GET api/GroupesSanguins
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<CroixRouge.Model.Groupesanguin> entities = await dataAccess.GetGroupesanguinsAsync();
            
            var results = Mapper.Map<IEnumerable<GroupesanguinModel>>(entities);

            //return Ok(entities.Select(CreateDTOFromEntity));
            return Ok(results);
        }
    }
}
