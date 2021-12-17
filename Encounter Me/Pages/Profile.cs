using Encounter_Me.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Encounter_Me.Shared;


namespace Encounter_Me.Pages
{
    public partial class Profile
    {
        [Parameter]
        public string Id { get; set; }
        public UserData User { get; set; } = new UserData();

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }
        private bool isAuthenticated = false;
        private Guid userId;


        private string imageBorderGradient = "";
        private string primaryColor = "";
        private string secondaryColor = "";

        protected async override Task OnInitializedAsync()
        {
            User = await UserDataService.GetUserDetails(Guid.Parse(Id));
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.IsAuthenticated)
            {
                isAuthenticated = true;
                userId = Guid.Parse(authState.User.FindFirst(ClaimTypes.UserData).Value);
            }

            switch (User.Faction)
            {
                case Factions.Blue:
                    imageBorderGradient = "border-image: linear-gradient(to right, #004cff, #00ffe7);";
                    primaryColor = "#0051fe";
                    secondaryColor = "#00caee";
                    break;
                case Factions.Red:
                    imageBorderGradient = "border-image: linear-gradient(to right, #640f0f, #f02d2d);";
                    primaryColor = "#b50000";
                    secondaryColor = "#ff2929";
                    break;
                case Factions.Green:
                    imageBorderGradient = "border-image: linear-gradient(to right, #01952d, #73f445);";
                    primaryColor = "#0d9f30";
                    secondaryColor = "#59ca32";
                    break;
                case Factions.Yellow:
                    imageBorderGradient = "border-image: linear-gradient(to right, #ffb400, #f7ff00);";
                    primaryColor = "#f7ff00";
                    secondaryColor = "#ffb400";
                    break;
            }
        }
    }
}
