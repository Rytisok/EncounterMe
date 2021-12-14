using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encounter_Me.Services;
using Encounter_Me.Shared;
using Microsoft.AspNetCore.Components;

namespace Encounter_Me.Pages
{

    public partial class Leaderboard
    {
        public IEnumerable<UserData> Users { get; set; }
        public IEnumerable<UserData> UsersByFive { get; set; }
        public IEnumerable<IGrouping<int, UserData>> FctionsGroup;

        int pageNumber = 1;
        int nr = 1;

        [Inject]
        public IUserDataService UserDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserDataService.GetAllUsers()).ToList();
            await OrderUsersByExp();
            //await ShowFactionsMembers(Factions.Yellow);
            await doUsers5(pageNumber);
        }

        public async Task OrderUsersByExp()
        {
            
            Users = (from usr in Users
                     orderby  usr.Level descending, usr.ExperiencePoints descending
                     select usr).ToList();
        }


        public async Task GivePositionsToUsers()
        {
            /*IEnumerable<UserData> temp;
            int ntemp = (from usr in Users
                    select usr).Count();
            temp =(from usr in Users
                   select new var{ });*/
        }
        public async Task doUsers5(int pageNmbr)
        {

                UsersByFive = (from usr in Users
                               select usr).Take(pageNmbr * 5).ToList();

                UsersByFive = (from usr in UsersByFive
                           select usr).Skip((pageNmbr-1) * 5).ToList();           

        }


    }
}
