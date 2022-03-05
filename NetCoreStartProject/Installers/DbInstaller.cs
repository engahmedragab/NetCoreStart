using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;
using NetCoreStartProject.Options;
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
            //services.AddDefaultIdentity<User>()
            //    .AddEntityFrameworkStores<DataContext>();

            services.AddIdentity<User, Role>(options =>
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
          .AddTokenProvider<CustomEmailConfirmationTokenProvider<User>>("CustomEmailConfirmation");


            var mailSettings = new MailSettings();
            configuration.Bind(nameof(mailSettings), mailSettings);
            services.AddSingleton(mailSettings);

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IEmailSender, MailService>();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                        o.TokenLifespan = TimeSpan.FromHours(5));

            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>
                        o.TokenLifespan = TimeSpan.FromDays(3));


            services.AddScoped<IPostService, PostService>();
        }
    }
}