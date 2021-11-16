using Encounter_Me.Models;
using System.Threading.Tasks;

namespace Encounter_Me.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(LoginUserModel userForAuthentication);
        Task Logout();
    }
}