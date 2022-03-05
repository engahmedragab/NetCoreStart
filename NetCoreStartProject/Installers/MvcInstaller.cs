using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCoreStartProject.Options;
using NetCoreStartProject.Services.Identity;

namespace NetCoreStartProject.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var cookieSettings = new CookieSettings();
            configuration.Bind(nameof(cookieSettings), cookieSettings);
            services.AddSingleton(cookieSettings);

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddMvc(options => { options.EnableEndpointRouting = false; });
            services.AddRazorPages();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthorization();
            services.AddAuthentication(x =>
                {
                    //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,cfg => cfg.SlidingExpiration = true);
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            //    options.SlidingExpiration = true;
            //    options.AccessDeniedPath = cookieSettings.AccessDeniedPath;
            //    options.LoginPath = cookieSettings.LoginPath;

            //});

            var multiSchemePolicy = new AuthorizationPolicyBuilder(
                                        CookieAuthenticationDefaults.AuthenticationScheme,
                                        JwtBearerDefaults.AuthenticationScheme)
                                        .RequireAuthenticatedUser()
                                        .Build();

            services.AddAuthorization(o => o.DefaultPolicy = multiSchemePolicy);

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSession();

            services.AddSwaggerGen(x =>
            {
                
                x.SwaggerDoc("v1", new OpenApiInfo{ Title = "NetCoreStart API", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0]}
                };
                
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()} 
                });
            });

        }
    }
}