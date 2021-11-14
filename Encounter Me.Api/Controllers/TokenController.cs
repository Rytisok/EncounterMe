using Encounter_Me.Api.Authentication;
using Encounter_Me.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;


        public TokenController(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllTokens()
        {
            return Ok("Here you go."); // For testing only
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Create([FromForm]UserCreds user)
        {
            if (await IsValidUsernameAndPassword(user.Email, user.Password))
            {
                return new ObjectResult(await GenerateToken(user.Email));
            }
            else
            {
                return BadRequest(user.Email);
            }
        }

        // FIXME: add verification of password - for it password hashing functionality must be transferred from client to this api.
        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            UserData user = _userRepository.GetUserByEmail(username);
            if(user is not null)
            {
                return true;
            }
            return false;  
        }

        private async Task<dynamic> GenerateToken(string username)
        {
            UserData user = _userRepository.GetUserByEmail(username);

            // no roles for now

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.FirstName), //should be user id. 
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyHereIsVerySecret")), // TODO: add secret to appsetings
                            SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = username
            };

            return output;
        }

    }
}
