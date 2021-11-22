using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Encounter_Me.Services
{
    public class UserDataService:IUserDataService
    {
        private readonly HttpClient _httpClient;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            return null;
        }

        public async Task UpdateUser(UserData user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/user", userJson);
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


    }
}
