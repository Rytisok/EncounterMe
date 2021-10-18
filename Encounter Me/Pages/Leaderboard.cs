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

            var u1 = new UserData(0, "rm", "Rytis", "Mikalauskas", "rytis@gmail.com");
            u1.Level = 43;
            u1.ExperiencePoints = 10001;


            var u2 = new UserData(1, "gg", "Gabija", "Gakaite", "gg@gmail.com");
            u2.Level = 43;
            u2.ExperiencePoints = 10002;


            var u3 = new UserData(2, "cb", "Catarina", "Boto", "catarina@gmail.com");
            u3.Level = 41;
            u3.ExperiencePoints = 8888;


            var u4 = new UserData(3, "obuolys", "Dominykas", "Zagreckas", "dz@gmail.com",
                userPhotoUrl: "https://i.pinimg.com/originals/83/6d/69/836d69f49e80af2825c7db264be44af0.jpg");

            Users = new List<UserData> { u1, u2, u3, u4 };
            Users.Sort();

        }

    }
}
