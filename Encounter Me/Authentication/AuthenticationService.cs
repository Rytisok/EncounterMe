using Blazored.LocalStorage;
using Encounter_Me.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Encounter_Me.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client,
                                     AuthenticationStateProvider authStateProvider,
                                     ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant-type", "password"),
                new KeyValuePair<string, string>("userName", userForAuthentication.UserName),
                new KeyValuePair<string, string>("password", userForAuthentication.Password)
            });

            var authResult = await _client.PostAsync("https://localhost:5001/token", data); // to be changed later! Api will be hosted somewhere, not at localhost

            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null; // return null if not login = AuthStateProvider will return anonymous 
            }

            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // take in camelCase json, conver to Pascal case. Ensures that result is correct

            await _localStorage.SetItemAsStringAsync(key: "authToken", result.Access_Token); // put token, which proves that user is authenticated inside local storage

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Access_Token);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Access_Token);

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(key: "authToken"); // if you don't clear access token after logout the user will remain logged in.
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
