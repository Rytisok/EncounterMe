using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Shared
{
    public class UserData
    {
        public int Id { get; set; } 

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPhotoUrl { get; set; }

        public int Level { get; set; }
        public double ExperiencePoints { get; set; }

        public UserData(int id, string userName, string firstName, string lastName, string email, string userPhotoUrl = null)
        {
            this.Id = id;
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserPhotoUrl = userPhotoUrl;
        }

    }
}