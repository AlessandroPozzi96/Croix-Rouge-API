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
[Route("api/[controller]")]
    public class DonsController : Controller
    {
        private bdCroixRougeContext _context;
        private DataAccess dataAccess;

        public DonsController(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dataAccess = new DataAccess(this._context);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]DonDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<Don>(dto);

            await dataAccess.addDonAsync(entity);
            
            return Created($"api/Dons/{entity.Id}", Mapper.Map<DonDTO>(entity));
        }


    }
}