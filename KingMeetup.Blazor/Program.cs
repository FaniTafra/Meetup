using Blazored.Toast;
using KingMeetup.Blazor.IService;
using KingMeetup.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using AutoMapper;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Mvc.Razor;
using KingMeetup.Service.Mapping;

namespace KingMeetup.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new InterestProfile());
                mc.AddProfile(new EventProfile());
            });


            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IInterestsService, InterestsService>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
            builder.Services.AddBlazoredToast();
            builder.Services.AddSingleton(mappingConfig.CreateMapper());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
      
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}