using System.Net;
using System.Text;
using FamilyFoundsApi.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FamilyFoundsApi.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration config, string policyName)
    {
        var allowedOriginsString = config.GetValue<string>("AllowedOrigins");
        var allowedOrigins = Array.Empty<string>();
        if (!string.IsNullOrEmpty(allowedOriginsString))
        {
            allowedOrigins = allowedOriginsString.Split(",", StringSplitOptions.TrimEntries);
        }
        
        services.AddCors(o => 
            o.AddPolicy(policyName, policy => 
                policy
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Type", "Content-Length", "Cache-Control")
                    .WithOrigins(allowedOrigins)));
    }

    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
                var jwtKey = configuration.GetValue<string>("JwtKey");

                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    LifetimeValidator = (notBefore, expires, _, _) => 
                        notBefore <= DateTime.UtcNow && expires > DateTime.UtcNow,
                    ClockSkew = TimeSpan.Zero
                };
                config.Events = new JwtBearerEvents()
                {
                    // JWT handler automatically return a WWW-Authenticate: Bearer response header
                    // containing an error code/description when a 401 response is returned
                    // stąd nie ma sensu nadpisywania eventu OnAuthenticationFailed,
                    // bo nie zostanie on wywołany
                    OnChallenge = (context) =>
                    {
                        context.HandleResponse();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        
                        return context.Response.WriteAsJsonAsync(new
                        {
                            Message = "Brak zalogowanego użytkownika"
                        });
                    }
                };
            });
    }

    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            options.AddPolicy(FFConstants.AuthorizationPolicy, cp =>
                cp.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(FFConstants.UserIdClaim)));
    }
}