using System;
using System.ComponentModel.DataAnnotations;

namespace Encounter_Me.Shared
{
    public class RegistrationUserModel
    {
        [Required, StringLength(30, ErrorMessage ="Username must be shorter than 30 characters")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$",
            ErrorMessage = "Name must start with a capital letter, and have only letters")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$",
            ErrorMessage = "Surname must start with a capital letter and have only letters")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress] 
        // TODO : improve this validation as it is not robust enough.
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
            ErrorMessage = "Password should have at least 8 characters, one uppercase letter, one lowercase letter and a number.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password and Password fields values do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
