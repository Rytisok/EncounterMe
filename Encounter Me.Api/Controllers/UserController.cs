using Encounter_Me.Api.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetUserById(int id)
        {
            return Ok(_userRepository.GetUserById(id));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserData user)
        {
            if (user == null)
                return BadRequest();

            if (user.FirstName == string.Empty || user.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hashSalt = PasswordManager.EncryptPassword(user.Password, null);
            user.Password = hashSalt.Hash;
            user.StoredSalt = hashSalt.Salt;

            var createdUser = _userRepository.AddUser(user);

            return Created("user", createdUser);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserData user)
        {
            if (user == null)
                return BadRequest();

            if (user.FirstName == string.Empty || user.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToUpdate = _userRepository.GetUserById(user.Id);

            if (userToUpdate == null)
                return NotFound();

            _userRepository.UpdateUser(user);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
                return BadRequest();

            var userToDelete = _userRepository.GetUserById(id);
            if (userToDelete == null)
                return NotFound();

            _userRepository.DeleteUser(id);

            return NoContent();//success
        }
    }
}
