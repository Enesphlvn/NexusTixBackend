using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NexusTix.Domain.Entities;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace NexusTix.WebAPI.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = configuration.GetValue<bool>("JwtSettings:RequireHttpsMetadata", false);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

                        var stampClaim = context.Principal?.Claims.FirstOrDefault
                        (
                            x => x.Type == "AspNet.Identity.SecurityStamp"
                        );
                        var tokenSecurityStamp = stampClaim?.Value;

                        var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                        if (userId == null)
                        {
                            context.Fail("Token NameIdentifier (UserId) claim'i içermiyor.");
                            return;
                        }

                        var user = await userManager.FindByIdAsync(userId);

                        if (user == null || user.SecurityStamp != tokenSecurityStamp)
                        {
                            context.Fail("Security stamp doğrulaması başarısız.");
                        }
                    },

                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var responseBody = new
                        {
                            ErrorMessages = new List<string> { "Kimlik doğrulaması başarısız. Lütfen giriş yapın." },
                            Status = 401,
                            IsSuccess = false,
                            IsFail = true
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
                    },

                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var responseBody = new
                        {
                            ErrorMessages = new List<string> { "Bu işlemi yapmak için yetkiniz bulunmamaktadır." },
                            Status = 403,
                            IsSuccess = false,
                            IsFail = true
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
                    }
                };
            });

            return services;
        }
    }
}