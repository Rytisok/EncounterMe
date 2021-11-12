using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Encounter_Me.Services
{
    public class TrailService : ITrailService
    {
        private readonly HttpClient _httpClient;

        public TrailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TrailContainer> AddTrail(TrailContainer trail)
        {
            var trailJson =
                new StringContent(JsonSerializer.Serialize(trail), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/trail", trailJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<TrailContainer>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteTrail(int Id)
        {
            await _httpClient.DeleteAsync($"api/trail/{Id}");
        }

        public async Task<IEnumerable<TrailContainer>> GetAllTrails()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<TrailContainer>>
                    (await _httpClient.GetStreamAsync($"api/trail"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<TrailContainer> GetTrailDetails(int Id)
        {
            return await JsonSerializer.DeserializeAsync<TrailContainer>
                 (await _httpClient.GetStreamAsync($"api/trail/{Id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
