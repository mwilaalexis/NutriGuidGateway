using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Security.Claims;

namespace UserProfileServiceProject.Extensions
{
    public static class ApplicationDependency
    {
        public static void ConfigureAuthorizationPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {

                options.AddPolicy("BasicUserAccess", policy =>
                    policy.RequireRole("User", "Moderator", "Admin"));


                options.AddPolicy("ContentManager", policy =>
                    policy.RequireRole("Moderator", "Admin"));


                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });
        }
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

                     
                        NameClaimType = "sub",
                        RoleClaimType = ClaimTypes.Role,

                        ClockSkew = TimeSpan.FromHours(5)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices
                                .GetRequiredService<ILoggerFactory>()
                                .CreateLogger("JwtAuth");

                            logger.LogWarning(context.Exception, "JWT authentication failed");

                            return Task.CompletedTask;
                        }
                    };
                });
        }

    }
}
