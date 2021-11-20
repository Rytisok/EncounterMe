using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me
{
    public class UserData : IComparable<UserData>
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] StoredSalt { get; set; }

        public string UserPhotoUrl { get; set; }

        private int _level;
        public int Level 
        {
            get => _level;
            set
            {
                if((value >= 0) && (value <=100))
                {
                    _level = value;
                }
                /// TODO: Throw an exception if value is not in range.
            }
        }

        private double _experiencePoints;
        public double ExperiencePoints 
        {
            get => _experiencePoints;
            set
            {
                if(value >= 0 && value <= 100000)
                {
                    _experiencePoints = value;
                }
                /// TODO: Throw an exception if value is not in range.
            }
        }

        public UserData(Guid id, string userName, string firstName, string lastName, string email, string password, byte[] salt = null, string userPhotoUrl = "https://www.kindpng.com/picc/m/24-248253_user-profile-default-image-png-clipart-png-download.png")
        {
            this.Id = id;
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.StoredSalt = salt;
            this.UserPhotoUrl = userPhotoUrl;
            this.Level = 0;
            this.ExperiencePoints = 0;
        }

        public UserData()
        {

        }

        public int CompareTo(UserData other)
        {
            if(this.Level==other.Level)
            {
                return other.ExperiencePoints.CompareTo(this.ExperiencePoints);
            }else
                return other.Level.CompareTo(this.Level);
        }
    }
}