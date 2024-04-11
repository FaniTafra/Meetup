using Azure;
using KingMeetup.API.Common;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using KingMeetup.Service;
using Microsoft.AspNetCore.Mvc;

namespace KingMeetup.API.Controllers
{
    public class LocationController : BaseApiController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
                _locationService = locationService;
        }

        [HttpGet("GetStates")]
        public async Task<IActionResult> GetStates(CancellationToken cancellationToken) 
        {
            try
            {
                List<StateResponse> response = await _locationService.GetStates(cancellationToken);
                return Ok(response);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }     
        }
        [HttpGet("GetCities:{id}")]
        public async Task<IActionResult> GetCities(int id , CancellationToken cancellationToken)
        {
            CityRequest request = new CityRequest() { stateId = id };
            CityResponse response = await _locationService.GetCities(request,cancellationToken);
            if (response.Success)
                return Ok(response.Cities);
            return BadRequest(response);     
        }
    }
}
