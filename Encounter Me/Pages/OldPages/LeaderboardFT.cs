using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encounter_Me.Services;
using Encounter_Me.Shared;
using Microsoft.AspNetCore.Components;

namespace Encounter_Me.Pages
{

    public partial class LeaderboardFT
    {
        public IEnumerable<UserData> Users { get; set; }
        public IEnumerable<IGrouping<int, UserData>> FctionsGroup { get; set; }
        int RedCount;
        int GreenCount;
        int BlueCount;
        int YellowCount;
        [Inject]
        public IUserDataService UserDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserDataService.GetAllUsers()).ToList();
            await ShowFactionsStats();
        }


        public async Task ShowFactionsStats()
        {
            /*FctionsGroup = (from usr in Users
                            group usr by (int)usr.Faction into faction
                            where faction.Key == 1
                            select faction);*/

            RedCount = (from usr in Users
                             select usr).Count(usr=>usr.Faction==Factions.Red);
            GreenCount = (from usr in Users
                             select usr).Count(usr => usr.Faction == Factions.Green);
            BlueCount = (from usr in Users
                             select usr).Count(usr => usr.Faction == Factions.Blue);
            YellowCount = (from usr in Users
                             select usr).Count(usr => usr.Faction == Factions.Yellow);

        }



    }
}
