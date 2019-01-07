using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CroixRouge.api.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CroixRouge.DTO;
using CroixRouge.Model;
using CroixRouge.Dal;

namespace CroixRouge.api.Controllers
{
    // Ce controller est accessible même aux utilisateurs non identifiés
    // En effet, ces derniers doivent pouvoir demander un token!
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {

        private readonly JwtIssuerOptions _jwtOptions;
        private Dal.bdCroixRougeContext _context;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, Dal.bdCroixRougeContext context)
        {
            _jwtOptions = jwtOptions.Value;
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // POST api/Jwt
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDTO LoginDTO)
        {
            // Depuis ASP.NET Core 2.1, ces deux lignes sont superflues.
            // En effet, le framework prend en charge le retour d'une erreur 400 
            // incluant le détail de cette dernière si la validation ne s'est pas bien déroulée. 
            // Il est possible de désactiver ce mode de fonctionnement. Pour plus d'informations, voir https://docs.microsoft.com/en-us/aspnet/core/web-api/index?view=aspnetcore-2.1#automatic-http-400-responses 
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            var repository = new AuthenticationRepository(this._context);

            var users = await repository.GetUsers();

            users.FirstOrDefault(u => {
                Console.Write("LOGIN : " + u.Login);
                Console.Write("PASSWORD : " + u.Password);
                return true;
            });

            Model.Utilisateur utilisateurFound = users.FirstOrDefault(utilisateur => utilisateur.Login == LoginDTO.UserName && Hashing.ValidatePassword(LoginDTO.Password, utilisateur.Password));
            if (utilisateurFound == null)
                return Unauthorized();


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateurFound.Login),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                        ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                        ClaimValueTypes.Integer64),
                // new Claim(PrivateClaims.UserId,userFound.Id.ToString())  l'id est le login

            };

            // rappel: le token est configurable. On y ajoute ce que l'on veut comme claims!
            // un ensemble de nom de claims est "réservé" (voir JwtRegisteredClaimNames)
            // le reste est utilisable à loisir! Voir classe PrivateClaims par exemple. 
            if (utilisateurFound.FkRole != null)
            {
                claims.Add(new Claim("role",utilisateurFound.FkRole));
                /* userFound.Roles.ToList().ForEach(roleName =>
                claims.Add(new Claim("roles", roleName)));*/
            }

            //IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Sérialisation et retour
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };
            return Ok(response);
        }
        private static long ToUnixEpochDate(DateTime date)
              => (long)Math.Round((date.ToUniversalTime() -
                                   new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                                  .TotalSeconds);
    }

}
