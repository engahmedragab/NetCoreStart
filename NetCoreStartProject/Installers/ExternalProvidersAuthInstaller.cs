using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStartProject.Options;
using NetCoreStartProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Installers
{
    public class ExternalProvidersAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            FacebookAuthSettings facebookAuthSettings = new FacebookAuthSettings();
            configuration.Bind(nameof(FacebookAuthSettings),facebookAuthSettings);
            services.AddSingleton(facebookAuthSettings);

            LinkedInAuthSettings linkedInAuthSettings = new LinkedInAuthSettings();
            configuration.Bind(nameof(LinkedInAuthSettings), linkedInAuthSettings);
            services.AddSingleton(linkedInAuthSettings);

            services.AddHttpClient();
            services.AddSingleton<IExternalProvidersIdentityService, ExternalProvidersIdentityService>();
        }
    }
}
