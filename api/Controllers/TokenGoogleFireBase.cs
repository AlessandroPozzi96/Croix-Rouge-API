using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CroixRouge.Model;
using CroixRouge.Dal;

namespace CroixRouge.api.Controllers
{
    [Route("api/[controller]")]
    public class TokenGoogleFireBaseController : Controller
    {
        string token;
        public TokenGoogleFireBaseController()
        {
            token = new ConfigurationHelper("googleFireBaseToken").GetConnectionString();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.Roles.Admin)]
        public ActionResult Get()
        {
            var response = new
            {
                access_token = token
            };

            return Ok(response);
        }
    }
}