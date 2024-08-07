using Application.Implementations;
using Application.Services;
using Domain;
using Domain.Repositories.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared;
using Shared.Commons;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace ApiService;

public static class ConfigureServices
{
    public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
    {
        services.Repositories(configuration);
        services.Services(configuration);
        services.Database(configuration);
        services.Misc(configuration);
    }

    public static void Repositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserRepo>();
        services.AddScoped<AppRepo>();
        services.AddScoped<WaitlistRepo>();
        services.AddScoped<UserSubscriptionRepo>(); 
        services.AddScoped<SettingRepo>();
    }

    public static void Misc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(name: MiscilenousConstants._policy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.SwaggerDoc("v1",
             new OpenApiInfo
             {
                 Title = "QuickValide",
                 Version = "v1",
                 Description = "Turn your brightest\r\nideas into Reality - Fast Simple and Effective",
             });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddGoogle(options =>
        {
            options.ClientId = configuration.GetSection("GoogleAuth:ClientId").Value!;
            options.ClientSecret = configuration.GetSection("GoogleAuth:ClientSecret").Value!;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration.GetSection("SecretKeys:JWT").Value!))
            };
        });
    }

    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IStateHelper, StateHelper>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IGPTService, GPTService>();
        services.AddHttpClient<ICloudflareService, CloudflareService>();
        
    }

    public static void Database(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<IApplicationDBContext, ApplicationDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("cs")));
    }
}
