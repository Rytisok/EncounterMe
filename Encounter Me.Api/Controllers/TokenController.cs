using Encounter_Me.Api.Authentication;
using Encounter_Me.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;


        public TokenController(AppDbContext context, IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAllTokens()
        {
            return Ok("Here you go."); // For testing only
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Create([FromForm]UserCreds loginUser)
        {
            /// Check if user exists in DB here! 
            UserData user = _userRepository.GetUserByEmail(loginUser.Email);

            if(user is not null)
            {
                if (IsValidPassword(user, loginUser.Password))
                {
                    return new ObjectResult(await GenerateToken(loginUser.Email));
                }
                Log.Error("Authentication failure, wrong password was provided.");
                return BadRequest("Wrong password, try again.");
            }
            return BadRequest("User with this email does not exist.");
        }

        private Boolean IsValidPassword(UserData user, string password)
        {
           var passwordIsValid = PasswordManager.IsPasswordCorrect(password, user.StoredSalt, user.Password);

            if(passwordIsValid)
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
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
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
