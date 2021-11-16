using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<UserData>> GetAllUsers();

        Task<UserData> GetUserDetails(int Id);

        Task<UserData> AddUser(UserData user);

        Task UpdateUser(UserData user);

        Task DeleteUser(int Id);
    }
}
