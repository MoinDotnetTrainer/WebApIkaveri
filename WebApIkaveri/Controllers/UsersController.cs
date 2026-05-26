using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApIkaveri.Models;
using Microsoft.AspNetCore.Authorization;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApIkaveri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDb _context;
        public readonly IConfiguration _configuration;

        public UsersController(AppDb context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginModel obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == obj.Email && x.Password == obj.Password);

            if (user != null)
            {
                var claims = new[]
                    {
                          new Claim (JwtRegisteredClaimNames. Sub, _configuration["Jwt:Subject"]),
                          new Claim (JwtRegisteredClaimNames. Jti, Guid.NewGuid().ToString()),
                          new Claim ("UserId",user.UserId.ToString()),
                          new Claim ("Email", user.Email.ToString())
                  };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
                );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenValue, User = obj });
            }
            return NoContent();


        }
    }
}
