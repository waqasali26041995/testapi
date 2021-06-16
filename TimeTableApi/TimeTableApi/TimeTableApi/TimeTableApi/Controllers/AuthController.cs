using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.RoleManagement;
using TimeTableApi.Application.Shared.UserManagement;
using TimeTableApi.Application.Shared.UserManagement.Dtos;

namespace TimeTableApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;
        public AuthController(
            IUserAppService userAppService,
            IRoleAppService roleAppService,
            IConfiguration configuration) {
            _userAppService = userAppService;
            _roleAppService = roleAppService;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginModel input)
        {
            var user = _userAppService.GetByEmail(input);
            if (user != null)
            {
                var role = _roleAppService.GetById(user.RoleId);
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:Issuer"],
                    audience: Configuration["JWT:Audience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: new List<Claim> {
                        new Claim("UserId", user.Id),
                        new Claim("Email", user.Email),
                        new Claim("Role", role.Name),
                    },
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }
    }
}
