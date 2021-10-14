using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Encounter_Me.Pages
{
    public partial class Leaderboard
    {
        protected override Task OnInitializedAsync()
        {
            InitializeUsers();

            return base.OnInitializedAsync();
        }


        public IEnumerable<UserData> Users { get; set; }


        private void InitializeUsers()
        {
            var u1 = new UserData
            {
                FirstName = "Rytis",
                LastName = "Mikalauskas",
                Email = "rytis@gmail.com",
                Id = 0,
                Level = 42,
                ExperiencePoints = 9999
            };

            var u2 = new UserData
            {
                FirstName = "Gabija",
                LastName = "Gakaite",
                Email = "gabija@gmail.com",
                Id = 1,
                Level = 43,
                ExperiencePoints = 10000
            };

            var u3 = new UserData
            {
                FirstName = "Catarina",
                LastName = "Boto",
                Email = "catarina@gmail.com",
                Id = 2,
                Level = 44,
                ExperiencePoints = 10002
            };

            Users = new List<UserData> { u1, u2, u3 };

        }

    }
}
