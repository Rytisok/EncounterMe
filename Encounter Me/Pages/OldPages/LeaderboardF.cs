using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encounter_Me.Services;
using Encounter_Me.Shared;
using Microsoft.AspNetCore.Components;

namespace Encounter_Me.Pages
{
    /*
    public partial class LeaderboardF
    {
        public IEnumerable<UserData> Users { get; set; }
        public IEnumerable<IGrouping<int,UserData>> FctionsGroup { get; set; }

        Factions x = Factions.Red;

        [Inject]
        public IUserDataService UserDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserDataService.GetAllUsers()).ToList();
            await ShowFactionsMembers(x);
        }


        public async Task ShowFactionsMembers(Factions x)
        {
            FctionsGroup = (from usr in Users
                            orderby usr.Level descending, usr.ExperiencePoints descending
                            group usr by (int)usr.Faction into faction
                            where faction.Key == (int)x
                            select faction);

        }
    }
    */
}