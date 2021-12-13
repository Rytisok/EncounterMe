using Encounter_Me.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Encounter_Me.Pages
{
    public partial class EditProfile
    {
        [Parameter]
        public string Id { get; set; }
        public UserData User { get; set; } = new UserData();

        [Inject]

        public IUserDataService UserDataService { get; set; }

        [Inject]
        public NavigationManager NavManager{ get; set; }

        protected async override Task OnInitializedAsync()
        {

            User = await UserDataService.GetUserDetails(Guid.Parse(Id));

        }

        protected async Task HandleValidSubmit()
        {
            await UserDataService.UpdateUser(User);
            NavManager.NavigateTo("/");
        }
    }
}
