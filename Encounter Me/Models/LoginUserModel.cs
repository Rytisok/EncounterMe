using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Models
{
    public class LoginUserModel // use as credentials during authentication process
    {
        [Required(ErrorMessage ="Please provide Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please provide password.")]
        public string Password { get; set; }


        public LoginUserModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public LoginUserModel()
        {

        }
    }
}
