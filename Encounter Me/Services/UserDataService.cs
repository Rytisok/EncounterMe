using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Encounter_Me.Authentication;

namespace Encounter_Me.Services
{
    [Authorize]
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;

        private readonly IAuthenticationService _authenticationService;

        public UserDataService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _authenticationService = authenticationService;
            _httpClient.BaseAddress = new Uri("https://localhost:44340/");


        }

        public async Task<UserData> AddUser(UserData user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/user", userJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<UserData>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        [Authorize]
        public async Task UpdateUser(UserData user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync("api/user", userJson);
            if (response.IsSuccessStatusCode is false)
            { 
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteUser(Guid userId)
        {
            await _httpClient.DeleteAsync($"api/user/{userId}");
        }

        public async Task<IEnumerable<UserData>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UserData>>
                    (await _httpClient.GetStreamAsync($"api/user"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserData> GetUserDetails(Guid userId)
        {
            return await JsonSerializer.DeserializeAsync<UserData>
                (await _httpClient.GetStreamAsync($"api/user/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async void UpdateUserXp(UserData user, int xpGain)
        {
            if (user.ExperiencePoints + xpGain >= LevelAndXp.XpToLevelUp(user.Level))
            {
                user.ExperiencePoints = user.ExperiencePoints + xpGain - LevelAndXp.XpToLevelUp(user.Level);
                user.Level++;
            }
            else
            {
                user.ExperiencePoints += xpGain;
            }

            await UpdateUser(user);
        }

    }
}
