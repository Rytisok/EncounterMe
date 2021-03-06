using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me;

namespace Encounter_Me.Api.Models
{
    public interface IUserRepository
    {
        IEnumerable<UserData> GetAllUsers();
        UserData GetUserById(Guid userId);
        UserData AddUser(UserData user);
        UserData UpdateUser(UserData user);
        void DeleteUser(Guid userId);
        UserData GetUserByEmail(string email);
        bool IsUsernameTaken(string username);
        bool IsEmailTaken(string email);
    }
}
