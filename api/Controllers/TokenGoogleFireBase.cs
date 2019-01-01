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

namespace CroixRouge.api.Controllers
{
    [Route("api/[controller]")]
    public class TokenGoogleFireBaseController : Controller
    {
        string token = "AAAAMHQFea8:APA91bEm2_7e00CxPDRsSZ4jBzn0Zvg8Txjfa_5JdLabQjxvYVmeLSWs1XQqexjjyDm9d06YlQaTNqCkBtSRcOOLSfjGFut-Uwsx34A-A1u7hulFZW66kIFNCtnXKqYthuv3Da-Xn519";
        public TokenGoogleFireBaseController()
        {
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