using NetCoreStartProject.Contracts.V1.Responses.ExternalProviders;
using System.Threading.Tasks;

namespace NetCoreStartProject.Services.Identity
{
    public interface IExternalProvidersIdentityService
    {
        Task<FacebookTokenValidatorResult> ValidateFacebookAccessTokenAsync(string accessToken);
        Task<FacebookUserInfoResult> GetFacebookUserInfoAsync(string accessToken);
        Task<LinkedInAccessTokenResult> GetLinkedInCallbackAsync(string code, string state = "");
        Task<LinkedInTokenValidatorResult> ValidateLinkedInAccessTokenAsync(string accessToken);
        Task<LinkedInEmailUserInfoResult> GetLinkedInUserInfoAsync(string accessToken);

    }
}
