using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStartProject.Data;
using NetCoreStartProject.Security;
using NetCoreStartProject.Services;

namespace NetCoreStartProject.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<DataContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //options.Password.RequiredLength = 10;
                //options.Password.RequiredUniqueChars = 3;

                options.SignIn.RequireConfirmedEmail = true;

                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
          .AddEntityFrameworkStores<DataContext>()
          .AddDefaultTokenProviders()
          .AddTokenProvider<CustomEmailConfirmationTokenProvider<IdentityUser>>("CustomEmailConfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                        o.TokenLifespan = TimeSpan.FromHours(5));

            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>
                        o.TokenLifespan = TimeSpan.FromDays(3));


            services.AddScoped<IPostService, PostService>();
        }
    }
}