using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me;

namespace Encounter_Me.Api.Models
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<UserData> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public UserData GetUserById(int Id)
        {
            return _appDbContext.Users.FirstOrDefault(c => c.Id == Id);
        }

        public UserData GetUserByEmail(string Email)
        {
            return _appDbContext.Users.FirstOrDefault(c => c.Email == Email); ;
        }


        public UserData AddUser(UserData user)
        {
            var addedEntity = _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public UserData UpdateUser(UserData user)
        {
            var foundUser = _appDbContext.Users.FirstOrDefault(e => e.Id == user.Id);

            if (foundUser != null)
            {
                foundUser.FirstName = user.FirstName;
                foundUser.LastName = user.LastName;
                foundUser.Email = user.Email;
                foundUser.Password = user.Password;
                foundUser.StoredSalt = user.StoredSalt;
                foundUser.UserPhotoUrl = user.UserPhotoUrl;
                foundUser.Level = user.Level;
                foundUser.ExperiencePoints = user.ExperiencePoints;


                _appDbContext.SaveChanges();

                return foundUser;
            }

            return null;
        }

        public void DeleteUser(int Id)
        {
            var foundUser = _appDbContext.Users.FirstOrDefault(e => e.Id == Id);
            if (foundUser == null) return;

            _appDbContext.Users.Remove(foundUser);
            _appDbContext.SaveChanges();
        
    }}
}
