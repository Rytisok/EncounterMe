
using System.ComponentModel.DataAnnotations;

namespace Encounter_Me.Models
{
    public class EditUserModel
    {
        [StringLength(30, ErrorMessage = "Username must be shorter than 30 characters")]
        public string UserName { get; set; }

        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$",
            ErrorMessage = "Name must start with a capital letter, and have only letters")]

        public string FirstName { get; set; }
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$",
            ErrorMessage = "Surname must start with a capital letter and have only letters")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Url]
        public string PhotoUrl { get; set; }

        //Password?

    }
}
