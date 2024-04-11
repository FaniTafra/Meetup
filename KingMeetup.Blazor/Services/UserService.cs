using Blazored.Toast.Services;
using KingMeetup.Blazor.IService;
using Microsoft.AspNetCore.Components;
using KingMeetup.Messaging;
using AutoMapper;
using System.Net.Http.Headers;

namespace KingMeetup.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly IToastService _toastService;
        private IConfiguration _config;
        private readonly IMapper _mapper;
        public UserService(HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider, NavigationManager navigationManager, IToastService toastService, IConfiguration config, IMapper mapper)
        {
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _navigationManager = navigationManager;
            _toastService = toastService;
            _config = config;
            _mapper = mapper;
        }

        public async Task<int> GetUserId()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            return await _customAuthenticationStateProvider.GetUserId();
        }
        public async Task<UserUpdateResponse> GetUser()
        {
            int userId = await GetUserId();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            UserUpdateResponse user = await _httpClient.GetFromJsonAsync<UserUpdateResponse>($"{_config["Endpoints:GetUser"]}?userID={userId}");
            return user;
        }

        public async Task UpdateUser(UserUpdateResponse updateResponse)
        {
            UserUpdateRequest? updateRequest = _mapper.Map<UserUpdateRequest>(updateResponse);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            var response = await _httpClient.PutAsJsonAsync($"{_config["Endpoints:UpdateUser"]}?userId={updateRequest.Id}", updateRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<UserResponse>();
                throw new ArgumentException("Greška kod ažuriranja podataka");
            }
        }

    }
}
