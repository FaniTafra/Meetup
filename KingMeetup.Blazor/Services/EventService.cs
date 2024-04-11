using KingMeetup.Messaging;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using KingMeetup.Blazor.IService;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using KingMeetup.Model;


namespace KingMeetup.Blazor.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        private IConfiguration _config;
        private readonly IInterestsService _interestsService;

        public EventService(HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider, IConfiguration config, IInterestsService interestsService)
        {
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _config = config;
            _interestsService = interestsService;
        }
        public async Task<int> GetUserId()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            return await _customAuthenticationStateProvider.GetUserId();
        }
        public async Task<EventResponse> Create(EventRequest eventRequest)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            AuthenticationState state = await _customAuthenticationStateProvider.GetAuthenticationStateAsync();
            eventRequest.UserId = int.Parse(state.User.Claims.FirstOrDefault().Value);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:Event:Create"]);
            request.Content = new StringContent(JsonSerializer.Serialize(eventRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<EventResponse>();
        }
        public async Task<EventResponse> Update(EventRequest eventRequest)
        {           
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            AuthenticationState state = await _customAuthenticationStateProvider.GetAuthenticationStateAsync();
            eventRequest.UserId = int.Parse(state.User.Claims.FirstOrDefault().Value);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:Event:Update"]);
            request.Content = new StringContent(JsonSerializer.Serialize(eventRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<EventResponse>();
        }
        public async Task<EventResponse> Get(int eventId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{ _config["EndPoints:Event:Get"]}:{eventId}");          
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<EventResponse>();
        }

        public async Task<AttendeeListsResponse> IsUserSignedUp(int eventId, int userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_config["EndPoints:Event:IsSignedUp"]}?eventId={eventId}&userId={userId}");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<AttendeeListsResponse>();
        }

        public async Task<AttendeeListsResponse> SignUp(int eventId, int userId,bool onSite)
        {
            AttendeeListsRequest attendeeListsRequest = new AttendeeListsRequest() {EventId = eventId,UserId = userId,IsOnSite = onSite };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:Event:SignUp"]);
            request.Content = new StringContent(JsonSerializer.Serialize(attendeeListsRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<AttendeeListsResponse>();
        }

        public async Task<AttendeeListsResponse> SignOff(int eventId, int userId)
        {
            AttendeeListsRequest attendeeListsRequest = new AttendeeListsRequest() { EventId = eventId, UserId = userId };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config["Endpoints:Event:SignOff"]);
            request.Content = new StringContent(JsonSerializer.Serialize(attendeeListsRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<AttendeeListsResponse>();
        }
        public async Task<List<EventResponse>> GetUserEvents()
        {
            try
            {
                int userId = await _customAuthenticationStateProvider.GetUserId();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
                return await _httpClient.GetFromJsonAsync<List<EventResponse>>($"{_config["Endpoints:Event:GetUserEvents"]}/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public async Task<List<EventResponse>> GetAdminEvents()
        {
            try
            {
                int userId = await _customAuthenticationStateProvider.GetUserId();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
                return await _httpClient.GetFromJsonAsync<List<EventResponse>>($"{_config["Endpoints:Event:GetAdminEvents"]}/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<EventResponse>> GetSignedUpEvents()
        {
            try
            {
                int userId = await _customAuthenticationStateProvider.GetUserId();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
                return await _httpClient.GetFromJsonAsync<List<EventResponse>>($"{_config["Endpoints:Event:GetSignedUpEvents"]}/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
