using Encounter_Me.DataClasses;
using Encounter_Me.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Pages
{
    public partial class Profile
    {
        
        [Parameter]
        public string Id { get; set; }
        public UserData User { get; set; } = new UserData();
        private DummyUser dummyUser = new DummyUser(1, "username");

        [Inject]
        public IUserDataService UserDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {

            User = await UserDataService.GetUserDetails(int.Parse(Id));
            dummyUser.ID = User.Id;
            dummyUser.Name = User.UserName;
        }
    }
}
