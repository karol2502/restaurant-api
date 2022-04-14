using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Domain.Common.Models.User;
using RestaurantAPI.Domain.Common.Utils;
using RestaurantAPI.Domain.Facades;
using RestaurantAPI.Domain.Facades.Middleware;
using RestaurantAPI.Domain.Interfaces.Facades;
using RestaurantAPI.Domain.Interfaces.Infrastructure;
using RestaurantAPI.Infrastructure.EntityFramework;
using RestaurantAPI.Infrastructure.EntityFramework.Entities;
using RestaurantAPI.Infrastructure.EntityFramework.Services;

namespace RestaurantAPI.Domain.DI;

public static class DependencyInjector
{
    public static void AddDependency(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IRestaurantFcd, RestaurantFcd>();
        serviceCollection.AddScoped<IRestaurantSrv, RestaurantSrv>();

        serviceCollection.AddScoped<IAccountSrv, AccountSrv>();
        serviceCollection.AddScoped<IAccountFcd, AccountFcd>();
        serviceCollection.AddScoped<IJwtUtils, JwtUtils>();

        serviceCollection.AddDbContext<RestaurantDbContext>();
        serviceCollection.AddScoped<ErrorHandlingMiddleware>();
        serviceCollection.AddScoped<DbSeeder>();

        //JWT
        var authenticationSettings = new AuthenticationSettings();

        configuration.GetSection("Authentication").Bind(authenticationSettings);

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
            };
        });

        serviceCollection.AddSingleton(authenticationSettings);
    }
}