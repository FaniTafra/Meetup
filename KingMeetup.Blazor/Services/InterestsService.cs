using KingMeetup.Blazor.IService;
using KingMeetup.Messaging;
using KingMeetup.Model;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace KingMeetup.Blazor.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _customAuthProvider;
        private IConfiguration _config;
        public InterestsService(HttpClient httpClient, IConfiguration config, CustomAuthenticationStateProvider customAuthProvider)
        {
            _httpClient = httpClient;
            _config = config;
            _customAuthProvider = customAuthProvider;
        }

        public async Task<List<InterestResponse>> GetInterests()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthProvider.GetTokenAsync());
            return await _httpClient.GetFromJsonAsync<List<InterestResponse>>(_config["Endpoints:Interests"]);
        }

        public async Task<int> GetUserId()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthProvider.GetTokenAsync());
            return await _customAuthProvider.GetUserId();
        }

        public async Task AddUsersInterests(List<UsersInterestRequest> usersInterestRequest)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthProvider.GetTokenAsync());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:AddUserInterests"]);
            request.Content = new StringContent(JsonSerializer.Serialize(usersInterestRequest), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task<bool> CheckUsersInterest()
        {
            try
            {
                int userId = await GetUserId();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthProvider.GetTokenAsync());
                return await _httpClient.GetFromJsonAsync<bool>($"{_config["Endpoints:CheckInterests"]}/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<InterestResponse>> GetUserInterests()
        {
            int userId = await GetUserId();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthProvider.GetTokenAsync());
            return await _httpClient.GetFromJsonAsync<List<InterestResponse>>($"{_config["Endpoints:GetUserInterests"]}/{userId}");
        }
    }
}
