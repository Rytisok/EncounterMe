using Encounter_Me.Authentication;
using Encounter_Me.Models;
using Encounter_Me.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Encounter_Me.Pages
{
    public partial class EditProfile
    {
        [Parameter]
        public string Id { get; set; }
        public UserData User { get; set; } = new UserData();

        private EditUserModel editUserModel = new();

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set;}

        [Inject]
        public NavigationManager NavManager{ get; set; }

        private bool isAuthenticated = false;
        private Guid userId;
        private bool showSignUpError = false;
        private string signUpErrorText = "";

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

        protected async Task ExecuteUpdate()
        {

            User.UserName = editUserModel.UserName ?? User.UserName; 
            User.FirstName = editUserModel.FirstName ?? User.FirstName;
            User.LastName = editUserModel.LastName ?? User.LastName;
            User.Email = editUserModel.Email ?? User.Email;
            User.UserPhotoUrl = editUserModel.PhotoUrl ?? User.UserPhotoUrl;

            try 
            {
               await UserDataService.UpdateUser(User);
            }
            catch(Exception ex)
            {
                Log.Debug(ex.Message);
                showSignUpError = true;
                signUpErrorText = ex.Message;
            }
            
            if(showSignUpError is false)
            {
                await AuthenticationService.Logout();
                NavManager.NavigateTo("/Login");
            }
        }

        protected async Task HandleValidSubmit()
        {


            await UserDataService.UpdateUser(User);
            NavManager.NavigateTo("/");
        }

    }
}
