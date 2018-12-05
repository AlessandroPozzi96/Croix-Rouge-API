using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using api.infrastructure;
using dto;


namespace api.controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repository = new AuthenticationRepository();
            User userFound = repository.GetUsers().FirstOrDefault(user => user.UserName == loginModel.Username && user.PassWord == loginModel.Password);

            if (userFound == null)
            {
                return Unauthorized();
            }

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, userFound.UserName), 
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()), 
                new Claim(JwtRegisteredClaimNames.Iat, 
                ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),

            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer : _jwtOptions.Issuer, 
                audience : _jwtOptions.Audience, 
                claims : claims, 
                notBefore : _jwtOptions.NotBefore, 
                expires : _jwtOptions.Expiration, 
                signingCredentials : _jwtOptions.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            //Sérialisation
            var response = new 
            {
                access_token = encodedJwt, 
                expires_in = (int) _jwtOptions.ValidFor.TotalSeconds
            };

            return Ok(response);
        }

        private static long ToUnixEpochDate(DateTime date) 
        => (long)Math.Round(
            (date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
    }
}
