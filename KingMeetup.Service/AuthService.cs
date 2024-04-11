using AutoMapper;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using KingMeetup.Model;
using KingMeetup.Model.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KingMeetup.Service
{
    public class AuthService : IAuthService
    {
        private IConfiguration _config;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IConfiguration config, IAuthRepository authRepository, IMapper mapper, JwtSecurityTokenHandler jwtTokenHandler, ILogger<AuthService> logger)
        {
            _config = config;
            _authRepository = authRepository;
            _mapper = mapper;
            _jwtTokenHandler = jwtTokenHandler;
            _logger = logger;
        }

         public async Task<LoginResponse> Generate(LoginRequest request, CancellationToken cancellationToken)
         {
            LoginResponse response = new LoginResponse()
            {
                Request = request
            };

            try
            {
                User? user = await Authenticate(request, cancellationToken);

                if (user != null)
                {
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.Rsa, user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                        new Claim(ClaimTypes.Role, user.RoleID.ToString())
                    };
                    JwtSecurityToken token = new JwtSecurityToken(
                        _config["Jwt:Issuer"], 
                        _config["Jwt:Audience"], 
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:Exp"])),
                        signingCredentials: credentials);
                    String? userToken = _jwtTokenHandler.WriteToken(token);

                    response.Token = userToken;
                    response.Success = true;
                    _logger.LogDebug("Generating token for user in request:{@request} {@response}", request, response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Nije uspješno logiran";
                    _logger.LogDebug("Error while generating token for user in request: {@request} {@response}", request, response) ;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError("Error with user login: {@request} {@response}", request, response);

                return response;
            }  
         }

        public async Task<User?> Authenticate(LoginRequest userDto, CancellationToken cancellationToken)
        {
            return await _authRepository.FindByEmailAndPassword(userDto.Email, HashUserPassword(userDto.Password), cancellationToken);          
        }

        public async Task<UserResponse> RegisterUser(UserRequest request, CancellationToken cancellationToken)
        {
            UserResponse response = new UserResponse()
            {
                Request = request
            };

            try
            {
                User? user = _mapper.Map<User>(request);
                if (await _authRepository.FindByEmail(user.Email, cancellationToken) == null)
                {
                    user.Password = HashUserPassword(user.Password);

                    await _authRepository.Add(user, cancellationToken);

                    response.Message = "Uspješno registriran.";
                    response.Success = true;
                    _logger.LogDebug("Registering user:{@request} {@response}", request, response);

                    return response;
                }
                else 
                {                
                    throw new Exception("Vec postoji korisnik s tom email adresom.");                  
                }             
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                _logger.LogError("Error with registering user: {@request} {@response}", request, response);

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
