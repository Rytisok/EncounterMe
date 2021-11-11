using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Models
{
    public class AuthenticationUserModel // use as credentials during authentication process
    {
        [Required(ErrorMessage ="Please provide email.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please provide password.")]
        public string Passsword { get; set; }
    }
}
