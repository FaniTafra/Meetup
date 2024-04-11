using KingMeetup.Blazor.IService;
using KingMeetup.Messaging;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace KingMeetup.Blazor.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        public LocationService(HttpClient httpClient, IConfiguration config, CustomAuthenticationStateProvider customAuthenticationStateProvider)
        {
            _httpClient = httpClient;
            _config = config;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
        }

        public async Task<List<CityRequest>> GetCities(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            return  await _httpClient.GetFromJsonAsync<List<CityRequest>>($"{_config["Endpoints:Location:GetCities"]}:{id}");
        }

        public async Task<List<StateResponse>> GetStates()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            return await _httpClient.GetFromJsonAsync<List<StateResponse>>(_config["Endpoints:Location:GetStates"]);
        }
    }
}
