using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.ServicesInterfaces;

namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public TokenController(ITokenService tokenService, IUserService userService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a previously created user through email and password, and returns an access token
        /// </summary>
        /// <param name="email">Email User</param>
        /// <param name="password">Password User</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AuthenticateUser")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            if (await _tokenService.IsValidUser(email, password))
                return Ok(new { token = GenerateToken(await _userService.GetUserByEmail(email)) });
            return NotFound();
        }

        private string GenerateToken(UserDTO user)
        {
            var simmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var singingCredentials = new SigningCredentials(simmetricKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(singingCredentials);

            var claims = new[]
            {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                    new Claim("State", user.State.ToString())
                };

            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                null
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
