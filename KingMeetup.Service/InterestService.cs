using AutoMapper;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using KingMeetup.Messaging.Common;
using KingMeetup.Model;
using KingMeetup.Model.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace KingMeetup.Service
{
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        public InterestService(IInterestRepository interestRepository, IMapper mapper, ILogger<AuthService> logger)
        {
            _interestRepository = interestRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<InterestResponse>> GetInterests(CancellationToken cancellationToken)
        {
            try
            {
                List<Interest> interests = await _interestRepository.GetInterests(cancellationToken);

                _logger.LogDebug("Getting interests from database:{@response}", interests);
                return _mapper.Map<List<InterestResponse>>(interests);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error while getting interests from database: {@ex}", ex);
                throw;
            }
        }

        public async Task AddUserInterest(List<UsersInterestRequest> requests, CancellationToken cancellationToken)
        {
            try
            {
                List<UsersInterest> usersInterests = _mapper.Map<List<UsersInterest>>(requests);
                await _interestRepository.AddUsersInterestAsync(usersInterests, cancellationToken);

                _logger.LogDebug("Adding users interests to database:{@request}", requests);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding interests to database:{@request} {@ex}", requests, ex);
            }
        }

        public async Task<bool> CheckUsersInterest(int userId)
        {
            return await _interestRepository.CheckUsersInterest(userId);
        }

        public async Task<List<InterestResponse>> GetUserInterests(int userId)
        {
            try
            {
                List<Interest> userInterests = await _interestRepository.GetUserInterests(userId);

                _logger.LogDebug("Getting user's interests from database:{@response}", userInterests);
                return _mapper.Map<List<InterestResponse>>(userInterests);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error while getting user's interests from database: {@ex}", ex);
                throw;
            }
        }
    }
}
