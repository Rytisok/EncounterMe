using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Shared
{
    public class UserData
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPhotoUrl { get; set; }

        public int Id { get; set; }
        public int Level { get; set; }
        public double ExperiencePoints { get; set; }

    }
}