
namespace Encounter_Me.Models
{
    public class AuthenticatedUserModel
    {

        public string Access_Token { get; set; }
        public string UserName { get; set; }
        public AuthenticatedUserModel(string access_Token, string userName)
        {
            Access_Token = access_Token;
            UserName = userName;
        }
        public AuthenticatedUserModel()
        {
        }
    }
}
