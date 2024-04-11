using KingMeetup.Blazor.IService;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;
using Blazored.Toast.Services;

namespace KingMeetup.Blazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly IToastService _toastService;
        private IConfiguration _config;
        public AuthService(HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider, NavigationManager navigationManager, IToastService toastService, IConfiguration config)
        {
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _navigationManager = navigationManager;
            _toastService = toastService;
            _config = config;
        }

        public async Task Login(LoginRequest userDto)
        {
            if (userDto.Email == null || userDto.Password == null)
                Console.WriteLine("nesto nevalja"); //Ignorirajte
            // LoginPageBase.message = "Username or password cannot be empty!";
            else
            {
                var httpPostRequest = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:Login"]);
                httpPostRequest.Content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpPostRequest);
                
                var jwtToken = httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    await _customAuthenticationStateProvider.SetTokenAsync(jwtToken.Result);
                }
                else
                {
                    throw new ArgumentException("Pogrešno korisničko ime i/ili lozinka");
                }
            }
        }

        public async Task AddUserAsync(UserRequest user)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:Issuer"]);
            request.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ArgumentException("Već postoji registrirani korisnik s navedenom email adresom");
            }
        }
    }
}