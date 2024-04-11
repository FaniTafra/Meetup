using AutoMapper;
using KingMeetup.Messaging;
using KingMeetup.Model;
using KingMeetup.Model.IRepository;
using KingMeetup.Contract;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace KingMeetup.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, JwtSecurityTokenHandler jwtTokenHandler, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenHandler = jwtTokenHandler;
            _logger = logger;
        }

        public async Task<UserUpdateResponse> GetUser(int userId)
        {
            try
            {
                var users = await _userRepository.GetUser(userId);
 
                _logger.LogDebug("Getting user from database:{@response}", users);
                return _mapper.Map<UserUpdateResponse>(users);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error while getting user from database: {@ex}", ex);
                throw;
            }
        }

        public async Task<UserResponse> UpdateUser(int UserId, UserUpdateRequest request)
        {
            UserResponse response = new UserResponse();

            try
            {

                    User? user = _mapper.Map<User>(request);
                    if(user.Password != null) 
                    {
                        user.Password = HashUserPassword(user.Password);
                    }
                    
                    await _userRepository.Update(user, UserId);

                    response.Message = "Uspješno updatean.";
                    response.Success = true;
                    _logger.LogDebug("Updating user:{@request} {@response}", request, response);

                    return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                _logger.LogError("Error with updating user:{@request} {@response}", request, response);

                return response;
            }
        }
        private string HashUserPassword(string plainTextPassword)
        {
            string hashedPassword;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hashedPassword = GetHash(sha256Hash, plainTextPassword);
            }
            return hashedPassword;
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder? sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}
