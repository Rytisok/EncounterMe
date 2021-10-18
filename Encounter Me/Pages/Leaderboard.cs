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


        public List<UserData> Users { get; set; }


        private void InitializeUsers()
        {
            var u1 = new UserData
            {
                FirstName = "Rytis",
                LastName = "Mikalauskas",
                Email = "rytis@gmail.com",
                Id = 0,
                Level = 43,
                ExperiencePoints = 10001
            };

            var u2 = new UserData
            {
                FirstName = "Gabija",
                LastName = "Gakaite",
                Email = "gabija@gmail.com",
                Id = 1,
                Level = 43,
                ExperiencePoints = 10002
            };

            var u3 = new UserData
            {
                FirstName = "Catarina",
                LastName = "Boto",
                Email = "catarina@gmail.com",
                Id = 2,
                Level = 41,
                ExperiencePoints = 8888
            };

            Users = new List<UserData> { u1, u2, u3 };
            Users.Sort();

        }

    }
}
