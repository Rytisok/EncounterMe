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
        UserData GetUserById(int userId);
        UserData AddUser(UserData user);
        UserData UpdateUser(UserData user);
        void DeleteUser(int userId);
    }
}
