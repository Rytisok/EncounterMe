using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Models
{
    public class AuthenticationUserModel // use as credentials during authentication process
    {
        [Required(ErrorMessage ="Please provide Username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please provide password.")]
        public string Password { get; set; }
    }
}
