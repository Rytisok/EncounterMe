using Encounter_Me.Authentication;
using Encounter_Me.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        private bool isAuthenticated = false;
        private Guid userId;
        protected async override Task OnInitializedAsync()
        {

            User = await UserDataService.GetUserDetails(Guid.Parse(Id));

            var authState = await AuthStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.IsAuthenticated)
            {
                isAuthenticated = true;

                userId = Guid.Parse(authState.User.FindFirst(ClaimTypes.UserData).Value);
            }
        }

        protected async Task HandleValidSubmit()
        {
            await UserDataService.UpdateUser(User);
            NavManager.NavigateTo("/");
        }
    }
}
