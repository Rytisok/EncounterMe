using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using BrowserInterop.Geolocation;
using Encounter_Me.Shared;

namespace Encounter_Me.Services
{
    public class CapturePointService : ICapturePointService
    {
        private readonly HttpClient _httpClient;

        public CapturePointService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CapturePoint> GetCapturePoint(Guid Id)
        {
            return await JsonSerializer.DeserializeAsync<CapturePoint>
                  (await _httpClient.GetStreamAsync($"api/capturepoint/{Id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateCapturePoint(CapturePoint capturePoint)
        {
            var capturePointJson =
                new StringContent(JsonSerializer.Serialize(capturePoint), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("api/capturepoint", capturePointJson);
        }

        public async Task<IEnumerable<CapturePoint>> GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CapturePoint>>
                     (await _httpClient.GetStreamAsync($"api/capturepoint/{Lat1.ToString().Replace(',', '.')}/{Lon1.ToString().Replace(',', '.')}/{Lat2.ToString().Replace(',', '.')}/{Lon2.ToString().Replace(',', '.')}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<double> GetCapturePointPercentage(Factions faction)
        {
            return await JsonSerializer.DeserializeAsync<double>
                  (await _httpClient.GetStreamAsync($"api/capturepoint/CapturePointPercentage/{faction.ToString()}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
