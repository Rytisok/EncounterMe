﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using BrowserInterop.Geolocation;

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

        public Task UpdateCapturePoint(CapturePoint capturePoint)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CapturePoint>> GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CapturePoint>>
                     (await _httpClient.GetStreamAsync($"api/capturepoint/{Lat1.ToString().Replace(',', '.')}/{Lon1.ToString().Replace(',', '.')}/{Lat2.ToString().Replace(',', '.')}/{Lon2.ToString().Replace(',', '.')}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
       
    }
}
