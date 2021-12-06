using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encounter_Me.Services;
using Microsoft.AspNetCore.Components;

namespace Encounter_Me.Pages
{

    public partial class Leaderboard
    {
        public IEnumerable<UserData> Users { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserDataService.GetAllUsers()).ToList();
        }
    }
}
