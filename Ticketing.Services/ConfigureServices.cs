using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Services.UnitOfWork;
using Ticketing.Services.Identity.SeedDatabaseService;
using Ticketing.Services.Identity.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Ticketing.Data.Entities.Users;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Common.Enums;
using Ticketing.Data.Persistence;
using Ticketing.Services.Identity;
using Ticketing.Services.Dtos;
using Ticketing.Services.Repository.Contracts;
using Ticketing.Services.Repository.Services;

namespace Ticketing.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();

            services.AddIdentity<User, Role>(option => option.SignIn.RequireConfirmedAccount = false)
                        .AddEntityFrameworkStores<TicketingDbContext>()
                                   .AddDefaultTokenProviders();

            services.AddScoped<ITokenInfoService, TokenInfoService>();

            var jwtSettings = new JwtSettings();
            configuration.Bind(key: nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = false;
                options.Lockout.AllowedForNewUsers = false;
                options.SignIn.RequireConfirmedEmail = false;
            });


            services.AddScoped<AppUserManager>();
            services.AddScoped<AppSignInManager>();
            services.AddScoped<AppRoleManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                      JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                      JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.ClaimsIssuer = configuration["Authentication:JwtIssuer"];

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = configuration["Authentication:JwtIssuer"],

                   ValidateAudience = true,
                   ValidAudience = configuration["Authentication:JwtAudience"],

                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtKey"])),
                   RequireExpirationTime = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               };
           });


            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromDays(3));

            services.AddScoped<SeedDataBase>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
