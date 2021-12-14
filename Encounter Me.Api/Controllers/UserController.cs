using Encounter_Me.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _userRepository.GetUserById(id);
            if (user is not null)
                return Ok(_userRepository.GetUserById(id));
            else
                return NotFound("User with given Id doesn't exist.");
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserData user)
        {
            if (user == null)
                return BadRequest(); 

            if (_userRepository.IsUsernameTaken(user.UserName))
            {
                return BadRequest($"Username {user.UserName} is already taken.");
            }

            if (_userRepository.IsEmailTaken(user.Email))
            {
                return BadRequest($"Email adress {user.Email} is already taken.");
            }

            if (user.FirstName == string.Empty || user.LastName == string.Empty || user.Faction is null || user.Email is null || user.Id.Equals(Guid.Empty))
            {
                return BadRequest("Wrong data provided.");
            }

            var hashSalt = PasswordManager.EncryptPassword(user.Password, null);
            user.Password = hashSalt.Hash;
            user.StoredSalt = hashSalt.Salt;

            var createdUser = _userRepository.AddUser(user);

            Log.Information("Created new user: {@model}", createdUser);
            return Created("user", createdUser);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserData user)
        {
            if (user == null)
                return BadRequest();

            var userToUpdate = _userRepository.GetUserById(user.Id);

                if (userToUpdate == null)
                    return NotFound();

            var IsUsernameNew = !_userRepository.GetUserById(user.Id).UserName.Equals(user.UserName);
            if (IsUsernameNew is true && _userRepository.IsUsernameTaken(user.UserName))
            {
                return BadRequest($"Username {user.UserName} is already taken.");
            }

            var IsEmailNew = !_userRepository.GetUserById(user.Id).Email.Equals(user.Email);
            if (IsEmailNew is true && _userRepository.IsEmailTaken(user.Email))
            {
                return BadRequest($"Email adress {user.Email} is already taken.");
            }

            _userRepository.UpdateUser(user);

                return NoContent(); //success
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return BadRequest("Error - got empty Guid.");

            if (_userRepository.GetUserById(id) is null)
                return NotFound();

            _userRepository.DeleteUser(id);

            return NoContent();//success
        }
    }
}
