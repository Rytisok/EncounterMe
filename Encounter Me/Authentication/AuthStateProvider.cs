using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Encounter_Me.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider // inherit from original AuthenticationStateProvider - overvriting some methods
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() //Are you authenticated?
        {
            var token = await _localStorage.GetItemAsync<string>(key: "authToken"); // TODO: optimize later when stuff works.

            if(string.IsNullOrWhiteSpace(token))
            {
                return _anonymous; // we are not logged in if authToken is not in local storage. 
            }

            // found token:
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token); // add token as "bearer" in the header
            
            return new AuthenticationState(
                user: new ClaimsPrincipal(
                    identity: new ClaimsIdentity(JwtParser.ParseClaimsFromJWT(token), 
                     authenticationType: "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string token) // this is an event - we changed the login status of the user
        {
            var authenticatedUser = new ClaimsPrincipal(
                    identity: new ClaimsIdentity(JwtParser.ParseClaimsFromJWT(token),
                     authenticationType: "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }


    }
}
