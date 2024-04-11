using AutoMapper;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using KingMeetup.Model;
using KingMeetup.Model.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationService> _logger;

        public LocationService(ILocationRepository repository, IMapper mapper, ILogger<LocationService> logger)
        {
            _locationRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StateResponse>> GetStates(CancellationToken cancellationToken) 
        {
            try
            {
                List<State> States = await _locationRepository.GetAllStates(cancellationToken);
                _logger.LogDebug("Getting states from database:{@States}", States);

                return _mapper.Map<List<StateResponse>>(States);
            }
            catch (Exception ex) 
            {
                _logger.LogDebug("Error while getting states from database: {@ex}", ex);
                throw;
            }
        }
        public async Task<CityResponse> GetCities(CityRequest request, CancellationToken cancellationToken)
        {
            CityResponse response = new CityResponse()
            {
                Request = request,
                Cities = new List<CityRequest>(),
            };   
            try
            {   
                List<City> Cities = await _locationRepository.GetCitiesInState(request.stateId, cancellationToken);
                if (Cities.Count > 0)
                {
                    _logger.LogDebug("Getting cities: {@Cities}", Cities);
                    List<CityRequest> temp = _mapper.Map<List<CityRequest>>(Cities);

                    response.Cities.AddRange(temp);
                    response.Message = "Dohvaćeni gradovi.";
                    response.Success = true;
                    return response;
                }
                else {
                    response.Message = "Nema gradova u toj državi.";
                    response.Success = false;
                    _logger.LogDebug("Error while getting cities:{@request} {@response}", request, response);
                    return response;
                }
            }
            catch(Exception ex) 
            {
                response.Message = ex.Message;
                response.Success = false;
                _logger.LogDebug("Error while getting cities:{@request} {@response}", request, response);
                return response;
            }
        }
    }
}
