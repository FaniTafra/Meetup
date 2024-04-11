using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using KingMeetup.Contract;
using KingMeetup.Infrastructure;
using KingMeetup.Messaging.Validation;
using KingMeetup.Model.IRepository;
using KingMeetup.Model.Repositories;
using KingMeetup.Repository;
using KingMeetup.Service;
using KingMeetup.Service.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace KingMeetup.IOC
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureApplicationServices(services, configuration);
            ConfigureRepositories(services, configuration);
            ConfigureAuthentication(services, configuration);
        }

		public static void ConfigureLogging(this IServiceCollection services, WebApplicationBuilder builder)
		{
			builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
		}

		private static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new InterestProfile());
                mc.AddProfile(new EventProfile());
                mc.AddProfile(new LocationProfile());
            });
            
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddValidatorsFromAssemblyContaining<UserRequestValidator>(ServiceLifetime.Transient);
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddScoped<JwtSecurityTokenHandler>();

            services.AddSingleton(mappingConfig.CreateMapper());
        }

        private static void ConfigureRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MeetupDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")
            ));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IAttendeeListRepository, AttendeeListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthorization();
        }

	}
}
