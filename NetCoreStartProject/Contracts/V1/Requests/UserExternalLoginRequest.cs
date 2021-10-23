using NetCoreStartProject.Enums;

namespace NetCoreStartProject.Contracts.V1.Requests
{
    public class UserExternalLoginRequest
    {
        public string AccessToken { get; set; }

        public ExternalProvidersType ExternalProvidersType { get; set; }
    }
}