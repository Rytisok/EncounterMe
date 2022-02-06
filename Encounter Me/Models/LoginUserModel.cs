using System.ComponentModel.DataAnnotations;


namespace Encounter_Me.Models
{
    public class LoginUserModel
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
